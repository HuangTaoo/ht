using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductPackagingBL))]
  [LogicName("成品包装")]
  public interface IProductPackagingBL : IDepartmentWorkFlowBillBL<ProductPackaging>
  {
  }

  public class ProductPackagingBL : DepartmentWorkFlowBillBL<ProductPackaging>, IProductPackagingBL
  {
  }
}
