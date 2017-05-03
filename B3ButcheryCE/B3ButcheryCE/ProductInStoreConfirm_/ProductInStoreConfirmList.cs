using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;

namespace B3HRCE.ProductInStoreConfirm_
{
    public partial class ProductInStoreConfirmList : Form
    {
        public ProductInStoreConfirmList()
        {
            InitializeComponent();
        }
        private void ProductInStoreConfirmList_Load(object sender, EventArgs e)
        {
            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/GetProductInStoreList");
            listView1.BeginUpdate();
           foreach (RpcObject  item in list)
           {
               var lvItem = new ListViewItem(item.Get<long>("ID").ToString());
               lvItem.SubItems.Add(item.Get<DateTime>("InStoreDate").ToShortDateString());
               listView1.Items.Add(lvItem);
           }
           listView1.EndUpdate();
        }
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null)
            {
                return;
            }
            var ID = listView1.FocusedItem.Text as String;
            if (!string.IsNullOrEmpty(ID))
            {
                new ProductInStoreConfirmOK(ID).ShowDialog();
            }
        }

        private void ProductInStoreConfirmList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }
    }
}