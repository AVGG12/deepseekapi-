using System.Drawing;
using System.Windows.Forms;

namespace AI_novel_agent
{
    partial class CharacterForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lstCharacters = new System.Windows.Forms.ListBox();
            this.gbInfo = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            // 第一行
            this.lblName = new System.Windows.Forms.Label(); this.txtName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label(); this.txtTitle = new System.Windows.Forms.TextBox();

            // 第二行
            this.lblAliases = new System.Windows.Forms.Label(); this.txtAliases = new System.Windows.Forms.TextBox();
            this.lblFaction = new System.Windows.Forms.Label(); this.txtFaction = new System.Windows.Forms.TextBox();

            // 第三行
            this.lblRealm = new System.Windows.Forms.Label(); this.txtRealm = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label(); this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblDeathChapter = new System.Windows.Forms.Label(); this.numDeathChapter = new System.Windows.Forms.NumericUpDown();

            // 第四行：性格标签
            this.lblPersonality = new System.Windows.Forms.Label();
            this.clbPersonality = new System.Windows.Forms.CheckedListBox();
            this.txtPersonality = new System.Windows.Forms.TextBox();

            // 第五行：外貌 + 头发
            this.lblAppearance = new System.Windows.Forms.Label(); this.txtAppearance = new System.Windows.Forms.TextBox();
            this.lblHair = new System.Windows.Forms.Label(); this.txtHair = new System.Windows.Forms.TextBox();

            // 第六行：爱好
            this.lblHobbies = new System.Windows.Forms.Label(); this.txtHobbies = new System.Windows.Forms.TextBox();

            // 第七行：所在地
            this.lblLocation = new System.Windows.Forms.Label(); this.txtLocation = new System.Windows.Forms.TextBox();

            // 第八行：背景
            this.lblBackground = new System.Windows.Forms.Label(); this.txtBackground = new System.Windows.Forms.TextBox();

            // 第九行：关系
            this.lblRelations = new System.Windows.Forms.Label(); this.txtRelations = new System.Windows.Forms.TextBox();

