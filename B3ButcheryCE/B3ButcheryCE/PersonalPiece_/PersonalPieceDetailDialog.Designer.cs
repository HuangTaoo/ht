namespace B3HRCE.PersonalPiece_
{
    partial class PersonalPieceDetailDialog
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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.员工 = new System.Windows.Forms.ColumnHeader();
            this.岗位 = new System.Windows.Forms.ColumnHeader();
            this.计件品项 = new System.Windows.Forms.ColumnHeader();
            this.数量 = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.员工);
            this.listView1.Columns.Add(this.岗位);
            this.listView1.Columns.Add(this.计件品项);
            this.listView1.Columns.Add(this.数量);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 251);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // 员工
            // 
            this.员工.Text = "员工";
            this.员工.Width = 79;
            // 
            // 岗位
            // 
            this.岗位.Text = "岗位";
            this.岗位.Width = 80;
            // 
            // 计件品项
            // 
            this.计件品项.Text = "计件品项";
            this.计件品项.Width = 80;
            // 
            // 数量
            // 
            this.数量.Text = "数量";
            this.数量.Width = 75;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "确 定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PersonalPieceDetailDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonalPieceDetailDialog";
            this.Text = "个人计件录入详情";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PersonalPieceDetailDialog_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 员工;
        private System.Windows.Forms.ColumnHeader 岗位;
        private System.Windows.Forms.ColumnHeader 计件品项;
        private System.Windows.Forms.ColumnHeader 数量;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}