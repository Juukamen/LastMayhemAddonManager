namespace LastTryMayhemAddonManager
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gb_wow_installation = new System.Windows.Forms.GroupBox();
            this.rb_classic = new System.Windows.Forms.RadioButton();
            this.rb_retail = new System.Windows.Forms.RadioButton();
            this.gb_myaddons = new System.Windows.Forms.GroupBox();
            this.pnl_addonWrapper = new System.Windows.Forms.Panel();
            this.gb_wow_installation.SuspendLayout();
            this.gb_myaddons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_wow_installation
            // 
            this.gb_wow_installation.Controls.Add(this.rb_classic);
            this.gb_wow_installation.Controls.Add(this.rb_retail);
            this.gb_wow_installation.Location = new System.Drawing.Point(12, 12);
            this.gb_wow_installation.Name = "gb_wow_installation";
            this.gb_wow_installation.Size = new System.Drawing.Size(1234, 117);
            this.gb_wow_installation.TabIndex = 0;
            this.gb_wow_installation.TabStop = false;
            this.gb_wow_installation.Text = "World of Warcraft";
            // 
            // rb_classic
            // 
            this.rb_classic.AutoSize = true;
            this.rb_classic.Location = new System.Drawing.Point(20, 79);
            this.rb_classic.Name = "rb_classic";
            this.rb_classic.Size = new System.Drawing.Size(89, 29);
            this.rb_classic.TabIndex = 1;
            this.rb_classic.Text = "Classic";
            this.rb_classic.UseVisualStyleBackColor = true;
            this.rb_classic.CheckedChanged += new System.EventHandler(this.WowClientCheckedChanged);
            // 
            // rb_retail
            // 
            this.rb_retail.AutoSize = true;
            this.rb_retail.Location = new System.Drawing.Point(20, 43);
            this.rb_retail.Name = "rb_retail";
            this.rb_retail.Size = new System.Drawing.Size(80, 29);
            this.rb_retail.TabIndex = 0;
            this.rb_retail.TabStop = true;
            this.rb_retail.Text = "Retail";
            this.rb_retail.UseVisualStyleBackColor = true;
            this.rb_retail.CheckedChanged += new System.EventHandler(this.WowClientCheckedChanged);
            // 
            // gb_myaddons
            // 
            this.gb_myaddons.Controls.Add(this.pnl_addonWrapper);
            this.gb_myaddons.Location = new System.Drawing.Point(12, 135);
            this.gb_myaddons.Name = "gb_myaddons";
            this.gb_myaddons.Size = new System.Drawing.Size(1234, 821);
            this.gb_myaddons.TabIndex = 1;
            this.gb_myaddons.TabStop = false;
            this.gb_myaddons.Text = "My Addons";
            // 
            // pnl_addonWrapper
            // 
            this.pnl_addonWrapper.AutoScroll = true;
            this.pnl_addonWrapper.Location = new System.Drawing.Point(6, 30);
            this.pnl_addonWrapper.Name = "pnl_addonWrapper";
            this.pnl_addonWrapper.Size = new System.Drawing.Size(1222, 785);
            this.pnl_addonWrapper.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 968);
            this.Controls.Add(this.gb_myaddons);
            this.Controls.Add(this.gb_wow_installation);
            this.Name = "Main";
            this.Text = "Form1";
            this.gb_wow_installation.ResumeLayout(false);
            this.gb_wow_installation.PerformLayout();
            this.gb_myaddons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_wow_installation;
        private System.Windows.Forms.RadioButton rb_classic;
        private System.Windows.Forms.RadioButton rb_retail;
        private System.Windows.Forms.GroupBox gb_myaddons;
        private System.Windows.Forms.Panel pnl_addonWrapper;
    }
}

