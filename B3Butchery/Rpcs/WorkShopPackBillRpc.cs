using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class WorkShopPackBillRpc
  {//WorkShopPackBillEdit

    //接口
    [Rpc(RpcFlags.SkipAuth)]
    public static long InertAndCheck(string json)
    {
      //static JavaScriptSerializer serializer = new JavaScriptSerializer();
      //var list = serializer.Deserialize<List<CTuple<long, string, string>>>(result);


      //反序列化读取数据
      WorkShopPackBill jsonDom = JsonConvert.DeserializeObject<WorkShopPackBill>(json);

      long returnid;
      using (new SpecialDomainUserBLScope(jsonDom.CreateUser_Name))
      {
        //判断是否有权限
        if (!BLContext.User.IsInRole("B3Butchery.车间包装.新建"))
        {
          throw new Exception("没有新建权限");
        }

        //事务
        using (var session = Dmo.NewSession())
        {
          var dmo = GetBillByChaCarBarCode(session, jsonDom.ChaCarBarCode);

          //dmo.Domain_ID = DomainContext.Current.ID

          var bl = BIFactory.Create<IWorkShopPackBillBL>(session);

          if (dmo == null)
          {
            foreach (WorkShopRecord record in jsonDom.Details)
            {
              //已知record.PlanNumber_Name
              //record.ProductLine_ID = "select ProductPlan.ID   from   ProductPlan   where  ProductPlan.PlanNumber_Name=ProductPlan.PlanNumber_Name";

              record.PlanNumber_ID = GetPlanIDByName(session, record.PlanNumber_Name);
            }
            bl.InitNewDmo(jsonDom);
            //插入单据
            bl.Insert(jsonDom);
            //审核
            bl.Check(jsonDom);
            returnid = jsonDom.ID;
          }
          else
          {
            if (dmo.BillState == 单据状态.已审核)
            {
              bl.UnCheck(dmo);
            }

            foreach (WorkShopRecord item in jsonDom.Details)
            {
              item.PlanNumber_ID = GetPlanIDByName(session, item.PlanNumber_Name);
              dmo.Details.Add(item);
            }
            bl.Update(dmo);
            bl.Check(dmo);
            returnid = dmo.ID;
          }
          //提交事务
          session.Commit();

        }
      }
      return returnid;

    }

    private static long? GetPlanIDByName(IDmoSessionWithTransaction session, string name)
    {
      var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Where.Conditions.Add(DQCondition.EQ("PlanNumber", name));
      var id = session.ExecuteScalar(query);
      if (id != null)
      {
        return Convert.ToInt64(id);
      }
      return null;
    }

    private static WorkShopPackBill GetBillByChaCarBarCode(IDmoSession session, string charBarCode)
    {
      var query = new DmoQuery(typeof(WorkShopPackBill));
      query.Where.Conditions.Add(DQCondition.EQ("ChaCarBarCode", charBarCode));
      var res = (WorkShopPackBill)session.ExecuteScalar(query);
      return res;
    }
    //[Rpc]
    //public static long Insert(string json)
    //{
    //    WorkShopPackBill work=json;
    //  if(!BLContext.User.IsInRole("B3Butchery.车间包装.新建"))
    //  {
    //     throw new Exception("没有新建权限");
    //  }

    //  using(var session1=Dmo.NewSession())
    //  {
    //     var bl=BIFactory.Create<IWorkShopPackBillBL>(session);

    //    bl.InitNewDmo(work);
    //    bl.Insert(work);
    //    bl.Check(work);
    //     session1.Commit();

    //    return work.ID;

    //  }

  }
}


