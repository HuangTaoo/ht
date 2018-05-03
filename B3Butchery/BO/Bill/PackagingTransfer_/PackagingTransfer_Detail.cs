using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("包装调拨单_明细")]
  public class PackagingTransfer_Detail : Base
  {

    public long PackagingTransfer_ID { get; set; }

    [LogicName("存货")]
    public long? Goods_ID { get; set; }

    [ReferenceTo(typeof(ButcheryGoods), "Name")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货")]
    public string Goods_Name { get; set; }


    [ReferenceTo(typeof(ButcheryGoods), "Code")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货编码")]
    public string Goods_Code { get; set; }

    [ReferenceTo(typeof(ButcheryGoods), "Spec")]
    [Join("Goods_ID", "ID")]
    [DFPrompt("存货规格")]
    public string Goods_Spec { get; set; }

    [LogicName("主数量")]
    public Money<decimal>? Number { get; set; }

    [LogicName("辅数量")]
    public Money<decimal>? SecondNumber { get; set; }

  }

  [Serializable]
  public class PackagingTransfer_DetailCollection : DmoCollection<PackagingTransfer_Detail>
  {
  }
}
