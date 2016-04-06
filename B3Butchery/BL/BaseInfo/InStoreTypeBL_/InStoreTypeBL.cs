using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL
{
	[BusinessInterface(typeof(InStoreTypeBL))]
	[LogicName("入库类型")]
	public interface IInStoreTypeBL : IDomainBaseInfoBL<InStoreType>
	{ }

	public class InStoreTypeBL : DomainBaseInfoBL<InStoreType>, IInStoreTypeBL
	{
	}
}
