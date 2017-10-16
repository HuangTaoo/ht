using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [LogicName("速冻入库配置单明细")]
  [Serializable]
  [DFClass]
 public  class FrozenInStoreSetBill_Detail:Base
  {
    public long FrozenInStoreSetBill_ID { get; set; }

    [LogicName("存货")]
    public long Goods_ID { get; set; }

    [ReferenceTo(typeof(Goods), "Name")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货名称")]
    public string Goods_Name { get; set; }

    [ReferenceTo(typeof(Goods), "PrintShortName")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货简称")]
    public string Goods_PrintShortName { get; set; }

    [ReferenceTo(typeof(Goods), "Code")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货编号")]
    public string Goods_Code { get; set; }

    [LogicName("主单位")]
    [ReferenceTo(typeof(Goods), "MainUnit")]
    [Join("Goods_ID", "ID")]
    public string Goods_MainUnit { get; set; }

    [LogicName("辅单位")]
    [ReferenceTo(typeof(Goods), "SecondUnit")]
    [Join("Goods_ID", "ID")]
    public string Goods_SecondUnit { get; set; }

    [LogicName("规格")]
    [ReferenceTo(typeof(Goods), "Spec")]
    [Join("Goods_ID", "ID")]
    public string Goods_Spec { get; set; }

    [LogicName("存货属性")]
    public long? GoodsProperty_ID { get; set; }

    [LogicName("存货属性")]
    [Join("GoodsProperty_ID", "ID")]
    [ReferenceTo(typeof(GoodsProperty), "Name")]
    public string GoodsProperty_Name { get; set; }


    [ReferenceTo(typeof(GoodsProperty), "GoodsPropertyCatalog_Name")]
    [LogicName("存货属性分类")]
    [Join("GoodsProperty_ID", "ID")]
    public string GoodsPropertyCatalog_Name { get; set; }

    [LogicName("默认盘数")]
    public int? DefaultNumber { get; set; }


//    [LogicName("主辅换算辅单位比例")]
//    public Money<decimal>? SecondUnitRatio { get; set; }

    [LogicName("主辅换算主单位比例")]
    public Money<decimal>? MainUnitRatio { get; set; }

  }

  [Serializable]
  public class FrozenInStoreSetBill_DetailCollection : DmoCollection<FrozenInStoreSetBill_Detail>
  {
  }
}
