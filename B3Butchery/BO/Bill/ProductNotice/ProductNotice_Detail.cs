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

    #region 存货品牌
    [LogicName("存货品牌")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3SaleDataSources.树形存货品牌)]
    [DFExtProperty("DisplayField", "GoodsBrand_Name")]
    public long? GoodsBrand_ID { get; set; }

    [ReferenceTo(typeof(GoodsBrand), "Name")]
    [Join("GoodsBrand_ID", "ID")]
    [LogicName("存货品牌")]
    public string GoodsBrand_Name { get; set; }

    [ReferenceTo(typeof(GoodsBrand), "Depth"), LogicName("分类深度"), Join("GoodsBrand_ID", "ID")]
    public int? GoodsBrand_Depth { get; set; }

    [NonDmoProperty, LogicName("分类树形名称")]
    public string CatalogTreeName
    {
        get
        {

            return (TreeUtil.GetTreePrefix(this.GoodsBrand_Depth) + this.GoodsBrand_Name);
        }
    }

    [ReferenceTo(typeof(GoodsBrand), "TreeDeep1ID"), Join("GoodsBrand_ID", "ID")]
    public long? TreeDeep1ID { get; set; }

    [ReferenceTo(typeof(GoodsBrand), "TreeDeep2ID"), Join("GoodsBrand_ID", "ID")]
    public long? TreeDeep2ID { get; set; }

    [ReferenceTo(typeof(GoodsBrand), "TreeDeep3ID"), Join("GoodsBrand_ID", "ID")]
    public long? TreeDeep3ID { get; set; }

    [Join("GoodsBrand_ID", "ID"), ReferenceTo(typeof(GoodsBrand), "TreeDeep4ID")]
    public long? TreeDeep4ID { get; set; }

    [ReferenceTo(typeof(GoodsBrand), "TreeDeep5ID"), Join("GoodsBrand_ID", "ID")]
    public long? TreeDeep5ID { get; set; }

    [Join("GoodsBrand_ID", "ID"), ReferenceTo(typeof(GoodsBrand), "TreeDeep6ID")]
    public long? TreeDeep6ID { get; set; }

    [Join("GoodsBrand_ID", "ID"), ReferenceTo(typeof(GoodsBrand), "TreeDeep7ID")]
    public long? TreeDeep7ID { get; set; }

    [Join("GoodsBrand_ID", "ID"), ReferenceTo(typeof(GoodsBrand), "TreeDeep8ID")]
    public long? TreeDeep8ID { get; set; }
    #endregion
  }

  [Serializable]
  public class ProductNotice_DetailCollection : DmoCollection<ProductNotice_Detail> {

  }
}
