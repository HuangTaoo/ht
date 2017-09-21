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
using BWP.Compact.Devices;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;
using B3ButcheryCE.Rpc_;

namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    public partial class FrozenInStoreConfirmScan : Form
    {
        public FrozenInStoreConfirmScan()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        //List<ClientStore> list = XmlSerializerUtil.GetClientListXmlDeserialize<ClientStore>();

        private List<ClientStore> _storeList;
        private List<ClientProductPlan> _productPlanList;
        public List<ClientStore> StoreList{get{return _storeList;}set{_storeList=value;}}
        public List<ClientProductPlan> ProductPlanList { get { return _productPlanList; } set { _productPlanList = value; } }

        private void FrozenInStoreConfirmScan_Load(object sender, EventArgs e)
        {
            //不做离线，直接从接口取
            var listObj = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/SyncFrozenStore");
            var list = new List<ClientStore>();
            foreach (RpcObject obj in listObj)
            {
                var store = new ClientStore();
                store.ID = obj.Get<long>("ID");
                store.Name = obj.Get<string>("Name");
                list.Add(store);
            }
            _storeList = list;
            comboBox1.DataSource = _storeList;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            var productPlanObj = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProduceOutputRpc/GetProductPlan");
            var productPlanList = new List<ClientProductPlan>();
            foreach (RpcObject obj in productPlanObj)
            {
                var productPlan = new ClientProductPlan();
                productPlan.ID = obj.Get<long>("ID");
                productPlan.PlanNumber = obj.Get<string>("PlanNumber");
                productPlanList.Add(productPlan);
            }
            _productPlanList = productPlanList;
            comboBox2.DataSource = _productPlanList;
            comboBox2.DisplayMember = "PlanNumber";
            comboBox2.ValueMember = "ID";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedValue.ToString());
            OpenSelectGoodsFrom(long.Parse(comboBox1.SelectedValue.ToString()), long.Parse(comboBox2.SelectedValue.ToString()));
            //txtFrozenInStoreID.Text = "";
        }
        void OpenSelectGoodsFrom(long goodsId,long productPlan)
        {
            var store = _storeList.FirstOrDefault(x => x.ID == goodsId);
            if (store == null)
            {
                MessageBox.Show("没找到" + goodsId + "速冻库");
                return;
            }

            var product = _productPlanList.FirstOrDefault(x => x.ID == productPlan);
            if (product == null)
            {
                MessageBox.Show("没找到" + product.PlanNumber + "计划号");
                return;
            }


            var f = new FrozenInStoreConfirmList(store.ID, product.ID);
            f.ShowDialog();
        }
        private void FrozenInStoreConfirmScan_Deactivate(object sender, EventArgs e)
        {
            //HardwareUtil.ScanPowerOff();
            //HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        private void FrozenInStoreConfirmScan_Activated(object sender, EventArgs e)
        {
            //HardwareUtil.HardWareInit();
            //HardwareUtil.ScanPowerOn();
            //HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(BarCodeRead);
        }

        //void BarCodeRead(object sender, ScanEventArgs e)
        //{
        //    var result = e.BarCode.Trim();
        //    OpenSelectGoodsFrom(result);
        //}

        private void FrozenInStoreConfirmScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnOK_Click(sender, e);
            }
        }

    }
}