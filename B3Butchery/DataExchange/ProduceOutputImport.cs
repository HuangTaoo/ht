using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.DataExchange;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.B3UnitedInfos.DataExchange;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BWP.B3Butchery.DataExchange {
  public class ProduceOutputImport : IDataFileImport {

    public string Description {
      get {
        using (
          var r =
            new StreamReader(ResourcesUtil.GetResourceStream(this.GetType().FullName + ".html", GetType().Assembly))) {
          return r.ReadToEnd();
        }
      }
    }

    public string Name {
      get { return "屠宰分割产出单导入"; }
    }

    public IList<string> RequireRoles {
      get { return new[] { "B3Butchery.产出单.新建" }; }
    }

    public void Import(Stream fileStream) {
      var man = new ExcelImportManager();
      var accColumn = man.Add(new ExecelImportColumn() {
        Name = "会计单位",
        Required = true,
        Nullable = false
      });

      var deptColumn = man.Add(new ExecelImportColumn() {
        Name = "部门",
        Required = true,
        Nullable = false
      });

      var empColumn = man.Add(new ExecelImportColumn() {
        Name = "经办人",
        Required = true,
        Nullable = false
      });

      var timeColumn = man.Add(new ExecelImportColumn() {
        Name = "日期",
        Required = true,
        Nullable = false
      });

      var planNumberColumn = man.Add(new ExecelImportColumn() {
        Name = "计划号",
        Required = false,
        Nullable = true
      });

      var productLinksColumn = man.Add(new ExecelImportColumn() {
        Name = "生产环节",
        Required = false,
        Nullable = true
      });
      var remarkColumn = man.Add(new ExecelImportColumn() {
        Name = "摘要",
        Required = false,
        Nullable = true
      });


      var nameColumn = man.Add(new ExecelImportColumn() {
        Name = "存货名称",
        Required = true,
        Nullable = false
      });

      var codeColumn = man.Add(new ExecelImportColumn() {
        Name = "存货编码",
        Required = false,
        Nullable = true
      });

      var numberColumn = man.Add(new ExecelImportColumn() {
        Name = "数量",
        Required = true,
        Nullable = false
      });

      var detailRemarkColumn = man.Add(new ExecelImportColumn() {
        Name = "备注",
        Required = false,
        Nullable = true
      });

      var billList = new DmoCollection<ProduceOutput>();
      int i = 0;
      using (var context = new TransactionContext()) {
        var bl = BIFactory.Create<IProduceOutputBL>(context);

        foreach (var row in man.Parse(fileStream)) {
          var bill = new ProduceOutput();
          bl.InitNewDmo(bill);
          i++;
          bill.ID = i;
          billList.Add(bill);
          long accID = default(long);
          if (accColumn.TryGetIDByName<AccountingUnit>(row, ref accID))
            bill.AccountingUnit_ID = accID;

          long deptID = default(long);
          if (deptColumn.TryGetIDByName<Department>(row, ref deptID))
            bill.Department_ID = deptID;

          long empID = default(long);
          if (empColumn.TryGetIDByName<Employee>(row, ref empID))
            bill.Employee_ID = empID;

          long planNumberID = default(long);
          if (ExecelImportHelper.TryGetID<ProductPlan>(planNumberColumn, row, "PlanNumber", ref planNumberID))
            bill.PlanNumber_ID = planNumberID;

          long productLinksID = default(long);
          if (productLinksColumn.TryGetIDByName<ProductLinks>(row, ref productLinksID))
            bill.ProductLinks_ID = productLinksID;

          DateTime time = default(DateTime);
          if (timeColumn.TryGetValue(row, ref time))
            bill.Time = time;

          bill.Remark = remarkColumn.GetStringValue(row);

          var detail = new ProduceOutput_Detail();
          long goodsID = default(long);
          if (codeColumn.TryGetIDByCode<Goods>(row, ref goodsID))
            detail.Goods_ID = goodsID;
          else if (nameColumn.TryGetIDByName<Goods>(row, ref goodsID)) {
            detail.Goods_ID = goodsID;
          }

          decimal number = default(decimal);
          if (numberColumn.TryGetValue(row, ref number)) {
            detail.Number = number;
          }

          detail.Remark = detailRemarkColumn.GetStringValue(row);

          DmoUtil.RefreshDependency(detail, "Goods_ID");
          ConvertToSecondNumber(detail);
          bill.Details.Add(detail);
        }

        foreach (var group in billList.GroupBy(x => new { x.Time, x.AccountingUnit_ID, x.Employee_ID, x.Department_ID, x.PlanNumber_ID, x.ProductLinks_ID })) {
          var dmo = group.FirstOrDefault();
          foreach (var produceOutput in group) {
            if (dmo.ID == produceOutput.ID) {
              continue;
            }
            foreach (var detail in produceOutput.Details) {
              dmo.Details.Add(detail);
            }
          }
          dmo.ID = 0;
          bl.Insert(dmo);
        }
        context.Commit();
      }
    }

    static void ConvertToSecondNumber<T>(T detail)  where T : GoodsDetail {
      if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅) {
        if (detail.Goods_MainUnitRatio != 0) {
          detail.SecondNumber = detail.Number * detail.Goods_SecondUnitRatio / detail.Goods_MainUnitRatio;
        }
      }
    }
  }
}
