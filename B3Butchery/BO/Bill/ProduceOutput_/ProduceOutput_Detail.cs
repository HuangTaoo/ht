﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("产出单明细")]

  public class ProduceOutput_Detail : GoodsDetail
  {
    public long ProduceOutput_ID { get; set; }

    [LogicName("辅单位数量Ⅱ")]
    public Money<decimal>? SecondNumber2 { get; set; }

    [LogicName("辅单位Ⅱ")]
    [ReferenceTo(typeof(Goods), "SecondUnit")]
    [Join("Goods_ID", "ID")]
    public string Goods_SecondUnit2 { get; set; }
  }
  [Serializable]
  public class ProduceOutput_DetailCollection : DmoCollection<ProduceOutput_Detail>
  { }
}
