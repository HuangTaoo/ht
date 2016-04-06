using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
	[Rpc]
	public static class ProductLinkRpc
	{
		[Rpc]
		public static IList<EntityRowVersion> GetRowVersion(long? accountingUnit)
		{
			var query = new DQueryDom(new JoinAlias(typeof(ProductLinkTemplate)));
			query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountingUnit));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("RowVersion"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
			return query.EExecuteList<long, int>().Select(x => new EntityRowVersion(x.Item1, x.Item2)).ToList();
		}

		[Rpc]
		public static IList<ProductLinkTemplate> GetProductLinkTemplate(long[] id)
		{
			if (id.Length == 0)
				return new List<ProductLinkTemplate>();
			var query = new DmoQuery(typeof(ProductLinkTemplate));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ID"), id.Select(x => DQExpression.Value(x)).ToArray()));
			return query.EExecuteList().Cast<ProductLinkTemplate>().ToList();
		}

		[Rpc]
		public static void InsertProduceInput(ProduceInput dmo)
		{
			using (var context = new TransactionContext())
			{
				foreach (var d in dmo.Details)
				{
					d.Price = 0;
					d.Money = 0;
				}
				var bl = BIFactory.Create<IProduceInputBL>(context);
				dmo.Time = BLContext.Today;
				dmo.BillState = 单据状态.已审核;
				dmo.IsHandsetSend = true;
				bl.Insert(dmo);
				context.Commit();
			}
		}

		[Rpc]
		public static void InsertProduceOutput(ProduceOutput dmo)
		{
			using (var context = new TransactionContext())
			{
				foreach (var d in dmo.Details)
				{
					d.Price = 0;
					d.Money = 0;
				}
				var bl = BIFactory.Create<IProduceOutputBL>(context);
				dmo.Time = BLContext.Today;
				dmo.BillState = 单据状态.已审核;
				dmo.IsHandsetSend = true;
				bl.Insert(dmo);
				context.Commit();
			}
		}
	}
}
