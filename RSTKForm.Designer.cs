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
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.lblStatus = new Marzersoft.Themes.ThemedLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panMaxOutputBufferSize = new System.Windows.Forms.Panel();
            this.labMaxOutputBufferSize = new System.Windows.Forms.Label();
            this.trackMaxOutputBufferSize = new System.Windows.Forms.TrackBar();
            this.panLatencyBuffer = new System.Windows.Forms.Panel();
            this.labLatencyBuffer = new System.Windows.Forms.Label();
            this.trackLatencyBuffer = new System.Windows.Forms.TrackBar();
            this.themedLabel1 = new Marzersoft.Themes.ThemedLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.themedLabel2 = new Marzersoft.Themes.ThemedLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFullscreenMode = new Marzersoft.Themes.ThemedComboBox();
            this.checkEmulatedFullscreen = new System.Windows.Forms.CheckBox();
            this.checkExclusiveMode = new System.Windows.Forms.CheckBox();
            this.checkUltraLowLatencyMode = new System.Windows.Forms.CheckBox();
            this.lblEffectiveLatency = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbResolution = new Marzersoft.Themes.ThemedComboBox();
            this.btnRocksmithPath = new Marzersoft.Themes.ThemedButton();
            this.themedLabel3 = new Marzersoft.Themes.ThemedLabel();
            this.tbRocksmithPath = new Marzersoft.Themes.ThemedTextBox();
            this.dlgBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnLaunch = new Marzersoft.Themes.ThemedButton();
            this.btnLaunchSteam = new Marzersoft.Themes.ThemedButton();
            this.panLaunchButtons = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.panStatus = new System.Windows.Forms.Panel();
            this.lblAbout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panMaxOutputBufferSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxOutputBufferSize)).BeginInit();
            this.panLatencyBuffer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLatencyBuffer)).BeginInit();
            this.panLaunchButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.panStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgStatus
            // 
            this.imgStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.imgStatus.Image = global::RSTK.Properties.Resources.rstk_waiting_64;
            this.imgStatus.Location = new System.Drawing.Point(0, 0);
            this.imgStatus.Margin = new System.Windows.Forms.Padding(0);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Size = new System.Drawing.Size(133, 100);
            this.imgStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgStatus.TabIndex = 0;
            this.imgStatus.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Accent = ((uint)(0u));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 100);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(133, 77);
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
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.Controls.Add(this.panMaxOutputBufferSize, 1, 9);
            this.tableLayoutPanel.Controls.Add(this.panLatencyBuffer, 1, 8);
            this.tableLayoutPanel.Controls.Add(this.themedLabel1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.themedLabel2, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.label6, 0, 9);
            this.tableLayoutPanel.Controls.Add(this.label4, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.label7, 0, 10);
            this.tableLayoutPanel.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.cbFullscreenMode, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.checkEmulatedFullscreen, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.checkExclusiveMode, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.checkUltraLowLatencyMode, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.lblEffectiveLatency, 1, 10);
            this.tableLayoutPanel.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.cbResolution, 1, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(10, 95);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 13;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(799, 390);
            this.tableLayoutPanel.TabIndex = 5;
            this.tableLayoutPanel.Visible = false;
            // 
            // panMaxOutputBufferSize
            // 
            this.panMaxOutputBufferSize.Controls.Add(this.labMaxOutputBufferSize);
            this.panMaxOutputBufferSize.Controls.Add(this.trackMaxOutputBufferSize);
            this.panMaxOutputBufferSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMaxOutputBufferSize.Location = new System.Drawing.Point(175, 258);
            this.panMaxOutputBufferSize.Margin = new System.Windows.Forms.Padding(0);
            this.panMaxOutputBufferSize.Name = "panMaxOutputBufferSize";
            this.panMaxOutputBufferSize.Size = new System.Drawing.Size(594, 30);
            this.panMaxOutputBufferSize.TabIndex = 29;
            // 
            // labMaxOutputBufferSize
            // 
            this.labMaxOutputBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labMaxOutputBufferSize.Location = new System.Drawing.Point(0, 0);
            this.labMaxOutputBufferSize.Margin = new System.Windows.Forms.Padding(0);
            this.labMaxOutputBufferSize.Name = "labMaxOutputBufferSize";
            this.labMaxOutputBufferSize.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.labMaxOutputBufferSize.Size = new System.Drawing.Size(48, 30);
            this.labMaxOutputBufferSize.TabIndex = 28;
            this.labMaxOutputBufferSize.Text = "0";
            this.labMaxOutputBufferSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackMaxOutputBufferSize
            // 
            this.trackMaxOutputBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackMaxOutputBufferSize.AutoSize = false;
            this.trackMaxOutputBufferSize.LargeChange = 32;
            this.trackMaxOutputBufferSize.Location = new System.Drawing.Point(48, 0);
            this.trackMaxOutputBufferSize.Margin = new System.Windows.Forms.Padding(0);
            this.trackMaxOutputBufferSize.Maximum = 2048;
            this.trackMaxOutputBufferSize.Name = "trackMaxOutputBufferSize";
            this.trackMaxOutputBufferSize.Size = new System.Drawing.Size(546, 30);
            this.trackMaxOutputBufferSize.SmallChange = 8;
            this.trackMaxOutputBufferSize.TabIndex = 43;
            this.trackMaxOutputBufferSize.TickFrequency = 32;
            // 
            // panLatencyBuffer
            // 
            this.panLatencyBuffer.Controls.Add(this.labLatencyBuffer);
            this.panLatencyBuffer.Controls.Add(this.trackLatencyBuffer);
            this.panLatencyBuffer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLatencyBuffer.Location = new System.Drawing.Point(175, 228);
            this.panLatencyBuffer.Margin = new System.Windows.Forms.Padding(0);
            this.panLatencyBuffer.Name = "panLatencyBuffer";
            this.panLatencyBuffer.Size = new System.Drawing.Size(594, 30);
            this.panLatencyBuffer.TabIndex = 26;
            // 
            // labLatencyBuffer
            // 
            this.labLatencyBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labLatencyBuffer.Location = new System.Drawing.Point(0, 0);
            this.labLatencyBuffer.Margin = new System.Windows.Forms.Padding(0);
            this.labLatencyBuffer.Name = "labLatencyBuffer";
            this.labLatencyBuffer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.labLatencyBuffer.Size = new System.Drawing.Size(48, 30);
            this.labLatencyBuffer.TabIndex = 27;
            this.labLatencyBuffer.Text = "4";
            this.labLatencyBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackLatencyBuffer
            // 
            this.trackLatencyBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackLatencyBuffer.AutoSize = false;
            this.trackLatencyBuffer.LargeChange = 1;
            this.trackLatencyBuffer.Location = new System.Drawing.Point(48, 0);
            this.trackLatencyBuffer.Margin = new System.Windows.Forms.Padding(0);
            this.trackLatencyBuffer.Maximum = 16;
            this.trackLatencyBuffer.Minimum = 1;
            this.trackLatencyBuffer.Name = "trackLatencyBuffer";
            this.trackLatencyBuffer.Size = new System.Drawing.Size(546, 30);
            this.trackLatencyBuffer.TabIndex = 42;
            this.trackLatencyBuffer.Value = 4;
            // 
            // themedLabel1
            // 
            this.themedLabel1.Accent = ((uint)(0u));
            this.themedLabel1.AccentMode = true;
            this.themedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themedLabel1.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.themedLabel1, 3);
            this.themedLabel1.FontSize = 1;
            this.themedLabel1.Location = new System.Drawing.Point(0, 0);
            this.themedLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.themedLabel1.Name = "themedLabel1";
            this.themedLabel1.Size = new System.Drawing.Size(799, 34);
            this.themedLabel1.TabIndex = 6;
            this.themedLabel1.Text = "Renderer/Window Settings";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label1.Size = new System.Drawing.Size(175, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resolution";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(0, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label2.Size = new System.Drawing.Size(175, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fullscreen mode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(0, 168);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label3.Size = new System.Drawing.Size(175, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Exclusive mode";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // themedLabel2
            // 
            this.themedLabel2.Accent = ((uint)(0u));
            this.themedLabel2.AccentMode = true;
            this.themedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themedLabel2.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.themedLabel2, 3);
            this.themedLabel2.FontSize = 1;
            this.themedLabel2.Location = new System.Drawing.Point(0, 134);
            this.themedLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.themedLabel2.Name = "themedLabel2";
            this.themedLabel2.Size = new System.Drawing.Size(799, 34);
            this.themedLabel2.TabIndex = 7;
            this.themedLabel2.Text = "Audio Settings";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(0, 258);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label6.Size = new System.Drawing.Size(175, 30);
            this.label6.TabIndex = 10;
            this.label6.Text = "Max output buffer size";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(0, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label4.Size = new System.Drawing.Size(175, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ultra-low latency mode";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(0, 288);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label7.Size = new System.Drawing.Size(175, 30);
            this.label7.TabIndex = 11;
            this.label7.Text = "Latency (estimated)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(0, 94);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label9.Size = new System.Drawing.Size(175, 30);
            this.label9.TabIndex = 14;
            this.label9.Text = "Emulate fullscreen";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFullscreenMode
            // 
            this.cbFullscreenMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFullscreenMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFullscreenMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFullscreenMode.FormattingEnabled = true;
            this.cbFullscreenMode.Location = new System.Drawing.Point(175, 64);
            this.cbFullscreenMode.Margin = new System.Windows.Forms.Padding(0);
            this.cbFullscreenMode.Name = "cbFullscreenMode";
            this.cbFullscreenMode.Size = new System.Drawing.Size(594, 21);
            this.cbFullscreenMode.TabIndex = 21;
            // 
            // checkEmulatedFullscreen
            // 
            this.checkEmulatedFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEmulatedFullscreen.AutoSize = true;
            this.checkEmulatedFullscreen.Location = new System.Drawing.Point(175, 94);
            this.checkEmulatedFullscreen.Margin = new System.Windows.Forms.Padding(0);
            this.checkEmulatedFullscreen.Name = "checkEmulatedFullscreen";
            this.checkEmulatedFullscreen.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.checkEmulatedFullscreen.Size = new System.Drawing.Size(594, 30);
            this.checkEmulatedFullscreen.TabIndex = 22;
            this.checkEmulatedFullscreen.Text = "(only applies in Windowed mode)";
            this.checkEmulatedFullscreen.UseVisualStyleBackColor = true;
            // 
            // checkExclusiveMode
            // 
            this.checkExclusiveMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkExclusiveMode.AutoSize = true;
            this.checkExclusiveMode.Checked = true;
            this.checkExclusiveMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkExclusiveMode.Location = new System.Drawing.Point(175, 168);
            this.checkExclusiveMode.Margin = new System.Windows.Forms.Padding(0);
            this.checkExclusiveMode.Name = "checkExclusiveMode";
            this.checkExclusiveMode.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.checkExclusiveMode.Size = new System.Drawing.Size(594, 30);
            this.checkExclusiveMode.TabIndex = 40;
            this.checkExclusiveMode.UseVisualStyleBackColor = true;
            // 
            // checkUltraLowLatencyMode
            // 
            this.checkUltraLowLatencyMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkUltraLowLatencyMode.AutoSize = true;
            this.checkUltraLowLatencyMode.Checked = true;
            this.checkUltraLowLatencyMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUltraLowLatencyMode.Location = new System.Drawing.Point(175, 198);
            this.checkUltraLowLatencyMode.Margin = new System.Windows.Forms.Padding(0);
            this.checkUltraLowLatencyMode.Name = "checkUltraLowLatencyMode";
            this.checkUltraLowLatencyMode.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.checkUltraLowLatencyMode.Size = new System.Drawing.Size(594, 30);
            this.checkUltraLowLatencyMode.TabIndex = 41;
            this.checkUltraLowLatencyMode.UseVisualStyleBackColor = true;
            // 
            // lblEffectiveLatency
            // 
            this.lblEffectiveLatency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEffectiveLatency.Location = new System.Drawing.Point(175, 288);
            this.lblEffectiveLatency.Margin = new System.Windows.Forms.Padding(0);
            this.lblEffectiveLatency.Name = "lblEffectiveLatency";
            this.lblEffectiveLatency.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblEffectiveLatency.Size = new System.Drawing.Size(594, 30);
            this.lblEffectiveLatency.TabIndex = 24;
            this.lblEffectiveLatency.Text = "label1";
            this.lblEffectiveLatency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(0, 228);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 6, 8);
            this.label5.Size = new System.Drawing.Size(175, 30);
            this.label5.TabIndex = 9;
            this.label5.Text = "Latency buffer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbResolution
            // 
            this.cbResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(175, 34);
            this.cbResolution.Margin = new System.Windows.Forms.Padding(0);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(594, 21);
            this.cbResolution.TabIndex = 20;
            // 
            // btnRocksmithPath
            // 
            this.btnRocksmithPath.Accent = ((uint)(0u));
            this.btnRocksmithPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRocksmithPath.FlatAppearance.BorderSize = 0;
            this.btnRocksmithPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRocksmithPath.Image = global::RSTK.Properties.Resources.open_16;
            this.btnRocksmithPath.Location = new System.Drawing.Point(753, 43);
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
            this.themedLabel3.Location = new System.Drawing.Point(10, 10);
            this.themedLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.themedLabel3.Name = "themedLabel3";
            this.themedLabel3.Size = new System.Drawing.Size(799, 30);
            this.themedLabel3.TabIndex = 25;
            this.themedLabel3.Text = "Rocksmith Location";
            // 
            // tbRocksmithPath
            // 
            this.tbRocksmithPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRocksmithPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRocksmithPath.Location = new System.Drawing.Point(13, 43);
            this.tbRocksmithPath.Margin = new System.Windows.Forms.Padding(0);
            this.tbRocksmithPath.Name = "tbRocksmithPath";
            this.tbRocksmithPath.ReadOnly = true;
            this.tbRocksmithPath.Size = new System.Drawing.Size(736, 20);
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
            this.btnLaunch.Size = new System.Drawing.Size(133, 65);
            this.btnLaunch.TabIndex = 100;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLaunch.UseVisualStyleBackColor = true;
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
            this.btnLaunchSteam.Size = new System.Drawing.Size(133, 65);
            this.btnLaunchSteam.TabIndex = 101;
            this.btnLaunchSteam.Text = "Launch via Steam";
            this.btnLaunchSteam.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLaunchSteam.UseVisualStyleBackColor = true;
            this.btnLaunchSteam.Click += new System.EventHandler(this.btnLaunchSteam_Click);
            // 
            // panLaunchButtons
            // 
            this.panLaunchButtons.Controls.Add(this.btnLaunch);
            this.panLaunchButtons.Controls.Add(this.btnLaunchSteam);
            this.panLaunchButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLaunchButtons.Location = new System.Drawing.Point(10, 187);
            this.panLaunchButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panLaunchButtons.Name = "panLaunchButtons";
            this.panLaunchButtons.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panLaunchButtons.Size = new System.Drawing.Size(133, 274);
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
            this.splitter.Panel1.Controls.Add(this.tableLayoutPanel);
            this.splitter.Panel1.Controls.Add(this.themedLabel3);
            this.splitter.Panel1.Controls.Add(this.btnRocksmithPath);
            this.splitter.Panel1.Controls.Add(this.tbRocksmithPath);
            this.splitter.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.panLaunchButtons);
            this.splitter.Panel2.Controls.Add(this.panStatus);
            this.splitter.Panel2.Controls.Add(this.lblAbout);
            this.splitter.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitter.Size = new System.Drawing.Size(973, 495);
            this.splitter.SplitterDistance = 819;
            this.splitter.SplitterWidth = 1;
            this.splitter.TabIndex = 29;
            this.splitter.TabStop = false;
            // 
            // panStatus
            // 
            this.panStatus.Controls.Add(this.lblStatus);
            this.panStatus.Controls.Add(this.imgStatus);
            this.panStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.panStatus.Location = new System.Drawing.Point(10, 10);
            this.panStatus.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panStatus.Name = "panStatus";
            this.panStatus.Size = new System.Drawing.Size(133, 177);
            this.panStatus.TabIndex = 29;
            // 
            // lblAbout
            // 
            this.lblAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAbout.Location = new System.Drawing.Point(10, 461);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(133, 24);
            this.lblAbout.TabIndex = 102;
            this.lblAbout.Text = "label8";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RSTKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(973, 495);
            this.Controls.Add(this.splitter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(750, 475);
            this.Name = "RSTKForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSTK";
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panMaxOutputBufferSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxOutputBufferSize)).EndInit();
            this.panLatencyBuffer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackLatencyBuffer)).EndInit();
            this.panLaunchButtons.ResumeLayout(false);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel1.PerformLayout();
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.panStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgStatus;
        private Marzersoft.Themes.ThemedLabel lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private Marzersoft.Themes.ThemedLabel themedLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Marzersoft.Themes.ThemedLabel themedLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private Marzersoft.Themes.ThemedTextBox tbRocksmithPath;
        private Marzersoft.Themes.ThemedButton btnRocksmithPath;
        private Marzersoft.Themes.ThemedComboBox cbFullscreenMode;
        private System.Windows.Forms.CheckBox checkExclusiveMode;
        private System.Windows.Forms.CheckBox checkUltraLowLatencyMode;
        private System.Windows.Forms.Label lblEffectiveLatency;
        private System.Windows.Forms.OpenFileDialog dlgBrowse;
        private Marzersoft.Themes.ThemedLabel themedLabel3;
        private System.Windows.Forms.TrackBar trackLatencyBuffer;
        private System.Windows.Forms.Panel panLatencyBuffer;
        private System.Windows.Forms.Label labLatencyBuffer;
        private System.Windows.Forms.TrackBar trackMaxOutputBufferSize;
        private System.Windows.Forms.Label labMaxOutputBufferSize;
        private System.Windows.Forms.Panel panMaxOutputBufferSize;
        private Marzersoft.Themes.ThemedComboBox cbResolution;
        private Marzersoft.Themes.ThemedButton btnLaunch;
        private Marzersoft.Themes.ThemedButton btnLaunchSteam;
        private System.Windows.Forms.Panel panLaunchButtons;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkEmulatedFullscreen;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Panel panStatus;
        private System.Windows.Forms.Label lblAbout;
    }
}

