using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
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

namespace BWP.Web.Pages.B3Butchery.Tools.GoodsReferencePriceConfig_
{
	class GoodsReferencePriceConfig : ServerPage
	{
		QueryContainer mQueryContainer;
		DFInfo mDFInfo;

		public GoodsReferencePriceConfig()
		{
			mQueryContainer = QueryContainer.FromResource(this.GetType().FullName + ".xml", this.GetType().Assembly);
			mDFInfo = DFInfo.Get(typeof(Goods));
		}

		protected override void InitForm(HtmlForm form)
		{
			if (!User.IsInRole("B3Butchery.功能.存货参考单价配置"))
				throw new AppSecurityException();
			form.Controls.Add(new PageTitle("存货参考单价配置"));
			var vPanel = form.EAdd(new VLayoutPanel());
			AddQueryControl(vPanel);
			AddQueryResult(vPanel);
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
			grid.Columns.Add(new DFEditGridColumn("ReferencePrice"));
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
					var refObj = new GoodsReferencePrice();
					refObj.Goods_ID = (long)row["ID"];
					refObj.ReferencePrice = (decimal?)row["ReferencePrice"];
					refObj.Remark = (string)row["Remark"];
					if (refObj.ReferencePrice == null)
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
			var deleteDom = new DQDeleteDom(typeof(GoodsReferencePrice));
			deleteDom.Where.Conditions.Add(DQCondition.EQ("Goods_ID", goodsID));
			session.ExecuteNonQuery(deleteDom);
		}

		DQueryDom GetQueryDom()
		{
			var dom = mQueryContainer.Build();
			var goodsAlias = dom.From.RootSource.Alias;
			var refAlias = new JoinAlias(typeof(GoodsReferencePrice));
			dom.From.AddJoin(JoinType.Left, new DQDmoSource(refAlias), DQCondition.EQ(goodsAlias, "ID", refAlias, "Goods_ID"));
			dom.Columns.Add(DQSelectColumn.Field("ID"));
			dom.Columns.Add(DQSelectColumn.Field("Name"));
			dom.Columns.Add(DQSelectColumn.Field("Code"));
			dom.Columns.Add(DQSelectColumn.Field("Spec"));
			dom.Columns.Add(DQSelectColumn.Field("ReferencePrice", refAlias));
			dom.Columns.Add(DQSelectColumn.Field("Remark", refAlias));
			dom.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
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
