namespace BWP.WinFormControl
{
  partial class SmallDfDatePicker
  {
    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.SuspendLayout();
      // 
      // btnDown
      // 
      this.btnDown.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnDown.Location = new System.Drawing.Point(274, 2);
      this.btnDown.Margin = new System.Windows.Forms.Padding(2);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(50, 44);
      this.btnDown.TabIndex = 6;
      this.btnDown.TabStop = false;
      this.btnDown.Text = "↓";
      this.btnDown.UseVisualStyleBackColor = true;
      // 
      // btnUp
      // 
      this.btnUp.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnUp.Location = new System.Drawing.Point(216, 2);
      this.btnUp.Margin = new System.Windows.Forms.Padding(2);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(54, 44);
      this.btnUp.TabIndex = 5;
      this.btnUp.TabStop = false;
      this.btnUp.Text = "↑";
      this.btnUp.UseVisualStyleBackColor = true;
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 20F);
      this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
      this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker1.Location = new System.Drawing.Point(2, 2);
      this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(210, 44);
      this.dateTimePicker1.TabIndex = 4;
      // 
      // SmallDfDatePicker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnDown);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.dateTimePicker1);
      this.Name = "SmallDfDatePicker";
      this.Size = new System.Drawing.Size(327, 48);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
  }
}
