using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BL;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.Utils;
using BWP.Web.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class BaseInfoRpc
  {
    [Rpc]
    public static string GetAccountUnitNameById(long id=0)
    {
      return WebBLUtil.GetDmoPropertyByID<string>(typeof(AccountingUnit), "Name", id);
    }

    [Rpc]
    public static DepartmentDto GetDepartmentBaseInfoDto()
    {
      var dto = new DepartmentDto();
      using (var session=Dmo.NewSession())
      {
        var employeeId = GetCurrentBindingEmployeeID(session);
        var query=new DQueryDom(new JoinAlias(typeof(Employee)));
        query.Where.Conditions.Add(DQCondition.EQ("ID", employeeId));
        query.Columns.Add(DQSelectColumn.Field("Department_ID"));
        query.Columns.Add(DQSelectColumn.Field("Department_Name"));
        query.Columns.Add(DQSelectColumn.Field("Department_Depth"));
        using (var reader=session.ExecuteReader(query))
        {
          if (reader.Read())
          {
            dto.ID = (long?)reader[0]??0;
            if (dto.ID > 0)
            {
              dto.Name = (string)reader[1];
              dto.Department_Depth = (int?) reader[2];
            }
          }
        }
      }
      return dto;
    }

    private static long? GetCurrentBindingEmployeeID(IDmoSession session)
    {
      if (BLContext.User.RoleSchema != B3FrameworksConsts.RoleSchemas.employee)
      {
        throw new Exception("当前用户不是员工类型");
      }

      var query = new DQueryDom(new JoinAlias(typeof(User_Employee)));
      query.Where.Conditions.Add(DQCondition.EQ("User_ID", BLContext.User.ID));
      query.Columns.Add(DQSelectColumn.Field("Employee_ID"));

      var result = (long?)query.EExecuteScalar(session);
      return result;
    }

  }
}
