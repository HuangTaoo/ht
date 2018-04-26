using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductInStore_
{
	class ProductInStorePrint : DomainTemplatePrintPage<ProductInStore, IProductInStoreBL>
	{
		protected override void AddParameters(IDictionary<string, object> dic)
		{
			dic.Add("$Details", Dmo.Details);
      dic.Add("$CargoSpace_Name",Dmo.Details[0].CargoSpace_Name);
			dic.Add("$DetailType", typeof(ProductInStore_Detail));
		}
	}
}
