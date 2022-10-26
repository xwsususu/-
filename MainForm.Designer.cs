
namespace PrecisionControlApp
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.resetMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rectQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.queryFKD = new System.Windows.Forms.ToolStripMenuItem();
            this.queryJD = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addFKD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.FKDxiugai = new System.Windows.Forms.ToolStripMenuItem();
            this.街道防控信息修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pathGuide = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.实时街道检测信息统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.今日最终检测信息统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Map1 = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Panel();
            this.labeltitle = new System.Windows.Forms.Label();
            this.buttonbox = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolbox = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Map1.SuspendLayout();
            this.title.SuspendLayout();
            this.buttonbox.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetMap,
            this.toolStripSeparator1,
            this.rectQuery,
            this.toolStripSeparator2,
            this.toolStripSplitButton1,
            this.toolStripSeparator3,
            this.addFKD,
            this.toolStripSeparator4,
            this.toolStripDropDownButton1,
            this.toolStripSeparator5,
            this.pathGuide,
            this.toolStripSeparator6,
            this.toolStripDropDownButton3,
            this.toolStripSeparator7,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(465, 75);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "工具栏";
            // 
            // resetMap
            // 
            this.resetMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetMap.Image = global::PrecisionControlApp.Properties.Resources.fwck;
            this.resetMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetMap.Name = "resetMap";
            this.resetMap.Size = new System.Drawing.Size(44, 72);
            this.resetMap.Text = "复位";
            this.resetMap.Click += new System.EventHandler(this.resetMap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 75);
            // 
            // rectQuery
            // 
            this.rectQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rectQuery.Image = global::PrecisionControlApp.Properties.Resources.lkcx;
            this.rectQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rectQuery.Name = "rectQuery";
            this.rectQuery.Size = new System.Drawing.Size(44, 72);
            this.rectQuery.Text = "拉框查询封控点实时状态";
            this.rectQuery.Click += new System.EventHandler(this.rectQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 75);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queryFKD,
            this.queryJD});
            this.toolStripSplitButton1.Image = global::PrecisionControlApp.Properties.Resources.tjfkd;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(54, 72);
            this.toolStripSplitButton1.Text = "点击查询实时防疫信息";
            // 
            // queryFKD
            // 
            this.queryFKD.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.queryFKD.Image = global::PrecisionControlApp.Properties.Resources.dcd;
            this.queryFKD.Name = "queryFKD";
            this.queryFKD.Size = new System.Drawing.Size(335, 46);
            this.queryFKD.Text = "点击查询封控点实时状态";
            this.queryFKD.Click += new System.EventHandler(this.queryFKD_Click);
            // 
            // queryJD
            // 
            this.queryJD.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.queryJD.Image = global::PrecisionControlApp.Properties.Resources.dcm;
            this.queryJD.Name = "queryJD";
            this.queryJD.Size = new System.Drawing.Size(335, 46);
            this.queryJD.Text = "点击查询街道每日状态";
            this.queryJD.Click += new System.EventHandler(this.queryJD_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 75);
            // 
            // addFKD
            // 
            this.addFKD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addFKD.Image = global::PrecisionControlApp.Properties.Resources.cx;
            this.addFKD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFKD.Name = "addFKD";
            this.addFKD.Size = new System.Drawing.Size(44, 72);
            this.addFKD.Text = "增设封控点";
            this.addFKD.Click += new System.EventHandler(this.addFKD_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 75);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FKDxiugai,
            this.街道防控信息修改ToolStripMenuItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStripDropDownButton1.Image = global::PrecisionControlApp.Properties.Resources.xxxg;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(54, 72);
            this.toolStripDropDownButton1.Text = "防疫信息管理";
            // 
            // FKDxiugai
            // 
            this.FKDxiugai.Image = global::PrecisionControlApp.Properties.Resources.fkdxx;
            this.FKDxiugai.Name = "FKDxiugai";
            this.FKDxiugai.Size = new System.Drawing.Size(298, 32);
            this.FKDxiugai.Text = "封控点关闭与信息修改";
            this.FKDxiugai.Click += new System.EventHandler(this.editFKD);
            // 
            // 街道防控信息修改ToolStripMenuItem
            // 
            this.街道防控信息修改ToolStripMenuItem.Image = global::PrecisionControlApp.Properties.Resources.jdxx;
            this.街道防控信息修改ToolStripMenuItem.Name = "街道防控信息修改ToolStripMenuItem";
            this.街道防控信息修改ToolStripMenuItem.Size = new System.Drawing.Size(298, 32);
            this.街道防控信息修改ToolStripMenuItem.Text = "街道防控信息修改";
            this.街道防控信息修改ToolStripMenuItem.Click += new System.EventHandler(this.街道防控信息修改ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 75);
            // 
            // pathGuide
            // 
            this.pathGuide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pathGuide.Image = global::PrecisionControlApp.Properties.Resources.ljdh;
            this.pathGuide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pathGuide.Name = "pathGuide";
            this.pathGuide.Size = new System.Drawing.Size(44, 72);
            this.pathGuide.Text = "最优路径导航";
            this.pathGuide.Click += new System.EventHandler(this.pathGuide_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 75);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.实时街道检测信息统计ToolStripMenuItem,
            this.今日最终检测信息统计ToolStripMenuItem});
            this.toolStripDropDownButton3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStripDropDownButton3.Image = global::PrecisionControlApp.Properties.Resources.xxtj;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(54, 72);
            this.toolStripDropDownButton3.Text = "统计信息";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButton3_Click);
            // 
            // 实时街道检测信息统计ToolStripMenuItem
            // 
            this.实时街道检测信息统计ToolStripMenuItem.Image = global::PrecisionControlApp.Properties.Resources.sstj;
            this.实时街道检测信息统计ToolStripMenuItem.Name = "实时街道检测信息统计ToolStripMenuItem";
            this.实时街道检测信息统计ToolStripMenuItem.Size = new System.Drawing.Size(298, 32);
            this.实时街道检测信息统计ToolStripMenuItem.Text = "实时街道检测信息统计";
            this.实时街道检测信息统计ToolStripMenuItem.Click += new System.EventHandler(this.实时街道检测信息统计ToolStripMenuItem_Click);
            // 
            // 今日最终检测信息统计ToolStripMenuItem
            // 
            this.今日最终检测信息统计ToolStripMenuItem.Image = global::PrecisionControlApp.Properties.Resources.jrtj;
            this.今日最终检测信息统计ToolStripMenuItem.Name = "今日最终检测信息统计ToolStripMenuItem";
            this.今日最终检测信息统计ToolStripMenuItem.Size = new System.Drawing.Size(298, 32);
            this.今日最终检测信息统计ToolStripMenuItem.Text = "今日最终检测信息统计";
            this.今日最终检测信息统计ToolStripMenuItem.Click += new System.EventHandler(this.今日最终检测信息统计ToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 75);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::PrecisionControlApp.Properties.Resources.hcqfx;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(44, 72);
            this.toolStripButton3.Text = "核酸检测能力分布";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Map1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 643);
            this.panel1.TabIndex = 0;
            // 
            // Map1
            // 
            this.Map1.AutoSize = true;
            this.Map1.BackColor = System.Drawing.Color.White;
            this.Map1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Map1.Controls.Add(this.title);
            this.Map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Map1.Location = new System.Drawing.Point(0, 0);
            this.Map1.Margin = new System.Windows.Forms.Padding(64, 67, 64, 67);
            this.Map1.Name = "Map1";
            this.Map1.Size = new System.Drawing.Size(1144, 643);
            this.Map1.TabIndex = 0;
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(198)))), ((int)(((byte)(239)))));
            this.title.Controls.Add(this.labeltitle);
            this.title.Controls.Add(this.buttonbox);
            this.title.Controls.Add(this.toolbox);
            this.title.Location = new System.Drawing.Point(0, 60);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1144, 106);
            this.title.TabIndex = 6;
            this.title.Paint += new System.Windows.Forms.PaintEventHandler(this.title_Paint);
            // 
            // labeltitle
            // 
            this.labeltitle.AutoSize = true;
            this.labeltitle.BackColor = System.Drawing.Color.Transparent;
            this.labeltitle.Font = new System.Drawing.Font("微软雅黑 Light", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeltitle.Location = new System.Drawing.Point(515, 23);
            this.labeltitle.Name = "labeltitle";
            this.labeltitle.Size = new System.Drawing.Size(487, 65);
            this.labeltitle.TabIndex = 0;
            this.labeltitle.Text = "江汉区疫情精准防控";
            // 
            // buttonbox
            // 
            this.buttonbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonbox.BackColor = System.Drawing.Color.Transparent;
            this.buttonbox.Controls.Add(this.toolStrip2);
            this.buttonbox.Location = new System.Drawing.Point(937, 31);
            this.buttonbox.Name = "buttonbox";
            this.buttonbox.Size = new System.Drawing.Size(193, 57);
            this.buttonbox.TabIndex = 7;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.toolStripButton2,
            this.toolStripLabel2,
            this.toolStripDropDownButton2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(193, 57);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "工具栏";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::PrecisionControlApp.Properties.Resources.sx;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(44, 54);
            this.toolStripButton1.Text = "复位";
            this.toolStripButton1.Click += new System.EventHandler(this.minbutton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(21, 54);
            this.toolStripLabel1.Text = "   ";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::PrecisionControlApp.Properties.Resources.sf;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(44, 54);
            this.toolStripButton2.Text = "拉框查询封控点实时状态";
            this.toolStripButton2.Click += new System.EventHandler(this.normalbutton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(21, 54);
            this.toolStripLabel2.Text = "   ";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.Image = global::PrecisionControlApp.Properties.Resources.gb;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(44, 54);
            this.toolStripDropDownButton2.Text = "点击查询实时防疫信息";
            this.toolStripDropDownButton2.Click += new System.EventHandler(this.closebutton_Click);
            // 
            // toolbox
            // 
            this.toolbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.toolbox.BackColor = System.Drawing.Color.Transparent;
            this.toolbox.Controls.Add(this.toolStrip1);
            this.toolbox.Location = new System.Drawing.Point(10, 21);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(465, 75);
            this.toolbox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 643);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1144, 643);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "精准防控";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Map1.ResumeLayout(false);
            this.Map1.PerformLayout();
            this.title.ResumeLayout(false);
            this.title.PerformLayout();
            this.buttonbox.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton resetMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton rectQuery;
        private System.Windows.Forms.Panel Map1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addFKD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem queryFKD;
        private System.Windows.Forms.ToolStripMenuItem queryJD;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem FKDxiugai;
        private System.Windows.Forms.ToolStripMenuItem 街道防控信息修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton pathGuide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Panel toolbox;
        private System.Windows.Forms.Panel title;
        private System.Windows.Forms.Label labeltitle;
        private System.Windows.Forms.Panel buttonbox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem 实时街道检测信息统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 今日最终检测信息统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}