            // 第十行：备注
            this.lblNotes = new System.Windows.Forms.Label(); this.txtNotes = new System.Windows.Forms.TextBox();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numDeathChapter)).BeginInit();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();

            // lstCharacters
            this.lstCharacters.Font = new Font("Microsoft YaHei", 10F);
            this.lstCharacters.FormattingEnabled = true;
            this.lstCharacters.ItemHeight = 20;
            this.lstCharacters.Location = new Point(12, 12);
            this.lstCharacters.Size = new Size(280, 520);
            this.lstCharacters.TabIndex = 0;
            this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);

            // gbInfo
            this.gbInfo.AutoScroll = true;
            this.gbInfo.Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold);
            this.gbInfo.Location = new Point(300, 12);
            this.gbInfo.Size = new Size(760, 520);
            // Title via label
            this.gbInfo.TabIndex = 1;

            // === 第一行: 名称 + 称号 ===
            this.lblName.Font = new Font("Microsoft YaHei", 10F);
            this.lblName.Location = new Point(20, 30);
            this.lblName.Size = new Size(60, 28);
            this.lblName.TextAlign = ContentAlignment.MiddleLeft;
            this.lblName.Text = "名称:";
            this.txtName.Font = new Font("Microsoft YaHei", 10F);
            this.txtName.Location = new Point(80, 28);
            this.txtName.Size = new Size(200, 28);

            this.lblTitle.Font = new Font("Microsoft YaHei", 10F);
            this.lblTitle.Location = new Point(300, 30);
            this.lblTitle.Size = new Size(60, 28);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.Text = "称号:";
            this.txtTitle.Font = new Font("Microsoft YaHei", 10F);
            this.txtTitle.Location = new Point(360, 28);
            this.txtTitle.Size = new Size(370, 28);

            // === 第二行: 别名 + 势力 ===
            this.lblAliases.Font = new Font("Microsoft YaHei", 10F);
            this.lblAliases.Location = new Point(20, 66);
            this.lblAliases.Size = new Size(60, 28);
            this.lblAliases.TextAlign = ContentAlignment.MiddleLeft;
            this.lblAliases.Text = "别名:";
            this.txtAliases.Font = new Font("Microsoft YaHei", 10F);
            this.txtAliases.Location = new Point(80, 64);
            this.txtAliases.Size = new Size(200, 28);

            this.lblFaction.Font = new Font("Microsoft YaHei", 10F);
            this.lblFaction.Location = new Point(300, 66);
            this.lblFaction.Size = new Size(60, 28);
            this.lblFaction.TextAlign = ContentAlignment.MiddleLeft;
            this.lblFaction.Text = "势力:";
            this.txtFaction.Font = new Font("Microsoft YaHei", 10F);
            this.txtFaction.Location = new Point(360, 64);
            this.txtFaction.Size = new Size(370, 28);

            // === 第三行: 境界 + 状态 + 死亡章节 ===
            this.lblRealm.Font = new Font("Microsoft YaHei", 10F);
            this.lblRealm.Location = new Point(20, 102);
            this.lblRealm.Size = new Size(60, 28);
            this.lblRealm.TextAlign = ContentAlignment.MiddleLeft;
            this.lblRealm.Text = "境界:";
            this.txtRealm.Font = new Font("Microsoft YaHei", 10F);
            this.txtRealm.Location = new Point(80, 100);
            this.txtRealm.Size = new Size(200, 28);

            this.lblStatus.Font = new Font("Microsoft YaHei", 10F);
            this.lblStatus.Location = new Point(300, 102);
            this.lblStatus.Size = new Size(60, 28);
            this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.lblStatus.Text = "状态:";
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new Font("Microsoft YaHei", 10F);
            this.cmbStatus.Location = new Point(360, 100);
            this.cmbStatus.Size = new Size(160, 28);
            this.cmbStatus.Items.AddRange(new object[] { "存活", "死亡", "失踪", "囚禁", "转世" });

            this.lblDeathChapter.Font = new Font("Microsoft YaHei", 10F);
            this.lblDeathChapter.Location = new Point(540, 102);
            this.lblDeathChapter.Size = new Size(80, 28);
            this.lblDeathChapter.TextAlign = ContentAlignment.MiddleLeft;
            this.lblDeathChapter.Text = "死亡章节:";
            this.numDeathChapter.Font = new Font("Microsoft YaHei", 10F);
            this.numDeathChapter.Location = new Point(620, 100);
            this.numDeathChapter.Size = new Size(100, 28);

            // === 第四行: 性格标签 ===
            this.lblPersonality.Font = new Font("Microsoft YaHei", 10F);
            this.lblPersonality.Location = new Point(20, 140);
            this.lblPersonality.Size = new Size(60, 60);
            this.lblPersonality.TextAlign = ContentAlignment.MiddleLeft;
            this.lblPersonality.Text = "性格:";

            this.clbPersonality.Font = new Font("Microsoft YaHei", 9F);
            this.clbPersonality.CheckOnClick = true;
            this.clbPersonality.Location = new Point(80, 138);
            this.clbPersonality.Size = new Size(650, 60);
            this.clbPersonality.ColumnWidth = 70;
            this.clbPersonality.MultiColumn = true;
            this.clbPersonality.ItemCheck += new ItemCheckEventHandler(this.clbPersonality_ItemCheck);

            this.txtPersonality.Font = new Font("Microsoft YaHei", 9F);
            this.txtPersonality.Location = new Point(80, 204);
            this.txtPersonality.Size = new Size(650, 24);
            this.txtPersonality.ReadOnly = true;
            this.txtPersonality.Text = "";
            this.txtPersonality.BackColor = Color.WhiteSmoke;

            // === 第五行: 外貌 + 头发 ===
            this.lblAppearance.Font = new Font("Microsoft YaHei", 10F);
            this.lblAppearance.Location = new Point(20, 240);
            this.lblAppearance.Size = new Size(60, 28);
            this.lblAppearance.TextAlign = ContentAlignment.MiddleLeft;
            this.lblAppearance.Text = "外貌:";
            this.txtAppearance.Font = new Font("Microsoft YaHei", 10F);
            this.txtAppearance.Location = new Point(80, 238);
            this.txtAppearance.Size = new Size(270, 28);

            this.lblHair.Font = new Font("Microsoft YaHei", 10F);
            this.lblHair.Location = new Point(370, 240);
            this.lblHair.Size = new Size(60, 28);
            this.lblHair.TextAlign = ContentAlignment.MiddleLeft;
            this.lblHair.Text = "头发:";
            this.txtHair.Font = new Font("Microsoft YaHei", 10F);
            this.txtHair.Location = new Point(430, 238);
            this.txtHair.Size = new Size(300, 28);

            // === 第六行: 爱好 ===
            this.lblHobbies.Font = new Font("Microsoft YaHei", 10F);
            this.lblHobbies.Location = new Point(20, 276);
            this.lblHobbies.Size = new Size(60, 28);
            this.lblHobbies.TextAlign = ContentAlignment.MiddleLeft;
            this.lblHobbies.Text = "爱好:";
            this.txtHobbies.Font = new Font("Microsoft YaHei", 10F);
            this.txtHobbies.Location = new Point(80, 274);
            this.txtHobbies.Size = new Size(650, 28);

            // === 第七行: 所在地 ===
            this.lblLocation.Font = new Font("Microsoft YaHei", 10F);
            this.lblLocation.Location = new Point(20, 312);
            this.lblLocation.Size = new Size(60, 28);
            this.lblLocation.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLocation.Text = "所在地:";
            this.txtLocation.Font = new Font("Microsoft YaHei", 10F);
            this.txtLocation.Location = new Point(80, 310);
            this.txtLocation.Size = new Size(650, 28);

            // === 第八行: 背景 ===
            this.lblBackground.Font = new Font("Microsoft YaHei", 10F);
            this.lblBackground.Location = new Point(20, 350);
            this.lblBackground.Size = new Size(60, 24);
            this.lblBackground.Text = "背景:";
            this.txtBackground.Font = new Font("Microsoft YaHei", 10F);
            this.txtBackground.Location = new Point(80, 346);
            this.txtBackground.Size = new Size(650, 60);
            this.txtBackground.Multiline = true;
            this.txtBackground.ScrollBars = ScrollBars.Vertical;

            // === 第九行: 关系 ===
            this.lblRelations.Font = new Font("Microsoft YaHei", 10F);
            this.lblRelations.Location = new Point(20, 416);
            this.lblRelations.Size = new Size(60, 24);
            this.lblRelations.Text = "关系:";
            this.txtRelations.Font = new Font("Microsoft YaHei", 10F);
            this.txtRelations.Location = new Point(80, 412);
            this.txtRelations.Size = new Size(650, 60);
            this.txtRelations.Multiline = true;
            this.txtRelations.ScrollBars = ScrollBars.Vertical;

            // === 第十行: 备注 ===
            this.lblNotes.Font = new Font("Microsoft YaHei", 10F);
            this.lblNotes.Location = new Point(20, 482);
            this.lblNotes.Size = new Size(60, 24);
            this.lblNotes.Text = "备注:";
            this.txtNotes.Font = new Font("Microsoft YaHei", 10F);
            this.txtNotes.Location = new Point(80, 478);
            this.txtNotes.Size = new Size(650, 60);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = ScrollBars.Vertical;

