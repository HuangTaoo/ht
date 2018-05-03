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
  
  [BusinessInterface(typeof(PackagingTransferBL))]
  [LogicName("包装调拨单")]
  public interface IPackagingTransferBL : IDomainBillBL<PackagingTransfer>
  {

  }

  public class  PackagingTransferBL : DomainBillBL<PackagingTransfer>, IPackagingTransferBL
  {

  }
}
