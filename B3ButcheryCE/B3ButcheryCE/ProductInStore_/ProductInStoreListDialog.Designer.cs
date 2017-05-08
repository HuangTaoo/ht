namespace B3HRCE.ProductInStore_
{
    partial class ProductInStoreListDialog
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
            this.时间 = new System.Windows.Forms.ColumnHeader();
            this.生产计划 = new System.Windows.Forms.ColumnHeader();
            this.入库类型 = new System.Windows.Forms.ColumnHeader();
            this.仓库 = new System.Windows.Forms.ColumnHeader();
            this.存货 = new System.Windows.Forms.ColumnHeader();
            this.主数量 = new System.Windows.Forms.ColumnHeader();
            this.辅数量 = new System.Windows.Forms.ColumnHeader();
            this.用户 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.时间);
            this.listView1.Columns.Add(this.生产计划);
            this.listView1.Columns.Add(this.入库类型);
            this.listView1.Columns.Add(this.仓库);
            this.listView1.Columns.Add(this.存货);
            this.listView1.Columns.Add(this.主数量);
            this.listView1.Columns.Add(this.辅数量);
            this.listView1.Columns.Add(this.用户);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 295);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // 时间
            // 
            this.时间.Text = "时间";
            this.时间.Width = 60;
            // 
            // 生产计划
            // 
            this.生产计划.Text = "生产计划";
            this.生产计划.Width = 60;
            // 
            // 入库类型
            // 
            this.入库类型.Text = "入库类型";
            this.入库类型.Width = 62;
            // 
            // 仓库
            // 
            this.仓库.Text = "仓库";
            this.仓库.Width = 55;
            // 
            // 存货
            // 
            this.存货.Text = "存货";
            this.存货.Width = 55;
            // 
            // 主数量
            // 
            this.主数量.Text = "主数量";
            this.主数量.Width = 50;
            // 
            // 辅数量
            // 
            this.辅数量.Text = "辅数量";
            this.辅数量.Width = 50;
            // 
            // 用户
            // 
            this.用户.Text = "用户";
            this.用户.Width = 60;
            // 
            // ProductInStoreListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductInStoreListDialog";
            this.Text = "成品入库历史记录";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 时间;
        private System.Windows.Forms.ColumnHeader 入库类型;
        private System.Windows.Forms.ColumnHeader 仓库;
        private System.Windows.Forms.ColumnHeader 存货;
        private System.Windows.Forms.ColumnHeader 主数量;
        private System.Windows.Forms.ColumnHeader 辅数量;
        private System.Windows.Forms.ColumnHeader 用户;
        private System.Windows.Forms.ColumnHeader 生产计划;
    }
}