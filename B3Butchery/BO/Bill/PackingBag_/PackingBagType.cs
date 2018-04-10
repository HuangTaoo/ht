using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using DocumentFormat.OpenXml.Math;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
    [Serializable, DFClass, LogicName("内包材领用配置单")]
    public class PackingBagType : DomainBill 
    {
        [Join("Department_ID", "ID")]
        [LogicName("部门深度")]
        [ReferenceTo(typeof(Department), "Depth")]
        public int? Department_Depth { get; set; }
        [DFDataKind("授权部门")]
        [DFNotEmpty]
        [LogicName("部门")]
        public long? Department_ID { get; set; }
        [Join("Department_ID", "ID")]
        [LogicName("发起部门")]
        [ReferenceTo(typeof(Department), "Name")]
        public string Department_Name { get; set; }

        [LogicName("名称")]
        public string Name { get; set; }


        private readonly PackingBagType_DetailCollection _details = new PackingBagType_DetailCollection();

        [OneToMany(typeof(PackingBagType_Detail), "ID")]
        [Join("ID", "PackingBagType_ID")]
        public PackingBagType_DetailCollection Details
        {
            get { return _details; }
        }
       
    }
}