using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Newtonsoft.Json;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
  [Rpc]
  public static class CSBaseInfoRpc
  {


        [Rpc(RpcFlags.SkipAuth)]
        public static string GetAllWorkShopCountConfigList()
        {
            var dmoquery = new DmoQuery(typeof(WorkShopCountConfig));
            dmoquery.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
            var list = dmoquery.EExecuteList().Cast<WorkShopCountConfig>().ToList();

            var jsonStr = JsonConvert.SerializeObject(list);
            //var jsonStr = serializer.Serialize(list);
            return jsonStr;
        }




        [Rpc(RpcFlags.SkipAuth)]
    public static List<WorkshopCategory> GetWorkshopCategory()
    {
      var query = new DQueryDom(new JoinAlias(typeof(WorkshopCategory)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Spell"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      var list = new List<WorkshopCategory>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new WorkshopCategory();
            entity.ID = (long)reader[0];
            entity.Name = (string)reader[1];
            entity.Spell = (string)reader[2];
            entity.Code= (string)reader[3];
            list.Add(entity);
          }
        }
      }
      return list;
    }

      [Rpc(RpcFlags.SkipAuth)]
      public static string GetPlanNoBaseInfo()
      {
          var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
          query.Columns.Add(DQSelectColumn.Field("PlanNumber"));
          query.Columns.Add(DQSelectColumn.Field("ID"));
          query.Where.Conditions.Add(DQCondition.LessThanOrEqual("Date", DateTime.Today));
          query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("EndDate", DateTime.Today));
          query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("BillState", 20));

          using (var context = new TransactionContext())
          {
              var list = new List<PlanNoBaseInfo>();
              ;

              using (var reader = context.Session.ExecuteReader(query))
              {
                  while (reader.Read())
                  {
                      var plan = new PlanNoBaseInfo();
                      plan.ID = (long)reader[1];
                      plan.Name = (string)reader[0];
                      list.Add(plan);
                  }

                  return JsonConvert.SerializeObject(list);
              }


          }

      }


    [Rpc(RpcFlags.SkipAuth)]
    public static string GetCalculateGoods()
    {
      var query = new DmoQuery(typeof(CalculateGoods));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      var list = query.EExecuteList().Cast<CalculateGoods>().ToList();
      return JsonConvert.SerializeObject(list);
    }


    [Rpc(RpcFlags.SkipAuth)]
    public static string GetCalculateSpec()
    {
      var query = new DmoQuery(typeof(CalculateSpec));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      var list = query.EExecuteList().Cast<CalculateSpec>().ToList();
      return JsonConvert.SerializeObject(list);
    }


    static List<T> GetBaseInfoList<T>() where T : BaseInfo, new()
    {
      var query = new DQueryDom(new JoinAlias(typeof(T)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Spell"));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      var list = new List<T>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new T();
            entity.ID = (long)reader[0];
            entity.Name = (string)reader[1];
            entity.Spell = (string)reader[2];
            list.Add(entity);
          }
        }
      }
      return list;
    }

    [Rpc(RpcFlags.SkipAuth)]
    public static List<EmpInfoTable> GetEmpInfo()
    {
      var user = new JoinAlias(typeof(WpfUser));
      var rel = new JoinAlias(typeof(User_Employee));
      var emp = new JoinAlias(typeof(Employee));
      var du = new JoinAlias(typeof(DomainUser));
      var setting = new JoinAlias(typeof(B3ButcheryUserProfile));
      var query = new DQueryDom(user);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(rel), DQCondition.EQ(user, "ID", rel, "User_ID"));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(emp), DQCondition.EQ(rel, "Employee_ID", emp, "ID"));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(du), DQCondition.And(DQCondition.EQ(rel, "User_ID", du, "User_ID"), DQCondition.EQ(du, "Domain_ID", emp, "Domain_ID")));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(setting), DQCondition.EQ(du, "ID", setting, "ID"));


      query.Columns.Add(DQSelectColumn.Field("ID", user));
      query.Columns.Add(DQSelectColumn.Field("Name", user));
      query.Columns.Add(DQSelectColumn.Field("Domain_ID", emp));
      query.Columns.Add(DQSelectColumn.Field("ID", emp));
      query.Columns.Add(DQSelectColumn.Field("Name", emp));
      query.Columns.Add(DQSelectColumn.Field("Department_ID", emp));
      query.Columns.Add(DQSelectColumn.Field("Department_Name", emp));
      query.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID", setting));
      query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", setting));
      query.Columns.Add(DQSelectColumn.Field("ProductionUnit_ID", setting));
      query.Columns.Add(DQSelectColumn.Field("ProductionUnit_Name", setting));
      query.Columns.Add(DQSelectColumn.Field("Remark", emp));
      query.Where.Conditions.Add(DQCondition.IsNotNull(DQExpression.Field(rel, "Employee_ID")));
      var list = new List<EmpInfoTable>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new EmpInfoTable();
            entity.User_ID = (long)reader[0];
            entity.User_Name = (string)reader[1];
            entity.Domain_ID = (long)reader[2];
            entity.Employee_ID = (long)reader[3];
            entity.Employee_Name = (string)reader[4];
            entity.Department_ID = (long?)reader[5];
            entity.Department_Name = (string)reader[6];
            entity.AccountingUnit_ID = (long?)reader[7];
            entity.AccountingUnit_Name = (string)reader[8];
            entity.ProductionUnit_ID = (long?)reader[9];
            entity.ProductionUnit_Name = (string)reader[10];
            entity.Role = (string)reader[11];
            list.Add(entity);
          }
        }
      }
      return list;
    }

    [Rpc(RpcFlags.SkipAuth)]
    public static List<WpfUser> GetWpfUser()
    {
      var query = new DQueryDom(new JoinAlias(typeof(WpfUser)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Stopped"));
      query.Columns.Add(DQSelectColumn.Field("Password"));
      query.Columns.Add(DQSelectColumn.Field("RoleSchema"));

      var list = new List<WpfUser>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new WpfUser();
            entity.ID = (long)reader[0];
            entity.Name = (string)reader[1];
            entity.Stopped = (bool)reader[2];
            entity.Password = (byte[])reader[3];
            entity.RoleSchema = (string)reader[4];
            list.Add(entity);
          }
        }
      }
      return list;
    }

    [Rpc(RpcFlags.SkipAuth)]
    public static List<User_Employee> GetUserEmployee()
    {
      var query = new DQueryDom(new JoinAlias(typeof(User_Employee)));
      query.Columns.Add(DQSelectColumn.Field("Employee_ID"));
      query.Columns.Add(DQSelectColumn.Field("User_ID"));

      var list = new List<User_Employee>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new User_Employee();
            entity.Employee_ID = (long)reader[0];
            entity.User_ID = (long)reader[1];
            list.Add(entity);
          }
        }
      }
      return list;
    }

    [Rpc(RpcFlags.SkipAuth)]
    public static List<Employee> GetEmployee()
    {
      var query = new DQueryDom(new JoinAlias(typeof(Employee)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      query.Columns.Add(DQSelectColumn.Field("Stopped"));
      query.Columns.Add(DQSelectColumn.Field("Domain_ID"));
      query.Columns.Add(DQSelectColumn.Field("Spell"));

      var list = new List<Employee>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var entity = new Employee();
            entity.ID = (long)reader[0];
            entity.Name = (string)reader[1];
            entity.Code = (string)reader[2];
            entity.Stopped = (bool)reader[3];
            entity.Domain_ID = (long)reader[4];
            entity.Spell = (string)reader[5];
            list.Add(entity);
          }
        }
      }
      return list;
    }

      [Rpc(RpcFlags.SkipAuth)]
      public static List<Store> GetStore()
      {
          var query = new DQueryDom(new JoinAlias(typeof(Store)));
          query.Columns.Add(DQSelectColumn.Field("ID"));
          query.Columns.Add(DQSelectColumn.Field("Name"));
          query.Columns.Add(DQSelectColumn.Field("Code"));
          query.Columns.Add(DQSelectColumn.Field("Stopped"));
          query.Columns.Add(DQSelectColumn.Field("Domain_ID"));
          query.Columns.Add(DQSelectColumn.Field("Spell"));

          var list = new List<Store>();
          using (var context = new TransactionContext())
          {
              using (var reader = context.Session.ExecuteReader(query))
              {
                  while (reader.Read())
                  {
                      var entity = new Store();
                      entity.ID = (long)reader[0];
                      entity.Name = (string)reader[1];
                      entity.Code = (string)reader[2];
                      entity.Stopped = (bool)reader[3];
                      entity.Domain_ID = (long)reader[4];
                      entity.Spell = (string)reader[5];
                      list.Add(entity);
                  }
              }
          }
          return list;
      }

  }
}
