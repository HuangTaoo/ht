using System.Collections.Generic;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System.Linq;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductNotice_ {
  class ProductNoticePrint : DomainTemplatePrintPage<ProductNotice, IProductNoticeBL> {
    protected override void AddParameters(IDictionary<string, object> dic) {
      dic.Add("$Details", Dmo.Details);

      dic.Add("$OrderByDetails", Dmo.Details.OrderBy(x => x.OrderByID).ToList());
      dic.Add("$DetailType", typeof(ProductNotice_Detail));
    }
  }
}

