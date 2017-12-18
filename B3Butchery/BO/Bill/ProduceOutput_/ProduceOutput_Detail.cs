using System;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using BWP.B3Butchery.Utils;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass, LogicName("产出单明细")]

  public class ProduceOutput_Detail : GoodsDetail
  {


      #region 仙坛客户端 添加数据

      [LogicName("计数名称")]
      public long? CalculateGoods_ID { get; set; }

      [LogicName("计数名称")]
      [ReferenceTo(typeof(CalculateGoods), "Name")]
      [Join("CalculateGoods_ID", "ID")]
      public string CalculateGoods_Name { get; set; }
      [LogicName("计划号")]
      public long? PlanNumber_ID { get; set; }

      [LogicName("计划号")]
      [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
      [Join("PlanNumber_ID", "ID")]
      public string PlanNumber_Name { get; set; }

        [LogicName("包装模式")]
        [ReferenceTo(typeof(ButcheryGoods), "PackageModel")]
        [Join("Goods_ID", "ID")]
        public NamedValue<包装模式>? Goods_PackageModel { get; set; }

        [LogicName("生产班组")]
        [ReferenceTo(typeof(ButcheryGoods), "ProductShift_ID")]
        [Join("Goods_ID", "ID")]
        public long? Goods_ProductShift_ID { get; set; }

        [LogicName("存货类别")]
        [ReferenceTo(typeof(ButcheryGoods), "GoodsCategory_ID")]
        [Join("Goods_ID", "ID")]
        public long? GoodsCategory_ID { get; set; }
        
        #endregion



        [LogicName("内包装比例")]
    [ReferenceTo(typeof(Goods), "InnerPackingPer")]
    [Join("Goods_ID", "ID")]
    public Money<decimal>? Goods_InnerPackingPer { get; set; }

    public long ProduceOutput_ID { get; set; }

    [LogicName("辅单位数量Ⅱ")]
    public Money<decimal>? SecondNumber2 { get; set; }

    [LogicName("辅单位Ⅱ")]
    [ReferenceTo(typeof(Goods), "SecondUnitII")]
    [Join("Goods_ID", "ID")]
    public string Goods_SecondUnit2 { get; set; }

    [LogicName("主辅II换算主单位比例")]
    [ReferenceTo(typeof(Goods), "SecondUnitII_MainUnitRatio")]
    [Join("Goods_ID", "ID")]
    public Money<decimal>? Goods_SecondUnitII_MainUnitRatio { get; set; }

    [LogicName("主辅II换算辅单位比例")]
    [ReferenceTo(typeof(Goods), "SecondUnitII_SecondUnitRatio")]
    [Join("Goods_ID", "ID")]
    public Money<decimal>? Goods_SecondUnitII_SecondUnitRatio { get; set; }

      public int? RecordCount  { get; set; }

      [LogicName("计数分类")]

      public long? CalculateCatalog_ID { get; set; }

      [LogicName("计数分类")]
      [ReferenceTo(typeof(CalculateCatalog), "Name")]
      [Join("CalculateCatalog_ID", "ID")]
      public string CalculateCatalog_Name { get; set; }
  }
  [Serializable]
  public class ProduceOutput_DetailCollection : DmoCollection<ProduceOutput_Detail>
  { }
}
