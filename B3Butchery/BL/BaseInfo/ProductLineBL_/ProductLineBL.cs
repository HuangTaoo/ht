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
  [BusinessInterface(typeof(ProductLineBL))]
	[LogicName("生产线")]
  public interface IProductLineBL : IDomainBaseInfoBL<ProductLine>
  { }

  public class ProductLineBL : DomainBaseInfoBL<ProductLine>, IProductLineBL
  {
  }
}
