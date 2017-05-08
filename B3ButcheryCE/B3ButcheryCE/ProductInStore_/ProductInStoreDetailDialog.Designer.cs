namespace B3HRCE.ProductInStore_
{
    partial class ProductInStoreDetailDialog
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
            this.生产计划 = new System.Windows.Forms.ColumnHeader();
            this.仓库 = new System.Windows.Forms.ColumnHeader();
            this.存货 = new System.Windows.Forms.ColumnHeader();
            this.主数量 = new System.Windows.Forms.ColumnHeader();
            this.辅数量 = new System.Windows.Forms.ColumnHeader();
            this.btn_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.生产计划);
            this.listView1.Columns.Add(this.仓库);
            this.listView1.Columns.Add(this.存货);
            this.listView1.Columns.Add(this.主数量);
            this.listView1.Columns.Add(this.辅数量);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 251);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // 生产计划
            // 
            this.生产计划.Text = "生产计划";
            this.生产计划.Width = 70;
            // 
            // 仓库
            // 
            this.仓库.Text = "仓库";
            this.仓库.Width = 70;
            // 
            // 存货
            // 
            this.存货.Text = "存货";
            this.存货.Width = 80;
            // 
            // 主数量
            // 
            this.主数量.Text = "主数量";
            this.主数量.Width = 55;
            // 
            // 辅数量
            // 
            this.辅数量.Text = "辅数量";
            this.辅数量.Width = 55;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(66, 257);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(107, 26);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确 定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ProductInStoreDetailDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.listView1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductInStoreDetailDialog";
            this.Text = "成品入库录入详情";
            this.Load += new System.EventHandler(this.ProductInStoreDetailDialog_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductInStoreDetailDialog_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ColumnHeader 仓库;
        private System.Windows.Forms.ColumnHeader 存货;
        private System.Windows.Forms.ColumnHeader 主数量;
        private System.Windows.Forms.ColumnHeader 辅数量;
        private System.Windows.Forms.ColumnHeader 生产计划;
    }
}