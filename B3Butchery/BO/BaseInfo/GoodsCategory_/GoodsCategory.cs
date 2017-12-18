using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("存货类别")]
  public class GoodsCategory : DomainBaseInfo
  {
    [LogicName("所属类别分类")]
    [DFDataKind(B3ButcheryDataSource.类别分类)]
    [DFExtProperty("DisplayField", "CategoryClassification_Name")]
    [DFExtProperty(B3ButcheryDataSource.速冻库, B3ButcheryDataSource.类别分类)]
    public long? CategoryClassification_ID { get; set; }

    [LogicName("所属类别分类")]
    [ReferenceTo(typeof(CategoryClassification), "Name")]
    [Join("CategoryClassification_ID", "ID")]
    public string CategoryClassification_Name { get; set; }
  }
}
