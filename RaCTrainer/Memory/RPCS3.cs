using racman;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Markup;


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


        public IntPtr ProcessHandle { get; set; }

        private List<MemorySubItem> SubItems = new List<MemorySubItem>();

        Mutex SubMutex = new Mutex(false);

        bool MemoryWorkerStarted = false;

        public RPCS3(string ip) : base(ip)
        {
        }

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

            return true;
        }

        public override bool Disconnect()
        {
            MemoryWorkerStarted = false;

            try
            {
                return CloseHandle(ProcessHandle);
            } catch (Exception)
            {
                return true;
            }
        }

        public override int getCurrentPID()
        {
            return getGameTitleID() == "NOGAME" ? 0 : 1;
        }

        public override string getGameTitleID()
        {
            List<string> titles = func.GetWindowTitles("rpcs3");

            foreach(string title in titles)
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

        public override int MemSubIDForAddress(uint address)
        {
            throw new NotImplementedException();
        }

        public override void Notify(string message)
        {
            MessageBox.Show(message);
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
                SubMutex.WaitOne(10000000);

                for (int i = 0; i < SubItems.Count; i++)
                {
                    MemorySubItem item = SubItems[i];

                    if (item.Released) continue;
                    

                    bool hitConditional = false;

                    byte[] currentValue = ReadMemory(0, item.Address, item.Size);

                    if (item.Condition == MemoryCondition.Any)
                    {
                        hitConditional = true;
                    }
                    else if (item.Condition == MemoryCondition.Changed)
                    {
                        if (item.LastValue != null && !currentValue.SequenceEqual(item.LastValue))
                        {
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
                    SubItems[i] = item;
                }

                Thread.Sleep(1000 / 120);

                SubMutex.ReleaseMutex();
            }
        }

        private void StartMemorySubWorker()
        {
            MemoryWorkerStarted = true;

            Thread thread = new Thread(MemorySubWorker);
            thread.Start();
        }

        private void StopMemorySubWorker()
        {
            MemoryWorkerStarted = false;
        }

        public override void ReleaseSubID(int memSubID)
        {
            var subItem = SubItems[memSubID];
            subItem.Released = true;

            SubMutex.WaitOne(10000);
            SubItems[memSubID] = subItem;
            SubMutex.ReleaseMutex();
        }

        public override int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback)
        {
            MemorySubItem item = new MemorySubItem();
            item.Address = address;
            item.Size = size;
            item.Condition = condition;
            item.Callback = callback;
            item.SetValue = memory;
            item.Freeze = false;

            SubMutex.WaitOne(10000);
            SubItems.Add(item);
            SubMutex.ReleaseMutex();

            if (SubItems.Count == 1)
            {
                StartMemorySubWorker();
            }

            return SubItems.Count-1;
        }

        public override int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory)
        {
            MemorySubItem item = new MemorySubItem();
            item.Address = address;
            item.Size = size;
            item.Condition = condition;
            item.SetValue = memory;
            item.Freeze = true;

            SubMutex.WaitOne(10000);
            SubItems.Add(item);
            SubMutex.ReleaseMutex();

            if (SubItems.Count == 1)
            {

            }

            return SubItems.Count - 1;
        }

    }
}

internal struct MemorySubItem
{
    public uint Address;
    public uint Size;
    public IPS3API.MemoryCondition Condition;
    public bool Freeze;
    public bool Released;

    public byte[] LastValue;
    public byte[] SetValue;

    public Action<byte[]> Callback;
}