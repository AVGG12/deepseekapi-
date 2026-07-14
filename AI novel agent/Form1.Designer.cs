namespace AI_novel_agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("📁 项目");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("📄 章节大纲");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("✍️ 写作区");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("🌍 世界观配置");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("👤 角色卡设计");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("📤 导出");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.扩展XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslAiStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslWordCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelWriting = new System.Windows.Forms.Panel();
            this.cmbChapter = new System.Windows.Forms.ComboBox();
            this.lblChapterTitle = new System.Windows.Forms.Label();
            this.rtbWriting = new System.Windows.Forms.RichTextBox();
            this.panelAiPreview = new System.Windows.Forms.Panel();
            this.rtbAiOutput = new System.Windows.Forms.RichTextBox();
            this.btnRejectAi = new System.Windows.Forms.Button();
            this.btnConfirmAi = new System.Windows.Forms.Button();
            this.lblAiPreview = new System.Windows.Forms.Label();
            this.contextMenuWriting = new System.Windows.Forms.ContextMenuStrip();
            this.tsmiContinue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRewrite = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolish = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOutlineGen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCustomPrompt = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelWriting.SuspendLayout();
            this.panelAiPreview.SuspendLayout();
            this.contextMenuWriting.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.扩展XToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // 文件FToolStripMenuItem
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.文件FToolStripMenuItem.Text = "文件（F）";
            this.文件FToolStripMenuItem.DropDownItems.Add("保存", null, (s, e) => SaveCurrentChapter());
            this.文件FToolStripMenuItem.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            this.文件FToolStripMenuItem.DropDownItems.Add("退出", null, (s, e) => this.Close());

            // 工具TToolStripMenuItem
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.工具TToolStripMenuItem.Text = "工具（T）";
            this.工具TToolStripMenuItem.DropDownItems.Add("AI 配置", null, (s, e) => OpenAiConfig());
            this.工具TToolStripMenuItem.DropDownItems.Add("知识库管理", null, (s, e) => OpenKnowledgeBase());
            this.工具TToolStripMenuItem.DropDownItems.Add("缓存统计", null, (s, e) => ShowCacheStats());

            // 扩展XToolStripMenuItem
            this.扩展XToolStripMenuItem.Name = "扩展XToolStripMenuItem";
            this.扩展XToolStripMenuItem.Size = new System.Drawing.Size(110, 28);
            this.扩展XToolStripMenuItem.Text = "扩展（X）";

            // treeView1
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "📁 项目";
            treeNode2.Name = "节点1";
            treeNode2.Text = "📄 章节大纲";
            treeNode3.Name = "节点2";
            treeNode3.Text = "✍️ 写作区";
            treeNode4.Name = "节点3";
            treeNode4.Text = "🌍 世界观配置";
            treeNode5.Name = "节点4";
            treeNode5.Text = "👤 角色卡设计";
            treeNode6.Name = "节点5";
            treeNode6.Text = "📤 导出";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6});
            this.treeView1.Size = new System.Drawing.Size(200, 900);
            this.treeView1.TabIndex = 1;
            this.treeView1.ItemHeight = 28;
            this.treeView1.Font = new System.Drawing.Font("Microsoft YaHei", 10F);

            // splitContainer1
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1600, 900);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;

            // splitContainer1.Panel1
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);

            // splitContainer1.Panel2
            this.splitContainer1.Panel2.Controls.Add(this.panelWriting);

            // panelWriting
            this.panelWriting.Controls.Add(this.cmbChapter);
            this.panelWriting.Controls.Add(this.lblChapterTitle);
            this.panelWriting.Controls.Add(this.rtbWriting);
            this.panelWriting.Controls.Add(this.panelAiPreview);
            this.panelWriting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWriting.Location = new System.Drawing.Point(0, 0);
            this.panelWriting.Name = "panelWriting";
            this.panelWriting.Size = new System.Drawing.Size(1400, 900);
            this.panelWriting.TabIndex = 0;

            // cmbChapter
            this.cmbChapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChapter.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.cmbChapter.Location = new System.Drawing.Point(140, 12);
            this.cmbChapter.Name = "cmbChapter";
            this.cmbChapter.Size = new System.Drawing.Size(300, 28);
            this.cmbChapter.TabIndex = 3;
            this.cmbChapter.SelectedIndexChanged += new System.EventHandler(this.cmbChapter_SelectedIndexChanged);

            // lblChapterTitle
            this.lblChapterTitle.AutoSize = true;
            this.lblChapterTitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblChapterTitle.Location = new System.Drawing.Point(12, 12);
            this.lblChapterTitle.Name = "lblChapterTitle";
            this.lblChapterTitle.Size = new System.Drawing.Size(120, 22);
            this.lblChapterTitle.Text = "当前章节：";

            // rtbWriting
            this.rtbWriting.AcceptsTab = true;
            this.rtbWriting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbWriting.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.rtbWriting.Location = new System.Drawing.Point(12, 50);
            this.rtbWriting.Name = "rtbWriting";
            this.rtbWriting.Size = new System.Drawing.Size(1380, 560);
            this.rtbWriting.TabIndex = 0;
            this.rtbWriting.Text = "";
            this.rtbWriting.ContextMenuStrip = this.contextMenuWriting;
            this.rtbWriting.TextChanged += new System.EventHandler(this.rtbWriting_TextChanged);

            // panelAiPreview
            this.panelAiPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAiPreview.Controls.Add(this.rtbAiOutput);
            this.panelAiPreview.Controls.Add(this.btnRejectAi);
            this.panelAiPreview.Controls.Add(this.btnConfirmAi);
            this.panelAiPreview.Controls.Add(this.lblAiPreview);
            this.panelAiPreview.Location = new System.Drawing.Point(12, 616);
            this.panelAiPreview.Name = "panelAiPreview";
            this.panelAiPreview.Size = new System.Drawing.Size(1380, 280);
            this.panelAiPreview.TabIndex = 1;
            this.panelAiPreview.Visible = false;

            // rtbAiOutput
            this.rtbAiOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbAiOutput.BackColor = System.Drawing.Color.FromArgb(245, 245, 255);
            this.rtbAiOutput.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.rtbAiOutput.Location = new System.Drawing.Point(3, 28);
            this.rtbAiOutput.Name = "rtbAiOutput";
            this.rtbAiOutput.ReadOnly = true;
            this.rtbAiOutput.Size = new System.Drawing.Size(1374, 210);
            this.rtbAiOutput.TabIndex = 3;

            // btnRejectAi
            this.btnRejectAi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnRejectAi.Location = new System.Drawing.Point(1260, 248);
            this.btnRejectAi.Name = "btnRejectAi";
            this.btnRejectAi.Size = new System.Drawing.Size(110, 28);
            this.btnRejectAi.Text = "❌ 拒绝";
            this.btnRejectAi.Click += new System.EventHandler(this.btnRejectAi_Click);

            // btnConfirmAi
            this.btnConfirmAi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnConfirmAi.Location = new System.Drawing.Point(1140, 248);
            this.btnConfirmAi.Name = "btnConfirmAi";
            this.btnConfirmAi.Size = new System.Drawing.Size(110, 28);
            this.btnConfirmAi.Text = "✅ 确认插入";
            this.btnConfirmAi.Click += new System.EventHandler(this.btnConfirmAi_Click);

            // lblAiPreview
            this.lblAiPreview.AutoSize = true;
            this.lblAiPreview.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.lblAiPreview.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAiPreview.Location = new System.Drawing.Point(3, 6);
            this.lblAiPreview.Name = "lblAiPreview";
            this.lblAiPreview.Size = new System.Drawing.Size(150, 17);
            this.lblAiPreview.Text = "🤖 AI 输出预览：";

            // contextMenuWriting
            this.contextMenuWriting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContinue, this.tsmiExpand, this.tsmiRewrite, this.tsmiPolish,
            new System.Windows.Forms.ToolStripSeparator(),
            this.tsmiOutlineGen, this.tsmiCustomPrompt});

            this.tsmiContinue.Text = "✍️ AI 续写";
            this.tsmiContinue.Click += new System.EventHandler(this.tsmiContinue_Click);

            this.tsmiExpand.Text = "📖 AI 扩写（选中文本）";
            this.tsmiExpand.Click += new System.EventHandler(this.tsmiExpand_Click);

            this.tsmiRewrite.Text = "🔄 AI 改写（选中文本）";
            this.tsmiRewrite.Click += new System.EventHandler(this.tsmiRewrite_Click);

            this.tsmiPolish.Text = "✨ AI 润色（选中文本）";
            this.tsmiPolish.Click += new System.EventHandler(this.tsmiPolish_Click);

            this.tsmiOutlineGen.Text = "📋 生成细纲";
            this.tsmiOutlineGen.Click += new System.EventHandler(this.tsmiOutlineGen_Click);

            this.tsmiCustomPrompt.Text = "⚡ 自定义指令";
            this.tsmiCustomPrompt.Click += new System.EventHandler(this.tsmiCustomPrompt_Click);

            // saveTimer
            this.saveTimer.Interval = 60000;
            this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
            this.saveTimer.Start();

            // statusStrip1
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslAiStatus, this.tsslWordCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 932);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1600, 25);
            this.statusStrip1.TabIndex = 2;

            // tsslAiStatus
            this.tsslAiStatus.Name = "tsslAiStatus";
            this.tsslAiStatus.Size = new System.Drawing.Size(120, 20);
            this.tsslAiStatus.Text = "🔴 AI 未连接";
            this.tsslAiStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // tsslWordCount
            this.tsslWordCount.Name = "tsslWordCount";
            this.tsslWordCount.Size = new System.Drawing.Size(180, 20);
            this.tsslWordCount.Spring = true;
            this.tsslWordCount.Text = "📝 总字数: 0";
            this.tsslWordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 960);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "📖 AI 小说创作助手";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelWriting.ResumeLayout(false);
            this.panelWriting.PerformLayout();
            this.panelAiPreview.ResumeLayout(false);
            this.panelAiPreview.PerformLayout();
            this.contextMenuWriting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 扩展XToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslAiStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslWordCount;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelWriting;
        private System.Windows.Forms.ComboBox cmbChapter;
        private System.Windows.Forms.Label lblChapterTitle;
        private System.Windows.Forms.RichTextBox rtbWriting;
        private System.Windows.Forms.Panel panelAiPreview;
        private System.Windows.Forms.RichTextBox rtbAiOutput;
        private System.Windows.Forms.Button btnRejectAi;
        private System.Windows.Forms.Button btnConfirmAi;
        private System.Windows.Forms.Label lblAiPreview;
        private System.Windows.Forms.ContextMenuStrip contextMenuWriting;
        private System.Windows.Forms.ToolStripMenuItem tsmiContinue;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpand;
        private System.Windows.Forms.ToolStripMenuItem tsmiRewrite;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolish;
        private System.Windows.Forms.ToolStripMenuItem tsmiOutlineGen;
        private System.Windows.Forms.ToolStripMenuItem tsmiCustomPrompt;
        private System.Windows.Forms.Timer saveTimer;
    }
}
