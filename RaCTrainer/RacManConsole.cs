using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class RacManConsole : Form
    {
        static TextWriter stdout;
        static TextWriter stderr;

        static StringWriterExt consoleStd;
        static StringWriterExt consoleErr;

        static int stdPos = 0;
        static int errPos = 0;

        string filter = "";

        static List<Dictionary<string, string>> consoleOutput = new List<Dictionary<string, string>>();

        public RacManConsole()
        {
            InitializeComponent();
        }

        public static void RedirectOutput()
        {
            consoleStd = new StringWriterExt(true);
            consoleErr = new StringWriterExt(true);

            consoleStd.Flushed += new StringWriterExt.FlushedEventHandler(stdWriter);
            consoleErr.Flushed += new StringWriterExt.FlushedEventHandler(errWriter);

            stdout = Console.Out;
            stderr = Console.Error;

            Console.SetOut(consoleStd);
            Console.SetError(consoleErr);
        }

        public void WriteStandard(Dictionary<string, string> element)
        {
            if (!element["text"].Contains(filter))
            {
                return;
            }

            this.Invoke((MethodInvoker)delegate
            {
                consoleTextBox.Text += $"[{element["timestamp"]}] {element["text"]}";

                if (autoscrollCheckbox.Checked)
                {
                    consoleTextBox.SelectionStart = consoleTextBox.Text.Length;
                    consoleTextBox.ScrollToCaret();
                }
            });
        }

        public void WriteError(Dictionary<string, string> element)
        {
            if (!element["text"].Contains(filter))
            {
                return;
            }

            this.Invoke((MethodInvoker)delegate
            {
                consoleTextBox.SelectionStart = consoleTextBox.TextLength;
                consoleTextBox.SelectionLength = 0;
                consoleTextBox.SelectionColor = Color.Red;

                consoleTextBox.AppendText($"[{element["timestamp"]}] {element["text"]}");

                consoleTextBox.SelectionColor = consoleTextBox.ForeColor;

                if (autoscrollCheckbox.Checked)
                {
                    consoleTextBox.SelectionStart = consoleTextBox.Text.Length;
                    consoleTextBox.ScrollToCaret();
                }
            });
        }

        static void stdWriter(object sender, EventArgs args)
        {
            if (sender.ToString().Length < stdPos)
            {
                stdPos = 0;
            }

            string output = sender.ToString().Substring(stdPos);
            stdPos += output.Length;

            if (stdout != null)
            {
                stdout.Write(output);
            }

            foreach (var line in output.Split(new char[] { '\n' }))
            {
                if (line.Trim() == "")
                {
                    continue;
                }

                var stdElement = new Dictionary<string, string> { 
                    ["type"] = "std", 
                    ["text"] = line, 
                    ["timestamp"] = DateTime.Now.TimeOfDay.ToString()
                };

                consoleOutput.Add(stdElement);

                if (!AttachPS3Form.console.IsDisposed && AttachPS3Form.console.IsHandleCreated)
                {
                    (AttachPS3Form.console).WriteStandard(stdElement);
                }
            }
        }

        static void errWriter(object sender, EventArgs arg)
        {
            if (sender.ToString().Length < errPos)
            {
                errPos = 0;
            }

            string output = sender.ToString().Substring(errPos);
            errPos += output.Length;

            if (stderr != null)
            {
                stderr.Write(output);
            }

            foreach (var line in output.Split(new char[] { '\n' }))
            {
                if (line.Trim() == "")
                {
                    continue;
                }

                var errElement = new Dictionary<string, string> { 
                    { "type", "err" }, 
                    { "text", line },
                    { "timestamp", DateTime.Now.TimeOfDay.ToString() }
                };

                consoleOutput.Add(errElement);

                if (!AttachPS3Form.console.IsDisposed && AttachPS3Form.console.IsHandleCreated)
                {
                    (AttachPS3Form.console).WriteError(errElement);
                }
            }
        }

        private void RacManConsole_Shown(object sender, EventArgs e)
        {
            foreach (var item in consoleOutput)
            {
                if (item["type"] == "std")
                {
                    this.WriteStandard(item);
                }
                else
                {
                    this.WriteError(item);
                }
            }
        }

        private void filterComboBox_TextChanged(object sender, EventArgs e)
        {
            filter = "";
            
            consoleTextBox.SuspendLayout();

            if (filterComboBox.Text != "All")
            {
                filter = filterComboBox.Text;
            }

            consoleTextBox.Clear();

            foreach(var item in consoleOutput)
            {
                if (item["text"].Contains(filter))
                {
                    if (item["type"] == "std")
                    {
                        WriteStandard(item);
                    } else
                    {
                        WriteError(item);
                    }
                }
            }

            consoleTextBox.ResumeLayout();
        }

        private void RacManConsole_Load(object sender, EventArgs e)
        {
 
        }

        private void RacManConsole_Activated(object sender, EventArgs e)
        {

        }

        private void filterComboBox_DropDown(object sender, EventArgs e)
        {
            filterComboBox.Items.Clear();
            filterComboBox.Items.Add("All");

            foreach (Mod mod in ModLoaderForm.mods)
            {
                if (mod.loaded)
                {
                    filterComboBox.Items.Add($"{mod.name}");
                }
            }
        }
    }

    public class StringWriterExt : StringWriter
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public delegate void FlushedEventHandler(object sender, EventArgs args);
        public event FlushedEventHandler Flushed;
        public virtual bool AutoFlush { get; set; }

        public StringWriterExt()
            : base() { }

        public StringWriterExt(bool autoFlush)
            : base() { this.AutoFlush = autoFlush; }

        protected void OnFlush()
        {
            var eh = Flushed;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        public override void Flush()
        {
            base.Flush();
            OnFlush();
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (AutoFlush) Flush();
        }

        public override void Write(string value)
        {
            base.Write(value);
            if (AutoFlush) Flush();
        }

        public override void Write(char[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
            if (AutoFlush) Flush();
        }
    }
}
