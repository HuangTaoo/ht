namespace B3HRCE
{
    partial class UpdateDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.withProgreeLabel1 = new BWP.Compact.WithProgreeLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "开始更新";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // withProgreeLabel1
            // 
            this.withProgreeLabel1.Location = new System.Drawing.Point(3, 57);
            this.withProgreeLabel1.Name = "withProgreeLabel1";
            this.withProgreeLabel1.Size = new System.Drawing.Size(230, 51);
            this.withProgreeLabel1.TabIndex = 2;
            // 
            // UpdateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.withProgreeLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateDialog";
            this.Text = "更新程序";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private BWP.Compact.WithProgreeLabel withProgreeLabel1;
    }
}