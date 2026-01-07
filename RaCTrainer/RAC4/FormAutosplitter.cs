using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman.RAC4
{
    public partial class FormAutosplitter : Form
    {
        private enum SplitterCommand
        {
            Reset = 1,
            Split = 2,
            Pause = 3,
            Unpause = 4,
        }

        Thread dataThread;

        private MemoryMappedFile mmfFile;
        private MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        private TcpClient client;
        private NetworkStream stream;
        private int port = 9672;


        public FormAutosplitter(string ip)
        {
            InitializeComponent();

            mmfFile = MemoryMappedFile.CreateOrOpen("racman-autosplitter-lc", 256);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
            
            client = new TcpClient(ip, port);
            client.NoDelay = true;
            stream = client.GetStream();

            dataThread = new Thread(this.ManageConnection);
            dataThread.Start();
        }

        private void ManageConnection()
        {
            while (true) {
                var s1 = stream.ReadByte();
                if (s1 == -1) this.Close();
                var s2 = stream.ReadByte();
                if (s2 == -1) this.Close();
                
                var cmd = (byte)s1;
                var planet = (byte)s2;
                
                Console.WriteLine($"Got message: {cmd} & packet: {planet}");
                switch ((SplitterCommand)cmd)
                {
                    case SplitterCommand.Split:
                    case SplitterCommand.Reset:
                        WriteAutosplitterValue(2, planet);
                        WriteAutosplitterValue(0, cmd);
                        Thread.Sleep(100);
                        WriteAutosplitterValue(0, 0);
                        break;
                    case SplitterCommand.Pause:
                        WriteAutosplitterValue(1, 1);
                        break;
                    case SplitterCommand.Unpause:
                        WriteAutosplitterValue(1, 0);
                        break;
                }
            }
        }

        private void WriteAutosplitterValue(int offset, byte value)
        {
            writer?.Seek(offset, SeekOrigin.Begin);
            writer?.Write(value);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAutosplitter_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream.Close();
            client.Close();
            Application.Exit();
        }
    }
}
