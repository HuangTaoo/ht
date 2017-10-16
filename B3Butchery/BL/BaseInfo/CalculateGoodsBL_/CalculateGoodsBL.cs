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
  [BusinessInterface(typeof(CalculateGoodsBL))]
  [LogicName("计数存货")]
  public interface ICalculateGoodsBL : IDomainBaseInfoBL<CalculateGoods>
  { }

  public class CalculateGoodsBL : DomainBaseInfoBL<CalculateGoods>, ICalculateGoodsBL
  {
  }
}
