using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO.BaseInfo
{
    [Serializable, LogicName("交接记录")]
    public class HandoverRecord : Base
    {
        /// <summary>
        /// 单据创建人用户名
        /// </summary>
        public string CreateUser_Name { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool DeleteState { get; set; }

        [LogicName("计数名称")]
        public long? CalculateGoods_ID { get; set; }
        [LogicName("计划号")]
        public string PlanNumber { get; set; }

        [LogicName("计数名称")]
        public string CalculateGoods_Name { get; set; }

        [LogicName("计数规格")]
        public string CalculateSpec_Name { get; set; }

        [LogicName("盘数")]//辅数量
        public decimal? InputNumber { get; set; }

        [LogicName("主辅换算主单位比例")]
        public decimal? MainUnitRatio { get; set; }

        [LogicName("主辅II换算主单位比例")]
        public decimal? SecondUnitII_MainUnitRatio { get; set; }


        [LogicName("辅单位II数量")]
        public decimal? SecondIINumber { get; set; }

        //主数量=辅数量ii* 主辅ii换算主单位比例

        //辅数量=主数量/主辅换算主单位比例

        [LogicName("重量")]//主数量
        public decimal? Weight
        {
            get;
            set;
        }

        [LogicName("显示名称")]
        public string DisplayName
        {
            get;
            set;
        }

        [LogicName("是否辅单位Ⅱ")]
        public bool? IsSecondⅡ { get; set; }


        [LogicName("存货ID")]
        public long? Goods_ID { get; set; }

        [LogicName("标识")]//记录哪台电脑
        public string BiaoShi { get; set; }

    }
}
