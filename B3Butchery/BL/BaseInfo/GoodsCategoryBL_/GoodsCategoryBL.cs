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
 
  [BusinessInterface(typeof(GoodsCategoryBL))]
  [LogicName("存货类别")]
  public interface IGoodsCategoryBL : IDomainBaseInfoBL<GoodsCategory>
  { }

  public class GoodsCategoryBL : DomainBaseInfoBL<GoodsCategory>, IGoodsCategoryBL
  {
  }
}
