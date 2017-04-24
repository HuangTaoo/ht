using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{
  [LogicName("速冻入库明细")]
  [Serializable]
  [DFClass]
 public class FrozenInStore_Detail : PriceGoodsDetailBase
  {
    public long FrozenInStore_ID { get; set; }
  }
  [Serializable]
  public class FrozenInStore_DetailCollection : DmoCollection<FrozenInStore_Detail>
  {
  }
}
