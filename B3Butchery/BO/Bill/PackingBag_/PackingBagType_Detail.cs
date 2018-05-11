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
     [Serializable, DFClass, LogicName("包材领用配置单_明细")]
    public class PackingBagType_Detail:Base
    {
        public long PackingBagType_ID { get; set; }

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


      [LogicName("包装袋")]
      public long? GoodsPacking_ID { get; set; }

      [ReferenceTo(typeof(ButcheryGoods), "Name")]
      [Join("GoodsPacking_ID", "ID")]
      [DFPrompt("包装袋")]
      public string GoodsPacking_Name { get; set; }

        [LogicName("生产班组")]
        public long? ProductShift_ID { get; set; }


        [ReferenceTo(typeof(ButcheryGoods), "Name")]
        [Join("ProductShift_ID", "ID")]
        [LogicName("生产班组")]
        public string ProductShift_Name { get; set; }

        [LogicName("部门简称")]
        public string Abbreviation { get; set; }

        [LogicName("标准袋数")]
    public int? StandNumber { get; set; }


  }

     [Serializable]
     public class PackingBagType_DetailCollection : DmoCollection<PackingBagType_Detail>
     {
     }
}
