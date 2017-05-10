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
using System.IO;
using B3ButcheryCE.Rpc_;
using System.Xml;
using System.Xml.Serialization;

namespace B3ButcheryCE.FileGroupValuation_
{
    public partial class FileGroupValuationListDialog : Form
    {
        public FileGroupValuationListDialog(long departmentID)
        {
            InitializeComponent();
            Util.SetSceen(this);

            var path = Path.Combine(Util.DataFolder, typeof(ClientFileGroupValuationBillSave).Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string[] files = Directory.GetFiles(path + @"\", "*.xml");

            listView1.BeginUpdate();
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClientFileGroupValuationBillSave));
                    using (var stream = File.Open(file, FileMode.Open))
                    {
                        var fileGroupValuation = serializer.Deserialize(stream) as ClientFileGroupValuationBillSave;
                        if (fileGroupValuation.Department_ID == departmentID && SysConfig.Current.User_ID == fileGroupValuation.User_ID)
                        {
                            var item = new ListViewItem(string.Format("{0}", fileGroupValuation.CreateTime));
                            item.SubItems.Add(string.Format("{0}", fileGroupValuation.FileGroup_Name));
                            item.SubItems.Add(string.Format("{0}", fileGroupValuation.PieceItem_Name));
                            item.SubItems.Add(string.Format("{0}", fileGroupValuation.Number));
                            item.SubItems.Add(string.Format("{0}", SysConfig.Current.Username));

                            listView1.Items.Add(item);
                        }
                    }
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