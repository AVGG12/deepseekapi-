using System;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public partial class KnowledgeBaseForm : Form
    {
        private readonly KnowledgeBase _kb;

        public KnowledgeBaseForm(KnowledgeBase kb)
        {
            InitializeComponent();
            _kb = kb;
            LoadWorldSettings();
        }

        private void LoadWorldSettings()
        {
            lstSettings.Items.Clear();
            foreach (var ws in _kb.WorldSettings)
                lstSettings.Items.Add("[" + ws.Category + "] " + ws.Key + ": " + ws.Value);

            lstLocations.Items.Clear();
            foreach (var loc in _kb.Locations)
                lstLocations.Items.Add(loc.Name + " (" + loc.Type + "): " + loc.Description);

            lblCount.Text = "世界观条目: " + _kb.WorldSettings.Count + " | 地点: " + _kb.Locations.Count;
        }

        private void btnAddSetting_Click(object sender, EventArgs e)
        {
            var key = txtKey.Text.Trim();
            var val = txtValue.Text.Trim();
            var cat = cmbCategory.Text;
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(val))
            {
                MessageBox.Show("请填写键和值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _kb.WorldSettings.Add(new WorldSettingEntry
            {
                Key = key,
                Value = val,
                Category = string.IsNullOrEmpty(cat) ? "通用" : cat
            });
            LoadWorldSettings();
            txtKey.Clear();
            txtValue.Clear();
        }

        private void btnDeleteSetting_Click(object sender, EventArgs e)
        {
            if (lstSettings.SelectedIndex < 0) return;
            _kb.WorldSettings.RemoveAt(lstSettings.SelectedIndex);
            LoadWorldSettings();
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            var name = txtLocName.Text.Trim();
            var desc = txtLocDesc.Text.Trim();
            var type = cmbLocType.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入地点名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _kb.Locations.Add(new LocationEntry
            {
                Name = name,
                Description = desc,
                Type = string.IsNullOrEmpty(type) ? "未知" : type
            });
            LoadWorldSettings();
            txtLocName.Clear();
            txtLocDesc.Clear();
        }

        private void btnDeleteLocation_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedIndex < 0) return;
            _kb.Locations.RemoveAt(lstLocations.SelectedIndex);
            LoadWorldSettings();
        }

        private void btnImportDefaults_Click(object sender, EventArgs e)
        {
            string msg = "\u5C06\u5BFC\u5165\u300A\u8036\u5E95\u5E95\u4E9A\u00B7\u79D1\u65AF\u83AB\u65AF\u300B\u5B8C\u6574\u4E16\u754C\u89C2\u8BBE\u5B9A\uFF08\u542B\u4E16\u754C\u57FA\u7840\u3001\u4E1C\u897F\u65B9\u6587\u660E\u3001\u7EAA\u5143\u65F6\u95F4\u7EBF\u3001\u4EBA\u53E3\u7ECF\u6D4E\u7B49\uFF09\uFF0C\u662F\u5426\u7EE7\u7EED\uFF1F";
            var result = MessageBox.Show(msg, "\u5BFC\u5165\u9ED8\u8BA4\u4E16\u754C\u89C2", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            _kb.PopulateDefaultWorldSettings();
            LoadWorldSettings();
            MessageBox.Show("\u4E16\u754C\u89C2\u8BBE\u5B9A\u5BFC\u5165\u5B8C\u6210\uFF01\u5171 " + _kb.WorldSettings.Count + " \u6761\u6761\u76EE\u3002", "\u5BFC\u5165\u6210\u529F", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
