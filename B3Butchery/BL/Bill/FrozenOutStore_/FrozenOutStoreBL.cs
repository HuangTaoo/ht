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



    [BusinessInterface(typeof(FrozenOutStoreBL))]
    [LogicName("速冻出库")]
    public interface IFrozenOutStoreBL : IDepartmentWorkFlowBillBL<FrozenOutStore>
    {
    }
    public class FrozenOutStoreBL : DepartmentWorkFlowBillBL<FrozenOutStore>, IFrozenOutStoreBL
    {
        protected override void beforeSave(FrozenOutStore dmo)
        {
            base.beforeSave(dmo);
        }
    }
}
