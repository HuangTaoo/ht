using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;
using Forks.Utils.Collections;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using BWP.B3Frameworks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Dialogs
{
	class SelectGoodsDialog : DmoMultiSelectDialog<Goods, TemGoodsDetail>
	{

		string setMainNumber = @"if ({SecondUnitRatio} > 0) {
    dfContainer.setValue('主数量', (accMul(dfContainer.getValue('辅数量'),accDiv({MainUnitRatio},{SecondUnitRatio}))) );
}".Replace("{MainUnitRatio}", MainUnitRatio).Replace("{SecondUnitRatio}", SecondUnitRatio);

		string setSecondNumber = @"if ({SecondUnitRatio} > 0) {
    dfContainer.setValue('辅数量', (accMul(dfContainer.getValue('主数量'),accDiv({SecondUnitRatio},{MainUnitRatio}))) );
}".Replace("{MainUnitRatio}", MainUnitRatio).Replace("{SecondUnitRatio}", SecondUnitRatio);
		private const string ConvertDirection = "dfContainer.getValue('UnitConvertDirection')";
		private const string MainUnitRatio = "dfContainer.getValue('MainUnitRatio')";
		private const string SecondUnitRatio = "dfContainer.getValue('SecondUnitRatio')";


		protected override void CreateQuery(VLayoutPanel vPanel)
		{
			var layoutManager = new LayoutManager("", mDFInfo, mQueryContainer);
			layoutManager.Add("存货属性分类", new SimpleLabel("属性分类"), QueryCreator.DFChoiceBox(mDFInfo.Fields["ID"], B3UnitedInfosConsts.DataSources.存货属性分类));
      layoutManager.Add("ProductLine_ID", mQueryContainer.Add(QueryCreator.DFChoiceBox(mDFInfo.Fields["ProductLine_ID"], B3UnitedInfosConsts.DataSources.产品线全部), "ProductLine_ID"));
      layoutManager["ProductLine_ID"].NotAutoAddToContainer = true;
      var config = new AutoLayoutConfig { Cols = 8, DefaultLabelWidth = 4 };
			config.Add("Name");
			config.Add("Spec");
			config.Add("Code");
			config.Add("GoodsProperty_ID");
			config.Add("存货属性分类");
      config.Add("ProductLine_ID");
			layoutManager.Config = config;

			var section = mPageLayoutManager.AddSection(B3FrameworksConsts.PageLayouts.QueryConditions, B3FrameworksConsts.PageLayouts.QueryConditions_DisplayName);
			section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo as DFInfo);
			vPanel.Add(layoutManager.CreateLayout());
			base.CreateQuery(vPanel);
		}

		protected override void CreateQueryGridColumns(DFBrowseGrid grid)
		{
			grid.Columns.Add(new DFBrowseGridColumn("Name"));
			grid.Columns.Add(new DFBrowseGridColumn("Spec"));
			grid.Columns.Add(new DFBrowseGridColumn("GoodsProperty_Name"));
			grid.Columns.EAdd(new DFBrowseGridColumn("GoodsPropertyCatalog_Name")).HeaderText = "属性分类";
			grid.Columns.Add(new DFBrowseGridColumn("SecondUnit"));
			grid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("辅数量")).InitEditControl += (sender, e) =>
			{
				e.Control.Attributes["onchange"] = @"
if({convertDirection}=='双向转换'||{convertDirection}=='由辅至主'){ 
    {setMainNumber}
}".Replace("{setMainNumber}", setMainNumber).Replace("{convertDirection}", ConvertDirection);
			};
			grid.Columns.Add(new DFBrowseGridColumn("MainUnit"));
			grid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("主数量")).InitEditControl += (sender, e) =>
			{
				e.Control.Attributes["onchange"] = @"
if({convertDirection}=='双向转换'||{convertDirection}=='由主至辅'){ 
    {setSecondNumber}
}".Replace("{setSecondNumber}", setSecondNumber).Replace("{convertDirection}", ConvertDirection);
			};

			grid.ValueColumns.Add("MainUnitRatio");
			grid.ValueColumns.Add("SecondUnitRatio");
			grid.ValueColumns.Add("UnitConvertDirection");
		}

		protected override DQueryDom GetQueryDom()
		{
			var dom = base.GetQueryDom();
			var goodsProperty = dom.EJoin<GoodsProperty>();
			dom.Columns.Add(DQSelectColumn.Field("ID"));
			dom.Columns.Add(DQSelectColumn.Field("Name"));
			dom.Columns.Add(DQSelectColumn.Field("Spec"));
			dom.Columns.Add(DQSelectColumn.Field("GoodsProperty_Name"));
			dom.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Name", goodsProperty));
			dom.Columns.Add(DQSelectColumn.Field("SecondUnit"));
			dom.Columns.Add(DQSelectColumn.Field("MainUnit"));
			dom.Columns.Add(DQSelectColumn.Field("MainUnitRatio"));
			dom.Columns.Add(DQSelectColumn.Field("SecondUnitRatio"));
			dom.Columns.Add(DQSelectColumn.Field("UnitConvertDirection"));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Snippet<decimal?>("null"), "主数量"));
			dom.Columns.Add(DQSelectColumn.Create(DQExpression.Snippet<decimal?>("null"), "辅数量"));

			TreeUtil.AddTreeCondition<GoodsPropertyCatalog>(dom, mQueryContainer, "存货属性分类", null, goodsProperty);

			dom.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
			return dom;
		}

		protected override void SetResultFromDFDataRow(TemGoodsDetail dmo, DFDataRow row)
		{
			dmo.Goods_ID = (long)row["ID"];
			dmo.Number = (decimal?)row["主数量"];
			dmo.SecondNumber = (decimal?)row["辅数量"];
		}
	}

	[Serializable]
	internal class TemGoodsDetail : GoodsDetail
	{
	}
}
