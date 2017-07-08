using System;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{

  [Serializable, DFClass, LogicName("投入单明细")]

  public class ProduceInput_Detail : GoodsDetail
  {
    public long ProduceInput_ID { get; set; }

    #region 客户模块字段
    [LogicName("重量")]
    public decimal? Weight { get; set; }

    [LogicName("均重")]
    public decimal? AverageWeight { get; set; }
    #endregion
  }

  [Serializable]
  public class ProduceInput_DetailCollection : DmoCollection<ProduceInput_Detail>
  { }
}
