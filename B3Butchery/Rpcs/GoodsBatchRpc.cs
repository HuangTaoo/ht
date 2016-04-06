using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs
{
    [Rpc]
    public static class GoodsBatchRpc
    {
        [Rpc]
        public static GoodsBatch Get(long id, string[] fields)
        {
            if (fields.Length == 0)
                throw new Exception("未指定fields");
            var query = new DQueryDom(new JoinAlias(typeof(GoodsBatch)));
            foreach (var field in fields)
                query.Columns.Add(DQSelectColumn.Field(field));
            query.Where.Conditions.Add(DQCondition.EQ("ID", id));
            var dmoType = typeof(GoodsBatch);
            var i = 0;
            using (var context = new TransactionContext())
            {
                using (var reader = context.Session.ExecuteReader(query))
                {
                    if (reader.Read())
                    {
                        var student = new GoodsBatch { ID = id };
                        foreach (var field in fields)
                        {
                            dmoType.GetProperty(field).SetValue(student, reader[i++], null);
                        }
                        return student;
                    }
                    return null;
                }
            }
        }
    }
}
