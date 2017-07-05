using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
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
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class BaseInfoRpc
  {

    private static List<BaseInfoDto>  GetBaseInfoDQueryDom(Type type,bool hasCode, BaseInfoQueryDto queryDto)
    {
      var list = new List<BaseInfoDto>();
      var query = new DQueryDom(new JoinAlias(type));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      if (hasCode)
      {
        query.Columns.Add(DQSelectColumn.Field("Code"));
      }
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
      if (queryDto.PageSize > 0)
      {
        query.Range=new SelectRange(queryDto.PageIndex*queryDto.PageSize, queryDto.PageSize);
      }
      if (!string.IsNullOrWhiteSpace(queryDto.Input))
      {
        if (hasCode)
        {
          query.Where.Conditions.Add(DQCondition.Or(DQCondition.Like("Name", queryDto.Input), DQCondition.Like("Code", queryDto.Input)) );
        }
        else
        {
          query.Where.Conditions.Add(DQCondition.Like("Name", queryDto.Input));
        }
      }
      using (var session = Dmo.NewSession())
      {
        using (var reader = session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            list.Add(new BaseInfoDto() { ID = (long)reader[0], Name = (string)reader[1]});
          }
        }
      }
      return list;
    }

    [Rpc]
    public static List<BaseInfoDto> SyncStoresWithQuery(string input,int pageIndex,int pageSize)
    {
      var queryDto=new BaseInfoQueryDto();
      queryDto.Input = input;
      queryDto.PageIndex = pageIndex;
      queryDto.PageSize = pageSize;
      return GetBaseInfoDQueryDom(typeof(Store),true,queryDto);
    }

    [Rpc]
    public static List<FrozenStore> SyncFrozenStore()
    {
      var list = new List<FrozenStore>();
      var query = new DQueryDom(new JoinAlias(typeof(FrozenStore)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
      using (var session = Dmo.NewSession())
      {
        using (var reader = session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            list.Add(new FrozenStore() { ID = (long)reader[0], Name = (string)reader[1], Code = (string)reader[2] });
          }
        }
      }
      return list;
    }

    [Rpc]
    public static List<Store> SyncStores()
    {
      var list=new List<Store>();
      var query=new DQueryDom(new JoinAlias(typeof(Store)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped",false));
      query.Where.Conditions.Add(DQCondition.EQ("Domain_ID",DomainContext.Current.ID));
      using (var session=Dmo.NewSession())
      {
        using (var reader=session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            list.Add(new Store(){ID = (long)reader[0],Name=(string)reader[1],Code = (string)reader[2]});
          }
        }
      }
      return list;
    }

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
              dto.Department_Depth = (int?) reader[2]??0;
            }
          }
        }
      }
      return dto;
    }

    [Rpc]
    public static BaseInfoDto GetUserProfileAccountUnit()
    {
      var profile = DomainUserProfileUtil.Load<B3ButcheryUserProfile>();
      if (profile.AccountingUnit_ID == null)
      {
        throw new Exception("板块个性设置没有设置会计单位");
      }
      var dto = new BaseInfoDto();
      dto.ID= profile.AccountingUnit_ID.Value;
      dto.Name = profile.AccountingUnit_Name;
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
