using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using B3Butchery_TouchScreen.SqlEntityFramWork;

namespace B3Butchery_TouchScreen
{
  public partial class EditGuidNameAndGuiGeForm : Form
  {
    private int mGridId;
    public long? mGoodsId;
    public string mName, mGuiGe, mGoodsName;
    public DateTime? mProductDate;
    public void Init(GridConfig config)
    {
      mGridId = config.Id;
      mName = config.Name;
      mGuiGe = config.GuiGe;
      mGoodsId = config.Goods_ID;
      mGoodsName = config.Goods_Name;
    }

    public EditGuidNameAndGuiGeForm()
    {
      InitializeComponent();
    }

    private void EditGuidNameAndGuiGeForm_Load(object sender, EventArgs e)
    {
      txtName.Text = mName;
      txtGuiGe.Text = mGuiGe;
      txtGoodsName.Text = mGoodsName;
      if (mProductDate.HasValue)
      {
        txtDate.Text = mProductDate.Value.ToString("yyyy-MM-dd");
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      mGoodsId = null;
      mGoodsName = "";
      txtGoodsName.Text = "";
    }

    private void txtDate_Click(object sender, EventArgs e)
    {
      var f = new SelectDateFrom();
      if (f.ShowDialog() == DialogResult.OK)
      {
        mProductDate = f.mDateTime;
        txtDate.Text = mProductDate.Value.ToString("yyyy-MM-dd");
      }
    }

    private void btnClearDate_Click(object sender, EventArgs e)
    {
      mProductDate = null;
      txtDate.Text = "";
    }

    private void txtGoodsName_Click(object sender, EventArgs e)
    {
      var f = new SelectGoodsForm();
      if (f.ShowDialog() == DialogResult.OK)
      {
        mGoodsId = f.mGoodsID;
        mGoodsName = f.mGoodsName;
        txtGoodsName.Text = mGoodsName;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      mName = txtName.Text.Trim();
      mGuiGe = txtGuiGe.Text.Trim();
      using (var db = new SqlDbContext())
      {
        var grid = db.GridConfigs.Find(mGridId);
        db.GridConfigs.Attach(grid);
        grid.Name = mName;
        grid.GuiGe = mGuiGe;
        grid.Goods_ID = mGoodsId;
        grid.Goods_Name = mGoodsName;
        grid.ProductDate = mProductDate;
        db.SaveChanges();
      }
      DialogResult = DialogResult.OK;
      Close();
    }
  }
}
