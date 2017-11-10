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

    //仙坛客户端 车间包装生成的单据

    [Serializable, DFClass, LogicName("车间包装")]
    [OrganizationLimitedDmo("Department_ID", typeof(Department))]
    //[DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProduceOutput)]

    public class WorkShopPackBill: DepartmentWorkFlowBill
    {
        private  DateTime? _date = DateTime.Today;


        public DateTime? Date {
            get { return _date; } 
            set { _date = value; } }


        private WorkShopRecordCollection mDetails = new WorkShopRecordCollection();
        [OneToMany(typeof(WorkShopRecord), "ID")]
        [Join("ID", "WorkShopPackBill_ID")]
        public WorkShopRecordCollection Details
        {
            get { return mDetails; }
            set { mDetails = value; }
        }
    }
}
