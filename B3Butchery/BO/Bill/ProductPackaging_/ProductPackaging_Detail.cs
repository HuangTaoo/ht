﻿using System;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [DFClass]
  [Serializable]
  [LogicName("成品包装配置")]
  public class ProductPackaging_Detail : Base
  {
    public long? ProductPackaging_ID { get; set; }

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

    [LogicName("内包装名称")]
    public long? NeiGoods_ID { get; set; }

    [LogicName("内包装名称")]
    [ReferenceTo(typeof(Goods), "Name")]
    [Join("NeiGoods_ID", "ID")]
    public string NeiGoods_Name { get; set; }

    [LogicName("内包装编码")]
    [ReferenceTo(typeof(Goods), "Code")]
    [Join("NeiGoods_ID", "ID")]
    public string NeiGoods_Code { get; set; }

    [LogicName("内包装主辅换算主单位比例")]
    [ReferenceTo(typeof(Goods), "MainUnitRatio")]
    [Join("NeiGoods_ID", "ID")]
    public Money<decimal>? NeiGoods_MainUnitRatio { get; set; }

    [LogicName("内包装比例")]
    public Money<decimal>? NeiGoodsRatio { get; set; }

    [LogicName("外包装名称")]
    public long? WaiGoods_ID { get; set; }

    [LogicName("外包装名称")]
    [ReferenceTo(typeof(Goods), "Name")]
    [Join("WaiGoods_ID", "ID")]
    public string WaiGoods_Name { get; set; }

    [LogicName("外包装编码")]
    [ReferenceTo(typeof(Goods), "Code")]
    [Join("WaiGoods_ID", "ID")]
    public string WaiGoods_Code { get; set; }

    [LogicName("外包装比例")]
    public Money<decimal>? WaiGoodsRatio { get; set; }

    [LogicName("摘要")]
    [DbColumn(Length = 1000)]
    public string Remark { get; set; }
  }

  [Serializable]
  public class ProductPackaging_DetailCollection : DmoCollection<ProductPackaging_Detail>
  {

  }
}
