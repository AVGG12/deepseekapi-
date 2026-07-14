using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AI_novel_agent
{
    public class KnowledgeBase
    {
        public List<CharacterEntry> Characters { get; set; } = new List<CharacterEntry>();
        public List<EventEntry> Events { get; set; } = new List<EventEntry>();
        public List<ItemEntry> Items { get; set; } = new List<ItemEntry>();
        public List<LocationEntry> Locations { get; set; } = new List<LocationEntry>();
        public List<WorldSettingEntry> WorldSettings { get; set; } = new List<WorldSettingEntry>();

        [ScriptIgnore]
        public string SavePath { get; set; } = "";

        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        private Dictionary<string, CharacterEntry> _charIndex;
        private Dictionary<string, LocationEntry> _locIndex;
        private Dictionary<int, List<EventEntry>> _eventsByChapter;

        public void BuildIndex()
        {
            _charIndex = Characters.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);
            _locIndex = Locations.ToDictionary(l => l.Name, StringComparer.OrdinalIgnoreCase);
            _eventsByChapter = Events.GroupBy(e => e.Chapter).ToDictionary(g => g.Key, g => g.ToList());
        }

        public CharacterEntry GetCharacter(string name)
        {
            if (_charIndex == null) BuildIndex();
            CharacterEntry c;
            _charIndex.TryGetValue(name, out c);
            return c;
        }

        public LocationEntry GetLocation(string name)
        {
            if (_locIndex == null) BuildIndex();
            LocationEntry l;
            _locIndex.TryGetValue(name, out l);
            return l;
        }

        public List<EventEntry> GetEventsByChapterRange(int start, int end)
        {
            if (_eventsByChapter == null) BuildIndex();
            var result = new List<EventEntry>();
            for (int i = start; i <= end; i++)
            {
                List<EventEntry> evts;
                if (_eventsByChapter.TryGetValue(i, out evts))
                    result.AddRange(evts);
            }
            return result;
        }

        // ====== 持久化 ======
        public void Save()
        {
            if (string.IsNullOrEmpty(SavePath)) return;
            try
            {
                // 保存完整知识库到 JSON
                File.WriteAllText(SavePath, _json.Serialize(this));
            }
            catch { }
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(SavePath) || !File.Exists(SavePath)) return;
            try
            {
                var data = _json.Deserialize<KnowledgeBase>(File.ReadAllText(SavePath));
                if (data != null)
                {
                    Characters = data.Characters ?? new List<CharacterEntry>();
                    Events = data.Events ?? new List<EventEntry>();
                    Items = data.Items ?? new List<ItemEntry>();
                    Locations = data.Locations ?? new List<LocationEntry>();
                    WorldSettings = data.WorldSettings ?? new List<WorldSettingEntry>();
                    BuildIndex();
                }
            }
            catch { }
        }
        /// 填充耶底底亚·科斯莫斯默认世界观设定（使用标准分类）
        /// </summary>
        public void PopulateDefaultWorldSettings()
        {
            if (WorldSettings.Count > 0) return;

            AddSetting("世界基础", "大陆名称", "耶底底亚·科斯莫斯（Jedidia Cosmos）——蒙爱之有序宇宙");
            AddSetting("世界基础", "词源", "耶底底亚=耶和华所爱的；科斯莫斯=希腊语有序宇宙");
            AddSetting("世界基础", "东西方分界线", "岗坚——藏语大冰川，海拔10,000-12,000米，厚5,000-8,000米，长5,000公里");
            AddSetting("世界基础", "岗坚通行条件", "仅七阶以上修行者可翻越，耗时7-15天，全年无通行窗口");
            AddSetting("世界基础", "海路尺度", "大洋宽约10,000公里，风帆商船单程60-80天，蒸汽船40-50天");

            AddSetting("东方文明", "国名", "中夏王朝——中央正统博大之域，别称中土/龙壤/符朝，首都天邑");
            AddSetting("东方文明", "人口", "约6,000万");
            AddSetting("东方文明", "龙脉", "贯穿东方的地底灵气主干道，滋养土地，压制大气精灵");
            AddSetting("东方文明", "儒者七符体系", "文昌/武烈/仁育/孝睦/廉直/工巧/信诚，七符全则龙脉存");
            AddSetting("东方文明", "道士九阶", "识器→炼胚→通符→结丹→御器→化炁→辟谷→通神→尘仙");
            AddSetting("东方文明", "道士心性限制", "四阶须斩贪嗔痴，邪修可达八阶但永无九阶");
            AddSetting("东方文明", "施法铁律", "不得以己身灵气为亿万众生改天换地，违者魂飞魄散");

            AddSetting("西方文明", "四国概述", "艾尔法森林王国/诺德冰川领/萨伏恩联合领/亚尔兰海岛国");
            AddSetting("西方文明", "四国联盟", "各自独立，联盟会议协商，统一班柯货币，共同防御协定");
            AddSetting("西方文明", "总人口", "约3,000万");
            AddSetting("西方文明", "魔法使九阶灵契", "聆风→唤尘→结契→共鸣→御灵→融脉→化语→天衡→灵王");
            AddSetting("西方文明", "所罗门箴言", "不可强迫/欺骗/贪取/傲慢/恐惧/忘衡");
            AddSetting("西方文明", "黑魔法使", "堕落之因：傲慢、恐惧、执迷");

            AddSetting("共有设定", "散灵（游弋之息）", "全球底层能量微粒，东方称天地元气，西方称游弋之息");
            AddSetting("共有设定", "能量守恒", "一切术法为引导/转化而非创造能量");
            AddSetting("共有设定", "因果报偿", "取必有还——正修偿于平衡，邪修偿于自身");
            AddSetting("共有设定", "时间膨胀", "修行者飞行触发1:100时间膨胀（大洋磁场+纯粹能量耦合）");
            AddSetting("共有设定", "施法铁律", "不得大规模改变天地气象，违者魂飞魄散");

            AddSetting("纪元时间线", "纪元基准", "飞升纪元A.E.0：太一真人飞升");
            AddSetting("纪元时间线", "重要节点", "中夏统一->儒者归符->太一飞升->所罗门体系->班柯确立->猎鬼战争->当前A.E.1500");

            AddSetting("地理区域", "海路五站", "永夜港->铁浮岛->深渊群岛（非停靠）->翡翠列岛->启明港");
            AddSetting("地理区域", "铁浮岛", "大洋中央，全球最富煤矿与铁矿，第一次工业革命全盛期，人口约500万");
            AddSetting("地理区域", "永夜港", "吸血鬼永久中立国，约4,000-5,000人，出口血晶石/龙骨石等");

            AddSetting("人口经济", "全球人口", "中夏6,000万+四国3,000万+铁浮岛500万+永夜港5,000=约9,500万");
            AddSetting("人口经济", "产业分工", "中夏全产业链自给自足，西方轻工发达粮依赖进口，铁浮岛重工极发达粮100%进口");
            AddSetting("人口经济", "班柯货币", "超主权世界货币，独家中夏铸造，金/银/铜克朗体系，恒定不增发");

            AddSetting("修行体系", "道家九阶", "识器->炼胚->通符->结丹->御器->化炁->辟谷->通神->尘仙");
            AddSetting("修行体系", "魔法九阶灵契", "聆风->唤尘->结契->共鸣->御灵->融脉->化语->天衡->灵王");
            AddSetting("修行体系", "天心问", "八阶到九阶须说服天道，万古无人成功");

            AddSetting("政治军事", "不干涉凡尘契约", "每20年在岗坚寂风台签署，道门+法师塔为担保方");
            AddSetting("政治军事", "猎鬼战争", "A.E.1000-1050，吸血鬼由10万降至不足1万，西方死亡约2,000万");
            AddSetting("政治军事", "永夜中立条约", "A.E.1060签署，永夜港永久中立");
        }

        private void AddSetting(string category, string key, string value)
        {
            WorldSettings.Add(new WorldSettingEntry
            {
                Key = key,
                Value = value,
                Category = category
            });
        }
    }
    }
    public class CharacterEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public string Name { get; set; }
        public string Aliases { get; set; } = "";
        public string Status { get; set; } = "存活";
        public int DeathChapter { get; set; } = 0;
        public string Faction { get; set; }
        public string Title { get; set; }
        public string Realm { get; set; }
        public string Personality { get; set; }
        public List<string> PersonalityTags { get; set; } = new List<string>();
        public string Appearance { get; set; }
        public string Hair { get; set; }
        public string Hobbies { get; set; }
        public string Background { get; set; }
        public string CurrentLocation { get; set; }
        public int LastAppearChapter { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string Relations { get; set; }
        public string Notes { get; set; }

        public string ToContextString()
        {
            var prefix = "[" + Name + "] ";
            if (!string.IsNullOrEmpty(Title)) prefix += Title + "·";
            prefix += (Realm ?? "?") + " | " + Status + " | " + (Faction ?? "?");
            return prefix;
        }
    }

    public class EventEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public int Chapter { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<string> InvolvedChars { get; set; } = new List<string>();
        public string Location { get; set; }
        public string Impact { get; set; }
        public string Category { get; set; }
    }

    public class ItemEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public string Name { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public bool IsConsumed { get; set; }
        public int FirstAppearChapter { get; set; }
    }

    public class LocationEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public string Name { get; set; }
        public string Parent { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CurrentController { get; set; }
        public List<string> NotableChars { get; set; } = new List<string>();
    }

    public class WorldSettingEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public string Key { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
    }
