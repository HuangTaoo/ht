using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3HRCE.Rpc_.ClientProductInStore_;
using System.IO;
using System.Xml.Serialization;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;
using B3HRCE.Rpc_;

namespace B3HRCE.ProductInStore_
{
    public partial class ProductInStoreDialog : Form
    {
        public ProductInStoreDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            HardwareUtil.Device.ScannerReader += new EventHandler<B3HRCE.Device_.ScanEventArgs>(Device_ScannerReaders);
        }

        ClientProductInStore productInStoreTemplate;
        private int ScanIs = 1;
        ClientProductInStoreBillSave productInStore;
        Dictionary<long, Tuple<string, decimal?, decimal?>> goodsInfo = new Dictionary<long, Tuple<string, decimal?, decimal?>>();
        public ProductInStoreDialog(long departMentID, long template)
        {
            InitializeComponent();
            Util.SetSceen(this);
            productInStore = new ClientProductInStoreBillSave
            {
                AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                Department_ID = departMentID,
                Domain_ID = SysConfig.Current.Domain_ID,
                User_ID = SysConfig.Current.User_ID,
                CreateTime = DateTime.Today
            };

            comboBoxSelectStore.Items.Add("");
            comboBoxSelectGoods.Items.Add("");
            comboBoxProductPlan.Items.Add("");

            string file = Path.Combine(Path.Combine(Util.DataFolder, typeof(ClientProductInStore).Name), template.ToString() + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ClientProductInStore));
            using (var stream = File.Open(file, FileMode.Open))
            {
                productInStoreTemplate = serializer.Deserialize(stream) as ClientProductInStore;
            }
            productInStore.InStoreType_ID = productInStoreTemplate.InStoreType_ID;
            productInStore.InStoreType_Name = productInStoreTemplate.InStoreType_Name;
            foreach (var storeDetail in productInStoreTemplate.StoreDetails)
            {
                if (storeDetail.Store_ID != 0)
                    comboBoxSelectStore.Items.Add(new Option(storeDetail.Store_Name, storeDetail.Store_ID));
            }
            foreach (var goodsDetail in productInStoreTemplate.GoodsDetails)
            {
                if (goodsDetail.Goods_ID != 0)
                    comboBoxSelectGoods.Items.Add(new Option(goodsDetail.Goods_Name, goodsDetail.Goods_ID));
                if (!goodsInfo.ContainsKey(goodsDetail.Goods_ID))
                    goodsInfo.Add(goodsDetail.Goods_ID, new Tuple<string, decimal?, decimal?>(goodsDetail.Goods_UnitConvertDirection, goodsDetail.Goods_MainUnitRatio, goodsDetail.Goods_SecondUnitRatio));
            }

            var productPlanFolder = Path.Combine(Util.DataFolder, typeof(ClientProductPlan).Name);
            if (Directory.Exists(productPlanFolder))
            {
                var productPlanFile = Directory.GetFiles(productPlanFolder, "*.xml");

                XmlSerializer ppSerializer = new XmlSerializer(typeof(ClientProductPlan));
                foreach (var ppFile in productPlanFile)
                {
                    using (var stream = File.Open(ppFile, FileMode.Open))
                    {
                        var productPlan = ppSerializer.Deserialize(stream) as ClientProductPlan;
                        comboBoxProductPlan.Items.Add(new Option(productPlan.PlanNumber, productPlan.ID));
                    }
                }
            }
            comboBoxProductPlan.Focus();

