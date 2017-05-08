namespace B3ButcheryCE.ProductInStore_
{
    partial class ProductInStoreForm
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbxGoods = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(43, 234);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(72, 29);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "确  定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(181, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(46, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "确定";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "添加";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(68, 148);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.ReadOnly = true;
            this.txtNumber.Size = new System.Drawing.Size(107, 23);
            this.txtNumber.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 108);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 23);
            this.textBox1.TabIndex = 12;
            // 
            // cbxGoods
            // 
            this.cbxGoods.Location = new System.Drawing.Point(15, 70);
            this.cbxGoods.Name = "cbxGoods";
            this.cbxGoods.Size = new System.Drawing.Size(174, 23);
            this.cbxGoods.TabIndex = 10;
            this.cbxGoods.SelectedIndexChanged += new System.EventHandler(this.cbxGoods_SelectedIndexChanged_1);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "主数量";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "生产数量";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "存货名称:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(133, 234);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(77, 29);
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ProductInStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cbxGoods);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_OK);
            this.Name = "ProductInStoreForm";
            this.Text = "成品入库";
            this.Deactivate += new System.EventHandler(this.ProductInStoreForm_Deactivate);
            this.Load += new System.EventHandler(this.ProductInStoreForm_Load);
            this.Activated += new System.EventHandler(this.ProductInStoreForm_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbxGoods;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
    }
}