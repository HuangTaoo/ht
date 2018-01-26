using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BL
{


  [BusinessInterface(typeof(WorkShopPackBillBL))]
  [LogicName("车间包装")]
  public interface IWorkShopPackBillBL : IDepartmentWorkFlowBillBL<WorkShopPackBill>
  { }

  public class WorkShopPackBillBL : DepartmentWorkFlowBillBL<WorkShopPackBill>, IWorkShopPackBillBL
  {

    //当审核的时候生成单据 速冻出库单
    protected override void doCheck(WorkShopPackBill dmo)
    {
      base.doCheck(dmo);

      CreateFrozenOutStore(dmo);


    }

    private void CreateFrozenOutStore(WorkShopPackBill dmo)
    {
      var bl = BIFactory.Create<IFrozenOutStoreBL>(Session);
      var bo = new FrozenOutStore();
      bl.InitNewDmo(bo);
      bo.AccountingUnit_ID = dmo.AccountingUnit_ID;
      bo.Department_ID = dmo.Department_ID;
      bo.WorkBill_ID = dmo.ID;
      bo.Date = dmo.Date;
      var group = dmo.Details.GroupBy(x => new { x.PlanNumber_ID, x.Goods_ID });
      foreach (var one in group)
      {
        var de = new FrozenOutStore_Detail();
        de.Goods2_ID = one.Key.Goods_ID;
        de.Goods_ID = GetBanChengPinByGoodsID(one.Key.Goods_ID) ?? 0;
        DmoUtil.RefreshDependency(de, "Goods_ID", "Goods2_ID");
        de.PlanNumber_ID = one.Key.PlanNumber_ID;
        de.Number = one.Sum(x => (x.Number ?? 0).Value);
        if (de.Goods_SecondUnitII_MainUnitRatio != null && de.Goods_SecondUnitII_MainUnitRatio != 0)
        {
          de.SecondNumber2 = de.Number / de.Goods_SecondUnitII_MainUnitRatio * de.Goods_SecondUnitII_SecondUnitRatio;
        }
        if (de.Goods_MainUnitRatio != null && de.Goods_MainUnitRatio != 0)
        {
          de.SecondNumber = de.Number / de.Goods_MainUnitRatio * de.Goods_SecondUnitRatio;
        }



        bo.Details.Add(de);
      }
      bl.Insert(bo);


    }


    private long? GetBanChengPinByGoodsID(long goodID)
    {
      var main = new JoinAlias(typeof(ChengPinToBanChengPinConfig));
      var query = new DQueryDom(main);
      query.Columns.Add(DQSelectColumn.Field("Goods2_ID", main));
      query.Where.Conditions.Add(DQCondition.EQ(main, "Goods_ID", goodID));
      return query.EExecuteScalar<long?>(Session);

    }

    protected override void doUnCheck(WorkShopPackBill dmo)
    {
      base.doUnCheck(dmo);

      //撤销时删除当前单据

      DeleteFrozenOutStore(dmo);


    }

    private void DeleteFrozenOutStore(WorkShopPackBill dmo)
    {
      var bl = BIFactory.Create<IFrozenOutStoreBL>(Session);
      var id = GetFrozenId(dmo.ID);

      var bo = bl.Load(id ?? 0);
      if (bo != null)
      {

        if (bo.BillState == 单据状态.已审核)
        {
          throw new Exception("关联单据" + id + "已审核");
        }
        else if (bo.BillState == 单据状态.未审核)
        {
          bl.Delete(bo);
        }
      }




    }




    private long? GetFrozenId(long id)
    {
      var main = new JoinAlias(typeof(FrozenOutStore));
      var query = new DQueryDom(main);
      query.Columns.Add(DQSelectColumn.Field("ID", main));
      query.Where.Conditions.Add(DQCondition.EQ(main, "WorkBill_ID", id));
      long? froId = null;

      using (var reader = Session.ExecuteReader(query))
      {
        if (reader.Read())
        {
          froId = (long)reader[0];

        }
      }
      return froId;

    }




  }
}
