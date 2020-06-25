using System.Diagnostics;

namespace LastTryMayhemAddonManager.Data
{
    public interface ISourceConfiguration
    {
        string BaseUrl();
        object ListAddons(FileVersionInfo versionInfo);
    }
}
