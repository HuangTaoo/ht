namespace B3ButcheryCE
{
    partial class SysSettingDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxServerUrl = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBoxAccountingUnitID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "服务器地址";
            // 
            // textBoxServerUrl
            // 
            this.textBoxServerUrl.Location = new System.Drawing.Point(3, 42);
            this.textBoxServerUrl.Name = "textBoxServerUrl";
            this.textBoxServerUrl.Size = new System.Drawing.Size(232, 23);
            this.textBoxServerUrl.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(78, 258);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(72, 20);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "保存";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point(3, 119);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(235, 133);
            this.listView1.TabIndex = 14;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // textBoxAccountingUnitID
            // 
            this.textBoxAccountingUnitID.Location = new System.Drawing.Point(111, 79);
            this.textBoxAccountingUnitID.Name = "textBoxAccountingUnitID";
            this.textBoxAccountingUnitID.Size = new System.Drawing.Size(114, 23);
            this.textBoxAccountingUnitID.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.Text = "会计单位ID";
            // 
            // SysSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.textBoxAccountingUnitID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxServerUrl);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysSettingDialog";
            this.Text = "系统设置";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxServerUrl;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBoxAccountingUnitID;
        private System.Windows.Forms.Label label2;
    }
}