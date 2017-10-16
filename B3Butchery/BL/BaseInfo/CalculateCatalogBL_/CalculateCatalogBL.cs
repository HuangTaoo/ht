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

  [BusinessInterface(typeof(CalculateCatalogBL))]
  [LogicName("计数分类")]
  public interface ICalculateCatalogBL : IDomainBaseInfoBL<CalculateCatalog>
  { }

  public class CalculateCatalogBL : DomainBaseInfoBL<CalculateCatalog>, ICalculateCatalogBL
  {
  }

}
