using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;

namespace BWP.B3Butchery.BO
{
	[DFClass]
	[Serializable]
	[LogicName("屠宰分割模块个性设置")]
	public class B3ButcheryUserProfile : DomainUserProfileBase
	{
		[LogicName("成品入库单入库时间与操作时间间隔(天)")]
		public int? ProductInStoreDaysBrake { get; set; }
	}
}
