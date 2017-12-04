using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos;
using Forks.EnterpriseServices.DataForm;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.Utils;
using BWP.B3Frameworks.BO.MoneyTemplate;
using TSingSoft.WebControls2;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3UnitedInfos.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;

namespace BWP.B3Butchery.BO
{

  [Serializable, DFClass]
  public abstract class GoodsDetail : GoodsDetailSummaryBase
  {
    [LogicName("单价")]
    public Money<decimal>? Price { get; set; }

    [LogicName("金额")]
    public Money<金额>? Money { get; set; }

    [LogicName("存货批号ID")]
    [DFPrompt("批号")]
    public long? GoodsBatch_ID { get; set; }

    [LogicName("税率")]
    [DFExtProperty("WebControlType", DFEditControl.PercentageInput)]
    public Money<税率>? TaxRate { get; set; }

    [LogicName("不含税单价")]
    public Money<decimal>? NTaxPrice { get; set; }

    [LogicName("不含税金额")]
    public Money<金额>? NTaxMoney { get; set; }

    [DFDataKind(B3UnitedInfosConsts.DataSources.品牌项)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "BrandItem_Name")]
    [LogicName("品牌项")]
    public long? BrandItem_ID { get; set; }

    #region ReferenceTo

    [ReferenceTo(typeof(BrandItem), "Name")]
    [Join("BrandItem_ID", "ID")]
    [DFPrompt("品牌项")]
    public string BrandItem_Name { get; set; }

    [ReferenceTo(typeof(Goods), "TaxRate")]
    [Join("Goods_ID", "ID")]
    public Money<税率>? Goods_TaxRate { get; set; }

    [ReferenceTo(typeof(Goods), "GoodsProperty_ID")]
    [Join("Goods_ID", "ID")]
    public long? GoodsProperty_ID { get; set; }

    [ReferenceTo(typeof(Goods), "GoodsProperty_Name")]
    [Join("Goods_ID", "ID")]
    public string GoodsProperty_Name { get; set; }

    [ReferenceTo(typeof(GoodsBatch), "Name")]
    [Join("GoodsBatch_ID", "ID")]
    [LogicName("批号")]
    public string GoodsBatch_Name { get; set; }

    [LogicName("生产日期")]
    [ReferenceTo(typeof(GoodsBatch), "ProductionDate")]
    [Join("GoodsBatch_ID", "ID")]
    public DateTime? ProductionDate { get; set; }

    [LogicName("入库日期")]
    [ReferenceTo(typeof(GoodsBatch), "InStoreDate")]
    [Join("GoodsBatch_ID", "ID")]
    public DateTime? InStoreDate { get; set; }

    [LogicName("保质天数")]
    [ReferenceTo(typeof(Goods), "QualityDays")]
    [Join("GoodsBatch_ID", "ID")]
    public int? QualityDays { set; get; }

    [LogicName("品牌")]
    [ReferenceTo(typeof(Goods), "Brand")]
    [Join("Goods_ID", "ID")]
    public string Goods_Brand { get; set; }

    #endregion


  }
}
