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
using System.Threading;
using BWP.Compact.UI;
using System.IO;
using B3ButcheryCE.Rpc_;
using System.Xml;
using System.Xml.Serialization;
using BWP.Compact.Devices;


namespace B3ButcheryCE.FileGroupValuation_
{
    public partial class FileGroupValuationDialog : Form
    {
        public FileGroupValuationDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);

            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        ClientFileGroupValuation fileGroupValuationTemplate;
        ClientFileGroupValuationBillSave fileGroupValuation;
        private int ScanIs = 1;


        public FileGroupValuationDialog(long departMentID, long template)
        {
            InitializeComponent();
            Util.SetSceen(this);
            fileGroupValuation = new ClientFileGroupValuationBillSave
            {
                AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                Department_ID = departMentID,
                Domain_ID = SysConfig.Current.Domain_ID,
                User_ID = SysConfig.Current.User_ID,
                CreateTime = DateTime.Now,
                IsHandsetSend = true
            };
            comboBoxFileGroup.Items.Add("");
            comboBoxPieceItem.Items.Add("");

            string file = Path.Combine(Path.Combine(Util.DataFolder, typeof(ClientFileGroupValuation).Name), template.ToString() + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientFileGroupValuation));
            using (var stream = File.Open(file, FileMode.Open))
            {
                fileGroupValuationTemplate = serializer.Deserialize(stream) as ClientFileGroupValuation;
            }
            foreach (var fileGroupDetail in fileGroupValuationTemplate.FileGroupDetails)
            {
                if (fileGroupDetail.FileGroup_ID != 0)
                    comboBoxFileGroup.Items.Add(new Option(fileGroupDetail.FileGroup_Name, fileGroupDetail.FileGroup_ID));
            }
            foreach (var pieceItemDetail in fileGroupValuationTemplate.PieceItemDetails)
            {
                if (pieceItemDetail.PieceItem_ID != 0)
                    comboBoxPieceItem.Items.Add(new Option(pieceItemDetail.PieceItem_Name, pieceItemDetail.PieceItem_ID));
            }

            comboBoxFileGroup.Focus();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        public void Device_ScannerReaders(object sender, ScanEventArgs e)
        {
            bool hasFileGroupCode = false;
            bool hasPieceItemCode = false;

            if (ScanIs == 1)
            {
                //案组
                if (fileGroupValuationTemplate.FileGroupDetails.Count > 0)
                {
                    foreach (var row in fileGroupValuationTemplate.FileGroupDetails)
                    {
                        var code = row.FileGroupCode;

                        if (code == e.BarCode)
                        {
                            hasFileGroupCode = true;
                            comboBoxFileGroup.SelectedItem = (long?)row.FileGroup_ID;
                            comboBoxFileGroup.Text = (string)row.FileGroup_Name;
                            comboBoxPieceItem.Focus();
                            break;
                        }
                    }
                }

                if (!hasFileGroupCode)
                {
                    MessageBox.Show("模板中不存在该案组：" + e.BarCode);
                    return;
                }
            }
            else if (ScanIs == 2)
            {
                //计件品项
                if (fileGroupValuationTemplate.PieceItemDetails.Count > 0)
                {
                    foreach (var row in fileGroupValuationTemplate.PieceItemDetails)
                    {
                        var code = (string)row.PieceItem_Code;

                        if (code == e.BarCode)
                        {
                            hasPieceItemCode = true;
                            comboBoxPieceItem.SelectedItem = (long?)row.PieceItem_ID;
                            comboBoxPieceItem.Text = (string)row.PieceItem_Name;
                            txtBoxNumber.Focus();
                            break;
                        }
                    }
                }

                if (!hasPieceItemCode)
                {
                    MessageBox.Show("模板中不存在该计件品项：" + e.BarCode);
                    return;
                }
            }

            ScanIs = 2;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Util.ExistError(() => comboBoxFileGroup.SelectedItem == null, "请选择案组"))
            {
                return;
            }
            long fileGroup = ((Option)comboBoxFileGroup.SelectedItem).Value;

            if (Util.ExistError(() => comboBoxPieceItem.SelectedItem == null, "请选择计件品项"))
            {
                return;
            }
            long pieceItem = ((Option)comboBoxPieceItem.SelectedItem).Value;

            decimal number = 0;
            if (!Util.TryParseDecimal(txtBoxNumber.Text, (x) => { number = x; }, "请输入数量"))
            {
                return;
            }

            fileGroupValuation.FileGroup_ID = fileGroup;
            fileGroupValuation.FileGroup_Name = comboBoxFileGroup.SelectedItem.ToString();
            fileGroupValuation.PieceItem_ID = pieceItem;
            fileGroupValuation.PieceItem_Name = comboBoxPieceItem.SelectedItem.ToString();
            fileGroupValuation.Number = number;

            var folder = Path.Combine(Util.DataFolder, typeof(ClientFileGroupValuationBillSave).Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = Path.Combine(folder, typeof(ClientFileGroupValuationBillSave).Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientFileGroupValuationBillSave));
            using (var stream = File.Open(file, FileMode.Create))
            {
                serializer.Serialize(stream, fileGroupValuation);
            }

            MessageBox.Show("已保存到本机等待发送");
            ClearInput();
        }

        private void menuItemHistory_Click(object sender, EventArgs e)
        {
            new FileGroupValuationListDialog(fileGroupValuationTemplate.Department_ID).ShowDialog();
        }

        private void FileGroupValuationDialog_Closing(object sender, CancelEventArgs e)
        {
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        private void FileGroupValuationDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonSave_Click(new object(), EventArgs.Empty);
            }
            else if (e.KeyChar == 27)
            {
                ClearInput();
            }
        }

        void ClearInput()
        {
            comboBoxFileGroup.SelectedItem = 0;
            comboBoxFileGroup.Text = "";
            comboBoxPieceItem.SelectedItem = 0;
            comboBoxPieceItem.Text = "";
            ScanIs = 1;
            comboBoxFileGroup.Focus();
            txtBoxNumber.Text = "";
        }

    }
}