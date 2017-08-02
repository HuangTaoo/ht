using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("领料单_明细")]
  public class Picking_Detail : GoodsDetail
  {
    public long Picking_ID { get; set; }
  }

  [Serializable]
  public class Picking_DetailCollection : DmoCollection<Picking_Detail>
  { }
}
