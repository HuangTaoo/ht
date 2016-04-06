using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebPluginFramework.TimerTasks;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Tasks
{
  public class NullifyProductNoticeTask : ITimerTask
  {
    public string Name { get { return "作废固定天数之前的生产通知单"; } }

    public long? Days { get; set; }//天数

    volatile static object _lockObj = new object();
    public void Execute()
    {
      if (!Monitor.TryEnter(_lockObj)) {
        throw new SameTaskNotFinishException(this);
      }
      try {
        DoExecute();
      } finally {
        Monitor.Exit(_lockObj);
      }

    }

    private void DoExecute()
    {
      var days = Days ?? 0;
      var date = DateTime.Today.AddDays(-1 * days);

      var ids = FindNeedNullifyBillIDs(date);
      if (ids == null || ids.Count == 0)
        return;

      var bl = BIFactory.Create<IProductNoticeBL>();
      foreach (var item in ids) {
        var dmo = bl.Load(item);
        if (dmo == null)
          continue;
        bl.Nullify(dmo);
      }
    }

    private List<long> FindNeedNullifyBillIDs(DateTime date)
    {
      var dom = new DQueryDom(new JoinAlias(typeof(ProductNotice)));
      dom.Columns.Add(DQSelectColumn.Field("ID"));
      dom.Where.Conditions.Add(DQCondition.EQ("BillState", 单据状态.未审核));
      dom.Where.Conditions.Add(DQCondition.LessThan("Date", date));
      return dom.EExecuteList<long>();
    }

  }
}
