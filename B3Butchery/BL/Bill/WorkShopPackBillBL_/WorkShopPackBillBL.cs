using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BL
{


    [BusinessInterface(typeof(WorkShopPackBillBL))]
    [LogicName("车间包装")]
    public interface IWorkShopPackBillBL : IDepartmentWorkFlowBillBL<WorkShopPackBill>
    { }

    public class WorkShopPackBillBL : DepartmentWorkFlowBillBL<WorkShopPackBill>, IWorkShopPackBillBL
    {
     

    }
}
