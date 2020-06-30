using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LastTryMayhemAddonManager.Data
{
    internal class AddonIO
    {
        #region Public Methods
        public static void Backup(DirectoryInfo directory)
        {
            DirectoryInfo exportDir = AddonIO.GetBackupDirectory();
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
            AddonIO.CleanupOldBackup(directory, exportFile);
        }
        #endregion //Public Methods

        #region Private Methods
        private static DirectoryInfo GetBackupDirectory()
        {
            DirectoryInfo exportDir = new DirectoryInfo("Backup");
            if (!exportDir.Exists)
            {
                exportDir.Create();
            }
            return exportDir;
        }

        private static void CleanupOldBackup(DirectoryInfo dir, FileInfo exportFile)
        {
            foreach (FileInfo f in exportFile.Directory.GetFiles("*" + dir.Name + ".bak"))
            {
                if(f.FullName != exportFile.FullName)
                {
                    f.Delete();
                }
            }
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
        #endregion //Private Methods
    }
}
