namespace B3ButcheryCE
{
    partial class LoginForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemSysSetting = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemSysInfo = new System.Windows.Forms.MenuItem();
            this.menuItemScanTest = new System.Windows.Forms.MenuItem();
            this.linkLabelShowPassword = new System.Windows.Forms.LinkLabel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItemExit);
            this.mainMenu1.MenuItems.Add(this.menuItemHelp);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemSysSetting);
            this.menuItem1.Text = "设置";
            // 
            // menuItemSysSetting
            // 
            this.menuItemSysSetting.Text = "系统设置";
            this.menuItemSysSetting.Click += new System.EventHandler(this.menuItemSysSetting_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "退出";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.MenuItems.Add(this.menuItemSysInfo);
            this.menuItemHelp.MenuItems.Add(this.menuItemScanTest);
            this.menuItemHelp.Text = "帮助";
            // 
            // menuItemSysInfo
            // 
            this.menuItemSysInfo.Text = "系统信息";
            this.menuItemSysInfo.Click += new System.EventHandler(this.menuItemSysInfo_Click);
            // 
            // menuItemScanTest
            // 
            this.menuItemScanTest.Text = "扫描测试";
            this.menuItemScanTest.Click += new System.EventHandler(this.menuItemScanTest_Click);
            // 
            // linkLabelShowPassword
            // 
            this.linkLabelShowPassword.Location = new System.Drawing.Point(114, 149);
            this.linkLabelShowPassword.Name = "linkLabelShowPassword";
            this.linkLabelShowPassword.Size = new System.Drawing.Size(100, 20);
            this.linkLabelShowPassword.TabIndex = 13;
            this.linkLabelShowPassword.Text = "显示密码";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(62, 192);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(104, 41);
            this.buttonLogin.TabIndex = 12;
            this.buttonLogin.Text = "登录系统";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(93, 115);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(121, 23);
            this.textBoxPassword.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.Text = "密码";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(93, 65);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(121, 23);
            this.textBoxUsername.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.Text = "用户名";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.linkLabelShowPassword);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "青花瓷手持机系统";
            this.Closed += new System.EventHandler(this.LoginForm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LoginForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemSysSetting;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemSysInfo;
        private System.Windows.Forms.LinkLabel linkLabelShowPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItemScanTest;

    }
}