using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3UnitedInfos.BO;
using Forks.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.BO.MoneyTemplate;

namespace BWP.B3Butchery.BO
{

  [Serializable, DFClass, LogicName("投入单明细")]

  public class ProduceInput_Detail : GoodsDetail
  {
    public long ProduceInput_ID { get; set; }
  }

  [Serializable]
  public class ProduceInput_DetailCollection : DmoCollection<ProduceInput_Detail>
  { }
}
