using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using System;

namespace BWP.B3Butchery.BO
{
  [LogicName("速冻入库明细")]
  [Serializable]
  [DFClass]
 public class FrozenInStore_Detail : PriceGoodsDetailBase
  {
    public long FrozenInStore_ID { get; set; }
    [LogicName("辅单位数量Ⅱ")]
    public Money<decimal>? SecondNumber2 { get; set; }

    [LogicName("辅单位Ⅱ")]
    [ReferenceTo(typeof(Goods), "SecondUnitII")]
    [Join("Goods_ID", "ID")]
    public string Goods_SecondUnit2 { get; set; }

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
  public class FrozenInStore_DetailCollection : DmoCollection<FrozenInStore_Detail>
  {
  }
}
