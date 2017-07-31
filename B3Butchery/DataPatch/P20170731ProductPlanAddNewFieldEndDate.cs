using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using TSingSoft.WebPluginFramework.Install;

namespace BWP.B3Butchery.DataPatch
{

  [DataPatch]
  public class P20170731ProductPlanAddNewFieldEndDate : IDataPatch
  {
    public void Execute(TransactionContext context)
    {
      var sql = @"update B3Butchery_ProductPlan set [EndDate] = [Date] where  [EndDate]  is null";
      context.Session.ExecuteSqlNonQuery(sql);
    }
  }
}
