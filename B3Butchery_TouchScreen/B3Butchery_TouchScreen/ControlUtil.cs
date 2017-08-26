using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using B3Butchery_TouchScreen.SqlEntityFramWork;

namespace B3Butchery_TouchScreen
{
  public class ControlUtil
  {
    public static Panel CreateGridPanel(GridConfig gridConfig)
    {
      var panel = new Panel();
      panel.BorderStyle = BorderStyle.FixedSingle;
      panel.Location = new System.Drawing.Point(3, 3);
      panel.Name = "panel";
      panel.Size = new System.Drawing.Size(1252, 89);
      panel.Margin = new Padding(1);
      panel.Tag = gridConfig;

      panel.Controls.Add(CreateBiaoShiLable(gridConfig));
      panel.Controls.Add(CreateNumberText());
      panel.Controls.Add(CreateNumberFlowLayoutPanel(gridConfig));
      panel.Controls.Add(CreateRecordPanel());
      panel.Controls.Add(CreateButtonSet(gridConfig));
      panel.Controls.Add(CreateButtonDelete());
      panel.Controls.Add(CreateWeightLable());
      panel.Controls.Add(CreateOkButton(gridConfig));




      //设置初始值 为了 调用 textchange 事件 给记录和重量赋值
      var text = panel.Controls["textBox"];
      text.Text = "0";
      //
      return panel;
    }

    private static Control CreateButtonDelete()
    {
      var btnDelete = new Button();
      btnDelete.Location = new System.Drawing.Point(702, 45);
      btnDelete.Name = "btnDelete";
      btnDelete.Size = new System.Drawing.Size(69, 35);
      btnDelete.Text = "回删";
      btnDelete.Click += BtnDelete_Click;
      btnDelete.UseVisualStyleBackColor = true;
      return btnDelete;
    }

    private static void BtnDelete_Click(object sender, EventArgs e)
    {
      var btnDelete = sender as Button;
      var txt = btnDelete.Parent.Controls["textBox"] as TextBox;
      var gridConfig = txt.Parent.Tag as GridConfig;

      var count = gridConfig.InputRecords.Count;
      if (count > 0)
      {
        //移除最后一次
        var lastRecord = gridConfig.InputRecords[count - 1];
        gridConfig.InputRecords.RemoveAt(count-1);

        txt.Text = "";
        txt.Text = "0";

        //删除最后一次，保存到数据库中
        using (var db=new SqlDbContext())
        {
          var record=  db.InputRecords.Find(lastRecord.Id);
          db.InputRecords.Remove(record);
          db.SaveChanges();
        }
      }

    }

    private static Control CreateButtonSet(GridConfig gridConfig)
    {
      var btnSet = new Button();
      btnSet.Location = new System.Drawing.Point(702, 4);
      btnSet.Name = "btnSet";
      btnSet.Size = new System.Drawing.Size(69, 35);
      btnSet.Text = "设置";
      btnSet.UseVisualStyleBackColor = true;
      btnSet.Click += BtnSet_Click;
      return btnSet;

    }

