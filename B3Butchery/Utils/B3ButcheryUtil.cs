using System.Collections.Generic;
using System.Linq;
using BWP.B3Frameworks.BO;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;

namespace BWP.B3Butchery.Utils
{
  public static class B3ButcheryUtil
  {
    public static IDQExpression 部门或上级部门条件(long deptID, JoinAlias alias = null)
    {
      var department = WebBLUtil.GetSingleDmo<Department>("ID", deptID);
      if (alias != null)
        return DQCondition.Or(from i in Range(1, department.Depth)
                              select DQCondition.EQ(alias, "Department_ID", department.NodePath[i]));
      return DQCondition.Or(from i in Range(1, department.Depth)
                            select DQCondition.EQ("Department_ID", department.NodePath[i]));
    }

    static IEnumerable<int> Range(int min, int max)
    {
      for (int i = min - 1; i < max; i++)
      {
        yield return i;
      }
    }
  }
}
