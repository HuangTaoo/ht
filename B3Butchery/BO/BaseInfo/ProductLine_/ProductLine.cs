using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass]
  [LogicName("生产线")]

  public class ProductLine : DomainBaseInfo, IWithCodeBaseInfo
  {
    [LogicName("编号")]
    [DbColumn(AllowNull = false, Unique = true)]
    public string Code { get; set; }
  }
}
