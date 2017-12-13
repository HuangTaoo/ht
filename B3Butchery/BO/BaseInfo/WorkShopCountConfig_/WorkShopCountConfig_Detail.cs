using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{
    [LogicName("车间计数配置单明细")]
    [Serializable]
    [DFClass]
    public class WorkShopCountConfig_Detail : GoodsDetailSummaryBase
    {


        public long WorkShopCountConfig_ID { get; set; }

        [LogicName("默认盘数")]
        public int? DefaultNumber1 { get; set; }


        [LogicName("辅单位Ⅱ")]
        [ReferenceTo(typeof(Goods), "SecondUnitII")]
        [Join("Goods_ID", "ID")]
        public string Goods_SecondUnitII { get; set; }

        [LogicName("主辅II换算主单位比例")]
        [ReferenceTo(typeof(Goods), "SecondUnitII_MainUnitRatio")]
        [Join("Goods_ID", "ID")]
        public Money<decimal>? Goods_SecondUnitII_MainUnitRatio { get; set; }

        [LogicName("主辅II换算辅单位比例")]
        [ReferenceTo(typeof(Goods), "SecondUnitII_SecondUnitRatio")]
        [Join("Goods_ID", "ID")]
        public Money<decimal>? Goods_SecondUnitII_SecondUnitRatio { get; set; }


        [LogicName("存货属性")]
        [ReferenceTo(typeof(Goods), "GoodsProperty_ID")]
        [Join("Goods_ID", "ID")]
        public long? GoodsProperty_ID { get; set; }

        [LogicName("存货属性")]
        [ReferenceTo(typeof(Goods), "GoodsProperty_Name")]
        [Join("Goods_ID", "ID")]
        public string GoodsProperty_Name { get; set; }
    }

    [Serializable]
    public class WorkShopCountConfig_DetailCollection : DmoCollection<WorkShopCountConfig_Detail>
    {
    }
}
