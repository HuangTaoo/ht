namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    partial class FrozenInStoreConfirmList
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.存货名称 = new System.Windows.Forms.ColumnHeader();
            this.存货重量 = new System.Windows.Forms.ColumnHeader();
            this.确认重量 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.存货名称);
            this.listView1.Columns.Add(this.存货重量);
            this.listView1.Columns.Add(this.确认重量);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 295);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // 存货名称
            // 
            this.存货名称.Text = "存货名称";
            this.存货名称.Width = 75;
            // 
            // 存货重量
            // 
            this.存货重量.Text = "存货重量";
            this.存货重量.Width = 90;
            // 
            // 确认重量
            // 
            this.确认重量.Text = "确认重量";
            this.确认重量.Width = 109;
            // 
            // FrozenInStoreConfirmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrozenInStoreConfirmList";
            this.Text = "选择存货确认";
            this.Load += new System.EventHandler(this.FrozenInStoreConfirmList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 存货名称;
        private System.Windows.Forms.ColumnHeader 存货重量;
        private System.Windows.Forms.ColumnHeader 确认重量;
    }
}