using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using B3HuaDu_TouchScreen.Client;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;

namespace B3Butchery_TouchScreen
{
  public partial class SelectGoodsForm : Form
  {
    public long mGoodsID;
    public string mGoodsName;

    public SelectGoodsForm()
    {
      InitializeComponent();
      dataGridView1.AutoGenerateColumns = false;
    }

    private void SelectGoodsForm_Load(object sender, EventArgs e)
    {

    }

    void DataGridViewBind()
    {
      var input = txtInput.Text.Trim();
      List<RpcObject> list;
      if (string.IsNullOrWhiteSpace(input))
      {
         list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/GoodsInfoRpc/GetAllGoodsWithOutQuery");
      }
      else
      {
        list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/GoodsInfoRpc/GetAllGoods", input);
      }
      var clientList = GetClientGoodsList(list);
      dataGridView1.DataSource = clientList;
    }

    private List<ClientGoods> GetClientGoodsList(List<RpcObject> rpcObjects)
    {
      var list=new List<ClientGoods>();
      foreach (RpcObject obj in rpcObjects)
      {
        var goods=new ClientGoods();
        goods.Goods_ID = obj.Get<long>("Goods_ID");
        goods.Goods_Code = obj.Get<string>("Goods_Code");
        goods.Goods_Name = obj.Get<string>("Goods_Name");
        goods.Goods_Spec = obj.Get<string>("Goods_Spec");
        list.Add(goods);
      }
      return list;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DataGridViewBind();
    }

    private void txtCode_TextChanged(object sender, EventArgs e)
    {
      DataGridViewBind();
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
      {
        return;
      }
      if (e.ColumnIndex == 4)
      {
        var goodsid =Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        var goodsName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        mGoodsID = goodsid;
        mGoodsName = goodsName;
        DialogResult=DialogResult.OK;
        Close();
      }
    }
  }
}
