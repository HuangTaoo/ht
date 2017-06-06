using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3UnitedInfos.BO;
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


    [ReferenceTo(typeof(Goods), "SecondUnitII")]
    [Join("Goods_ID", "ID")]
    public string Goods_SecondUnitII { get; set; }

    [LogicName("主辅II换算主单位比例")]
    [ReferenceTo(typeof(Goods), "SecondUnitII_MainUnitRatio")]
    [Join("Goods_ID", "ID")]
    public Money<decimal>? Goods_SecondUnitII_MainUnitRatio { get; set; }

    [LogicName("主辅II换算辅单位比例")]
    [ReferenceTo(typeof(Goods), "SecondUnitII_SecondUnitRatio")]
    [Join("Goods_ID", "ID")]
    public Money<decimal>? Goods_SecondUnitII_SecondUnitRatio { get; set; }

  }

  [Serializable]
  public class ProductPlan_OutputDetailCollection : DmoCollection<ProductPlan_OutputDetail>
  { }
}
