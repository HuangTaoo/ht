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
  [BusinessInterface(typeof(WorkshopCategoryBL))]
  [LogicName("车间品类")]
  public interface IWorkshopCategoryBL : IDomainBaseInfoBL<WorkshopCategory>
  { }

  public class WorkshopCategoryBL : DomainBaseInfoBL<WorkshopCategory>, IWorkshopCategoryBL
  {
  }


}
