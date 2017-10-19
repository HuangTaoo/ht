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
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using B3ButcheryCE.Rpc_;
using B3ButcheryCE.Util_;

namespace B3ButcheryCE.ProductInStoreConfirm_
{
    public partial class ProductInStoreConfirmOK : Form
    {
        string pId;
        public int state = 0;
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

                    var goods = new ClientGoods();
                    goods.Bill_ID = long.Parse(pId);
                    goods.ID = item.Get<long>("ID");
                    goods.Goods_Name = item.Get<string>("Goods_Name");
                    goods.Goods_Number = item.Get<decimal>("Number");
                    lvItem.Tag = goods;
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
                var dmo = new ClientProduceOutputBillSave();
                dmo.ID = long.Parse(pId);

                foreach (ListViewItem item in listView1.Items)
                {
                    var goods = item.Tag as ClientGoods;
                    var detail = new ClientGoods();
                    detail.Bill_ID = goods.Bill_ID;
                    detail.ID = goods.ID;
                    detail.Goods_ID = goods.Goods_ID;
                    detail.Goods_Number = goods.Goods_Number;
                    dmo.Details.Add(detail);
                }
                var cprkId = SyncBillUtil.ProductInStoreSaveAndCheck(dmo);
                if (cprkId == 0)
                {
                    MessageBox.Show("没有成品入库新建权限");
                    return;
                }
                else if (cprkId == -1)
                {
                    MessageBox.Show("没有成品入库审核权限");
                    return;
                }
                MessageBox.Show("审核成功");
                state = 1;
                try
                {
                    //九联业务，如果九联模块用户个性设置中，{默认其他出库会计单位}{默认其他出库仓库}和单据相同，生成【其他出库单】
                    var id = RpcFacade.Call<long>("/MainSystem/B3_JiuLian/Rpcs/ButcherTouchScreenRpc/OtherOutStoreRpc/OtherOutStoreInsertAndCheck", long.Parse(pId));
                    if (id == 0)
                    {
                        MessageBox.Show("没有其他出库新建权限");
                        return;
                    }
                    else if (id == -1)
                    {
                        MessageBox.Show("没有其他出库审核权限");
                        return;
                    }
                    else if (id == -2)
                        return;
                    else if (id == -3)
                        return;
                    MessageBox.Show("生成已审核其他出库No." + id);
                }
                catch (Exception)
                {
                }
                //RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/ProductInStoreCheck", long.Parse(pId));
                //MessageBox.Show("审核成功");                
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
            if (e.KeyValue == 13)
            {
                btnOk_Click(sender, e);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            var item = listView1.Items[listView1.FocusedItem.Index];
            if (listView1.FocusedItem == null)
            {
                return;
            }
            var inputNumber = new ProductInStoreConfirmInputNumberForm();
            if (inputNumber.ShowDialog() == DialogResult.OK)
            {
                var number = inputNumber.Number;
                item.SubItems[2].Text = number.ToString();
                var goods = item.Tag as ClientGoods;
                goods.Goods_Number = number;
            }
        }
    }
}