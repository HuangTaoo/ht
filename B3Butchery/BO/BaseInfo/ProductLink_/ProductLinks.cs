using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BO;
using TSingSoft.WebControls2;
using BWP.B3Butchery.Utils;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
  [Serializable, DFClass]
  [LogicName("生产环节")]

  public class ProductLinks : DomainBaseInfo
  {
    [LogicName("生产线")]
    [DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
    [DFDataKind(B3ButcheryDataSource.生产线)]
    [DFExtProperty("DisplayField", "ProductLine_Name")]
    public long? ProductLine_ID { get; set; }

    [ReferenceTo(typeof(ProductLine), "Name")]
    [Join("ProductLine_ID", "ID")]
    [LogicName("生产线")]
    public string ProductLine_Name { get; set; }

    [LogicName("负责人")]
    public long? ChargePerson_ID { get; set; }

    [Join("ChargePerson_ID", "ID")]
    [LogicName("负责人")]
    [ReferenceTo(typeof(Employee), "Name")]
    public string ChargePerson_Name { get; set; }


    private ProductLinks_InputDetailCollection mInputDetails = new ProductLinks_InputDetailCollection();
    [OneToMany(typeof(ProductLinks_InputDetail),"ID")]
    [Join("ID", "ProductLinks_ID")]
    public ProductLinks_InputDetailCollection InputDetails
    {
      get { return mInputDetails; }
      set { mInputDetails = value; }
    }

    private ProductLinks_OutputDetailCollection mOutputDetails = new ProductLinks_OutputDetailCollection();
    [OneToMany(typeof(ProductLinks_OutputDetail),"ID")]
    [Join("ID", "ProductLinks_ID")]
    public ProductLinks_OutputDetailCollection OutputDetails
    {
      get { return mOutputDetails; }
      set { mOutputDetails = value; }
    }
  }
}
