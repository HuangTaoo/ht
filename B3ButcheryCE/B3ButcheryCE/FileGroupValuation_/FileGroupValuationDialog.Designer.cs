namespace B3ButcheryCE.FileGroupValuation_
{
    partial class FileGroupValuationDialog
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
            this.comboBoxFileGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxPieceItem = new System.Windows.Forms.ComboBox();
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemHistory = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxFileGroup
            // 
            this.comboBoxFileGroup.Location = new System.Drawing.Point(89, 57);
            this.comboBoxFileGroup.Name = "comboBoxFileGroup";
            this.comboBoxFileGroup.Size = new System.Drawing.Size(146, 23);
            this.comboBoxFileGroup.TabIndex = 130;
            this.comboBoxFileGroup.TabStop = false;
            // 
            // comboBoxPieceItem
            // 
            this.comboBoxPieceItem.Location = new System.Drawing.Point(113, 127);
            this.comboBoxPieceItem.Name = "comboBoxPieceItem";
            this.comboBoxPieceItem.Size = new System.Drawing.Size(125, 23);
            this.comboBoxPieceItem.TabIndex = 133;
            this.comboBoxPieceItem.TabStop = false;
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.Location = new System.Drawing.Point(113, 184);
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.Size = new System.Drawing.Size(122, 23);
            this.txtBoxNumber.TabIndex = 136;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.Text = "数 量：";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(65, 246);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 26);
            this.buttonSave.TabIndex = 138;
            this.buttonSave.Text = "确定";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.label1.Text = "选择案组：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.Text = "选择计件品项：";
            // 
            // FileGroupValuationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.txtBoxNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxPieceItem);
            this.Controls.Add(this.comboBoxFileGroup);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FileGroupValuationDialog";
            this.Text = "案组计件";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FileGroupValuationDialog_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FileGroupValuationDialog_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFileGroup;
        private System.Windows.Forms.ComboBox comboBoxPieceItem;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;

    }
}