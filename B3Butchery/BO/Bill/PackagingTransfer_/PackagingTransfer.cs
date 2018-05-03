using System;
using System.Collections.Generic;
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

  [Serializable, DFClass, LogicName("包装调拨单")]
  public  class PackagingTransfer:AccountingUnitBill
  {

    private DateTime? mDate = DateTime.Today;
    [LogicName("日期")]
    public DateTime? Date
    {
      get { return mDate;}
      set { mDate = value; }
    }

    [LogicName("调出部门")]
    [DFDataKind(B3FrameworksConsts.DataSources.部门全部)]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty("DisplayField", "OutDepartment_Name")]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.部门全部)]
    public long? OutDepartment_ID { get; set; }


    [Join("OutDepartment_ID", "ID")]
    [LogicName("调出部门")]
    [ReferenceTo(typeof(Department), "Name")]
    public string OutDepartment_Name { get; set; }



    [LogicName("调入部门")]
    [DFDataKind(B3FrameworksConsts.DataSources.部门全部)]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty("DisplayField", "InDepartment_Name")]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.部门全部)]
    public long? InDepartment_ID { get; set; }

    [Join("InDepartment_ID", "ID")]
    [LogicName("调入部门")]
    [ReferenceTo(typeof(Department), "Name")]
    public string InDepartment_Name { get; set; }



    private readonly PackagingTransfer_DetailCollection _details = new PackagingTransfer_DetailCollection();

    [OneToMany(typeof(PackagingTransfer_Detail), "ID")]
    [Join("ID", "PackagingTransfer_ID")]
    public PackagingTransfer_DetailCollection Details
    {
      get { return _details; }
    }
  }
}
