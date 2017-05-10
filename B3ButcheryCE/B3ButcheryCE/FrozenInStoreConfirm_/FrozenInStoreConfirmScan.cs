using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Rpc_.BaseInfo_;
using B3ButcheryCE.Util_;
using BWP.Compact.Devices;

namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    public partial class FrozenInStoreConfirmScan : Form
    {
        public FrozenInStoreConfirmScan()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        List<ClientStore> list = XmlSerializerUtil.GetClientListXmlDeserialize<ClientStore>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            OpenSelectGoodsFrom(txtFrozenInStoreID.Text);
            txtFrozenInStoreID.Text = "";
        }
        void OpenSelectGoodsFrom(string code)
        {
            var store = list.FirstOrDefault(x => x.BarCode == code);
            if (store == null)
            {
                MessageBox.Show("没找到" + code + "对应的速冻入库单号，确认编码是否正确或者同步数据");
                return;
            }

            var f = new FrozenInStoreConfirmList(store.ID);
            f.ShowDialog();
        }
        private void FrozenInStoreConfirmScan_Deactivate(object sender, EventArgs e)
        {
            HardwareUtil.ScanPowerOff();
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        private void FrozenInStoreConfirmScan_Activated(object sender, EventArgs e)
        {
            HardwareUtil.HardWareInit();
            HardwareUtil.ScanPowerOn();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(BarCodeRead);
        }
        void BarCodeRead(object sender, ScanEventArgs e)
        {
            var result = e.BarCode.Trim();
            OpenSelectGoodsFrom(result);
        }

        private void FrozenInStoreConfirmScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnOK_Click(sender, e);
            }
        }
    }
}