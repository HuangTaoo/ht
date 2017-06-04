using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO
{
  [DFClass]
  [LogicName("速冻入库")]
  [Serializable]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.FrozenInStore)]
  
 public class FrozenInStore : DepartmentWorkFlowBill
  {
    private DateTime? _date = BLContext.Today;
    [LogicName("入库日期")]
    [DFNotEmpty]
    public DateTime? Date {
      get { return _date; }
      set { _date = value; }
    }

    [DFDataKind(B3ButcheryDataSource.速冻库)]
    [DFExtProperty("DisplayField", "Store_Name")]
    [DFExtProperty(B3ButcheryDataSource.速冻库, B3ButcheryDataSource.速冻库)]
    [DFPrompt("仓库")]
    [DFNotEmpty]
    public long? Store_ID { get; set; }


    [LogicName("仓库")]
    [ReferenceTo(typeof(FrozenStore), "Name")]
    [Join("Store_ID", "ID")]
    public string Store_Name { get; set; }

    [LogicName("入库类型")]
    [DFDataKind(B3ButcheryDataSource.屠宰分割入库类型)]
    [DFExtProperty("DisplayField", "OtherInStoreType_Name")]
    [DFExtProperty(B3ButcheryDataSource.屠宰分割入库类型全部, B3ButcheryDataSource.屠宰分割入库类型全部)]
    public long? OtherInStoreType_ID { get; set; }

    [LogicName("入库类型")]
    [ReferenceTo(typeof(OtherInStoreType), "Name")]
    [Join("OtherInStoreType_ID", "ID")]
    public string OtherInStoreType_Name { get; set; }

    [DFDataKind(B3ButcheryDataSource.计划号)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductionPlan_PlanNumber")]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.计划号)]
    [DFPrompt("生产计划")]
    public long? ProductionPlan_ID { get; set; }

    [LogicName("生产计划")]
    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("ProductionPlan_ID", "ID")]
    public string ProductionPlan_PlanNumber { get; set; }


    private readonly FrozenInStore_DetailCollection _details = new FrozenInStore_DetailCollection();

    [OneToMany(typeof(FrozenInStore_Detail), "ID")]
    [Join("ID", "FrozenInStore_ID")]
    public FrozenInStore_DetailCollection Details {
      get { return _details; }
    }
  }
}
