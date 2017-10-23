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
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [LogicName("速冻入库配置单明细")]
  [Serializable]
  [DFClass]
 public  class FrozenInStoreSetBill_Detail:Base
  {
    public long FrozenInStoreSetBill_ID { get; set; }

    [LogicName("计数存货")]
    public long CalculateGoods_ID { get; set; }

    [ReferenceTo(typeof(CalculateGoods), "Name")]
    [Join("CalculateGoods_ID", "ID")]
    [LogicName("计数存货名称")]
    public string CalculateGoods_Name { get; set; }

    [ReferenceTo(typeof(CalculateGoods), "Code")]
    [Join("CalculateGoods_ID", "ID")]
    [LogicName("计数存货编号")]
    public string CalculateGoods_Code { get; set; }

    [LogicName("主单位")]
    [ReferenceTo(typeof(CalculateGoods), "MainUnit")]
    [Join("CalculateGoods_ID", "ID")]
    public string CalculateGoods_MainUnit { get; set; }

    [LogicName("辅单位")]
    [ReferenceTo(typeof(CalculateGoods), "SecondUnit")]
    [Join("CalculateGoods_ID", "ID")]
    public string CalculateGoods_SecondUnit { get; set; }


    [LogicName("主辅换算主单位比例")]
    [ReferenceTo(typeof(CalculateGoods), "MainUnitRatio")]
    [Join("CalculateGoods_ID", "ID")]
    public decimal? MainUnitRatio { get; set; }

    [LogicName("主辅换算辅单位比例")]
    [ReferenceTo(typeof(CalculateGoods), "SecondUnitRatio")]
    [Join("CalculateGoods_ID", "ID")]
    public decimal? SecondUnitRatio { get; set; }

    [LogicName("主辅转换方向")]
    [ReferenceTo(typeof(CalculateGoods), "UnitConvertDirection")]
    [Join("CalculateGoods_ID", "ID")]
    public NamedValue<主辅转换方向>? UnitConvertDirection { get; set; }

    [LogicName("辅单位II")]
    [ReferenceTo(typeof(CalculateGoods), "SecondUnitII")]
    [Join("CalculateGoods_ID", "ID")]
    public string SecondUnitII { get; set; }

    [LogicName("主辅II换算主单位比例")]
    [ReferenceTo(typeof(CalculateGoods), "SecondUnitII_MainUnitRatio")]
    [Join("CalculateGoods_ID", "ID")]
    public decimal? SecondUnitII_MainUnitRatio { get; set; }

    [LogicName("主辅II换算辅单位比例")]
    [ReferenceTo(typeof(CalculateGoods), "SecondUnitII_SecondUnitRatio")]
    [Join("CalculateGoods_ID", "ID")]
    public decimal? SecondUnitII_SecondUnitRatio { get; set; }

    [LogicName("计数分类")]
    public long? CalculateCatalog_ID { get; set; }

    [LogicName("计数分类")]
    [Join("CalculateCatalog_ID", "ID")]
    [ReferenceTo(typeof(CalculateCatalog), "Name")]
    public string CalculateCatalog_Name { get; set; }

    [LogicName("默认盘数")]
    [Join("CalculateGoods_ID", "ID")]
    [ReferenceTo(typeof(CalculateGoods), "DefaultNumber1")]
    public int? DefaultNumber1 { get; set; }
    
  }

  [Serializable]
  public class FrozenInStoreSetBill_DetailCollection : DmoCollection<FrozenInStoreSetBill_Detail>
  {
  }
}