            HardwareUtil.Device.ScannerReader += new EventHandler<B3HRCE.Device_.ScanEventArgs>(Device_ScannerReaders);
        }

        public void Device_ScannerReaders(object sender, B3HRCE.Device_.ScanEventArgs e)
        {
            bool hasStoreCode = false;
            bool hasGoodsCode = false;

            if (ScanIs == 1)
            {
                //仓库
                foreach (var storeDetail in productInStoreTemplate.StoreDetails)
                {
                    var code = storeDetail.Store_Code;

                    if (code == e.BarCode)
                    {
                        hasStoreCode = true;
                        comboBoxSelectStore.SelectedItem = storeDetail.Store_ID;
                        comboBoxSelectStore.Text = storeDetail.Store_Name;
                        comboBoxSelectGoods.Focus();
                        break;
                    }
                }

                if (!hasStoreCode)
                {
                    MessageBox.Show("模板中不存在该仓库：" + e.BarCode);
                    return;
                }
            }
            else if (ScanIs == 2)
            {
                //存货
                foreach (var goodsDetail in productInStoreTemplate.GoodsDetails)
                {
                    var code = goodsDetail.Goods_Code;

                    if (code == e.BarCode)
                    {
                        hasGoodsCode = true;
                        comboBoxSelectGoods.SelectedItem = goodsDetail.Goods_ID;
                        comboBoxSelectGoods.Text = goodsDetail.Goods_Name;
                        textBoxSecondNumber.Focus();
                        break;
                    }
                }

                if (!hasGoodsCode)
                {
                    MessageBox.Show("模板中不存在该存货：" + e.BarCode);
                    return;
                }
            }

            ScanIs = 2;
        }


        private void menuItemHistore_Click(object sender, EventArgs e)
        {
            new ProductInStoreListDialog(productInStoreTemplate.Department_ID).ShowDialog();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (Util.ExistError(() => comboBoxProductPlan.SelectedItem == null, "请选择生产计划"))
            {
                return;
            }
            long productPlan = ((Option)comboBoxProductPlan.SelectedItem).Value;

            if (Util.ExistError(() => comboBoxSelectStore.SelectedItem == null, "请选择仓库"))
            {
                return;
            }
            long store = ((Option)comboBoxSelectStore.SelectedItem).Value;

            if (Util.ExistError(() => comboBoxSelectGoods.SelectedItem == null ||  comboBoxSelectGoods.SelectedItem.ToString() == "", "请选择存货"))
            {
                return;
            }

            long goods = ((Option)comboBoxSelectGoods.SelectedItem).Value;
            decimal mainNumber = 0;
            if (!Util.TryParseDecimal(textBoxMainNumber.Text, (x) => { mainNumber = x; }, "请输入主数量"))
            {
                return;
            }
            decimal? secondNumber = 0;
            if (!string.IsNullOrEmpty(textBoxSecondNumber.Text))
                secondNumber = decimal.Parse(textBoxSecondNumber.Text);

            var detail = new ClientProductInStoreDetail();
            productInStore.Details.Add(detail);
            detail.ProductPlanID = productPlan;
            detail.ProductNumber = comboBoxProductPlan.SelectedItem.ToString();
            detail.Store_ID = store;
            detail.Store_Name = comboBoxSelectStore.SelectedItem.ToString();
            detail.Goods_ID = goods;
            detail.Goods_Name = comboBoxSelectGoods.SelectedItem.ToString();
            detail.MainNumber = mainNumber;
            detail.SecondNumber = secondNumber;
            MessageBox.Show("加入成功。");
            ClearInput();
        }

        private void btn_Finish_Click(object sender, EventArgs e)
        {
            new ProductInStoreDetailDialog(productInStore).ShowDialog();
        }

        private void ClearInput()
        {
            comboBoxSelectGoods.SelectedItem = 0;
            comboBoxSelectGoods.Text = "";
            //ScanIs = 1;
      
            textBoxMainNumber.Text = "";
            textBoxSecondNumber.Text = "";  
            comboBoxSelectGoods.Focus();
        }

        private void ProductInStoreDialog_Closing(object sender, CancelEventArgs e)
        {
            HardwareUtil.Device.ScannerReader -= new EventHandler<B3HRCE.Device_.ScanEventArgs>(Device_ScannerReaders);
        }

        private void ProductInStoreDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_OK.Focus();
                btn_OK_Click(new object(), EventArgs.Empty);
            }
            else if (e.KeyChar == 27)
            {
                ClearInput();
            }else if (e.KeyChar == 8)
            {
                if (textBoxMainNumber.Focused)
                {
                    if (string.IsNullOrEmpty(textBoxMainNumber.Text))
                    {   
                        textBoxMainNumber.Text = "-";
                    }
                }
                else if (textBoxSecondNumber.Focused)
                {
                    if (string.IsNullOrEmpty(textBoxSecondNumber.Text))
                        textBoxSecondNumber.Text = "-";
                }
            }
        }

        private void textBoxMainNumber_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxMainNumber.Text))
            {
                textBoxSecondNumber.Text = string.Empty;
                return;
            }
            if (comboBoxSelectGoods.SelectedItem != null && comboBoxSelectGoods.SelectedItem.ToString() !="")
            {
                var item = goodsInfo[((Option)comboBoxSelectGoods.SelectedItem).Value];
                if (item.Item1 == "双向转换" || item.Item1 == "由主至辅")
                {
                    if (item.Item3 > 0)
                        textBoxSecondNumber.Text = string.Format("{0}", decimal.Parse(textBoxMainNumber.Text) * item.Item3 / item.Item2);
                }
            }
        }

        private void textBoxSecondNumber_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSecondNumber.Text))
            {
                textBoxMainNumber.Text = string.Empty;
                return;
            }
            if (comboBoxSelectGoods.SelectedItem != null && comboBoxSelectGoods.SelectedItem.ToString() !="")
            {
                var item = goodsInfo[((Option)comboBoxSelectGoods.SelectedItem).Value];
                if (item.Item1 == "双向转换" || item.Item1 == "由辅至主")
                {
                    if (item.Item2 > 0)
                        textBoxMainNumber.Text = string.Format("{0}", decimal.Parse(textBoxSecondNumber.Text) * item.Item2 / item.Item3);
                }
            }
        }
    }
}