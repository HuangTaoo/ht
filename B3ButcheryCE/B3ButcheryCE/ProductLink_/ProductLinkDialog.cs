using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Rpc_.ClientProductLink_;
using System.IO;
using System.Xml.Serialization;
using B3ButcheryCE.Rpc_;
using BWP.Compact.Devices;

namespace B3ButcheryCE.ProductLink_
{
    public partial class ProductLinkDialog : Form
    {
        public ProductLinkDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            //HardwareUtil.Device.ScannerReader += new EventHandler<B3HRCE.Device_.ScanEventArgs>(Device_ScannerReaders);
        }

        ClientProductLink productLinkTemplate;
        private int ScanIs = 1;
        ClientProductLinkBillSave productLink;
        Dictionary<long, Tuple<string, decimal?, decimal?>> goodsInfo = new Dictionary<long, Tuple<string, decimal?, decimal?>>();

        public ProductLinkDialog(long departMentID, long template)
        {
            InitializeComponent();
            Util.SetSceen(this);
            productLink = new ClientProductLinkBillSave
            {
                AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                Department_ID = departMentID,
                Domain_ID = SysConfig.Current.Domain_ID,
                User_ID = SysConfig.Current.User_ID,
                CreateTime = DateTime.Now
            };

            comboBoxProductPlan.Items.Add("");
            comboBoxGoods.Items.Add("");

            string file = Path.Combine(Path.Combine(Util.DataFolder, typeof(ClientProductLink).Name), template.ToString() + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLink));
            using (var stream = File.Open(file, FileMode.Open))
            {
                productLinkTemplate = serializer.Deserialize(stream) as ClientProductLink;
            }
            productLink.CollectType = productLinkTemplate.CollectType;
            productLink.ProductLinks_ID = productLinkTemplate.ProductLinks_ID;
            foreach (var detail in productLinkTemplate.Details)
            {
                if (detail.Goods_ID != 0)
                    comboBoxGoods.Items.Add(new Option(detail.Goods_Name, detail.Goods_ID));
                if (!goodsInfo.ContainsKey(detail.Goods_ID))
                    goodsInfo.Add(detail.Goods_ID, new Tuple<string, decimal?, decimal?>(detail.Goods_UnitConvertDirection, detail.Goods_MainUnitRatio, detail.Goods_SecondUnitRatio));
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
                        //if (productPlan.PlanDate == productPlan.SyncDate)
                            comboBoxProductPlan.Items.Add(new Option(productPlan.PlanNumber, productPlan.ID));
                    }
                }

                if (comboBoxProductPlan.Items.Count > 1)
                    comboBoxProductPlan.SelectedIndex = 1;
            }
            comboBoxProductPlan.Focus();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        public void Device_ScannerReaders(object sender, ScanEventArgs e)
        {
            bool hasGoodsCode = false;

            if (ScanIs == 1)
            {
                //存货
                foreach (var detail in productLinkTemplate.Details)
                {
                    var code = detail.Goods_Code;

                    if (code == e.BarCode)
                    {
                        hasGoodsCode = true;
                        comboBoxGoods.SelectedItem = detail.Goods_ID;
                        comboBoxGoods.Text = detail.Goods_Name;
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
        }


        private void menuItemHistory_Click(object sender, EventArgs e)
        {
            new ProductLinkListDialog(productLinkTemplate.Department_ID).ShowDialog();
        }

        private void ProductLinkDialog_Closing(object sender, CancelEventArgs e)
        {
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        private void ProductLinkDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                okBtn.Focus();
                okBtn_Click(new object(), EventArgs.Empty);
            }
            else if (e.KeyChar == 27)
            {
                ClearInput();
            }
            else if (e.KeyChar == 8)
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

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (Util.ExistError(() => comboBoxProductPlan.SelectedItem == null, "请选择生产计划"))
            {
                return;
            }
            long productPlan = ((Option)comboBoxProductPlan.SelectedItem).Value;
            if (Util.ExistError(() => comboBoxGoods.SelectedItem == null ||  comboBoxGoods.SelectedItem.ToString() == "", "请选择存货"))
            {
                return;
            }
            long goods = ((Option)comboBoxGoods.SelectedItem).Value;
            
            if (string.IsNullOrEmpty(textBoxMainNumber.Text) && string.IsNullOrEmpty(textBoxSecondNumber.Text))
            {
                MessageBox.Show("请输入数量");
                return;
            }
            decimal mainNumber = 0;
            if (!string.IsNullOrEmpty(textBoxMainNumber.Text))
                mainNumber = decimal.Parse(textBoxMainNumber.Text);
            decimal? secondNumber = 0;
            if (!string.IsNullOrEmpty(textBoxSecondNumber.Text))
                secondNumber = decimal.Parse(textBoxSecondNumber.Text);

            var detail = new ClientProductLinkDetail();
            productLink.Details.Add(detail);
            detail.ProductPlanID = productPlan;
            detail.ProductNumber = comboBoxProductPlan.SelectedItem.ToString();
            detail.Goods_ID = goods;
            detail.Goods_Name = comboBoxGoods.SelectedItem.ToString();
            detail.MainNumber = mainNumber;
            detail.SecondNumber = secondNumber;
            MessageBox.Show("加入成功。");
            ClearInput();
        }

        private void ClearInput()
        {
            comboBoxGoods.SelectedItem = 0;
            comboBoxGoods.Text = "";
            comboBoxGoods.Focus();
            textBoxMainNumber.Text = "";
            textBoxSecondNumber.Text = "";
        }

        private void btn_Finish_Click(object sender, EventArgs e)
        {
            new ProductLinkDetailDialog(productLink).ShowDialog();
        }

        private void textBoxMainNumber_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxMainNumber.Text))
            {
                textBoxSecondNumber.Text = string.Empty;
                return;
            }
            if (comboBoxGoods.SelectedItem != null && comboBoxGoods.SelectedItem.ToString() != "")
            {
                var item = goodsInfo[((Option)comboBoxGoods.SelectedItem).Value];
                if (item.Item1 == "双向转换" || item.Item1 == "由主至辅")
                {
                    if (item.Item3 > 0)
                        textBoxSecondNumber.Text = string.Format("{0}", Decimal.Round((decimal)(decimal.Parse(textBoxMainNumber.Text) * item.Item3 / item.Item2),2));
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
            if (comboBoxGoods.SelectedItem != null && comboBoxGoods.SelectedItem.ToString() != "")
            {
                var item = goodsInfo[((Option)comboBoxGoods.SelectedItem).Value];
                if (item.Item1 == "双向转换" || item.Item1 == "由辅至主")
                {
                    if (item.Item2 > 0)
                        textBoxMainNumber.Text = string.Format("{0}", Decimal.Round((decimal)(decimal.Parse(textBoxSecondNumber.Text) * item.Item2 / item.Item3),2));
                }
            }
        }
    }
}