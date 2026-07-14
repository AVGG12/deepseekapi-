using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class ProjectForm : Form
    {
        public NovelProject SelectedProject { get; private set; }
        public string GeneratedDocxPath { get; private set; }
        private List<NovelProject> _recentProjects;

        public ProjectForm()
        {
            InitializeComponent();
            LoadRecentList();
            InitBackgroundPainting();
        }

        // ========== 东方风格渐变背景 ==========
        private System.Windows.Forms.Timer _animTimer;
        private System.Random _rnd = new System.Random();
        private Particle[] _particles;

        private struct Particle
        {
            public float X, Y;
            public float SpeedX, SpeedY;
            public float Size;
            public int Alpha;
            public float Hue;
        }

        private void InitBackgroundPainting()
        {
            _particles = new Particle[30];
            for (int i = 0; i < _particles.Length; i++)
                _particles[i] = CreateRandomParticle();

            _animTimer = new System.Windows.Forms.Timer();
            _animTimer.Interval = 40;
            _animTimer.Tick += (s, e) => { AnimateParticles(); this.Invalidate(); };
            _animTimer.Start();
        }

        private Particle CreateRandomParticle()
        {
            var p = new Particle();
            p.X = (float)(_rnd.NextDouble() * this.Width);
            p.Y = (float)(_rnd.NextDouble() * this.Height);
            p.SpeedX = (float)(_rnd.NextDouble() * 0.5 - 0.25);
            p.SpeedY = (float)(_rnd.NextDouble() * 0.3 - 0.6);
            p.Size = (float)(_rnd.NextDouble() * 4 + 1.5);
            p.Alpha = _rnd.Next(40, 160);
            p.Hue = _rnd.Next(0, 2) == 0 ? _rnd.Next(280, 340) : _rnd.Next(180, 220);
            return p;
        }

        private void AnimateParticles()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                var p = _particles[i];
                p.X += p.SpeedX;
                p.Y += p.SpeedY;
                p.SpeedX += (float)(_rnd.NextDouble() * 0.02 - 0.01);
                p.SpeedY += (float)(_rnd.NextDouble() * 0.02 - 0.01);
                if (p.SpeedX > 0.6f) p.SpeedX = 0.6f;
                if (p.SpeedX < -0.6f) p.SpeedX = -0.6f;
                if (p.SpeedY > 0.4f) p.SpeedY = 0.4f;
                if (p.SpeedY < -0.8f) p.SpeedY = -0.8f;
                p.Alpha += _rnd.Next(-3, 4);
                if (p.Alpha > 180) p.Alpha = 180;
                if (p.Alpha < 20) p.Alpha = 20;
                if (p.X < -20 || p.X > this.Width + 20 || p.Y < -20 || p.Y > this.Height + 20)
                {
                    p = CreateRandomParticle();
                    p.X = (float)(_rnd.NextDouble() * this.Width);
                    p.Y = p.SpeedY > 0 ? -10 : this.Height + 10;
                }
                _particles[i] = p;
            }
        }

        private void ProjectForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            if (w <= 0 || h <= 0) return;

            // 渐变背景: 上紫 -> 中蓝 -> 下粉
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new System.Drawing.Point(0, 0), new System.Drawing.Point(0, h),
                System.Drawing.Color.FromArgb(50, 20, 80),
                System.Drawing.Color.FromArgb(20, 40, 100)))
            {
                brush.SetSigmaBellShape(0.4f);
                g.FillRectangle(brush, 0, 0, w, h);
            }

            // 底部粉色光晕
            using (var brush2 = new System.Drawing.Drawing2D.LinearGradientBrush(
                new System.Drawing.Point(0, h / 2), new System.Drawing.Point(0, h),
                System.Drawing.Color.FromArgb(0, 120, 60, 120),
                System.Drawing.Color.FromArgb(80, 180, 80, 140)))
            {
                g.FillRectangle(brush2, 0, h / 2, w, h / 2);
            }

            // 大光晕装饰
            DrawGlow(g, w * 0.2f, h * 0.3f, 180, System.Drawing.Color.FromArgb(40, 160, 100, 220));
            DrawGlow(g, w * 0.7f, h * 0.6f, 150, System.Drawing.Color.FromArgb(35, 220, 120, 180));
            DrawGlow(g, w * 0.5f, h * 0.8f, 120, System.Drawing.Color.FromArgb(30, 100, 180, 220));

            // 固定四角星
            DrawStar(g, 60, 60, 3, System.Drawing.Color.FromArgb(120, 255, 220, 255));
            DrawStar(g, w - 80, 90, 2.5f, System.Drawing.Color.FromArgb(100, 200, 230, 255));
            DrawStar(g, 120, h - 60, 2, System.Drawing.Color.FromArgb(100, 255, 200, 230));
            DrawStar(g, w - 50, h - 40, 3.5f, System.Drawing.Color.FromArgb(90, 180, 200, 255));

            // 飘浮粒子
            foreach (var p in _particles)
            {
                var c = HSLToColor(p.Hue, 0.7f, 0.9f, p.Alpha);
                float sz = p.Size;
                g.FillEllipse(new System.Drawing.SolidBrush(c), p.X - sz, p.Y - sz, sz * 2, sz * 2);
                if (sz > 2.5f)
                {
                    using (var gb = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(p.Alpha / 3, c.R, c.G, c.B)))
                        g.FillEllipse(gb, p.X - sz * 2, p.Y - sz * 2, sz * 4, sz * 4);
                }
            }
        }

        private void DrawGlow(System.Drawing.Graphics g, float x, float y, float r, System.Drawing.Color c)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(x - r, y - r, r * 2, r * 2);
            using (var brush = new System.Drawing.Drawing2D.PathGradientBrush(path))
            {
                brush.CenterColor = c;
                brush.SurroundColors = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, c) };
                g.FillEllipse(brush, x - r, y - r, r * 2, r * 2);
            }
            path.Dispose();
        }

        private void DrawStar(System.Drawing.Graphics g, float x, float y, float s, System.Drawing.Color c)
        {
            using (var pen = new System.Drawing.Pen(c, 1.5f))
            {
                g.DrawLine(pen, x - s, y, x + s, y);
                g.DrawLine(pen, x, y - s, x, y + s);
                g.DrawLine(pen, x - s * 0.6f, y - s * 0.6f, x + s * 0.6f, y + s * 0.6f);
                g.DrawLine(pen, x + s * 0.6f, y - s * 0.6f, x - s * 0.6f, y + s * 0.6f);
            }
        }

        private System.Drawing.Color HSLToColor(float h, float s, float v, int a)
        {
            int hi = (int)(h / 60) % 6;
            float f = h / 60 - (int)(h / 60);
            float pv = v * (1 - s);
            float qv = v * (1 - f * s);
            float tv = v * (1 - (1 - f) * s);
            float r = 0, g = 0, b = 0;
            switch (hi) {
                case 0: r = v; g = tv; b = pv; break;
                case 1: r = qv; g = v; b = pv; break;
                case 2: r = pv; g = v; b = tv; break;
                case 3: r = pv; g = qv; b = v; break;
                case 4: r = tv; g = pv; b = v; break;
                case 5: r = v; g = pv; b = qv; break;
            }
            return System.Drawing.Color.FromArgb(a, (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private void ProjectForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (_animTimer != null) _animTimer.Stop();
        }

        private void LoadRecentList()
        {
            _recentProjects = NovelProject.LoadRecentProjects();
            lstRecentProjects.Items.Clear();
            foreach (var p in _recentProjects)
                lstRecentProjects.Items.Add(p.Title);
        }

        private void lstRecentProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRecentProjects.SelectedIndex < 0) return;
            ShowProject(_recentProjects[lstRecentProjects.SelectedIndex]);
        }

        private void lstRecentProjects_DoubleClick(object sender, EventArgs e)
        {
            if (lstRecentProjects.SelectedIndex < 0) return;
            SelectedProject = _recentProjects[lstRecentProjects.SelectedIndex];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ShowProject(NovelProject p)
        {
            txtTitle.Text = p.Title;
            txtAuthor.Text = p.Author;
            cmbGenre.Text = p.Genre;
            txtDescription.Text = p.Description;
            lblCreatedAt.Text = "创建时间: " + (p.CreatedAt == DateTime.MinValue ? "-" : p.CreatedAt.ToString("yyyy-MM-dd HH:mm"));
            lblUpdatedAt.Text = "修改时间: " + (p.UpdatedAt == DateTime.MinValue ? "-" : p.UpdatedAt.ToString("yyyy-MM-dd HH:mm"));
            lblTotalWords.Text = "总字数: " + p.TotalWords.ToString("N0");
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            cmbGenre.SelectedIndex = -1;
            txtDescription.Clear();
            lblCreatedAt.Text = "创建时间: -";
            lblUpdatedAt.Text = "修改时间: -";
            lblTotalWords.Text = "总字数: 0";
        }

        // === 新建项目 — 弹出保存对话框让用户命名 ===
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入书名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 弹出保存对话框让用户选择路径和文件名
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "保存小说项目为 Word 文件";
                dialog.Filter = "Word 文档 (*.docx)|*.docx|所有文件 (*.*)|*.*";
                dialog.FileName = txtTitle.Text.Trim() + ".docx";
                dialog.DefaultExt = "docx";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var filePath = dialog.FileName;

                // 生成 Word 文档
                try
                {
                    CreateWordDocument(filePath, txtTitle.Text.Trim(), txtAuthor.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("生成 Word 文件失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 保存项目元数据
                var project = new NovelProject
                {
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthor.Text.Trim(),
                    Genre = cmbGenre.Text,
                    Description = txtDescription.Text.Trim(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    FilePath = filePath
                };

                // 同时保存 .novel 元数据文件（同目录、同名）
                var metaPath = Path.ChangeExtension(filePath, ".novel");
                project.FilePath = filePath; // 主文件指向 .docx
                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(project);
                File.WriteAllText(metaPath, json);

                // 更新最近列表
                _recentProjects.Insert(0, project);
                if (_recentProjects.Count > 20)
                    _recentProjects.RemoveAt(_recentProjects.Count - 1);
                NovelProject.SaveRecentProjects(_recentProjects);
                LoadRecentList();

                SelectedProject = project;
                GeneratedDocxPath = filePath;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // === 打开文件 — 默认显示所有文件 ===
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "打开小说项目";
                dialog.Filter = "支持的文件 (*.docx;*.novel)|*.docx;*.novel|Word 文档 (*.docx)|*.docx|项目文件 (*.novel)|*.novel|所有文件 (*.*)|*.*";
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = dialog.FileName;
                    var ext = Path.GetExtension(filePath).ToLower();

                    if (ext == ".novel")
                    {
                        // 加载元数据文件
                        SelectedProject = NovelProject.Load(filePath);
                    }
                    else if (ext == ".docx")
                    {
                        // 尝试加载同名的 .novel 元数据文件
                        var metaPath = Path.ChangeExtension(filePath, ".novel");
                        if (File.Exists(metaPath))
                        {
                            SelectedProject = NovelProject.Load(metaPath);
                            SelectedProject.FilePath = filePath;
                        }
                        else
                        {
                            // 没有元数据文件，以文件名创建项目
                            SelectedProject = new NovelProject
                            {
                                Title = Path.GetFileNameWithoutExtension(filePath),
                                FilePath = filePath,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            };
                        }
                    }
                    else
                    {
                        SelectedProject = new NovelProject
                        {
                            Title = Path.GetFileNameWithoutExtension(filePath),
                            FilePath = filePath
                        };
                    }

                    GeneratedDocxPath = SelectedProject.FilePath;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnOpenOther_Click(object sender, EventArgs e)
        {
            btnOpen_Click(sender, e);
        }

        // === 保存 ===
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入书名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 如果还没有文件路径，弹出保存对话框
            if (SelectedProject == null || string.IsNullOrEmpty(SelectedProject.FilePath))
            {
                btnNew_Click(sender, e);
                return;
            }

            var project = SelectedProject ?? new NovelProject();
            project.Title = txtTitle.Text.Trim();
            project.Author = txtAuthor.Text.Trim();
            project.Genre = cmbGenre.Text;
            project.Description = txtDescription.Text.Trim();
            project.UpdatedAt = DateTime.Now;

            // 更新 Word 文档内容（只更新标题）
            try
            {
                UpdateWordDocument(project.FilePath, project.Title, project.Author);
            }
            catch { /* 非关键错误, 忽略 */ }

            // 保存 .novel 元数据
            var metaPath = Path.ChangeExtension(project.FilePath, ".novel");
            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(project);
            File.WriteAllText(metaPath, json);

            SelectedProject = project;

            _recentProjects.RemoveAll(p => p != null && p.Id == project.Id);
            _recentProjects.Insert(0, project);
            if (_recentProjects.Count > 20)
                _recentProjects.RemoveAt(_recentProjects.Count - 1);
            NovelProject.SaveRecentProjects(_recentProjects);
            LoadRecentList();

            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstRecentProjects.SelectedIndex < 0) return;
            var p = _recentProjects[lstRecentProjects.SelectedIndex];
            var result = MessageBox.Show(
                "确定要删除《" + p.Title + "》？\n文件将被永久删除。",
                "确认删除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!string.IsNullOrEmpty(p.FilePath) && File.Exists(p.FilePath))
                        File.Delete(p.FilePath);
                    var metaPath = Path.ChangeExtension(p.FilePath, ".novel");
                    if (File.Exists(metaPath))
                        File.Delete(metaPath);
                }
                catch { }

                _recentProjects.RemoveAt(lstRecentProjects.SelectedIndex);
                NovelProject.SaveRecentProjects(_recentProjects);
                LoadRecentList();
                ClearForm();
            }
        }

        // === Word 文档生成（纯 .NET，无外部依赖） ===
        private void CreateWordDocument(string filePath, string title, string author)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                // [Content_Types].xml
                var ct = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                    + "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">"
                    + "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>"
                    + "<Default Extension=\"xml\" ContentType=\"application/xml\"/>"
                    + "<Override PartName=\"/word/document.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"/>"
                    + "<Override PartName=\"/word/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml\"/>"
                    + "</Types>";
                AddEntry(archive, "[Content_Types].xml", ct);

                // _rels/.rels
                var rels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                    + "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">"
                    + "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"word/document.xml\"/>"
                    + "</Relationships>";
                AddEntry(archive, "_rels/.rels", rels);

                // word/_rels/document.xml.rels
                var docRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                    + "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">"
                    + "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>"
                    + "</Relationships>";
                AddEntry(archive, "word/_rels/document.xml.rels", docRels);

                // word/styles.xml
                var stylesXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                    + "<w:styles xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\">"
                    + "<w:docDefaults>"
                    + "<w:rPrDefault><w:rPr><w:sz w:val=\"24\"/><w:szCs w:val=\"24\"/></w:rPr></w:rPrDefault>"
                    + "</w:docDefaults>"
                    + "<w:style w:type=\"paragraph\" w:styleId=\"Normal\"><w:name w:val=\"Normal\"/></w:style>"
                    + "</w:styles>";
                AddEntry(archive, "word/styles.xml", stylesXml);

                // word/document.xml
                var docXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                    + "<w:document xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\""
                    + " xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">"
                    + "<w:body>"
                    +   "<w:p>"
                    +     "<w:pPr><w:jc w:val=\"center\"/></w:pPr>"
                    +     "<w:r><w:rPr><w:sz w:val=\"48\"/></w:rPr><w:t>" + EscapeXml(title) + "</w:t></w:r>"
                    +   "</w:p>"
                    +   "<w:p>"
                    +     "<w:pPr><w:jc w:val=\"center\"/></w:pPr>"
                    +     "<w:r><w:rPr><w:sz w:val=\"24\"/><w:i/></w:rPr><w:t>作者: " + EscapeXml(author) + "</w:t></w:r>"
                    +   "</w:p>"
                    +   "<w:p><w:r><w:br/></w:r></w:p>"
                    +   "<w:p>"
                    +     "<w:r><w:rPr><w:sz w:val=\"24\"/></w:rPr><w:t>请在此开始写作...</w:t></w:r>"
                    +   "</w:p>"
                    + "</w:body>"
                    + "</w:document>";
                AddEntry(archive, "word/document.xml", docXml);
            }
        }

        private static string EscapeXml(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        private void UpdateWordDocument(string filePath, string title, string author)
        {
            if (!File.Exists(filePath)) return;
            // 重新生成（简单替换）
            CreateWordDocument(filePath, title, author);
        }

        private static void AddEntry(ZipArchive archive, string name, string content)
        {
            var entry = archive.CreateEntry(name, CompressionLevel.Optimal);
            using (var writer = new StreamWriter(entry.Open(), Encoding.UTF8))
            {
                writer.Write(content);
            }
        }
    }
}
