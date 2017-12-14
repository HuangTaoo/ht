using BWP.B3Butchery.Attributes;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{
    [LogicName("车间计数配置单明细")]
    [Serializable]
    [DFClass]
    public class WorkShopCountConfig_Detail : Base
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
        [JsonConverter(typeof(MoneyJsonConverter))]
        public Money<decimal>? Goods_SecondUnitII_MainUnitRatio { get; set; }

        [LogicName("主辅II换算辅单位比例")]
        [ReferenceTo(typeof(Goods), "SecondUnitII_SecondUnitRatio")]
        [Join("Goods_ID", "ID")]
        [JsonConverter(typeof(MoneyJsonConverter))]
        public Money<decimal>? Goods_SecondUnitII_SecondUnitRatio { get; set; }


        [LogicName("存货属性")]
        [ReferenceTo(typeof(Goods), "GoodsProperty_ID")]
        [Join("Goods_ID", "ID")]
        public long? GoodsProperty_ID { get; set; }

        [LogicName("存货属性")]
        [ReferenceTo(typeof(Goods), "GoodsProperty_Name")]
        [Join("Goods_ID", "ID")]
        public string GoodsProperty_Name { get; set; }


        [Join("Goods_ID", "ID")]
        [LogicName("存货名称")]
        [ReferenceTo(typeof(Goods), "Name")]
        public string Goods_Name { get; set; }
        [Join("Goods_ID", "ID")]
        [LogicName("辅单位")]
        [ReferenceTo(typeof(Goods), "SecondUnit")]
        public string Goods_SecondUnit { get; set; }
        [Join("Goods_ID", "ID")]
        [LogicName("主单位")]
        [ReferenceTo(typeof(Goods), "MainUnit")]
        public string Goods_MainUnit { get; set; }
        [Join("Goods_ID", "ID")]
        [ReferenceTo(typeof(Goods), "SecondUnitRatio")]
        [JsonConverter(typeof(MoneyJsonConverter))]
        public Money<decimal>? Goods_SecondUnitRatio { get; set; }
        [Join("Goods_ID", "ID")]
        [ReferenceTo(typeof(Goods), "MainUnitRatio")]
        [JsonConverter(typeof(MoneyJsonConverter))]
        public Money<decimal>? Goods_MainUnitRatio { get; set; }



        [Join("Goods_ID", "ID")]
        [LogicName("存货编号")]
        [ReferenceTo(typeof(Goods), "Code")]
        public string Goods_Code { get; set; }
     
        [LogicName("备注")]
        public string Remark { get; set; }
  
        [LogicName("存货")]
        public long Goods_ID { get; set; }
        [Join("Goods_ID", "ID")]
        [LogicName("规格")]
        [ReferenceTo(typeof(Goods), "Spec")]
        public string Goods_Spec { get; set; }




    }

    [Serializable]
    public class WorkShopCountConfig_DetailCollection : DmoCollection<WorkShopCountConfig_Detail>
    {
    }
}
