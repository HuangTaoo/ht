namespace B3HRCE.PersonalPiece_
{
    partial class PersonalPieceListDialog
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
            this.时间 = new System.Windows.Forms.ColumnHeader();
            this.员工 = new System.Windows.Forms.ColumnHeader();
            this.计件品项 = new System.Windows.Forms.ColumnHeader();
            this.数量 = new System.Windows.Forms.ColumnHeader();
            this.用户 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.时间);
            this.listView1.Columns.Add(this.员工);
            this.listView1.Columns.Add(this.计件品项);
            this.listView1.Columns.Add(this.数量);
            this.listView1.Columns.Add(this.用户);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 295);
            this.listView1.TabIndex = 2;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // 时间
            // 
            this.时间.Text = "时间";
            this.时间.Width = 53;
            // 
            // 员工
            // 
            this.员工.Text = "员工";
            this.员工.Width = 60;
            // 
            // 计件品项
            // 
            this.计件品项.Text = "计件品项";
            this.计件品项.Width = 75;
            // 
            // 数量
            // 
            this.数量.Text = "数量";
            this.数量.Width = 58;
            // 
            // 用户
            // 
            this.用户.Text = "用户";
            this.用户.Width = 72;
            // 
            // PersonalPieceListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonalPieceListDialog";
            this.Text = "个人计件历史记录";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 时间;
        private System.Windows.Forms.ColumnHeader 员工;
        private System.Windows.Forms.ColumnHeader 计件品项;
        private System.Windows.Forms.ColumnHeader 数量;
        private System.Windows.Forms.ColumnHeader 用户;
    }
}