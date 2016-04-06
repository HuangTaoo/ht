using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BL;
using BWP.B3Butchery.BO;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.B3Frameworks.Utils;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductInStore_TempBL))]
  [LogicName("模板")]
  public interface IProductInStore_TempBL : IDomainBaseInfoBL<ProductInStore_Temp>
  { }

  public class ProductInStore_TempBL : DomainBaseInfoBL<ProductInStore_Temp>, IProductInStore_TempBL
  {
    protected override void beforeSave(ProductInStore_Temp dmo)
    {
      base.beforeSave(dmo);
    }
  }

}
