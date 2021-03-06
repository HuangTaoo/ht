﻿using System;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BL;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BL;
using Forks.EnterpriseServices.BusinessInterfaces;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices;
using System.Linq;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(ProductInStoreBL))]
  [LogicName("成品入库")]
  public interface IProductInStoreBL : IDepartmentWorkFlowBillBL<ProductInStore>
  { }

  public class ProductInStoreBL : DepartmentWorkFlowBillBL<ProductInStore>, IProductInStoreBL
  {
    protected override void beforeSave(ProductInStore dmo) {
      foreach (var detail in dmo.Details) { 
        if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅) {
          detail.SecondNumber = detail.Number / (detail.Goods_MainUnitRatio ?? 1) * (detail.Goods_SecondUnitRatio ?? 1);
        } 
        detail.Money = detail.Number*detail.Price;
      }
      dmo.Money = dmo.Details.Sum(x => (x.Money ?? 0).Value);
      base.beforeSave(dmo);
    }

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
          CargoSpace_ID = detail.CargoSpace_ID,
          Number = detail.Number ?? 0,
          SecondNumber = detail.SecondNumber,
          Price = detail.Price,
        };
        resultDetail.BrandItem_ID = detail.BrandItem_ID;
        resultDetail.BrandItem_Name = detail.BrandItem_Name;
        resultBill.Details.Add(resultDetail);
      }
      return resultBill;
    }
  }
}
