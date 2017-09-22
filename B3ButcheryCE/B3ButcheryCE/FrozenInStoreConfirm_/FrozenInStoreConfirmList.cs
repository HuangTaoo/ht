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
using B3ButcheryCE.Rpc_;
using B3ButcheryCE.FrozenInStore_;

namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    public partial class FrozenInStoreConfirmList : Form
    {

        long mStoreId;
        long mProductPlanId;
        public FrozenInStoreConfirmList(long storeId, long productPlanId)
        {
            InitializeComponent();
            mStoreId = storeId;
            mProductPlanId = productPlanId;
        }

        void LoadGoodsFromRpc()
        {
            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProduceOutputRpc/GetTodayGoodsByStore", SysConfig.Current.AccountingUnit_ID, SysConfig.Current.Department_ID, mStoreId, mProductPlanId);
            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (RpcObject item in list)
            {
                var goods = new ClientGoods();
                goods.Goods_ID = item.Get<long>("Goods_ID");
                goods.Goods_Name = item.Get<string>("Goods_Name");
                goods.Goods_Number = item.Get<decimal?>("Number");
                goods.Goods_SecondNumber = item.Get<decimal?>("InnerPackingPer");
                goods.Goods_InnerPackingPer = item.Get<decimal?>("Goods_InnerPackingPer");

                var lvItem = new ListViewItem(goods.Goods_Name);
                lvItem.Tag = goods;
                lvItem.SubItems.Add(goods.Goods_Number.ToString());
                lvItem.SubItems.Add(goods.Goods_Number.ToString());
                lvItem.SubItems.Add(goods.Goods_SecondNumber.ToString());

                listView1.Items.Add(lvItem);
                //listView1.Items[]
            }
            listView1.EndUpdate();
        }

        private void FrozenInStoreConfirmList_Load(object sender, EventArgs e)
        {
            LoadGoodsFromRpc();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = listView1.Items[e.Index];
            //选中的情况
            if (!item.Checked)
            {
                //var goods = item.Tag as ClientGoods;
                //var f = new FrozenInStoreInputNumber(goods);
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //    var number = f.SumNumber;
                //    item.SubItems[2].Text = number.ToString();                    
                //}


                //MessageBox.Show("选中" + e.Index);
            }
            else
            {

                //MessageBox.Show("no" + e.Index);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

            var selectIndex = listView1.SelectedIndices[0];
            //
            var f = new FrozenInStoreConfirmOK();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.Number != 0)
                {
                    listView1.Items[selectIndex].SubItems[2].Text = f.Number.ToString();
                    var goods = listView1.Items[selectIndex].Tag as ClientGoods;
                    if (goods != null)
                    {
                        listView1.Items[selectIndex].SubItems[3].Text = (f.Number / goods.Goods_InnerPackingPer).ToString();
                    }

                }

            }

            //获取第1列的值
            //MessageBox.Show(filename);

            //MessageBox.Show("双击");
        }

        private void FrozenInStoreConfirmList_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();
            try
            {
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.Checked)
                    {
                        var goods = item.Tag as ClientGoods;
                        var number = decimal.Parse(item.SubItems[2].Text);

                        var dmo = new RpcObject("/MainSystem/B3Butchery/BO/FrozenInStore");

                        dmo.Set("AccountingUnit_ID", SysConfig.Current.AccountingUnit_ID);
                        dmo.Set("Department_ID", SysConfig.Current.Department_ID);
                        dmo.Set("Store_ID", mStoreId);
                        dmo.Set("ProductionPlan_ID", mProductPlanId);

                        var detail = new RpcObject("/MainSystem/B3Butchery/BO/FrozenInStore_Detail");
                        detail.Set("Goods_ID", goods.Goods_ID);
                        detail.Set("Number", number);

                        dmo.Get<ManyList>("Details").Add(detail);

                        long id = RpcFacade.Call<long>("/MainSystem/B3Butchery/Rpcs/FrozenInStoreRpc/PdaInsertAndCheck", dmo);
                        ids.Add(id.ToString());

                    }
                }
                if (ids.Any(x => x == "0"))
                {
                    MessageBox.Show("没有速冻入库新建权限");
                    return;
                }
                if (ids.Any(x => x == "-1"))
                {
                    MessageBox.Show("没有速冻入库审核权限");
                    return;
                }
                MessageBox.Show("生成速冻入库单：" + string.Join(",", ids.ToArray()));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            finally
            {
                LoadGoodsFromRpc();
            }
        }
    }
}