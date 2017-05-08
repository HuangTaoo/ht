using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3HRCE.Rpc_;
using B3ButcheryCE.Util_;
using B3ButcheryCE.FrozenInStore_;
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using B3ButcheryCE;

namespace B3HRCE.FrozenInStore_
{
    public partial class FrozenInStoreSelectGoodsForm : Form
    {
        long mStoreId;
        public FrozenInStoreSelectGoodsForm(long storeId)
        {
            InitializeComponent();
            Util.SetSceen(this);
            mStoreId = storeId;
        }

        List<ClientGoods> mGoodsList = XmlSerializerUtil.GetClientListXmlDeserialize<ClientGoods>();
        // List<ClientGoods> mListViewItem;
        private void FrozenInStoreSelectGoodsForm_Load(object sender, EventArgs e)
        {
            //mListViewItem = mGoodsList;
            RefreshListView();

        }

        private void RefreshListView()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (ClientGoods goods in mGoodsList)
            {
                var item = new ListViewItem(goods.Goods_Name);
                item.Tag = goods;
                item.SubItems.Add("");
                string number = "";
                if (goods.Goods_Number.HasValue)
                {
                    number = goods.Goods_Number.Value.ToString();
                }
                item.SubItems.Add(number);
                //if (goods.ListViewChecked)
                //{
                //    item.Checked = true;
                //}
                listView1.Items.Add(item);
            }

            listView1.EndUpdate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (ListViewItem lv in listView1.Items)
            //{
            //    if (lv.Selected)
            //    {               
            //        lv.Checked = true;

            //    }      
            //}
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = listView1.Items[e.Index];
            //选中的情况
            if (!item.Checked)
            {
                var goods = item.Tag as ClientGoods;
                var f = new FrozenInStoreInputNumber(goods);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    var number = f.SumNumber;
                    item.SubItems[2].Text = number.ToString();
                    //mListViewItem[e.Index].Goods_Number = number;

                    //mListViewItem[e.Index].ListViewChecked = true;
                    //RefreshListView();
                }
                //mListViewItem[i]
                //MessageBox.Show("选中" + e.Index);
            }
            else
            {

                //MessageBox.Show("no" + e.Index);
            }

            //if (!listView1.Items[e.Index].Checked)//如果点击的CheckBoxes没有选中  
            //{
            //foreach (ListViewItem lv in listView1.Items)
            //{
            //    if (lv.Checked)//取消所有已选中的CheckBoxes  
            //    {
            //        lv.Checked = false;
            //        lv.Selected = false;
            //        // lv.BackColor = Color.White;  
            //    }
            //}

            //listView1.Items[e.Index].Selected = true;

            // lv.Checked = false;  
            //}  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientProduceOutputBillSave dmo = new ClientProduceOutputBillSave();
            dmo.AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID ?? 0;
            dmo.Department_ID = SysConfig.Current.Department_ID ?? 0;
            dmo.Domain_ID = SysConfig.Current.Domain_ID;
            dmo.User_ID = SysConfig.Current.User_ID;

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    var goods = item.Tag as ClientGoods;
                    var detail = new ClientGoods();
                    detail.Goods_ID = goods.Goods_ID;
                    decimal? number = null;
                    try
                    {
                        number = decimal.Parse(item.SubItems[2].Text);                       
                    }
                    catch (Exception)
                    {
                                                
                    }
                    detail.Goods_Number = number;
                    dmo.Details.Add(detail);

                }
            }
            XmlSerializerUtil.ClientXmlSerializer(dmo);

            SyncBillUtil.SyncProductInStore();

            MessageBox.Show("操作成功");
            Close();
        }


    }
}