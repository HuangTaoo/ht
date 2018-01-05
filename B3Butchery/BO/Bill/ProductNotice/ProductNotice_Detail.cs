using System;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using BWP.B3SaleInterface.BO;
using BWP.B3Frameworks.BO;
using TSingSoft.WebControls2;
using BWP.B3Frameworks;

namespace BWP.B3Butchery.BO {
  [Serializable, DFClass, LogicName("生产通知单明细")]
  [OrganizationLimitedDmo("Detail_Customer_ID", typeof(ICustomer))]
  public class  ProductNotice_Detail : GoodsDetail
  {
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

    #region 耘垦模块
    [DFDataKind("B3Sale_客户")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, "B3Sale_客户全部")]
    [DFExtProperty("DisplayField", "Detail_Customer_Name")]
    [LogicName("客户")]
    public long? Detail_Customer_ID { get; set; }

    [LogicName("客户")]
    [Join("Detail_Customer_ID", "ID")]
    [ReferenceTo(typeof(ICustomer), "Name")]
    public string Detail_Customer_Name { get; set; }

    [LogicName("排序号")]
    public long? OrderByID { get; set; }
    #endregion

  }

  [Serializable]
  public class ProductNotice_DetailCollection : DmoCollection<ProductNotice_Detail> {

  }
}
