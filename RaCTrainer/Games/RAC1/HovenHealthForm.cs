using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class HovenHealthForm : Form
    {
        public HovenHealthForm()
        {
            InitializeComponent();
        }
        uint[] turretPvars = new uint[5];
        int turretCount = 0;
        float[] turretHealth = new float[5];

        bool showHealth = false;
        void GetTurrets()
        {
            uint mobysPtr = Convert.ToUInt32(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, 0xA390A0, 4), 16);
            Console.WriteLine($"Moby Ptr: {mobysPtr.ToString("X4")}");

            for (uint i = 590; i < 0x2b7; i++)
            {
                ushort mobyType = Convert.ToUInt16(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, mobysPtr + i * 0x100 + 0xA6, 2), 16);
                Console.WriteLine($"Moby Type: {mobyType.ToString("X2")}, i = {i}");

                if (mobyType == 0x4FE)
                {
                    uint pvarPtr = Convert.ToUInt32(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, mobysPtr + i * 0x100 + 0x78, 4), 16);

                    turretPvars[turretCount] = pvarPtr;

                    turretCount++;
                }

                if (turretCount >= 5)
                {
                    turretCount = 0;
                    break;
                }
            }
        }

        async Task ShowHealth()
        {
            while(showHealth == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    turretHealth[i] = func.HexToFloat(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, turretPvars[i] + 0x80, 4));

                    if(turretHealth[4] > 0)
                    {
                        hpLabel.Text = $"1: {turretHealth[0]}\n2: {turretHealth[1]}\n3: {turretHealth[2]}\n4: {turretHealth[3]}\n5: {turretHealth[4]}";
                    }
                    await Task.Delay((int)16.66667); 
                }
            }
        }


        void refreshBtn_Click(object sender, EventArgs e)
        {
            // LOL
            showHealth = false;
            GetTurrets();
            showHealth = true;
            _ = ShowHealth();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void HovenHealthForm_Load(object sender, EventArgs e)
        {

        }
    }
}