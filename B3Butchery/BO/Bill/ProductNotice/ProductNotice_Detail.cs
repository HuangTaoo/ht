using System;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using TSingSoft.WebControls2;
using BWP.B3SaleInterface.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3Sale.Utils;


namespace BWP.B3Butchery.BO {
  [Serializable, DFClass, LogicName("生产计划投入明细")]
  public class  ProductNotice_Detail : GoodsDetail {
    public long ProductNotice_ID { get; set; }

    [LogicName("加工要求")]
    [DbColumn(Length = 1000)]
    public string ProduceRequest { get; set; }

    [LogicName("生产日期")]
    public DateTime? ProduceDate { get; set; }

    [LogicName("交货日期")]
    public DateTime? DeliveryDate { get; set; }

    [LogicName("源单据号")]
    public long? DmoID { get; set; }

    [LogicName("源单据类型")]
    [DmoTypeIDFormat]
    public short? DmoTypeID { get; set; }

    [LogicName("源单据明细ID")]
    public long? DmoDetailID { get; set; }

    [LogicName("已完工数量")]
    public Money<decimal>? DoneNumber { get; set; }

    //仙坛模块使用
    [DbColumn(DefaultValue = false)]
    [LogicName("新产品")]
    [DFBoolDisplayFormatter("是", "否")]
    public bool IsNewGoods { get; set; }
     
  }

  [Serializable]
  public class ProductNotice_DetailCollection : DmoCollection<ProductNotice_Detail> {

  }
}
