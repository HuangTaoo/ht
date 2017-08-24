namespace BWP.WinFormControl
{
  partial class DFDateTimePicker
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
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 30F);
      this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:ss";
      this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker1.Location = new System.Drawing.Point(1, 3);
      this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(438, 61);
      this.dateTimePicker1.TabIndex = 0;
      // 
      // btnUp
      // 
      this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUp.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnUp.Location = new System.Drawing.Point(443, 3);
      this.btnUp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(61, 61);
      this.btnUp.TabIndex = 1;
      this.btnUp.TabStop = false;
      this.btnUp.Text = "↑";
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnDown
      // 
      this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDown.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnDown.Location = new System.Drawing.Point(508, 3);
      this.btnDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(61, 61);
      this.btnDown.TabIndex = 1;
      this.btnDown.TabStop = false;
      this.btnDown.Text = "↓";
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // DFDateTimePicker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnDown);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.dateTimePicker1);
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "DFDateTimePicker";
      this.Size = new System.Drawing.Size(571, 67);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnDown;
  }
}
