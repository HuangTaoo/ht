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
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Butchery;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebPluginFramework;


namespace BWP.Web.Pages.B3Butchery.Bills.ProductPlan_
{
  class ProductPlanEdit : DepartmentWorkFlowBillEditPage<ProductPlan, IProductPlanBL>
  {
    protected override void BuildBody(Control container)
    {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      config.Add("Date");
      config.Add("EndDate");
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("PlanNumber");
      config.Add("ProductType");
      layoutManager.Config = config;
      container.Controls.Add(layoutManager.CreateLayout());

      var planNumberPanel = new HLayoutPanel();
      container.Controls.Add(planNumberPanel);
      planNumberButton = new TSButton();
      planNumberPanel.Add(planNumberButton).Click += delegate
      {
        if (Dmo.BillState == 单据状态.已审核 && !string.IsNullOrEmpty(Dmo.PlanNumber))
        {
          using (var context = new TransactionContext())
          {
            var update = new DQUpdateDom(typeof(ProductPlan));
            update.Where.Conditions.Add(DQCondition.EQ("PlanNumber", Dmo.PlanNumber));
            update.Columns.Add(new DQUpdateColumn("PlanNumbers", true));

            context.Session.ExecuteNonQuery(update);
            context.Commit();
          }

          Dmo.PlanNumbers = true;
          mBL.Update(Dmo);
        }
        AspUtil.RedirectAndAlert(this, Request.RawUrl, "计划号关闭成功");
      };
      var vPanel = container.EAdd(new VLayoutPanel());
      CreateInputDetailPanel(vPanel);
      CreateOutputDetailPanel(vPanel);
    }

    private TSButton planNumberButton;
    DFEditGrid inputDetailGrid, outputDetailGrid;
    private void CreateInputDetailPanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      hPanel.Add(new LiteralControl("<h2>投入明细：</h2>"));
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
              var d = new ProductPlan_InputDetail() { Goods_ID = empID };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.InputDetails.Add(d);
            }
          }
          selectEmp.Clear();
          inputDetailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProductPlan_InputDetail>(() => Dmo.InputDetails);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      inputDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("PlanNumber"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("PlanSecondNumber"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      inputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
    }

    private void CreateOutputDetailPanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      hPanel.Add(new LiteralControl("<h2>产出明细：</h2>"));
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
              var d = new ProductPlan_OutputDetail() { Goods_ID = empID };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.OutputDetails.Add(d);
            }
          }
          selectEmp.Clear();
          outputDetailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProductPlan_OutputDetail>(() => Dmo.OutputDetails);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      outputDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("PlanNumber"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("PlanSecondNumber"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      planNumberButton.Enabled = Dmo.BillState == 单据状态.已审核 && Dmo.PlanNumbers==false && !IsNew;
      if (Dmo.PlanNumbers == true && !IsNew)
        planNumberButton.Text = "计划号已经关闭";
      else
        planNumberButton.Text = "是否关闭计划号";

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

    protected override void InitNewDmo(ProductPlan dmo)
    {
      base.InitNewDmo(dmo);
      dmo.Date = DateTime.Today;
    }
  }
}
