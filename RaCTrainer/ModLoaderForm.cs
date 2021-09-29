using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class ModLoaderForm : Form
    {
        static Mod[] mods;

        public ModLoaderForm()
        {
            InitializeComponent();

            if (mods == null)
            {
                mods = this.LoadMods().ToArray();
            }

            foreach (var mod in mods)
            {
                this.modsCheckedListBox.Items.Add(mod.name, mod.loaded);
            }
        }

        public List<Mod> LoadMods()
        {
            string gameModFolder = $"{ Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\";

            if (!Directory.Exists(gameModFolder))
            {
                return new List<Mod>();
            }

            var modFolders = Directory.EnumerateDirectories(gameModFolder);

            List<Mod> mods = new List<Mod>();

            foreach (var modFolder in modFolders)
            {
                if (!File.Exists($"{modFolder}\\patch.txt"))
                {
                    continue;
                }

                var mod = new Mod();
                mod.modFolder = modFolder;

                var patchFileStream = File.OpenRead($"{modFolder}\\patch.txt");

                using (StreamReader reader = new StreamReader(patchFileStream))
                {
                    mod.patchLines = reader.ReadToEnd().Split('\n').ToList();

                    foreach (var patchLine in mod.patchLines)
                    {
                        if (patchLine.Length > 2 && patchLine.Substring(0, 2) == "#-")
                        {
                            var patchLineComponents = patchLine.Split(':');
                            if (patchLineComponents.Length < 2)
                            {
                                continue;
                            }

                            var key = patchLineComponents[0].Substring(2).Trim();
                            var value = patchLineComponents[1].Trim();

                            mod.variables[key] = value;
                        }
                    }
                }

                if (mod.variables.ContainsKey("name"))
                {
                    mod.name = mod.variables["name"];
                }
                else
                {
                    mod.name = new DirectoryInfo(modFolder).Name;
                }

                mods.Add(mod);
            }

            return mods;
        }

        private void modsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                ModLoaderForm.mods[e.Index].Load();
            }
            else
            {
                ModLoaderForm.mods[e.Index].Unload();
            }
        }
    }

    public class Mod
    {
        public string name;
        public string version;

        public bool loaded = false;

        public string modFolder = "";

        public Dictionary<string, string> variables = new Dictionary<string, string>();
        public List<string> patchLines = new List<string>();
        public Dictionary<uint, byte[]> originalData = new Dictionary<uint, byte[]>();

        private void LoadOriginalData()
        {
            foreach (string patch in patchLines)
            {
                if (patch.Length < 2 || patch[0] == '#')
                {
                    continue;
                }

                var patchComponents = patch.Split(':');
                var addressString = patchComponents[0].Trim();
                uint address = UInt32.Parse(addressString.Substring(addressString.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber);
                var value = patchComponents[1].Trim();

                byte[] patchBytes;

                if (value.Contains("0x"))
                {
                    patchBytes = BitConverter.GetBytes(UInt32.Parse(value.Substring(value.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber)).Reverse().ToArray();
                }
                else
                {
                    patchBytes = File.ReadAllBytes($"{modFolder}\\{value}");
                }

                int bytesRead = 0;
                byte[] bytesToWrite = new byte[] { };
                List<byte> ogData = new List<byte>();
                while (bytesRead < patchBytes.Length)
                {
                    bytesToWrite = patchBytes.Skip(bytesRead).Take(1024).ToArray();

                    Ratchetron api = (Ratchetron)func.api;
                    ogData.AddRange(api.ReadMemory(AttachPS3Form.pid, address + (uint)bytesRead, (uint)bytesToWrite.Length));

                    bytesRead += bytesToWrite.Length;
                }

                originalData[address] = ogData.ToArray();
            }
        }

        public void Load()
        {
            Console.WriteLine($"Loading mod: {this.name}");

            if (this.originalData.Keys.Count <= 0)
            {
                this.LoadOriginalData();
            }

            Ratchetron api = (Ratchetron)func.api;
            //api.WriteMemory(AttachPS3Form.pid, 0xe1638, 4, new byte[] { 0x42, 0x80, 0xff, 0xfc });


            foreach (string patch in patchLines)
            {
                if (patch.Length < 2 || patch[0] == '#')
                {
                    continue;
                }

                var patchComponents = patch.Split(':');
                var addressString = patchComponents[0].Trim();
                uint address = UInt32.Parse(addressString.Substring(addressString.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber);
                var value = patchComponents[1].Trim();

                byte[] patchBytes;

                if (value.Contains("0x"))
                {
                    patchBytes = BitConverter.GetBytes(UInt32.Parse(value.Substring(value.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber)).Reverse().ToArray();
                }
                else
                {
                    patchBytes = File.ReadAllBytes($"{modFolder}\\{value}");
                }

                int bytesWritten = 0;
                byte[] bytesToWrite = new byte[] { };
                while (bytesWritten < patchBytes.Length)
                {
                    bytesToWrite = patchBytes.Skip(bytesWritten).Take(1024).ToArray();

                    api.WriteMemory(AttachPS3Form.pid, address + (uint)bytesWritten, (uint)bytesToWrite.Length, bytesToWrite);

                    bytesWritten += bytesToWrite.Length;
                }
            }

            //api.WriteMemory(AttachPS3Form.pid, 0xe1638, 4, new byte[] { 0x38, 0x60, 0x00, 0x01 });
        }

        public void Unload()
        {
            foreach (KeyValuePair<uint, byte[]> entry in this.originalData)
            {
                int bytesWritten = 0;
                byte[] bytesToWrite = new byte[] { };
                while (bytesWritten < entry.Value.Length)
                {
                    bytesToWrite = entry.Value.Skip(bytesWritten).Take(1024).ToArray();

                    Ratchetron api = (Ratchetron)func.api;
                    api.WriteMemory(AttachPS3Form.pid, entry.Key + (uint)bytesWritten, (uint)bytesToWrite.Length, bytesToWrite);

                    bytesWritten += bytesToWrite.Length;
                }
            }
        }
    }

}
