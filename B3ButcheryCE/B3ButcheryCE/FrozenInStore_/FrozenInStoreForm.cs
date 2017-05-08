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
            Util.SetSceen(this);
        }

        List<ClientStore> list = XmlSerializerUtil.GetClientListXmlDeserialize<ClientStore>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenSelectGoodsFrom(textBox1.Text);
            textBox1.Text = "";
        }

        void OpenSelectGoodsFrom(string code)
        {
            var store = list.FirstOrDefault(x => x.BarCode == code);
            if (store == null)
            {
                MessageBox.Show("没找到" + code + "对应的仓库，确认编码是否正确或者同步数据");
                return;
            }
           
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
            var result = e.BarCode.Trim();
            OpenSelectGoodsFrom(result);
            //this.Invoke(new Action(() =>
            //{
            //    textBox1.Text = result;
            //    OpenSelectGoodsFrom();
            //}));
               
           
            //MessageBox.Show(e.BarCode);
        }

        private void FrozenInStoreForm_Deactivate(object sender, EventArgs e)
        {
            HardwareUtil.ScanPowerOff();
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
            //HardwareUtil.ScanClose();
        }
    }
}