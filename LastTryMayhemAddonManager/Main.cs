using LastTryMayhemAddonManager.Data;
using LastTryMayhemAddonManager.Data.Configurations;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LastTryMayhemAddonManager
{
    public partial class Main : Form
    {
        #region Members
        private Dictionary<RadioButton, DirectoryInfo> installations;
        private List<ISourceConfiguration> sources;
        private FileVersionInfo versionInfo;
        private int[] interfaceNumbers;
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

                this.versionInfo = FileVersionInfo.GetVersionInfo(dir.FullName + "\\wow" + (rb.Text == this.rb_classic.Text ? "classic" : "") + ".exe");
                this.GetInterfaceNumber(dir);

                this.gb_wow_installation.Text = "World of Warcraft " + rb.Text + " (" + this.versionInfo.FileVersion + " @ " + string.Join(", ", this.interfaceNumbers) + "): " + dir.FullName;

                this.ListInstalledAddons(dir);

                var x = this.GetComposedInterfaceNumbersFromVersion();
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

        private void GetInterfaceNumber(DirectoryInfo dir)
        {
            int[] composed = this.GetComposedInterfaceNumbersFromVersion();

            FileInfo configWtfFile = new FileInfo(dir.FullName + "\\WTF\\config.wtf");
            if (configWtfFile.Exists)
            {
                StreamReader sr = new StreamReader(configWtfFile.FullName);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if(Regex.Match(line, "^set lastAddonVersion", RegexOptions.IgnoreCase).Success)
                    {
                        int value = int.Parse(line.Substring(22, line.Length - 23));
                        if(composed.Contains(value))
                        {
                            this.interfaceNumbers = new int[] { value };
                            return;
                        }
                    }
                }
                sr.Close();
            }
            this.interfaceNumbers = composed;
        }

        private int[] GetComposedInterfaceNumbersFromVersion()
        {
            int min = int.Parse(this.versionInfo.FileMajorPart.ToString() + this.versionInfo.FileMinorPart.ToString().PadLeft(2, '0') + "00");
            int max = int.Parse(this.versionInfo.FileMajorPart.ToString() + this.versionInfo.FileMinorPart.ToString().PadLeft(2, '0') + this.versionInfo.FileBuildPart.ToString().PadLeft(2, '0'));
            List<int> buffer = new List<int>();
            for(int i=min; i<=max; i++)
            {
                buffer.Add(i);
            }
            return buffer.ToArray();
        }

        private void UpdateAddonList(DirectoryInfo[] directories)
        {
            Control parent = this.pnl_addonWrapper;
            parent.Controls.Clear();
            Dictionary<int, List<Control>> components = new Dictionary<int, List<Control>>();

            Label header1 = new Label();
            header1.Text = "Addon";
            header1.Font = new Font(header1.Font, FontStyle.Bold);
            header1.Location = new Point(0, 0);
            header1.Size = header1.PreferredSize;

            Label header2 = new Label();
            header2.Text = "Version";
            header2.Font = new Font(header2.Font, FontStyle.Bold);
            header2.Location = new Point(0, 0);
            header2.Size = header2.PreferredSize;

            components.Add(0, new List<Control>());
            components.Add(1, new List<Control>());

            components[0].Add(header1);
            components[1].Add(header2);

            parent.Controls.Add(header1);
            parent.Controls.Add(header2);
            foreach (DirectoryInfo dir in directories)
            {
                Label column1 = new Label();
                column1.Text = dir.Name;
                column1.Location = new Point(0, 0);
                column1.Size = column1.PreferredSize;

                Label column2 = new Label();
                column2.Text = "Pre-Installed";
                column2.ForeColor = Color.Red;
                column2.Location = new Point(0, 0);
                column2.Size = column2.PreferredSize;

                parent.Controls.Add(column1);
                parent.Controls.Add(column2);

                components[0].Add(column1);
                components[1].Add(column2);
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
