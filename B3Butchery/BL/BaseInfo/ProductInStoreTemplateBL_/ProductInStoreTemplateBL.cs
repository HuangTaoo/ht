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
	[BusinessInterface(typeof(ProductInStoreTemplateBL))]
	[LogicName("成品入库模板")]
	public interface IProductInStoreTemplateBL : IDomainBaseInfoBL<ProductInStoreTemplate>
	{ }

	public class ProductInStoreTemplateBL : DomainBaseInfoBL<ProductInStoreTemplate>, IProductInStoreTemplateBL
	{
	}
}
