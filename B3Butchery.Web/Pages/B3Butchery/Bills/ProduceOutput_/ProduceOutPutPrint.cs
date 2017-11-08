using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;

namespace BWP.Web.Pages.B3Butchery.Bills.ProduceOutput_
{


    class ProduceOutputPrint : DomainTemplatePrintPage<ProduceOutput, IProduceOutputBL>
    {
        protected override void AddParameters(IDictionary<string, object> dic)
        {

            var details = Dmo.Details.OrderBy(x => x.CalculateCatalog_Name).ToList();
            dic.Add("$Details", details);
            dic.Add("$DetailType", typeof(ProduceOutput_Detail));
            var date = Dmo.Time == null ? "" : Dmo.Time.Value.ToShortDateString();
            dic.Add("$日期", date);
        }





    }
}
