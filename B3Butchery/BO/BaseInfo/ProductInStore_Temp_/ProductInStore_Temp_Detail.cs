using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("模板_明细")]
  public class ProductInStore_Temp_Detail:GoodsDetail
  {
    public long ProductInStoreTemp_ID { get; set; }

   
  }

  [Serializable]
  public class ProductInStore_Temp_DetailCollection : DmoCollection<ProductInStore_Temp_Detail>
  { }

}
