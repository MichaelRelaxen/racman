using racman;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace racman.Memory 
{
    internal class RPCS3 : IPS3API 
    {
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            Int64 lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            Int64 lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        public static int TitlePollIntervalMs = 1000;
        public static int WorkerIntervalMs = 1000 / 120;

        public IntPtr ProcessHandle { get; set; }

        private readonly List<MemorySubItem> SubItems = new List<MemorySubItem>();
        private readonly object subLock = new object();

        private volatile bool MemoryWorkerStarted = false;
        private Thread workerThread;
        private volatile bool pollerStarted = false;
        private Thread pollerThread;
        private string cachedTitleId = "NOGAME";
        private int cachedPid;
        private readonly object cacheLock = new object();

        private string _rpcs3Root;

        public RPCS3(string ip) : base(ip) 
        {
        }

        public override bool IsConnected => ProcessHandle != IntPtr.Zero;

        public override bool Connect() 
        {
            if (Process.GetProcessesByName("rpcs3").Length <= 0) 
            {
                return false;
            }

            Process process = Process.GetProcessesByName("rpcs3")[0];
            ProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);

            if (ProcessHandle == IntPtr.Zero) 
            {
                return false;
            }

            RefreshGameState();
            StartTitlePoller();

            return true;
        }

        public override bool Disconnect() 
        {
            StopMemorySubWorker();
            pollerStarted = false;

            JoinThread(ref workerThread);
            JoinThread(ref pollerThread);

            lock (subLock) {
                SubItems.Clear();
            }

            try {
                if (ProcessHandle == IntPtr.Zero) return true;

                bool result = CloseHandle(ProcessHandle);
                ProcessHandle = IntPtr.Zero;
                return result;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error closing RPCS3 process handle: {ex.Message}");
                ProcessHandle = IntPtr.Zero;
                return true;
            }
        }

        private static void JoinThread(ref Thread thread) 
        {
            Thread t = thread;
            thread = null;

            if (t != null && t != Thread.CurrentThread && t.IsAlive) {
                t.Join(500);
            }
        }

        public override int getCurrentPID() 
        {
            lock (cacheLock) {
                return cachedPid;
            }
        }

        public override string getGameTitleID() 
        {
            lock (cacheLock) {
                return cachedTitleId;
            }
        }
        private string QueryGameTitleID() {
            List<string> titles;
            try {
                titles = func.GetWindowTitles("rpcs3");
            }
            catch (Exception ex) {
                Console.WriteLine($"Could not enumerate RPCS3 windows: {ex.Message}");
                return "NOGAME";
            }

            foreach (string title in titles) 
            {
                if (title.Contains("[")) {
                    Regex regex = new Regex(@"(?<=\[).*(?=\])");
                    Match match = regex.Match(title);

                    if (match.Success)
                    {
                        Console.WriteLine($"Match found: {match.Value}");

                        return match.Value;
                    }
                }
            }

            return "NOGAME";
        }

        private void RefreshGameState() {
            string title = QueryGameTitleID();
            int pid = title == "NOGAME" ? 0 : 1;

            bool changed;
            lock (cacheLock) {
                changed = cachedPid != pid || cachedTitleId != title;
                cachedTitleId = title;
                cachedPid = pid;
            }

            if (changed) {
                Console.WriteLine($"RPCS3 game state changed: {title} (pid {pid})");
                RaiseInGameChanged(pid != 0);
            }
        }

        private void StartTitlePoller() {
            if (pollerStarted) return;

            pollerStarted = true;
            pollerThread = new Thread(() => {
                while (pollerStarted) {
                    try {
                        RefreshGameState();
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"RPCS3 title poll failed: {ex.Message}");
                    }

                    Thread.Sleep(TitlePollIntervalMs);
                }
            });
            pollerThread.IsBackground = true;
            pollerThread.Name = "RPCS3 title poll";
            pollerThread.Start();
        }

        public override void Notify(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message);
        }

        public override byte[] ReadMemory(int pid, uint address, uint size) 
        {
            byte[] buffer = new byte[size];
            IntPtr bytesRead;
            ReadProcessMemory(ProcessHandle, (Int64)(address + 0x300000000), buffer, (int)size, out bytesRead);

            return buffer;
        }

        public override void WriteMemory(int pid, uint address, uint size, byte[] memory) 
        {
            WriteProcessMemory(ProcessHandle, (Int64)(address + 0x300000000), memory, memory.Length, out _);
        }

        private void MemorySubWorker() 
        {
            while (MemoryWorkerStarted) 
            {
                MemorySubItem[] items;
                lock (subLock) {
                    items = SubItems.ToArray();
                }

                foreach (MemorySubItem item in items) {
                    if (item.Released) continue;

                    bool hitConditional = false;

                    byte[] currentValue;
                    try {
                        currentValue = ReadMemory(0, item.Address, item.Size);
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"RPCS3 subscription read failed at {item.Address:X}: {ex.Message}");
                        continue;
                    }

                    if (item.Condition == MemoryCondition.Any) {
                        hitConditional = true;
                    }
                    else if (item.Condition == MemoryCondition.Changed) {
                        if (item.LastValue != null && !currentValue.SequenceEqual(item.LastValue)) {
                            hitConditional = true;
                        }
                    }

                    if (hitConditional) 
                    {
                        if (item.Freeze) {
                            WriteMemory(0, item.Address, item.SetValue);
                        }

                        if (item.Callback != null) 
                        {
                            item.Callback(currentValue.Reverse().ToArray());
                        }
                    }

                    item.LastValue = currentValue;
                }

                Thread.Sleep(WorkerIntervalMs);
            }
        }

        private void StartMemorySubWorker() 
        {
            if (MemoryWorkerStarted) return;

            MemoryWorkerStarted = true;

            workerThread = new Thread(MemorySubWorker);
            workerThread.IsBackground = true;
            workerThread.Name = "RPCS3 memory subscriptions";
            workerThread.Start();
        }

        private void StopMemorySubWorker() 
        {
            MemoryWorkerStarted = false;
        }

        public override void ReleaseSubID(int memSubID) 
        {
            lock (subLock) {
                if (memSubID < 0 || memSubID >= SubItems.Count) {
                    Console.WriteLine($"Ignoring release of unknown RPCS3 subscription {memSubID}.");
                    return;
                }

                SubItems[memSubID].Released = true;
            }
        }

        public override void ReleaseAllSubs() {
            lock (subLock) {
                foreach (MemorySubItem item in SubItems) {
                    item.Released = true;
                }
            }
        }

        public override int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback) 
        {
            MemorySubItem item = new MemorySubItem {
                Address = address,
                Size = size,
                Condition = condition,
                Callback = callback,
                SetValue = memory,
                Freeze = false,
            };

            int id;
            lock (subLock) {
                SubItems.Add(item);
                id = SubItems.Count - 1;
            }

            StartMemorySubWorker();

            return id;
        }

        public override int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory) {
            MemorySubItem item = new MemorySubItem {
                Address = address,
                Size = size,
                Condition = condition,
                SetValue = memory,
                Freeze = true,
            };

            int id;
            lock (subLock) {
                SubItems.Add(item);
                id = SubItems.Count - 1;
            }
            StartMemorySubWorker();
            return id;
        }

        public override void WriteFile(string remotePath, byte[] buffer) 
        {
            if (string.IsNullOrEmpty(_rpcs3Root))
                PromptSetRpcs3Root();

            // Convert remotePath (like "/dev_hdd0/game/…") to full local path
            string fullPath = Path.Combine(_rpcs3Root, remotePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            string dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(fullPath, buffer);
        }

        public override void WriteFile(string remotePath, string filePath) 
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Local file not found", filePath);

            if (string.IsNullOrEmpty(_rpcs3Root))
                PromptSetRpcs3Root();

            string fullPath = Path.Combine(_rpcs3Root, remotePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            string dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.Copy(filePath, fullPath, overwrite: true);
        }

        private void PromptSetRpcs3Root() 
        {
            using (var folderBrowserDialog = new FolderBrowserDialog()) 
            {
                folderBrowserDialog.Description = "Select the RPCS3 root folder";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    _rpcs3Root = folderBrowserDialog.SelectedPath;
                }
                else 
                {
                    throw new InvalidOperationException("RPCS3 root folder not selected.");
                }
            }
        }

        public override uint GetUserID() 
        {
            // not implementing this
            return 0;
        }

        public override int DeleteDirectory(string remotePath) 
        {
            throw new NotImplementedException();
        }
    }
}

internal class MemorySubItem 
{
    public uint Address;
    public uint Size;
    public IPS3API.MemoryCondition Condition;
    public bool Freeze;
    public volatile bool Released;

    public byte[] LastValue;
    public byte[] SetValue;

    public Action<byte[]> Callback;
}