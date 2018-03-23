using System;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Attributes;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable]
  [LogicName("成品包装配置")]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProductPackaging)]
  [EditUrl("~/B3Butchery/Bills/ProductPackaging_/ProductPackagingEdit.aspx")]
  public class ProductPackaging : DepartmentWorkFlowBill
  {
    private DateTime? _date = DateTime.Today;
    public DateTime? StartDate { get { return _date; } set { _date = value; } }



    readonly ProductPackaging_DetailCollection _details = new ProductPackaging_DetailCollection();

    [OneToMany(typeof(ProductPackaging_Detail), "ID")]
    [Join("ID", "ProductPackaging_ID")]
    public ProductPackaging_DetailCollection Details
    {
      get { return _details; }
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
  }
}
