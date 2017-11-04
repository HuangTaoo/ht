using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc.Dtos
{

  public class OutPutDto
  {
    public OutPutDto()
    {
      Details=new List<OutPut_DetailDto>();
    }
    public long? AccountingUnit_ID { get; set; }
    public long? Department_ID { get; set; }
    public DateTime Time { get; set; }

    public List<OutPut_DetailDto> Details { get; set; }
  }

  public class OutPut_DetailDto
  {
    public long? Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public decimal? Number { get; set; }
    public decimal? SecondNumber { get; set; }
    public decimal? SecondNumber2 { get; set; }

    public string CalculateSpec_Name { get; set; }
  }
}
