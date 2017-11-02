using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BL
{
    [BusinessInterface(typeof(ClientGoodsSetBL))]
    [LogicName("客户端存货配置")]
    public interface IClientGoodsSetBL : IDomainBaseInfoBL<ClientGoodsSet>
    {

    }

    public class ClientGoodsSetBL : DomainBaseInfoBL<ClientGoodsSet>, IClientGoodsSetBL
    {

    }
}