    //设置 快速输入的数字
    private static void BtnSet_Click(object sender, EventArgs e)
    {
      var btnSet = sender as Button;
      var flpNumber = btnSet.Parent.Controls["numberFlowLayoutPanel"];
      string input1="", input2="", input3="", input4="";
      int index = 0;
      foreach (Control control in flpNumber.Controls)
      {
        index++;
        if (index == 1)
        {
          input1 = control.Text;
        }
        else if (index == 2)
        {
          input2 = control.Text;
        }
        else if (index == 3)
        {
          input3 = control.Text;
        }
        else if (index == 4)
        {
          input4 = control.Text;
        }
      }
      var f=new SetInputNumberForm();
      f.Init(input1,input2,input3,input4);
      if (f.ShowDialog() == DialogResult.OK)
      {
        flpNumber.Controls.Clear();

        int in1;
        if (int.TryParse(f.Input1, out in1))
        {
          if (in1 != 0)
          {
            var btnNumberInput = new Button();
            btnNumberInput.Text = f.Input1;
            btnNumberInput.Size = new System.Drawing.Size(69, 63);
            btnNumberInput.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            btnNumberInput.Click += BtnNumber_Click;
            flpNumber.Controls.Add(btnNumberInput);
          }
        }

        int in2;
        if (int.TryParse(f.Input2, out in2))
        {
          if (in2 != 0)
          {
            var btnNumberInput = new Button();
            btnNumberInput.Text = f.Input2;
            btnNumberInput.Size = new System.Drawing.Size(69, 63);
            btnNumberInput.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            btnNumberInput.Click += BtnNumber_Click;
            flpNumber.Controls.Add(btnNumberInput);
          }
        }

        int in3;
        if (int.TryParse(f.Input3, out in3))
        {
          if (in3 != 0)
          {
            var btnNumberInput = new Button();
            btnNumberInput.Text = f.Input3;
            btnNumberInput.Size = new System.Drawing.Size(69, 63);
            btnNumberInput.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            btnNumberInput.Click += BtnNumber_Click;
            flpNumber.Controls.Add(btnNumberInput);
          }
        }

        int in4;
        if (int.TryParse(f.Input4, out in4))
        {
          if (in4 != 0)
          {
            var btnNumberInput = new Button();
            btnNumberInput.Text = f.Input4;
            btnNumberInput.Size = new System.Drawing.Size(69, 63);
            btnNumberInput.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            btnNumberInput.Click += BtnNumber_Click;
            flpNumber.Controls.Add(btnNumberInput);
          }
        }


      }
      //把配置的数字键更新到数据库
      var gridConfig = btnSet.Parent.Tag as GridConfig;
      using (var db = new SqlDbContext())
      {
        var addnumbers = db.GridAddedNumbers.Where(x => x.GridConfigId == gridConfig.Id).ToList();
        db.GridAddedNumbers.RemoveRange(addnumbers);

        foreach (Control control in flpNumber.Controls)
        {
          db.GridAddedNumbers.Add(new GridAddedNumber(){GridConfigId = gridConfig.Id,Number = Convert.ToInt32(control.Text) });
        }
        db.SaveChanges();
      }

    }

    private static Button CreateOkButton(GridConfig config)
    {
      var button = new Button();
      button.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      button.Location = new System.Drawing.Point(1148, -1);
      button.Name = "btnOk";
      button.Size = new System.Drawing.Size(103, 89);
      button.Text = "确定";
      button.UseVisualStyleBackColor = true;
      button.Enabled = config.IsCommited;
      if (config.IsCommited)
      {
        button.BackColor= Color.DodgerBlue;
      }
      else
      {
        button.BackColor =  SystemColors.Control;
      }
      button.Click += ButtonOk_Click;

      return button;

    }

    private static void ButtonOk_Click(object sender, EventArgs e)
    {
      var btn = sender as Button;
     var gridConfig= btn.Parent.Tag as GridConfig;

      using (var db=new SqlDbContext())
      {
        var config = db.GridConfigs.Include(x=>x.InputRecords).FirstOrDefault(x=>x.Id==gridConfig.Id);
        db.GridConfigs.Attach(config);
        //清空生产日期
        config.ProductDate = null;
        config.IsCommited = false;

        //清空输入记录
        var dbRecords = db.InputRecords.Where(x => x.GirdConfigId == config.Id);
        foreach (InputRecord record in dbRecords)
        {
          db.Entry(record).State=EntityState.Deleted;
        }
        db.SaveChanges();
      }

      gridConfig.InputRecords.Clear();
      gridConfig.ProductDate = null;
      gridConfig.IsCommited = false;
      btn.Parent.Tag = gridConfig;
      btn.Parent.Controls["textBox"].Text = "";
      btn.Parent.Controls["textBox"].Text="0";

      btn.BackColor = SystemColors.Control;
      btn.Enabled = false;
    }

