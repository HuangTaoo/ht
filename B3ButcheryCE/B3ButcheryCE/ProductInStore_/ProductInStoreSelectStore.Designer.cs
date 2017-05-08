namespace B3ButcheryCE.ProductInStore_
{
    partial class ProductInStoreSelectStore
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.cbxStore = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.存货名称 = new System.Windows.Forms.ColumnHeader();
            this.存货数量 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(72, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.存货名称);
            this.listView1.Columns.Add(this.存货数量);
            this.listView1.Location = new System.Drawing.Point(0, 51);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 194);
            this.listView1.TabIndex = 1;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // cbxStore
            // 
            this.cbxStore.Location = new System.Drawing.Point(72, 22);
            this.cbxStore.Name = "cbxStore";
            this.cbxStore.Size = new System.Drawing.Size(122, 23);
            this.cbxStore.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.Text = "仓库：";
            // 
            // 存货名称
            // 
            this.存货名称.Text = "存货名称";
            this.存货名称.Width = 119;
            // 
            // 存货数量
            // 
            this.存货数量.Text = "存货数量";
            this.存货数量.Width = 116;
            // 
            // ProductInStoreSelectStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.cbxStore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Name = "ProductInStoreSelectStore";
            this.Text = "选择仓库";
            this.Load += new System.EventHandler(this.ProductInStoreSelectStore_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox cbxStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader 存货名称;
        private System.Windows.Forms.ColumnHeader 存货数量;
    }
}