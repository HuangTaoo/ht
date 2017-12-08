
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
    [Serializable, DFClass, LogicName("速冻出库")]
    [OrganizationLimitedDmo("Department_ID", typeof(Department))]
    public class FrozenOutStore:DepartmentWorkFlowBill
    {


        //包装单的单据ID
        [LogicName("包装单")]
        public long? WorkBill_ID { get; set; }

        private DateTime? _date = DateTime.Today;


        [LogicName("日期")]

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }


        private readonly FrozenOutStore_DetailCollection _details = new FrozenOutStore_DetailCollection();

        [OneToMany(typeof(FrozenOutStore_Detail), "ID")]
        [Join("ID", "FrozenOutStore_ID")]
        public FrozenOutStore_DetailCollection Details
        {
            get { return _details; }
        }
    }
}
