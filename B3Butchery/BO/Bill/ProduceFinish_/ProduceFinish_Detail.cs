using System;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataDictionary;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO {

  [LogicName("生产完工单明细")]
  [DFClass]
  [Serializable]
  public partial class ProduceFinish_Detail : GoodsDetail {
    [LogicName("生产完工单ID")]
    [DFPrompt("生产完工单号")]
    [DbColumn(Index = IndexType.Normal )]
    public long  ProduceFinish_ID { get; set; }

    [LogicName("工艺描述")]
    [DbColumn(Length = 500)]
    public string TechnicalDescribe { get; set; }

    [LogicName("会计单位ID")]
    [DFPrompt("会计单位")]
    public long? AccountingUnit_ID { get; set; }

    [LogicName("生产通知单")]
    public long? ProductNotice_ID { get; set; }

    [LogicName("生产通知单明细ID")]
    public long? ProductNotice_Detail_ID { get; set; }

    [LogicName("单据号")]
    public long? BillID { get; set; }

    [LogicName("单据类型")]
    [DmoTypeIDFormat]
    public short? BillType { get; set; }

  }

  [Serializable]
  public class ProduceFinish_DetailCollection : DmoCollection<ProduceFinish_Detail> {
 
  }
}
