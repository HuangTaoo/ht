namespace B3Butchery_TouchScreen
{
  partial class EditGuidNameAndGuiGeForm
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
      this.lblName = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtName = new BWP.WinFormControl.DFTextBox();
      this.txtGuiGe = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.txtGoodsName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.button2 = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDate = new System.Windows.Forms.TextBox();
      this.btnClearDate = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.lblName.Location = new System.Drawing.Point(33, 33);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(130, 24);
      this.lblName.TabIndex = 0;
      this.lblName.Text = "显示名称：";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label1.Location = new System.Drawing.Point(81, 90);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 24);
      this.label1.TabIndex = 0;
      this.label1.Text = "规格：";
      // 
      // txtName
      // 
      this.txtName.Font = new System.Drawing.Font("宋体", 20F);
      this.txtName.Location = new System.Drawing.Point(164, 28);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(266, 38);
      this.txtName.TabIndex = 1;
      // 
      // txtGuiGe
      // 
      this.txtGuiGe.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtGuiGe.Location = new System.Drawing.Point(164, 87);
      this.txtGuiGe.Multiline = true;
      this.txtGuiGe.Name = "txtGuiGe";
      this.txtGuiGe.Size = new System.Drawing.Size(91, 36);
      this.txtGuiGe.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button1.Location = new System.Drawing.Point(189, 337);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(130, 71);
      this.button1.TabIndex = 3;
      this.button1.Text = "保存";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtGoodsName
      // 
      this.txtGoodsName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtGoodsName.Location = new System.Drawing.Point(164, 154);
      this.txtGoodsName.Multiline = true;
      this.txtGoodsName.Name = "txtGoodsName";
      this.txtGoodsName.Size = new System.Drawing.Size(266, 36);
      this.txtGoodsName.TabIndex = 2;
      this.txtGoodsName.Click += new System.EventHandler(this.txtGoodsName_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label2.Location = new System.Drawing.Point(33, 157);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(130, 24);
      this.label2.TabIndex = 0;
      this.label2.Text = "存货名称：";
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button2.Location = new System.Drawing.Point(446, 154);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 36);
      this.button2.TabIndex = 4;
      this.button2.Text = "清除";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label3.Location = new System.Drawing.Point(33, 236);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(130, 24);
      this.label3.TabIndex = 0;
      this.label3.Text = "生产日期：";
      // 
      // txtDate
      // 
      this.txtDate.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtDate.Location = new System.Drawing.Point(164, 236);
      this.txtDate.Multiline = true;
      this.txtDate.Name = "txtDate";
      this.txtDate.Size = new System.Drawing.Size(266, 36);
      this.txtDate.TabIndex = 2;
      this.txtDate.Click += new System.EventHandler(this.txtDate_Click);
      // 
      // btnClearDate
      // 
      this.btnClearDate.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnClearDate.Location = new System.Drawing.Point(446, 236);
      this.btnClearDate.Name = "btnClearDate";
      this.btnClearDate.Size = new System.Drawing.Size(75, 36);
      this.btnClearDate.TabIndex = 4;
      this.btnClearDate.Text = "清除";
      this.btnClearDate.UseVisualStyleBackColor = true;
      // 
      // EditGuidNameAndGuiGeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(529, 435);
      this.Controls.Add(this.btnClearDate);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.txtDate);
      this.Controls.Add(this.txtGoodsName);
      this.Controls.Add(this.txtGuiGe);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblName);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditGuidNameAndGuiGeForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "设置名称和规格";
      this.Load += new System.EventHandler(this.EditGuidNameAndGuiGeForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label label1;
    private BWP.WinFormControl.DFTextBox txtName;
    private System.Windows.Forms.TextBox txtGuiGe;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox txtGoodsName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtDate;
    private System.Windows.Forms.Button btnClearDate;
  }
}