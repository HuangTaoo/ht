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
            this.数量 = new System.Windows.Forms.ColumnHeader();
            this.确认数量 = new System.Windows.Forms.ColumnHeader();
            this.包装数 = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.Add(this.存货名称);
            this.listView1.Columns.Add(this.数量);
            this.listView1.Columns.Add(this.确认数量);
            this.listView1.Columns.Add(this.包装数);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 267);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            // 
            // 存货名称
            // 
            this.存货名称.Text = "存货名称";
            this.存货名称.Width = 75;
            // 
            // 数量
            // 
            this.数量.Text = "数量";
            this.数量.Width = 50;
            // 
            // 确认数量
            // 
            this.确认数量.Text = "确认数量";
            this.确认数量.Width = 70;
            // 
            // 包装数
            // 
            this.包装数.Text = "包装数";
            this.包装数.Width = 60;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "生成速冻入库";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrozenInStoreConfirmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.ColumnHeader 数量;
        private System.Windows.Forms.ColumnHeader 确认数量;
        private System.Windows.Forms.ColumnHeader 包装数;
        private System.Windows.Forms.Button button1;
    }
}