using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3HRCE.Rpc_;
using B3ButcheryCE.Rpc_;
using B3HRCE.OutputStatistics_;

namespace B3ButcheryCE.FrozenInStore_
{
    public partial class FrozenInStoreInputNumber : Form
    {
        ClientGoods mClientGoods;
        public decimal SumNumber = 0;
        public FrozenInStoreInputNumber(ClientGoods goods)
        {
            InitializeComponent();
            this.Text = goods.Goods_Name;
            mClientGoods = goods;
            SumNumber = mClientGoods.Goods_Number ?? 0;
        }

        private void FrozenInStoreInputNumber_Load(object sender, EventArgs e)
        {
            txtNumber.Text = (mClientGoods.Goods_Number ?? 0).ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//辅数量确定
            try
            {
                var secondNumber = decimal.Parse(txtSecondNumber.Text);
                var number = ClientUtil.GetNumberBySecondNumber(mClientGoods, secondNumber);

                SumNumber += number ?? 0;
                txtNumber.Text = SumNumber.ToString();
                txtSecondNumber.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new MaterialStatisticsInputNumberForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var number = f.Number;
                SumNumber += number;
                txtNumber.Text = SumNumber.ToString();
            }
        }
    }
}