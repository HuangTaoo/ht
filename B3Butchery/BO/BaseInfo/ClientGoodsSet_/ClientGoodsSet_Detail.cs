using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using BWP.B3Butchery.Attributes;
using Newtonsoft.Json;

namespace BWP.B3Butchery.BO
{
  [Serializable]
  public class ClientGoodsSet_Detail:Base
  {
    public long ClientGoodsSet_ID { get; set; }


    [LogicName("存货")]
    public long Goods_ID { get; set; }


      
      [LogicName("存货属性")]
      [Join("Goods_ID", "ID")]
      [ReferenceTo(typeof(Goods), "GoodsProperty_ID")]
    public long? GoodsProperty_ID { get; set; }

          [LogicName("存货属性")]
      [Join("Goods_ID", "ID")]
      [ReferenceTo(typeof(Goods), "GoodsProperty_Name")]
      public string GoodsProperty_Name { get; set; }

          

    [LogicName("存货")]
    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(Goods), "Name")]
    public string Goods_Name { get; set; }

    [LogicName("存货编码")]
    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(Goods), "Code")]
    public string Goods_Code { get; set; }

    [LogicName("存货规格")]
    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(Goods), "Spec")]
    public string Goods_Spec { get; set; }


    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(Goods), "Spell")]
    public string Goods_Spell { get; set; }

    [Join("Goods_ID", "ID")]
    [ReferenceTo(typeof(Goods), "MainUnitRatio")]
    [JsonConverter(typeof(MoneyJsonConverter))]  
    public Money<decimal>? Goods_MainUnitRatio { get; set; }//主副转换关系 主比例

      [Join("Goods_ID", "ID")]
      [ReferenceTo(typeof(Goods), "SecondUnitII_MainUnitRatio")]
      [JsonConverter(typeof(MoneyJsonConverter))]
    public Money<decimal>? Goods_SecondUnitII_MainUnitRatio { get; set; }//主副转换关系 主比例
      

      [Join("Goods_ID", "ID")]
      [ReferenceTo(typeof(Goods), "StandardSecondNumber")]
      [JsonConverter(typeof(MoneyJsonConverter))]
      public Money<decimal>? Goods_StandardSecondNumber { get; set; }//仙坛客户端中标准箱数

      





      //当数据源为生产通知单时 设置
    public DateTime? CompletedDate { get; set; }

  }

  [Serializable]
  public class ClientGoodsSet_DetailCollection : DmoCollection<ClientGoodsSet_Detail>
  {
  }
}
