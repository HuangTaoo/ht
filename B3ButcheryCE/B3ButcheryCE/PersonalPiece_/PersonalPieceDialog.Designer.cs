namespace B3HRCE.PersonalPiece_
{
    partial class PersonalPieceDialog
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
            this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPieceItem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "选择员工：";
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.Location = new System.Drawing.Point(92, 57);
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.Size = new System.Drawing.Size(146, 23);
            this.comboBoxEmployee.TabIndex = 130;
            this.comboBoxEmployee.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(-2, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.Text = "选择计件品项：";
            // 
            // comboBoxPieceItem
            // 
            this.comboBoxPieceItem.Location = new System.Drawing.Point(103, 125);
            this.comboBoxPieceItem.Name = "comboBoxPieceItem";
            this.comboBoxPieceItem.Size = new System.Drawing.Size(135, 23);
            this.comboBoxPieceItem.TabIndex = 133;
            this.comboBoxPieceItem.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.Text = "数 量：";
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.Location = new System.Drawing.Point(92, 184);
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.Size = new System.Drawing.Size(146, 23);
            this.txtBoxNumber.TabIndex = 136;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(4, 246);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 26);
            this.buttonSave.TabIndex = 138;
            this.buttonSave.Text = "确 定";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 26);
            this.button1.TabIndex = 142;
            this.button1.Text = "完 毕";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PersonalPieceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.txtBoxNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxPieceItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEmployee);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PersonalPieceDialog";
            this.Text = "个人计件";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PersonalPieceDialog_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PersonalPieceDialog_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEmployee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPieceItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button button1;

    }
}