namespace RSTK
{
    partial class RSTKForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSTKForm));
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.lblStatus = new Marzersoft.Themes.ThemedLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRocksmithPath = new Marzersoft.Themes.ThemedButton();
            this.themedLabel3 = new Marzersoft.Themes.ThemedLabel();
            this.tbRocksmithPath = new Marzersoft.Themes.ThemedTextBox();
            this.dlgBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnLaunch = new Marzersoft.Themes.ThemedButton();
            this.btnLaunchSteam = new Marzersoft.Themes.ThemedButton();
            this.panLaunchButtons = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.panRocksmithPath = new System.Windows.Forms.Panel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.themedLabel4 = new Marzersoft.Themes.ThemedLabel();
            this.cbResolution = new Marzersoft.Controls.ComboBoxRow();
            this.cbFullscreenMode = new Marzersoft.Controls.ComboBoxRow();
            this.checkEmulateFullscreen = new Marzersoft.Controls.CheckBoxRow();
            this.checkCycleDisplays = new Marzersoft.Controls.CheckBoxRow();
            this.themedLabel1 = new Marzersoft.Themes.ThemedLabel();
            this.checkExclusiveMode = new Marzersoft.Controls.CheckBoxRow();
            this.checkUltraLowLatencyMode = new Marzersoft.Controls.CheckBoxRow();
            this.trackLatencyBuffer = new Marzersoft.Controls.TrackBarRow();
            this.trackMaxOutputBufferSize = new Marzersoft.Controls.TrackBarRow();
            this.lblEffectiveLatency = new Marzersoft.Controls.LabelRow();
            this.checkMicrophone = new Marzersoft.Controls.CheckBoxRow();
            this.checkDumpAudioLog = new Marzersoft.Controls.CheckBoxRow();
            this.themedLabel2 = new Marzersoft.Themes.ThemedLabel();
            this.checkStartup = new Marzersoft.Controls.CheckBoxRow();
            this.checkStartMinimized = new Marzersoft.Controls.CheckBoxRow();
            this.checkLaunchAnywhere = new Marzersoft.Controls.CheckBoxRow();
            this.checkAutoHide = new Marzersoft.Controls.CheckBoxRow();
            this.checkExitWhenRocksmithTerminated = new Marzersoft.Controls.CheckBoxRow();
            this.tbLaunchCommand = new Marzersoft.Controls.TextBoxRow();
            this.panStatus = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.panLaunchButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.panRocksmithPath.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLatencyBuffer.TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxOutputBufferSize.TrackBar)).BeginInit();
            this.panStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbStatus.Image = global::RSTK.Properties.Resources.rstk_waiting_64;
            this.pbStatus.Location = new System.Drawing.Point(0, 0);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(92, 100);
            this.pbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbStatus.TabIndex = 2;
            this.pbStatus.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Accent = ((uint)(0u));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 100);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(92, 57);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Waiting for game path.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnRocksmithPath
            // 
            this.btnRocksmithPath.Accent = ((uint)(0u));
            this.btnRocksmithPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRocksmithPath.FlatAppearance.BorderSize = 0;
            this.btnRocksmithPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRocksmithPath.Image = global::RSTK.Properties.Resources.open_16;
            this.btnRocksmithPath.Location = new System.Drawing.Point(1078, 35);
            this.btnRocksmithPath.Margin = new System.Windows.Forms.Padding(0);
            this.btnRocksmithPath.Name = "btnRocksmithPath";
            this.btnRocksmithPath.Size = new System.Drawing.Size(26, 26);
            this.btnRocksmithPath.TabIndex = 2;
            this.btnRocksmithPath.UseVisualStyleBackColor = true;
            // 
            // themedLabel3
            // 
            this.themedLabel3.Accent = ((uint)(0u));
            this.themedLabel3.AccentMode = true;
            this.themedLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themedLabel3.FontSize = 1;
            this.themedLabel3.Location = new System.Drawing.Point(0, 0);
            this.themedLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.themedLabel3.Name = "themedLabel3";
            this.themedLabel3.Size = new System.Drawing.Size(1104, 30);
            this.themedLabel3.TabIndex = 25;
            this.themedLabel3.Text = "Rocksmith Location";
            // 
            // tbRocksmithPath
            // 
            this.tbRocksmithPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRocksmithPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRocksmithPath.Location = new System.Drawing.Point(3, 35);
            this.tbRocksmithPath.Margin = new System.Windows.Forms.Padding(0);
            this.tbRocksmithPath.Name = "tbRocksmithPath";
            this.tbRocksmithPath.ReadOnly = true;
            this.tbRocksmithPath.Size = new System.Drawing.Size(1070, 20);
            this.tbRocksmithPath.TabIndex = 1;
            // 
            // dlgBrowse
            // 
            this.dlgBrowse.Filter = "Rocksmith executables|Rocksmith2014.exe;Rocksmith.exe";
            this.dlgBrowse.Title = "Browse to Rocksmith2014.exe";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Accent = ((uint)(0u));
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.FlatAppearance.BorderSize = 0;
            this.btnLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaunch.Image = global::RSTK.Properties.Resources.gamepad_24;
            this.btnLaunch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLaunch.Location = new System.Drawing.Point(0, 20);
            this.btnLaunch.Margin = new System.Windows.Forms.Padding(0);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnLaunch.Size = new System.Drawing.Size(92, 70);
            this.btnLaunch.TabIndex = 100;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip.SetToolTip(this.btnLaunch, "Launches Rocksmith by directly launching the game\'s executable. Unless you have e" +
        "xplicit reason to do this\r\n(you will know if you do), it is recommended that you" +
        " use Launch via Steam instead.");
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.EnabledChanged += new System.EventHandler(this.btnLaunch_EnabledChanged);
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnLaunchSteam
            // 
            this.btnLaunchSteam.Accent = ((uint)(0u));
            this.btnLaunchSteam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunchSteam.FlatAppearance.BorderSize = 0;
            this.btnLaunchSteam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaunchSteam.Image = global::RSTK.Properties.Resources.steam_24;
            this.btnLaunchSteam.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLaunchSteam.Location = new System.Drawing.Point(0, 103);
            this.btnLaunchSteam.Margin = new System.Windows.Forms.Padding(0);
            this.btnLaunchSteam.Name = "btnLaunchSteam";
            this.btnLaunchSteam.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnLaunchSteam.Size = new System.Drawing.Size(92, 70);
            this.btnLaunchSteam.TabIndex = 101;
            this.btnLaunchSteam.Text = "Launch via Steam";
            this.btnLaunchSteam.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip.SetToolTip(this.btnLaunchSteam, "Launches the game via Steam.");
            this.btnLaunchSteam.UseVisualStyleBackColor = true;
            this.btnLaunchSteam.EnabledChanged += new System.EventHandler(this.btnLaunchSteam_EnabledChanged);
            this.btnLaunchSteam.Click += new System.EventHandler(this.btnLaunchSteam_Click);
            // 
            // panLaunchButtons
            // 
            this.panLaunchButtons.Controls.Add(this.btnLaunch);
            this.panLaunchButtons.Controls.Add(this.btnLaunchSteam);
            this.panLaunchButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLaunchButtons.Location = new System.Drawing.Point(10, 167);
            this.panLaunchButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panLaunchButtons.Name = "panLaunchButtons";
            this.panLaunchButtons.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panLaunchButtons.Size = new System.Drawing.Size(92, 747);
            this.panLaunchButtons.TabIndex = 28;
            this.panLaunchButtons.Visible = false;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitter.IsSplitterFixed = true;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.panRocksmithPath);
            this.splitter.Panel1.Controls.Add(this.flowLayoutPanel);
            this.splitter.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.panLaunchButtons);
            this.splitter.Panel2.Controls.Add(this.panStatus);
            this.splitter.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitter.Size = new System.Drawing.Size(1237, 924);
            this.splitter.SplitterDistance = 1124;
            this.splitter.SplitterWidth = 1;
            this.splitter.TabIndex = 29;
            this.splitter.TabStop = false;
            // 
            // panRocksmithPath
            // 
            this.panRocksmithPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panRocksmithPath.Controls.Add(this.themedLabel3);
            this.panRocksmithPath.Controls.Add(this.tbRocksmithPath);
            this.panRocksmithPath.Controls.Add(this.btnRocksmithPath);
            this.panRocksmithPath.Location = new System.Drawing.Point(10, 10);
            this.panRocksmithPath.Name = "panRocksmithPath";
            this.panRocksmithPath.Size = new System.Drawing.Size(1104, 79);
            this.panRocksmithPath.TabIndex = 31;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Controls.Add(this.themedLabel4);
            this.flowLayoutPanel.Controls.Add(this.cbResolution);
            this.flowLayoutPanel.Controls.Add(this.cbFullscreenMode);
            this.flowLayoutPanel.Controls.Add(this.checkEmulateFullscreen);
            this.flowLayoutPanel.Controls.Add(this.checkCycleDisplays);
            this.flowLayoutPanel.Controls.Add(this.themedLabel1);
            this.flowLayoutPanel.Controls.Add(this.checkExclusiveMode);
            this.flowLayoutPanel.Controls.Add(this.checkUltraLowLatencyMode);
            this.flowLayoutPanel.Controls.Add(this.trackLatencyBuffer);
            this.flowLayoutPanel.Controls.Add(this.trackMaxOutputBufferSize);
            this.flowLayoutPanel.Controls.Add(this.lblEffectiveLatency);
            this.flowLayoutPanel.Controls.Add(this.checkMicrophone);
            this.flowLayoutPanel.Controls.Add(this.checkDumpAudioLog);
            this.flowLayoutPanel.Controls.Add(this.themedLabel2);
            this.flowLayoutPanel.Controls.Add(this.checkStartup);
            this.flowLayoutPanel.Controls.Add(this.checkStartMinimized);
            this.flowLayoutPanel.Controls.Add(this.checkLaunchAnywhere);
            this.flowLayoutPanel.Controls.Add(this.checkAutoHide);
            this.flowLayoutPanel.Controls.Add(this.checkExitWhenRocksmithTerminated);
            this.flowLayoutPanel.Controls.Add(this.tbLaunchCommand);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(10, 124);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1104, 790);
            this.flowLayoutPanel.TabIndex = 30;
            this.flowLayoutPanel.Visible = false;
            this.flowLayoutPanel.WrapContents = false;
            this.flowLayoutPanel.Resize += new System.EventHandler(this.flowLayoutPanel_Resize);
            // 
            // themedLabel4
            // 
            this.themedLabel4.Accent = ((uint)(0u));
            this.themedLabel4.AccentMode = true;
            this.themedLabel4.FontSize = 1;
            this.themedLabel4.Location = new System.Drawing.Point(0, 0);
            this.themedLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.themedLabel4.Name = "themedLabel4";
            this.themedLabel4.Size = new System.Drawing.Size(442, 30);
            this.themedLabel4.TabIndex = 26;
            this.themedLabel4.Text = "Renderer/Window Settings";
            // 
            // cbResolution
            // 
            this.cbResolution.Caption = "Resolution";
            // 
            // 
            // 
            this.cbResolution.ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbResolution.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbResolution.ComboBox.Location = new System.Drawing.Point(0, 5);
            this.cbResolution.ComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.cbResolution.ComboBox.Name = "";
            this.cbResolution.ComboBox.Size = new System.Drawing.Size(177, 21);
            this.cbResolution.ComboBox.TabIndex = 0;
            this.cbResolution.Location = new System.Drawing.Point(0, 30);
            this.cbResolution.Margin = new System.Windows.Forms.Padding(0);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(442, 30);
            this.cbResolution.TabIndex = 27;
            // 
            // cbFullscreenMode
            // 
            this.cbFullscreenMode.Caption = "Fullscreen mode";
            // 
            // 
            // 
            this.cbFullscreenMode.ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFullscreenMode.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFullscreenMode.ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFullscreenMode.ComboBox.Location = new System.Drawing.Point(0, 5);
            this.cbFullscreenMode.ComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.cbFullscreenMode.ComboBox.Name = "";
            this.cbFullscreenMode.ComboBox.Size = new System.Drawing.Size(177, 21);
            this.cbFullscreenMode.ComboBox.TabIndex = 0;
            this.cbFullscreenMode.Location = new System.Drawing.Point(0, 60);
            this.cbFullscreenMode.Margin = new System.Windows.Forms.Padding(0);
            this.cbFullscreenMode.Name = "cbFullscreenMode";
            this.cbFullscreenMode.Size = new System.Drawing.Size(442, 30);
            this.cbFullscreenMode.TabIndex = 28;
            this.toolTip.SetToolTip(this.cbFullscreenMode, resources.GetString("cbFullscreenMode.ToolTip"));
            // 
            // checkEmulateFullscreen
            // 
            this.checkEmulateFullscreen.Caption = "Emulate fullscreen";
            // 
            // 
            // 
            this.checkEmulateFullscreen.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkEmulateFullscreen.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkEmulateFullscreen.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkEmulateFullscreen.CheckBox.Name = "";
            this.checkEmulateFullscreen.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkEmulateFullscreen.CheckBox.TabIndex = 0;
            this.checkEmulateFullscreen.CheckBox.Text = "(only applies in Windowed mode)";
            this.checkEmulateFullscreen.Location = new System.Drawing.Point(0, 90);
            this.checkEmulateFullscreen.Margin = new System.Windows.Forms.Padding(0);
            this.checkEmulateFullscreen.Name = "checkEmulateFullscreen";
            this.checkEmulateFullscreen.Size = new System.Drawing.Size(442, 30);
            this.checkEmulateFullscreen.TabIndex = 29;
            this.checkEmulateFullscreen.Text = "(only applies in Windowed mode)";
            this.toolTip.SetToolTip(this.checkEmulateFullscreen, resources.GetString("checkEmulateFullscreen.ToolTip"));
            // 
            // checkCycleDisplays
            // 
            this.checkCycleDisplays.Caption = "Cycle displays when Rocksmith exits";
            // 
            // 
            // 
            this.checkCycleDisplays.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkCycleDisplays.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkCycleDisplays.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkCycleDisplays.CheckBox.Name = "";
            this.checkCycleDisplays.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkCycleDisplays.CheckBox.TabIndex = 0;
            this.checkCycleDisplays.CheckBox.Text = "(only applies in ExclusiveFullscreen mode)";
            this.checkCycleDisplays.Location = new System.Drawing.Point(0, 120);
            this.checkCycleDisplays.Margin = new System.Windows.Forms.Padding(0);
            this.checkCycleDisplays.Name = "checkCycleDisplays";
            this.checkCycleDisplays.Size = new System.Drawing.Size(442, 30);
            this.checkCycleDisplays.TabIndex = 42;
            this.checkCycleDisplays.Text = "(only applies in ExclusiveFullscreen mode)";
            this.toolTip.SetToolTip(this.checkCycleDisplays, resources.GetString("checkCycleDisplays.ToolTip"));
            // 
            // themedLabel1
            // 
            this.themedLabel1.Accent = ((uint)(0u));
            this.themedLabel1.AccentMode = true;
            this.themedLabel1.FontSize = 1;
            this.themedLabel1.Location = new System.Drawing.Point(0, 170);
            this.themedLabel1.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.themedLabel1.Name = "themedLabel1";
            this.themedLabel1.Size = new System.Drawing.Size(442, 30);
            this.themedLabel1.TabIndex = 30;
            this.themedLabel1.Text = "Audio Settings";
            // 
            // checkExclusiveMode
            // 
            this.checkExclusiveMode.Caption = "Exclusive mode";
            // 
            // 
            // 
            this.checkExclusiveMode.CheckBox.Checked = true;
            this.checkExclusiveMode.CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkExclusiveMode.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkExclusiveMode.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkExclusiveMode.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkExclusiveMode.CheckBox.Name = "";
            this.checkExclusiveMode.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkExclusiveMode.CheckBox.TabIndex = 0;
            this.checkExclusiveMode.Location = new System.Drawing.Point(0, 200);
            this.checkExclusiveMode.Margin = new System.Windows.Forms.Padding(0);
            this.checkExclusiveMode.Name = "checkExclusiveMode";
            this.checkExclusiveMode.Size = new System.Drawing.Size(442, 30);
            this.checkExclusiveMode.TabIndex = 31;
            this.toolTip.SetToolTip(this.checkExclusiveMode, resources.GetString("checkExclusiveMode.ToolTip"));
            // 
            // checkUltraLowLatencyMode
            // 
            this.checkUltraLowLatencyMode.Caption = "Ultra-low latency mode";
            // 
            // 
            // 
            this.checkUltraLowLatencyMode.CheckBox.Checked = true;
            this.checkUltraLowLatencyMode.CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUltraLowLatencyMode.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkUltraLowLatencyMode.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkUltraLowLatencyMode.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkUltraLowLatencyMode.CheckBox.Name = "";
            this.checkUltraLowLatencyMode.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkUltraLowLatencyMode.CheckBox.TabIndex = 0;
            this.checkUltraLowLatencyMode.Location = new System.Drawing.Point(0, 230);
            this.checkUltraLowLatencyMode.Margin = new System.Windows.Forms.Padding(0);
            this.checkUltraLowLatencyMode.Name = "checkUltraLowLatencyMode";
            this.checkUltraLowLatencyMode.Size = new System.Drawing.Size(442, 30);
            this.checkUltraLowLatencyMode.TabIndex = 32;
            this.toolTip.SetToolTip(this.checkUltraLowLatencyMode, resources.GetString("checkUltraLowLatencyMode.ToolTip"));
            // 
            // trackLatencyBuffer
            // 
            this.trackLatencyBuffer.Caption = "Latency buffer";
            this.trackLatencyBuffer.Location = new System.Drawing.Point(0, 260);
            this.trackLatencyBuffer.Margin = new System.Windows.Forms.Padding(0);
            this.trackLatencyBuffer.Name = "trackLatencyBuffer";
            this.trackLatencyBuffer.Size = new System.Drawing.Size(442, 30);
            this.trackLatencyBuffer.TabIndex = 33;
            this.toolTip.SetToolTip(this.trackLatencyBuffer, resources.GetString("trackLatencyBuffer.ToolTip"));
            // 
            // 
            // 
            this.trackLatencyBuffer.TrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackLatencyBuffer.TrackBar.AutoSize = false;
            this.trackLatencyBuffer.TrackBar.LargeChange = 1;
            this.trackLatencyBuffer.TrackBar.Location = new System.Drawing.Point(48, 0);
            this.trackLatencyBuffer.TrackBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackLatencyBuffer.TrackBar.Maximum = 16;
            this.trackLatencyBuffer.TrackBar.Minimum = 1;
            this.trackLatencyBuffer.TrackBar.Name = "";
            this.trackLatencyBuffer.TrackBar.TabIndex = 1;
            this.trackLatencyBuffer.TrackBar.Value = 4;
            // 
            // trackMaxOutputBufferSize
            // 
            this.trackMaxOutputBufferSize.Caption = "Max output buffer size";
            this.trackMaxOutputBufferSize.Location = new System.Drawing.Point(0, 290);
            this.trackMaxOutputBufferSize.Margin = new System.Windows.Forms.Padding(0);
            this.trackMaxOutputBufferSize.Name = "trackMaxOutputBufferSize";
            this.trackMaxOutputBufferSize.Size = new System.Drawing.Size(442, 30);
            this.trackMaxOutputBufferSize.TabIndex = 34;
            this.toolTip.SetToolTip(this.trackMaxOutputBufferSize, resources.GetString("trackMaxOutputBufferSize.ToolTip"));
            // 
            // 
            // 
            this.trackMaxOutputBufferSize.TrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackMaxOutputBufferSize.TrackBar.AutoSize = false;
            this.trackMaxOutputBufferSize.TrackBar.LargeChange = 32;
            this.trackMaxOutputBufferSize.TrackBar.Location = new System.Drawing.Point(48, 0);
            this.trackMaxOutputBufferSize.TrackBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackMaxOutputBufferSize.TrackBar.Maximum = 2048;
            this.trackMaxOutputBufferSize.TrackBar.Name = "";
            this.trackMaxOutputBufferSize.TrackBar.SmallChange = 8;
            this.trackMaxOutputBufferSize.TrackBar.TabIndex = 1;
            this.trackMaxOutputBufferSize.TrackBar.TickFrequency = 32;
            // 
            // lblEffectiveLatency
            // 
            this.lblEffectiveLatency.Caption = "Latency (estimated)";
            this.lblEffectiveLatency.Location = new System.Drawing.Point(0, 320);
            this.lblEffectiveLatency.Margin = new System.Windows.Forms.Padding(0);
            this.lblEffectiveLatency.Name = "lblEffectiveLatency";
            this.lblEffectiveLatency.Size = new System.Drawing.Size(442, 30);
            this.lblEffectiveLatency.TabIndex = 35;
            this.lblEffectiveLatency.TabStop = false;
            this.toolTip.SetToolTip(this.lblEffectiveLatency, resources.GetString("lblEffectiveLatency.ToolTip"));
            // 
            // checkMicrophone
            // 
            this.checkMicrophone.Caption = "Enable microphone";
            // 
            // 
            // 
            this.checkMicrophone.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkMicrophone.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkMicrophone.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkMicrophone.CheckBox.Name = "";
            this.checkMicrophone.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkMicrophone.CheckBox.TabIndex = 0;
            this.checkMicrophone.Location = new System.Drawing.Point(0, 350);
            this.checkMicrophone.Margin = new System.Windows.Forms.Padding(0);
            this.checkMicrophone.Name = "checkMicrophone";
            this.checkMicrophone.Size = new System.Drawing.Size(442, 30);
            this.checkMicrophone.TabIndex = 36;
            this.toolTip.SetToolTip(this.checkMicrophone, "Set this value to true to enable the use of voice microphone.\r\nThis is duplicated" +
        " in the Rocksmith 2014 menus.");
            // 
            // checkDumpAudioLog
            // 
            this.checkDumpAudioLog.Caption = "Dump audio log";
            // 
            // 
            // 
            this.checkDumpAudioLog.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkDumpAudioLog.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkDumpAudioLog.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkDumpAudioLog.CheckBox.Name = "";
            this.checkDumpAudioLog.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkDumpAudioLog.CheckBox.TabIndex = 0;
            this.checkDumpAudioLog.Location = new System.Drawing.Point(0, 380);
            this.checkDumpAudioLog.Margin = new System.Windows.Forms.Padding(0);
            this.checkDumpAudioLog.Name = "checkDumpAudioLog";
            this.checkDumpAudioLog.Size = new System.Drawing.Size(442, 30);
            this.checkDumpAudioLog.TabIndex = 38;
            this.toolTip.SetToolTip(this.checkDumpAudioLog, resources.GetString("checkDumpAudioLog.ToolTip"));
            // 
            // themedLabel2
            // 
            this.themedLabel2.Accent = ((uint)(0u));
            this.themedLabel2.AccentMode = true;
            this.themedLabel2.FontSize = 1;
            this.themedLabel2.Location = new System.Drawing.Point(0, 430);
            this.themedLabel2.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.themedLabel2.Name = "themedLabel2";
            this.themedLabel2.Size = new System.Drawing.Size(442, 30);
            this.themedLabel2.TabIndex = 37;
            this.themedLabel2.Text = "RSTK Settings";
            // 
            // checkStartup
            // 
            this.checkStartup.Caption = "Run when Windows starts";
            // 
            // 
            // 
            this.checkStartup.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkStartup.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkStartup.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkStartup.CheckBox.Name = "";
            this.checkStartup.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkStartup.CheckBox.TabIndex = 0;
            this.checkStartup.Location = new System.Drawing.Point(0, 460);
            this.checkStartup.Margin = new System.Windows.Forms.Padding(0);
            this.checkStartup.Name = "checkStartup";
            this.checkStartup.Size = new System.Drawing.Size(442, 30);
            this.checkStartup.TabIndex = 39;
            // 
            // checkStartMinimized
            // 
            this.checkStartMinimized.Caption = "Start minimized to tray";
            // 
            // 
            // 
            this.checkStartMinimized.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkStartMinimized.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkStartMinimized.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkStartMinimized.CheckBox.Name = "";
            this.checkStartMinimized.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkStartMinimized.CheckBox.TabIndex = 0;
            this.checkStartMinimized.Location = new System.Drawing.Point(0, 490);
            this.checkStartMinimized.Margin = new System.Windows.Forms.Padding(0);
            this.checkStartMinimized.Name = "checkStartMinimized";
            this.checkStartMinimized.Size = new System.Drawing.Size(442, 30);
            this.checkStartMinimized.TabIndex = 41;
            this.toolTip.SetToolTip(this.checkStartMinimized, "With this option enabled, the RSTK window will not be shown when the application\r" +
        "\nis launched, and will instead be immediately minimized to the system task tray." +
        "");
            // 
            // checkLaunchAnywhere
            // 
            this.checkLaunchAnywhere.Caption = "CTRL+ SHIFT+ R launches Rocksmith";
            // 
            // 
            // 
            this.checkLaunchAnywhere.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkLaunchAnywhere.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkLaunchAnywhere.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkLaunchAnywhere.CheckBox.Name = "";
            this.checkLaunchAnywhere.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkLaunchAnywhere.CheckBox.TabIndex = 0;
            this.checkLaunchAnywhere.Location = new System.Drawing.Point(0, 520);
            this.checkLaunchAnywhere.Margin = new System.Windows.Forms.Padding(0);
            this.checkLaunchAnywhere.Name = "checkLaunchAnywhere";
            this.checkLaunchAnywhere.Size = new System.Drawing.Size(442, 30);
            this.checkLaunchAnywhere.TabIndex = 44;
            this.toolTip.SetToolTip(this.checkLaunchAnywhere, "With this option enabled, RSTK will register a global system hotkey bound to CTRL" +
        " + SHIFT + R\r\nfor quickly launching Rocksmith (via Steam) from anywhere.");
            // 
            // checkAutoHide
            // 
            this.checkAutoHide.Caption = "Hide while Rocksmith is running";
            // 
            // 
            // 
            this.checkAutoHide.CheckBox.Checked = true;
            this.checkAutoHide.CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAutoHide.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkAutoHide.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkAutoHide.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkAutoHide.CheckBox.Name = "";
            this.checkAutoHide.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkAutoHide.CheckBox.TabIndex = 0;
            this.checkAutoHide.Location = new System.Drawing.Point(0, 550);
            this.checkAutoHide.Margin = new System.Windows.Forms.Padding(0);
            this.checkAutoHide.Name = "checkAutoHide";
            this.checkAutoHide.Size = new System.Drawing.Size(442, 30);
            this.checkAutoHide.TabIndex = 40;
            this.toolTip.SetToolTip(this.checkAutoHide, "With this option enabled, RSTK will minimize itself Rocksmith is launched, and wi" +
        "ll restore itself\r\nwhen Rocksmith exits (if RSTK was not already hidden when Roc" +
        "ksmith was launched).");
            // 
            // checkExitWhenRocksmithTerminated
            // 
            this.checkExitWhenRocksmithTerminated.Caption = "Exit when Rocksmith exits";
            // 
            // 
            // 
            this.checkExitWhenRocksmithTerminated.CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkExitWhenRocksmithTerminated.CheckBox.Location = new System.Drawing.Point(0, 0);
            this.checkExitWhenRocksmithTerminated.CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkExitWhenRocksmithTerminated.CheckBox.Name = "";
            this.checkExitWhenRocksmithTerminated.CheckBox.Size = new System.Drawing.Size(177, 30);
            this.checkExitWhenRocksmithTerminated.CheckBox.TabIndex = 0;
            this.checkExitWhenRocksmithTerminated.Location = new System.Drawing.Point(0, 580);
            this.checkExitWhenRocksmithTerminated.Margin = new System.Windows.Forms.Padding(0);
            this.checkExitWhenRocksmithTerminated.Name = "checkExitWhenRocksmithTerminated";
            this.checkExitWhenRocksmithTerminated.Size = new System.Drawing.Size(442, 30);
            this.checkExitWhenRocksmithTerminated.TabIndex = 43;
            this.toolTip.SetToolTip(this.checkExitWhenRocksmithTerminated, "Recommended setting: Off\r\n\r\nWith this option enabled, RSTK will terminate itself " +
        "when Rocksmith is terminated\r\n(will not take effect if the Rocksmith session is " +
        "shorter than 30 seconds).");
            // 
            // tbLaunchCommand
            // 
            this.tbLaunchCommand.Caption = "Pre-launch command lines";
            this.tbLaunchCommand.Location = new System.Drawing.Point(0, 610);
            this.tbLaunchCommand.Margin = new System.Windows.Forms.Padding(0);
            this.tbLaunchCommand.Name = "tbLaunchCommand";
            this.tbLaunchCommand.Size = new System.Drawing.Size(442, 102);
            this.tbLaunchCommand.TabIndex = 45;
            // 
            // 
            // 
            this.tbLaunchCommand.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLaunchCommand.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaunchCommand.TextBox.Location = new System.Drawing.Point(0, 0);
            this.tbLaunchCommand.TextBox.Margin = new System.Windows.Forms.Padding(0);
            this.tbLaunchCommand.TextBox.Multiline = true;
            this.tbLaunchCommand.TextBox.Name = "";
            this.tbLaunchCommand.TextBox.Size = new System.Drawing.Size(442, 72);
            this.tbLaunchCommand.TextBox.TabIndex = 0;
            this.tbLaunchCommand.TextBox.WordWrap = false;
            this.toolTip.SetToolTip(this.tbLaunchCommand, resources.GetString("tbLaunchCommand.ToolTip"));
            this.tbLaunchCommand.Vertical = true;
            // 
            // panStatus
            // 
            this.panStatus.Controls.Add(this.lblStatus);
            this.panStatus.Controls.Add(this.pbStatus);
            this.panStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.panStatus.Location = new System.Drawing.Point(10, 10);
            this.panStatus.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panStatus.Name = "panStatus";
            this.panStatus.Size = new System.Drawing.Size(92, 157);
            this.panStatus.TabIndex = 29;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 5000;
            this.toolTip.AutoPopDelay = 32767;
            this.toolTip.InitialDelay = 150;
            this.toolTip.ReshowDelay = 1000;
            this.toolTip.ShowAlways = true;
            // 
            // RSTKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1237, 924);
            this.Controls.Add(this.splitter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RSTKForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rocksmith Tookit";
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.panLaunchButtons.ResumeLayout(false);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.panRocksmithPath.ResumeLayout(false);
            this.panRocksmithPath.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackLatencyBuffer.TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxOutputBufferSize.TrackBar)).EndInit();
            this.panStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbStatus;
        private Marzersoft.Themes.ThemedLabel lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Marzersoft.Themes.ThemedTextBox tbRocksmithPath;
        private Marzersoft.Themes.ThemedButton btnRocksmithPath;
        private System.Windows.Forms.OpenFileDialog dlgBrowse;
        private Marzersoft.Themes.ThemedLabel themedLabel3;
        private Marzersoft.Themes.ThemedButton btnLaunch;
        private Marzersoft.Themes.ThemedButton btnLaunchSteam;
        private System.Windows.Forms.Panel panLaunchButtons;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Panel panStatus;
        private System.Windows.Forms.ToolTip toolTip;
        private Marzersoft.Controls.ComboBoxRow cbResolution;
        private Marzersoft.Themes.ThemedLabel themedLabel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private Marzersoft.Controls.ComboBoxRow cbFullscreenMode;
        private Marzersoft.Controls.CheckBoxRow checkEmulateFullscreen;
        private Marzersoft.Themes.ThemedLabel themedLabel1;
        private Marzersoft.Controls.CheckBoxRow checkExclusiveMode;
        private Marzersoft.Controls.CheckBoxRow checkUltraLowLatencyMode;
        private Marzersoft.Controls.TrackBarRow trackLatencyBuffer;
        private Marzersoft.Controls.TrackBarRow trackMaxOutputBufferSize;
        private Marzersoft.Controls.LabelRow lblEffectiveLatency;
        private Marzersoft.Controls.CheckBoxRow checkMicrophone;
        private Marzersoft.Themes.ThemedLabel themedLabel2;
        private System.Windows.Forms.Panel panRocksmithPath;
        private Marzersoft.Controls.CheckBoxRow checkDumpAudioLog;
        private Marzersoft.Controls.CheckBoxRow checkStartup;
        private Marzersoft.Controls.CheckBoxRow checkAutoHide;
        private Marzersoft.Controls.CheckBoxRow checkStartMinimized;
        private Marzersoft.Controls.CheckBoxRow checkCycleDisplays;
        private Marzersoft.Controls.CheckBoxRow checkExitWhenRocksmithTerminated;
        private Marzersoft.Controls.CheckBoxRow checkLaunchAnywhere;
        private Marzersoft.Controls.TextBoxRow tbLaunchCommand;
    }
}

