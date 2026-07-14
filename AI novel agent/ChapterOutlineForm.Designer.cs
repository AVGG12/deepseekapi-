using System.Windows.Forms;

namespace AI_novel_agent
{
    partial class ChapterOutlineForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewVolume = new System.Windows.Forms.ToolStripButton();
            this.btnNewChapter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.btnCollapseAll = new System.Windows.Forms.ToolStripButton();

            this.treeOutline = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStats = new System.Windows.Forms.ToolStripStatusLabel();

            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // toolStrip1
            this.toolStrip1.Items.AddRange(new ToolStripItem[] {
                this.btnNewVolume, this.btnNewChapter, this.toolStripSeparator1,
                this.btnRename, this.btnDelete, this.toolStripSeparator2,
                this.btnExpandAll, this.btnCollapseAll
            });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(780, 25);
            this.toolStrip1.TabIndex = 0;

            this.btnNewVolume.Text = "➕ 新建卷";
            this.btnNewVolume.Click += new System.EventHandler(this.btnNewVolume_Click);

            this.btnNewChapter.Text = "📄 新建章";
            this.btnNewChapter.Click += new System.EventHandler(this.btnNewChapter_Click);

            this.btnRename.Text = "✏️ 重命名";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);

            this.btnDelete.Text = "🗑️ 删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnExpandAll.Text = "展开全部";
            this.btnExpandAll.Click += (s, e) => this.treeOutline.ExpandAll();

            this.btnCollapseAll.Text = "折叠全部";
            this.btnCollapseAll.Click += (s, e) => this.treeOutline.CollapseAll();

            // imageList
            this.imageList.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);

            // treeOutline
            this.treeOutline.Dock = DockStyle.Fill;
            this.treeOutline.ImageIndex = 0;
            this.treeOutline.ImageList = this.imageList;
            this.treeOutline.Location = new System.Drawing.Point(0, 25);
            this.treeOutline.Name = "treeOutline";
            this.treeOutline.Size = new System.Drawing.Size(780, 470);
            this.treeOutline.TabIndex = 1;
            this.treeOutline.ItemHeight = 28;
            this.treeOutline.Font = new System.Drawing.Font("Microsoft YaHei", 10F);

            // 拖拽支持
            this.treeOutline.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeOutline_ItemDrag);
            this.treeOutline.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeOutline_DragEnter);
            this.treeOutline.DragOver += new System.Windows.Forms.DragEventHandler(this.treeOutline_DragOver);
            this.treeOutline.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeOutline_DragDrop);
            this.treeOutline.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeOutline_MouseDown);
            this.treeOutline.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeOutline_NodeMouseDoubleClick);

            // statusStrip
            this.statusStrip.Items.Add(this.lblStats);
            this.statusStrip.Location = new System.Drawing.Point(0, 495);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);

            this.lblStats.Text = "卷: 0 | 章: 0 | 总字数: 0";
            this.lblStats.Spring = true;
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ChapterOutlineForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 520);
            this.Controls.Add(this.treeOutline);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "📄 章节大纲";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ToolStrip toolStrip1;
        private ToolStripButton btnNewVolume;
        private ToolStripButton btnNewChapter;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnRename;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnExpandAll;
        private ToolStripButton btnCollapseAll;
        private TreeView treeOutline;
        private ImageList imageList;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStats;
    }
}
