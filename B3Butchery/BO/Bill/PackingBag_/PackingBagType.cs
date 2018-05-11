using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using DocumentFormat.OpenXml.Math;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("包材领用配置单")]
  public class PackingBagType : DomainBill
  {
       


        [Join("Department_ID", "ID")]
    [LogicName("部门深度")]
    [ReferenceTo(typeof(Department), "Depth")]
    public int? Department_Depth { get; set; }

    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, "授权部门")]
    [DFExtProperty("DisplayField", "Department_Name")]
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

    [LogicName("显示标识")]
    public string DisplayMark { get; set; }


    [LogicName("包装模式")]
    public NamedValue<包装模式>? Packing_Pattern { get; set; }


    [LogicName("包装属性")]
    public NamedValue<包装属性>? Packing_Attr { get; set; }

        [LogicName("生产班组")]
        public string ProductShift_Name { get; set; }


        [LogicName("部门简称")]
        public string Abbreviation { get; set; }



        private readonly PackingBagType_DetailCollection _details = new PackingBagType_DetailCollection();

    [OneToMany(typeof(PackingBagType_Detail), "ID")]
    [Join("ID", "PackingBagType_ID")]
    public PackingBagType_DetailCollection Details
    {
      get { return _details; }
    }



  }
}