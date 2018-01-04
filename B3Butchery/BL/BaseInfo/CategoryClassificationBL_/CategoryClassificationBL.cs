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

  [BusinessInterface(typeof(CategoryClassificationBL))]
  [LogicName("类别分类")]
  public interface ICategoryClassificationBL : IDomainBaseInfoBL<CategoryClassification>
  { }

  public class CategoryClassificationBL : DomainBaseInfoBL<CategoryClassification>, ICategoryClassificationBL
  {
  }
}
