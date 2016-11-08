using System;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("暂存单")]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.TemporaryStorage)]
  public class TemporaryStorage : DepartmentWorkFlowBill
  {
    [LogicName("生产计划号")]
    [DFExtProperty("DisplayField", "ProductPlan_Name")]
    [DFExtProperty("QueryDataKind", B3ButcheryDataSource.计划号)]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3ButcheryDataSource.计划号)]
    public long? ProductPlan_ID { get; set; }

    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("ProductPlan_ID", "ID")]
    [LogicName("生产计划号")]
    public string ProductPlan_Name { get; set; }

    [LogicName("日期")]
    public DateTime? Date { get; set; }

    [LogicName("暂存类型")]
    public NamedValue<暂存类型>? TemporaryStorageType { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep1ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep1ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep2ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep2ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep3ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep3ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep4ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep4ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep5ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep5ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep6ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep6ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep7ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep7ID { get; set; }

    [ReferenceTo(typeof(Department), "TreeDeep8ID")]
    [Join("Department_ID", "ID")]
    public long? Department_TreeDeep8ID { get; set; }

    private TemporaryStorage_DetailCollection _mDetails = new TemporaryStorage_DetailCollection();
    [OneToMany(typeof(TemporaryStorage_Detail), "ID")]
    [Join("ID", "TemporaryStorage_ID")]
    public TemporaryStorage_DetailCollection Details
    {
      get { return _mDetails; }
      set { _mDetails = value; }
    }
  }
}
