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

namespace B3HRCE.FrozenInStoreConfirm_
{
    public partial class FrozenInStoreConfirmList : Form
    {

        long fID;
        public FrozenInStoreConfirmList(long id)
        {
            InitializeComponent();
            fID = id;
        }

        private void FrozenInStoreConfirmList_Load(object sender, EventArgs e)
        {
            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/FrozenInStoreRpc/GetRpcEasyFrozenInStoreDetailById", fID);
            listView1.BeginUpdate();
            foreach (RpcObject item in list)
            {
                var lvItem = new ListViewItem(item.Get<long>("Goods_Name").ToString());
                lvItem.SubItems.Add(item.Get<DateTime>("Number").ToShortDateString());
                listView1.Items.Add(lvItem);
            }
            listView1.EndUpdate();
        }
    }
}