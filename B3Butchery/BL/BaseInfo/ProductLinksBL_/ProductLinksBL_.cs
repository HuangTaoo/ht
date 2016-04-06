using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using BWP.B3Frameworks.BL;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductLinksBL))]
	[LogicName("生产环节")]
  public interface IProductLinksBL : IDomainBaseInfoBL<ProductLinks>
  { }

  public class ProductLinksBL : DomainBaseInfoBL<ProductLinks>, IProductLinksBL
  {
  }
}
