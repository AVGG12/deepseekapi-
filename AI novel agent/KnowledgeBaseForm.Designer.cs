using System.Drawing;
using System.Windows.Forms;

namespace AI_novel_agent
{
    partial class KnowledgeBaseForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lstSettings = new System.Windows.Forms.ListBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnAddSetting = new System.Windows.Forms.Button();
            this.btnDeleteSetting = new System.Windows.Forms.Button();
            this.tabLocations = new System.Windows.Forms.TabPage();
            this.lstLocations = new System.Windows.Forms.ListBox();
            this.lblLocName = new System.Windows.Forms.Label();
            this.txtLocName = new System.Windows.Forms.TextBox();
            this.lblLocDesc = new System.Windows.Forms.Label();
            this.txtLocDesc = new System.Windows.Forms.TextBox();
            this.lblLocType = new System.Windows.Forms.Label();
            this.cmbLocType = new System.Windows.Forms.ComboBox();
            this.btnAddLocation = new System.Windows.Forms.Button();
            this.btnDeleteLocation = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Controls.Add(this.tabLocations);
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tabControl.Location = new System.Drawing.Point(14, 14);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(960, 660);
            this.tabControl.TabIndex = 0;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.lstSettings);
            this.tabSettings.Controls.Add(this.lblKey);
            this.tabSettings.Controls.Add(this.txtKey);
            this.tabSettings.Controls.Add(this.lblValue);
            this.tabSettings.Controls.Add(this.txtValue);
            this.tabSettings.Controls.Add(this.lblCategory);
            this.tabSettings.Controls.Add(this.cmbCategory);
            this.tabSettings.Controls.Add(this.btnAddSetting);
            this.tabSettings.Controls.Add(this.btnDeleteSetting);
            this.tabSettings.Location = new System.Drawing.Point(4, 40);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(952, 616);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "🌍 世界观设定";
            // 
            // lstSettings
            // 
            this.lstSettings.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstSettings.FormattingEnabled = true;
            this.lstSettings.ItemHeight = 31;
            this.lstSettings.Location = new System.Drawing.Point(14, 14);
            this.lstSettings.Name = "lstSettings";
            this.lstSettings.Size = new System.Drawing.Size(920, 345);
            this.lstSettings.TabIndex = 0;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblKey.Location = new System.Drawing.Point(14, 396);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(116, 31);
            this.lblKey.TabIndex = 1;
            this.lblKey.Text = "设定名称:";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtKey.Location = new System.Drawing.Point(136, 392);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(264, 39);
            this.txtKey.TabIndex = 2;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblValue.Location = new System.Drawing.Point(420, 396);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(116, 31);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "设定内容:";
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtValue.Location = new System.Drawing.Point(542, 392);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(388, 39);
            this.txtValue.TabIndex = 4;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblCategory.Location = new System.Drawing.Point(14, 446);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(116, 31);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "所属分类:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cmbCategory.Items.AddRange(new object[] {
            "通用",
            "历史",
            "地理",
            "魔法",
            "科技",
            "文化",
            "宗教"});
            this.cmbCategory.Location = new System.Drawing.Point(136, 438);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 39);
            this.cmbCategory.TabIndex = 6;
            // 
            // btnAddSetting
            // 
            this.btnAddSetting.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAddSetting.Location = new System.Drawing.Point(406, 443);
            this.btnAddSetting.Name = "btnAddSetting";
            this.btnAddSetting.Size = new System.Drawing.Size(130, 38);
            this.btnAddSetting.TabIndex = 7;
            this.btnAddSetting.Text = "添加";
            this.btnAddSetting.UseVisualStyleBackColor = true;
            this.btnAddSetting.Click += new System.EventHandler(this.btnAddSetting_Click);
            // 
            // btnDeleteSetting
            // 
            this.btnDeleteSetting.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDeleteSetting.Location = new System.Drawing.Point(790, 441);
            this.btnDeleteSetting.Name = "btnDeleteSetting";
            this.btnDeleteSetting.Size = new System.Drawing.Size(140, 38);
            this.btnDeleteSetting.TabIndex = 8;
            this.btnDeleteSetting.Text = "删除选中";
            this.btnDeleteSetting.UseVisualStyleBackColor = true;
            // 
            // btnImportDefaults
            // 
            this.btnImportDefaults = new System.Windows.Forms.Button();
            this.btnImportDefaults.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnImportDefaults.Location = new System.Drawing.Point(300, 440);
            this.btnImportDefaults.Name = "btnImportDefaults";
            this.btnImportDefaults.Size = new System.Drawing.Size(200, 38);
            this.btnImportDefaults.TabIndex = 10;
            this.btnImportDefaults.Text = "📥 导入默认世界观";
            this.btnImportDefaults.UseVisualStyleBackColor = true;
            this.btnImportDefaults.Click += new System.EventHandler(this.btnImportDefaults_Click);
            this.btnDeleteSetting.Click += new System.EventHandler(this.btnDeleteSetting_Click);
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.lstLocations);
            this.tabLocations.Controls.Add(this.lblLocName);
            this.tabLocations.Controls.Add(this.txtLocName);
            this.tabLocations.Controls.Add(this.lblLocDesc);
            this.tabLocations.Controls.Add(this.txtLocDesc);
            this.tabLocations.Controls.Add(this.lblLocType);
            this.tabLocations.Controls.Add(this.cmbLocType);
            this.tabLocations.Controls.Add(this.btnAddLocation);
            this.tabLocations.Controls.Add(this.btnDeleteLocation);
            this.tabLocations.Location = new System.Drawing.Point(4, 40);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Size = new System.Drawing.Size(952, 616);
            this.tabLocations.TabIndex = 1;
            this.tabLocations.Text = "📍 地点库";
            // 
            // lstLocations
            // 
            this.lstLocations.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstLocations.FormattingEnabled = true;
            this.lstLocations.ItemHeight = 31;
            this.lstLocations.Location = new System.Drawing.Point(14, 14);
            this.lstLocations.Name = "lstLocations";
            this.lstLocations.Size = new System.Drawing.Size(920, 345);
            this.lstLocations.TabIndex = 0;
            // 
            // lblLocName
            // 
            this.lblLocName.AutoSize = true;
            this.lblLocName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLocName.Location = new System.Drawing.Point(14, 396);
            this.lblLocName.Name = "lblLocName";
            this.lblLocName.Size = new System.Drawing.Size(68, 31);
            this.lblLocName.TabIndex = 1;
            this.lblLocName.Text = "名称:";
            // 
            // txtLocName
            // 
            this.txtLocName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtLocName.Location = new System.Drawing.Point(70, 392);
            this.txtLocName.Name = "txtLocName";
            this.txtLocName.Size = new System.Drawing.Size(330, 39);
            this.txtLocName.TabIndex = 2;
            // 
            // lblLocDesc
            // 
            this.lblLocDesc.AutoSize = true;
            this.lblLocDesc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLocDesc.Location = new System.Drawing.Point(420, 396);
            this.lblLocDesc.Name = "lblLocDesc";
            this.lblLocDesc.Size = new System.Drawing.Size(68, 31);
            this.lblLocDesc.TabIndex = 3;
            this.lblLocDesc.Text = "描述:";
            // 
            // txtLocDesc
            // 
            this.txtLocDesc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtLocDesc.Location = new System.Drawing.Point(470, 392);
            this.txtLocDesc.Name = "txtLocDesc";
            this.txtLocDesc.Size = new System.Drawing.Size(460, 39);
            this.txtLocDesc.TabIndex = 4;
            // 
            // lblLocType
            // 
            this.lblLocType.AutoSize = true;
            this.lblLocType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLocType.Location = new System.Drawing.Point(14, 446);
            this.lblLocType.Name = "lblLocType";
            this.lblLocType.Size = new System.Drawing.Size(68, 31);
            this.lblLocType.TabIndex = 5;
            this.lblLocType.Text = "类型:";
            // 
            // cmbLocType
            // 
            this.cmbLocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cmbLocType.Items.AddRange(new object[] {
            "城市",
            "国家",
            "大陆",
            "建筑",
            "自然",
            "秘境",
            "副本"});
            this.cmbLocType.Location = new System.Drawing.Point(80, 442);
            this.cmbLocType.Name = "cmbLocType";
            this.cmbLocType.Size = new System.Drawing.Size(200, 39);
            this.cmbLocType.TabIndex = 6;
            // 
            // btnAddLocation
            // 
            this.btnAddLocation.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAddLocation.Location = new System.Drawing.Point(300, 440);
            this.btnAddLocation.Name = "btnAddLocation";
            this.btnAddLocation.Size = new System.Drawing.Size(130, 38);
            this.btnAddLocation.TabIndex = 7;
            this.btnAddLocation.Text = "添加";
            this.btnAddLocation.UseVisualStyleBackColor = true;
            this.btnAddLocation.Click += new System.EventHandler(this.btnAddLocation_Click);
            // 
            // btnDeleteLocation
            // 
            this.btnDeleteLocation.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDeleteLocation.Location = new System.Drawing.Point(450, 440);
            this.btnDeleteLocation.Name = "btnDeleteLocation";
            this.btnDeleteLocation.Size = new System.Drawing.Size(140, 38);
            this.btnDeleteLocation.TabIndex = 8;
            this.btnDeleteLocation.Text = "删除选中";
            this.btnDeleteLocation.UseVisualStyleBackColor = true;
            this.btnDeleteLocation.Click += new System.EventHandler(this.btnDeleteLocation_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblCount.Location = new System.Drawing.Point(18, 680);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(237, 30);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "世界观条目: 0 | 地点: 0";
            // 
            // KnowledgeBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblCount);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KnowledgeBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🌍 世界观配置";
            this.tabControl.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.tabLocations.ResumeLayout(false);
            this.tabLocations.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TabControl tabControl;
        private TabPage tabSettings;
        private ListBox lstSettings;
        private Label lblKey;
        private TextBox txtKey;
        private Label lblValue;
        private TextBox txtValue;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Button btnAddSetting;
        private Button btnDeleteSetting;
        private TabPage tabLocations;
        private ListBox lstLocations;
        private Label lblLocName;
        private TextBox txtLocName;
        private Label lblLocDesc;
        private TextBox txtLocDesc;
        private Label lblLocType;
        private ComboBox cmbLocType;
        private Button btnAddLocation;
        private Button btnDeleteLocation;
        private Label lblCount;
        private System.Windows.Forms.Button btnImportDefaults;
    }
}