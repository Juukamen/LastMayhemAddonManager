using System.Diagnostics;

namespace LastTryMayhemAddonManager.Data.Configurations
{
    internal class CurseConfig : ISourceConfiguration
    {
        public string BaseUrl()
        {
            return "https://www.curseforge.com/wow/addons";
        }

        public object ListAddons(FileVersionInfo versionInfo)
        {
            return null;
        }
    }
}
