namespace B3HRCE.FrozenInStore_
{
    partial class FrozenInStoreSelectGoodsForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.listView1 = new System.Windows.Forms.ListView();
            this.名称 = new System.Windows.Forms.ColumnHeader();
            this.生产数量 = new System.Windows.Forms.ColumnHeader();
            this.主数量 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.名称);
            this.listView1.Columns.Add(this.生产数量);
            this.listView1.Columns.Add(this.主数量);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(268, 353);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // 名称
            // 
            this.名称.Text = "名称";
            this.名称.Width = 100;
            // 
            // 生产数量
            // 
            this.生产数量.Text = "生产数量";
            this.生产数量.Width = 80;
            // 
            // 主数量
            // 
            this.主数量.Text = "主数量";
            this.主数量.Width = 60;
            // 
            // FrozenInStoreSelectGoodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(268, 353);
            this.Controls.Add(this.listView1);
            this.Menu = this.mainMenu1;
            this.Name = "FrozenInStoreSelectGoodsForm";
            this.Text = "选择存货";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 名称;
        private System.Windows.Forms.ColumnHeader 生产数量;
        private System.Windows.Forms.ColumnHeader 主数量;

    }
}