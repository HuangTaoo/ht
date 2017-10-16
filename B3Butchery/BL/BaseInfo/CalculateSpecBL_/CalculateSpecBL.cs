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

  [BusinessInterface(typeof(CalculateSpecBL))]
  [LogicName("计数规格")]
  public interface ICalculateSpecBL : IDomainBaseInfoBL<CalculateSpec>
  { }

  public class CalculateSpecBL : DomainBaseInfoBL<CalculateSpec>, ICalculateSpecBL
  {
  }
}
