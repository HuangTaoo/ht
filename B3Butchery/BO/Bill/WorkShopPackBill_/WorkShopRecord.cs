using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{


    [LogicName("车间包装记录")]
    public class WorkShopRecord : GoodsDetail
    {
        public long WorkShopPackBill_ID { get; set; }




        #region 仙坛客户端 添加数据

        //[LogicName("计数名称")]
        //public long? CalculateGoods_ID { get; set; }

        //[LogicName("计数名称")]
        //[ReferenceTo(typeof(CalculateGoods), "Name")]
        //[Join("CalculateGoods_ID", "ID")]
        //public string CalculateGoods_Name { get; set; }
        [LogicName("计划号")]
        public long? PlanNumber_ID { get; set; }

        [LogicName("计划号")]
        [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
        [Join("PlanNumber_ID", "ID")]
        public string PlanNumber_Name { get; set; }

        #endregion

        public string ChaCarBoardCode { get; set; }//叉车板的条码

        public string BarCode { get; set; }//箱子上的条码

    }

    [Serializable]
    public class WorkShopRecordCollection : DmoCollection<WorkShopRecord>
    {

    }
}
