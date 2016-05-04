using System;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BL;
using BWP.B3UnitedInfos.BL;
using Forks.EnterpriseServices.BusinessInterfaces;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductInStoreBL))]
  [LogicName("成品入库")]
  public interface IProductInStoreBL : IDepartmentWorkFlowBillBL<ProductInStore>
  { }

  public class ProductInStoreBL : DepartmentWorkFlowBillBL<ProductInStore>, IProductInStoreBL
  {
    protected override void doCheck(ProductInStore dmo)
    {
      base.doCheck(dmo);
      UnitedInfoUtil.InsertInOutStoreBill(Session, CreateInOutStoreBill(dmo));
    }

    protected override void doUnCheck(ProductInStore dmo)
    {
      base.doUnCheck(dmo);
      UnitedInfoUtil.CancelInOutStoreBill(Session, BWP.B3UnitedInfos.BO.InOutStoreBill.CreateBillKey(mDmoTypeID.Value, dmo.ID));
    }

    private InOutStoreBill CreateInOutStoreBill(ProductInStore dmo)
    {
      var resultBill = new BWP.B3UnitedInfos.BO.InOutStoreBill()
      {
        Domain_ID = dmo.Domain_ID.Value,
        AccountingUnit_ID = dmo.AccountingUnit_ID.Value,
        InStore = true,
        Time = dmo.InStoreDate ?? DateTime.Now,
        BillKey = BWP.B3UnitedInfos.BO.InOutStoreBill.CreateBillKey(mDmoTypeID.Value, dmo.ID),
        SourceBillID = dmo.ID,
        SourceBillTypeID = mDmoTypeID.Value
      };
      resultBill.FromTo = string.Format("{0}→{1}", dmo.Department_Name, dmo.Store_Name);
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
          Price = detail.Price,
        };

        resultBill.Details.Add(resultDetail);
      }
      return resultBill;
    }
  }
}
