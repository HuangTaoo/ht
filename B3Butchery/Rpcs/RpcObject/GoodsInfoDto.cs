using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class GoodsInfoDto
  {
    public long Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public string Goods_Code { get; set; }
    public string Goods_Spell { get; set; }
    public string Goods_Spec { get; set; }

    public string Goods_MainUnit { get; set; }

    public string Goods_SecondUnit { get; set; }

    public NamedValue<主辅转换方向>? Goods_UnitConvertDirection { get; set; }

    public Money<decimal>? Goods_MainUnitRatio { get; set; }

    public Money<decimal>? Goods_SecondUnitRatio { get; set; }

    public long? GoodsProperty_ID { get; set; }
    public string GoodsProperty_Name { get; set; }
    public string GoodsPropertyCatalog_Name { get; set; }
    public int? GoodsPropertyCatalog_Sort { get; set; }

    [LogicName("辅单位II")]
    public string SecondUnitII { get; set; }

    [LogicName("主辅II换算主单位比例")]
    public Money<decimal>? SecondUnitII_MainUnitRatio { get; set; }

    [LogicName("主辅II换算辅单位比例")]
    public Money<decimal>? SecondUnitII_SecondUnitRatio { get; set; }

    [LogicName("包装数")]
    public decimal? InnerPackingPer { get; set; }

    [LogicName("内包装比例")]
    public decimal? Goods_InnerPackingPer { get; set; }

    [LogicName("主数量")]
    public decimal? Number { get; set; }

  }
}
