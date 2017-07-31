using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("生产计划")]
  [OrganizationLimitedDmo("Department_ID", typeof(Department))]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProductPlan)]

  public class ProductPlan : DepartmentWorkFlowBill
  {
    [LogicName("开始日期")]
    [DFNotEmpty]
    public DateTime? Date { get; set; }

    [LogicName("结束日期")]
    [DFNotEmpty]
    public DateTime? EndDate { get; set; }

    private NamedValue<生产类型>? mProductType = 生产类型.日计划;
    [LogicName("生产类型")]
    public NamedValue<生产类型>? ProductType {
      get { return mProductType; }
      set { mProductType = value; }
  }

    [LogicName("计划号")]
    public string PlanNumber { get; set; }

    [DbColumn(DefaultValue = false)]
    [LogicName("计划号")]
    [DFBoolDisplayFormatter("是", "否")]
    public bool PlanNumbers { get; set; }

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

    private ProductPlan_InputDetailCollection mInputDetails = new ProductPlan_InputDetailCollection();
    [OneToMany(typeof(ProductPlan_InputDetail),"ID")]
    [Join("ID", "ProductPlan_ID")]
    public ProductPlan_InputDetailCollection InputDetails
    {
      get { return mInputDetails; }
      set { mInputDetails = value; }
    }

    private ProductPlan_OutputDetailCollection mOutputDetails = new ProductPlan_OutputDetailCollection();
    [OneToMany(typeof(ProductPlan_OutputDetail),"ID")]
    [Join("ID", "ProductPlan_ID")]
    public ProductPlan_OutputDetailCollection OutputDetails
    {
      get { return mOutputDetails; }
      set { mOutputDetails = value; }
    }
  }
}
