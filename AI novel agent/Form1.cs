using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class Form1 : Form
    {
        private NovelProject _currentProject;
        private AiConfig _aiConfig;
        private CacheManager _cacheManager;
        private DeepSeekService _aiService;
        private KnowledgeBase _knowledgeBase;
        private ContextAssembler _contextAssembler;

        // 章节内容映射: 章节ID -> 文本内容
        private Dictionary<int, string> _chapterContents = new Dictionary<int, string>();
        private int _currentChapterId = -1;
        private string _currentChapterTitle = "";
        private string _lastAiResult = "";
        private bool _isAiWorking = false;
        private bool _hasUnsavedChanges = false;

        private string OutlinePath
        {
            get
            {
                if (_currentProject == null) return null;
                var dir = Path.GetDirectoryName(_currentProject.FilePath);
                return Path.Combine(dir ?? Application.StartupPath, "outline.json");
            }
        }

        private string ProjectDir
        {
            get
            {
                if (_currentProject == null) return null;
                var dir = Path.GetDirectoryName(_currentProject.FilePath);
                return dir ?? Application.StartupPath;
            }
        }

        public Form1()
        {
            InitializeComponent();

            _aiConfig = AiConfig.Load();
            _cacheManager = new CacheManager();
            _aiService = new DeepSeekService(_aiConfig, _cacheManager);
            _knowledgeBase = new KnowledgeBase();
            _contextAssembler = new ContextAssembler(_knowledgeBase);

            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);

            UpdateAiStatus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 自动保存当前章节
            if (_hasUnsavedChanges)
                SaveCurrentChapter();
            _cacheManager.Save();
            if (_knowledgeBase != null)
                _knowledgeBase.Save();
        }

        // ========== AI 状态更新 ==========
        private void UpdateAiStatus()
        {
            if (_aiConfig == null || string.IsNullOrEmpty(_aiConfig.ApiKey))
            {
                tsslAiStatus.Text = "🔴 AI 未配置";
                tsslAiStatus.ForeColor = Color.Red;
            }
            else
            {
                tsslAiStatus.Text = "🟢 AI 已连接 (" + _aiConfig.Model + ")";
                tsslAiStatus.ForeColor = Color.Green;
            }
        }

        // ========== TreeView 导航 ==========
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains("项目"))
            {
                using (var form = new ProjectForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _currentProject = form.SelectedProject;
                        this.Text = _currentProject.Title + " - AI 小说创作助手";
                        // 设置知识库路径并加载已有数据
                        _knowledgeBase.SavePath = Path.Combine(ProjectDir ?? Application.StartupPath, "knowledge.json");
                        _knowledgeBase.Load();
                        // 填充默认世界观设定（如果尚无数据）
                        _knowledgeBase.PopulateDefaultWorldSettings();
                        LoadChapters();
                        UpdateWordCount();
                        UpdateAiStatus();
                    }
                }
            }
            else if (e.Node.Text.Contains("章节大纲"))
            {
                if (_currentProject == null)
                {
                    MessageBox.Show("请先创建一个项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var form = new ChapterOutlineForm(OutlinePath))
                {
                    form.ShowDialog();
                    // 重新加载章节列表
                    LoadChapters();
                }
            }
            else if (e.Node.Text.Contains("写作区"))
            {
                if (_currentProject == null)
                {
                    MessageBox.Show("请先创建一个项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (e.Node.Text.Contains("世界观配置"))
            {
                if (_currentProject == null)
                {
                    MessageBox.Show("请先创建一个项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenKnowledgeBase();
                _knowledgeBase.Save();
            }
            else if (e.Node.Text.Contains("角色卡设计"))
            {
                if (_currentProject == null)
                {
                    MessageBox.Show("请先创建一个项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var form = new CharacterForm(_knowledgeBase))
                {
                    form.ShowDialog();
                }
                _knowledgeBase.Save();
            }
            else if (e.Node.Text.Contains("导出"))
            {
                if (_currentProject == null)
                {
                    MessageBox.Show("请先创建一个项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SaveCurrentChapter();
                MessageBox.Show("章节内容已保存到项目目录:\n" + ProjectDir, "导出完成",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========== 章节管理 ==========
        private void LoadChapters()
        {
            cmbChapter.Items.Clear();
            _chapterContents.Clear();
            _currentChapterId = -1;

            var outlinePath = OutlinePath;
            if (outlinePath == null || !File.Exists(outlinePath)) return;

            try
            {
                var json = File.ReadAllText(outlinePath);
                var data = new System.Web.Script.Serialization.JavaScriptSerializer()
                    .Deserialize<OutlineData>(json);
                if (data == null) return;

                foreach (var ch in data.Chapters)
                {
                    int idx = cmbChapter.Items.Add(ch.Title);
                    // 加载章节内容
                    var chDir = Path.Combine(ProjectDir ?? Application.StartupPath, "chapters");
                    var chFile = Path.Combine(chDir, "ch_" + ch.Id + ".txt");
                    if (File.Exists(chFile))
                        _chapterContents[ch.Id] = File.ReadAllText(chFile);
                    else
                        _chapterContents[ch.Id] = "";
                }

                if (cmbChapter.Items.Count > 0)
                    cmbChapter.SelectedIndex = 0;
            }
            catch { }
        }

        private void cmbChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 保存当前章节
            if (_currentChapterId > 0 && _hasUnsavedChanges)
                SaveCurrentChapter();

            if (cmbChapter.SelectedIndex < 0) return;

            var outlinePath = OutlinePath;
            if (outlinePath == null || !File.Exists(outlinePath)) return;

            try
            {
                var json = File.ReadAllText(outlinePath);
                var data = new System.Web.Script.Serialization.JavaScriptSerializer()
                    .Deserialize<OutlineData>(json);
                if (data == null || cmbChapter.SelectedIndex >= data.Chapters.Count) return;

                var ch = data.Chapters[cmbChapter.SelectedIndex];
                _currentChapterId = ch.Id;
                _currentChapterTitle = ch.Title;

                // 加载内容
                string content;
                if (_chapterContents.TryGetValue(ch.Id, out content))
                    rtbWriting.Text = content ?? "";
                else
                    rtbWriting.Text = "";

                _hasUnsavedChanges = false;
            }
            catch { }
        }

        private void SaveCurrentChapter()
        {
            if (_currentProject == null || _currentChapterId < 0) return;

            try
            {
                var chDir = Path.Combine(ProjectDir ?? Application.StartupPath, "chapters");
                Directory.CreateDirectory(chDir);
                var chFile = Path.Combine(chDir, "ch_" + _currentChapterId + ".txt");
                File.WriteAllText(chFile, rtbWriting.Text);
                _chapterContents[_currentChapterId] = rtbWriting.Text;
                _hasUnsavedChanges = false;

                // 更新字数
                UpdateWordCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateWordCount()
        {
            int total = 0;
            foreach (var kv in _chapterContents)
                total += kv.Value?.Length ?? 0;
            if (_currentProject != null)
            {
                _currentProject.TotalWords = total;
                tsslWordCount.Text = "📝 总字数: " + total.ToString("N0");
            }
        }

        // ========== 自动保存 ==========
        private void rtbWriting_TextChanged(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
                SaveCurrentChapter();
        }

        // ========== 工具菜单 ==========
        private void OpenAiConfig()
        {
            using (var form = new AIConfigForm(_aiConfig, _cacheManager))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _aiConfig.Save();
                    _aiService = new DeepSeekService(_aiConfig, _cacheManager);
                    UpdateAiStatus();
                }
            }
        }

        private void OpenKnowledgeBase()
        {
            using (var form = new KnowledgeBaseForm(_knowledgeBase))
            {
                form.ShowDialog();
            }
        }

        private void ShowCacheStats()
        {
            string msg = string.Format(
                "缓存命中: {0} 次\n缓存未命中: {1} 次\n命中率: {2:F1}%\n节省 Tokens: {3}",
                _cacheManager.HitCount, _cacheManager.MissCount,
                _cacheManager.HitRate, _cacheManager.TotalTokensSaved);
            MessageBox.Show(msg, "缓存统计", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========== AI 右键菜单处理 ==========
        private async void tsmiContinue_Click(object sender, EventArgs e)
        {
            await DoAiAction("续写", BuildContinuePrompt());
        }

        private async void tsmiExpand_Click(object sender, EventArgs e)
        {
            var selected = rtbWriting.SelectedText;
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("请先选中要扩写的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await DoAiAction("扩写", BuildExpandPrompt(selected));
        }

        private async void tsmiRewrite_Click(object sender, EventArgs e)
        {
            var selected = rtbWriting.SelectedText;
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("请先选中要改写的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await DoAiAction("改写", BuildRewritePrompt(selected));
        }

        private async void tsmiPolish_Click(object sender, EventArgs e)
        {
            var selected = rtbWriting.SelectedText;
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("请先选中要润色的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await DoAiAction("润色", BuildPolishPrompt(selected));
        }

        private async void tsmiOutlineGen_Click(object sender, EventArgs e)
        {
            await DoAiAction("生成细纲", BuildOutlineGenPrompt());
        }

        private async void tsmiCustomPrompt_Click(object sender, EventArgs e)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "请输入AI写作指令：", "自定义指令", "",
                -1, -1);
            if (string.IsNullOrEmpty(input)) return;
            await DoAiAction("自定义", BuildCustomPrompt(input));
        }

        // ========== AI 请求核心 ==========
        private async Task DoAiAction(string actionName, string userPrompt)
        {
            if (_aiConfig == null || string.IsNullOrEmpty(_aiConfig.ApiKey))
            {
                MessageBox.Show("请先配置 AI（工具 -> AI 配置）", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isAiWorking)
            {
                MessageBox.Show("AI 正在工作中，请稍候...", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAiWorking = true;
            tsslAiStatus.Text = "🟡 AI " + actionName + "中...";
            tsslAiStatus.ForeColor = Color.Orange;

            try
            {
                // 构建系统提示 — 严格模式
                var systemPrompt = "你是一位专业的小说创作助手。你必须严格遵守以下规则："
                    + "1. 严格按照【上下文】中的【世界观设定】创作，不得偏离或添加矛盾内容。"
                    + "2. 严格按照【角色】的性格、外貌、背景、能力描写角色，不得OOC。"
                    + "3. 严格遵守【约束】中的限制（如已死亡角色不能出场、已消耗物品不能使用）。"
                    + "4. 严格遵循【事件】记录，已发生的事件不能改变或忽略。"
                    + "5. 输出格式为纯文本小说内容，无需额外解释。";

                // 添加上下文
                if (_currentChapterId > 0 && _knowledgeBase != null)
                {
                    var context = _contextAssembler.BuildContext(_currentChapterId, rtbWriting.Text, ProjectDir);
                    if (!string.IsNullOrEmpty(context))
                        systemPrompt += "\n\n【上下文】\n" + context;
                }

                // 发送请求
                var result = await Task.Run(() =>
                    _aiService.SendRequest(systemPrompt, userPrompt));

                if (result != null && !string.IsNullOrEmpty(result.Item1))
                {
                    _lastAiResult = result.Item1;
                    ShowAiPreview(result.Item1, actionName);

                    if (result.Item2 > 0)
                    {
                        tsslAiStatus.Text = "✅ " + actionName + "完成 (消耗 " + result.Item2 + " tokens)";
                        tsslAiStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        tsslAiStatus.Text = "✅ " + actionName + "完成 (缓存命中)";
                        tsslAiStatus.ForeColor = Color.Green;
                    }
                }
                else
                {
                    tsslAiStatus.Text = "🔴 " + actionName + "失败: 无返回内容";
                    tsslAiStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                tsslAiStatus.Text = "🔴 " + actionName + "失败: " + ex.Message;
                tsslAiStatus.ForeColor = Color.Red;
                MessageBox.Show("AI 请求失败: " + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isAiWorking = false;
            }
        }
        private void btnPrevChapter_Click(object sender, EventArgs e)
        {
            if (_currentProject == null) return;
            if (_hasUnsavedChanges) SaveCurrentChapter();
            if (cmbChapter.SelectedIndex > 0)
                cmbChapter.SelectedIndex--;
        }

        private void btnNextChapter_Click(object sender, EventArgs e)
        {
            if (_currentProject == null) return;
            if (_hasUnsavedChanges) SaveCurrentChapter();
            if (cmbChapter.SelectedIndex < cmbChapter.Items.Count - 1)
                cmbChapter.SelectedIndex++;
            else
                CreateNewChapter();
        }

        private void CreateNewChapter()
        {
            var outlinePath = OutlinePath;
            if (outlinePath == null || !File.Exists(outlinePath)) return;
            try
            {
                var json = File.ReadAllText(outlinePath);
                var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                var data = ser.Deserialize<OutlineData>(json);
                if (data == null) return;
                int nextId = 1 + (data.Chapters.Count > 0 ? data.Chapters.Max(c => c.Id) : 0);
                int nextNum = data.Chapters.Count + 1;
                var newCh = new ChapterData();
                newCh.Id = nextId;
                newCh.Title = "\u7B2C" + nextNum + "\u7AE0";
                data.Chapters.Add(newCh);
                File.WriteAllText(outlinePath, ser.Serialize(data));
                LoadChapters();
                for (int i = 0; i < cmbChapter.Items.Count; i++)
                {
                    if (cmbChapter.Items[i].ToString() == newCh.Title)
                    { cmbChapter.SelectedIndex = i; break; }
                }
            }
            catch { }
        }

        private void ShowAiPreview(string content, string actionName)
        {
            rtbAiOutput.Text = content;
            lblAiPreview.Text = "🤖 AI 输出预览（" + actionName + "）";
            panelAiPreview.Visible = true;
        }

        private void btnConfirmAi_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastAiResult))
            {
                // 在光标位置插入或追加
                var selStart = rtbWriting.SelectionStart;
                if (selStart >= 0 && selStart <= rtbWriting.Text.Length)
                {
                    rtbWriting.SelectedText = _lastAiResult;
                }
                else
                {
                    rtbWriting.AppendText(_lastAiResult);
                }
                _lastAiResult = "";
                panelAiPreview.Visible = false;
            }
        }

        private void btnRejectAi_Click(object sender, EventArgs e)
        {
            _lastAiResult = "";
            panelAiPreview.Visible = false;
        }

        // ========== Prompt 构造 ==========
        private string BuildContinuePrompt()
        {
            var text = rtbWriting.Text;
            var tail = text.Length > 800 ? text.Substring(text.Length - 800) : text;
            return "请根据以下内容续写小说。保持风格一致，自然衔接。\n\n" + tail;
        }

        private string BuildExpandPrompt(string selected)
        {
            return "请扩写以下文本，增加细节描写、心理活动和场景氛围。保持风格一致。\n\n" + selected;
        }

        private string BuildRewritePrompt(string selected)
        {
            return "请用不同的表达方式改写以下文本。保持原意，提升文学性。\n\n" + selected;
        }

        private string BuildPolishPrompt(string selected)
        {
            return "请润色以下文本，修正语法错误，优化表达，提升文笔流畅度。\n\n" + selected;
        }

        private string BuildOutlineGenPrompt()
        {
            var text = rtbWriting.Text;
            var preview = text.Length > 1000 ? text.Substring(0, 1000) + "..." : text;
            if (string.IsNullOrEmpty(preview))
                return "请为当前章节生成详细的写作提纲（1000字左右），包含：\n1. 本章核心冲突\n2. 主要人物出场\n3. 情节推进步骤\n4. 结尾悬念设置";
            return "基于以下内容，为当前章节生成后续写作细纲：\n\n" + preview;
        }

        private string BuildCustomPrompt(string instruction)
        {
            var text = rtbWriting.Text;
            var ctx = text.Length > 500 ? text.Substring(0, 500) + "..." : text;
            return "【用户指令】" + instruction + "\n\n【当前文本】\n" + ctx;
        }
    }
}
