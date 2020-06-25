using System.Drawing;
using System.Windows.Forms;
using System;
using System.IO;
using LastTryMayhemAddonManager.Data;

namespace LastTryMayhemAddonManager.Controls
{
    internal class AddonActions : Panel
    {
        #region Members
        private Button btn_update;
        private Button btn_delete;
        private DirectoryInfo dir;
        private TocData tocData;
        #endregion //Members

        #region Delegates
        public delegate void ClickHandler(DirectoryInfo dir, TocData tocData);
        #endregion //Delegates

        #region Events
        public event ClickHandler OnUpdate;
        public event ClickHandler OnDelete;
        #endregion //Events

        #region Enums
        public enum Buttons
        {
            Update, Delete
        }
        #endregion //Enums

        #region Constructors
        public AddonActions(DirectoryInfo dir, TocData tocData)
        {
            this.tocData = tocData;
            this.dir = dir;
            this.btn_update = new Button();
            this.btn_update.Location = new Point(0, 0);
            this.btn_update.Text = "Update";
            this.btn_update.Click += Btn_update_Click;
            this.btn_update.Size = this.btn_update.PreferredSize;
            this.Controls.Add(this.btn_update);

            this.btn_delete = new Button();
            this.btn_delete.Location = new Point(this.btn_update.Width, 0);
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_delete.Size = this.btn_delete.PreferredSize;
            this.Controls.Add(this.btn_delete);

            this.btn_update.Enabled = false;
            this.btn_delete.Enabled = false;
        }
        #endregion //Constructors

        #region Public Methods
        public void Enable(Buttons button, bool value)
        {
            if(button == Buttons.Delete)
            {
                this.btn_delete.Enabled = value;
            }
            if(button == Buttons.Update)
            {
                this.btn_update.Enabled = value;
            }
        }
        #endregion //Public Methods

        #region Private Methods
        #region events
        private void Btn_delete_Click(object sender, EventArgs e)
        {
            string name = this.dir.Name;
            if(this.tocData != null && this.tocData.ContainsKey("Title"))
            {
                name = this.tocData["Title"];
            }

            if (MessageBox.Show("Are you sure you want to delete the addon\n  - " + name + "", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.OnDelete?.Invoke(this.dir, this.tocData);
            }
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            this.OnUpdate?.Invoke(this.dir, this.tocData);
        }
        #endregion //Events
        #endregion //Private Methods
    }
}
