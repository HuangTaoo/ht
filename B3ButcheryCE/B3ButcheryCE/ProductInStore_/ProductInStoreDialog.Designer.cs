namespace B3ButcheryCE.ProductInStore_
{
    partial class ProductInStoreDialog
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
            this.menuItemHistore = new System.Windows.Forms.MenuItem();
            this.comboBoxSelectStore = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSelectGoods = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMainNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSecondNumber = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Finish = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProductPlan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemHistore);
            // 
            // menuItemHistore
            // 
            this.menuItemHistore.Text = "历史记录";
            this.menuItemHistore.Click += new System.EventHandler(this.menuItemHistore_Click);
            // 
            // comboBoxSelectStore
            // 
            this.comboBoxSelectStore.Location = new System.Drawing.Point(89, 74);
            this.comboBoxSelectStore.Name = "comboBoxSelectStore";
            this.comboBoxSelectStore.Size = new System.Drawing.Size(146, 23);
            this.comboBoxSelectStore.TabIndex = 2;
            this.comboBoxSelectStore.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.Text = "选择仓库：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.Text = "选择存货：";
            // 
            // comboBoxSelectGoods
            // 
            this.comboBoxSelectGoods.Location = new System.Drawing.Point(89, 114);
            this.comboBoxSelectGoods.Name = "comboBoxSelectGoods";
            this.comboBoxSelectGoods.Size = new System.Drawing.Size(146, 23);
            this.comboBoxSelectGoods.TabIndex = 5;
            this.comboBoxSelectGoods.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.Text = "主数量：";
            // 
            // textBoxMainNumber
            // 
            this.textBoxMainNumber.Location = new System.Drawing.Point(88, 169);
            this.textBoxMainNumber.Name = "textBoxMainNumber";
            this.textBoxMainNumber.Size = new System.Drawing.Size(146, 23);
            this.textBoxMainNumber.TabIndex = 7;
            this.textBoxMainNumber.LostFocus += new System.EventHandler(this.textBoxMainNumber_LostFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.Text = "辅数量：";
            // 
            // textBoxSecondNumber
            // 
            this.textBoxSecondNumber.Location = new System.Drawing.Point(88, 207);
            this.textBoxSecondNumber.Name = "textBoxSecondNumber";
            this.textBoxSecondNumber.Size = new System.Drawing.Size(146, 23);
            this.textBoxSecondNumber.TabIndex = 9;
            this.textBoxSecondNumber.LostFocus += new System.EventHandler(this.textBoxSecondNumber_LostFocus);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(3, 246);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(107, 26);
            this.btn_OK.TabIndex = 10;
            this.btn_OK.Text = "确 定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Finish
            // 
            this.btn_Finish.Location = new System.Drawing.Point(127, 246);
            this.btn_Finish.Name = "btn_Finish";
            this.btn_Finish.Size = new System.Drawing.Size(107, 26);
            this.btn_Finish.TabIndex = 11;
            this.btn_Finish.Text = "完 毕";
            this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "生产计划：";
            // 
            // comboBoxProductPlan
            // 
            this.comboBoxProductPlan.Location = new System.Drawing.Point(89, 34);
            this.comboBoxProductPlan.Name = "comboBoxProductPlan";
            this.comboBoxProductPlan.Size = new System.Drawing.Size(146, 23);
            this.comboBoxProductPlan.TabIndex = 17;
            this.comboBoxProductPlan.TabStop = false;
            // 
            // ProductInStoreDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.comboBoxProductPlan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Finish);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.textBoxSecondNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMainNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxSelectGoods);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSelectStore);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ProductInStoreDialog";
            this.Text = "成品入库";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductInStoreDialog_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductInStoreDialog_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemHistore;
        private System.Windows.Forms.ComboBox comboBoxSelectStore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSelectGoods;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMainNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSecondNumber;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Finish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProductPlan;
    }
}