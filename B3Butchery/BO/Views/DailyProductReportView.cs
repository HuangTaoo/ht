using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils.Data;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO.Views
{
	[DFClass, Serializable, MapToView]
	[LogicName("生产日报视图")]
	public class DailyProductReportView : ICreateView
	{
		public ISqlDom CreateView()
		{
			var main = new JoinAlias(typeof(DailyProductReport));
			var inputdetail = new JoinAlias(typeof(DailyProductReport_InputDetail));
			var dom = new DQueryDom(main);
			dom.From.AddJoin(JoinType.Inner, new DQDmoSource(inputdetail), DQCondition.EQ(main, "ID", inputdetail, "DailyProductReport_ID"));

			dom.Columns.Add(DQSelectColumn.Field("Domain_ID"));
			dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Department_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Date"));
			dom.Columns.Add(DQSelectColumn.Field("PlanNumber_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Goods_ID", inputdetail));
			dom.Columns.Add(DQSelectColumn.Field("Number", inputdetail));
			dom.Columns.Add(DQSelectColumn.Field("Weight", inputdetail));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Value<decimal?>(null), "OutputPrecent"));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Value<decimal>(0), "Price"));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Value<bool>(false), "DetailType"));
			dom.Where.Conditions.Add(DQCondition.EQ("BillState", 单据状态.已审核));
			SelectDom selectDom = (SelectDom)dom.Build(DbProviderType.SqlClient, DbmsType.SqlServer2005);

			UnionDom unionDom = new UnionDom();
			UnionItem inputDetailUnion = new UnionItem(selectDom, UnionType.Default);
			UnionItem outputDetailUnion = new UnionItem(GetOutputDetailDom(), UnionType.All);

			unionDom.Items.Add(inputDetailUnion);
			unionDom.Items.Add(outputDetailUnion);
			return unionDom;
		}

		private SelectDom GetOutputDetailDom()
		{
			var main = new JoinAlias(typeof(DailyProductReport));
			var outputdetail = new JoinAlias(typeof(DailyProductReport_OutputDetail));
			var dom = new DQueryDom(main);
			dom.From.AddJoin(JoinType.Inner, new DQDmoSource(outputdetail), DQCondition.EQ(main, "ID", outputdetail, "DailyProductReport_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Domain_ID"));
			dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Department_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Date"));
			dom.Columns.Add(DQSelectColumn.Field("PlanNumber_ID"));
			dom.Columns.Add(DQSelectColumn.Field("Goods_ID", outputdetail));
			dom.Columns.Add(DQSelectColumn.Field("Number", outputdetail));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Value<decimal?>(null), "Weight"));
			dom.Columns.Add(DQSelectColumn.Field("OutputPrecent", outputdetail));
			dom.Columns.Add(DQSelectColumn.Field("Price", outputdetail));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Value<bool>(true), "DetailType"));
			dom.Where.Conditions.Add(DQCondition.EQ("BillState", 单据状态.已审核));
			SelectDom selectDom = (SelectDom)dom.Build(DbProviderType.SqlClient, DbmsType.SqlServer2005);

			return selectDom;
		}

		#region fileds
		[LogicName("版块")]
		public long Domain_ID { get; set; }

		[LogicName("会计单位")]
		public long AccountingUnit_ID { get; set; }

		[LogicName("部门")]
		public long Department_ID { get; set; }

		[LogicName("日期")]
		public DateTime? Date { get; set; }

		[LogicName("计划号")]
		public long? PlanNumber_ID { get; set; }

		[LogicName("存货")]
		public long? Goods_ID { get; set; }

		[LogicName("数量")]
		public decimal? Number { get; set; }

		[LogicName("重量")]
		public decimal? Weight { get; set; }

		[LogicName("出成率")]
		public decimal? OutputPrecent { get; set; }

		[LogicName("单价")]
		public decimal? Price { get; set; }

		[LogicName("明细类型")]
		public bool DetailType { get; set; }
		#endregion

		#region referenceFields
		[ReferenceTo(typeof(AccountingUnit), "Name")]
		[Join("AccountingUnit_ID", "ID")]
		[LogicName("会计单位")]
		public string AccountingUnit_Name { get; set; }

		[ReferenceTo(typeof(Department), "Name")]
		[Join("Department_ID", "ID")]
		[LogicName("部门")]
		public string Department_Name { get; set; }

		[LogicName("计划号")]
		[ReferenceTo(typeof(ProductPlan), "PlanNumber")]
		[Join("PlanNumber_ID", "ID")]
		public string PlanNumber_Name { get; set; }

		[LogicName("存货名称")]
		[ReferenceTo(typeof(Goods), "Name")]
		[Join("Goods_ID", "ID")]
		public string Goods_Name { get; set; }

		[LogicName("存货编码")]
		[ReferenceTo(typeof(Goods), "Code")]
		[Join("Goods_ID", "ID")]
		public string Goods_Code { get; set; }
		#endregion
	}
}
