using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BL;
using BWP.B3UnitedInfos.BO;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using TSingSoft.WebControls2;
using Forks.EnterpriseServices.DataForm;
using System.Web.UI.WebControls;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ButcheryGoods_
{



    class ButcheryGoodsEdit : BaseInfoEditPage<ButcheryGoods, IButcheryGoodsBL>
    {
        protected override void AddFirstRowRightControls(System.Web.UI.HtmlControls.HtmlTableCell cell)
        {
            base.AddFirstRowRightControls(cell);

            if (!IsNew)
            {
                cell.Controls.Add(new MultiViewSwitcher("存货编辑", "屠宰分割"));
            }
            else
            {
                cell.Controls.Add(new MultiViewSwitcher("存货新建", "屠宰分割"));
            }
        }


        private bool IsManager
        {
            get
            {
                return IsNew || User.IsInRole("B3UnitedInfos.存货.管理员") || DomainUserUtil.DomainUserIsInGroup(DomainContext.Current.DomainUser.ID, MinDmo.GoodsManagerDomainUserGroup_ID);
            }
        }

        GoodsProperty mMinGoodsProperty;

        protected override void InitLoadedMinDmo(ButcheryGoods mMinDmo)
        {
            base.InitLoadedMinDmo(mMinDmo);

            if (mMinDmo.GoodsProperty_ID.HasValue)
            {
                mMinGoodsProperty = BIFactory.Create<IGoodsPropertyBL>().LoadMinDmo(mMinDmo.GoodsProperty_ID.Value);
                if (mMinGoodsProperty == null)
                {
                    throw new Exception("存货上的存货属性在数据库中已不存在");
                }
            }
        }

        protected override bool JudgeCanSave
        {
            get
            {
                return base.JudgeCanSave &&
                    IsManager;
            }
        }

        protected override bool JudgeCanDelete
        {
            get
            {
                return base.JudgeCanDelete && IsManager;
            }
        }

        protected override bool JudgeCanLock
        {
            get
            {
                return base.JudgeCanLock && IsManager;
            }
        }

        protected override bool JudgeCanUnLock
        {
            get
            {
                return base.JudgeCanUnLock && IsManager;
            }
        }

        protected override bool JudgeCanStop
        {
            get
            {
                return base.JudgeCanStop && IsManager;
            }
        }

        protected override bool JudgeCanStart
        {
            get
            {
                return base.JudgeCanStart && IsManager;
            }
        }


        protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
        {
            var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

            if (mMinGoodsProperty != null)
            {
                if (!string.IsNullOrEmpty(mMinGoodsProperty.GoodsCodeRole))
                {
                    layoutManager.Add("Code", new DFValueLabel());
                }

                if (!string.IsNullOrEmpty(mMinGoodsProperty.GoodsNameRole))
                {
                    layoutManager.Add("Name", new DFValueLabel());
                }
            }

            var config = new AutoLayoutConfig();
            layoutManager.Config = config;
            config.Add("GoodsProperty_ID");

            config.Add("Name");
            config.Add("PrintShortName");
            config.Add("Code");
            config.Add("Spec");
            config.Add("Feature");
            config.Add("Origin");
            config.Add("Brand");
            config.Add("ProductLine_ID");
            config.Add("TaxRate");
            config.Add("MainUnit");
            config.Add("SecondUnit");
            config.Add("MainUnitRatio");
            config.Add("SecondUnitRatio");
            config.Add("UnitConvertDirection");
            config.Add("Barcode");
            config.Add("OuterCode");
            config.Add("SecondUnitII");
            config.Add("SecondUnitII_MainUnitRatio");
            config.Add("SecondUnitII_SecondUnitRatio");
            config.Add("StandPlateNumber");
           
            config.Add("Remark");




            pageLayoutSection.SetRequired("GoodsProperty_ID", "Name", "Code");
            pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

            titlePanel.Controls.Add(layoutManager.CreateLayout());

       

        }

        protected override void AddActions(ButtonGroup buttonGroup)
        {
            base.AddActions(buttonGroup);
            var saveAndNew = buttonGroup.Actions.Where((item) => item.Text == "保存并新建").FirstOrDefault();
            if (saveAndNew != null)
            {
                buttonGroup.Actions.Remove(saveAndNew);
            }
        }

        bool CanSelectGoodsProperty
        {
            get
            {
                return MinDmo.GoodsProperty_ID == null || User.IsInRole("B3UnitedInfos.存货属性.管理员");
            }
        }

        public override void AppToUI()
        {
            base.AppToUI();

            var goodsPropertyIDInput = mDFContainer.GetControl("GoodsProperty_ID") as IDFFieldEditor;
            if (goodsPropertyIDInput != null && !goodsPropertyIDInput.Readonly && !CanSelectGoodsProperty)
            {
                mDFContainer.MakeReadonly("GoodsProperty_ID", true);
            }
        }




        public override void GetFromUI()
        {
            base.GetFromUI();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                DataBind();
            }
        }
    }
}
