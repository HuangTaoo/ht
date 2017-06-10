using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
 public static class FrozenInStoreRpc
  {

    [Rpc]
    public static long PdaInsertAndCheck(FrozenInStore dmo)
    {
      SetSecondNumberByNumber(dmo);
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IFrozenInStoreBL>(context.Session);
        dmo.Date = dmo.Date ?? DateTime.Today;

        dmo.Employee_ID = B3ButcheryUtil.GetCurrentBindingEmployeeID(context.Session);
        bl.InitNewDmo(dmo);
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }
    static void SetSecondNumberByNumber(FrozenInStore dmo)
    {
      foreach (FrozenInStore_Detail detail in dmo.Details)
      {
        DmoUtil.RefreshDependency(detail, "Goods_ID");
        if (detail.Goods_UnitConvertDirection == null)
        {
          continue;
        }

        if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅)
        {
          //辅单位数量
          if (detail.Goods_MainUnitRatio != null && detail.Goods_SecondUnitRatio != null)
          {
            detail.SecondNumber = detail.Number * detail.Goods_SecondUnitRatio / detail.Goods_MainUnitRatio;
          }
          //辅单位Ⅱ数量
          if (detail.Goods_SecondUnitII_MainUnitRatio != null && detail.Goods_SecondUnitII_SecondUnitRatio != null)
          {
            detail.SecondNumber2 = detail.Number * detail.Goods_SecondUnitII_SecondUnitRatio / detail.Goods_SecondUnitII_MainUnitRatio;
          }
        }
      }
    }

  }
}
