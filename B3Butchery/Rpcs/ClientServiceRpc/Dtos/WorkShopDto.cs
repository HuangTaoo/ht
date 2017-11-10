using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc.Dtos
{
    public class WorkShopDto
    {
        public long? AccountingUnit_ID { get; set; }
        public long? Department_ID { get; set; }
        public DateTime Time { get; set; }
        public string Code { get; set; }//叉车板上的码
        public List<WorkShopRecordDto> Details { get; set; }
    }

    public class WorkShopRecordDto
    {
        public string Code { get; set; }// 箱子上的码
        public string ChaCarBarCode { get; set; }
        public long? Goods_ID { get; set; }
        public string Goods_Name { get; set; }
        public decimal? Number { get; set; }
        public decimal? SecondNumber { get; set; }
        public decimal? SecondNumber2 { get; set; }
        public string CalculateSpec_Name { get; set; }

        public string PlanNumber { get; set; }


    }
}
