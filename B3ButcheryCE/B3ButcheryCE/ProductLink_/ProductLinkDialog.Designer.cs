namespace B3ButcheryCE.ProductLink_
{
    partial class ProductLinkDialog
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
            this.menuItemHistory = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.btn_Finish = new System.Windows.Forms.Button();
            this.comboBoxProductPlan = new System.Windows.Forms.ComboBox();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.textBoxMainNumber = new System.Windows.Forms.TextBox();
            this.textBoxSecondNumber = new System.Windows.Forms.TextBox();
            this.comboBoxGoods = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemHistory);
            // 
            // menuItemHistory
            // 
            this.menuItemHistory.Text = "历史记录";
            this.menuItemHistory.Click += new System.EventHandler(this.menuItemHistory_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "生产计划：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.Text = "选择存货：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.Text = "主数量：";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.Text = "辅数量：";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(6, 246);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(107, 26);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "确 定";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // btn_Finish
            // 
            this.btn_Finish.Location = new System.Drawing.Point(124, 246);
            this.btn_Finish.Name = "btn_Finish";
            this.btn_Finish.Size = new System.Drawing.Size(107, 26);
            this.btn_Finish.TabIndex = 5;
            this.btn_Finish.Text = "完 毕";
            this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
            // 
            // comboBoxProductPlan
            // 
            this.comboBoxProductPlan.Location = new System.Drawing.Point(91, 47);
            this.comboBoxProductPlan.Name = "comboBoxProductPlan";
            this.comboBoxProductPlan.Size = new System.Drawing.Size(140, 23);
            this.comboBoxProductPlan.TabIndex = 6;
            this.comboBoxProductPlan.TabStop = false;
            // 
            // textBoxMainNumber
            // 
            this.textBoxMainNumber.Location = new System.Drawing.Point(91, 126);
            this.textBoxMainNumber.Name = "textBoxMainNumber";
            this.textBoxMainNumber.Size = new System.Drawing.Size(140, 23);
            this.textBoxMainNumber.TabIndex = 7;
            this.textBoxMainNumber.LostFocus += new System.EventHandler(this.textBoxMainNumber_LostFocus);
            // 
            // textBoxSecondNumber
            // 
            this.textBoxSecondNumber.Location = new System.Drawing.Point(91, 166);
            this.textBoxSecondNumber.Name = "textBoxSecondNumber";
            this.textBoxSecondNumber.Size = new System.Drawing.Size(140, 23);
            this.textBoxSecondNumber.TabIndex = 8;
            this.textBoxSecondNumber.LostFocus += new System.EventHandler(this.textBoxSecondNumber_LostFocus);
            // 
            // comboBoxGoods
            // 
            this.comboBoxGoods.Location = new System.Drawing.Point(91, 86);
            this.comboBoxGoods.Name = "comboBoxGoods";
            this.comboBoxGoods.Size = new System.Drawing.Size(140, 23);
            this.comboBoxGoods.TabIndex = 9;
            this.comboBoxGoods.TabStop = false;
            // 
            // ProductLinkDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.comboBoxGoods);
            this.Controls.Add(this.textBoxSecondNumber);
            this.Controls.Add(this.textBoxMainNumber);
            this.Controls.Add(this.comboBoxProductPlan);
            this.Controls.Add(this.btn_Finish);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ProductLinkDialog";
            this.Text = "生产环节";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductLinkDialog_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductLinkDialog_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button btn_Finish;
        private System.Windows.Forms.ComboBox comboBoxProductPlan;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.TextBox textBoxMainNumber;
        private System.Windows.Forms.TextBox textBoxSecondNumber;
        private System.Windows.Forms.ComboBox comboBoxGoods;

    }
}