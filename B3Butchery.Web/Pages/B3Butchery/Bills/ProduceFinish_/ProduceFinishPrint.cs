using System.Collections.Generic;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;

namespace BWP.Web.Pages.B3Butchery.Bills.ProduceFinish_ {
  class ProduceFinishPrint : DomainTemplatePrintPage<ProduceFinish, IProduceFinishBL> {
    protected override void AddParameters(IDictionary<string, object> dic) {
      dic.Add("$Details", Dmo.Details);
      dic.Add("$DetailType", typeof(ProduceFinish_Detail));
    }
  }
}

