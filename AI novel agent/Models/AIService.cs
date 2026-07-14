using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public class AiConfig
    {
        public string ApiKey { get; set; } = "";
        public string ApiUrl { get; set; } = "https://api.deepseek.com/v1";
        public string Model { get; set; } = "deepseek-chat";
        public int MaxTokens { get; set; } = 4096;
        public double Temperature { get; set; } = 0.8;
        public string ContextStrategy { get; set; } = "最近5段";
        public bool CacheEnabled { get; set; } = true;

        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        public static AiConfig Load()
        {
            var path = Path.Combine(Application.StartupPath, "ai_config.json");
            if (!File.Exists(path)) return new AiConfig();
            try { return _json.Deserialize<AiConfig>(File.ReadAllText(path)) ?? new AiConfig(); }
            catch { return new AiConfig(); }
        }

        public void Save()
        {
            var path = Path.Combine(Application.StartupPath, "ai_config.json");
            File.WriteAllText(path, _json.Serialize(this));
        }
    }

    public class ContextAssembler
    {
        private readonly KnowledgeBase _kb;
        public ContextAssembler(KnowledgeBase kb) { _kb = kb; }

        public string BuildContext(int currentChapter, string recentText, string projectDir = null)
        {
            var parts = new List<string>();
            parts.Add("===== 世界设定与上下文 =====");
            parts.Add("以下是你必须严格遵守的世界设定、角色信息和当前剧情上下文。");
            parts.Add("");

            // 1. 世界观设定
            var wsList = _kb.WorldSettings.Take(30).ToList();
            if (wsList.Any())
            {
                parts.Add("【世界观设定（必须遵守）】");
                foreach (var ws in wsList)
                    parts.Add("  [" + ws.Category + "] " + ws.Key + ": " + ws.Value);
                parts.Add("");
            }

            // 2. 前几章衔接（加载上一章的尾部内容）
            if (!string.IsNullOrEmpty(projectDir))
            {
                string prevTail = LoadChapterTail(projectDir, currentChapter - 1);
                if (!string.IsNullOrEmpty(prevTail))
                    parts.Add("【上章结尾】" + Truncate(prevTail, 2000));

                string prevPrevTail = LoadChapterTail(projectDir, currentChapter - 2);
                if (!string.IsNullOrEmpty(prevPrevTail))
                    parts.Add("【上上章结尾】" + Truncate(prevPrevTail, 1000));
            }

            // 3. 本卷概要
            var vol = GetVolumeSummary(currentChapter);
            if (vol != null) parts.Add("【本卷概要】" + vol);

            // 3. 活跃角色
            var evts = _kb.GetEventsByChapterRange(Math.Max(1, currentChapter - 10), currentChapter);
            var activeNames = evts.SelectMany(e => e.InvolvedChars).Distinct().Take(8).ToList();
            foreach (var c in _kb.Characters.Take(10))
            {
                if (!activeNames.Contains(c.Name))
                    activeNames.Add(c.Name);
            }
            if (activeNames.Count > 12) activeNames = activeNames.Take(12).ToList();

            foreach (var name in activeNames)
            {
                var c = _kb.GetCharacter(name);
                if (c != null)
                {
                    parts.Add("【角色】" + c.ToContextString());
                    if (!string.IsNullOrEmpty(c.Personality))
                        parts.Add("  性格: " + c.Personality);
                    if (!string.IsNullOrEmpty(c.Appearance))
                        parts.Add("  外貌: " + c.Appearance);
                    if (!string.IsNullOrEmpty(c.Hair))
                        parts.Add("  头发: " + c.Hair);
                    if (!string.IsNullOrEmpty(c.Hobbies))
                        parts.Add("  爱好: " + c.Hobbies);
                    if (!string.IsNullOrEmpty(c.Background))
                        parts.Add("  背景: " + Truncate(c.Background, 200));
                    if (!string.IsNullOrEmpty(c.CurrentLocation))
                        parts.Add("  所在地: " + c.CurrentLocation);
                }
            }

            // 4. 地点
            var loc = DetectLocation(recentText);
            if (loc != null) parts.Add("【地点】" + loc.Name + ": " + loc.Description);

            // 5. 近期事件
            var recentEvts = _kb.GetEventsByChapterRange(
                Math.Max(1, currentChapter - 5), currentChapter)
                .OrderByDescending(e => e.Chapter).Take(8);
            foreach (var e in recentEvts)
                parts.Add("【第" + e.Chapter + "章事件】" + e.Title + ": " + e.Summary);

            // 6. 物品
            var items = _kb.Items
                .Where(i => !i.IsConsumed && activeNames.Contains(i.Owner ?? ""))
                .Take(5);
            foreach (var i in items)
                parts.Add("【物品】" + i.Name + "(" + i.Owner + "): " + i.Description);

            // 7. 约束
            var cons = GetConstraints(currentChapter, activeNames);
            if (cons.Any()) parts.Add("【约束】" + string.Join("; ", cons));

            // 8. 当前文本
            if (!string.IsNullOrEmpty(recentText))
                parts.Add("【当前文本】" + Truncate(recentText, 3000));

            return string.Join("\n", parts);
        }        private List<string> GetConstraints(int chapter, List<string> active)
        {
            var r = new List<string>();
            foreach (var c in _kb.Characters)
            {
                if (c.Status == "死亡" && active.Contains(c.Name))
                    r.Add(c.Name + "已死于第" + c.DeathChapter + "章，不能出场");
            }
            foreach (var i in _kb.Items)
            {
                if (i.IsConsumed && active.Contains(i.Owner ?? ""))
                    r.Add(i.Name + "已消耗");
            }
            return r;
        }

        private LocationEntry DetectLocation(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            foreach (var l in _kb.Locations)
                if (text.Contains(l.Name)) return l;
            return null;
        }

        private string GetVolumeSummary(int chapter)
        {
            int vol = (chapter - 1) / 50 + 1;
            return "第" + vol + "卷（第" + ((vol - 1) * 50 + 1) + "-" + (vol * 50) + "章）";
        }

        private string LoadChapterTail(string projectDir, int chapterId)
        {
            if (chapterId <= 0) return null;
            var tailFile = System.IO.Path.Combine(projectDir, "chapters", "ch_" + chapterId + "_tail.txt");
            if (!System.IO.File.Exists(tailFile)) return null;
            try { return System.IO.File.ReadAllText(tailFile); }
            catch { return null; }
        }

        private static string Truncate(string s, int max)
        {
            if (s == null) return "";
            return s.Length <= max ? s : s.Substring(0, max) + "...";
        }
    }

    public class CacheManager
    {
        private readonly Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();
        private readonly string _filePath;
        private int _hitCount;
        private int _missCount;
        private int _totalTokensSaved;
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        public int HitCount { get { return _hitCount; } }
        public int MissCount { get { return _missCount; } }
        public int TotalTokensSaved { get { return _totalTokensSaved; } }
        public double HitRate
        {
            get
            {
                int total = _hitCount + _missCount;
                return total == 0 ? 0 : (double)_hitCount / total * 100;
            }
        }
        public int EntryCount { get { return _cache.Count; } }

        public CacheManager()
        {
            _filePath = Path.Combine(Application.StartupPath, "ai_cache.json");
            Load();
        }

        public bool TryGet(string prompt, string model, out string result)
        {
            result = null;
            var fp = ComputeFingerprint(prompt, model);
            CacheEntry entry;
            if (_cache.TryGetValue(fp, out entry))
            {
                entry.HitCount++;
                _hitCount++;
                _totalTokensSaved += entry.TokensUsed;
                result = entry.Response;
                return true;
            }
            _missCount++;
            return false;
        }

        public void Add(string prompt, string model, string response, int tokensUsed)
        {
            var fp = ComputeFingerprint(prompt, model);
            if (_cache.Count >= 500)
            {
                var oldest = _cache.OrderBy(kv => kv.Value.CreatedAt).First();
                _cache.Remove(oldest.Key);
            }
            _cache[fp] = new CacheEntry
            {
                Fingerprint = fp, Model = model, Response = response,
                TokensUsed = tokensUsed, PromptPreview = Truncate(prompt, 80),
                CreatedAt = DateTime.Now, HitCount = 0
            };
            Save();
        }

        public void Clear()
        {
            _cache.Clear();
            _hitCount = 0;
            _missCount = 0;
            _totalTokensSaved = 0;
            Save();
        }

        private string ComputeFingerprint(string prompt, string model)
        {
            var input = model + "|" + prompt;
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower().Substring(0, 16);
            }
        }

        private void Load()
        {
            if (!File.Exists(_filePath)) return;
            try
            {
                var data = _json.Deserialize<CacheData>(File.ReadAllText(_filePath));
                if (data != null)
                {
                    _hitCount = data.HitCount;
                    _missCount = data.MissCount;
                    _totalTokensSaved = data.TotalTokensSaved;
                    _cache.Clear();
                    foreach (var e in data.Entries)
                        _cache[e.Fingerprint] = e;
                }
            }
            catch { }
        }

        public void Save()
        {
            var data = new CacheData
            {
                HitCount = _hitCount, MissCount = _missCount,
                TotalTokensSaved = _totalTokensSaved,
                Entries = _cache.Values.ToList()
            };
            File.WriteAllText(_filePath, _json.Serialize(data));
        }

        private string LoadChapterTail(string projectDir, int chapterId)
        {
            if (chapterId <= 0) return null;
            var tailFile = System.IO.Path.Combine(projectDir, "chapters", "ch_" + chapterId + "_tail.txt");
            if (!System.IO.File.Exists(tailFile)) return null;
            try { return System.IO.File.ReadAllText(tailFile); }
            catch { return null; }
        }

        private static string Truncate(string s, int max)
        {
            if (s == null) return "";
            return s.Length <= max ? s : s.Substring(0, max);
        }

        public class CacheEntry
        {
            public string Fingerprint { get; set; }
            public string Model { get; set; }
            public string Response { get; set; }
            public int TokensUsed { get; set; }
            public string PromptPreview { get; set; }
            public DateTime CreatedAt { get; set; }
            public int HitCount { get; set; }
        }

        private class CacheData
        {
            public int HitCount { get; set; }
            public int MissCount { get; set; }
            public int TotalTokensSaved { get; set; }
            public List<CacheEntry> Entries { get; set; } = new List<CacheEntry>();
        }
    }

    public class DeepSeekService
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly AiConfig _config;
        private readonly CacheManager _cache;
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        public DeepSeekService(AiConfig config, CacheManager cache)
        {
            _config = config;
            _cache = cache;
        }

        private string NormalizeApiUrl()
        {
            var url = _config.ApiUrl.TrimEnd('/');
            if (!url.Contains("/v1") && !url.Contains("/v2"))
                url = url + "/v1";
            return url;
        }

        public async Task<List<string>> GetAvailableModels()
        {
            var url = NormalizeApiUrl() + "/models";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("Authorization", "Bearer " + _config.ApiKey);

            var resp = await _http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadAsStringAsync();

            var result = new List<string>();
            var obj = _json.Deserialize<Dictionary<string, object>>(body);

            if (obj == null) return result;

            // 尝试解析 data 数组
            object dataObj = null;
            if (obj.TryGetValue("data", out dataObj))
            {
                // JavaScriptSerializer 返回 ArrayList，不是 object[]，需要用 ToArray 转换
                var list = dataObj as System.Collections.ArrayList;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        var dict = item as Dictionary<string, object>;
                        if (dict != null)
                        {
                            object idObj;
                            // 兼容不同 API 返回格式: id / model / name
                            if (dict.TryGetValue("id", out idObj) ||
                                dict.TryGetValue("model", out idObj) ||
                                dict.TryGetValue("name", out idObj))
                            {
                                var modelId = idObj?.ToString();
                                if (!string.IsNullOrEmpty(modelId) && !result.Contains(modelId))
                                    result.Add(modelId);
                            }
                        }
                    }
                }
                else
                {
                    // 也可能是 object[]
                    var arr = dataObj as object[];
                    if (arr != null)
                    {
                        foreach (var item in arr)
                        {
                            var dict = item as Dictionary<string, object>;
                            if (dict != null)
                            {
                                object idObj;
                                if (dict.TryGetValue("id", out idObj) ||
                                    dict.TryGetValue("model", out idObj) ||
                                    dict.TryGetValue("name", out idObj))
                                {
                                    var modelId = idObj?.ToString();
                                    if (!string.IsNullOrEmpty(modelId) && !result.Contains(modelId))
                                        result.Add(modelId);
                                }
                            }
                        }
                    }
                }
            }

            // 如果 data 解析失败，尝试其他字段
            if (result.Count == 0)
            {
                // 某些 API 直接返回 ["model1", "model2"]
                var rawArrayList = dataObj as System.Collections.ArrayList;
                if (rawArrayList != null)
                {
                    foreach (var item in rawArrayList)
                    {
                        var s = item?.ToString();
                        if (!string.IsNullOrEmpty(s) && !result.Contains(s))
                            result.Add(s);
                    }
                }
                else
                {
                    var rawArr = dataObj as object[];
                    if (rawArr != null)
                    {
                        foreach (var item in rawArr)
                        {
                            var s = item?.ToString();
                            if (!string.IsNullOrEmpty(s) && !result.Contains(s))
                                result.Add(s);
                        }
                    }
                }
            }

            // 过滤掉非对话模型（以文本嵌入等结尾的）
            result = result.Where(m =>
                !m.Contains("embedding") &&
                !m.Contains("instruct") &&
                !m.Contains("davinci") &&
                !m.Contains("tts-")
            ).ToList();

            // 按优先级排序
            // 如果检测到模型数为0，记录返回数据用于诊断
            if (result.Count == 0)
            {
                var preview = body.Length > 300 ? body.Substring(0, 300) + "..." : body;
                throw new Exception("API 返回了空模型列表。返回数据: " + preview);
            }

            var ordered = new List<string>();
            string[] preferred = { "deepseek-chat", "deepseek-reasoner", "deepseek-v3", "deepseek-r1", "deepseek", "gpt-4o", "gpt-4o-mini", "gpt-4", "gpt-3.5" };
            foreach (var p in preferred)
                if (result.Remove(p))
                    ordered.Add(p);
            ordered.AddRange(result.OrderBy(m => m));

            return ordered;
        }

        public async Task<Tuple<string, int>> SendRequest(string systemPrompt, string userPrompt)
        {
            var fullPrompt = systemPrompt + "\n---\n" + userPrompt;
            string cached;
            if (_config.CacheEnabled && _cache.TryGet(fullPrompt, _config.Model, out cached))
                return Tuple.Create(cached, 0);

            var messages = new object[] {
                new Dictionary<string, object> { { "role", "system" }, { "content", systemPrompt } },
                new Dictionary<string, object> { { "role", "user" }, { "content", userPrompt } }
            };
            var bodyObj = new Dictionary<string, object>
            {
                { "model", _config.Model },
                { "messages", messages },
                { "temperature", _config.Temperature },
                { "max_tokens", _config.MaxTokens },
                { "stream", false }
            };

            var url = NormalizeApiUrl() + "/chat/completions";
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Add("Authorization", "Bearer " + _config.ApiKey);
            req.Content = new StringContent(_json.Serialize(bodyObj), Encoding.UTF8, "application/json");

            var resp = await _http.SendAsync(req);
            var json = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
            {
                // 尝试解析错误信息
                string errMsg = "HTTP " + (int)resp.StatusCode;
                try
                {
                    var errObj = _json.Deserialize<Dictionary<string, object>>(json);
                    if (errObj != null)
                    {
                        object errDetail;
                        if (errObj.TryGetValue("error", out errDetail))
                        {
                            var errDict = errDetail as Dictionary<string, object>;
                            if (errDict != null)
                            {
                                object msg;
                                if (errDict.TryGetValue("message", out msg))
                                    errMsg = msg.ToString();
                            }
                            else
                            {
                                errMsg = errDetail.ToString();
                            }
                        }
                    }
                }
                catch { }
                throw new HttpRequestException("API 返回错误: " + errMsg + "\n请求地址: " + url + "\n模型: " + _config.Model);
            }
            var obj = _json.Deserialize<Dictionary<string, object>>(json);

            string content = "";
            int tokens = 0;

            if (obj != null)
            {
                                object choicesObj;
                if (obj.TryGetValue("choices", out choicesObj))
                {
                    var choices = choicesObj as System.Collections.ArrayList;
                    var choicesArr = choicesObj as object[];
                    if (choices != null && choices.Count > 0)
                    {
                        var first = choices[0] as Dictionary<string, object>;
                        if (first != null)
                        {
                            object msgObj;
                            if (first.TryGetValue("message", out msgObj))
                            {
                                var msg = msgObj as Dictionary<string, object>;
                                if (msg != null)
                                {
                                    object contentObj;
                                    if (msg.TryGetValue("content", out contentObj))
                                        content = contentObj?.ToString() ?? "";
                                }
                            }
                        }
                    }
                    else if (choicesArr != null && choicesArr.Length > 0)
                    {
                        var first = choicesArr[0] as Dictionary<string, object>;
                        if (first != null)
                        {
                            object msgObj;
                            if (first.TryGetValue("message", out msgObj))
                            {
                                var msg = msgObj as Dictionary<string, object>;
                                if (msg != null)
                                {
                                    object contentObj;
                                    if (msg.TryGetValue("content", out contentObj))
                                        content = contentObj?.ToString() ?? "";
                                }
                            }
                        }
                    }
                }
                object usageObj;
                if (obj.TryGetValue("usage", out usageObj))
                {
                    var usage = usageObj as Dictionary<string, object>;
                    if (usage != null)
                    {
                        object tObj;
                        if (usage.TryGetValue("total_tokens", out tObj))
                            tokens = Convert.ToInt32(tObj);
                    }
                }
            }

            if (_config.CacheEnabled && !string.IsNullOrEmpty(content))
                _cache.Add(fullPrompt, _config.Model, content, tokens);

            return Tuple.Create(content, tokens);
        }
    }
}