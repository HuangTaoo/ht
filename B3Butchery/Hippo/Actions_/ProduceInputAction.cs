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
	public static class ProduceInputAction
	{
		[Rpc]
		public static ListData Query(ListData data)
		{
			var queryobj = (ProduceInputQueryObj)data.QueryObject;
			var query = new DQueryDom(new JoinAlias(typeof(ProduceInput)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("BillState"));
			query.Columns.Add(DQSelectColumn.Field("Time"));
			query.Columns.Add(DQSelectColumn.Field("PlanNumber_Name"));
			query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
			query.Columns.Add(DQSelectColumn.Field("Department_Name"));
			query.Columns.Add(DQSelectColumn.Field("Employee_Name"));
			query.Columns.Add(DQSelectColumn.Field("ProductLinks_Name"));
      query.Columns.Add(DQSelectColumn.Field("InputType"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", true));
			HippoUtil.AddEQ(query, "ID", queryobj.ID);
			HippoUtil.AddEQ(query, "BillState", queryobj.BillState);
			HippoUtil.AddEQ(query, "AccountingUnit_ID", queryobj.AccountingUnit_ID);
			HippoUtil.AddEQ(query, "Department_ID", queryobj.Department_ID);
			HippoUtil.AddEQ(query, "Employee_ID", queryobj.Employee_ID);
			HippoUtil.AddEQ(query, "PlanNumber_ID", queryobj.PlanNumber_ID);
			HippoUtil.AddEQ(query, "ProductLinks_ID", queryobj.ProductLinks_ID);
      HippoUtil.AddEQ(query, "InputType", queryobj.InputType);
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
			var productInput = (ProduceInput)data.MainObject;
			var bl = BIFactory.Create<IProduceInputBL>();

			switch (action)
			{
				case FormActionNames.Load:
					var dom = bl.Load(productInput.ID);
					data.MainObject = dom;
					break;
				case FormActionNames.Save:
					if (productInput.ID == 0)
					{
						bl.InitNewDmo(productInput);
						bl.Insert(productInput);
						data.MainObject = productInput;
						return FormActions(FormActionNames.Load, data);
					}
					bl.Update(productInput);
					return FormActions(FormActionNames.Load, data);
				case FormActionNames.New:
					var dmo = new ProduceInput();
					data.MainObject = dmo;
					break;
				case FormActionNames.Prev:
					var prevDmo = GetPrevOrNext(productInput.ID);
					if (prevDmo == null)
						throw new IndexOutOfRangeException("Current is first");
					data.MainObject = prevDmo;
					break;
				case FormActionNames.Next:
					var nextDmo = GetPrevOrNext(productInput.ID, false);
					if (nextDmo == null)
						throw new IndexOutOfRangeException("Current is last");
					data.MainObject = nextDmo;
					break;
				//case "LoadDetail":
				//	LoadDetail(productInput);
				//	break;
				default:
					throw new ArgumentException("Unknown action: " + action);
			}
			return data;
		}

		static ProduceInput GetPrevOrNext(long currentID, bool prev = true)
		{
			var query = new DmoQuery(typeof(ProduceInput));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
			if (prev)
				query.Where.Conditions.Add(DQCondition.LessThan("ID", currentID));
			else
				query.Where.Conditions.Add(DQCondition.GreaterThan("ID", currentID));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", prev));
			query.Range = SelectRange.Top(1);
			return query.EExecuteScalar<ProduceInput>();
		}
	}
}
