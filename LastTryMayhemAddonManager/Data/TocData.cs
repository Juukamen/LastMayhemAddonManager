using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LastTryMayhemAddonManager.Data
{
    internal class TocData : Dictionary<string, string[]> 
    {
        #region Constructors
        public TocData(FileInfo file)
        {
            if (file.Exists)
            {
                StreamReader sr = new StreamReader(file.FullName);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (Regex.Match(line, "^##").Success)
                    {
                        line = line.Substring(3, line.Length - 3);
                        string[] components = line.Split(new char[] { ':' });
                        if (components.Length >= 2)
                        {
                            string key = components[0].Trim();
                            List<string> values = new List<string>(components.Select(x => x.Trim()));
                            values.RemoveAt(0);

                            if(!this.ContainsKey(key))
                            {
                                this.Add(key, values.ToArray());
                            }
                            else
                            {
                                values.AddRange(this[key]);
                                this[key] = values.ToArray();
                            }
                        }
                    }
                }
                sr.Close();
            }
        }

        public TocData(string file) 
            : this(new FileInfo(file))
        {
            
        }
        #endregion //Constructors
    }
}
