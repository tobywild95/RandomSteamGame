namespace RandomSteamGame
{
    partial class randomSteamGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(randomSteamGame));
            this.browse1 = new System.Windows.Forms.Button();
            this.steamDirectory = new System.Windows.Forms.TextBox();
            this.openGame = new System.Windows.Forms.Button();
            this.browseHelpLabel = new System.Windows.Forms.Label();
            this.filterBox = new System.Windows.Forms.ListBox();
            this.applyFilters = new System.Windows.Forms.CheckBox();
            this.autoRun = new System.Windows.Forms.CheckBox();
            this.deselectAll = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.gameLoaded = new System.Windows.Forms.Label();
            this.saveSettings = new System.Windows.Forms.Button();
            this.resetSettings = new System.Windows.Forms.Button();
            this.autoClose = new System.Windows.Forms.CheckBox();
            this.showLogo = new System.Windows.Forms.CheckBox();
            this.filterHelpLabel = new System.Windows.Forms.Label();
            this.filterLocation = new System.Windows.Forms.TextBox();
            this.browse2 = new System.Windows.Forms.Button();
            this.loadFilter = new System.Windows.Forms.Button();
            this.saveFilter = new System.Windows.Forms.Button();
            this.saveFileName = new System.Windows.Forms.TextBox();
            this.overwriteFilter = new System.Windows.Forms.CheckBox();
            this.tabBox = new System.Windows.Forms.TabControl();
            this.rsgHome = new System.Windows.Forms.TabPage();
            this.repopulateFilters = new System.Windows.Forms.Button();
            this.steamOptions = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.deselectAllOptions = new System.Windows.Forms.Button();
            this.selectAllOptions = new System.Windows.Forms.Button();
            this.customLaunchOptions = new System.Windows.Forms.TextBox();
            this.browse3 = new System.Windows.Forms.Button();
            this.overwriteOptions = new System.Windows.Forms.CheckBox();
            this.optionsName = new System.Windows.Forms.TextBox();
            this.saveOptions = new System.Windows.Forms.Button();
            this.loadOptions = new System.Windows.Forms.Button();
            this.optionsPath = new System.Windows.Forms.TextBox();
            this.antiAddictionLaunch = new System.Windows.Forms.CheckBox();
            this.force32BitLaunch = new System.Windows.Forms.CheckBox();
            this.glLaunch = new System.Windows.Forms.CheckBox();
            this.dx11Launch = new System.Windows.Forms.CheckBox();
            this.dx9Launch = new System.Windows.Forms.CheckBox();
            this.noSoundLaunch = new System.Windows.Forms.CheckBox();
            this.noMicLaunch = new System.Windows.Forms.CheckBox();
            this.nod9xLaunch = new System.Windows.Forms.CheckBox();
            this.highPriorityLaunch = new System.Windows.Forms.CheckBox();
            this.noVideoLaunch = new System.Windows.Forms.CheckBox();
            this.developerLaunch = new System.Windows.Forms.CheckBox();
            this.consoleLaunch = new System.Windows.Forms.CheckBox();
            this.autoConfigLaunch = new System.Windows.Forms.CheckBox();
            this.fullscreenLaunch = new System.Windows.Forms.CheckBox();
            this.windowedLaunch = new System.Windows.Forms.CheckBox();
            this.enableOptions = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.customLaunch = new System.Windows.Forms.CheckBox();
            this.launchOptions = new System.Windows.Forms.TextBox();
            this.gameLoadImage = new System.Windows.Forms.PictureBox();
            this.tabBox.SuspendLayout();
            this.rsgHome.SuspendLayout();
            this.steamOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameLoadImage)).BeginInit();
            this.SuspendLayout();
            // 
            // browse1
            // 
            this.browse1.Location = new System.Drawing.Point(297, 23);
            this.browse1.Name = "browse1";
            this.browse1.Size = new System.Drawing.Size(75, 22);
            this.browse1.TabIndex = 0;
            this.browse1.Text = "Browse...";
            this.browse1.UseVisualStyleBackColor = true;
            this.browse1.Click += new System.EventHandler(this.browse1_Click);
            // 
            // steamDirectory
            // 
            this.steamDirectory.Location = new System.Drawing.Point(12, 24);
            this.steamDirectory.Name = "steamDirectory";
            this.steamDirectory.ReadOnly = true;
            this.steamDirectory.Size = new System.Drawing.Size(282, 20);
            this.steamDirectory.TabIndex = 1;
            this.steamDirectory.TextChanged += new System.EventHandler(this.steamDirectory_TextChanged);
            // 
            // openGame
            // 
            this.openGame.Location = new System.Drawing.Point(297, 47);
            this.openGame.Name = "openGame";
            this.openGame.Size = new System.Drawing.Size(75, 22);
            this.openGame.TabIndex = 2;
            this.openGame.Text = "Open Game";
            this.openGame.UseVisualStyleBackColor = true;
            this.openGame.Click += new System.EventHandler(this.openGame_Click);
            // 
            // browseHelpLabel
            // 
            this.browseHelpLabel.AutoSize = true;
            this.browseHelpLabel.Location = new System.Drawing.Point(13, 8);
            this.browseHelpLabel.Name = "browseHelpLabel";
            this.browseHelpLabel.Size = new System.Drawing.Size(157, 13);
            this.browseHelpLabel.TabIndex = 3;
            this.browseHelpLabel.Text = "Please Select Steam\'s Directory";
            // 
            // filterBox
            // 
            this.filterBox.FormattingEnabled = true;
            this.filterBox.Location = new System.Drawing.Point(12, 73);
            this.filterBox.Name = "filterBox";
            this.filterBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.filterBox.Size = new System.Drawing.Size(360, 186);
            this.filterBox.Sorted = true;
            this.filterBox.TabIndex = 4;
            // 
            // applyFilters
            // 
            this.applyFilters.AutoSize = true;
            this.applyFilters.Location = new System.Drawing.Point(12, 290);
            this.applyFilters.Name = "applyFilters";
            this.applyFilters.Size = new System.Drawing.Size(82, 17);
            this.applyFilters.TabIndex = 6;
            this.applyFilters.Text = "Apply Filters";
            this.applyFilters.UseVisualStyleBackColor = true;
            this.applyFilters.CheckedChanged += new System.EventHandler(this.applyFilters_CheckedChanged);
            // 
            // autoRun
            // 
            this.autoRun.AutoSize = true;
            this.autoRun.Location = new System.Drawing.Point(95, 290);
            this.autoRun.Name = "autoRun";
            this.autoRun.Size = new System.Drawing.Size(71, 17);
            this.autoRun.TabIndex = 7;
            this.autoRun.Text = "Auto-Run";
            this.autoRun.UseVisualStyleBackColor = true;
            // 
            // deselectAll
            // 
            this.deselectAll.Location = new System.Drawing.Point(298, 261);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(75, 23);
            this.deselectAll.TabIndex = 8;
            this.deselectAll.Text = "Deselect All";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // selectAll
            // 
            this.selectAll.Location = new System.Drawing.Point(221, 261);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(75, 23);
            this.selectAll.TabIndex = 9;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // gameLoaded
            // 
            this.gameLoaded.AutoSize = true;
            this.gameLoaded.Location = new System.Drawing.Point(44, 359);
            this.gameLoaded.Name = "gameLoaded";
            this.gameLoaded.Size = new System.Drawing.Size(171, 13);
            this.gameLoaded.TabIndex = 11;
            this.gameLoaded.Text = "Welcome to Random Steam Game";
            // 
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(11, 261);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(85, 23);
            this.saveSettings.TabIndex = 13;
            this.saveSettings.Text = "Save Settings";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // resetSettings
            // 
            this.resetSettings.Location = new System.Drawing.Point(98, 261);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.Size = new System.Drawing.Size(85, 23);
            this.resetSettings.TabIndex = 14;
            this.resetSettings.Text = "Reset Settings";
            this.resetSettings.UseVisualStyleBackColor = true;
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // autoClose
            // 
            this.autoClose.AutoSize = true;
            this.autoClose.Location = new System.Drawing.Point(167, 290);
            this.autoClose.Name = "autoClose";
            this.autoClose.Size = new System.Drawing.Size(77, 17);
            this.autoClose.TabIndex = 15;
            this.autoClose.Text = "Auto-Close";
            this.autoClose.UseVisualStyleBackColor = true;
            // 
            // showLogo
            // 
            this.showLogo.AutoSize = true;
            this.showLogo.Location = new System.Drawing.Point(242, 290);
            this.showLogo.Name = "showLogo";
            this.showLogo.Size = new System.Drawing.Size(112, 17);
            this.showLogo.TabIndex = 16;
            this.showLogo.Text = "Show Logo / Text";
            this.showLogo.UseVisualStyleBackColor = true;
            this.showLogo.CheckedChanged += new System.EventHandler(this.showLogo_CheckedChanged);
            // 
            // filterHelpLabel
            // 
            this.filterHelpLabel.AutoSize = true;
            this.filterHelpLabel.Location = new System.Drawing.Point(12, 57);
            this.filterHelpLabel.Name = "filterHelpLabel";
            this.filterHelpLabel.Size = new System.Drawing.Size(80, 13);
            this.filterHelpLabel.TabIndex = 17;
            this.filterHelpLabel.Text = "Available Filters";
            // 
            // filterLocation
            // 
            this.filterLocation.Location = new System.Drawing.Point(12, 313);
            this.filterLocation.Name = "filterLocation";
            this.filterLocation.ReadOnly = true;
            this.filterLocation.Size = new System.Drawing.Size(282, 20);
            this.filterLocation.TabIndex = 18;
            // 
            // browse2
            // 
            this.browse2.Location = new System.Drawing.Point(297, 312);
            this.browse2.Name = "browse2";
            this.browse2.Size = new System.Drawing.Size(75, 22);
            this.browse2.TabIndex = 19;
            this.browse2.Text = "Browse...";
            this.browse2.UseVisualStyleBackColor = true;
            this.browse2.Click += new System.EventHandler(this.browse2_Click);
            // 
            // loadFilter
            // 
            this.loadFilter.Location = new System.Drawing.Point(297, 336);
            this.loadFilter.Name = "loadFilter";
            this.loadFilter.Size = new System.Drawing.Size(75, 23);
            this.loadFilter.TabIndex = 20;
            this.loadFilter.Text = "Load Filter";
            this.loadFilter.UseVisualStyleBackColor = true;
            this.loadFilter.Click += new System.EventHandler(this.loadFilter_Click);
            // 
            // saveFilter
            // 
            this.saveFilter.Location = new System.Drawing.Point(220, 336);
            this.saveFilter.Name = "saveFilter";
            this.saveFilter.Size = new System.Drawing.Size(75, 23);
            this.saveFilter.TabIndex = 21;
            this.saveFilter.Text = "Save Filter";
            this.saveFilter.UseVisualStyleBackColor = true;
            this.saveFilter.Click += new System.EventHandler(this.saveFilter_Click);
            // 
            // saveFileName
            // 
            this.saveFileName.Location = new System.Drawing.Point(49, 337);
            this.saveFileName.Name = "saveFileName";
            this.saveFileName.Size = new System.Drawing.Size(168, 20);
            this.saveFileName.TabIndex = 22;
            this.saveFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.saveFileName_KeyDown);
            // 
            // overwriteFilter
            // 
            this.overwriteFilter.AutoSize = true;
            this.overwriteFilter.Location = new System.Drawing.Point(281, 360);
            this.overwriteFilter.Name = "overwriteFilter";
            this.overwriteFilter.Size = new System.Drawing.Size(96, 17);
            this.overwriteFilter.TabIndex = 23;
            this.overwriteFilter.Text = "Overwrite Filter";
            this.overwriteFilter.UseVisualStyleBackColor = true;
            // 
            // tabBox
            // 
            this.tabBox.Controls.Add(this.rsgHome);
            this.tabBox.Controls.Add(this.steamOptions);
            this.tabBox.Location = new System.Drawing.Point(3, 3);
            this.tabBox.Name = "tabBox";
            this.tabBox.SelectedIndex = 0;
            this.tabBox.Size = new System.Drawing.Size(393, 411);
            this.tabBox.TabIndex = 24;
            // 
            // rsgHome
            // 
            this.rsgHome.BackColor = System.Drawing.Color.White;
            this.rsgHome.Controls.Add(this.repopulateFilters);
            this.rsgHome.Controls.Add(this.browse2);
            this.rsgHome.Controls.Add(this.overwriteFilter);
            this.rsgHome.Controls.Add(this.browse1);
            this.rsgHome.Controls.Add(this.saveFileName);
            this.rsgHome.Controls.Add(this.steamDirectory);
            this.rsgHome.Controls.Add(this.saveFilter);
            this.rsgHome.Controls.Add(this.openGame);
            this.rsgHome.Controls.Add(this.loadFilter);
            this.rsgHome.Controls.Add(this.browseHelpLabel);
            this.rsgHome.Controls.Add(this.filterBox);
            this.rsgHome.Controls.Add(this.filterLocation);
            this.rsgHome.Controls.Add(this.applyFilters);
            this.rsgHome.Controls.Add(this.filterHelpLabel);
            this.rsgHome.Controls.Add(this.autoRun);
            this.rsgHome.Controls.Add(this.showLogo);
            this.rsgHome.Controls.Add(this.deselectAll);
            this.rsgHome.Controls.Add(this.autoClose);
            this.rsgHome.Controls.Add(this.selectAll);
            this.rsgHome.Controls.Add(this.resetSettings);
            this.rsgHome.Controls.Add(this.gameLoaded);
            this.rsgHome.Controls.Add(this.saveSettings);
            this.rsgHome.Controls.Add(this.gameLoadImage);
            this.rsgHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rsgHome.Location = new System.Drawing.Point(4, 22);
            this.rsgHome.Name = "rsgHome";
            this.rsgHome.Padding = new System.Windows.Forms.Padding(3);
            this.rsgHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rsgHome.Size = new System.Drawing.Size(385, 385);
            this.rsgHome.TabIndex = 0;
            this.rsgHome.Text = "RSG Home";
            // 
            // repopulateFilters
            // 
            this.repopulateFilters.Location = new System.Drawing.Point(181, 47);
            this.repopulateFilters.Name = "repopulateFilters";
            this.repopulateFilters.Size = new System.Drawing.Size(113, 22);
            this.repopulateFilters.TabIndex = 24;
            this.repopulateFilters.Text = "Re-Populate Filters";
            this.repopulateFilters.UseVisualStyleBackColor = true;
            // 
            // steamOptions
            // 
            this.steamOptions.Controls.Add(this.label2);
            this.steamOptions.Controls.Add(this.deselectAllOptions);
            this.steamOptions.Controls.Add(this.selectAllOptions);
            this.steamOptions.Controls.Add(this.customLaunchOptions);
            this.steamOptions.Controls.Add(this.browse3);
            this.steamOptions.Controls.Add(this.overwriteOptions);
            this.steamOptions.Controls.Add(this.optionsName);
            this.steamOptions.Controls.Add(this.saveOptions);
            this.steamOptions.Controls.Add(this.loadOptions);
            this.steamOptions.Controls.Add(this.optionsPath);
            this.steamOptions.Controls.Add(this.antiAddictionLaunch);
            this.steamOptions.Controls.Add(this.force32BitLaunch);
            this.steamOptions.Controls.Add(this.glLaunch);
            this.steamOptions.Controls.Add(this.dx11Launch);
            this.steamOptions.Controls.Add(this.dx9Launch);
            this.steamOptions.Controls.Add(this.noSoundLaunch);
            this.steamOptions.Controls.Add(this.noMicLaunch);
            this.steamOptions.Controls.Add(this.nod9xLaunch);
            this.steamOptions.Controls.Add(this.highPriorityLaunch);
            this.steamOptions.Controls.Add(this.noVideoLaunch);
            this.steamOptions.Controls.Add(this.developerLaunch);
            this.steamOptions.Controls.Add(this.consoleLaunch);
            this.steamOptions.Controls.Add(this.autoConfigLaunch);
            this.steamOptions.Controls.Add(this.fullscreenLaunch);
            this.steamOptions.Controls.Add(this.windowedLaunch);
            this.steamOptions.Controls.Add(this.enableOptions);
            this.steamOptions.Controls.Add(this.label1);
            this.steamOptions.Controls.Add(this.customLaunch);
            this.steamOptions.Controls.Add(this.launchOptions);
            this.steamOptions.Location = new System.Drawing.Point(4, 22);
            this.steamOptions.Name = "steamOptions";
            this.steamOptions.Padding = new System.Windows.Forms.Padding(3);
            this.steamOptions.Size = new System.Drawing.Size(385, 385);
            this.steamOptions.TabIndex = 1;
            this.steamOptions.Text = "Steam Options";
            this.steamOptions.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Not all games support launch options";
            // 
            // deselectAllOptions
            // 
            this.deselectAllOptions.Location = new System.Drawing.Point(296, 113);
            this.deselectAllOptions.Name = "deselectAllOptions";
            this.deselectAllOptions.Size = new System.Drawing.Size(83, 22);
            this.deselectAllOptions.TabIndex = 32;
            this.deselectAllOptions.Text = "Deselect All";
            this.deselectAllOptions.UseVisualStyleBackColor = true;
            this.deselectAllOptions.Click += new System.EventHandler(this.deselectAllOptions_Click);
            // 
            // selectAllOptions
            // 
            this.selectAllOptions.Location = new System.Drawing.Point(212, 113);
            this.selectAllOptions.Name = "selectAllOptions";
            this.selectAllOptions.Size = new System.Drawing.Size(83, 22);
            this.selectAllOptions.TabIndex = 31;
            this.selectAllOptions.Text = "Select All";
            this.selectAllOptions.UseVisualStyleBackColor = true;
            this.selectAllOptions.Click += new System.EventHandler(this.selectAllOptions_Click);
            // 
            // customLaunchOptions
            // 
            this.customLaunchOptions.Location = new System.Drawing.Point(2, 307);
            this.customLaunchOptions.Multiline = true;
            this.customLaunchOptions.Name = "customLaunchOptions";
            this.customLaunchOptions.ReadOnly = true;
            this.customLaunchOptions.Size = new System.Drawing.Size(378, 74);
            this.customLaunchOptions.TabIndex = 30;
            // 
            // browse3
            // 
            this.browse3.Location = new System.Drawing.Point(296, 141);
            this.browse3.Name = "browse3";
            this.browse3.Size = new System.Drawing.Size(83, 22);
            this.browse3.TabIndex = 25;
            this.browse3.Text = "Browse...";
            this.browse3.UseVisualStyleBackColor = true;
            this.browse3.Click += new System.EventHandler(this.browse3_Click);
            // 
            // overwriteOptions
            // 
            this.overwriteOptions.AutoSize = true;
            this.overwriteOptions.Location = new System.Drawing.Point(277, 193);
            this.overwriteOptions.Name = "overwriteOptions";
            this.overwriteOptions.Size = new System.Drawing.Size(110, 17);
            this.overwriteOptions.TabIndex = 29;
            this.overwriteOptions.Text = "Overwrite Options";
            this.overwriteOptions.UseVisualStyleBackColor = true;
            // 
            // optionsName
            // 
            this.optionsName.Location = new System.Drawing.Point(6, 165);
            this.optionsName.Name = "optionsName";
            this.optionsName.Size = new System.Drawing.Size(205, 20);
            this.optionsName.TabIndex = 28;
            this.optionsName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.optionsName_KeyDown);
            // 
            // saveOptions
            // 
            this.saveOptions.Location = new System.Drawing.Point(213, 164);
            this.saveOptions.Name = "saveOptions";
            this.saveOptions.Size = new System.Drawing.Size(82, 23);
            this.saveOptions.TabIndex = 27;
            this.saveOptions.Text = "Save Options";
            this.saveOptions.UseVisualStyleBackColor = true;
            this.saveOptions.Click += new System.EventHandler(this.saveOptions_Click);
            // 
            // loadOptions
            // 
            this.loadOptions.Location = new System.Drawing.Point(296, 164);
            this.loadOptions.Name = "loadOptions";
            this.loadOptions.Size = new System.Drawing.Size(83, 23);
            this.loadOptions.TabIndex = 26;
            this.loadOptions.Text = "Load Options";
            this.loadOptions.UseVisualStyleBackColor = true;
            this.loadOptions.Click += new System.EventHandler(this.loadOptions_Click);
            // 
            // optionsPath
            // 
            this.optionsPath.Location = new System.Drawing.Point(6, 142);
            this.optionsPath.Name = "optionsPath";
            this.optionsPath.ReadOnly = true;
            this.optionsPath.Size = new System.Drawing.Size(288, 20);
            this.optionsPath.TabIndex = 24;
            // 
            // antiAddictionLaunch
            // 
            this.antiAddictionLaunch.AutoSize = true;
            this.antiAddictionLaunch.Location = new System.Drawing.Point(6, 89);
            this.antiAddictionLaunch.Name = "antiAddictionLaunch";
            this.antiAddictionLaunch.Size = new System.Drawing.Size(91, 17);
            this.antiAddictionLaunch.TabIndex = 19;
            this.antiAddictionLaunch.Text = "Anti-Addiction";
            this.antiAddictionLaunch.UseVisualStyleBackColor = true;
            this.antiAddictionLaunch.CheckedChanged += new System.EventHandler(this.antiAddictionLaunch_CheckedChanged);
            // 
            // force32BitLaunch
            // 
            this.force32BitLaunch.AutoSize = true;
            this.force32BitLaunch.Location = new System.Drawing.Point(139, 89);
            this.force32BitLaunch.Name = "force32BitLaunch";
            this.force32BitLaunch.Size = new System.Drawing.Size(83, 17);
            this.force32BitLaunch.TabIndex = 18;
            this.force32BitLaunch.Text = "Force 32-Bit";
            this.force32BitLaunch.UseVisualStyleBackColor = true;
            this.force32BitLaunch.CheckedChanged += new System.EventHandler(this.force32BitLaunch_CheckedChanged);
            // 
            // glLaunch
            // 
            this.glLaunch.AutoSize = true;
            this.glLaunch.Location = new System.Drawing.Point(268, 89);
            this.glLaunch.Name = "glLaunch";
            this.glLaunch.Size = new System.Drawing.Size(70, 17);
            this.glLaunch.TabIndex = 17;
            this.glLaunch.Text = "Force GL";
            this.glLaunch.UseVisualStyleBackColor = true;
            this.glLaunch.CheckedChanged += new System.EventHandler(this.glLaunch_CheckedChanged);
            // 
            // dx11Launch
            // 
            this.dx11Launch.AutoSize = true;
            this.dx11Launch.Location = new System.Drawing.Point(268, 72);
            this.dx11Launch.Name = "dx11Launch";
            this.dx11Launch.Size = new System.Drawing.Size(83, 17);
            this.dx11Launch.TabIndex = 16;
            this.dx11Launch.Text = "Force DX11";
            this.dx11Launch.UseVisualStyleBackColor = true;
            this.dx11Launch.CheckedChanged += new System.EventHandler(this.dx11Launch_CheckedChanged);
            // 
            // dx9Launch
            // 
            this.dx9Launch.AutoSize = true;
            this.dx9Launch.Location = new System.Drawing.Point(268, 55);
            this.dx9Launch.Name = "dx9Launch";
            this.dx9Launch.Size = new System.Drawing.Size(77, 17);
            this.dx9Launch.TabIndex = 15;
            this.dx9Launch.Text = "Force DX9";
            this.dx9Launch.UseVisualStyleBackColor = true;
            this.dx9Launch.CheckedChanged += new System.EventHandler(this.dx9Launch_CheckedChanged);
            // 
            // noSoundLaunch
            // 
            this.noSoundLaunch.AutoSize = true;
            this.noSoundLaunch.Location = new System.Drawing.Point(268, 38);
            this.noSoundLaunch.Name = "noSoundLaunch";
            this.noSoundLaunch.Size = new System.Drawing.Size(74, 17);
            this.noSoundLaunch.TabIndex = 14;
            this.noSoundLaunch.Text = "No Sound";
            this.noSoundLaunch.UseVisualStyleBackColor = true;
            this.noSoundLaunch.CheckedChanged += new System.EventHandler(this.noSoundLaunch_CheckedChanged);
            // 
            // noMicLaunch
            // 
            this.noMicLaunch.AutoSize = true;
            this.noMicLaunch.Location = new System.Drawing.Point(268, 21);
            this.noMicLaunch.Name = "noMicLaunch";
            this.noMicLaunch.Size = new System.Drawing.Size(101, 17);
            this.noMicLaunch.TabIndex = 13;
            this.noMicLaunch.Text = "No-Mic Settings";
            this.noMicLaunch.UseVisualStyleBackColor = true;
            this.noMicLaunch.CheckedChanged += new System.EventHandler(this.noMicLaunch_CheckedChanged);
            // 
            // nod9xLaunch
            // 
            this.nod9xLaunch.AutoSize = true;
            this.nod9xLaunch.Location = new System.Drawing.Point(139, 72);
            this.nod9xLaunch.Name = "nod9xLaunch";
            this.nod9xLaunch.Size = new System.Drawing.Size(82, 17);
            this.nod9xLaunch.TabIndex = 12;
            this.nod9xLaunch.Text = "No D3D9ex";
            this.nod9xLaunch.UseVisualStyleBackColor = true;
            this.nod9xLaunch.CheckedChanged += new System.EventHandler(this.nod9xLaunch_CheckedChanged);
            // 
            // highPriorityLaunch
            // 
            this.highPriorityLaunch.AutoSize = true;
            this.highPriorityLaunch.Location = new System.Drawing.Point(139, 55);
            this.highPriorityLaunch.Name = "highPriorityLaunch";
            this.highPriorityLaunch.Size = new System.Drawing.Size(82, 17);
            this.highPriorityLaunch.TabIndex = 11;
            this.highPriorityLaunch.Text = "High Priority";
            this.highPriorityLaunch.UseVisualStyleBackColor = true;
            this.highPriorityLaunch.CheckedChanged += new System.EventHandler(this.highPriorityLaunch_CheckedChanged);
            // 
            // noVideoLaunch
            // 
            this.noVideoLaunch.AutoSize = true;
            this.noVideoLaunch.Location = new System.Drawing.Point(139, 38);
            this.noVideoLaunch.Name = "noVideoLaunch";
            this.noVideoLaunch.Size = new System.Drawing.Size(70, 17);
            this.noVideoLaunch.TabIndex = 10;
            this.noVideoLaunch.Text = "No Video";
            this.noVideoLaunch.UseVisualStyleBackColor = true;
            this.noVideoLaunch.CheckedChanged += new System.EventHandler(this.noVideoLaunch_CheckedChanged);
            // 
            // developerLaunch
            // 
            this.developerLaunch.AutoSize = true;
            this.developerLaunch.Location = new System.Drawing.Point(139, 21);
            this.developerLaunch.Name = "developerLaunch";
            this.developerLaunch.Size = new System.Drawing.Size(75, 17);
            this.developerLaunch.TabIndex = 9;
            this.developerLaunch.Text = "Developer";
            this.developerLaunch.UseVisualStyleBackColor = true;
            this.developerLaunch.CheckedChanged += new System.EventHandler(this.developerLaunch_CheckedChanged);
            // 
            // consoleLaunch
            // 
            this.consoleLaunch.AutoSize = true;
            this.consoleLaunch.Location = new System.Drawing.Point(6, 55);
            this.consoleLaunch.Name = "consoleLaunch";
            this.consoleLaunch.Size = new System.Drawing.Size(64, 17);
            this.consoleLaunch.TabIndex = 8;
            this.consoleLaunch.Text = "Console";
            this.consoleLaunch.UseVisualStyleBackColor = true;
            this.consoleLaunch.CheckedChanged += new System.EventHandler(this.consoleLaunch_CheckedChanged);
            // 
            // autoConfigLaunch
            // 
            this.autoConfigLaunch.AutoSize = true;
            this.autoConfigLaunch.Location = new System.Drawing.Point(6, 72);
            this.autoConfigLaunch.Name = "autoConfigLaunch";
            this.autoConfigLaunch.Size = new System.Drawing.Size(81, 17);
            this.autoConfigLaunch.TabIndex = 7;
            this.autoConfigLaunch.Text = "Auto-Config";
            this.autoConfigLaunch.UseVisualStyleBackColor = true;
            this.autoConfigLaunch.CheckedChanged += new System.EventHandler(this.autoConfigLaunch_CheckedChanged);
            // 
            // fullscreenLaunch
            // 
            this.fullscreenLaunch.AutoSize = true;
            this.fullscreenLaunch.Location = new System.Drawing.Point(6, 38);
            this.fullscreenLaunch.Name = "fullscreenLaunch";
            this.fullscreenLaunch.Size = new System.Drawing.Size(74, 17);
            this.fullscreenLaunch.TabIndex = 6;
            this.fullscreenLaunch.Text = "Fullscreen";
            this.fullscreenLaunch.UseVisualStyleBackColor = true;
            this.fullscreenLaunch.CheckedChanged += new System.EventHandler(this.fullscreenLaunch_CheckedChanged);
            // 
            // windowedLaunch
            // 
            this.windowedLaunch.AutoSize = true;
            this.windowedLaunch.Location = new System.Drawing.Point(6, 21);
            this.windowedLaunch.Name = "windowedLaunch";
            this.windowedLaunch.Size = new System.Drawing.Size(77, 17);
            this.windowedLaunch.TabIndex = 5;
            this.windowedLaunch.Text = "Windowed";
            this.windowedLaunch.UseVisualStyleBackColor = true;
            this.windowedLaunch.CheckedChanged += new System.EventHandler(this.windowedLaunch_CheckedChanged);
            // 
            // enableOptions
            // 
            this.enableOptions.AutoSize = true;
            this.enableOptions.Location = new System.Drawing.Point(141, 193);
            this.enableOptions.Name = "enableOptions";
            this.enableOptions.Size = new System.Drawing.Size(137, 17);
            this.enableOptions.TabIndex = 4;
            this.enableOptions.Text = "Enable Launch Options";
            this.enableOptions.UseVisualStyleBackColor = true;
            this.enableOptions.CheckedChanged += new System.EventHandler(this.enableOptions_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Check a box to add a Steam Launch Option to the game on launch.";
            // 
            // customLaunch
            // 
            this.customLaunch.AutoSize = true;
            this.customLaunch.Location = new System.Drawing.Point(247, 290);
            this.customLaunch.Name = "customLaunch";
            this.customLaunch.Size = new System.Drawing.Size(139, 17);
            this.customLaunch.TabIndex = 1;
            this.customLaunch.Text = "Custom Launch Options";
            this.customLaunch.UseVisualStyleBackColor = true;
            this.customLaunch.CheckedChanged += new System.EventHandler(this.customLaunch_CheckedChanged);
            // 
            // launchOptions
            // 
            this.launchOptions.Location = new System.Drawing.Point(3, 210);
            this.launchOptions.Multiline = true;
            this.launchOptions.Name = "launchOptions";
            this.launchOptions.ReadOnly = true;
            this.launchOptions.Size = new System.Drawing.Size(378, 74);
            this.launchOptions.TabIndex = 0;
            // 
            // gameLoadImage
            // 
            this.gameLoadImage.BackColor = System.Drawing.Color.White;
            this.gameLoadImage.Location = new System.Drawing.Point(11, 339);
            this.gameLoadImage.Name = "gameLoadImage";
            this.gameLoadImage.Size = new System.Drawing.Size(32, 32);
            this.gameLoadImage.TabIndex = 12;
            this.gameLoadImage.TabStop = false;
            // 
            // randomSteamGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 416);
            this.Controls.Add(this.tabBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "randomSteamGame";
            this.Text = "RandomSteamGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.randomSteamGame_FormClosing);
            this.tabBox.ResumeLayout(false);
            this.rsgHome.ResumeLayout(false);
            this.rsgHome.PerformLayout();
            this.steamOptions.ResumeLayout(false);
            this.steamOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameLoadImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button browse1;
        private System.Windows.Forms.TextBox steamDirectory;
        private System.Windows.Forms.Button openGame;
        private System.Windows.Forms.Label browseHelpLabel;
        private System.Windows.Forms.ListBox filterBox;
        private System.Windows.Forms.CheckBox applyFilters;
        private System.Windows.Forms.CheckBox autoRun;
        private System.Windows.Forms.Button deselectAll;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Label gameLoaded;
        private System.Windows.Forms.PictureBox gameLoadImage;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Button resetSettings;
        private System.Windows.Forms.CheckBox autoClose;
        private System.Windows.Forms.CheckBox showLogo;
        private System.Windows.Forms.Label filterHelpLabel;
        private System.Windows.Forms.TextBox filterLocation;
        private System.Windows.Forms.Button browse2;
        private System.Windows.Forms.Button loadFilter;
        private System.Windows.Forms.Button saveFilter;
        private System.Windows.Forms.TextBox saveFileName;
        private System.Windows.Forms.CheckBox overwriteFilter;
        private System.Windows.Forms.TabControl tabBox;
        private System.Windows.Forms.TabPage rsgHome;
        private System.Windows.Forms.TabPage steamOptions;
        private System.Windows.Forms.Button repopulateFilters;
        private System.Windows.Forms.TextBox launchOptions;
        private System.Windows.Forms.CheckBox customLaunch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox enableOptions;
        private System.Windows.Forms.CheckBox nod9xLaunch;
        private System.Windows.Forms.CheckBox highPriorityLaunch;
        private System.Windows.Forms.CheckBox noVideoLaunch;
        private System.Windows.Forms.CheckBox developerLaunch;
        private System.Windows.Forms.CheckBox consoleLaunch;
        private System.Windows.Forms.CheckBox autoConfigLaunch;
        private System.Windows.Forms.CheckBox fullscreenLaunch;
        private System.Windows.Forms.CheckBox windowedLaunch;
        private System.Windows.Forms.CheckBox antiAddictionLaunch;
        private System.Windows.Forms.CheckBox force32BitLaunch;
        private System.Windows.Forms.CheckBox glLaunch;
        private System.Windows.Forms.CheckBox dx11Launch;
        private System.Windows.Forms.CheckBox dx9Launch;
        private System.Windows.Forms.CheckBox noSoundLaunch;
        private System.Windows.Forms.CheckBox noMicLaunch;
        private System.Windows.Forms.Button browse3;
        private System.Windows.Forms.CheckBox overwriteOptions;
        private System.Windows.Forms.TextBox optionsName;
        private System.Windows.Forms.Button saveOptions;
        private System.Windows.Forms.Button loadOptions;
        private System.Windows.Forms.TextBox optionsPath;
        private System.Windows.Forms.TextBox customLaunchOptions;
        private System.Windows.Forms.Button deselectAllOptions;
        private System.Windows.Forms.Button selectAllOptions;
        private System.Windows.Forms.Label label2;
    }
}

