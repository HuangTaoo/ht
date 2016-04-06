using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using System.Web.UI;
using BWP.Web.Layout;
using TSingSoft.WebControls2;
using System.Web.UI.WebControls;
using BWP.B3UnitedInfos;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.DataForm;
using BWP.Web.Utils;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductLinks_
{
  class ProductLinksEdit : DomainBaseInfoEditPage<ProductLinks, IProductLinksBL>
  {
    protected override void BuildBody(Control container)
    {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      layoutManager.Add("ChargePerson_ID", InputCreator.DFChoiceBox("授权员工", "ChargePerson_Name", true));
      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("ProductLine_ID");
      config.Add("ChargePerson_ID");
      layoutManager.Config = config;
      container.Controls.Add(layoutManager.CreateLayout());
      var vPanel = container.EAdd(new VLayoutPanel());
      CreateInputDetailPanel(vPanel);
      CreateOutputDetailPanel(vPanel);
    }

    DFEditGrid inputDetailGrid, outputDetailGrid;
    private void CreateInputDetailPanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      hPanel.Add(new LiteralControl("<h2>投入明细设置：</h2>"));
      if (CanSave)
      {
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectEmp = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
        selectEmp.SelectedValueChanged += delegate
        {
          inputDetailGrid.GetFromUI();
          if (!selectEmp.IsEmpty)
          {
            var empID = long.Parse(selectEmp.Value);
            if (!Dmo.InputDetails.Any(x => x.Goods_ID == empID))
            {
              var d = new ProductLinks_InputDetail() { Goods_ID = empID };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.InputDetails.Add(d);
            }
          }
          selectEmp.Clear();
          inputDetailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProductLinks_InputDetail>(() => Dmo.InputDetails);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      inputDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn("LivingBodyMark"));
    }

    private void CreateOutputDetailPanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      hPanel.Add(new LiteralControl("<h2>产出明细设置：</h2>"));
      if (CanSave)
      {
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectEmp = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
        selectEmp.SelectedValueChanged += delegate
        {
          outputDetailGrid.GetFromUI();
          if (!selectEmp.IsEmpty)
          {
            var empID = long.Parse(selectEmp.Value);
            if (!Dmo.OutputDetails.Any(x => x.Goods_ID == empID))
            {
              var d = new ProductLinks_OutputDetail() { Goods_ID = empID };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.OutputDetails.Add(d);
            }
          }
          selectEmp.Clear();
          outputDetailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProductLinks_OutputDetail>(() => Dmo.OutputDetails);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      outputDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
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
      inputDetailGrid.GetFromUI();
      outputDetailGrid.GetFromUI();
    }

    public override void AppToUI()
    {
      base.AppToUI();
      if (CanSave)
        mDFContainer.MakeReadonly("Employee_ID");
    }

  }
}
