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
	public static class ProduceOutputAction
	{
		[Rpc]
		public static ListData Query(ListData data)
		{
			var queryobj = (ProduceOutputQueryObj)data.QueryObject;
			var query = new DQueryDom(new JoinAlias(typeof(ProduceOutput)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("BillState"));
			query.Columns.Add(DQSelectColumn.Field("Time"));
			query.Columns.Add(DQSelectColumn.Field("PlanNumber_Name"));
			query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
			query.Columns.Add(DQSelectColumn.Field("Department_Name"));
			query.Columns.Add(DQSelectColumn.Field("Employee_Name"));
			query.Columns.Add(DQSelectColumn.Field("ProductLinks_Name"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", true));
			HippoUtil.AddEQ(query, "ID", queryobj.ID);
			HippoUtil.AddEQ(query, "BillState", queryobj.BillState);
			HippoUtil.AddEQ(query, "AccountingUnit_ID", queryobj.AccountingUnit_ID);
			HippoUtil.AddEQ(query, "Department_ID", queryobj.Department_ID);
			HippoUtil.AddEQ(query, "Employee_ID", queryobj.Employee_ID);
			HippoUtil.AddEQ(query, "PlanNumber_ID", queryobj.PlanNumber_ID);
			HippoUtil.AddEQ(query, "ProductLinks_ID", queryobj.ProductLinks_ID);
			if (queryobj.MinTime.HasValue)
				query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("Time", queryobj.MinTime.Value));
			if (queryobj.MaxTime.HasValue)
				query.Where.Conditions.Add(DQCondition.LessThanOrEqual("Time", queryobj.MaxTime.Value));
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
			var productOutput = (ProduceOutput)data.MainObject;
			var bl = BIFactory.Create<IProduceOutputBL>();

			switch (action)
			{
				case FormActionNames.Load:
					var dom = bl.Load(productOutput.ID);
					data.MainObject = dom;
					break;
				case FormActionNames.Save:
					if (productOutput.ID == 0)
					{
						bl.InitNewDmo(productOutput);
						bl.Insert(productOutput);
						data.MainObject = productOutput;
						return FormActions(FormActionNames.Load, data);
					}
					bl.Update(productOutput);
					return FormActions(FormActionNames.Load, data);
				case FormActionNames.New:
					var dmo = new ProduceOutput();
					data.MainObject = dmo;
					break;
				case FormActionNames.Prev:
					var prevDmo = GetPrevOrNext(productOutput.ID);
					if (prevDmo == null)
						throw new IndexOutOfRangeException("Current is first");
					data.MainObject = prevDmo;
					break;
				case FormActionNames.Next:
					var nextDmo = GetPrevOrNext(productOutput.ID, false);
					if (nextDmo == null)
						throw new IndexOutOfRangeException("Current is last");
					data.MainObject = nextDmo;
					break;
				case "LoadDetail":
					LoadDetail(productOutput);
					break;
				case "ReferToCreate":
					data.MainObject = HippoUtil.ReferenceToCreate<ProduceOutput>(productOutput);
					break;
				default:
					throw new ArgumentException("Unknown action: " + action);
			}
			return data;
		}

		static ProduceOutput GetPrevOrNext(long currentID, bool prev = true)
		{
			var query = new DmoQuery(typeof(ProduceOutput));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
			if (prev)
				query.Where.Conditions.Add(DQCondition.LessThan("ID", currentID));
			else
				query.Where.Conditions.Add(DQCondition.GreaterThan("ID", currentID));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", prev));
			query.Range = SelectRange.Top(1);
			return query.EExecuteScalar<ProduceOutput>();
		}

		static void LoadDetail(ProduceOutput dmo)
		{
			if (dmo.ProductLinkTemplate_ID == null)
				throw new ArgumentException("请先选择生产环节模板");
			var query = new DQueryDom(new JoinAlias(typeof(ProductLinkTemplate_Detail)));
			query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
			query.Where.Conditions.Add(DQCondition.EQ("ProductLinkTemplate_ID", dmo.ProductLinkTemplate_ID));
			var list = query.EExecuteList<long, string, string>().Select(x => new ProduceOutput_Detail { Goods_ID = x.Item1, Goods_Name = x.Item2, Goods_Code = x.Item3 });
			var removeDetail = new List<ProduceOutput_Detail>();
			foreach (var detail in dmo.Details)
			{
				if (!list.Any(x => x.Goods_ID == detail.Goods_ID))
					removeDetail.Add(detail);
			}
			foreach (var remove in removeDetail)
				dmo.Details.Remove(remove);
			foreach (var add in list)
			{
				if (!dmo.Details.Any(x => x.Goods_ID == add.Goods_ID))
					dmo.Details.Add(add);
			}
		}
	}
}
