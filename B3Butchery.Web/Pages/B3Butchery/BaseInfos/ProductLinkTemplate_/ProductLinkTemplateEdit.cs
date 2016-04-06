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

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductLinkTemplate_
{
	class ProductLinkTemplateEdit : DomainBaseInfoEditPage<ProductLinkTemplate, IProductLinkTemplateBL>
	{
		protected override void BuildBody(Control container)
		{
			var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
			var config = new AutoLayoutConfig();
			config.Add("Name");
			config.Add("AccountingUnit_ID");
			config.Add("Department_ID");
			config.Add("ProductLinks_ID"); 
			config.Add("CollectType");
			config.Add("Remark");
			layoutManager.Config = config;
			container.Controls.Add(layoutManager.CreateLayout());
			var vPanel = container.EAdd(new VLayoutPanel());
			CreateDetailPanel(vPanel);
		}

		DFEditGrid detailGrid;
		private void CreateDetailPanel(VLayoutPanel vPanel)
		{
			var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
			hPanel.Add(new LiteralControl("<h2>存货明细：</h2>"));
			if (CanSave)
			{
				hPanel.Add(new SimpleLabel("选择存货"));
				var selectGoods = hPanel.Add(new ChoiceBox(B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
				selectGoods.SelectedValueChanged += delegate
				{
					detailGrid.GetFromUI();
					if (!selectGoods.IsEmpty)
					{
						var goodsID = long.Parse(selectGoods.Value);
						if (!Dmo.Details.Any(x => x.Goods_ID == goodsID))
						{
							var d = new ProductLinkTemplate_Detail() { Goods_ID = goodsID };
							DmoUtil.RefreshDependency(d, "Goods_ID");
							Dmo.Details.Add(d);
						}
					}
					selectGoods.Clear();
					detailGrid.DataBind();
				};
			}
			var detailEditor = new DFCollectionEditor<ProductLinkTemplate_Detail>(() => Dmo.Details);
			detailEditor.AllowDeletionFunc = () => CanSave;
			detailEditor.CanDeleteFunc = (detail) => CanSave;
			detailEditor.IsEditableFunc = (field, detail) => CanSave;
			detailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
			detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
			detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
			detailGrid.Columns.Add(new DFEditGridColumn("Remark"));
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
			detailGrid.GetFromUI();
		}
	}
}
