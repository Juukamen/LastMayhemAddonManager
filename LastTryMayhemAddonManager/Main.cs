using LastTryMayhemAddonManager.Controls;
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
        #region Constants
        private const string Title = "Last Try Mayhem Addon Manager";
        #endregion //Constants

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
            this.Text = Main.Title;
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

                this.Text = Main.Title + " (World of Warcraft " + rb.Text + " - " + this.versionInfo.FileVersion + ")";
                this.lbl_interface_dynamic.Text = string.Join(", ", this.interfaceNumbers);
                this.lbl_path_dynamic.Text = dir.FullName;

                this.ListInstalledAddons(dir);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            AddonListing form = new AddonListing(this.sources.ToArray());
            if(form.ShowDialog() == DialogResult.OK)
            {
                throw new Exception("Form adding close not implemented yet");
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

        private TocData ReadTOC(DirectoryInfo dir)
        {
            FileInfo tocFile = new FileInfo(dir.FullName + "\\" + dir.Name + ".toc");
            if(tocFile.Exists)
            {
                return new TocData(tocFile);
            }
            return null;
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

            Label header3 = new Label();
            header3.Text = "Game State";
            header3.Font = new Font(header3.Font, FontStyle.Bold);
            header3.Location = new Point(0, 0);
            header3.Size = header3.PreferredSize;

            Label header4 = new Label();
            header4.Text = "Actions";
            header4.Font = new Font(header4.Font, FontStyle.Bold);
            header4.Location = new Point(0, 0);
            header4.Size = header4.PreferredSize;

            components.Add(0, new List<Control>());
            components.Add(1, new List<Control>());
            components.Add(2, new List<Control>());
            components.Add(3, new List<Control>());

            components[0].Add(header1);
            components[1].Add(header2);
            components[2].Add(header3);
            components[3].Add(header4);

            parent.Controls.Add(header1);
            parent.Controls.Add(header2);
            parent.Controls.Add(header3);
            parent.Controls.Add(header4);
            foreach (DirectoryInfo dir in directories)
            {
                TocData tocData = this.ReadTOC(dir);

                Label column1 = new Label();
                column1.Text = dir.Name;
                column1.Location = new Point(0, 0);
                column1.ForeColor = Color.Orange;
                column1.Size = column1.PreferredSize;
                if(tocData != null && tocData.ContainsKey("Title"))
                {
                    column1.Text = tocData["Title"];
                    column1.ForeColor = Color.Green;
                }
                column1.Size = column1.PreferredSize;

                Label column2 = new Label();
                column2.Text = "Unknown";
                column2.ForeColor = Color.Red;
                if(tocData != null && tocData.ContainsKey("Version"))
                {
                    column2.Text = tocData["Version"];
                    column2.ForeColor = Color.Green;
                }
                column2.Location = new Point(0, 0);
                column2.Size = column2.PreferredSize;

                Label column3 = new Label();
                column3.Text = "Unknown";
                column3.ForeColor = Color.Red;
                if(tocData != null && tocData.ContainsKey("Interface"))
                {
                    int tocInterface = int.Parse(tocData["Interface"]);

                    column3.Text = "Ok";
                    column3.ForeColor = Color.Green;
                    if(!this.interfaceNumbers.Contains(tocInterface))
                    {
                        column3.Text = "Outdated (" + tocInterface + ")";
                        column3.ForeColor = Color.Orange;
                    }
                }
                column3.Location = new Point(0, 0);
                column3.Size = column3.PreferredSize;

                AddonActions column4 = new AddonActions(dir, tocData);
                column4.Location = new Point();
                column4.Size = column4.PreferredSize;
                column4.OnDelete += Column4_OnDelete;
                column4.Enable(AddonActions.Buttons.Delete, true);

                parent.Controls.Add(column1);
                parent.Controls.Add(column2);
                parent.Controls.Add(column3);
                parent.Controls.Add(column4);

                components[0].Add(column1);
                components[1].Add(column2);
                components[2].Add(column3);
                components[3].Add(column4);
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

        private void Column4_OnDelete(DirectoryInfo dir, TocData tocData)
        {
            MessageBox.Show("Backup and delete not yet implemented");
            //throw new NotImplementedException();
        }
        #endregion //Private Methods
    }
}
