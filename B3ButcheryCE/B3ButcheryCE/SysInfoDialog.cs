using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3HRCE.Device_;


namespace B3HRCE
{
    public partial class SysInfoDialog : Form
    {

        void AddInfo(string info)
        {
            Controls.Add(new Label()
            {
                Text = info,
                Dock = DockStyle.Top
            });
        }

        void AddInfo(string title, object value)
        {
            AddInfo(string.Format("{0}:{1}", title, value));
        }

        public SysInfoDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            AddInfo("当前IP", HardwareUtil.Device.GetIP());
            AddInfo("设备型号", HardwareUtil.Device.ToString());
            AddInfo("当前程序版本", GetType().Assembly.GetName().Version);
            AddInfo("手持机序列号", HardwareUtil.GetGetSerialNumber());
            AddInfo("设备名", Util.DeviceIdentName);
            AddInfo("分辨率", string.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            AddInfo("运行环境", Environment.Version);
            AddInfo("平台", string.Format("{0} {1}", Environment.OSVersion.Platform, Environment.OSVersion.Version));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}