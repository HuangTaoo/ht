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
using B3HRCE;
using B3ButcheryCE.Rpc_;
using B3HRCE.OutputStatistics_;
using B3HRCE.Device_;
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using B3HRCE.Rpc_;
using B3HRCE.Rpc_.ClientProductInStore_;

namespace B3ButcheryCE.ProductInStore_
{
    public partial class ProductInStoreForm : Form
    {
        public ProductInStoreForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }
        ClientAllGoods mClientGoods;
        Dictionary<string, decimal> dClienGoods=new Dictionary<string,decimal>();
        List<ClientAllGoods> mGoodsList = XmlSerializerUtil.GetClientListXmlDeserialize<ClientAllGoods>();
        decimal mSumNumber = 0;

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {

                //ClientProduceOutputBillSave clientDmo = new ClientProduceOutputBillSave
                //{
                //    AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                //    Department_ID = SysConfig.Current.Department_ID ?? 0,
                //    Domain_ID = SysConfig.Current.Domain_ID,
                //    User_ID = SysConfig.Current.User_ID,
                //    CreateTime = DateTime.Now
                //};
                //ClientProductInStore productInStore = new ClientProductInStore
                //{
                //    Department_ID = SysConfig.Current.Department_ID ?? 0,
                //    InStoreType_ID = 01,
                //};
                var clientDetail = new ClientAllGoods();
                clientDetail.Goods_ID = mClientGoods.Goods_ID;
                clientDetail.Goods_Number = decimal.Parse(txtNumber.Text);
                //productInStore.GoodsDetails.Add(clientDetail);
                mGoodsList.Add(clientDetail);
                //XmlSerializerUtil.ClientXmlSerializer(clientDetail);

                //SyncBillUtil.SyncProductOut();


                MessageBox.Show("操作成功");
                txtNumber.Text = "";
                textBox1.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); ;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {

        }

        private void cbxGoods_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {

                var secondNumber = decimal.Parse(textBox1.Text);
                var productId = cbxGoods.SelectedValue.ToString();
                decimal number = 0;

                if (dClienGoods.ContainsKey(productId))
                {
                    number = dClienGoods[productId];
                 // number= ( dClienGoods[nub] = dClienGoods[nub] + ClientUtil.GetNumberBySecondNumber(mClientGoods, secondNumber));
                }
                else
                {
                    dClienGoods.Add(cbxGoods.SelectedValue.ToString(), number = ClientUtil.GetNumberBySecondNumber(mClientGoods, secondNumber) ?? 0);
                }

                
                //mSumNumber += number ?? 0;
                txtNumber.Text = number.ToString() ;// mSumNumber.ToString();
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int GetSelectIndexByCode(string code)
        {
            int index = -1;
            foreach (var item in mGoodsList)
            {
                index++;
                if (item.Goods_BarCode == code)
                {
                    return index;
                }
            }
            return index;
        }
        void BarCodeRead(object sender, ScanEventArgs e)
        {
            var result = e.BarCode.Trim();
            var index = GetSelectIndexByCode(result);
            if (index == -1)
            {
                MessageBox.Show("没有找到编码： " + result + " 对应的存货");
            }


            this.Invoke(new Action(() =>
            {
                cbxGoods.SelectedIndex = index;
                SelectIndexChanged();

            }));
        }
        void SelectIndexChanged()
        {
            if (mGoodsList.Count > 0)
            {
                var goodsId = cbxGoods.SelectedValue as long?;
                if (goodsId.HasValue)
                {
                    mClientGoods = mGoodsList.FirstOrDefault(x => x.Goods_ID == goodsId);
                }
                else
                {
                    mClientGoods = mGoodsList.FirstOrDefault();
                }
            }
            txtNumber.Text = "0";
            textBox1.Focus();
        }
        private void ProductInStoreForm_Deactivate(object sender, EventArgs e)
        {
            HardwareUtil.ScanPowerOff();
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        private void ProductInStoreForm_Activated(object sender, EventArgs e)
        {
            HardwareUtil.HardWareInit();
            HardwareUtil.ScanPowerOn();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var f = new MaterialStatisticsInputNumberForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var number = f.Number;
                mSumNumber += number;
                txtNumber.Text = mSumNumber.ToString();
            }
        }

        private void ProductInStoreForm_Load(object sender, EventArgs e)
        {
            DataBindGoods();
        }
        private void DataBindGoods()
        {

            cbxGoods.DataSource = mGoodsList;
            cbxGoods.DisplayMember = "Goods_Name";
            cbxGoods.ValueMember = "Goods_ID";
            if (mGoodsList.Count > 0)
            {
                mClientGoods = mGoodsList[0];
            }

        }

        private void cbxGoods_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SelectIndexChanged();
        }
    }
}