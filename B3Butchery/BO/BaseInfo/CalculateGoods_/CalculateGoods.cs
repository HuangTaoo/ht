using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataDictionary;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("计数名称")]
  public class CalculateGoods :  DomainBaseInfo
  {
    [DbColumn(DbType = SqlDbType.NVarChar, AllowNull = false, Length = 50, Unique = true)]
    [DFNotEmpty]
    [LogicName("编码")]
    public string Code { get; set; }

    
    [LogicName("计数分类")]
    [DFDataKind(B3ButcheryDataSource.计数分类)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.计数分类)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "CalculateCatalog_Name")]
    public long? CalculateCatalog_ID { get; set; }

    [LogicName("计数分类")]
    [ReferenceTo(typeof(CalculateCatalog), "Name")]
    [Join("CalculateCatalog_ID", "ID")]
    public string CalculateCatalog_Name{get;set;}

    [LogicName("主单位")]
    [DFExtProperty("WebControlType", DFEditControl.MemoInput)]
    [DFDataKind("存货单位")]
    public string MainUnit { get; set; }

    [LogicName("辅单位")]
    [DFExtProperty("WebControlType", DFEditControl.MemoInput)]
    [DFDataKind("存货单位")]
    public string SecondUnit { get; set; }

    [LogicName("主辅换算主单位比例")]
    public decimal? MainUnitRatio { get; set; }

    [LogicName("主辅换算辅单位比例")]
    public decimal? SecondUnitRatio { get; set; }

    [LogicName("主辅转换方向")]
    public NamedValue<主辅转换方向>? UnitConvertDirection { get; set; }


    [LogicName("辅单位II")]
    [DFExtProperty("WebControlType", DFEditControl.MemoInput)]
    [DFDataKind("辅单位II")]
    public string SecondUnitII { get; set; }

    [LogicName("主辅II换算主单位比例")]
    public decimal? SecondUnitII_MainUnitRatio { get; set; }

    [LogicName("主辅II换算辅单位比例")]
    public decimal? SecondUnitII_SecondUnitRatio { get; set; }


    [LogicName("默认盘数1")]
    public int? DefaultNumber1 { get; set; }


    [LogicName("存货")]
    [DFDataKind(B3UnitedInfosConsts.DataSources.存货)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3UnitedInfosConsts.DataSources.存货全部)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "Goods_Name")]
    public long? Goods_ID { get; set; }

    [LogicName("存货")]
    [ReferenceTo(typeof(Goods), "Name")]
    [Join("Goods_ID", "ID")]
    public string Goods_Name { get; set; }
  }
}
