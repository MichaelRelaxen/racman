using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace racman
{
    public partial class AutosplitterConfigForm : Form
    {
        public AutosplitterHelper helper;
        public Ratchetron api;

        public Route SelectedRoute => routeSelectionListBox.SelectedItem as Route;
        private const string filePath = "usr";

        private int previousIndex = -1;

        public AutosplitterConfigForm()
        {
            InitializeComponent();

            grid.Enabled = false;
            applyChangesButton.Enabled = false;
            textBox1.Enabled = false;

            LoadStuffFromDisk();
        }

        private void LoadRoute(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var route = new Route();
            route.bytes.AddRange(bytes);
            route.name = Path.GetFileNameWithoutExtension(path);
            routeSelectionListBox.Items.Add(route);
        }

        private void LoadStuffFromDisk()
        {
            Directory.CreateDirectory(filePath);
            foreach (string filename in Directory.GetFiles(filePath))
            {
                LoadRoute(filename);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            routeSelectionListBox.Items.Add(new Route());
        }

        // Basically just a byte list and a string
        public class Route
        {
            public List<byte> bytes;
            public byte[] ByteArray => bytes.ToArray();
            public string name;

            public override string ToString() => name;
            public Route()
            {
                bytes = new List<byte>();
                name = "Unknown";
            }
        }

        // Double clicking a list item to change its name
        //TODO having duplicate names causes weird issues with saving/deleting
        private void routeSelectionListBox_DoubleClick(object sender, EventArgs e)
        {
            var dialog = new SimpleInputDialogForm(defaultInput: SelectedRoute.name);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedRoute.name = dialog.inputTextBox.Text;

                // goofy ahh thing I got from stackoverflow but it doesn't reflect the changes in listview withoug it
                routeSelectionListBox.Items[routeSelectionListBox.SelectedIndex] = routeSelectionListBox.SelectedItem;
            }
        }

        private void routeSelectionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (routeSelectionListBox.SelectedIndex == previousIndex) return;
            previousIndex = routeSelectionListBox.SelectedIndex;

            // enable stuff
            applyChangesButton.Enabled = true;
            textBox1.Enabled = true;
            grid.Enabled = true;


            if (SelectedRoute != null)
            {
                

                textBox1.Text = SelectedRoute.name;
                if (SelectedRoute.bytes.Count != 0) grid.RowCount = SelectedRoute.bytes.Count / 2;

                // load dropdowns from route's bytes
                grid.Rows.Clear();
                if (SelectedRoute.bytes.Count > 0)
                {
                    for (int i = 0; i < SelectedRoute.bytes.Count(); i++)
                    {
                        if (i % 2 == 0) grid.Rows.Add();
                        var cell = grid.Rows[i / 2].Cells[i % 2] as DataGridViewComboBoxCell;
                        cell.Value = SelectedRoute.bytes[i] == 0x69 ? null : cell.Items[SelectedRoute.bytes[i] - 1];
                    }
                }
            }
  
        }

        // save stuff
        private void applyChangesButton_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count - 1 > 64)
            {
                MessageBox.Show("That's too many rows. The maximum is 64. Your changes have not been saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SelectedRoute.name = textBox1.Text;
            SelectedRoute.bytes = new List<byte>(CommitDropdownChangesStuff());
            routeSelectionListBox.Items[routeSelectionListBox.SelectedIndex] = routeSelectionListBox.SelectedItem;

            // Save route to disk
            // If the user renames the route it'll make a new file, hopefully that won't cause an issue
            // TODO yes this does cause an issue pls fix
            string filename = $"usr/{SelectedRoute.name}.usr";
                File.WriteAllBytes(filename, SelectedRoute.ByteArray);
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(
                    "Unable to create or write to file. This may be caused by an invalid name. Your changes have been saved locally but not to disk.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // actually save stuff
        private byte[] CommitDropdownChangesStuff()
        {
            int row = 0, col = 0;
            int i = 0;
            byte[] data = new byte[(grid.Rows.Count-1)*2];
            while (row < grid.Rows.Count-1)
            {
                var cell = grid[col, row] as DataGridViewComboBoxCell;
                if (cell != null)
                {
                    if (cell.Value != null)
                    {
                        data[i] = (byte) (cell.Items.IndexOf(cell.Value) + 1);
                    }
                    else
                    {
                        data[i] = 0x69;
                    }
                    i++;

                    col++;
                    if (col == 2)
                    {
                        col = 0;
                        row++;
                    }
                }
            }
            return data;
        }

        // disable horrible mousewheel thing and also double click thing that you cant turn off
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.grid.EditingControl is DataGridViewComboBoxEditingControl editingControl)
            {
                editingControl.DroppedDown = true;
                editingControl.MouseWheel += mouseWheelHandler;
            }
        }

        private void mouseWheelHandler(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"This will remove the split route ({SelectedRoute.name}) from your computer. This action cannot be undone! Are you sure you want to proceed?",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                File.Delete($"usr/{SelectedRoute.name}.usr");
                routeSelectionListBox.Items.Remove(SelectedRoute);
                grid.Rows.Clear();

                grid.Enabled = false;
                applyChangesButton.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void openFromFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            Console.WriteLine(openFileDialog1.FileName);
            File.Copy(openFileDialog1.FileName, $"usr/{Path.GetFileName(openFileDialog1.FileName)}");
            LoadRoute($"usr/{Path.GetFileName(openFileDialog1.FileName)}");
        }
    }
}
