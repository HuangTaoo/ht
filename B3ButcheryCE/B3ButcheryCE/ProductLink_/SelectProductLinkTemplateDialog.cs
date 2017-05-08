using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using B3HRCE.Rpc_.ClientProductLink_;
using System.Xml.Serialization;

namespace B3HRCE.ProductLink_
{
    public partial class SelectProductLinkTemplateDialog : Form
    {
        public SelectProductLinkTemplateDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            treeView1.BeginUpdate();
            var root = new TreeNode("根目录");
            treeView1.Nodes.Add(root);
            mDic.Add(0, root);
            AddTemplates();
            root.Expand();
            treeView1.EndUpdate();
        }

        private void AddTemplates()
        {
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductLink).Name);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string[] files = Directory.GetFiles(folder, "*.xml");

            foreach (var file in files)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLink));
                using (var stream = File.Open(file, FileMode.Open))
                {
                    var productLink = serializer.Deserialize(stream) as ClientProductLink;
                    var parentNode = GetParentNode(productLink.Department_ID, productLink.Department_Name);
                    var nodes = new TreeNode("[模板]" + productLink.Name) { Tag = productLink.ID };
                    parentNode.Nodes.Add(nodes);
                }
            }

        }

        private TreeNode GetParentNode(long departmentID, string departmentName)
        {
            if (departmentID == 0)
            {
                return mDic[0];
            }
            else
            {
                if (!mDic.ContainsKey(departmentID))
                {
                    var node = new TreeNode("[部门]" + departmentName);
                    mDic.Add(departmentID, node);
                    mDic[0].Nodes.Add(node);
                }
                return mDic[departmentID];
            }
        }

        Dictionary<long, TreeNode> mDic = new Dictionary<long, TreeNode>();

        long departMentID;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var templete = e.Node.Tag;
            if (templete == null)
            {
                return;
            }
            var parentNode = e.Node.Parent.Text;

            string node = e.Node.Text;
            string nodeSub = node.ToString().Substring(node.LastIndexOf(']') + 1);

            foreach (var i in mDic.Keys)
            {
                if (mDic[i].Text.Equals(parentNode))
                {
                    departMentID = i;
                }
            }

            new ProductLinkDialog(departMentID, (long)templete).ShowDialog();
        }
    }
}