    private static Label CreateWeightLable()
    {
      var label = new Label();
      label.BorderStyle = BorderStyle.FixedSingle;
      label.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      label.Location = new System.Drawing.Point(1002, 0);
      label.Margin = new Padding(0);
      label.Name = "lblWeight";
      label.Size = new System.Drawing.Size(143, 88);
      label.Text = "";
      label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

      return label;
    }

    //创建5行记录
    private static Panel CreateRecordPanel()
    {
      var panel = new Panel();
      var lblLine1 = new Label();
      lblLine1.Location = new System.Drawing.Point(17, 6);
      lblLine1.Name = "lblLine1";
      lblLine1.Padding = new Padding(1);
      lblLine1.Size = new System.Drawing.Size(181, 15);
      lblLine1.Text = "";
      panel.Controls.Add(lblLine1);

      var lblLine2 = new Label();
      lblLine2.Location = new System.Drawing.Point(17, 22);
      lblLine2.Name = "lblLine2";
      lblLine2.Padding = new Padding(1);
      lblLine2.Size = new System.Drawing.Size(181, 15);
      lblLine2.Text = "";
      panel.Controls.Add(lblLine2);

      var lblLine3 = new Label();
      lblLine3.Location = new System.Drawing.Point(17, 37);
      lblLine3.Name = "lblLine3";
      lblLine3.Padding = new Padding(1);
      lblLine3.Size = new System.Drawing.Size(181, 15);
      lblLine3.Text = "";
      panel.Controls.Add(lblLine3);

      var lblLine4 = new Label();
      lblLine4.Location = new System.Drawing.Point(17, 52);
      lblLine4.Name = "lblLine4";
      lblLine4.Padding = new Padding(1);
      lblLine4.Size = new System.Drawing.Size(181, 15);
      lblLine4.Text = "";
      panel.Controls.Add(lblLine4);

      var lblLine5 = new Label();
      lblLine5.Location = new System.Drawing.Point(17, 68);
      lblLine5.Name = "lblLine5";
      lblLine5.Padding = new Padding(1);
      lblLine5.Size = new System.Drawing.Size(181, 15);
      lblLine5.Text = "";
      panel.Controls.Add(lblLine5);

      panel.Location = new System.Drawing.Point(777, 0);
      panel.Name = "panelRecord";
      panel.Size = new System.Drawing.Size(211, 88);
      panel.BorderStyle=BorderStyle.FixedSingle;
      return panel;
    }

    private static FlowLayoutPanel CreateNumberFlowLayoutPanel(GridConfig gridConfig)
    {
      var fpanel = new FlowLayoutPanel();
      fpanel.BorderStyle = BorderStyle.FixedSingle;
      fpanel.Location = new System.Drawing.Point(394, 0);
      fpanel.Margin = new Padding(0);
      fpanel.Name = "numberFlowLayoutPanel";
      fpanel.Size = new System.Drawing.Size(305, 87);

      var nuberList = gridConfig.GridAddedNumbers.Skip(0).Take(4).ToList();
      foreach (GridAddedNumber number in nuberList)
      {
        var btnNumber=new Button();
        btnNumber.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
        btnNumber.Name = "btnNumber";
        btnNumber.Size = new System.Drawing.Size(69, 63);
        btnNumber.Text = number.ToString();
        btnNumber.UseVisualStyleBackColor = true;

        btnNumber.Click += BtnNumber_Click;
        fpanel.Controls.Add(btnNumber);
      }
      return fpanel;
    }
    //点击数字按钮
    private static void BtnNumber_Click(object sender, EventArgs e)
    {
      var btn = sender as Button;
      var txt = btn.Parent.Parent.Controls["textBox"] as TextBox;
      var gridConfig = txt.Parent.Tag as GridConfig;

      if (gridConfig.InputRecords.Count == 50)
      {
        MessageBox.Show("最多添加50条记录");
        return;
      }

      var record = new InputRecord() ;
      record.Number = Convert.ToInt32(btn.Text);
      record.GirdConfigId = gridConfig.Id;
      int guige;
      if (int.TryParse(gridConfig.GuiGe, out guige))
      {
        record.Weight = record.Number * guige;
      }
      gridConfig.InputRecords.Add(record);

      //添加到数据库中
      using (var db=new SqlDbContext())
      {
        db.InputRecords.Add(record);
        db.SaveChanges();
      }
      txt.Text = "";
      txt.Text = btn.Text;

    }

