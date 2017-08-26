namespace B3Butchery_TouchScreen
{
  partial class FrozenInStorePieceForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.flpBiaoQian = new System.Windows.Forms.FlowLayoutPanel();
      this.btnSetBiaoQian = new System.Windows.Forms.Button();
      this.flpGrid = new System.Windows.Forms.FlowLayoutPanel();
      this.btnCommit = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.flpGrid);
      this.splitContainer1.Size = new System.Drawing.Size(1264, 985);
      this.splitContainer1.SplitterDistance = 61;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 0;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.flpBiaoQian);
      this.flowLayoutPanel1.Controls.Add(this.btnSetBiaoQian);
      this.flowLayoutPanel1.Controls.Add(this.btnCommit);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(1264, 61);
      this.flowLayoutPanel1.TabIndex = 0;
      // 
      // flpBiaoQian
      // 
      this.flpBiaoQian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.flpBiaoQian.Location = new System.Drawing.Point(3, 3);
      this.flpBiaoQian.Name = "flpBiaoQian";
      this.flpBiaoQian.Size = new System.Drawing.Size(946, 55);
      this.flpBiaoQian.TabIndex = 0;
      // 
      // btnSetBiaoQian
      // 
      this.btnSetBiaoQian.Location = new System.Drawing.Point(955, 3);
      this.btnSetBiaoQian.Name = "btnSetBiaoQian";
      this.btnSetBiaoQian.Size = new System.Drawing.Size(75, 52);
      this.btnSetBiaoQian.TabIndex = 1;
      this.btnSetBiaoQian.Text = "设置标签";
      this.btnSetBiaoQian.UseVisualStyleBackColor = true;
      this.btnSetBiaoQian.Click += new System.EventHandler(this.button6_Click);
      // 
      // flpGrid
      // 
      this.flpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpGrid.Location = new System.Drawing.Point(0, 0);
      this.flpGrid.Name = "flpGrid";
      this.flpGrid.Size = new System.Drawing.Size(1264, 923);
      this.flpGrid.TabIndex = 0;
      // 
      // btnCommit
      // 
      this.btnCommit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnCommit.Location = new System.Drawing.Point(1053, 3);
      this.btnCommit.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
      this.btnCommit.Name = "btnCommit";
      this.btnCommit.Size = new System.Drawing.Size(115, 52);
      this.btnCommit.TabIndex = 1;
      this.btnCommit.Text = "提交";
      this.btnCommit.UseVisualStyleBackColor = true;
      this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
      // 
      // FrozenInStorePieceForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1264, 985);
      this.Controls.Add(this.splitContainer1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrozenInStorePieceForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "速冻入库计件";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrozenInStorePieceForm_FormClosing);
      this.Load += new System.EventHandler(this.FrozenInStorePieceForm_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.FlowLayoutPanel flpGrid;
    private System.Windows.Forms.FlowLayoutPanel flpBiaoQian;
    private System.Windows.Forms.Button btnSetBiaoQian;
    private System.Windows.Forms.Button btnCommit;
  }
}