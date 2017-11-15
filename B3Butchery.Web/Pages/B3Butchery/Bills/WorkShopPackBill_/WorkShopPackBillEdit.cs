using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;

using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.WorkShopPackBill_
{


    class WorkShopPackBillEdit : DepartmentWorkFlowBillEditPage<WorkShopPackBill, IWorkShopPackBillBL>
    {
        private DFEditGrid _detailGrid;
        protected override void BuildBody(Control control)
        {
            base.BuildBody(control);
            AddDetails(control.EAdd(new TitlePanel("存货明细", "存货明细")));
        }
        protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
        {
            var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
            var config = new AutoLayoutConfig();
            layoutManager.Config = config;
            config.Add("AccountingUnit_ID");
            config.Add("Department_ID");
            config.Add("Date");
            config.Add("Remark");
            pageLayoutSection.SetRequired("AccountingUnit_ID");
            pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

            titlePanel.Controls.Add(layoutManager.CreateLayout());
        }

        private void AddDetails(TitlePanel titlePanel)
        {
            var vPanel = titlePanel.EAdd(new VLayoutPanel());
            if (CanSave)
            {
                var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
                hPanel.Add(new SimpleLabel("选择存货"));
                var selectEmp = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
                selectEmp.SelectedValueChanged += delegate
                {
                    _detailGrid.GetFromUI();
                    if (!selectEmp.IsEmpty)
                    {
                        var empID = long.Parse(selectEmp.Value);

                        var d = new WorkShopRecord() { Goods_ID = empID };
                        DmoUtil.RefreshDependency(d, "Goods_ID");
                        Dmo.Details.Add(d);

                    }
                    selectEmp.Clear();
                    _detailGrid.DataBind();
                };
            }

            var editor = new DFCollectionEditor<WorkShopRecord>(() => Dmo.Details)
            {
                AllowDeletionFunc = () => CanSave,
                CanDeleteFunc = detail => true,
                IsEditableFunc = (field, detail) => CanSave
            };

       

            _detailGrid = new DFEditGrid(editor) { DFGridSetEnabled = false, Width = Unit.Percentage(100) };

            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Number"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("SecondNumber"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
        
            _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("SecondNumber2"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnitII"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Remark"));
            var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
            titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
            section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(WorkShopRecord)));

            vPanel.Add(_detailGrid);

        }

        public override void AppToUI()
        {
            base.AppToUI();
            _detailGrid.DataBind();
        }

        public override void GetFromUI()
        {
            base.GetFromUI();
            _detailGrid.GetFromUI();
        }

    }
}
