namespace B3Butchery_TouchScreen
{
  partial class SelectDateFrom
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.dfDatePicker1 = new BWP.WinFormControl.DFDatePicker();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // dfDatePicker1
      // 
      this.dfDatePicker1.Location = new System.Drawing.Point(24, 26);
      this.dfDatePicker1.Name = "dfDatePicker1";
      this.dfDatePicker1.Size = new System.Drawing.Size(418, 61);
      this.dfDatePicker1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button1.Location = new System.Drawing.Point(81, 132);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(100, 62);
      this.button1.TabIndex = 1;
      this.button1.Text = "确定";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button2.Location = new System.Drawing.Point(286, 132);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(102, 62);
      this.button2.TabIndex = 1;
      this.button2.Text = "关闭";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // SelectDateFrom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(467, 219);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.dfDatePicker1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectDateFrom";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "选择日期";
      this.ResumeLayout(false);

    }

    #endregion

    private BWP.WinFormControl.DFDatePicker dfDatePicker1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
  }
}