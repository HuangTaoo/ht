using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BL
{

  [BusinessInterface(typeof(PackingRecipientsBL))]
  [LogicName("包装领用")]
  public interface IPackingRecipientsBL : IDepartmentWorkFlowBillBL<PackingRecipients>
  {

  }

  public class PackingRecipientsBL : DepartmentWorkFlowBillBL<PackingRecipients>, IPackingRecipientsBL
  {
    protected override void doCheck(PackingRecipients dmo)
    {
      base.doCheck(dmo);
      UnitedInfoUtil.InsertInOutStoreBill(Session, CreateInOutStoreBill(dmo));
    }

    private InOutStoreBill CreateInOutStoreBill(PackingRecipients dmo)
    {
      var resultBill = new BWP.B3UnitedInfos.BO.InOutStoreBill()
      {
        Domain_ID = dmo.Domain_ID.Value,
        AccountingUnit_ID = dmo.AccountingUnit_ID.Value,
        InStore = false,
        Time = dmo.Date ?? DateTime.Now,
        BillKey = BWP.B3UnitedInfos.BO.InOutStoreBill.CreateBillKey(mDmoTypeID.Value, dmo.ID),
        SourceBillID = dmo.ID,
        SourceBillTypeID = mDmoTypeID.Value
      };
      //      resultBill.FromTo = string.Format("{0}→{1}", dmo.Department_Name, dmo.Store_Name);
      foreach (var detail in dmo.Details)
      {
        if ((detail.Number ?? 0) == 0)
          continue;
        var resultDetail = new BWP.B3UnitedInfos.BO.InOutStoreBill_Detail()
        {
          Store_ID = dmo.Store_ID ?? 0,
          Goods_ID = detail.Goods_ID,
          GoodsBatch_ID = detail.GoodsBatch_ID,
          Number = detail.Number ?? 0,
          SecondNumber = detail.SecondNumber,
//          Price = detail.Price,
//          Money=detail.Money.EToDecimal(),
        };

        resultBill.Details.Add(resultDetail);
      }
      return resultBill;
    }

    protected override void doUnCheck(PackingRecipients dmo)
    {
      base.doUnCheck(dmo);
      UnitedInfoUtil.CancelInOutStoreBill(Session, BWP.B3UnitedInfos.BO.InOutStoreBill.CreateBillKey(mDmoTypeID.Value, dmo.ID));
    }
  }
}
