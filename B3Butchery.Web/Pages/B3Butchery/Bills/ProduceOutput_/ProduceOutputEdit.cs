using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using System.Web.UI;
using BWP.Web.Layout;
using TSingSoft.WebControls2;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using System.Web.UI.WebControls;
using BWP.B3UnitedInfos;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebPluginFramework;
using BWP.Web.Utils;
using BWP.B3Butchery.Utils;


namespace BWP.Web.Pages.B3Butchery.Bills.ProduceOutput_
{
  public class ProduceOutputEdit : DepartmentWorkFlowBillEditPage<ProduceOutput, IProduceOutputBL>
  {
    protected override void BuildBody(Control container)
    {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      layoutManager.Add("Time", new DFDateInput());
      var planNumberBox = InputCreator.DFChoiceBox(B3ButcheryDataSource.计划号, "PlanNumber_Name", true);
      layoutManager.Add("PlanNumber_ID", planNumberBox);
      var config = new AutoLayoutConfig();
      config.Add("Time");
      config.Add("PlanNumber_ID");
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("ProductLinks_ID");
      layoutManager.Config = config;
      container.Controls.Add(layoutManager.CreateLayout());

      var vPanel = container.EAdd(new VLayoutPanel());
      CreateOutputDetailPanel(vPanel);
    }

    DFEditGrid outputDetailGrid;
    private void CreateOutputDetailPanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      hPanel.Add(new LiteralControl("<h2>明细清单：</h2>"));
      if (CanSave)
      {
        hPanel.Add(new TSButton("载入明细")).Click += delegate
        {
          GetFromUI();
          Dmo.Details.Clear();
          AddLoadDetailQy();
          outputDetailGrid.DataBind();
          AspUtil.Alert(this, "载入产出明细成功");
        };

        hPanel.Add(new SimpleLabel("选择存货"));
        var selectEmp = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
        selectEmp.SelectedValueChanged += delegate
        {
          outputDetailGrid.GetFromUI();
          if (!selectEmp.IsEmpty)
          {
            var empID = long.Parse(selectEmp.Value);
            if (!Dmo.Details.Any(x => x.Goods_ID == empID))
            {
              var d = new ProduceOutput_Detail() { Goods_ID = empID };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectEmp.Clear();
          outputDetailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProduceOutput_Detail>(() => Dmo.Details);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      outputDetailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Number"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("SecondNumber"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      outputDetailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
      outputDetailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      outputDetailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      outputDetailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      new Main_Second_ConvertRatioRowManager(outputDetailGrid, "Number", "SecondNumber");
    }

    protected virtual void AddLoadDetailQy()
    {
      mBL.GetGoodsDetailList(Dmo);
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
      outputDetailGrid.GetFromUI();
    }

    public override void AppToUI()
    {
      base.AppToUI();
      if (CanSave)
        mDFContainer.MakeReadonly("Employee_ID");
    }

    protected override void InitNewDmo(ProduceOutput dmo)
    {
      base.InitNewDmo(dmo);
      dmo.Time = BLContext.Now;
    }
  }
}
