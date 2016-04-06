using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
	[Rpc]
	public static class ProductInStoreRpc
	{
		[Rpc]
		public static IList<EntityRowVersion> GetRowVersion(long? accountingUnit)
		{
			var query = new DQueryDom(new JoinAlias(typeof(ProductInStoreTemplate)));
			query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountingUnit));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("RowVersion"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
			return query.EExecuteList<long, int>().Select(x => new EntityRowVersion(x.Item1, x.Item2)).ToList();
		}

		[Rpc]
		public static IList<ProductInStoreTemplate> GetProductInStoreTemplate(long[] id)
		{
			if (id.Length == 0)
				return new List<ProductInStoreTemplate>();
			var query = new DmoQuery(typeof(ProductInStoreTemplate));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ID"), id.Select(x => DQExpression.Value(x)).ToArray()));
			return query.EExecuteList().Cast<ProductInStoreTemplate>().ToList();
		}

		[Rpc]
		public static DFDataTable SelectInStoreType(long domainID)
		{
			var query = new DQueryDom(new JoinAlias(typeof(InStoreType)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("Name"));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", domainID));
			return new DFDataAdapter(new LoadArguments(query)).Fill();
		}

		[Rpc]
		public static void InsertProductInStore(ProductInStore dmo)
		{
            var query = new DQueryDom(new JoinAlias(typeof(ProductInStore)));
            query.Columns.Add(DQSelectColumn.Field("DeviceId"));
            query.Where.Conditions.Add(DQCondition.EQ("InStoreDate", dmo.InStoreDate));
            var deviceIds= query.EExecuteList<string>();
		    if (deviceIds.Contains(dmo.DeviceId))
                return;
			using (var context = new TransactionContext())
			{
				foreach (var d in dmo.Details)
				{
					d.Price = 0;
					d.Money = 0;
					d.ProductionDate = GetProductPlanDate(d.ProductPlan_ID);
				}
				var bl = BIFactory.Create<IProductInStoreBL>(context);
				dmo.InStoreDate = BLContext.Today;
				//dmo.BillState = 单据状态.已审核;
				dmo.IsHandsetSend = true;
				bl.Insert(dmo);
				if (new B3ButcheryConfig().DoCheckCreatedInStore.Value)
					bl.Check(dmo);
				context.Commit();
			}
		}

		static DateTime? GetProductPlanDate(long? productPlanID)
		{
			if (productPlanID == null)
				return null;
			var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
			query.Columns.Add(DQSelectColumn.Field("Date"));
			query.Where.Conditions.Add(DQCondition.EQ("ID", productPlanID));
			return query.EExecuteScalar<DateTime?>();
		}
	}

	[RpcObject]
	public class EntityRowVersion
	{
		public long ID { private set; get; }

		public int RowVersion { private set; get; }

		public EntityRowVersion()
		{ }

		public EntityRowVersion(long id, int rowVersion)
		{
			ID = id;
			RowVersion = rowVersion;
		}
	}
}
