using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace racman.RAC3
{
    public partial class ActiveMobysForm : Form
    {
        public rac3 game;
        private static IPS3API api = func.api;
        private static int pid = AttachPS3Form.pid;
        public Timer timer = new Timer();

        private uint activeTable = 0xEF56BC;
        private uint backupTable = 0xEF55BC;
        private uint activeMobyTags = 0xef57c8;
        private uint activeMobyCount = 0xef57bc;


        public ActiveMobysForm()
        {
            InitializeComponent();

            // so it doesnt flicker
            typeof(ListView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(table, true, null);

            timer.Interval = 16;
            timer.Tick += new EventHandler(UpdateList);
            timer.Enabled = true;
        }

        enum oClasses
        {
            Ranger = 6886,
            Tealanoid = 5860,
            Lasernoid = 6306,
            Shipanoid = 6577,
            Tyhrranoid = 5821,
            Dropship = 6800,
            Firefly = 6619,
            HorrifyingDude = 5805,
            Qwark = 5835,
            LowPolyDude = 5849,
            Bridge = 6319,
            IndoorsLight = 3287,
            OutdoorsLight = 7841,
            Slammer = 6333,
            FallingPlatform = 6310,
            UnkManager = 5988
        }

        private void ActiveMobysForm_Load(object sender, EventArgs e)
        {

        }

        private void UpdateList(object sender, EventArgs e)
        {
            int topIndex = table.TopItem?.Index ?? 0;

            table.BeginUpdate();
            table.Items.Clear();

            uint count = api.ReadMemory(pid, activeMobyCount);
            for (int i = 0; i < count; i++)
            {
                uint backup = api.ReadMemory(pid, (uint)(backupTable + i * 4));
                uint active = api.ReadMemory(pid, (uint)(activeTable + i * 4));
                uint tags = api.ReadMemory(pid, (uint)(activeMobyTags + i * 4));
                int backup_oClass = BitConverter.ToUInt16(api.ReadMemory(pid, backup + 0xAA, 2).Reverse().ToArray(), 0);
                int active_oClass = BitConverter.ToUInt16(api.ReadMemory(pid, active + 0xAA, 2).Reverse().ToArray(), 0);

                int backup_uid = BitConverter.ToUInt16(api.ReadMemory(pid, backup + 0xb2, 2).Reverse().ToArray(), 0);
                int active_uid = BitConverter.ToUInt16(api.ReadMemory(pid, active + 0xb2, 2).Reverse().ToArray(), 0);

                string backupName = Enum.GetName(typeof(oClasses), backup_oClass);
                string activeName = Enum.GetName(typeof(oClasses), active_oClass);

                ListViewItem item = new ListViewItem($"{active.ToString("X")}, uid: {active_uid}, o: {active_oClass} {activeName}");
                item.SubItems.Add($"{backup.ToString("X")}, uid: {backup_uid}, o: {backup_oClass} {backupName}");
                item.SubItems.Add(tags.ToString());
                table.Items.Add(item);
            }

            table.EndUpdate();

            if (topIndex > 0 && topIndex < table.Items.Count)
                table.TopItem = table.Items[topIndex];
        }
    }
}
