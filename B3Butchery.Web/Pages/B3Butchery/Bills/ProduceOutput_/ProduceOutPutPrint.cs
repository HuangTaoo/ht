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
            var details = new List<ProduceOutput_Detail>();
            var group = Dmo.Details.GroupBy(x => x.CalculateCatalog_ID);
            foreach (var one in group)
            {
                foreach (var de in one)
                {
                    details.Add(de);
                }
            }
            dic.Add("$Details", details);
            dic.Add("$DetailType", typeof(ProduceOutput_Detail));
        }





    }
}
