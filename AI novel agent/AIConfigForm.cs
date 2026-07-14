using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class AIConfigForm : Form
    {
        private readonly AiConfig _config;
        private readonly CacheManager _cache;
        private readonly DeepSeekService _service;
        private bool _modelsDetected;

        public AIConfigForm(AiConfig config, CacheManager cache)
        {
            InitializeComponent();
            _config = config;
            _cache = cache;
            _service = new DeepSeekService(config, cache);

            // 加载现有配置
            txtApiKey.Text = config.ApiKey;
            txtApiUrl.Text = config.ApiUrl;
            txtMaxTokens.Text = config.MaxTokens.ToString();
            trackTemp.Value = (int)(config.Temperature * 10);
            lblTempVal.Text = config.Temperature.ToString("F1");

            // 模型下拉 - 预置 DeepSeek V4 等常用模型
            cmbModel.Items.Add("deepseek-chat");
            cmbModel.Items.Add("deepseek-reasoner");
            cmbModel.Items.Add("deepseek-v3");
            cmbModel.Items.Add("deepseek-r1");
            cmbModel.Items.Add("deepseek-coder");
            if (!string.IsNullOrEmpty(config.Model))
            {
                if (!cmbModel.Items.Contains(config.Model))
                    cmbModel.Items.Add(config.Model);
                cmbModel.Text = config.Model;
            }
            else
            {
                cmbModel.SelectedIndex = 0;
            }

            // 上下文策略
            for (int i = 0; i < cmbContext.Items.Count; i++)
                if (cmbContext.Items[i].ToString() == config.ContextStrategy)
                    cmbContext.SelectedIndex = i;

            chkCache.Checked = config.CacheEnabled;
            UpdateCacheStats();
        }

        private void trackTemp_Scroll(object sender, EventArgs e)
        {
            lblTempVal.Text = (trackTemp.Value / 10.0).ToString("F1");
        }

        private async void btnDetect_Click(object sender, EventArgs e)
        {
            btnDetect.Enabled = false;
            lblDetectResult.Text = "⏳ 检测中...";
            lblStatus.Text = "🟡 正在连接...";

            // 临时更新配置用于检测
            _config.ApiKey = txtApiKey.Text.Trim();
            _config.ApiUrl = txtApiUrl.Text.Trim();

            try
            {
                var models = await _service.GetAvailableModels();
                cmbModel.Items.Clear();
                foreach (var m in models)
                    cmbModel.Items.Add(m);
                if (cmbModel.Items.Count > 0)
                {
                    cmbModel.SelectedIndex = 0;
                    _modelsDetected = true;
                    lblDetectResult.Text = "✅ 检测到 " + models.Count + " 个模型";
                    lblStatus.Text = "🟢 连接正常";
                }
                else
                {
                    lblDetectResult.Text = "⚠️ 未检测到模型";
                    lblStatus.Text = "🟡 连接成功但无模型";
                }
            }
            catch (Exception ex)
            {
                lblDetectResult.Text = "❌ 检测失败";
                lblStatus.Text = "🔴 连接失败: " + ex.Message;
            }
            finally
            {
                btnDetect.Enabled = true;
            }
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            _cache.Clear();
            UpdateCacheStats();
        }

        private void UpdateCacheStats()
        {
            lblCacheStats.Text = string.Format(
                "命中: {0} 次 | 未命中: {1} 次 | 命中率: {2:F1}% | 省 Tokens: {3}",
                _cache.HitCount, _cache.MissCount,
                _cache.HitRate, _cache.TotalTokensSaved);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApiKey.Text))
            {
                MessageBox.Show("请输入 API Key", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtMaxTokens.Text, out var maxTokens) || maxTokens < 1 || maxTokens > 8192)
            {
                MessageBox.Show("Max Token 需为 1-8192 的整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _config.ApiKey = txtApiKey.Text.Trim();
            _config.ApiUrl = txtApiUrl.Text.Trim();
            _config.MaxTokens = maxTokens;
            _config.Temperature = trackTemp.Value / 10.0;
            _config.ContextStrategy = cmbContext.SelectedItem?.ToString() ?? "最近5段(推荐)";
            _config.CacheEnabled = chkCache.Checked;
            if (cmbModel.SelectedItem != null)
                _config.Model = cmbModel.SelectedItem.ToString();
            else if (!string.IsNullOrWhiteSpace(cmbModel.Text))
                _config.Model = cmbModel.Text.Trim();

            _config.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}