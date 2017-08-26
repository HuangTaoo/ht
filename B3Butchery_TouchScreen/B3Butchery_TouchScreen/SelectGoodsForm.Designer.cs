namespace B3Butchery_TouchScreen
{
  partial class SelectGoodsForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.txtInput = new BWP.WinFormControl.DFTextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.存货名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.规格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.选择 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
      this.splitContainer1.Panel1.Controls.Add(this.label2);
      this.splitContainer1.Panel1.Controls.Add(this.button2);
      this.splitContainer1.Panel1.Controls.Add(this.button1);
      this.splitContainer1.Panel1.Controls.Add(this.txtInput);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
      this.splitContainer1.Size = new System.Drawing.Size(954, 772);
      this.splitContainer1.SplitterDistance = 58;
      this.splitContainer1.TabIndex = 0;
      // 
      // txtInput
      // 
      this.txtInput.Font = new System.Drawing.Font("宋体", 20F);
      this.txtInput.Location = new System.Drawing.Point(3, 10);
      this.txtInput.Name = "txtInput";
      this.txtInput.Size = new System.Drawing.Size(254, 38);
      this.txtInput.TabIndex = 1;
      this.txtInput.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(814, 10);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 38);
      this.button1.TabIndex = 0;
      this.button1.Text = "清空条件";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(682, 10);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(92, 38);
      this.button2.TabIndex = 0;
      this.button2.Text = "查询";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeight = 40;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.存货名称,
            this.编码,
            this.规格,
            this.选择});
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowTemplate.Height = 40;
      this.dataGridView1.Size = new System.Drawing.Size(954, 710);
      this.dataGridView1.TabIndex = 1;
      this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
      // 
      // ID
      // 
      this.ID.DataPropertyName = "Goods_ID";
      this.ID.HeaderText = "ID";
      this.ID.Name = "ID";
      this.ID.ReadOnly = true;
      this.ID.Width = 60;
      // 
      // 存货名称
      // 
      this.存货名称.DataPropertyName = "Goods_Name";
      this.存货名称.HeaderText = "存货名称";
      this.存货名称.Name = "存货名称";
      this.存货名称.ReadOnly = true;
      this.存货名称.Width = 320;
      // 
      // 编码
      // 
      this.编码.DataPropertyName = "Goods_Code";
      this.编码.HeaderText = "编码";
      this.编码.Name = "编码";
      this.编码.ReadOnly = true;
      this.编码.Width = 200;
      // 
      // 规格
      // 
      this.规格.DataPropertyName = "Goods_Spec";
      this.规格.HeaderText = "规格";
      this.规格.Name = "规格";
      this.规格.ReadOnly = true;
      this.规格.Width = 200;
      // 
      // 选择
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle4.NullValue = "选择";
      this.选择.DefaultCellStyle = dataGridViewCellStyle4;
      this.选择.HeaderText = "选择";
      this.选择.Name = "选择";
      this.选择.ReadOnly = true;
      this.选择.Width = 120;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label2.ForeColor = System.Drawing.Color.Red;
      this.label2.Location = new System.Drawing.Point(263, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(275, 19);
      this.label2.TabIndex = 2;
      this.label2.Text = "支持编码，拼音，名称模糊搜索";
      // 
      // SelectGoodsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(954, 772);
      this.Controls.Add(this.splitContainer1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectGoodsForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "快速选择存货";
      this.Load += new System.EventHandler(this.SelectGoodsForm_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private BWP.WinFormControl.DFTextBox txtInput;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    private System.Windows.Forms.DataGridViewTextBoxColumn 存货名称;
    private System.Windows.Forms.DataGridViewTextBoxColumn 编码;
    private System.Windows.Forms.DataGridViewTextBoxColumn 规格;
    private System.Windows.Forms.DataGridViewButtonColumn 选择;
    private System.Windows.Forms.Label label2;
  }
}