using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B3Butchery_TouchScreen
{
  public partial class EditGuidNameAndGuiGeForm : Form
  {
    private int mGridId;
    public long? mGoodsId;
    public string mName, mGuiGe, mGoodsName;
    public DateTime? mProductDate;
    public void Init(int gridid, string name, string guige, long? goodsId, string goodsName)
    {
      mGridId = gridid;
      mName = name;
      mGuiGe = guige;
      mGoodsId = goodsId;
      mGoodsName = goodsName;
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
    }

    private void button2_Click(object sender, EventArgs e)
    {
      mGoodsId = null;
      mGoodsName = "";
    }

    private void txtDate_Click(object sender, EventArgs e)
    {
      var f = new SelectDateFrom();
      if (f.ShowDialog() == DialogResult.OK)
      {
        mProductDate = f.mDateTime;
        txtDate.Text = f.mDateTime.ToString("yyyy-MM-dd");
      }
      
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
