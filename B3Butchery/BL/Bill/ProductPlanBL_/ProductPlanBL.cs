using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using BWP.B3Frameworks.BL;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebPluginFramework;
using Forks.EnterpriseServices;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductPlanBL))]
	[LogicName("生产计划")]
  public interface IProductPlanBL : IDepartmentWorkFlowBillBL<ProductPlan>
  { }

  public class ProductPlanBL : DepartmentWorkFlowBillBL<ProductPlan>, IProductPlanBL
  {
    protected override void beforeSave(ProductPlan dmo)
    {
      base.beforeSave(dmo);

      if (dmo.ID == 0)
      {
        GetPlanNumber(dmo);
      }
    }

    public static void GetPlanNumber(ProductPlan dmo)
    {
      var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
      query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("PlanNumber", dmo.PlanNumber),
           DQCondition.EQ("PlanNumbers", true)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      var result = query.EExecuteList<long>();
      if (result.Count != 0)
      {
        throw new Exception("此计划号已经完毕，不能重复做");
      }
    }
  }
}
