﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.Rpc_.ClientProductInStore_;
using System.IO;
using System.Xml.Serialization;

namespace B3ButcheryCE.ProductInStore_
{
    public partial class ProductInStoreListDialog : Form
    {
        public ProductInStoreListDialog(long departmentID)
        {
            InitializeComponent();
            Util.SetSceen(this);
            ClientProductInStoreBillSave productInStore = null;
            var Details = new List<ClientProductInStoreDetail>();
            var path = Path.Combine(Util.DataFolder, typeof(ClientProductInStoreBillSave).Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path + @"\", "*.xml");

            listView1.BeginUpdate();
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClientProductInStoreBillSave));
                    using (var stream = File.Open(file, FileMode.Open))
                    {
                        productInStore = serializer.Deserialize(stream) as ClientProductInStoreBillSave;
                        if (productInStore.Department_ID == departmentID && SysConfig.Current.User_ID == productInStore.User_ID)
                        {
                            foreach (var detail in productInStore.Details)
                            {
                                Details.Add(detail);
                               
                            }
                        }
                    }
                }
                foreach (var detail in Details.GroupBy(x => x.Goods_ID))
                {
                    var item = new ListViewItem(string.Format("{0}", productInStore.CreateTime.ToString("yyyy-MM-dd")));
                    item.SubItems.Add(detail.FirstOrDefault().ProductNumber);
                    item.SubItems.Add(productInStore.InStoreType_Name);
                    item.SubItems.Add(detail.FirstOrDefault().Store_Name);
                    item.SubItems.Add(detail.FirstOrDefault().Goods_Name);
                    item.SubItems.Add(string.Format("{0}", detail.Sum(x=>x.MainNumber)));
                    item.SubItems.Add(string.Format("{0}", detail.Sum(x=>x.SecondNumber)));
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