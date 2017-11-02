using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [LogicName("客户端存货配置")]
  [Serializable, DFClass]
  public class ClientGoodsSet: DomainBaseInfo
  {



    private readonly ClientGoodsSet_DetailCollection _details = new ClientGoodsSet_DetailCollection();

    [OneToMany(typeof(ClientGoodsSet_Detail), "ID", false)]
    [Join("ID", "ClientGoodsSet_ID")]
    public ClientGoodsSet_DetailCollection Details
    {
      get { return _details; }
    }

  }
}
