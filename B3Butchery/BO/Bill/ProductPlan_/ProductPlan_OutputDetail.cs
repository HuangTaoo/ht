using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.Utils;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("生产计划产出明细")]
  public class ProductPlan_OutputDetail : GoodsDetail
  {
    public long ProductPlan_ID { get; set; }

    [LogicName("计划主数量")]
    public Money<decimal>? PlanNumber { get; set; }

    [LogicName("计划辅数量")]
    public Money<decimal>? PlanSecondNumber { get; set; }
  }

  [Serializable]
  public class ProductPlan_OutputDetailCollection : DmoCollection<ProductPlan_OutputDetail>
  { }
}
