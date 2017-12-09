using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Pages;
using TSingSoft.WebPluginFramework.Security;
using Forks.Utils.Collections;

namespace BWP.Web.Pages.B3Butchery.Tools.ChengPinToBanChengPinConfigPage_
{
	class ChengPinToBanChengPinConfigPage : ServerPage
	{
		QueryContainer mQueryContainer;
		DFInfo mDFInfo;

		public ChengPinToBanChengPinConfigPage()
		{
			mQueryContainer = QueryContainer.FromResource(this.GetType().FullName + ".xml", this.GetType().Assembly);
			mDFInfo = DFInfo.Get(typeof(Goods));
		}

		protected override void InitForm(HtmlForm form)
		{
			if (!User.IsInRole("B3Butchery.功能.成品转半成品配置"))
				throw new AppSecurityException();
			form.Controls.Add(new PageTitle("成品转半成品配置"));
			var vPanel = form.EAdd(new VLayoutPanel());
			AddQueryControl(vPanel);
			AddQueryResult(vPanel);
      B3ButcheryWebUtil.CreateExportExcelPart(vPanel, grid, "成品转半成品配置导出.xlsx");
		}

		private void AddQueryControl(VLayoutPanel vPanel)
		{
			var manager = new LayoutManager("", mDFInfo, mQueryContainer);
			manager.Add("ID", new SimpleLabel("存货属性分类"), QueryCreator.DFChoiceBox(mDFInfo.Fields["ID"], B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货属性分类));
			manager.Add("GoodsProperty_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["GoodsProperty_ID"], B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货属性全部));
			var config = new AutoLayoutConfig
			{
				Cols = 8,
				DefaultLabelWidth = 4
			};
			config.Add("ID");
			config.Add("GoodsProperty_ID");
			config.Add("Name");
			config.Add("Code");
			manager.Config = config;
			vPanel.Add(manager.CreateLayout());
			var hParnel = vPanel.Add(new HLayoutPanel());
			hParnel.Add(new TSButton("开始查询", delegate { grid.Query = GetQueryDom(); grid.DataBind(); }));
			hParnel.Add(new RedirectTSButton("清空条件"));
		}

		DFBrowseGrid grid;
		private void AddQueryResult(VLayoutPanel vPanel)
		{
			vPanel.Add(new LiteralControl("<BR/>"));
			grid = vPanel.Add(new DFBrowseGrid(new DFDataTableEditor()) { Width = Unit.Percentage(100) });
			grid.Columns.Add(new DFBrowseGridColumn("Name"));
			grid.Columns.Add(new DFBrowseGridColumn("Code"));
			grid.Columns.Add(new DFBrowseGridColumn("Spec"));
			var goods2Name = new DFEditGridColumn<DFChoiceBox>("Goods2_ID");
			goods2Name.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFChoiceBox> e)
			{
				e.Control.DataKind = B3ButcheryDataSource.存货带编号;
				e.Control.DFDisplayField = "Goods2_Name";
				e.Control.EnableInputArgument = true;
				e.Control.EnableTopItem = true;
				e.Control.Width = Unit.Pixel(120);
			};
			grid.Columns.EAdd(goods2Name).HeaderText = "半成品名称";
			grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods2_Code"));
			grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods2_Spec"));
			grid.Columns.Add(new DFEditGridColumn("Remark"));
			vPanel.Add(new TSButton("保存", SaveBtnClick));
		}

		void SaveBtnClick(object sender, EventArgs e)
		{
			grid.GetFromUI();
			var data = grid.CurrentData.Data;
			using (var context = new TransactionContext())
			{
				foreach (var row in data.Rows)
				{
					var refObj = new ChengPinToBanChengPinConfig();
					refObj.Goods_ID = (long)row["ID"];
					refObj.Goods2_ID = (long?)row["Goods2_ID"];
					refObj.Remark = (string)row["Remark"];
					if (refObj.Goods2_ID == null)
						DeleteConfigIfExist(refObj.Goods_ID, context.Session);
					else
						context.Session.AddInsertOrUpdate(refObj);
				}
				context.Commit();
			}
			AspUtil.Alert(this, "保存成功");
			grid.DataBind();
		}

		void DeleteConfigIfExist(long goodsID, IDmoSession session)
		{
			var deleteDom = new DQDeleteDom(typeof(ChengPinToBanChengPinConfig));
			deleteDom.Where.Conditions.Add(DQCondition.EQ("Goods_ID", goodsID));
			session.ExecuteNonQuery(deleteDom);
		}

		DQueryDom GetQueryDom()
		{
            
			var dom = mQueryContainer.Build();
			var goodsAlias = dom.From.RootSource.Alias;
            var prop = new JoinAlias("pro", typeof(GoodsProperty));

			var refAlias = new JoinAlias(typeof(ChengPinToBanChengPinConfig));
			dom.From.AddJoin(JoinType.Left, new DQDmoSource(refAlias), DQCondition.EQ(goodsAlias, "ID", refAlias, "Goods_ID"));
            dom.From.AddJoin(JoinType.Left, new DQDmoSource(prop), DQCondition.EQ(goodsAlias, "GoodsProperty_ID", prop, "ID"));
			dom.Columns.Add(DQSelectColumn.Field("ID"));
			dom.Columns.Add(DQSelectColumn.Field("Name", "成品名称"));
			dom.Columns.Add(DQSelectColumn.Field("Code", "成品编码"));
			dom.Columns.Add(DQSelectColumn.Field("Spec", "成品规格"));
			dom.Columns.Add(DQSelectColumn.Field("Goods2_ID", refAlias));
			dom.Columns.Add(DQSelectColumn.Field("Goods2_Name", refAlias));
			dom.Columns.Add(DQSelectColumn.Field("Goods2_Code", refAlias));
			dom.Columns.Add(DQSelectColumn.Field("Goods2_Spec", refAlias));
			dom.Columns.Add(DQSelectColumn.Field("Remark", refAlias));
			dom.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
            dom.Where.Conditions.Add(DQCondition.EQ(prop, "EnableSale", true));
			return dom;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!IsPostBack)
			{
				grid.Query = GetQueryDom();
				grid.DataBind();
			}
		}
	}
}
