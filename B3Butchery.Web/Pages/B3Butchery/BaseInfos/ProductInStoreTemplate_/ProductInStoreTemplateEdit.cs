using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.Web.Layout;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductInStoreTemplate_
{
	class ProductInStoreTemplateEdit : DomainBaseInfoEditPage<ProductInStoreTemplate, IProductInStoreTemplateBL>
	{
		protected override void BuildBody(Control container)
		{
			var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
			var config = new AutoLayoutConfig();
			config.Add("Name");
			config.Add("AccountingUnit_ID");
			config.Add("Department_ID");
			config.Add("InStoreType_ID");
			config.Add("Employee_ID");
			config.Add("Remark");
			layoutManager.Config = config;
			container.Controls.Add(layoutManager.CreateLayout());
			var vPanel = container.EAdd(new VLayoutPanel());
			CreateStoreDetailPanel(vPanel);
			CreateGoodsDetailPanel(vPanel);
		}

		DFEditGrid storeDetailGrid, goodsDetailGrid;
		private void CreateStoreDetailPanel(VLayoutPanel vPanel)
		{
			var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
			hPanel.Add(new LiteralControl("<h2>仓库明细：</h2>"));
			if (CanSave)
			{
				hPanel.Add(new SimpleLabel("选择仓库"));
				var selectStore = hPanel.Add(new ChoiceBox(B3FrameworksConsts.DataSources.授权仓库) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
				selectStore.SelectedValueChanged += delegate
				{
					storeDetailGrid.GetFromUI();
					if (!selectStore.IsEmpty)
					{
						var storeID = long.Parse(selectStore.Value);
						if (!Dmo.StoreDetails.Any(x => x.Store_ID == storeID))
						{
							var d = new ProductInStoreTemplate_StoreDetail() { Store_ID = storeID };
							DmoUtil.RefreshDependency(d, "Store_ID");
							Dmo.StoreDetails.Add(d);
						}
					}
					selectStore.Clear();
					storeDetailGrid.DataBind();
				};
			}
			var detailEditor = new DFCollectionEditor<ProductInStoreTemplate_StoreDetail>(() => Dmo.StoreDetails);
			detailEditor.AllowDeletionFunc = () => CanSave;
			detailEditor.CanDeleteFunc = (detail) => CanSave;
			detailEditor.IsEditableFunc = (field, detail) => CanSave;
			storeDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
			storeDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Store_Name"));
			storeDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Store_Code"));
			storeDetailGrid.Columns.Add(new DFEditGridColumn("Remark"));
		}

		private void CreateGoodsDetailPanel(VLayoutPanel vPanel)
		{
			var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
			hPanel.Add(new LiteralControl("<h2>存货明细：</h2>"));
			if (CanSave)
			{
				hPanel.Add(new SimpleLabel("选择存货"));
				var selectGoods = hPanel.Add(new ChoiceBox(B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
				selectGoods.SelectedValueChanged += delegate
				{
					goodsDetailGrid.GetFromUI();
					if (!selectGoods.IsEmpty)
					{
						var goodsID = long.Parse(selectGoods.Value);
						if (!Dmo.GoodsDetails.Any(x => x.Goods_ID == goodsID))
						{
							var d = new ProductInStoreTemplate_GoodsDetail() { Goods_ID = goodsID };
							DmoUtil.RefreshDependency(d, "Goods_ID");
							Dmo.GoodsDetails.Add(d);
						}
					}
					selectGoods.Clear();
					goodsDetailGrid.DataBind();
				};
			}
			var detailEditor = new DFCollectionEditor<ProductInStoreTemplate_GoodsDetail>(() => Dmo.GoodsDetails);
			detailEditor.AllowDeletionFunc = () => CanSave;
			detailEditor.CanDeleteFunc = (detail) => CanSave;
			detailEditor.IsEditableFunc = (field, detail) => CanSave;
			goodsDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
			goodsDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
			goodsDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
			goodsDetailGrid.Columns.Add(new DFEditGridColumn("Remark"));
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!IsPostBack)
				DataBind();
		}

		public override void GetFromUI()
		{
			base.GetFromUI();
			storeDetailGrid.GetFromUI();
			goodsDetailGrid.GetFromUI();
		}
	}
}
