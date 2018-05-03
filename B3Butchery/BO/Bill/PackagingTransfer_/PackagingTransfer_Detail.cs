using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Attributes;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using Newtonsoft.Json;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("包装调拨单_明细")]
  public class PackagingTransfer_Detail : Base
  {

    public long PackagingTransfer_ID { get; set; }

    [LogicName("存货")]
    public long? Goods_ID { get; set; }

    [ReferenceTo(typeof(ButcheryGoods), "Name")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货")]
    public string Goods_Name { get; set; }


    [ReferenceTo(typeof(ButcheryGoods), "Code")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货编码")]
    public string Goods_Code { get; set; }

    [ReferenceTo(typeof(ButcheryGoods), "Spec")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货规格")]
    public string Goods_Spec { get; set; }

    [LogicName("包装袋")]
    public long? GoodsPacking_ID { get; set; }

    [ReferenceTo(typeof(ButcheryGoods), "Name")]
    [Join("GoodsPacking_ID", "ID")]
    [DFPrompt("包装袋")]
    public string GoodsPacking_Name { get; set; }

    [DFDataKind(B3ButcheryDataSource.计划号)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductionPlan_PlanNumber")]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.计划号)]
    [DFPrompt("生产计划")]
    public long? ProductionPlan_ID { get; set; }

    [LogicName("生产计划")]
    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("ProductionPlan_ID", "ID")]
    public string ProductionPlan_PlanNumber { get; set; }

    [LogicName("主数量")]
    [JsonConverter(typeof(MoneyDecimalJsonConverter))]
    public Money<decimal>? Number { get; set; }

    [LogicName("辅数量")]
    [JsonConverter(typeof(MoneyDecimalJsonConverter))]
    public Money<decimal>? SecondNumber { get; set; }

  }

  [Serializable]
  public class PackagingTransfer_DetailCollection : DmoCollection<PackagingTransfer_Detail>
  {
  }
}
