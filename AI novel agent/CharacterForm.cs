using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class CharacterForm : Form
    {
        private readonly KnowledgeBase _kb;

        // 性格推荐标签
        private static readonly string[] PersonalityPresets = {
            "勇敢", "谨慎", "善良", "冷酷", "热血", "理智",
            "狡猾", "直率", "温柔", "暴躁", "幽默", "严肃",
            "高傲", "谦逊", "乐观", "悲观", "忠诚", "叛逆",
            "内向", "外向", "腹黑", "天然呆", "病娇", "傲娇"
        };

        public CharacterForm(KnowledgeBase kb)
        {
            InitializeComponent();
            _kb = kb;

            // 初始化性格标签列表
            foreach (var p in PersonalityPresets)
                clbPersonality.Items.Add(p, false);

            LoadCharacters();
        }

        private void LoadCharacters()
        {
            lstCharacters.Items.Clear();
            foreach (var c in _kb.Characters)
                lstCharacters.Items.Add(c.Name + " | " + (c.Title ?? "") + " | " + (c.Realm ?? "") + " | " + c.Status);
            lblCount.Text = "角色总数: " + _kb.Characters.Count;
        }

        private void ClearForm()
        {
            txtName.Clear(); txtAliases.Clear();
            txtFaction.Clear(); txtTitle.Clear(); txtRealm.Clear();
            txtPersonality.Clear(); txtBackground.Clear();
            txtLocation.Clear(); txtRelations.Clear(); txtNotes.Clear();
            txtAppearance.Clear(); txtHair.Clear(); txtHobbies.Clear();
            cmbStatus.SelectedIndex = 0;
            numDeathChapter.Value = 0;
            for (int i = 0; i < clbPersonality.Items.Count; i++)
                clbPersonality.SetItemChecked(i, false);
        }

        private void lstCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedIndex < 0) return;
            var c = _kb.Characters[lstCharacters.SelectedIndex];
            txtName.Text = c.Name;
            txtAliases.Text = c.Aliases;
            txtFaction.Text = c.Faction;
            txtTitle.Text = c.Title;
            txtRealm.Text = c.Realm;
            txtPersonality.Text = string.Join(", ", c.PersonalityTags ?? new List<string>());
            txtAppearance.Text = c.Appearance;
            txtHair.Text = c.Hair;
            txtHobbies.Text = c.Hobbies;
            txtBackground.Text = c.Background;
            txtLocation.Text = c.CurrentLocation;
            txtRelations.Text = c.Relations;
            txtNotes.Text = c.Notes;
            cmbStatus.Text = c.Status;
            numDeathChapter.Value = c.DeathChapter;

            // 选中性格标签
            for (int i = 0; i < clbPersonality.Items.Count; i++)
            {
                var tag = clbPersonality.Items[i].ToString();
                clbPersonality.SetItemChecked(i, c.PersonalityTags.Contains(tag));
            }
        }

        private void UpdatePersonalityText()
        {
            var selected = new List<string>();
            for (int i = 0; i < clbPersonality.Items.Count; i++)
            {
                if (clbPersonality.GetItemChecked(i))
                    selected.Add(clbPersonality.Items[i].ToString());
            }
            txtPersonality.Text = string.Join(", ", selected);
        }

        private void clbPersonality_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 延迟更新文本，等状态改变后
            this.BeginInvoke((MethodInvoker)delegate { UpdatePersonalityText(); });
        }

        private CharacterEntry ReadFormData()
        {
            var tags = new List<string>();
            for (int i = 0; i < clbPersonality.Items.Count; i++)
            {
                if (clbPersonality.GetItemChecked(i))
                    tags.Add(clbPersonality.Items[i].ToString());
            }
            return new CharacterEntry
            {
                Name = txtName.Text.Trim(),
                Aliases = txtAliases.Text.Trim(),
                Faction = txtFaction.Text.Trim(),
                Title = txtTitle.Text.Trim(),
                Realm = txtRealm.Text.Trim(),
                PersonalityTags = tags,
                Personality = string.Join(", ", tags),
                Appearance = txtAppearance.Text.Trim(),
                Hair = txtHair.Text.Trim(),
                Hobbies = txtHobbies.Text.Trim(),
                Background = txtBackground.Text.Trim(),
                CurrentLocation = txtLocation.Text.Trim(),
                Relations = txtRelations.Text.Trim(),
                Notes = txtNotes.Text.Trim(),
                Status = cmbStatus.Text,
                DeathChapter = (int)numDeathChapter.Value
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请输入角色名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _kb.Characters.Add(ReadFormData());
            LoadCharacters();
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedIndex < 0) return;
            _kb.Characters[lstCharacters.SelectedIndex] = ReadFormData();
            LoadCharacters();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedIndex < 0) return;
            _kb.Characters.RemoveAt(lstCharacters.SelectedIndex);
            LoadCharacters();
            ClearForm();
        }
    }
}