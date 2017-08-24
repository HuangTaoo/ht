namespace B3Butchery_TouchScreen
{
  partial class LoginForm
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
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.txtPwd = new BWP.WinFormControl.DFTextBox();
      this.txtName = new BWP.WinFormControl.DFTextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button2.Location = new System.Drawing.Point(310, 255);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(87, 70);
      this.button2.TabIndex = 7;
      this.button2.Text = "关闭";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.button1.Location = new System.Drawing.Point(163, 255);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(87, 70);
      this.button1.TabIndex = 8;
      this.button1.Text = "登录";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtPwd
      // 
      this.txtPwd.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtPwd.Location = new System.Drawing.Point(212, 136);
      this.txtPwd.Name = "txtPwd";
      this.txtPwd.PasswordChar = '*';
      this.txtPwd.Size = new System.Drawing.Size(273, 46);
      this.txtPwd.TabIndex = 6;
      // 
      // txtName
      // 
      this.txtName.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtName.Location = new System.Drawing.Point(212, 47);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(273, 46);
      this.txtName.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label2.Location = new System.Drawing.Point(109, 139);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(97, 40);
      this.label2.TabIndex = 4;
      this.label2.Text = "密码";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label1.Location = new System.Drawing.Point(69, 50);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 40);
      this.label1.TabIndex = 5;
      this.label1.Text = "用户名";
      // 
      // LoginForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(591, 394);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.txtPwd);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "LoginForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "登录";
      this.Load += new System.EventHandler(this.LoginForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private BWP.WinFormControl.DFTextBox txtPwd;
    private BWP.WinFormControl.DFTextBox txtName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}