using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LastTryMayhemAddonManager.Data
{
    internal class TocData : Dictionary<string, string> 
    {
        #region Constructors
        public TocData(FileInfo file)
        {
            Dictionary<string, string[]> buffer = new Dictionary<string, string[]>();
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
                            List<string> values = new List<string>(components.Select(x => Regex.Replace(x.Trim().Replace("|r", null), "\\|c[0-9A-Fa-f]{8}", "")));
                            values.RemoveAt(0);

                            if(!buffer.ContainsKey(key))
                            {
                                buffer.Add(key, values.ToArray());
                            }
                            else
                            {
                                values.AddRange(buffer[key]);
                                buffer[key] = values.ToArray();
                            }
                        }
                    }
                }
                sr.Close();
            }
            foreach(KeyValuePair<string, string[]> kvp in buffer)
            {
                this.Add(kvp.Key, string.Join(", ", kvp.Value));
            }
        }

        public TocData(string file) 
            : this(new FileInfo(file))
        {
            
        }
        #endregion //Constructors
    }
}
