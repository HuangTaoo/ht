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
  [Serializable, DFClass, LogicName("领料单")]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.Picking)]
  public class Picking : DepartmentWorkFlowBill
  {
    DateTime? mDate = DateTime.Today;
    [LogicName("日期")]
    public DateTime? Date
    {
      get { return mDate; }
      set { mDate = value; }
    }


    [DFDataKind(B3FrameworksConsts.DataSources.授权仓库)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "Store_Name")]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权仓库全部)]
    [DFPrompt("仓库")]
    [DFNotEmpty]
    public long? Store_ID { get; set; }

    [LogicName("仓库")]
    [ReferenceTo(typeof(Store), "Name")]
    [Join("Store_ID", "ID")]
    public string Store_Name { get; set; }


    [LogicName("生产线")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3ButcheryDataSource.生产线)]
    [DFExtProperty("DisplayField", "ProductLine_Name")]
    public long? ProductLine_ID { get; set; }

    [ReferenceTo(typeof(ProductLine), "Name")]
    [Join("ProductLine_ID", "ID")]
    [LogicName("生产线")]
    public string ProductLine_Name { get; set; }


    #region 华都

    [LogicName("接收单号")]
    public long? ReceiveBill_ID { get; set; }

#endregion



    private readonly Picking_DetailCollection _details = new Picking_DetailCollection();
    [OneToMany(typeof(Picking_Detail), "ID")]
    [Join("ID", "Picking_ID")]
    public Picking_DetailCollection Details
    {
      get { return _details; }
    }

  }
}
