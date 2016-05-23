using System;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Attributes;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable]
  [LogicName("成品包装配置")]
  [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProductPackaging)]
  [EditUrl("~/B3Butchery/Bills/ProductPackaging_/ProductPackagingEdit.aspx")]
  public class ProductPackaging : DepartmentWorkFlowBill
  {
    readonly ProductPackaging_DetailCollection _details = new ProductPackaging_DetailCollection();

    [OneToMany(typeof(ProductPackaging_Detail), "ID")]
    [Join("ID", "ProductPackaging_ID")]
    public ProductPackaging_DetailCollection Details
    {
      get { return _details; }
    }
  }
}
