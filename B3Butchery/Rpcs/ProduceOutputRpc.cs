using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
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
    public static long PdaInsertAndCheck(ProduceOutput dmo)
    {
      SetSecondNumberByNumber(dmo);
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IProduceOutputBL>(context.Session);
        dmo.Time = dmo.Time ?? DateTime.Today;

        dmo.Employee_ID = GetCurrentBindingEmployeeID(context.Session);
        bl.InitNewDmo(dmo); 
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }

    static void SetSecondNumberByNumber(ProduceOutput dmo)
    {
      foreach (ProduceOutput_Detail detail in dmo.Details)
      {
        DmoUtil.RefreshDependency(detail,"Goods_ID");
        if (detail.Goods_UnitConvertDirection == null)
        {
          continue;
        }
        
        if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅)
        {
          //辅单位数量
          if (detail.Goods_MainUnitRatio != null && detail.Goods_SecondUnitRatio != null)
          {
            detail.SecondNumber =detail.Number * detail.Goods_SecondUnitRatio / detail.Goods_MainUnitRatio ;
          }
          //辅单位Ⅱ数量
          if (detail.Goods_SecondUnitII_MainUnitRatio != null && detail.Goods_SecondUnitII_SecondUnitRatio != null)
          {
            detail.SecondNumber2= detail.Number * detail.Goods_SecondUnitII_SecondUnitRatio / detail.Goods_SecondUnitII_MainUnitRatio;
          }
        }
      }
    }

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
