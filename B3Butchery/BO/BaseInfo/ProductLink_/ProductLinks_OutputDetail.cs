using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3UnitedInfos.BO;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("生产环节产出明细")]

  public class ProductLinks_OutputDetail : Base
  {
    public long ProductLinks_ID { get; set; }

    [LogicName("存货名称")]
    public long? Goods_ID { get; set; }

    [LogicName("存货名称")]
    [ReferenceTo(typeof(Goods), "Name")]
    [Join("Goods_ID", "ID")]
    public string Goods_Name { get; set; }

    [LogicName("存货编码")]
    [ReferenceTo(typeof(Goods), "Code")]
    [Join("Goods_ID", "ID")]
    public string Goods_Code { get; set; }
  }

  [Serializable]
  public class ProductLinks_OutputDetailCollection : DmoCollection<ProductLinks_OutputDetail>
  { }

}
