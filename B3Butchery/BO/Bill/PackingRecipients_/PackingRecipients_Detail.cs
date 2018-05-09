using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("包装领用_明细")]
  public class PackingRecipients_Detail : GoodsDetail
  {
    public long PackingRecipients_ID { get; set; }

    [LogicName("产品名称")]
    public long? GoodsGoods_ID { get; set; }

    [LogicName("产品名称")]
    [ReferenceTo(typeof(ButcheryGoods), "Name")]
    [Join("GoodsGoods_ID", "ID")]
    public string GoodsGoods_Name { get; set; }


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
  }

  [Serializable]
  public class PackingRecipients_DetailCollection : DmoCollection<PackingRecipients_Detail>
  { }
}
