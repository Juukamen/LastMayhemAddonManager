using System.Windows.Forms;
using LastTryMayhemAddonManager.Data;
using System.Collections.Generic;

namespace LastTryMayhemAddonManager
{
    public partial class AddonListing : Form
    {
        #region Constructors
        public AddonListing()
        {
            InitializeComponent();
        }

        public AddonListing(ISourceConfiguration[] configurations) 
            : this()
        {
            foreach(ISourceConfiguration configuration in configurations)
            {
                string baseUrl = configuration.BaseUrl();
            }
        }
        #endregion //Constructors
    }
}
