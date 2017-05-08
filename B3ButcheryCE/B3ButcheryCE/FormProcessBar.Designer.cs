namespace B3HRCE
{
    partial class FormProcessBar
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
            this.prcBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prcBar
            // 
            this.prcBar.Location = new System.Drawing.Point(0, 15);
            this.prcBar.Name = "prcBar";
            this.prcBar.Size = new System.Drawing.Size(185, 28);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(191, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormProcessBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 82);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prcBar);
            this.Location = new System.Drawing.Point(10, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProcessBar";
            this.Text = "信息同步中，请稍后……";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prcBar;
        private System.Windows.Forms.Label label1;
    }
}