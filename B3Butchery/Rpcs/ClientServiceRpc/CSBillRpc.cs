﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.ClientServiceRpc.Dtos;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
  [Rpc]
  public static class CSBillRpc
  {
    static JavaScriptSerializer serializer = new JavaScriptSerializer();
    [Rpc(RpcFlags.SkipAuth)]
    public static string GetAllFrozenInStoreSetBillList(int billstate)
    {
      var dmoquery = new DmoQuery(typeof(FrozenInStoreSetBill));
      dmoquery.Where.Conditions.Add(DQCondition.EQ("BillState",billstate));
      var list= dmoquery.EExecuteList().Cast<FrozenInStoreSetBill>().ToList();

      var jsonStr = JsonConvert.SerializeObject(list);
      //var jsonStr = serializer.Serialize(list);
      return jsonStr;
    }

    [Rpc]
    public static long CreateOutPutByJson(string json)
    {
      var dto = JsonConvert.DeserializeObject<OutPutDto>(json);

      using (var session = Dmo.NewSession())
      {
        var bl = BIFactory.Create<IProduceOutputBL>(session);
        var dmo=new ProduceOutput();

        dmo.Domain_ID = DomainContext.Current.ID;
        dmo.AccountingUnit_ID = dto.AccountingUnit_ID;
        dmo.Department_ID = dto.Department_ID;
        dmo.Time = dto.Time;
        foreach (var dtodetail in dto.Details)
        {
          var detail=new ProduceOutput_Detail();
          detail.Goods_ID = dtodetail.Goods_ID??0;
          detail.Goods_Name = dtodetail.Goods_Name;
          detail.Remark = dtodetail.CalculateSpec_Name;
          detail.Number = dtodetail.Number;
          detail.SecondNumber = dtodetail.SecondNumber;
          detail.SecondNumber2 = dtodetail.SecondNumber2;

          if (detail.Goods_ID == 0)
          {
            var goodsid = GetGoodsIdByName(session, detail.Goods_Name);
            if (goodsid == null || goodsid == 0)
            {
              throw new Exception("没有找到计数名称：" + detail.Goods_Name + " 对应的存货");
            }
            detail.Goods_ID = goodsid.Value;
          }
          dmo.Details.Add(detail);
        }

        bl.Insert(dmo);

        session.Commit();
        return dmo.ID;
        
      }
   
    }

    [Rpc]
    public static long CreateOutPut(ProduceOutput output)
    {
      
        using (var session=Dmo.NewSession())
        {
          var bl = BIFactory.Create<IProduceOutputBL>(session);

          output.Domain_ID = DomainContext.Current.ID;

          foreach (var detail in output.Details)
          {
            if (detail.Goods_ID == 0)
            {
              var goodsid = GetGoodsIdByName(session, detail.Goods_Name);
              if (goodsid == null || goodsid == 0)
              {
                throw new Exception("没有找到计数名称："+detail.Goods_Name+" 对应的存货");
              }
              detail.Goods_ID = goodsid.Value;
            }
          }

          bl.Insert(output);
          return output.ID;
        }
     
      
    }

    static long? GetGoodsIdByName(IDmoSession session, string name)
    {
      var query=new DQueryDom(new JoinAlias(typeof(Goods)));
      query.Where.Conditions.Add(DQCondition.EQ("Name",name));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      return query.EExecuteScalar<long?>(session);
    }
  }
}
