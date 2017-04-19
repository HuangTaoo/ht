using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using B3HRCE.Rpc_.ClientPersonalPiece_;
using System.Xml;
using System.Xml.Serialization;

namespace B3HRCE.PersonalPiece_
{
    public partial class PersonalPieceListDialog : Form
    {
        public PersonalPieceListDialog(long departmentID)
        {
            InitializeComponent();
            Util.SetSceen(this);
            var path = Path.Combine(Util.DataFolder, typeof(ClientPersonalPieceBillSave).Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path + @"\", "*.xml");

            listView1.BeginUpdate();
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClientPersonalPieceBillSave));
                    using (var stream = File.Open(file, FileMode.Open))
                    {
                        var personalPiece = serializer.Deserialize(stream) as ClientPersonalPieceBillSave;
                        if (personalPiece.Department_ID == departmentID && SysConfig.Current.User_ID == personalPiece.User_ID)
                        {
                            foreach (var detail in personalPiece.Details)
                            {
                                var item = new ListViewItem(string.Format("{0}", personalPiece.CreateTime));
                                item.SubItems.Add(detail.Employee_Name);
                                item.SubItems.Add(detail.PieceItem_Name);
                                item.SubItems.Add(string.Format("{0}", detail.Number));
                                item.SubItems.Add(SysConfig.Current.Username);

                                listView1.Items.Add(item);
                            }
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