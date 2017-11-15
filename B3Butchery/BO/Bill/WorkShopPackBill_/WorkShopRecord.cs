using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{

    [Serializable]

    [LogicName("车间包装记录")]
    public class WorkShopRecord : GoodsDetail
    {
        public long WorkShopPackBill_ID { get; set; }

        [LogicName("辅单位数量II")]
        public Money<decimal>? SecondNumber2 { get; set; }


        #region 仙坛客户端 添加数据

        [LogicName("计划号")]
        public long? PlanNumber_ID { get; set; }

        [LogicName("计划号")]
        [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
        [Join("PlanNumber_ID", "ID")]
        public string PlanNumber_Name { get; set; }


        [LogicName("辅单位Ⅱ")]
        [ReferenceTo(typeof(Goods), "SecondUnitII")]
        [Join("Goods_ID", "ID")]
        public string Goods_SecondUnitII { get; set; }

        #endregion

        public string ChaCarBoardCode { get; set; }//叉车板的条码

        public string BarCode { get; set; }//箱子上的条码

    }

    [Serializable]
    public class WorkShopRecordCollection : DmoCollection<WorkShopRecord>
    {

    }
}