var lblGbTitle = new System.Windows.Forms.Label();
            lblGbTitle.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold);
            lblGbTitle.Location = new System.Drawing.Point(10, 5);
            lblGbTitle.Size = new System.Drawing.Size(120, 24);
            lblGbTitle.Text = "角色信息";
            this.gbInfo.Controls.Add(lblGbTitle);

            // gbInfo controls
            this.gbInfo.Controls.Add(this.lblName); this.gbInfo.Controls.Add(this.txtName);
            this.gbInfo.Controls.Add(this.lblTitle); this.gbInfo.Controls.Add(this.txtTitle);
            this.gbInfo.Controls.Add(this.lblAliases); this.gbInfo.Controls.Add(this.txtAliases);
            this.gbInfo.Controls.Add(this.lblFaction); this.gbInfo.Controls.Add(this.txtFaction);
            this.gbInfo.Controls.Add(this.lblRealm); this.gbInfo.Controls.Add(this.txtRealm);
            this.gbInfo.Controls.Add(this.lblStatus); this.gbInfo.Controls.Add(this.cmbStatus);
            this.gbInfo.Controls.Add(this.lblDeathChapter); this.gbInfo.Controls.Add(this.numDeathChapter);
            this.gbInfo.Controls.Add(this.lblPersonality); this.gbInfo.Controls.Add(this.clbPersonality);
            this.gbInfo.Controls.Add(this.txtPersonality);
            this.gbInfo.Controls.Add(this.lblAppearance); this.gbInfo.Controls.Add(this.txtAppearance);
            this.gbInfo.Controls.Add(this.lblHair); this.gbInfo.Controls.Add(this.txtHair);
            this.gbInfo.Controls.Add(this.lblHobbies); this.gbInfo.Controls.Add(this.txtHobbies);
            this.gbInfo.Controls.Add(this.lblLocation); this.gbInfo.Controls.Add(this.txtLocation);
            this.gbInfo.Controls.Add(this.lblBackground); this.gbInfo.Controls.Add(this.txtBackground);
            this.gbInfo.Controls.Add(this.lblRelations); this.gbInfo.Controls.Add(this.txtRelations);
            this.gbInfo.Controls.Add(this.lblNotes); this.gbInfo.Controls.Add(this.txtNotes);

            // 按钮
            this.btnAdd.Font = new Font("Microsoft YaHei", 10F);
            this.btnAdd.Location = new Point(320, 540);
            this.btnAdd.Size = new Size(110, 32);
            this.btnAdd.Text = "添加角色";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Font = new Font("Microsoft YaHei", 10F);
            this.btnUpdate.Location = new Point(440, 540);
            this.btnUpdate.Size = new Size(110, 32);
            this.btnUpdate.Text = "更新信息";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Font = new Font("Microsoft YaHei", 10F);
            this.btnDelete.Location = new Point(560, 540);
            this.btnDelete.Size = new Size(110, 32);
            this.btnDelete.Text = "删除角色";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // lblCount
            this.lblCount.Font = new Font("Microsoft YaHei", 10F);
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new Point(12, 540);
            this.lblCount.Size = new Size(120, 24);
            this.lblCount.Text = "角色总数: 0";

            // CharacterForm
            this.AutoScaleDimensions = new SizeF(9F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1080, 586);
            this.Controls.Add(this.lstCharacters);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblCount);
            this.Font = new Font("Microsoft YaHei", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "👤 角色卡设计";
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDeathChapter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ListBox lstCharacters;
        private System.Windows.Forms.Panel gbInfo;
        private Label lblName; private TextBox txtName;
        private Label lblAliases; private TextBox txtAliases;
        private Label lblFaction; private TextBox txtFaction;
        private Label lblTitle; private TextBox txtTitle;
        private Label lblRealm; private TextBox txtRealm;
        private Label lblStatus; private ComboBox cmbStatus;
        private Label lblDeathChapter; private NumericUpDown numDeathChapter;
        private Label lblPersonality; private CheckedListBox clbPersonality; private TextBox txtPersonality;
        private Label lblAppearance; private TextBox txtAppearance;
        private Label lblHair; private TextBox txtHair;
        private Label lblHobbies; private TextBox txtHobbies;
        private Label lblLocation; private TextBox txtLocation;
        private Label lblBackground; private TextBox txtBackground;
        private Label lblRelations; private TextBox txtRelations;
        private Label lblNotes; private TextBox txtNotes;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Label lblCount;
    }
}