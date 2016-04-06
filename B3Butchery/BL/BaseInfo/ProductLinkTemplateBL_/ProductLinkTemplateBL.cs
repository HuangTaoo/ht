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
	[BusinessInterface(typeof(ProductLinkTemplateBL))]
	[LogicName("生产环节模板")]
	public interface IProductLinkTemplateBL : IDomainBaseInfoBL<ProductLinkTemplate>
	{ }

	public class ProductLinkTemplateBL : DomainBaseInfoBL<ProductLinkTemplate>, IProductLinkTemplateBL
	{
	}
}
