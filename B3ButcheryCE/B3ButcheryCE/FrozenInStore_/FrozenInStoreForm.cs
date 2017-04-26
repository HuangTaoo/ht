using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Util_;
using B3ButcheryCE.Rpc_.BaseInfo_;
using B3HRCE.Device_;

namespace B3HRCE.FrozenInStore_
{
    public partial class FrozenInStoreForm : Form
    {
        public FrozenInStoreForm()
        {
            InitializeComponent();
        }

        List<ClientStore> list = XmlSerializerUtil.GetClientListXmlDeserialize<ClientStore>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenSelectGoodsFrom();
        }

        void OpenSelectGoodsFrom()
        {
            var store = list.FirstOrDefault(x => x.BarCode == textBox1.Text);
            if (store == null)
            {
                MessageBox.Show("没找到对应的仓库，确认编码是否正确或者同步数据");
                return;
            }

            textBox1.Text = "";
            var f = new FrozenInStoreSelectGoodsForm(store.ID);
            f.ShowDialog();
        }

        private void FrozenInStoreForm_Load(object sender, EventArgs e)
        {
           
          
        }

        private void FrozenInStoreForm_Activated(object sender, EventArgs e)
        {
            HardwareUtil.HardWareInit();
            HardwareUtil.ScanPowerOn();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        void BarCodeRead(object sender, ScanEventArgs e)
        {
            MessageBox.Show(e.BarCode);
        }

        private void FrozenInStoreForm_Deactivate(object sender, EventArgs e)
        {
            HardwareUtil.ScanPowerOff();
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
            HardwareUtil.ScanClose();
        }
    }
}