using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BWP.Compact.Devices;

namespace B3ButcheryCE
{
    public partial class ScanTest : Form
    {
        public ScanTest()
        {
            InitializeComponent();
            Util.SetSceen(this);
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReader);

        }

        public void Device_ScannerReader(object sender, ScanEventArgs e)
        {
            textBox1.Text = e.BarCode;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Focused)
            {
                MessageBox.Show("1111");
            }
            else if (comboBox2.Focused)
            {
                MessageBox.Show("222222222");
            }
        }
    }
}