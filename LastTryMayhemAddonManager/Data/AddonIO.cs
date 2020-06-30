using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LastTryMayhemAddonManager.Data
{
    internal class AddonIO
    {
        public static void Backup(DirectoryInfo directory)
        {
            DirectoryInfo exportDir = new DirectoryInfo("Backup");
            if(!exportDir.Exists)
            {
                exportDir.Create();
            }
            FileInfo exportFile = new FileInfo(exportDir.FullName + "\\" + DateTime.Now.ToString("yyyyMMdd HHmmss") + "-" + directory.Name + ".bak");

            FileStream fs = new FileStream(exportFile.FullName, FileMode.Create, FileAccess.Write);
            using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                foreach(FileInfo f in AddonIO.ReadRecursive(directory))
                {
                    string relative = f.FullName.Replace(directory.FullName, "");
                    zip.CreateEntryFromFile(f.FullName, relative.Substring(1, relative.Length - 1));
                }
            }

            fs.Close();
        }

        private static FileInfo[] ReadRecursive(DirectoryInfo dir)
        {
            List<FileInfo> files = new List<FileInfo>();

            foreach(DirectoryInfo d in dir.GetDirectories())
            {
                files.AddRange(AddonIO.ReadRecursive(d));
            }
            files.AddRange(dir.GetFiles());

            return files.ToArray();
        }
    }
}
