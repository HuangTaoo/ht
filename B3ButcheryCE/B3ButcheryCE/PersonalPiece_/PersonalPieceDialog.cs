using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Rpc_.ClientPersonalPiece_;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BWP.Compact.Devices;

namespace B3ButcheryCE.PersonalPiece_
{
    public partial class PersonalPieceDialog : Form
    {
        public PersonalPieceDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        ClientPersonalPiece personalPieceTemplate;
        private int ScanIs = 1;
        ClientPersonalPieceBillSave personalPiece;
        Dictionary<long, Tuple<long?, string>> pieceItemJobPair;

        public PersonalPieceDialog(long departMentID, long template)
        {
            InitializeComponent();
            Util.SetSceen(this);
            personalPiece = new ClientPersonalPieceBillSave
            {
                AccountingUnit_ID = SysConfig.Current.AccountingUnit_ID.Value,
                Department_ID = departMentID,
                Domain_ID = SysConfig.Current.Domain_ID,
                User_ID = SysConfig.Current.User_ID,
                CreateTime = DateTime.Now
            };
            pieceItemJobPair = new Dictionary<long, Tuple<long?, string>>();

            comboBoxEmployee.Items.Add("");
            comboBoxPieceItem.Items.Add("");

            string file = Path.Combine(Path.Combine(Util.DataFolder, typeof(ClientPersonalPiece).Name), template.ToString() + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientPersonalPiece));
            using (var stream = File.Open(file, FileMode.Open))
            {
                personalPieceTemplate = serializer.Deserialize(stream) as ClientPersonalPiece;
            }
            foreach (var empDetail in personalPieceTemplate.EmployeeDetails)
            {
                if (empDetail.Employee_ID != 0)
                    comboBoxEmployee.Items.Add(new Option(empDetail.Employee_Name+"("+empDetail.Employee_Code+")", empDetail.Employee_ID));
            }
            foreach (var pieceItemDetail in personalPieceTemplate.PieceItemDetails)
            {
                if (pieceItemDetail.PieceItem_ID != 0)
                    comboBoxPieceItem.Items.Add(new Option(pieceItemDetail.PieceItem_Name, pieceItemDetail.PieceItem_ID));
                if (!pieceItemJobPair.ContainsKey(pieceItemDetail.PieceItem_ID))
                    pieceItemJobPair.Add(pieceItemDetail.PieceItem_ID, new Tuple<long?, string>(pieceItemDetail.Job_ID, pieceItemDetail.Job_Name));
            }

            comboBoxEmployee.Focus();
            HardwareUtil.Device.ScannerReader += new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        public void Device_ScannerReaders(object sender, ScanEventArgs e)
        {
            bool hasEmployeeCode = false;
            bool hasPieceItemCode = false;

            if (ScanIs == 1)
            {
                //员工
                if (personalPieceTemplate.EmployeeDetails.Count > 0)
                {
                    foreach (var empDetail in personalPieceTemplate.EmployeeDetails)
                    {
                        var code = empDetail.Employee_Code;

                        if (code == e.BarCode)
                        {
                            hasEmployeeCode = true;
                            comboBoxEmployee.SelectedItem = empDetail.Employee_ID;
                            comboBoxEmployee.Text = empDetail.Employee_Name;
                            comboBoxPieceItem.Focus();
                            break;
                        }
                    }
                }

                if (!hasEmployeeCode)
                {
                    MessageBox.Show("模板中不存在该员工：" + e.BarCode);
                    return;
                }
            }
            else if (ScanIs == 2)
            {
                //计件品项
                if (personalPieceTemplate.PieceItemDetails.Count > 0)
                {
                    foreach (var pieceItemDetail in personalPieceTemplate.PieceItemDetails)
                    {
                        var code = pieceItemDetail.PieceItem_Code;

                        if (code == e.BarCode)
                        {
                            hasPieceItemCode = true;
                            comboBoxPieceItem.SelectedItem = pieceItemDetail.PieceItem_ID;
                            comboBoxPieceItem.Text = pieceItemDetail.PieceItem_Name;
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
            if (Util.ExistError(() => comboBoxEmployee.SelectedItem == null, "请选择员工"))
            {
                return;
            }
            long employee = ((Option)comboBoxEmployee.SelectedItem).Value;

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

            var detail = new ClientPersonalPieceDetail();
            personalPiece.Details.Add(detail);
            detail.Employee_ID = employee;
            detail.Employee_Name = comboBoxEmployee.SelectedItem.ToString();
            detail.PieceItem_ID = pieceItem;
            detail.PieceItem_Name = comboBoxPieceItem.SelectedItem.ToString();
            detail.Number = number;
            detail.Job_ID = pieceItemJobPair[pieceItem].Item1;
            detail.Job_Name = pieceItemJobPair[pieceItem].Item2;
            MessageBox.Show("加入成功。");
            ClearInput();
        }

        private void menuItemHistory_Click(object sender, EventArgs e)
        {
            new PersonalPieceListDialog(personalPieceTemplate.Department_ID).ShowDialog();
        }

        private void PersonalPieceDialog_Closing(object sender, CancelEventArgs e)
        {
            HardwareUtil.Device.ScannerReader -= new EventHandler<ScanEventArgs>(Device_ScannerReaders);
        }

        private void PersonalPieceDialog_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            new PersonalPieceDetailDialog(personalPiece).ShowDialog();
        }

        private void ClearInput()
        {
            comboBoxEmployee.SelectedItem = 0;
            comboBoxEmployee.Text = "";
            comboBoxPieceItem.SelectedItem = 0;
            comboBoxPieceItem.Text = "";
            ScanIs = 1;
            comboBoxEmployee.Focus();
            txtBoxNumber.Text = "";
        }
    }
}