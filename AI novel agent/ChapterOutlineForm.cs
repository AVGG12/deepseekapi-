using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class ChapterOutlineForm : Form
    {
        private TreeNode _dragNode;
        private TreeNode _lastDropTarget;
        private int _volCounter;
        private int _chCounter;
        private string _savePath;
        private bool _hasChanges;

        private static readonly string[] StatusLabels = { "⏳ 草稿", "🔄 修订中", "✅ 已完成", "✏️ AI生成中" };

        // 无参构造（设计器用）
        public ChapterOutlineForm() : this(null) { }

        public ChapterOutlineForm(string savePath)
        {
            InitializeComponent();
            _savePath = savePath;
            this.FormClosing += ChapterOutlineForm_FormClosing;

            if (!string.IsNullOrEmpty(_savePath) && File.Exists(_savePath))
                LoadFromFile();
            // 没有示例数据了，开始是空的

            UpdateStats();
        }

        // ========== 保存/加载 ==========
        private void ChapterOutlineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoSave();
        }

        private void AutoSave()
        {
            if (string.IsNullOrEmpty(_savePath) || !_hasChanges) return;
            SaveToFile();
            _hasChanges = false;
        }

        private void SaveToFile()
        {
            try
            {
                var data = new OutlineData
                {
                    VolCounter = _volCounter,
                    ChCounter = _chCounter,
                    Volumes = new List<VolumeData>(),
                    Chapters = new List<ChapterData>()
                };

                foreach (TreeNode volNode in treeOutline.Nodes)
                {
                    if (volNode.Tag is VolumeData v)
                    {
                        data.Volumes.Add(v);
                        foreach (TreeNode chNode in volNode.Nodes)
                        {
                            if (chNode.Tag is ChapterData c)
                                data.Chapters.Add(c);
                        }
                    }
                }

                var json = new JavaScriptSerializer().Serialize(data);
                File.WriteAllText(_savePath, json);
            }
            catch { }
        }

        private void LoadFromFile()
        {
            try
            {
                var json = File.ReadAllText(_savePath);
                var data = new JavaScriptSerializer().Deserialize<OutlineData>(json);
                if (data == null) return;

                _volCounter = data.VolCounter;
                _chCounter = data.ChCounter;

                // 重建卷节点
                var volDict = new Dictionary<int, TreeNode>();
                foreach (var v in data.Volumes)
                {
                    var node = new TreeNode("📁  " + v.Title)
                    {
                        Name = "vol_" + v.Id,
                        Tag = v
                    };
                    treeOutline.Nodes.Add(node);
                    volDict[v.Id] = node;
                }

                // 重建章节节点
                foreach (var c in data.Chapters)
                {
                    // 找到所属卷
                    TreeNode parent = null;
                    foreach (TreeNode volNode in treeOutline.Nodes)
                    {
                        if (volNode.Tag is VolumeData v && v.Id == c.VolumeId)
                        {
                            parent = volNode;
                            break;
                        }
                    }
                    if (parent == null) continue;

                    var chNode = new TreeNode(BuildChapterText(c.Title, c.WordCount, c.Status, c.UpdatedAt))
                    {
                        Name = "ch_" + c.Id,
                        Tag = c
                    };
                    parent.Nodes.Add(chNode);
                    parent.Expand();
                }

                _hasChanges = false;
            }
            catch { }
        }

        // ========== 数据操作 ==========
        private void MarkChanged()
        {
            _hasChanges = true;
        }

        private TreeNode AddVolume(string title, string summary)
        {
            _volCounter++;
            var node = new TreeNode("📁  " + title)
            {
                Name = "vol_" + _volCounter,
                Tag = new VolumeData { Id = _volCounter, Title = title, Summary = summary }
            };
            treeOutline.Nodes.Add(node);
            MarkChanged();
            return node;
        }

        private TreeNode AddChapter(string title, int words, int statusIdx, DateTime? date = null)
        {
            _chCounter++;
            var volNode = treeOutline.SelectedNode;
            if (volNode != null && volNode.Tag is ChapterData)
                volNode = volNode.Parent;
            if (volNode == null || volNode.Tag is VolumeData == false)
                volNode = treeOutline.Nodes.Count > 0 ? treeOutline.Nodes[treeOutline.Nodes.Count - 1] : null;
            if (volNode == null) return null;

            var vd = volNode.Tag as VolumeData;
            var dt = date ?? DateTime.Now;
            var chNode = new TreeNode(BuildChapterText(title, words, statusIdx, dt))
            {
                Name = "ch_" + _chCounter,
                Tag = new ChapterData
                {
                    Id = _chCounter,
                    VolumeId = vd?.Id ?? 0,
                    Title = title,
                    WordCount = words,
                    Status = statusIdx,
                    UpdatedAt = dt
                }
            };
            volNode.Nodes.Add(chNode);
            volNode.Expand();
            MarkChanged();
            return chNode;
        }

        private string BuildChapterText(string title, int words, int statusIdx, DateTime dt)
        {
            var status = statusIdx >= 0 && statusIdx < StatusLabels.Length ? StatusLabels[statusIdx] : "⏳ 草稿";
            var dateStr = dt == DateTime.MinValue ? "" : dt.ToString("MM/dd");
            return "      " + status + "  " + title + "    " + words.ToString("N0") + "字  " + dateStr;
        }

        private void RefreshNodeText(TreeNode node)
        {
            if (node.Tag is VolumeData v)
                node.Text = "📁  " + v.Title;
            else if (node.Tag is ChapterData c)
                node.Text = BuildChapterText(c.Title, c.WordCount, c.Status, c.UpdatedAt);
        }

        private void UpdateStats()
        {
            int totalCh = 0, totalWords = 0, done = 0, revising = 0, drafting = 0;
            foreach (TreeNode vol in treeOutline.Nodes)
            {
                foreach (TreeNode ch in vol.Nodes)
                {
                    totalCh++;
                    var cd = ch.Tag as ChapterData;
                    if (cd == null) continue;
                    totalWords += cd.WordCount;
                    if (cd.Status == 2) done++;
                    else if (cd.Status == 1) revising++;
                    else drafting++;
                }
            }
            lblStats.Text = string.Format(
                "卷: {0} | 章: {1} | ✅ 已完成: {2} | 🔄 修订中: {3} | ⏳ 草稿: {4} | 📝 总字数: {5:N0}",
                treeOutline.Nodes.Count, totalCh, done, revising, drafting, totalWords);
        }

        private void btnNewVolume_Click(object sender, EventArgs e)
        {
            var title = PromptDialog.Show("请输入卷名：", "新建卷", "第" + (treeOutline.Nodes.Count + 1) + "卷");
            if (title == null) return;
            AddVolume(title, "");
            UpdateStats();
        }

        private void btnNewChapter_Click(object sender, EventArgs e)
        {
            var volNode = treeOutline.SelectedNode;
            if (volNode != null && volNode.Tag is ChapterData)
                volNode = volNode.Parent;
            if (volNode == null || volNode.Tag is VolumeData == false)
            {
                MessageBox.Show("请先选中一个卷", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var chCount = volNode.Nodes.Count + 1;
            var title = PromptDialog.Show("请输入章节名：", "新建章", "第" + chCount + "章");
            if (title == null) return;
            AddChapter(title, 0, 0);
            UpdateStats();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var node = treeOutline.SelectedNode;
            if (node == null) return;
            if (node.Tag is VolumeData v)
            {
                var newTitle = PromptDialog.Show("请输入新的卷名：", "重命名", v.Title);
                if (newTitle == null) return;
                v.Title = newTitle;
            }
            else if (node.Tag is ChapterData c)
            {
                var newTitle = PromptDialog.Show("请输入新的章节名：", "重命名", c.Title);
                if (newTitle == null) return;
                c.Title = newTitle;
            }
            RefreshNodeText(node);
            MarkChanged();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var node = treeOutline.SelectedNode;
            if (node == null) return;
            var label = node.Tag is VolumeData ? "此卷及其所有章节" : "此章节";
            var r = MessageBox.Show("确定删除" + label + "？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                if (node.Tag is VolumeData)
                    treeOutline.Nodes.Remove(node);
                else
                    node.Remove();
                MarkChanged();
                UpdateStats();
            }
        }

        private void treeOutline_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ChapterData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // ========== 拖拽排序 ==========
        private void treeOutline_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _dragNode = e.Item as TreeNode;
            if (_dragNode != null)
                treeOutline.DoDragDrop(_dragNode, DragDropEffects.Move);
        }

        private void treeOutline_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeOutline_DragOver(object sender, DragEventArgs e)
        {
            var pt = treeOutline.PointToClient(new Point(e.X, e.Y));
            var target = treeOutline.GetNodeAt(pt);
            if (_lastDropTarget != null && _lastDropTarget != _dragNode)
                _lastDropTarget.BackColor = Color.White;
            _lastDropTarget = target;
            if (target != null && target != _dragNode)
            {
                target.BackColor = Color.LightBlue;
                if (target.Tag is VolumeData && !target.IsExpanded)
                {
                    var t = new Timer { Interval = 600 };
                    t.Tick += (s, args) => { if (_lastDropTarget == target) target.Expand(); t.Stop(); };
                    t.Start();
                }
            }
        }

        private void treeOutline_DragDrop(object sender, DragEventArgs e)
        {
            if (_lastDropTarget != null && _lastDropTarget != _dragNode)
                _lastDropTarget.BackColor = Color.White;
            var pt = treeOutline.PointToClient(new Point(e.X, e.Y));
            var target = treeOutline.GetNodeAt(pt);
            if (_dragNode == null || target == null || target == _dragNode) { _dragNode = null; return; }

            if (_dragNode.Tag is VolumeData && target.Tag is VolumeData)
            {
                int dragIdx = _dragNode.Index, targetIdx = target.Index;
                if (dragIdx == targetIdx) return;
                treeOutline.Nodes.RemoveAt(dragIdx);
                treeOutline.Nodes.Insert(targetIdx, _dragNode);
            }
            else if (_dragNode.Tag is ChapterData)
            {
                if (target.Tag is ChapterData)
                {
                    TreeNode dp = _dragNode.Parent, tp = target.Parent;
                    int di = _dragNode.Index, ti = target.Index;
                    if (dp == tp) { dp.Nodes.RemoveAt(di); dp.Nodes.Insert(ti, _dragNode); }
                    else { _dragNode.Remove(); tp.Nodes.Insert(ti, _dragNode); }
                    // 更新章节的 VolumeId
                    if (_dragNode.Tag is ChapterData cd && tp.Tag is VolumeData vd)
                        cd.VolumeId = vd.Id;
                }
                else if (target.Tag is VolumeData)
                {
                    _dragNode.Remove();
                    target.Nodes.Add(_dragNode);
                    if (_dragNode.Tag is ChapterData cd)
                        cd.VolumeId = (target.Tag as VolumeData).Id;
                    target.Expand();
                }
            }
            _dragNode = null;
            MarkChanged();
            UpdateStats();
        }

        private void treeOutline_MouseDown(object sender, MouseEventArgs e)
        {
            var node = treeOutline.GetNodeAt(e.Location);
            if (node != null) treeOutline.SelectedNode = node;
        }
    }

    // ========== 数据模型 ==========
    public class VolumeData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }

    public class ChapterData
    {
        public int Id { get; set; }
        public int VolumeId { get; set; }
        public string Title { get; set; }
        public int WordCount { get; set; }
        public int Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileName { get; set; }
        public int TargetWords { get; set; }
        public string Summary { get; set; }
    }

    public class OutlineData
    {
        public int VolCounter { get; set; }
        public int ChCounter { get; set; }
        public List<VolumeData> Volumes { get; set; } = new List<VolumeData>();
        public List<ChapterData> Chapters { get; set; } = new List<ChapterData>();
    }

    internal static class PromptDialog
    {
        public static string Show(string text, string caption, string defaultValue = "")
        {
            var form = new Form
            {
                Text = caption, FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(360, 120),
                MaximizeBox = false, MinimizeBox = false
            };
            var lbl = new Label { Text = text, Left = 12, Top = 16, Width = 330 };
            var txt = new TextBox { Text = defaultValue, Left = 12, Top = 44, Width = 330 };
            var btnOk = new Button { Text = "确定", Left = 180, Top = 78, Width = 75, DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "取消", Left = 270, Top = 78, Width = 75, DialogResult = DialogResult.Cancel };
            form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            return form.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : null;
        }
    }
}