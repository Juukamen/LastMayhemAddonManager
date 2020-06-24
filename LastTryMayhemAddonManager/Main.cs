using LastTryMayhemAddonManager.Data;
using LastTryMayhemAddonManager.Data.Configurations;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LastTryMayhemAddonManager
{
    public partial class Main : Form
    {
        #region Members
        private Dictionary<RadioButton, DirectoryInfo> installations;
        private List<ISourceConfiguration> sources;
        #endregion //Members

        #region Constructors
        public Main()
        {
            InitializeComponent();
            this.InitializeSources();
            this.LoadWowClients();
        }
        #endregion //Constructors

        #region Private Methods
        #region Events
        private void WowClientCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                DirectoryInfo dir = this.installations[rb];

                this.gb_wow_installation.Text = "World of Warcraft " + rb.Text + ": " + dir.FullName;
                this.ListInstalledAddons(dir);
            }
        }
        #endregion //Events

        private void InitializeSources()
        {
            this.sources = new List<ISourceConfiguration>();
            this.sources.Add(new CurseConfig());
        }

        private void LoadWowClients()
        {
            string lookup = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Blizzard Entertainment\\World of Warcraft";
            object value = Registry.GetValue(lookup, "InstallPath", null);
            if (value == null)
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

            if (!this.rb_retail.Enabled)
            {
                this.rb_classic.Checked = true;
                this.rb_retail.Checked = false;
            }
            else
            {
                this.rb_retail.Checked = true;
            }
        }

        private void ListInstalledAddons(DirectoryInfo dir)
        {
            DirectoryInfo addonsDir = new DirectoryInfo(dir.FullName + "\\Interface\\AddOns");
            if(!addonsDir.Exists)
            {
                addonsDir.Create();
            }

            this.UpdateAddonList(addonsDir.GetDirectories());
        }

        private void UpdateAddonList(DirectoryInfo[] directories)
        {
            Control parent = this.pnl_addonWrapper;
            parent.Controls.Clear();
            Dictionary<int, List<Control>> components = new Dictionary<int, List<Control>>();

            Label h1 = new Label();
            h1.Text = "Addon";
            h1.Font = new Font(h1.Font, FontStyle.Bold);
            h1.Location = new Point(0, 0);
            h1.Size = h1.PreferredSize;

            Label h2 = new Label();
            h2.Text = "Version";
            h2.Font = new Font(h2.Font, FontStyle.Bold);
            h2.Location = new Point(0, 0);
            h2.Size = h2.PreferredSize;

            components.Add(0, new List<Control>());
            components.Add(1, new List<Control>());

            components[0].Add(h1);
            components[1].Add(h2);

            parent.Controls.Add(h1);
            parent.Controls.Add(h2);
            foreach (DirectoryInfo dir in directories)
            {
                Label l1 = new Label();
                l1.Text = dir.Name;
                l1.Location = new Point(0, 0);
                l1.Size = l1.PreferredSize;

                Label l2 = new Label();
                l2.Text = "Pre-Installed";
                l2.ForeColor = Color.Red;
                l2.Location = new Point(0, 0);
                l2.Size = l2.PreferredSize;

                parent.Controls.Add(l1);
                parent.Controls.Add(l2);

                components[0].Add(l1);
                components[1].Add(l2);
            }

            int xStart = 5;
            int yStart = 5;

            int xOffset = xStart;
            int yOffset = yStart;

            foreach(KeyValuePair<int, List<Control>> kvp in components)
            {
                foreach(Control control in kvp.Value)
                {
                    int idx = kvp.Value.IndexOf(control);
                    int height = components.Select(x => x.Value[idx].Height).Max();

                    control.Location = new Point(xOffset, yOffset);
                    yOffset += height;
                }

                yOffset = yStart;
                xOffset += kvp.Value.Select(x => x.Width).Max();
            }
        }
        #endregion //Private Methods
    }
}
