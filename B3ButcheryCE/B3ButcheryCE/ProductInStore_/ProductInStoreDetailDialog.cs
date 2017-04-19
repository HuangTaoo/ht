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

namespace B3HRCE.ProductInStore_
{
    public partial class ProductInStoreDetailDialog : Form
    {
        ClientProductInStoreBillSave productInStore;
        public ProductInStoreDetailDialog(ClientProductInStoreBillSave record)
        {
            InitializeComponent();
            Util.SetSceen(this);
            productInStore = record;
            listView1.BeginUpdate();
            foreach (var detail in record.Details)
            {
                var item = new ListViewItem(detail.ProductNumber);
                item.SubItems.Add(detail.Store_Name);
                item.SubItems.Add(detail.Goods_Name);
                item.SubItems.Add(string.Format("{0}", detail.MainNumber));
                item.SubItems.Add(string.Format("{0}", detail.SecondNumber));
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (productInStore.Details.Count == 0)
            {
                MessageBox.Show("至少录入一条记录");
                return;
            }
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductInStoreBillSave).Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            foreach (var details in productInStore.Details.GroupBy(x=>x.Store_ID))
            {
                var newBill = new ClientProductInStoreBillSave();
                newBill = productInStore;
                newBill.DeviceId = Guid.NewGuid().ToString();
                newBill.Details.Clear();
                foreach (var clientProductInStoreDetail in details)
                {
                    newBill.Details.Add(clientProductInStoreDetail);
                }

                var file = Path.Combine(folder, typeof(ClientProductInStoreBillSave).Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml");

                XmlSerializer serializer = new XmlSerializer(typeof(ClientProductInStoreBillSave));
                using (var stream = File.Open(file, FileMode.Create))
                {
                    serializer.Serialize(stream, productInStore);
                }
            }

            productInStore.Details.Clear();
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.EndUpdate();
            MessageBox.Show("已保存到本机等待发送");
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

        private void ProductInStoreDetailDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_OK_Click(new object(), EventArgs.Empty);
            }
        }
    }
}