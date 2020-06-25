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
            this.lbl_path_dynamic = new System.Windows.Forms.Label();
            this.lbl_path_static = new System.Windows.Forms.Label();
            this.lbl_interface_static = new System.Windows.Forms.Label();
            this.lbl_interface_dynamic = new System.Windows.Forms.Label();
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
            this.gb_wow_installation.Controls.Add(this.lbl_path_dynamic);
            this.gb_wow_installation.Controls.Add(this.lbl_path_static);
            this.gb_wow_installation.Controls.Add(this.lbl_interface_static);
            this.gb_wow_installation.Controls.Add(this.lbl_interface_dynamic);
            this.gb_wow_installation.Controls.Add(this.rb_classic);
            this.gb_wow_installation.Controls.Add(this.rb_retail);
            this.gb_wow_installation.Location = new System.Drawing.Point(12, 12);
            this.gb_wow_installation.Name = "gb_wow_installation";
            this.gb_wow_installation.Size = new System.Drawing.Size(1234, 116);
            this.gb_wow_installation.TabIndex = 0;
            this.gb_wow_installation.TabStop = false;
            this.gb_wow_installation.Text = "World of Warcraft";
            // 
            // lbl_path_dynamic
            // 
            this.lbl_path_dynamic.AutoSize = true;
            this.lbl_path_dynamic.Location = new System.Drawing.Point(280, 81);
            this.lbl_path_dynamic.Name = "lbl_path_dynamic";
            this.lbl_path_dynamic.Size = new System.Drawing.Size(62, 25);
            this.lbl_path_dynamic.TabIndex = 2;
            this.lbl_path_dynamic.Text = "-path-";
            // 
            // lbl_path_static
            // 
            this.lbl_path_static.AutoSize = true;
            this.lbl_path_static.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_path_static.Location = new System.Drawing.Point(156, 81);
            this.lbl_path_static.Name = "lbl_path_static";
            this.lbl_path_static.Size = new System.Drawing.Size(56, 25);
            this.lbl_path_static.TabIndex = 3;
            this.lbl_path_static.Text = "Path:";
            // 
            // lbl_interface_static
            // 
            this.lbl_interface_static.AutoSize = true;
            this.lbl_interface_static.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_interface_static.Location = new System.Drawing.Point(156, 43);
            this.lbl_interface_static.Name = "lbl_interface_static";
            this.lbl_interface_static.Size = new System.Drawing.Size(118, 25);
            this.lbl_interface_static.TabIndex = 3;
            this.lbl_interface_static.Text = "Interface ID:";
            // 
            // lbl_interface_dynamic
            // 
            this.lbl_interface_dynamic.AutoSize = true;
            this.lbl_interface_dynamic.Location = new System.Drawing.Point(280, 43);
            this.lbl_interface_dynamic.Name = "lbl_interface_dynamic";
            this.lbl_interface_dynamic.Size = new System.Drawing.Size(93, 25);
            this.lbl_interface_dynamic.TabIndex = 2;
            this.lbl_interface_dynamic.Text = "-interface-";
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
            this.gb_myaddons.Location = new System.Drawing.Point(12, 134);
            this.gb_myaddons.Name = "gb_myaddons";
            this.gb_myaddons.Size = new System.Drawing.Size(1234, 822);
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
            this.Text = "--dynamic title-";
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
        private System.Windows.Forms.Label lbl_path_dynamic;
        private System.Windows.Forms.Label lbl_path_static;
        private System.Windows.Forms.Label lbl_interface_static;
        private System.Windows.Forms.Label lbl_interface_dynamic;
    }
}

