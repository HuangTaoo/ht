using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(FrozenStoreBL))]
  [LogicName("入库类型")]
  public interface IFrozenStoreBL : IDomainBaseInfoBL<FrozenStore>
  { }
  public class FrozenStoreBL : DomainBaseInfoBL<FrozenStore>, IFrozenStoreBL
  {

  }
}
