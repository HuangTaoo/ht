using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forks.JsonRpc.Client.Data;

namespace B3HuaDu_TouchScreen.Client
{
  public static class ClientUtil
  {

    public static List<ClientGoods> EToClientGoodsList(this List<RpcObject> objs)
    {
      var list = new List<ClientGoods>();
      foreach (RpcObject o in objs)
      {
        var goods=new ClientGoods();
        goods.Goods_ID = o.Get<long>("Goods_ID");
        goods.Goods_Name = o.Get<string>("Goods_Name");
        goods.Goods_Code = o.Get<string>("Goods_Code");
        list.Add(goods);
      }
      return list;
    }

    public static List<ClientBaseInfo> EToClientBaseInfoList(this List<RpcObject> objs, string toponename="")
    {
      var list = new List<ClientBaseInfo>();
      if (!string.IsNullOrWhiteSpace(toponename))
      {
        list.Add(new ClientBaseInfo(){ID=0,Name = toponename });
      }

      foreach (RpcObject rpcObject in objs)
      {
        var info=new ClientBaseInfo();
        info.ID = rpcObject.Get<long>("ID");
        info.Name = rpcObject.Get<string>("Name");
        list.Add(info);
      }
      return list;
    }
  }
}
