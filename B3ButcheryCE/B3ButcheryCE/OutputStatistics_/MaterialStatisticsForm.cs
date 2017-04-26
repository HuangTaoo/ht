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
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using System.IO;
using System.Xml.Serialization;
using B3ButcheryCE;
using B3ButcheryCE.Util_;

namespace B3HRCE.OutputStatistics_
{
    public partial class MaterialStatisticsForm : Form
    {

        ClientGoods mClientGoods;

        SysConfig config = SysConfig.Current;
        decimal mSumNumber = 0;
        List<ClientGoods> mGoodsList = XmlSerializerUtil.GetClientListXmlDeserialize<ClientGoods>();
        public MaterialStatisticsForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        private void MaterialStatisticsForm_Load(object sender, EventArgs e)
        {
            lblAccountUnit.Text = config.AccountingUnit_Name;
            DataBindGoods();
        }

        private void DataBindGoods()
        {

            cbxGoods.DataSource = mGoodsList;
            cbxGoods.DisplayMember = "Goods_Name";
            cbxGoods.ValueMember = "Goods_ID";
            mClientGoods = mGoodsList[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new MaterialStatisticsInputNumberForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var number = f.Number;
                mSumNumber += number;
                txtNumber.Text = mSumNumber.ToString();
            }
        }

        //确定
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                ClientProduceOutputBillSave clientDmo = new ClientProduceOutputBillSave
                {
                    AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                    Department_ID = SysConfig.Current.Department_ID??0,
                    Domain_ID = SysConfig.Current.Domain_ID,
                    User_ID = SysConfig.Current.User_ID,
                    CreateTime = DateTime.Now
                };

                var clientDetail = new ClientGoods();
                clientDetail.Goods_ID = mClientGoods.Goods_ID;
                clientDetail.Goods_Number = decimal.Parse(txtNumber.Text);
                clientDmo.Details.Add(clientDetail);

                XmlSerializerUtil.ClientXmlSerializer(clientDmo);
 
                SyncBillUtil.SyncProductOut();
             
                MessageBox.Show("操作成功");
                txtNumber.Text = "";
                textBox1.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); ;
            }          

        }

  

        private void cbxGoods_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        //失去焦点
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                var secondNumber = decimal.Parse(textBox1.Text);
                var number = ClientUtil.GetNumberBySecondNumber(mClientGoods, secondNumber);

                mSumNumber += number ?? 0;
                txtNumber.Text = mSumNumber.ToString();
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}