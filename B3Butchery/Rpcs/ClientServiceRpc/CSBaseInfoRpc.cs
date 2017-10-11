using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
  [Rpc]
  public static class CSBaseInfoRpc
  {
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


//    [Rpc(RpcFlags.SkipAuth)]
//    public static List<Car> GetCar()
//    {
//      return GetBaseInfoList<Car>();
//    }


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

  }
}
