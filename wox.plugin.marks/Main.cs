using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wox.Plugin;

namespace wox.plugin.marks
{
    class Main : IPlugin
    {
        public void Init(PluginInitContext context)
        {
        }
        public List<Result> Query(Query query)
        {
            List<Result> results = NewResult();
            if (string.IsNullOrEmpty(query.Search))
            {
                return results;
            }
            else
            {
                return results.Where(m => m.Title.ToLower().Contains(query.Search.ToLower()) || StrToPinyin.GetChineseSpell(m.Title).Contains(query.Search)).ToList();
            }
        }

        public List<Result> NewResult()
        {
            List<Result> results = new List<Result>();
            try
            {
                using (StreamReader sr = new StreamReader(@"Plugins\wox.plugin.marks\config.yml"))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (!"#".Equals(line.Substring(0, 1)))
                            {
                                if (line.Contains(":"))
                                {
                                    int index = line.IndexOf(':');

                                    string name = line.Substring(0, index).Trim();
                                    string val = line.Substring(index + 1, line.Length - index - 1).Trim(); ;
                                    string img = val.Contains("www") || val.Contains("http") ? "Images\\web.png" : "Images\\file.png";
                                    results.Add(new Result()
                                    {
                                        Title = name,
                                        SubTitle = val,
                                        IcoPath = img,
                                        Action = e =>
                                        {
                                            System.Diagnostics.Process.Start(val);
                                            return true;
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                results.Add(new Result() { Title = "错误", SubTitle = ex.Message, IcoPath = "Images\\web.png",
                    Action = e =>
                    {
                        return false;
                    }
                });
            }
            return results;
        }
    }
}
