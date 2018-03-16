using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.AvailableStroage;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.AvailableStroage
{
  public class ProductInStoreAvailableStorage : IAvailableStroage
  {
    public DQueryDom CreatePartialQuery(AvailableStorageContext context)
    {
      var detail = new JoinAlias(typeof(ProductInStore_Detail));
      var bill = new JoinAlias(typeof(ProductInStore));
      var query = new DQueryDom(detail);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(bill), DQCondition.EQ(bill, "ID", detail, "ProductInStore_ID"));
      query.Where.Conditions.Add(DQCondition.EQ(bill, "BillState", 单据状态.未审核));
      if (context.Goods_ID.HasValue)
      {
        query.Where.Conditions.Add(DQCondition.EQ("Goods_ID", context.Goods_ID));
      }
      if (context.Store_ID.HasValue)
      {
        query.Where.Conditions.Add(DQCondition.EQ(bill, "Store_ID", context.Store_ID));
      }
      if (context.AccountingUnit_ID.HasValue)
      {
        query.Where.Conditions.Add(DQCondition.EQ(bill, "AccountingUnit_ID", context.AccountingUnit_ID));
      }
      if (context.Date.HasValue)
      {
        query.Where.Conditions.Add(DQCondition.LessThan(bill, "Time", context.Date.Value.Date.AddDays(1)));
      }

      query.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));

      query.Columns.Add(DQSelectColumn.Create(DQExpression.ConstValue("成品入库单"), "Source"));
      query.Columns.Add(DQSelectColumn.Field("Store_ID", bill));
      query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Multiply(DQExpression.Field("Number"), DQExpression.ConstValue(-1)), "Number"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Multiply(DQExpression.Field("SecondNumber"), DQExpression.ConstValue(-1)), "SecondNumber"));
      query.Columns.Add(DQSelectColumn.Field("GoodsBatch_ID"));
      return query;
    }

    public string Name
    {
      get { return "成品入库单"; }
    }
  }
}
