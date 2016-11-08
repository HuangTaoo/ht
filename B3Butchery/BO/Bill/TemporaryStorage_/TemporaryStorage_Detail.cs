using System;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("暂存单明细")]
  public class TemporaryStorage_Detail : Base
  {
    public long? TemporaryStorage_ID { get; set; }

    [LogicName("序号")]
    public int? SerialNumber { get; set; }

    [LogicName("存货")]
    public long? Goods_ID { get; set; }

    [ReferenceTo(typeof(Goods), "Name")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货名称")]
    public string Goods_Name { get; set; }

    [ReferenceTo(typeof(Goods), "Code")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货编号")]
    public string Goods_Code { get; set; }

    [LogicName("规格")]
    [ReferenceTo(typeof(Goods), "Spec")]
    [Join("Goods_ID", "ID")]
    public string Goods_Spec { get; set; }

    [LogicName("数量")]
    public Money<decimal>? Number { get; set; }

    [LogicName("主单位")]
    [ReferenceTo(typeof(Goods), "MainUnit")]
    [Join("Goods_ID", "ID")]
    public string Goods_MainUnit { get; set; }
  }

  [Serializable]
  public class TemporaryStorage_DetailCollection : DmoCollection<TemporaryStorage_Detail>
  { }
}
