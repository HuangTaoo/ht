using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class ProduceOutputRpc
  {

    [Rpc]
    public static long PdaInsert(ProduceOutput dmo)
    {
      using (var context=new TransactionContext())
      {
        var bl= BIFactory.Create<IProduceOutputBL>(context.Session);
        dmo.Time = DateTime.Today;
        dmo.IsHandsetSend = true;
        dmo.Employee_ID = GetCurrentBindingEmployeeID(context.Session);
        //        bl.InitNewDmo(dmo); 板块信息由手持机传入
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }


    [Rpc]
    public static long Insert(ProduceOutput dmo)
    {
      var bl = BIFactory.Create<IProduceOutputBL>();
      bl.InitNewDmo(dmo);
      bl.Insert(dmo);
      return dmo.ID;
    }

    private static long? GetCurrentBindingEmployeeID(IDmoSession session)
    {
      if (BLContext.User.RoleSchema != B3FrameworksConsts.RoleSchemas.employee)
      {
        throw new Exception("当前用户不是员工类型");
      }
      var query = new DQueryDom(new JoinAlias(typeof(User_Employee)));
      query.Where.Conditions.Add(DQCondition.EQ("User_ID", BLContext.User.ID));
      query.Columns.Add(DQSelectColumn.Field("Employee_ID"));

      var result = (long?)query.EExecuteScalar(session);
      return result;
    }


  }
}
