using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
	[DFClass]
	[Serializable]
	[LogicName("屠宰分割模块个性设置")]
	public class B3ButcheryUserProfile : DomainUserProfileBase
	{
		[LogicName("成品入库单入库时间与操作时间间隔(天)")]
		public int? ProductInStoreDaysBrake { get; set; }

    [LogicName("会计单位")]
    [DFDataKind(B3FrameworksConsts.DataSources.授权会计单位)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "AccountingUnit_Name")]
    public long? AccountingUnit_ID { get; set; }


    [ReferenceTo(typeof(AccountingUnit), "Name")]
    [Join("AccountingUnit_ID", "ID")]
    [DFPrompt("会计单位")]
    public string AccountingUnit_Name { get; set; }

  }
}
