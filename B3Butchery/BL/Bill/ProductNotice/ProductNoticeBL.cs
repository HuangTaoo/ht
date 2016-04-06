using System;
using System.Collections.Generic;
using System.Linq;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BL;
using BWP.B3Frameworks.BO.MoneyTemplate;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BL {
  [BusinessInterface(typeof(ProductNoticeBL))]
  [LogicName("生产通知单")]
  public interface IProductNoticeBL : IDepartmentWorkFlowBillBL<ProductNotice> {
      //载入明细
      void LoadPredictDetail(ProductNotice dmo);
  }

  public class ProductNoticeBL : DepartmentWorkFlowBillBL<ProductNotice>, IProductNoticeBL {
      public void CheckDetailGoods(ProductNotice dmo)
      {
          if (dmo.Customer_ID == null) return;
          var query = SaleForecastQuery(dmo.Customer_ID, dmo.Date);
          query.Columns.Clear();
          query.Columns.Add(DQSelectColumn.Field("SaleGoods_ID"));
          var result = query.EExecuteList<long>();
          foreach (var detail in dmo.Details.Where(detail => detail.DmoID != null && !result.Contains(detail.Goods_ID)))
          {
              throw new Exception(detail.Goods_Name + "和销售预报中不相符");
          }
      }

      public void LoadPredictDetail(ProductNotice dmo)
      {
          if (dmo.Customer_ID == null)
              throw new Exception("请先选择客户");
          if (dmo.Date == null)
          {
              throw new ArgumentException("请先选择生产日期");
          }
          var goods = CheckCustomer(dmo.Customer_ID);
          var query = SaleForecastQuery(dmo.Customer_ID, dmo.Date);
          using (var reader = Session.ExecuteReader(query))
          {
              while (reader.Read())
              {
                  if(goods!=null&& goods.ContainsKey((long) reader[1])&&goods[(long) reader[1]]==(long) reader[0])
                      continue;
                  var selectDmo = new ProductNotice_Detail();
                  selectDmo.DmoID = (long) reader[0];
                  selectDmo.Goods_ID = (long) reader[1];
                  selectDmo.Number = (Money<decimal>?) reader[2];
                  selectDmo.Goods_MainUnit = reader[3] + "";
                  selectDmo.Price = (Money<decimal>?) reader[4];
                  selectDmo.Money = (Money<金额>?) reader[5];
                  selectDmo.SecondNumber = (Money<decimal>?) reader[6];
                  selectDmo.Goods_SecondUnit = reader[7] + "";
                  selectDmo.Remark = reader[8] + "";
                  selectDmo.Goods_Code = reader[9] + "";
                  selectDmo.Goods_Name = reader[10] + "";
                  selectDmo.Goods_Spec = reader[11] + "";
                  dmo.Details.Add(selectDmo);
              }
          }
      }

      public Dictionary<long, long?> CheckCustomer(long? customer)
      {
          var dic = new Dictionary<long, long?>();
          var product = new JoinAlias(typeof (ProductNotice));
          var productDetail = new JoinAlias(typeof (ProductNotice_Detail));
          var dom = new DQueryDom(productDetail);
          dom.From.AddJoin(JoinType.Left, new DQDmoSource(product), DQCondition.EQ(product, "ID", productDetail, "ProductNotice_ID"));
          dom.Columns.Add(DQSelectColumn.Field("Goods_ID", productDetail));
          dom.Columns.Add(DQSelectColumn.Field("DmoID", productDetail));
          dom.Where.Conditions.Add(DQCondition.EQ(product, "Customer_ID",customer));
          using (var reader = Session.ExecuteReader(dom))
          {
              while (reader.Read())
              {
                  if(dic.ContainsKey((long)reader[0]))
                      continue;
                  dic.Add((long)reader[0],(long?)reader[1]);
              }
          }
          return dic;
      }

      DQueryDom SaleForecastQuery(long? customer,DateTime? dates)
      {
          if (!PluginManager.Current.Installed("B3Sale")) return null;
          var detail = new JoinAlias(Type.GetType("BWP.B3Sale.BO.SaleForecast_Detail, B3Sale"));
          var bill = new JoinAlias(Type.GetType("BWP.B3Sale.BO.SaleForecast, B3Sale"));
          var dom = new DQueryDom(detail);
          dom.From.AddJoin(JoinType.Inner, new DQDmoSource(bill), DQCondition.EQ(detail, "SaleForecast_ID", bill, "ID"));
          dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(bill, "ID"), "Forecase_ID"));
          dom.Columns.Add(DQSelectColumn.Field("SaleGoods_ID", detail));
          dom.Columns.Add(DQSelectColumn.Create(GetNumber(detail, "UnitNum"), "Number"));
          dom.Columns.Add(DQSelectColumn.Field("Unit", detail));
          dom.Columns.Add(DQSelectColumn.Field("Price", detail));
          dom.Columns.Add(DQSelectColumn.Field("Money", detail));
          dom.Columns.Add(DQSelectColumn.Field("SecondNumber", detail));
          dom.Columns.Add(DQSelectColumn.Field("Goods_SecondUnit", detail));
          dom.Columns.Add(DQSelectColumn.Field("Remark", detail));
          dom.Columns.Add(DQSelectColumn.Field("Goods_Code", detail));
          dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
          dom.Columns.Add(DQSelectColumn.Field("Goods_Spec", detail));
          dom.Where.Conditions.Add(DQCondition.EQ(bill, "Customer_ID", customer));
          dom.Where.Conditions.Add(DQCondition.EQ(bill, "BillState", 单据状态.已审核));
          dom.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));
          var date = dates ?? BLContext.Today;
          dom.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Date", date.Date.AddDays(-7)));//只显示7天内的预报
          return dom;
      }

      private static IDQExpression GetNumber(JoinAlias bill, string title)
      {
          return DQExpression.IfNull(DQExpression.Field(bill, title), DQExpression.ConstValue(0));
      }
  }
}
