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



    [BusinessInterface(typeof(WorkShopCountConfigBL))]
    [LogicName("车间计数配置单")]
    public interface IWorkShopCountConfigBL : IDomainBaseInfoBL<WorkShopCountConfig>
    { }

    public class WorkShopCountConfigBL : DomainBaseInfoBL<WorkShopCountConfig>, IWorkShopCountConfigBL
    {
    }

}
