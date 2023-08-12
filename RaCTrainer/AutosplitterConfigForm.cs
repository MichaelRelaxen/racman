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
        public Route SelectedRoute => routeSelectionListBox.SelectedItem as Route;
        private const string filePath = "usr";
        private static int maxRows = (AutosplitterHelper.mmfConfigBytes / 2);

        private int previousIndex = -1;

        // Basically just a byte list and a string
        public class Route
        {
            public List<byte> bytes;
            public byte[] ByteArray => bytes.ToArray();

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = GetUniqueName(value);
                }
            }
            public static AutosplitterConfigForm form;

            public override string ToString() => Name;
            public Route()
            {
                bytes = new List<byte>();
                Name = "Untitled";
            }

            public static string GetUniqueName(string value)
            {
                string nv = value;
                int i = 2;
                while (form.DoesNameExist(nv))
                {
                    nv = $"{value} ({i})";
                    i++;
                }
                return nv;
            }
        }

        public AutosplitterConfigForm()
        {
            InitializeComponent();

            Route.form = this;

            removeButton.Enabled = false;
            grid.Enabled = false;
            applyChangesButton.Enabled = false;
            textBox1.Enabled = false;

            LoadStuffFromDisk();
        }

        public bool TrySelectRoute(string name)
        {
            if (DoesNameExist(name))
            {
                foreach (Route r in routeSelectionListBox.Items)
                {
                    if (r.Name == name)
                    {
                        routeSelectionListBox.SelectedItem = r;
                        return true;
                    }
                }
                return false; // should never happen
            }
            else return false;
        }

        private void LoadRoute(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var route = new Route();
            route.bytes.AddRange(bytes);
            route.Name = Path.GetFileNameWithoutExtension(path);
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

        // Double clicking a list item to change its name
        private void routeSelectionListBox_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedRoute != null)
            {
                var dialog = new SimpleInputDialogForm(defaultInput: SelectedRoute.Name);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string oldName = SelectedRoute.Name;
                    string newName = dialog.inputTextBox.Text;


                    if (oldName != newName)
                    {
                        SelectedRoute.Name = newName;

                        // Rename the file
                        if (File.Exists($"usr/{oldName}.usr"))
                        {
                            File.Move($"usr/{oldName}.usr", $"usr/{SelectedRoute.Name}.usr");
                        }
                    }
                    else
                    {
                        // This is stupid
                        SelectedRoute.Name = "";
                        SelectedRoute.Name = newName;
                    }

                    // goofy ahh thing I got from stackoverflow but it doesn't reflect the changes in listview withoug it
                    routeSelectionListBox.Items[routeSelectionListBox.SelectedIndex] = routeSelectionListBox.SelectedItem;
                }
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
            removeButton.Enabled = true;


            if (SelectedRoute != null)
            {
                

                textBox1.Text = SelectedRoute.Name;
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
            if (grid.Rows.Count - 1 > maxRows)
            {
                MessageBox.Show($"That's too many rows. The maximum is {maxRows}. Your changes have not been saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string oldName = SelectedRoute.Name;
            string newName = textBox1.Text;


            if (oldName != newName)
            {
                SelectedRoute.Name = newName;

                // Rename the file
                if (File.Exists($"usr/{oldName}.usr"))
                {
                    File.Move($"usr/{oldName}.usr", $"usr/{SelectedRoute.Name}.usr");
                }
            }
            else
            {
                // This is stupid
                SelectedRoute.Name = "";
                SelectedRoute.Name = newName;
            }
            SelectedRoute.bytes = new List<byte>(CommitDropdownChangesStuff());
            routeSelectionListBox.Items[routeSelectionListBox.SelectedIndex] = routeSelectionListBox.SelectedItem;

            // Save route to disk
            string filename = $"usr/{SelectedRoute.Name}.usr";
            try
            {
                File.WriteAllBytes(filename, SelectedRoute.ByteArray);
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
            var result = MessageBox.Show($"This will remove the split route ({SelectedRoute.Name}) from your computer. This action cannot be undone! Are you sure you want to proceed?",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                File.Delete($"usr/{SelectedRoute.Name}.usr");
                routeSelectionListBox.Items.Remove(SelectedRoute);
                grid.Rows.Clear();
                removeButton.Enabled = false;
                grid.Enabled = false;
                applyChangesButton.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = string.Empty;
            }
        }

        private void openFromFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);

            if (DoesNameExist(Path.GetFileNameWithoutExtension(openFileDialog1.FileName)))
            {
                MessageBox.Show("Copy could not be completed because a file with the same name exists. Please remove or rename the other file.");
                return;
            }
            else
            {
                File.Copy(openFileDialog1.FileName, $"usr/{Path.GetFileName(openFileDialog1.FileName)}");
            }
            LoadRoute($"usr/{Path.GetFileName(openFileDialog1.FileName)}");
            MessageBox.Show($"Loaded {Path.GetFileNameWithoutExtension(openFileDialog1.FileName)} from {openFileDialog1.FileName}");
        }

        private bool DoesNameExist(string name)
        {
            foreach (object o in routeSelectionListBox.Items)
            {
                if ((o as Route)?.Name.ToLower() == name.ToLower()) return true;
            }
            return false;
        }

        private void insertAboveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.Rows.Insert(contextMenuRow, null, null);
        }

        private void insertBelowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.Rows.Insert(contextMenuRow + 1, null, null);
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.Rows.RemoveAt(contextMenuRow);
        }

        private int contextMenuRow = 0;
        private void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuRow = e.RowIndex;
                if (contextMenuRow == grid.Rows.Count - 1)
                {
                    insertBelowToolStripMenuItem.Enabled = false;
                    deleteRowToolStripMenuItem.Enabled = false;
                }
                else
                {
                    insertBelowToolStripMenuItem.Enabled = true;
                    deleteRowToolStripMenuItem.Enabled = true;
                }
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
    }
}
