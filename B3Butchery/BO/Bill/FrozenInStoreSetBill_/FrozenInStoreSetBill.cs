using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO
{
  [DFClass]
  [LogicName("速冻入库配置单")]
  [Serializable]
  public class FrozenInStoreSetBill : DepartmentWorkFlowBill
  {
    private DateTime? _date = BLContext.Today;
    [LogicName("日期")]
    [DFNotEmpty]
    public DateTime? Date
    {
      get { return _date; }
      set { _date = value; }
    }

    [LogicName("名称")]
    public string Name { get; set; }


    [DFDataKind(B3ButcheryDataSource.车间品类)]
    [DFExtProperty("DisplayField", "WorkshopCategory_Name")]
    [DFExtProperty(B3ButcheryDataSource.车间品类, B3ButcheryDataSource.车间品类)]
    [DFPrompt("车间品类")]
    [DFNotEmpty]
    public long? WorkshopCategory_ID { get; set; }


    [LogicName("车间品类")]
    [ReferenceTo(typeof(WorkshopCategory), "Name")]
    [Join("WorkshopCategory_ID", "ID")]
    public string WorkshopCategory_Name { get; set; }


    private readonly FrozenInStoreSetBill_DetailCollection _details = new FrozenInStoreSetBill_DetailCollection();

    [OneToMany(typeof(FrozenInStoreSetBill_Detail), "ID")]
    [Join("ID", "FrozenInStoreSetBill_ID")]
    public FrozenInStoreSetBill_DetailCollection Details
    {
      get { return _details; }
    }
  }
}
