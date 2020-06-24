using System.Diagnostics;

namespace LastTryMayhemAddonManager.Data
{
    internal interface ISourceConfiguration
    {
        string BaseUrl();
        object ListAddons(FileVersionInfo versionInfo);
    }
}
