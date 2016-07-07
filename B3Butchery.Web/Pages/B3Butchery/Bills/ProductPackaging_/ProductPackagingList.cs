using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.Web.Utils;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductPackaging_
{
  public class ProductPackagingList : DomainBillListPage<ProductPackaging, IProductPackagingBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        panel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], B3FrameworksConsts.DataSources.授权会计单位全部));
        panel.Add("Department_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Department_ID"], B3FrameworksConsts.DataSources.授权部门全部));
        panel.Add("Store_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Store_ID"], B3FrameworksConsts.DataSources.授权仓库));
        config.AddAfter("AccountingUnit_ID", "ID");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Store_ID", "Department_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid,"Store_ID");
      }
    }
  }
}
