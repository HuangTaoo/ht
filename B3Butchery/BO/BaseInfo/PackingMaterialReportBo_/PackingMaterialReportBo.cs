using BWP.B3Butchery.Utils;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO.BaseInfo
{

  [LogicName("班组测算打印")]
  [DFClass]
  public class PackingMaterialReportBo
  {
    //sb.Append("<td >存货名称</td>");
    //  sb.Append("<td > 计划号</td>");
    //  sb.Append("<td >计数规格</td>");
    //  sb.Append("<td >盘数</td>");
    //  sb.Append("<td >袋数</td>");
    //  sb.Append("<td >重量</td>");
    //  sb.Append("<td >包装模式</td>");


      [LogicName("存货名称")]
    public string Goods_Name { get; set; }

    [LogicName("计划号")]
    public string PlanNumber { get; set; }


    [LogicName("计数规格")]
    public string Goods_Spec { get; set; }


    [LogicName("盘数")]
    public Money<decimal>? SecondNumber { get; set; }

    [LogicName("袋数")]
    public Money<decimal>? SecondNumber2 { get; set; }

    [LogicName("重量")]
    public Money<decimal>? Number { get; set; }
    [LogicName("包装模式")]
    public NamedValue<包装模式>? PackageModel { get; set; }
    
  }
}
