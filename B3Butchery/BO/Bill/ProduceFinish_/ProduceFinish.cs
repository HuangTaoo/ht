using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3ProduceUnitedInfos;
using BWP.B3ProduceUnitedInfos.BO;
using BWP.B3SaleInterface.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO {
  [LogicName("生产完工单")]
  [Serializable]
  [DFClass]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProduceFinish)]
  public class ProduceFinish : DepartmentWorkFlowBill {
    [LogicName("生产单位ID")]
    [DFPrompt("生产单位")]
    [DFDataKind(B3ProduceUnitedInfosDataSources.生产单位全部)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductionUnit_Name")]
    public long? ProductionUnit_ID { get; set; }

    private DateTime mDate = DateTime.Today;

    [LogicName("日期")]
    [DFNotEmpty]
    public DateTime Date {
      get { return mDate; }
      set { mDate = value; }
    }

    [DFDataKind("B3Sale_客户")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, "B3Sale_客户全部")]
    [DFExtProperty("DisplayField", "Customer_Name")] 
    [LogicName("客户ID")]
    [DFPrompt("客户")]
    public long? Customer_ID { get; set; }

    [LogicName("存货数量")]
    public Money<decimal>? AllNum { get; set; }

    [LogicName("总辅数量")]
    public Money<decimal>? AllSecondNumber { get; set; }

    private readonly ProduceFinish_DetailCollection mDetails = new ProduceFinish_DetailCollection();
    [OneToMany(typeof(ProduceFinish_Detail), "ID")]
    [Join("ID", "ProduceFinish_ID")]
    public ProduceFinish_DetailCollection Details {
      get { return mDetails; }
    }

    #region ReferenceTo

    [LogicName("生产单位编号")]
    [ReferenceTo(typeof(ProductionUnit), "Code")]
    [Join("ProductionUnit_ID", "ID")]
    public string ProductionUnit_Code { get; set; }

    [LogicName("生产单位名称")]
    [ReferenceTo(typeof(ProductionUnit), "Name")]
    [Join("ProductionUnit_ID", "ID")]
    public string ProductionUnit_Name { get; set; }
     
    [LogicName("客户")]
    [Join("Customer_ID", "ID")]
    [ReferenceTo(typeof(ICustomer), "Name")]
    public string Customer_Name { get; set; }

    #endregion


  }

}
