using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO.BaseInfo;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL.BaseInfo.HandoverRecord_
{

    [BusinessInterface(typeof(HandoverRecordBL))]
    [LogicName("交接记录")]
    public interface IHandoverRecordBL : IBaseBL<HandoverRecord>
    { }
    public class HandoverRecordBL : BaseBL<HandoverRecord>, IHandoverRecordBL
    {

    }
}
