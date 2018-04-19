using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [DFClass]
  [LogicName("包装领用")]
  [Serializable]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.PackingRecipients)]

  public class PackingRecipients:DepartmentWorkFlowBill
  {

    private DateTime? mDate = DateTime.Today;
    [LogicName("日期")]
//    [DFExtProperty("WebControlType", DFEditControl.DateTimeInput)]
    public DateTime? Date {
      get { return mDate; }
      set { mDate = value; }
    }


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

    [LogicName("计划号")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3ButcheryDataSource.计划号)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.计划号)]
    [DFNotEmpty]
    [DFExtProperty("DisplayField", "PlanNumber_Name")]
    public long? PlanNumber_ID { get; set; }

    [LogicName("计划号")]
    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("PlanNumber_ID", "ID")]
    public string PlanNumber_Name { get; set; }

    private PackingRecipients_DetailCollection mDetails = new PackingRecipients_DetailCollection();
    [OneToMany(typeof(PackingRecipients_Detail), "ID")]
    [Join("ID", "PackingRecipients_ID")]
    public PackingRecipients_DetailCollection Details
    {
      get { return mDetails; }
      set { mDetails = value; }
    }


  }
}
