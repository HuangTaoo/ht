using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using B3HRCE.Rpc_;
using Forks.JsonRpc.Client.Data;

namespace B3ButcheryCE.Rpc_
{
     public class ClientUtil
    {
         public static ClientGoods CreateClientGoods(RpcObject obj)
         {
             var goods = new ClientGoods();
             goods.Goods_ID=obj.Get<long>("Goods_ID");

             goods.Goods_Name = obj.Get<string>("Goods_Name");
             goods.Goods_MainUnit = obj.Get<string>("Goods_MainUnit");
             goods.Goods_SecondUnit = obj.Get<string>("Goods_SecondUnit");

             var d=obj.Get<NamedValue>("Goods_UnitConvertDirection");
             if(d!=null)
             {
                 if (d.Value == 0)
                 {
                     goods.Goods_UnitConvertDirection = GoodsUnitConvertDirection.双向转换;
                 }
                 else if (d.Value == 1)
                 {
                     goods.Goods_UnitConvertDirection = GoodsUnitConvertDirection.由主至辅;
                 }
                 else if (d.Value == 2)
                 {
                     goods.Goods_UnitConvertDirection = GoodsUnitConvertDirection.由辅至主;
                 }
                
             }

             goods.Goods_MainUnitRatio = obj.Get<decimal>("Goods_MainUnitRatio");
             goods.Goods_SecondUnitRatio = obj.Get<decimal>("Goods_SecondUnitRatio");

             return goods;
         }
    }
}
