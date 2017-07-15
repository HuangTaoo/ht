using System;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [LogicName("会计单位_仓库_存货")]
  [DFClass, Serializable]
  public class Goods_Accounting_Store : Base
  {
    [LogicName("会计单位")]
    public long? AccountingUnit_ID { get; set; }

    [LogicName("仓库")]
    public long? Store_ID { get; set; }

    [LogicName("存货")]
    public long? Goods_ID { get; set; }

    [ReferenceTo(typeof(AccountingUnit), "Name")]
    [Join("AccountingUnit_ID", "ID")]
    [LogicName("会计单位")]
    public string AccountingUnit_Name { get; set; }

    [ReferenceTo(typeof(AccountingUnit), "Domain_ID")]
    [Join("AccountingUnit_ID", "ID")]
    [LogicName("会计单位版块")]
    public long? AccountingUnit_Domain_ID { get; set; }

    [ReferenceTo(typeof(Store), "Name")]
    [Join("Store_ID", "ID")]
    [LogicName("仓库")]
    public string Store_Name { get; set; }

    [ReferenceTo(typeof(Goods), "Name")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货名称")]
    public string Goods_Name { get; set; }

    [ReferenceTo(typeof(Goods), "Code")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货编码")]
    public string Goods_Code { get; set; }

    [ReferenceTo(typeof(Goods), "Spec")]
    [Join("Goods_ID", "ID")]
    [LogicName("规格")]
    public string Goods_Spec { get; set; }

    [ReferenceTo(typeof(Goods), "MainUnit")]
    [Join("Goods_ID", "ID")]
    [LogicName("主单位")]
    public string Goods_MainUnit { get; set; }

    [ReferenceTo(typeof(Goods), "SecondUnit")]
    [Join("Goods_ID", "ID")]
    [LogicName("辅单位")]
    public string Goods_SecondUnit { get; set; }
  }
}
