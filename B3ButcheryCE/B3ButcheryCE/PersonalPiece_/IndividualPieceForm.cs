using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE;
using BWP.Compact.Devices;

namespace B3ButcheryCE.PersonalPiece_
{
    public partial class IndividualPieceForm : Form
    {
        public IndividualPieceForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        private void IndividualPieceForm_Load(object sender, EventArgs e)
        {

        }

        void BarCodeRead(object sender, ScanEventArgs e)
        {
            var result = e.BarCode.Trim();
            //OpenSelectGoodsFrom(result);
            //this.Invoke(new Action(() =>
            //{
            //    textBox1.Text = result;
            //    OpenSelectGoodsFrom();
            //}));


            //MessageBox.Show(e.BarCode);
        }

        private void IndividualPieceForm_Activated(object sender, EventArgs e)
        {
            HardwareUtil.HardWareInit();
            HardwareUtil.ScanPowerOn();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        private void IndividualPieceForm_Deactivate(object sender, EventArgs e)
        {
            HardwareUtil.ScanPowerOff();
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
        }
    }
}