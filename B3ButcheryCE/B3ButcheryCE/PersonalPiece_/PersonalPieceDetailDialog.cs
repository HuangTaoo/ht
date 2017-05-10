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
using System.Xml.Serialization;

namespace B3ButcheryCE.PersonalPiece_
{
    public partial class PersonalPieceDetailDialog : Form
    {
        ClientPersonalPieceBillSave personalPiece;

        public PersonalPieceDetailDialog(ClientPersonalPieceBillSave record)
        {
            InitializeComponent();
            Util.SetSceen(this);
            personalPiece = record;
            listView1.BeginUpdate();
            foreach (var detail in record.Details)
            {
                var item = new ListViewItem(detail.Employee_Name);
                item.SubItems.Add(detail.Job_Name);
                item.SubItems.Add(detail.PieceItem_Name);
                item.SubItems.Add(string.Format("{0}", detail.Number));
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null)
            {
                return;
            }
            var lastError = listView1.FocusedItem.Tag as String;
            if (!string.IsNullOrEmpty(lastError))
            {
                new ErrorInfoDialog(lastError).ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (personalPiece.Details.Count == 0)
            {
                MessageBox.Show("至少录入一条记录");
                return;
            }
            var folder = Path.Combine(Util.DataFolder, typeof(ClientPersonalPieceBillSave).Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = Path.Combine(folder, typeof(ClientPersonalPieceBillSave).Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientPersonalPieceBillSave));
            using (var stream = File.Open(file, FileMode.Create))
            {
                serializer.Serialize(stream, personalPiece);
            }
            personalPiece.Details.Clear();
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.EndUpdate();
            MessageBox.Show("已保存到本机等待发送");
        }

        private void PersonalPieceDetailDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(new object(), EventArgs.Empty);
            }
        }
    }
}