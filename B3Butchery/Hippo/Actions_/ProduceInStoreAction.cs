using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bwp.Hippo;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Hippo.QueryObjs;
using BWP.B3Frameworks;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Hippo.Actions_
{
	[Rpc]
	public static class ProductInStoreAction
	{
		[Rpc]
		public static ListData Query(ListData data)
		{
			var queryobj = (ProductInStoreQueryObj)data.QueryObject;
			var query = new DQueryDom(new JoinAlias(typeof(ProductInStore)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("BillState"));
			query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
			query.Columns.Add(DQSelectColumn.Field("Department_Name"));
			query.Columns.Add(DQSelectColumn.Field("Employee_Name"));
			query.Columns.Add(DQSelectColumn.Field("Store_Name"));
			query.Columns.Add(DQSelectColumn.Field("InStoreType_Name"));
			query.Columns.Add(DQSelectColumn.Field("InStoreDate"));
			query.Columns.Add(DQSelectColumn.Field("CheckEmployee_Name"));
			query.Columns.Add(DQSelectColumn.Field("CheckDate"));
      query.Columns.Add(DQSelectColumn.Field("ProductInStoreTemplate_Name"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", true));
			HippoUtil.AddEQ(query, "ID", queryobj.ID);
			HippoUtil.AddEQ(query, "BillState", queryobj.BillState);
			HippoUtil.AddEQ(query, "AccountingUnit_ID", queryobj.AccountingUnit_ID);
			HippoUtil.AddEQ(query, "Department_ID", queryobj.Department_ID);
			HippoUtil.AddEQ(query, "Employee_ID", queryobj.Employee_ID);
			HippoUtil.AddEQ(query, "Store_ID", queryobj.Store_ID);
			HippoUtil.AddEQ(query, "InStoreType_ID", queryobj.InStoreType_ID);
			if (queryobj.MinInStoreDate.HasValue)
				query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("InStoreDate", queryobj.MinInStoreDate.Value));
			if (queryobj.MaxInStoreDate.HasValue)
				query.Where.Conditions.Add(DQCondition.LessThanOrEqual("InStoreDate", queryobj.MaxInStoreDate.Value));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
			query.Range = new SelectRange(data.Start, data.Count);
			var pagedData = new DFDataAdapter(new LoadArguments(query)).PagedFill();
			data.Start = 0;
			data.Count = (int)pagedData.TotalCount;
			data.Data = pagedData.Data;
			return data;
		}

		[Rpc]
		public static FormData FormActions(string action, FormData data)
		{
			var productInStore = (ProductInStore)data.MainObject;
			var bl = BIFactory.Create<IProductInStoreBL>();

			switch (action)
			{
				case FormActionNames.Load:
					var dom = bl.Load(productInStore.ID);
					data.MainObject = dom;
					break;
				case FormActionNames.Save:
					if (productInStore.ID == 0)
					{
						bl.InitNewDmo(productInStore);
						bl.Insert(productInStore);
						data.MainObject = productInStore;
						return FormActions(FormActionNames.Load, data);
					}
					bl.Update(productInStore);
					return FormActions(FormActionNames.Load, data);
				case FormActionNames.New:
					var dmo = new ProductInStore();
					data.MainObject = dmo;
          bl.InitNewDmo(dmo);
					break;
				case FormActionNames.Prev:
					var prevDmo = GetPrevOrNext(productInStore.ID);
					if (prevDmo == null)
						throw new IndexOutOfRangeException("Current is first");
					data.MainObject = prevDmo;
					break;
				case FormActionNames.Next:
					var nextDmo = GetPrevOrNext(productInStore.ID, false);
					if (nextDmo == null)
						throw new IndexOutOfRangeException("Current is last");
					data.MainObject = nextDmo;
					break;
				case "LoadDetail":
					LoadDetail(productInStore);
					break;
				case "ReferToCreate":
					data.MainObject = HippoUtil.ReferenceToCreate<ProductInStore>(productInStore);
					break;
				default:
					throw new ArgumentException("Unknown action: " + action);
			}
			return data;
		}

		static ProductInStore GetPrevOrNext(long currentID, bool prev = true)
		{
			var query = new DmoQuery(typeof(ProductInStore));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
			if (prev)
				query.Where.Conditions.Add(DQCondition.LessThan("ID", currentID));
			else
				query.Where.Conditions.Add(DQCondition.GreaterThan("ID", currentID));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", prev));
			query.Range = SelectRange.Top(1);
			return query.EExecuteScalar<ProductInStore>();
		}

		static void LoadDetail(ProductInStore dmo)
		{
			if (dmo.ProductInStoreTemplate_ID == null)
				throw new ArgumentException("请先选择成品入库模板");
			dmo.Details.Clear();
			var query = new DQueryDom(new JoinAlias(typeof(ProductInStoreTemplate_GoodsDetail)));
			query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
			query.Where.Conditions.Add(DQCondition.EQ("ProductInStoreTemplate_ID", dmo.ProductInStoreTemplate_ID));
			query.EExecuteList<long, string, string>().Select(x => new ProductInStore_Detail { ProductInStore_ID = dmo.ID, Goods_ID = x.Item1, Goods_Name = x.Item2, Goods_Code = x.Item3 }).EAddToCollection(dmo.Details);
		}
	}
}
