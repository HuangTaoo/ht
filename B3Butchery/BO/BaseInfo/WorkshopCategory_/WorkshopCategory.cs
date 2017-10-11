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
  [Serializable, DFClass]
  [LogicName("车间品类")]
  public class WorkshopCategory :DomainBaseInfo , IWithCodeBaseInfo
  {
    [LogicName("编号")]
    [DbColumn(AllowNull = false, Unique = true)]
    public string Code { get; set; }

    [LogicName("是否称重")]
    public bool? IfWeight { get; set; }
  }
}
