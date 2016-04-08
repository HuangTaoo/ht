using System;
using System.Web.UI.WebControls;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using TSingSoft.WebControls2.DFGrids;
using TSingSoft.WebPluginFramework.Controls;

namespace BWP.B3Butchery.Utils
{
  public static class B3ButcheryWebUtil
  {
    public static void CreateExportExcelPart(VLayoutPanel vPanel, DFBrowseGrid grid, string displayName)
    {
      HLayoutPanel hbox = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      var exporter = new Exporter();
      hbox.Add(new TSButton("导出到Excel", delegate
      {
        var lastQuery = grid.LastQuery;
        if (lastQuery == null)
          throw new Exception("请先进行查询");
        var dom = new LoadArguments((DQueryDom)lastQuery.DQuery.Clone());
        foreach (var colIndex in lastQuery.SumColumns)
          dom.SumColumns.Add(colIndex);
        foreach (var colIndex in lastQuery.GroupSumColumns)
          dom.GroupSumColumns.Add(colIndex);
        dom.DQuery.Range = SelectRange.All;
        exporter.Export(new TSingSoft.WebPluginFramework.Exports.QueryResultExcelExporter(displayName, GetQueryResult(dom)));
      }));
      hbox.Add(exporter);
    }

    private static QueryResult GetQueryResult(LoadArguments arg)
    {
      var data = new DFDataTableEditor().Load(arg);
      return new QueryResult(data.TotalCount, data.Data.Rows, data.Data.Columns, data.Data.HasSumRow ? data.Data.SumRow : null);
    }
  }
}