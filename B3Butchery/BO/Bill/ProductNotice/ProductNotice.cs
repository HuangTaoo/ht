using System;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Attributes;
using BWP.B3Frameworks.BO;
using BWP.B3ProduceUnitedInfos;
using BWP.B3ProduceUnitedInfos.BO;
using BWP.B3SaleInterface.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO {
  [DFClass, Serializable]
  [LogicName("生产通知单")] 
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProductNotice)]
  [EditUrl("~/B3Butchery/Bills/ProductNotice_/ProductNoticeEdit.aspx")]
  [BusinessCaseSlaveBO]
  [DFCPrompt("业务员", Property = "Employee_ID")]
  [DFCPrompt("业务员", Property = "Employee_Name")]
  [DFCPrompt("销售部门", Property = "Department_ID")]
  [DFCPrompt("销售部门", Property = "Department_Name")]
  [DFCPrompt("地址", Property = "CustomerAddress")]
  public class ProductNotice : DepartmentWorkFlowBill
    {
  
    private DateTime? _date = DateTime.Today;

    [LogicName("日期")]
    public DateTime? Date {
      get { return _date; }
      set { _date = value; }
    }

    [DFDataKind("B3Sale_客户")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, "B3Sale_客户全部")]
    [DFExtProperty("DisplayField", "Customer_Name")]
    [LogicName("客户")]
    public long? Customer_ID { get; set; }

    [LogicName("客户")]
    [Join("Customer_ID", "ID")]
    [ReferenceTo(typeof(ICustomer), "Name")]
    public string Customer_Name { get; set; }

    [LogicName("地址")]
    [Join("Customer_ID", "ID")]
    [ReferenceTo(typeof(ICustomer), "CustomerAddress")]
    public string CustomerAddress { get; set; }

    [LogicName("生产单位")]
    [DFDataKind(B3ProduceUnitedInfosDataSources.生产单位全部)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductionUnit_Name")]
    public long? ProductionUnit_ID { get; set; }
     
    private readonly ProductNotice_DetailCollection _details = new ProductNotice_DetailCollection();

    [OneToMany(typeof(ProductNotice_Detail), "ID")]
    [Join("ID", "ProductNotice_ID")]
    public ProductNotice_DetailCollection Details {
      get { return _details; } 
    }

    #region ReferenceTo
  
    [LogicName("生产单位")]
    [ReferenceTo(typeof(ProductionUnit), "Name")]
    [Join("ProductionUnit_ID", "ID")]
    public string ProductionUnit_Name { get; set; }
     
    #endregion

  }
}
