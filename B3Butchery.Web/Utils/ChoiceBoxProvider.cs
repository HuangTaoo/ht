using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TSingSoft.WebControls2;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks;
using TSingSoft.WebPluginFramework;
using BWP.B3UnitedInfos.BO;

namespace BWP.B3Butchery.Web
{
	public static class ChoiceBoxProvider
	{
		internal static void Register()
		{
			ChoiceBoxSettings.Register(B3ButcheryDataSource.生产线, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<ProductLine>(argu)
				{
					OnlyAvailable = true
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.生产线全部, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<ProductLine>(argu)
				{
					OnlyAvailable = false
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.计划号, argu =>
			{
				var main = new JoinAlias(typeof(ProductPlan));
				var query = new DQueryDom(main);
				query.Range = SelectRange.Top(60);
				query.Columns.Add(DQSelectColumn.Field("PlanNumber"));
				query.Columns.Add(DQSelectColumn.Field("ID"));
				query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("BillState", 单据状态.已审核),
					DQCondition.EQ(main, "Domain_ID", DomainContext.Current.ID),
					DQCondition.EQ("PlanNumbers", false)));
				if (!string.IsNullOrEmpty(argu.InputArgument))
					query.Where.Conditions.Add(DQCondition.Like("PlanNumber", argu.InputArgument));
				if (!string.IsNullOrEmpty(argu.CodeArgument))
					query.Where.Conditions.Add(DQCondition.EQ("Date", DateTime.Parse(argu.CodeArgument)));
        query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", true));
        return query.EExecuteList<string, long>().Select(x => new WordPair(x.Item1, x.Item2.ToString()));
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.生产环节, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<ProductLinks>(argu)
				{
					OnlyAvailable = true
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.生产环节全部, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<ProductLinks>(argu)
				{
					OnlyAvailable = false
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.屠宰分割入库类型, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<InStoreType>(argu)
				{
					OnlyAvailable = true
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.屠宰分割入库类型全部, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<InStoreType>(argu)
				{
					OnlyAvailable = false
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.存货带编号, (argu) =>
			{
				return SelectGoodsWithSpec(argu, true);
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.存货带编号全部, (argu) =>
			{
				return SelectGoodsWithSpec(argu, false);
			});

      ChoiceBoxSettings.Register(B3ButcheryDataSource.存货品牌, (argu) =>
			{
				return SelectGoodsBrand(argu);
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.成品入库模板, (argu) =>
			{
				return new DomainChoiceBoxQueryHelper<ProductInStoreTemplate>(argu)
				{
					OnlyAvailable = true
				}.GetData();
			});

			ChoiceBoxSettings.Register(B3ButcheryDataSource.生产环节模板, SelectProductLinkTemplate);
    }

    private static IEnumerable<WordPair> SelectProductLinkTemplate(ChoiceBoxArgument argu)
    {
      var query = new DQueryDom(new JoinAlias(typeof (ProductLinkTemplate)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      if (!string.IsNullOrEmpty(argu.CodeArgument))
      {
        var accDep = argu.CodeArgument.Split(new[] {'|'}).ToArray();
        query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("AccountingUnit_ID",accDep[0]),DQCondition.EQ("Department_ID",accDep[1])));
      }
      if (!string.IsNullOrEmpty(argu.InputArgument))
      {
        IList<IDQExpression> conditions = new List<IDQExpression>();
        conditions.Add(DQCondition.Like("Name", argu.InputArgument));
        conditions.Add(DQCondition.Like("Spell", argu.InputArgument));
        query.Where.Conditions.Add(DQCondition.Or(conditions));
      }
      return query.EExecuteList<long, string>().Select(x => new WordPair(x.Item2, x.Item1.ToString(CultureInfo.InvariantCulture)));
    }

	  private static IEnumerable<WordPair> SelectGoodsWithSpec(ChoiceBoxArgument argu, bool OnlyAvailable)
		{
			var query = new DQueryDom(new JoinAlias(typeof(Goods)));
			query.Range = SelectRange.Top(30);
			query.Columns.Add(DQSelectColumn.Field("Name"));
			query.Columns.Add(DQSelectColumn.Field("Code"));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			if (OnlyAvailable)
				query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
			if (!string.IsNullOrEmpty(argu.InputArgument))
			{
				IList<IDQExpression> conditions = new List<IDQExpression>();
				conditions.Add(DQCondition.Like("Name", argu.InputArgument));
				conditions.Add(DQCondition.Like("Spell", argu.InputArgument));
				conditions.Add(DQCondition.Like("Code", argu.InputArgument));
				query.Where.Conditions.Add(DQCondition.Or(conditions));
			}
			return query.EExecuteList<string, string, long>().Select((l)
				=> new WordPair(string.Concat(l.Item1, string.Concat("(", l.Item2, ")")), l.Item3.ToString()));
		}

    private static IEnumerable<WordPair> SelectGoodsBrand(ChoiceBoxArgument argu)
    {
      var query = new DQueryDom(new JoinAlias(typeof(Goods)));
      query.Range = SelectRange.Top(30);
      query.Columns.Add(DQSelectColumn.Field("Brand"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Brand"));
      if (!string.IsNullOrEmpty(argu.InputArgument))
      {
        query.Where.Conditions.Add(DQCondition.Like("Brand", argu.InputArgument));
      }
      return query.EExecuteList<string>().Select((l)
        => new WordPair(l, l));
    }
  }
}
