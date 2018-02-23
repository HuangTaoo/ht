using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;
using BWP.B3ProduceUnitedInfos;
using BWP.B3ProduceUnitedInfos.BO;
using Forks.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;

namespace BWP.B3Butchery.BO
{
	[DFClass]
	[Serializable]
	[LogicName("屠宰分割模块个性设置")]
	public class B3ButcheryUserProfile : DomainUserProfileBase
	{


    [LogicName("会计单位")]
    [DFDataKind(B3FrameworksConsts.DataSources.授权会计单位)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "AccountingUnit_Name")]
    public long? AccountingUnit_ID { get; set; }


    [ReferenceTo(typeof(AccountingUnit), "Name")]
    [Join("AccountingUnit_ID", "ID")]
    [DFPrompt("会计单位")]
    public string AccountingUnit_Name { get; set; }
    [LogicName("生产单位")]
    [DFDataKind(B3ProduceUnitedInfosDataSources.生产单位全部)]
    [DFExtProperty("DisplayField", "ProductionUnit_Name")]
    public long? ProductionUnit_ID { get; set; }

    [LogicName("生产单位")]
    [ReferenceTo(typeof(ProductionUnit), "Name")]
    [Join("ProductionUnit_ID", "ID")]
    public string ProductionUnit_Name { get; set; }


    [LogicName("成品入库默认仓库")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3FrameworksConsts.DataSources.授权仓库)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权仓库全部)]
    [DFExtProperty("DisplayField", "ProductInStore_Store_Name")]
    public long? ProductInStore_Store_ID { get; set; }

    [ReferenceTo(typeof(Store), "Name")]
    [Join("ProductInStore_Store_ID", "ID")]
    [LogicName("成品入库默认仓库")]
    public string ProductInStore_Store_Name { get; set; }

    [LogicName("速冻入库默认仓库")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3FrameworksConsts.DataSources.授权仓库)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权仓库全部)]
    [DFExtProperty("DisplayField", "FrozenInStore_Store_Name")]
    public long? FrozenInStore_Store_ID { get; set; }

    [ReferenceTo(typeof(Store), "Name")]
    [Join("FrozenInStore_Store_ID", "ID")]
    [LogicName("成品入库默认仓库")]
    public string FrozenInStore_Store_Name { get; set; }

    [LogicName("成品入库单入库时间与操作时间间隔(天)")]
    public int? ProductInStoreDaysBrake { get; set; }

    [LogicName("成品入库单光标位置")]
    public NamedValue<光标位置>? ProductInStoreCursorLocation { get; set; }

    [LogicName("生产完工单光标位置")]
    public NamedValue<光标位置>? ProduceFinishCursorLocation { get; set; }

    [LogicName("生产通知单光标位置")]
    public NamedValue<光标位置>? ProductNoticeCursorLocation { get; set; }




  }
}
