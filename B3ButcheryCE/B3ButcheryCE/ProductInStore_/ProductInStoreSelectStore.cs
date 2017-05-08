using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Rpc_;
using B3ButcheryCE.Rpc_.BaseInfo_;
using B3ButcheryCE.Util_;

namespace B3ButcheryCE.ProductInStore_
{
    public partial class ProductInStoreSelectStore : Form
    {
        List<ClientAllGoods> mGoodsListt = new List<ClientAllGoods>();
        List<ClientStore> list = XmlSerializerUtil.GetClientListXmlDeserialize<ClientStore>();
        public ProductInStoreSelectStore(List<ClientAllGoods> mGoodsList)
        {
            InitializeComponent();
             mGoodsListt = mGoodsList;
        }

        private void ProductInStoreSelectStore_Load(object sender, EventArgs e)
        {
            cbxStore.DataSource = list;
            cbxStore.DisplayMember = "Name";
            cbxStore.ValueMember = "BarCode";
            listView1.BeginUpdate();
            foreach (ClientAllGoods item in mGoodsListt)
            {
                var lvItem = new ListViewItem(item.Goods_Name);
                lvItem.SubItems.Add(item.Goods_Number.ToString());
                listView1.Items.Add(lvItem);
            }
            listView1.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

    }
}