    static TextBox CreateNumberText()
    {
      var text = new TextBox();
      text.Location = new System.Drawing.Point(263, 19);
      text.Multiline = true;
      text.Name = "textBox";
      text.Multiline = true;
      text.ReadOnly = true;
      text.Size = new System.Drawing.Size(121, 50);
      text.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      text.Click += Text_Click;
      text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      text.TextChanged += txtNumber_Change;
      return text;
    }

    private static void Text_Click(object sender, EventArgs e)
    {
      MessageBox.Show("文本框点击了");
    }

    private static void txtNumber_Change(object sender, EventArgs e)
    {
      var txt = sender as TextBox;
      if (string.IsNullOrWhiteSpace(txt.Text))
      {
        return;
      }
      var flPanel = txt.Parent.Controls["panelRecord"];
      var gridConfig = txt.Parent.Tag as GridConfig;
      var lineRecord1 = gridConfig.InputRecords.Skip(0).Take(10);
      var lineRecord2 = gridConfig.InputRecords.Skip(10).Take(10);
      var lineRecord3 = gridConfig.InputRecords.Skip(20).Take(10);
      var lineRecord4 = gridConfig.InputRecords.Skip(30).Take(10);
      var lineRecord5 = gridConfig.InputRecords.Skip(40).Take(10);

      var lblLine1 = flPanel.Controls["lblLine1"] as Label;
      lblLine1.Text = string.Join("+", lineRecord1);

      var lblLine2 = flPanel.Controls["lblLine2"] as Label;
      lblLine2.Text = string.Join("+", lineRecord2);

      var lblLine3 = flPanel.Controls["lblLine3"] as Label;
      lblLine3.Text = string.Join("+", lineRecord3);

      var lblLine4 = flPanel.Controls["lblLine4"] as Label;
      lblLine4.Text = string.Join("+", lineRecord4);

      var lblLine5 = flPanel.Controls["lblLine5"] as Label;
      lblLine5.Text = string.Join("+", lineRecord5);

      var lblWeight = txt.Parent.Controls["lblWeight"];
      lblWeight.Text = gridConfig.InputRecords.Sum(x=>x.Weight).ToString();
    }

    static Label CreateBiaoShiLable(GridConfig gridConfig)
    {
      var lable = new Label();
      lable.BorderStyle = BorderStyle.FixedSingle;
      lable.Location = new System.Drawing.Point(0, 0);
      lable.Margin = new Padding(0);
      lable.Name = "lblBiaoShi";
      lable.Size = new System.Drawing.Size(249, 88);
      lable.Text = gridConfig.Name;
      lable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      if (gridConfig.Goods_ID != null && gridConfig.Goods_ID > 0)
      {
        lable.BackColor = Color.DodgerBlue;
      }
      else
      {
        lable.BackColor = SystemColors.Control;
      }
      lable.Click += lblBiaoShi_Click;
      return lable;
    }

    private static void lblBiaoShi_Click(object sender, EventArgs e)
    {
      var lblBiaoShi = sender as Label;
      var gridConfig = lblBiaoShi.Parent.Tag as GridConfig;
      var f=new EditGuidNameAndGuiGeForm();
      f.Init(gridConfig);
      if (f.ShowDialog() == DialogResult.OK)
      {
        gridConfig.Name = f.mName;
        gridConfig.GuiGe = f.mGuiGe;
        gridConfig.Goods_ID = f.mGoodsId;
        gridConfig.Goods_Name = f.mGoodsName;
        gridConfig.ProductDate = f.mProductDate;
        lblBiaoShi.Text = f.mName;
        if (gridConfig.Goods_ID != null && gridConfig.Goods_ID > 0)
        {
          lblBiaoShi.BackColor=Color.DodgerBlue;
        }
        else
        {
          lblBiaoShi.BackColor = SystemColors.Control;
        }

      }
    }
  }
}
