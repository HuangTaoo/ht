using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductInStoreTemplate_
{
	class ProductInStoreTemplateList : DomainBaseInfoListPage<ProductInStoreTemplate, IProductInStoreTemplateBL>
	{
		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
			{
				config.AddAfter("AccountingUnit_ID", "ID");
				config.AddAfter("Department_ID", "AccountingUnit_ID");
				config.AddAfter("InStoreType_ID", "Department_ID");
				config.AddAfter("Employee_ID", "InStoreType_ID");
			}));
		}
		protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
		{
			base.AddDFBrowseGridColumn(grid, field);
			if (field == "Name")
			{
				AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
				AddDFBrowseGridColumn(grid, "Department_Name");
				AddDFBrowseGridColumn(grid, "InStoreType_Name");
				AddDFBrowseGridColumn(grid, "Employee_Name");
			}
		}

	}
}
