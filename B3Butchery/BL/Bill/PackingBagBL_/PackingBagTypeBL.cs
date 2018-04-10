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
    [BusinessInterface(typeof(PackingBagTypeBL))]
    [LogicName("内包材领用配置单")]
    public interface IPackingBagTypeBL : IDomainBillBL<PackingBagType>
    {

    }

    public class PackingBagTypeBL : DomainBillBL<PackingBagType>, IPackingBagTypeBL
    {
       
    }
}
