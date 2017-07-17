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

namespace B3ButcheryCE.ProductInStoreConfirm_
{
    public partial class ProductInStoreConfirmOK : Form
    {
        string pId;
        public ProductInStoreConfirmOK(string id)
        {
            InitializeComponent();
            pId = id;
        }
        private void ProductInStoreConfirmOK_Load(object sender, EventArgs e)
        {
            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/GetRpcEasyProductInStoreDetailById", long.Parse(pId));
            listView1.BeginUpdate();
            foreach (RpcObject item in list)
            {
                var lvItem = new ListViewItem(item.Get<string>("Goods_Name"));
                try
                {
                    lvItem.SubItems.Add(item.Get<decimal>("SecondNumber").ToString());
                    lvItem.SubItems.Add(item.Get<decimal>("Number").ToString());
                }
                catch (Exception)
                {
                    lvItem.SubItems.Add("");
                }
                listView1.Items.Add(lvItem);
            }
            listView1.EndUpdate();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/ProductInStoreCheck", long.Parse(pId));
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

        private void ProductInStoreConfirmOK_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                btnOk_Click(sender,e);
            }
        }

    }
}