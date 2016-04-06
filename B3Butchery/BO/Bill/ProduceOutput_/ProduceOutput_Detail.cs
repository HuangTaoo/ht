using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("产出单明细")]

  public class ProduceOutput_Detail : GoodsDetail
  {
    public long ProduceOutput_ID { get; set; }
  }
  [Serializable]
  public class ProduceOutput_DetailCollection : DmoCollection<ProduceOutput_Detail>
  { }
}
