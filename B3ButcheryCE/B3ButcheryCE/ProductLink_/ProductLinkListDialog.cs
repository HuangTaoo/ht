using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using B3ButcheryCE.Rpc_.ClientProductLink_;
using System.Xml.Serialization;

namespace B3ButcheryCE.ProductLink_
{
    public partial class ProductLinkListDialog : Form
    {
        public ProductLinkListDialog(long departmentID)
        {
            InitializeComponent();
            Util.SetSceen(this);
            ClientProductLinkBillSave productLink = null;
            var Details = new List<ClientProductLinkDetail>();
            var path = Path.Combine(Util.DataFolder, typeof(ClientProductLinkBillSave).Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path + @"\", "*.xml");

            listView1.BeginUpdate();
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLinkBillSave));
                    using (var stream = File.Open(file, FileMode.Open))
                    {
                        productLink = serializer.Deserialize(stream) as ClientProductLinkBillSave;
                        if (productLink.Department_ID == departmentID && SysConfig.Current.User_ID == productLink.User_ID)
                        {
                            foreach (var detail in productLink.Details)
                            {
                                Details.Add(detail);
                            }
                        }
                    }
                }

                foreach (var detail in Details.GroupBy(x=>x.Goods_ID))
                {
                    var item = new ListViewItem(string.Format("{0}", productLink.CreateTime));
                    item.SubItems.Add(productLink.CollectType);
                    item.SubItems.Add(detail.FirstOrDefault().ProductNumber);
                    item.SubItems.Add(detail.FirstOrDefault().Goods_Name);
                    item.SubItems.Add(string.Format("{0}", detail.Sum(x => x.MainNumber)));
                    item.SubItems.Add(string.Format("{0}", detail.Sum(x => x.SecondNumber)));
                    item.SubItems.Add(SysConfig.Current.Username);
                    listView1.Items.Add(item);
                }
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
    }
}