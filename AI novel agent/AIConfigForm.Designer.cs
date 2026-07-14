namespace AI_novel_agent
{
    partial class AIConfigForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.gbApi = new System.Windows.Forms.GroupBox();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblApiUrl = new System.Windows.Forms.Label();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.lblMaxTokens = new System.Windows.Forms.Label();
            this.txtMaxTokens = new System.Windows.Forms.TextBox();

            this.gbDetect = new System.Windows.Forms.GroupBox();
            this.btnDetect = new System.Windows.Forms.Button();
            this.lblDetectResult = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.cmbModel = new System.Windows.Forms.ComboBox();

            this.gbParams = new System.Windows.Forms.GroupBox();
            this.lblTemp = new System.Windows.Forms.Label();
            this.trackTemp = new System.Windows.Forms.TrackBar();
            this.lblTempVal = new System.Windows.Forms.Label();
            this.lblContext = new System.Windows.Forms.Label();
            this.cmbContext = new System.Windows.Forms.ComboBox();

            this.gbCache = new System.Windows.Forms.GroupBox();
            this.chkCache = new System.Windows.Forms.CheckBox();
            this.lblCacheStats = new System.Windows.Forms.Label();
            this.btnClearCache = new System.Windows.Forms.Button();

            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.trackTemp)).BeginInit();
            this.SuspendLayout();

            // gbApi — API 配置
            this.gbApi.Controls.Add(this.lblApiKey);
            this.gbApi.Controls.Add(this.txtApiKey);
            this.gbApi.Controls.Add(this.lblApiUrl);
            this.gbApi.Controls.Add(this.txtApiUrl);
            this.gbApi.Controls.Add(this.lblMaxTokens);
            this.gbApi.Controls.Add(this.txtMaxTokens);
            this.gbApi.Location = new System.Drawing.Point(12, 12);
            this.gbApi.Size = new System.Drawing.Size(520, 100);
            this.gbApi.Text = "API 配置";

            this.lblApiKey.AutoSize = true; this.lblApiKey.Location = new System.Drawing.Point(14, 24);
            this.lblApiKey.Text = "API Key:";
            this.txtApiKey.Location = new System.Drawing.Point(80, 20); this.txtApiKey.Size = new System.Drawing.Size(420, 21);
            this.txtApiKey.UseSystemPasswordChar = true;

            this.lblApiUrl.AutoSize = true; this.lblApiUrl.Location = new System.Drawing.Point(14, 52);
            this.lblApiUrl.Text = "API 地址:";
            this.txtApiUrl.Location = new System.Drawing.Point(80, 48); this.txtApiUrl.Size = new System.Drawing.Size(420, 21);
            this.txtApiUrl.Text = "https://api.deepseek.com";

            this.lblMaxTokens.AutoSize = true; this.lblMaxTokens.Location = new System.Drawing.Point(14, 78);
            this.lblMaxTokens.Text = "Max Token:";
            this.txtMaxTokens.Location = new System.Drawing.Point(80, 74); this.txtMaxTokens.Size = new System.Drawing.Size(100, 21);
            this.txtMaxTokens.Text = "4096";

            // gbDetect — 模型检测
            this.gbDetect.Controls.Add(this.btnDetect);
            this.gbDetect.Controls.Add(this.lblDetectResult);
            this.gbDetect.Controls.Add(this.lblModel);
            this.gbDetect.Controls.Add(this.cmbModel);
            this.gbDetect.Location = new System.Drawing.Point(12, 118);
            this.gbDetect.Size = new System.Drawing.Size(520, 80);
            this.gbDetect.Text = "模型检测";

            this.btnDetect.Location = new System.Drawing.Point(18, 22);
            this.btnDetect.Size = new System.Drawing.Size(110, 26);
            this.btnDetect.Text = "检测可用模型";
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);

            this.lblDetectResult.AutoSize = true; this.lblDetectResult.Location = new System.Drawing.Point(140, 28);
            this.lblDetectResult.Text = "⏳ 未检测";

            this.lblModel.AutoSize = true; this.lblModel.Location = new System.Drawing.Point(14, 56);
            this.lblModel.Text = "当前使用:";
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbModel.Location = new System.Drawing.Point(80, 52);
            this.cmbModel.Size = new System.Drawing.Size(280, 20);

            // gbParams — 写作参数
            this.gbParams.Controls.Add(this.lblTemp);
            this.gbParams.Controls.Add(this.trackTemp);
            this.gbParams.Controls.Add(this.lblTempVal);
            this.gbParams.Controls.Add(this.lblContext);
            this.gbParams.Controls.Add(this.cmbContext);
            this.gbParams.Location = new System.Drawing.Point(12, 204);
            this.gbParams.Size = new System.Drawing.Size(520, 100);
            this.gbParams.Text = "写作参数";

            this.lblTemp.Text = "创造性(Temperature):"; this.lblTemp.Location = new System.Drawing.Point(14, 24);
            this.trackTemp.Location = new System.Drawing.Point(140, 20); this.trackTemp.Size = new System.Drawing.Size(200, 42);
            this.trackTemp.Minimum = 1; this.trackTemp.Maximum = 20;

            this.lblTempVal.AutoSize = true; this.lblTempVal.Location = new System.Drawing.Point(350, 24);

            this.lblContext.Text = "上下文策略:"; this.lblContext.Location = new System.Drawing.Point(14, 68);
            this.cmbContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContext.Items.AddRange(new object[] { "仅当前段", "最近5段(推荐)", "最近10段", "最近3章", "整章" });
            this.cmbContext.Location = new System.Drawing.Point(80, 66);
            this.cmbContext.Size = new System.Drawing.Size(180, 20);
            this.cmbContext.SelectedIndex = 1;

            // gbCache — 缓存统计
            this.gbCache.Controls.Add(this.chkCache);
            this.gbCache.Controls.Add(this.lblCacheStats);
            this.gbCache.Controls.Add(this.btnClearCache);
            this.gbCache.Location = new System.Drawing.Point(12, 310);
            this.gbCache.Size = new System.Drawing.Size(520, 68);
            this.gbCache.Text = "缓存统计";

            this.chkCache.AutoSize = true; this.chkCache.Location = new System.Drawing.Point(18, 20);
            this.chkCache.Text = "启用请求缓存";
            this.chkCache.Checked = true;

            this.lblCacheStats.AutoSize = true; this.lblCacheStats.Location = new System.Drawing.Point(18, 44);
            this.lblCacheStats.Size = new System.Drawing.Size(300, 12);
            this.lblCacheStats.Text = "命中: 0 | 未命中: 0 | 命中率: 0% | 省 Tokens: 0";

            this.btnClearCache.Location = new System.Drawing.Point(400, 16);
            this.btnClearCache.Size = new System.Drawing.Size(100, 26);
            this.btnClearCache.Text = "清空缓存";
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);

            // lblStatus — 状态
            this.lblStatus.AutoSize = true; this.lblStatus.Location = new System.Drawing.Point(18, 394);
            this.lblStatus.Size = new System.Drawing.Size(200, 12);
            this.lblStatus.Text = "🔴 状态: 未连接";

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(324, 388);
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(410, 388);
            this.btnSave.Size = new System.Drawing.Size(120, 28);
            this.btnSave.Text = "保存配置";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // AIConfigForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(546, 430);
            this.Controls.Add(this.gbApi);
            this.Controls.Add(this.gbDetect);
            this.Controls.Add(this.gbParams);
            this.Controls.Add(this.gbCache);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AI 配置";

            ((System.ComponentModel.ISupportInitialize)(this.trackTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox gbApi;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblApiUrl;
        private System.Windows.Forms.TextBox txtApiUrl;
        private System.Windows.Forms.Label lblMaxTokens;
        private System.Windows.Forms.TextBox txtMaxTokens;

        private System.Windows.Forms.GroupBox gbDetect;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Label lblDetectResult;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.ComboBox cmbModel;

        private System.Windows.Forms.GroupBox gbParams;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.TrackBar trackTemp;
        private System.Windows.Forms.Label lblTempVal;
        private System.Windows.Forms.Label lblContext;
        private System.Windows.Forms.ComboBox cmbContext;

        private System.Windows.Forms.GroupBox gbCache;
        private System.Windows.Forms.CheckBox chkCache;
        private System.Windows.Forms.Label lblCacheStats;
        private System.Windows.Forms.Button btnClearCache;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
