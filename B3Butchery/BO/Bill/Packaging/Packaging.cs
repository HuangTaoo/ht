using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("包装物配置单")]
  public class Packaging : DomainBill
  {
    [LogicName("名称")]
    public string Name { get; set; }


    [LogicName("包装属性")]
    public NamedValue<包装属性>? Packing_Attr { get; set; }


    private readonly Packaging_DetailCollection _details = new Packaging_DetailCollection();

    [OneToMany(typeof(Packaging_Detail), "ID")]
    [Join("ID", "Packaging_ID")]
    public Packaging_DetailCollection Details
    {
      get { return _details; }
    }


    [Serializable]
    public class Packaging_DetailCollection : DmoCollection<Packaging_Detail>
    {

    }
  }
}
