using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("包装物配置单_明细")]
  public class Packaging_Detail : Base
  {

    public long Packaging_ID { get; set; }

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

    [LogicName("存货属性")]
    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(ButcheryGoods), "GoodsProperty_ID")]
    public long? GoodsProperty_ID { get; set; }

    [LogicName("存货属性")]
    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(ButcheryGoods), "GoodsProperty_Name")]
    public string GoodsProperty_Name { get; set; }

  }

  [Serializable]
  public class Packaging_DetailCollection : DmoCollection<Packaging_Detail>
  {
  }
}
