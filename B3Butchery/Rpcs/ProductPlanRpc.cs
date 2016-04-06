using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
	[Rpc]
	public static class ProductPlanRpc
	{
		[Rpc]
		public static List<ProductPlanInfo> GetProductPlanInfo(long[] id)
		{
			if (id.Length == 0)
				return new List<ProductPlanInfo>();
			var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("PlanNumber"));
			query.Columns.Add(DQSelectColumn.Field("Date"));
			query.Columns.Add(DQSelectColumn.Field("RowVersion"));
			query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ID"), id.Select(x => DQExpression.Value(x)).ToArray()));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			var result = query.EExecuteList<long, string, DateTime, int>();
			var rst = new List<ProductPlanInfo>();
			foreach (var item in result)
				rst.Add(new ProductPlanInfo { ID = item.Item1, PlanNumber = item.Item2, PlanDate = item.Item3, RowVersion = item.Item4 });
			return rst;
		}

		[Rpc]
		public static IList<EntityRowVersion> GetRowVersion(long? accountingUnit)
		{
			var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
			query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountingUnit));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("RowVersion"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.And(DQCondition.Between("Date", DateTime.Today.AddDays(-3), DateTime.Today.AddDays(1)), DQCondition.EQ("BillState", 单据状态.已审核)));
			return query.EExecuteList<long, int>().Select(x => new EntityRowVersion(x.Item1, x.Item2)).ToList();
		}
	}

	[RpcObject]
	public class ProductPlanInfo
	{
		public long ID { get; set; }

		public string PlanNumber { get; set; }

		public DateTime PlanDate { get; set; }

		public int RowVersion { get; set; }
	}
}
