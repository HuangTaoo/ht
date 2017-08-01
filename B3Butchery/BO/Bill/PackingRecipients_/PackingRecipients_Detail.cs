using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("包装领用_明细")]
  public class PackingRecipients_Detail : GoodsDetail
  {
    public long PackingRecipients_ID { get; set; }
  }

  [Serializable]
  public class PackingRecipients_DetailCollection : DmoCollection<PackingRecipients_Detail>
  { }
}
