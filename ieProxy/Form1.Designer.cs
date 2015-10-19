namespace ieProxy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.proxyGroupBox = new System.Windows.Forms.GroupBox();
            this.proxyBypassLocalCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyShsidButton = new System.Windows.Forms.Button();
            this.proxyGoAgentButton = new System.Windows.Forms.Button();
            this.proxyEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyPortLabel = new System.Windows.Forms.Label();
            this.proxyAddressLabel = new System.Windows.Forms.Label();
            this.pacGroupBox = new System.Windows.Forms.GroupBox();
            this.pacEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.pacAutoDetectCheckBox = new System.Windows.Forms.CheckBox();
            this.pacLabel = new System.Windows.Forms.Label();
            this.pacPathTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.reloadButton = new System.Windows.Forms.Button();
            this.pacBrowseButton = new ieProxy.BrowseFileButton();
            this.proxyAddressTextBox = new ieProxy.CheckedTextBox();
            this.proxyPortTextBox = new ieProxy.CheckedTextBox();
            this.proxyGroupBox.SuspendLayout();
            this.pacGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proxyAddressTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.proxyPortTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // proxyGroupBox
            // 
            this.proxyGroupBox.Controls.Add(this.proxyAddressTextBox);
            this.proxyGroupBox.Controls.Add(this.proxyPortTextBox);
            this.proxyGroupBox.Controls.Add(this.proxyBypassLocalCheckBox);
            this.proxyGroupBox.Controls.Add(this.proxyShsidButton);
            this.proxyGroupBox.Controls.Add(this.proxyGoAgentButton);
            this.proxyGroupBox.Controls.Add(this.proxyEnableCheckBox);
            this.proxyGroupBox.Controls.Add(this.proxyPortLabel);
            this.proxyGroupBox.Controls.Add(this.proxyAddressLabel);
            this.proxyGroupBox.Location = new System.Drawing.Point(12, 12);
            this.proxyGroupBox.Name = "proxyGroupBox";
            this.proxyGroupBox.Size = new System.Drawing.Size(333, 94);
            this.proxyGroupBox.TabIndex = 0;
            this.proxyGroupBox.TabStop = false;
            this.proxyGroupBox.Text = "Proxy server";
            // 
            // proxyBypassLocalCheckBox
            // 
            this.proxyBypassLocalCheckBox.AutoSize = true;
            this.proxyBypassLocalCheckBox.Location = new System.Drawing.Point(146, 67);
            this.proxyBypassLocalCheckBox.Name = "proxyBypassLocalCheckBox";
            this.proxyBypassLocalCheckBox.Size = new System.Drawing.Size(179, 17);
            this.proxyBypassLocalCheckBox.TabIndex = 6;
            this.proxyBypassLocalCheckBox.Text = "Bypass proxy for local addresses";
            this.proxyBypassLocalCheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyShsidButton
            // 
            this.proxyShsidButton.Location = new System.Drawing.Point(243, 33);
            this.proxyShsidButton.Name = "proxyShsidButton";
            this.proxyShsidButton.Size = new System.Drawing.Size(75, 23);
            this.proxyShsidButton.TabIndex = 5;
            this.proxyShsidButton.Text = "SHSID";
            this.proxyShsidButton.UseVisualStyleBackColor = true;
            this.proxyShsidButton.Click += new System.EventHandler(this.proxyShsidButton_Click);
            // 
            // proxyGoAgentButton
            // 
            this.proxyGoAgentButton.Location = new System.Drawing.Point(162, 33);
            this.proxyGoAgentButton.Name = "proxyGoAgentButton";
            this.proxyGoAgentButton.Size = new System.Drawing.Size(75, 23);
            this.proxyGoAgentButton.TabIndex = 4;
            this.proxyGoAgentButton.Text = "GoAgent";
            this.proxyGoAgentButton.UseVisualStyleBackColor = true;
            this.proxyGoAgentButton.Click += new System.EventHandler(this.proxyGoAgentButton_Click);
            // 
            // proxyEnableCheckBox
            // 
            this.proxyEnableCheckBox.AutoSize = true;
            this.proxyEnableCheckBox.Location = new System.Drawing.Point(15, 67);
            this.proxyEnableCheckBox.Name = "proxyEnableCheckBox";
            this.proxyEnableCheckBox.Size = new System.Drawing.Size(87, 17);
            this.proxyEnableCheckBox.TabIndex = 1;
            this.proxyEnableCheckBox.Text = "Enable proxy";
            this.proxyEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyPortLabel
            // 
            this.proxyPortLabel.AutoSize = true;
            this.proxyPortLabel.Location = new System.Drawing.Point(105, 19);
            this.proxyPortLabel.Name = "proxyPortLabel";
            this.proxyPortLabel.Size = new System.Drawing.Size(26, 13);
            this.proxyPortLabel.TabIndex = 3;
            this.proxyPortLabel.Text = "Port";
            // 
            // proxyAddressLabel
            // 
            this.proxyAddressLabel.AutoSize = true;
            this.proxyAddressLabel.Location = new System.Drawing.Point(12, 19);
            this.proxyAddressLabel.Name = "proxyAddressLabel";
            this.proxyAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.proxyAddressLabel.TabIndex = 1;
            this.proxyAddressLabel.Text = "Address";
            // 
            // pacGroupBox
            // 
            this.pacGroupBox.Controls.Add(this.pacEnableCheckBox);
            this.pacGroupBox.Controls.Add(this.pacAutoDetectCheckBox);
            this.pacGroupBox.Controls.Add(this.pacBrowseButton);
            this.pacGroupBox.Controls.Add(this.pacLabel);
            this.pacGroupBox.Controls.Add(this.pacPathTextBox);
            this.pacGroupBox.Location = new System.Drawing.Point(12, 112);
            this.pacGroupBox.Name = "pacGroupBox";
            this.pacGroupBox.Size = new System.Drawing.Size(333, 94);
            this.pacGroupBox.TabIndex = 2;
            this.pacGroupBox.TabStop = false;
            this.pacGroupBox.Text = "Automatic configuration";
            // 
            // pacEnableCheckBox
            // 
            this.pacEnableCheckBox.AutoSize = true;
            this.pacEnableCheckBox.Location = new System.Drawing.Point(15, 67);
            this.pacEnableCheckBox.Name = "pacEnableCheckBox";
            this.pacEnableCheckBox.Size = new System.Drawing.Size(172, 17);
            this.pacEnableCheckBox.TabIndex = 4;
            this.pacEnableCheckBox.Text = "Enable autoconfiguration script";
            this.pacEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // pacAutoDetectCheckBox
            // 
            this.pacAutoDetectCheckBox.AutoSize = true;
            this.pacAutoDetectCheckBox.Location = new System.Drawing.Point(208, 67);
            this.pacAutoDetectCheckBox.Name = "pacAutoDetectCheckBox";
            this.pacAutoDetectCheckBox.Size = new System.Drawing.Size(117, 17);
            this.pacAutoDetectCheckBox.TabIndex = 3;
            this.pacAutoDetectCheckBox.Text = "Autodetect settings";
            this.pacAutoDetectCheckBox.UseVisualStyleBackColor = true;
            // 
            // pacLabel
            // 
            this.pacLabel.AutoSize = true;
            this.pacLabel.Location = new System.Drawing.Point(12, 19);
            this.pacLabel.Name = "pacLabel";
            this.pacLabel.Size = new System.Drawing.Size(152, 13);
            this.pacLabel.TabIndex = 1;
            this.pacLabel.Text = "Autoconfiguration script (*.pac)";
            // 
            // pacPathTextBox
            // 
            this.pacPathTextBox.Location = new System.Drawing.Point(15, 35);
            this.pacPathTextBox.Name = "pacPathTextBox";
            this.pacPathTextBox.Size = new System.Drawing.Size(222, 20);
            this.pacPathTextBox.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 212);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(107, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(238, 212);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(107, 23);
            this.aboutButton.TabIndex = 4;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(125, 212);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(107, 23);
            this.reloadButton.TabIndex = 5;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // pacBrowseButton
            // 
            this.pacBrowseButton.DialogTitle = null;
            this.pacBrowseButton.Filter = "Proxy autoconfig files|*.pac|All files|*";
            this.pacBrowseButton.InitialDirectory = null;
            this.pacBrowseButton.Location = new System.Drawing.Point(243, 33);
            this.pacBrowseButton.MultiSelect = false;
            this.pacBrowseButton.Name = "pacBrowseButton";
            this.pacBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.pacBrowseButton.TabIndex = 2;
            this.pacBrowseButton.Text = "Browse...";
            this.pacBrowseButton.UseVisualStyleBackColor = true;
            this.pacBrowseButton.FileSelect += new System.EventHandler<ieProxy.FileSelectEventArgs>(this.pacBrowseButton_FileSelect);
            // 
            // proxyAddressTextBox
            // 
            this.proxyAddressTextBox.AdditionalChecks = null;
            this.proxyAddressTextBox.BackColor = System.Drawing.Color.White;
            this.proxyAddressTextBox.Location = new System.Drawing.Point(15, 35);
            this.proxyAddressTextBox.Name = "proxyAddressTextBox";
            this.proxyAddressTextBox.Size = new System.Drawing.Size(87, 20);
            this.proxyAddressTextBox.TabIndex = 8;
            // 
            // proxyPortTextBox
            // 
            this.proxyPortTextBox.AcceptedInput = ieProxy.CheckedTextBox.InputType.Numeric;
            this.proxyPortTextBox.AdditionalChecks = null;
            this.proxyPortTextBox.BackColor = System.Drawing.Color.White;
            this.proxyPortTextBox.Location = new System.Drawing.Point(108, 35);
            this.proxyPortTextBox.MaxLength = 5;
            this.proxyPortTextBox.Name = "proxyPortTextBox";
            this.proxyPortTextBox.Size = new System.Drawing.Size(48, 20);
            this.proxyPortTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 247);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pacGroupBox);
            this.Controls.Add(this.proxyGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ieProxy";
            this.proxyGroupBox.ResumeLayout(false);
            this.proxyGroupBox.PerformLayout();
            this.pacGroupBox.ResumeLayout(false);
            this.pacGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proxyAddressTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.proxyPortTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox proxyGroupBox;
        private System.Windows.Forms.Button proxyGoAgentButton;
        private System.Windows.Forms.Label proxyPortLabel;
        private System.Windows.Forms.Label proxyAddressLabel;
        private System.Windows.Forms.CheckBox proxyEnableCheckBox;
        private System.Windows.Forms.GroupBox pacGroupBox;
        private System.Windows.Forms.Label pacLabel;
        private System.Windows.Forms.TextBox pacPathTextBox;
        private BrowseFileButton pacBrowseButton;
        private System.Windows.Forms.Button proxyShsidButton;
        private System.Windows.Forms.CheckBox proxyBypassLocalCheckBox;
        private System.Windows.Forms.CheckBox pacEnableCheckBox;
        private System.Windows.Forms.CheckBox pacAutoDetectCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button aboutButton;
        private CheckedTextBox proxyPortTextBox;
        private CheckedTextBox proxyAddressTextBox;
        private System.Windows.Forms.Button reloadButton;

    }
}

