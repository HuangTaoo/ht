using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{

    //仙坛客户端 车间包装生成的单据

    [Serializable, DFClass, LogicName("车间包装")]
    [OrganizationLimitedDmo("Department_ID", typeof(Department))]
    //[DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProduceOutput)]

    public class WorkShopPackBill: DepartmentWorkFlowBill
    {
    
//
//
//        [LogicName("中间服务器单据ID")]
//        public long? MiddleWorkBillID { get; set; }

        [LogicName("仓库")]
        [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
        [DFDataKind(B3FrameworksConsts.DataSources.授权仓库)]
        [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权仓库全部)]
        [DFExtProperty("DisplayField", "Store_Name")]
        public long? Store_ID { get; set; }

        [ReferenceTo(typeof(Store), "Name")]
        [Join("Store_ID", "ID")]
        [LogicName("仓库")]
        public string Store_Name { get; set; }

        [DbColumn(DbType = SqlDbType.NVarChar, Length = 200)]
        //叉车码
        [LogicName("叉车码")]
        public string ChaCarBarCode { get; set; }


        private  DateTime? _date = DateTime.Today;


        [LogicName("日期")]

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
