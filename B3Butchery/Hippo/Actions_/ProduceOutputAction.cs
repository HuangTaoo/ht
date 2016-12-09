using System;
using System.Linq;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
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
using Forks.Utils;
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
      query.Columns.Add(DQSelectColumn.Field("ProductLinkTemplate_Name"));
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
          if (productOutput.Details.Count <= 0)
            LoadDetail(productOutput);
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
          dmo.Time = DateTime.Now;
          bl.InitNewDmo(dmo);
          var productPlan = GetProductPlan(dmo.AccountingUnit_ID, dmo.Department_ID, Convert.ToDateTime(Convert.ToDateTime(dmo.Time).ToShortDateString()));
          if (productPlan != null)
          {
            dmo.PlanNumber_ID = productPlan.Item1;
            dmo.PlanNumber_Name = productPlan.Item2;
          }
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
          bl.Update(productOutput);
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
      var bill = new JoinAlias(typeof(ProductLinkTemplate));
      var productLinkDetail = new JoinAlias(typeof(ProductLinkTemplate_Detail));
      var query = new DQueryDom(productLinkDetail);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(bill), DQCondition.EQ(bill, "ID", productLinkDetail, "ProductLinkTemplate_ID"));
      query.Columns.Add(DQSelectColumn.Field("Goods_ID", productLinkDetail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", productLinkDetail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Code", productLinkDetail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Spec", productLinkDetail));
      query.Columns.Add(DQSelectColumn.Field("Goods_MainUnitRatio", productLinkDetail));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnitRatio", productLinkDetail));
      query.Where.Conditions.Add(DQCondition.EQ(productLinkDetail, "ProductLinkTemplate_ID", dmo.ProductLinkTemplate_ID));
      query.Where.Conditions.Add(DQCondition.EQ(bill, "AccountingUnit_ID", dmo.AccountingUnit_ID));
      query.Where.Conditions.Add(DQCondition.EQ(bill, "Department_ID", dmo.Department_ID));
      dmo.Details.Clear();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var detail = new ProduceOutput_Detail
            {
              Goods_ID = (long)reader[0],
              Goods_Name = (string)reader[1],
              Goods_Code = (string)reader[2],
              Goods_Spec = (string)reader[3],
              Goods_MainUnitRatio = (Money<decimal>?)reader[4],
              Goods_SecondUnitRatio = (Money<decimal>?)reader[5]
            };
            dmo.Details.Add(detail);
          }
        }
      }
    }

    static Tuple<long?, string> GetProductPlan(long? nullable1, long? nullable2, DateTime? nullable3)
    {
      var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("PlanNumber"));
      query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("AccountingUnit_ID", nullable1), DQCondition.EQ("Date", nullable3), DQCondition.EQ("BillState", 单据状态.已审核)));
      query.Where.Conditions.Add(B3ButcheryUtil.部门或上级部门条件(nullable2 ?? 0));
      return query.EExecuteScalar<long?, string>();
    }
  }
}
