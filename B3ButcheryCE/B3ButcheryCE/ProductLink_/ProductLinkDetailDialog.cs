using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3HRCE.Rpc_.ClientProductLink_;
using System.IO;
using System.Xml.Serialization;

namespace B3HRCE.ProductLink_
{
    public partial class ProductLinkDetailDialog : Form
    {
        ClientProductLinkBillSave productLink;
        public ProductLinkDetailDialog(ClientProductLinkBillSave record)
        {
            InitializeComponent();
            Util.SetSceen(this);
            productLink = record;
            listView1.BeginUpdate();
            foreach (var detail in record.Details)
            {
                var item = new ListViewItem(detail.ProductNumber);
                item.SubItems.Add(detail.Goods_Name);
                item.SubItems.Add(string.Format("{0}", detail.MainNumber));
                item.SubItems.Add(string.Format("{0}", detail.SecondNumber));
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (productLink.Details.Count == 0)
            {
                MessageBox.Show("至少录入一条记录");
                return;
            }
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductLinkBillSave).Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = Path.Combine(folder, typeof(ClientProductLinkBillSave).Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLinkBillSave));
            using (var stream = File.Open(file, FileMode.Create))
            {
                serializer.Serialize(stream, productLink);
            }
            productLink.Details.Clear();
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.EndUpdate();
            MessageBox.Show("已保存到本机等待发送");
        }

        private void ProductLinkDetailDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_OK_Click(new object(), EventArgs.Empty);
            }
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
    }
}