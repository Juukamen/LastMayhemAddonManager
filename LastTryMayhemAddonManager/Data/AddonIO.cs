using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LastTryMayhemAddonManager.Data
{
    internal class AddonIO
    {
        #region Constants
        public const string Extension = ".abk";
        #endregion //Constants

        #region Public Methods
        public static void Restore(FileInfo file, DirectoryInfo addons)
        {
            string name = file.Name.Substring(16, file.Name.Length - (file.Extension.Length + 16));
            DirectoryInfo addon = new DirectoryInfo(addons.FullName + "\\" + name);
            if(addon.Exists)
            {
                addon.Delete(true);
            }
            addon.Create();
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            using(ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                zip.ExtractToDirectory(addon.FullName);
            }
            fs.Close();
        }

        public static void Backup(DirectoryInfo directory, DirectoryInfo client)
        {
            DirectoryInfo exportDir = AddonIO.GetBackupDirectory(client);
            FileInfo exportFile = new FileInfo(exportDir.FullName + "\\" + DateTime.Now.ToString("yyyyMMdd HHmmss") + "-" + directory.Name + AddonIO.Extension);

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

        public static DirectoryInfo GetBackupDirectory(DirectoryInfo client)
        {
            DirectoryInfo exportDir = new DirectoryInfo("Backup\\" + client.Name);
            if (!exportDir.Exists)
            {
                exportDir.Create();
            }
            return exportDir;
        }
        #endregion //Public Methods

        #region Private Methods
        private static void CleanupOldBackup(DirectoryInfo dir, FileInfo exportFile)
        {
            foreach (FileInfo f in exportFile.Directory.GetFiles("*" + dir.Name + AddonIO.Extension))
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
