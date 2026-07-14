using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AI_novel_agent
{
    [Serializable]
    public class NovelProject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Genre { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int TotalWords { get; set; } = 0;
        public string FilePath { get; set; } = "";

        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        public static List<NovelProject> LoadRecentProjects()
        {
            var path = Path.Combine(Application.StartupPath, "recent.json");
            if (!File.Exists(path)) return new List<NovelProject>();
            try
            {
                var json = File.ReadAllText(path);
                return _json.Deserialize<List<NovelProject>>(json) ?? new List<NovelProject>();
            }
            catch { return new List<NovelProject>(); }
        }

        public static void SaveRecentProjects(List<NovelProject> projects)
        {
            var path = Path.Combine(Application.StartupPath, "recent.json");
            var json = _json.Serialize(projects);
            File.WriteAllText(path, json);
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(FilePath)) return;
            var json = _json.Serialize(this);
            File.WriteAllText(FilePath, json);
        }

        public static NovelProject Load(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var project = _json.Deserialize<NovelProject>(json);
            if (project != null) project.FilePath = filePath;
            return project ?? new NovelProject();
        }
    }
}