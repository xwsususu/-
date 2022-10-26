
namespace PrecisionControlApp
{
    partial class EditFkdInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textTank2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textTank1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textTank3 = new System.Windows.Forms.ComboBox();
            this.流动人员数目 = new System.Windows.Forms.Label();
            this.textTank7 = new System.Windows.Forms.TextBox();
            this.工作状态 = new System.Windows.Forms.Label();
            this.textTank6 = new System.Windows.Forms.ComboBox();
            this.工作人员人数 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textTank5 = new System.Windows.Forms.ComboBox();
            this.textTank4 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入封控点名称：";
            // 
            // textTank2
            // 
            this.textTank2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank2.Location = new System.Drawing.Point(271, 60);
            this.textTank2.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.textTank2.Name = "textTank2";
            this.textTank2.Size = new System.Drawing.Size(300, 26);
            this.textTank2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入封控点等级：";
            // 
            // buttonOk
            // 
            this.buttonOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOk.Location = new System.Drawing.Point(20, 10);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(157, 50);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "确认";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNo.Location = new System.Drawing.Point(414, 10);
            this.buttonNo.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(157, 50);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "取消";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.no_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "序号：";
            // 
            // textTank1
            // 
            this.textTank1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank1.Location = new System.Drawing.Point(271, 20);
            this.textTank1.Margin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.textTank1.Name = "textTank1";
            this.textTank1.Size = new System.Drawing.Size(300, 26);
            this.textTank1.TabIndex = 19;
            this.textTank1.TextChanged += new System.EventHandler(this.textTank1_TextChanged);
            this.textTank1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTank1_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.textTank3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textTank2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textTank1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.流动人员数目, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.textTank7, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.工作状态, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textTank6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.工作人员人数, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textTank5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textTank4, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 355);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textTank3
            // 
            this.textTank3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textTank3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank3.FormattingEnabled = true;
            this.textTank3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.textTank3.Location = new System.Drawing.Point(271, 110);
            this.textTank3.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.textTank3.Name = "textTank3";
            this.textTank3.Size = new System.Drawing.Size(300, 24);
            this.textTank3.TabIndex = 34;
            // 
            // 流动人员数目
            // 
            this.流动人员数目.AutoSize = true;
            this.流动人员数目.Location = new System.Drawing.Point(20, 310);
            this.流动人员数目.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.流动人员数目.Name = "流动人员数目";
            this.流动人员数目.Size = new System.Drawing.Size(120, 16);
            this.流动人员数目.TabIndex = 30;
            this.流动人员数目.Text = "流动人员数目：";
            // 
            // textTank7
            // 
            this.textTank7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank7.Location = new System.Drawing.Point(271, 310);
            this.textTank7.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.textTank7.Name = "textTank7";
            this.textTank7.Size = new System.Drawing.Size(300, 26);
            this.textTank7.TabIndex = 31;
            this.textTank7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTank7_KeyPress);
            // 
            // 工作状态
            // 
            this.工作状态.AutoSize = true;
            this.工作状态.Location = new System.Drawing.Point(20, 260);
            this.工作状态.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.工作状态.Name = "工作状态";
            this.工作状态.Size = new System.Drawing.Size(88, 16);
            this.工作状态.TabIndex = 28;
            this.工作状态.Text = "工作状态：";
            // 
            // textTank6
            // 
            this.textTank6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textTank6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank6.FormattingEnabled = true;
            this.textTank6.Items.AddRange(new object[] {
            "正常",
            "休息",
            "检出阳性"});
            this.textTank6.Location = new System.Drawing.Point(271, 260);
            this.textTank6.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.textTank6.Name = "textTank6";
            this.textTank6.Size = new System.Drawing.Size(300, 24);
            this.textTank6.TabIndex = 29;
            // 
            // 工作人员人数
            // 
            this.工作人员人数.AutoSize = true;
            this.工作人员人数.Location = new System.Drawing.Point(20, 210);
            this.工作人员人数.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.工作人员人数.Name = "工作人员人数";
            this.工作人员人数.Size = new System.Drawing.Size(120, 16);
            this.工作人员人数.TabIndex = 26;
            this.工作人员人数.Text = "工作人员人数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "所属街道：(不可修改)";
            // 
            // textTank5
            // 
            this.textTank5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textTank5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank5.FormattingEnabled = true;
            this.textTank5.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.textTank5.Location = new System.Drawing.Point(271, 210);
            this.textTank5.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.textTank5.Name = "textTank5";
            this.textTank5.Size = new System.Drawing.Size(300, 24);
            this.textTank5.TabIndex = 27;
            // 
            // textTank4
            // 
            this.textTank4.BackColor = System.Drawing.SystemColors.Window;
            this.textTank4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textTank4.Location = new System.Drawing.Point(271, 160);
            this.textTank4.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.textTank4.Name = "textTank4";
            this.textTank4.ReadOnly = true;
            this.textTank4.Size = new System.Drawing.Size(300, 26);
            this.textTank4.TabIndex = 33;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonOk, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonDel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonNo, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 352);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(591, 80);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // buttonDel
            // 
            this.buttonDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDel.Location = new System.Drawing.Point(217, 10);
            this.buttonDel.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(157, 50);
            this.buttonDel.TabIndex = 24;
            this.buttonDel.Text = "删除";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // EditFkdInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 432);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(750, 400);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditFkdInfoForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "封控点防控信息修改";
            this.Load += new System.EventHandler(this.EditFkdInfoForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTank2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textTank1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label 工作人员人数;
        private System.Windows.Forms.ComboBox textTank5;
        private System.Windows.Forms.Label 工作状态;
        private System.Windows.Forms.ComboBox textTank6;
        private System.Windows.Forms.Label 流动人员数目;
        private System.Windows.Forms.TextBox textTank7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textTank4;
        private System.Windows.Forms.ComboBox textTank3;
    }
}