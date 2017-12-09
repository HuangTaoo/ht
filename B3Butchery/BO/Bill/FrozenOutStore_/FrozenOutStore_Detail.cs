using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
    [LogicName("速冻出库明细")]
    [Serializable]
    [DFClass]
    public class FrozenOutStore_Detail : GoodsDetail
    {

        public long FrozenOutStore_ID { get; set; }

        [LogicName("计划号")]
        public long? PlanNumber_ID { get; set; }

        [LogicName("计划号")]
        [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
        [Join("PlanNumber_ID", "ID")]
        public string PlanNumber_Name { get; set; }
        [LogicName("成品")]
        public long? Goods2_ID { get; set; }
        [LogicName("成品")]
        [ReferenceTo(typeof(Goods), "Name")]
        [Join("Goods2_ID", "ID")]
        public string Goods2_Name { get; set; }

        [LogicName("成品主单位")]
        [ReferenceTo(typeof(Goods), "MainUnit")]
        [Join("Goods2_ID", "ID")]
        public string Goods2_MainUnit { get; set; }

        [LogicName("成品单位")]
        [ReferenceTo(typeof(Goods), "SecondUnit")]
        [Join("Goods2_ID", "ID")]
        public string Goods2_SecondUnit { get; set; }




        //当前存货 ---半成品
        [LogicName("辅数量Ⅱ")]
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
    }

    [Serializable]
    public class FrozenOutStore_DetailCollection : DmoCollection<FrozenOutStore_Detail>
    {
    }
}
