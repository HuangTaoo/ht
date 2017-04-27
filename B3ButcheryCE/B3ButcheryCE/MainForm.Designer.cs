namespace B3HRCE
{
    partial class MainForm
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
            this.buttonFileGroupValuation = new System.Windows.Forms.Button();
            this.buttonSyncBaseInfo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_ProductInStore = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.productLink_Btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonFileGroupValuation
            // 
            this.buttonFileGroupValuation.Location = new System.Drawing.Point(12, 80);
            this.buttonFileGroupValuation.Name = "buttonFileGroupValuation";
            this.buttonFileGroupValuation.Size = new System.Drawing.Size(99, 29);
            this.buttonFileGroupValuation.TabIndex = 15;
            this.buttonFileGroupValuation.Text = "案组计件新增";
            this.buttonFileGroupValuation.Click += new System.EventHandler(this.buttonFileGroupValuation_Click);
            // 
            // buttonSyncBaseInfo
            // 
            this.buttonSyncBaseInfo.Location = new System.Drawing.Point(12, 26);
            this.buttonSyncBaseInfo.Name = "buttonSyncBaseInfo";
            this.buttonSyncBaseInfo.Size = new System.Drawing.Size(99, 29);
            this.buttonSyncBaseInfo.TabIndex = 16;
            this.buttonSyncBaseInfo.Text = "同步基础信息";
            this.buttonSyncBaseInfo.Click += new System.EventHandler(this.buttonSyncBaseInfo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 29);
            this.button1.TabIndex = 17;
            this.button1.Text = "个人计件新增";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_ProductInStore
            // 
            this.btn_ProductInStore.Location = new System.Drawing.Point(12, 188);
            this.btn_ProductInStore.Name = "btn_ProductInStore";
            this.btn_ProductInStore.Size = new System.Drawing.Size(99, 29);
            this.btn_ProductInStore.TabIndex = 19;
            this.btn_ProductInStore.Text = "成品入库新增";
            this.btn_ProductInStore.Click += new System.EventHandler(this.btn_ProductInStore_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 271);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(238, 24);
            // 
            // productLink_Btn
            // 
            this.productLink_Btn.Location = new System.Drawing.Point(128, 26);
            this.productLink_Btn.Name = "productLink_Btn";
            this.productLink_Btn.Size = new System.Drawing.Size(99, 29);
            this.productLink_Btn.TabIndex = 21;
            this.productLink_Btn.Text = "生产环节新增";
            this.productLink_Btn.Click += new System.EventHandler(this.productLink_Btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 29);
            this.button2.TabIndex = 23;
            this.button2.Text = "产量统计";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.productLink_Btn);
            this.Controls.Add(this.btn_ProductInStore);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.buttonSyncBaseInfo);
            this.Controls.Add(this.buttonFileGroupValuation);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "青花瓷手持机系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonFileGroupValuation;
        private System.Windows.Forms.Button buttonSyncBaseInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_ProductInStore;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Button productLink_Btn;
        private System.Windows.Forms.Button button2;
    }
}