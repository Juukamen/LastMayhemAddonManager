using System.Windows.Forms;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;

namespace LastTryMayhemAddonManager
{
    public partial class Main : Form
    {
        #region Members
        private Dictionary<RadioButton, DirectoryInfo> installations;
        #endregion //Members

        #region Constructors
        public Main()
        {
            InitializeComponent();
            this.LoadWowClients();
        }
        #endregion //Constructors

        #region Public Methods
        public void LoadWowClients()
        {
            string lookup = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Blizzard Entertainment\\World of Warcraft";
            object value = Registry.GetValue(lookup, "InstallPath", null);
            if(value == null)
            {
                throw new Exception("Cannot find Wow installation path.");
            }

            DirectoryInfo installationDir = new DirectoryInfo((string)value).Parent;
            DirectoryInfo retailDir = new DirectoryInfo(installationDir.FullName + "\\_retail_");
            DirectoryInfo classicDir = new DirectoryInfo(installationDir.FullName + "\\_classic_");

            this.installations = new Dictionary<RadioButton, DirectoryInfo>();
            this.installations.Add(this.rb_classic, classicDir);
            this.installations.Add(this.rb_retail, retailDir);

            this.rb_classic.Enabled = classicDir.Exists;
            this.rb_retail.Enabled = retailDir.Exists;

            if(!this.rb_retail.Enabled)
            {
                this.rb_classic.Checked = true;
                this.rb_retail.Checked = false;
            }
            else
            {
                this.rb_retail.Checked = true;
            }
        }
        #endregion //Public Methods

        #region Private Methods
        #region Events
        private void WowClientCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                DirectoryInfo dir = this.installations[rb];

                this.gb_wow_installation.Text = "World of Warcraft " + rb.Text + ": " + dir.FullName;
            }
        }
        #endregion //Events
        #endregion //Private Methods
    }
}
