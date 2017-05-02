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
    public partial class ProductInStoreConfirmOK : Form
    {
        public ProductInStoreConfirmOK(string ID)
        {
            InitializeComponent();
            label1.Text = ID;
        }
        private void ProductInStoreConfirmOK_Load(object sender, EventArgs e)
        {
            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/GetRpcEasyProductInStoreDetailById",long.Parse(label1.Text));
            listView1.BeginUpdate();
            foreach (RpcObject item in list)
            {
                var lvItem = new ListViewItem(item.Get<string>("Goods_Name"));
                lvItem.SubItems.Add(item.Get<decimal>("Number").ToString());
                listView1.Items.Add(lvItem);
            }
            listView1.EndUpdate();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/ProductInStoreCheck", long.Parse(label1.Text));
                MessageBox.Show("审核成功");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }
    }
}