﻿using Marzersoft.Forms;
using System;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using Marzersoft;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Marzersoft.Themes;
using System.Reflection;
using System.Diagnostics;

namespace RSTK
{
    public partial class RSTKForm : MainForm
    {
        private Rocksmith rocksmith = null;
        private volatile bool running = false, readingConfig = false;
        private readonly Marzersoft.Timer lastLaunchClickTimer
            = new Marzersoft.Timer();
        private readonly List<Control> disabledWhileRunning
            = new List<Control>();
        private readonly List<ToolStripItem> disabledWhileRunningToolStrip
            = new List<ToolStripItem>();
        private ToolStripItem tsLaunch, tsLaunchSteam;

        public RSTKForm()
        {
            InitializeComponent();
            if (IsDesignMode)
                return;

            //browse button
            btnRocksmithPath.Click += (s, e) =>
            {
                if (dlgBrowse.ShowDialog(this) == DialogResult.OK)
                {
                    var path = Path.GetFullPath(dlgBrowse.FileName).Trim();
                    if (!File.Exists(path))
                    {
                        Logger.ErrorMessage(this, "{0} does not exist.", path);
                        return;
                    }
                    SetRocksmithPath(path);
                }
            };

            //resolution
            foreach (var res in Rocksmith.SupportedResolutions)
            {
                cbResolution.ComboBox.Items.Add(string.Format("{0} x {1}", res.Item1, res.Item2));
                if (res.Item1 == 1280 && res.Item2 == 720)
                    cbResolution.ComboBox.SelectedIndex = cbResolution.ComboBox.Items.Count - 1;
            }
            cbResolution.ComboBox.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbResolution);

            //fullscreen mode
            cbFullscreenMode.ComboBox.Items.AddRange(Enum.GetNames(typeof(Rocksmith.FullscreenModes)));
            cbFullscreenMode.ComboBox.SelectedIndex = 2;
            cbFullscreenMode.ComboBox.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbFullscreenMode);

            //emulated fullscreen in windowed mode
            checkEmulateFullscreen.CheckBox.Checked = App.Config.User.Get("emulated_fullscreen", false);
            checkEmulateFullscreen.CheckBox.CheckedChanged += (s, e) =>
            {
                if (rocksmith != null)
                    rocksmith.EmulateFullscreenWhenWindowed = checkEmulateFullscreen.CheckBox.Checked;
                checkEmulateFullscreen.ShowWarningIcon = checkEmulateFullscreen.CheckBox.Checked;
                App.Config.User.Set("emulated_fullscreen", checkEmulateFullscreen.CheckBox.Checked);
                App.Config.User.Flush();
            };

            //exclusive mode
            checkExclusiveMode.CheckBox.CheckedChanged += (s, e) =>
            {
                checkExclusiveMode.ShowWarningIcon = !checkExclusiveMode.CheckBox.Checked;
                RefreshEffectiveLatency();
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkExclusiveMode);

            //win32ultralowlatency
            checkUltraLowLatencyMode.CheckBox.CheckedChanged += (s, e) =>
            {
                checkUltraLowLatencyMode.ShowWarningIcon = !checkUltraLowLatencyMode.CheckBox.Checked;
                RefreshEffectiveLatency();
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkUltraLowLatencyMode);

            //latency buffer
            trackLatencyBuffer.TrackBar.ValueChanged += LatencyTrackBarValueChanged;
            disabledWhileRunning.Add(trackLatencyBuffer);

            //max output buffer size          
            trackMaxOutputBufferSize.TrackBar.ValueChanged += LatencyTrackBarValueChanged;
            disabledWhileRunning.Add(trackMaxOutputBufferSize);

            //effective latency label
            RefreshEffectiveLatency();

            //microphone mode
            checkMicrophone.CheckBox.CheckedChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(checkMicrophone);

            //dump audio log
            checkDumpAudioLog.CheckBox.CheckedChanged += (s, e) =>
            {
                checkDumpAudioLog.ShowWarningIcon = checkDumpAudioLog.CheckBox.Checked;
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkDumpAudioLog);

            //launch buttons
            disabledWhileRunning.Add(panLaunchButtons);

            //apply themes
            App.ThemeChanged += (t) =>
            {
                this.Execute(() =>
                {
                    splitter.Panel2.BackColor = t.Controls.HighContrast.Colour;
                }, false);
            };
            App.Theme = App.Themes["dark"];

            //position and size
            Size = new Size(765, 480);
            Resizable = false;
            flowLayoutPanel.Height += flowLayoutPanel.Top - 10;
            flowLayoutPanel.Top = 10;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (rocksmith != null)
                rocksmith.Dispose();
            base.OnFormClosed(e);
        }

        protected override void OnFirstShown(EventArgs e)
        {
            base.OnFirstShown(e);
            if (IsDesignMode)
                return;

            //splitter
            splitter.SplitterDistance = splitter.Width - 170;

            //set icon
            TrayIcon.Icon = Icon = App.Icons.Resource("rstk_waiting_icon", App.Assembly);

            //tray menu items
            tsLaunch = AddTrayIconMenuItem("Launch game");
            tsLaunch.Visible = false;
            tsLaunch.Image = App.Images.Resource("gamepad_24", "invert", App.Assembly);
            tsLaunch.Click += btnLaunch_Click;
            disabledWhileRunningToolStrip.Add(tsLaunch);
            tsLaunchSteam = AddTrayIconMenuItem("Launch game via Steam");
            tsLaunchSteam.Visible = false;
            tsLaunchSteam.Image = App.Images.Resource("steam_24", "invert", App.Assembly);
            tsLaunchSteam.Click += btnLaunchSteam_Click;
            disabledWhileRunningToolStrip.Add(tsLaunchSteam);

            //show message if not in admin mode
            /*
            if (!App.IsAdministrator)
            {
                Logger.WarningMessage(this, "Oh my, it appears you're running RSTK as a regular user." +
                    "\r\n\r\nRSTK requires elevated permissions to monitor the state of the Rocksmith process " +
                    "(and potentially more, depending on your system setup)." +
                    "\r\n\r\nFor best results (and less unpredictable weirdness), consider relaunching RSTK " +
                    "with elevated permissions (\"Run as Administrator\").");
            }
            */

            //check command line params
            string path = "";
            if (App.Arguments.Value("path", ref path)
                && File.Exists(path = path.Trim())
                && SetRocksmithPath(Path.GetFullPath(path), false))
                return;

            //check user's config
            if ((path = App.Config.User.Get("path", "").Trim()).Length > 0
                && File.Exists(path = path.Trim()))
                SetRocksmithPath(Path.GetFullPath(path), false);

            //bring to front of z order
            BringToFront();
        }

        private void RefreshEffectiveLatency()
        {
            var lat = Rocksmith.CalulateEffectiveLatency((uint)trackMaxOutputBufferSize.TrackBar.Value,
                (uint)trackLatencyBuffer.TrackBar.Value);
            if (!checkExclusiveMode.CheckBox.Checked)
                lat *= 1.5;
            if (!checkUltraLowLatencyMode.CheckBox.Checked)
                lat *= 1.5;
            lblEffectiveLatency.Value = string.Format("{0:0} ms", lat);
            lblEffectiveLatency.ShowWarningIcon = lat < 10.0 || lat > 75.0;
        }

        private void LatencyTrackBarValueChanged(object sender, EventArgs args)
        {
            trackLatencyBuffer.ShowWarningIcon = !trackLatencyBuffer.TrackBar.Value.IsBetween(1, 4);
            trackMaxOutputBufferSize.ShowWarningIcon = !trackMaxOutputBufferSize.TrackBar.Value.IsBetween(100, 400);
            RefreshEffectiveLatency();
            SaveConfigChanges();
        }

        private bool SetRocksmithPath(string fullPath, bool writeToConfig = true)
        {
            //do nothing if it's the same exe as current instance
            if (rocksmith != null && rocksmith.GamePath.ToLower().Equals(fullPath.ToLower()))
                return true;

            //create new rocksmith instance
            Rocksmith newRocksmith = null;
            try
            {
                newRocksmith = new Rocksmith(fullPath);
            }
            catch (Exception e)
            {
#if DEBUG
                if (Debugger.IsAttached)
                    throw;
#endif
                Logger.ErrorMessage(this, "Error instantiating Rocksmith manager:\r\n\r\nPath: {0}\r\nMessage: {1}",
                    fullPath, e.Message);
                return false;
            }

            //clean up old instance
            if (rocksmith != null)
                rocksmith.Dispose();

            //assign new instance
            rocksmith = newRocksmith;
            rocksmith.EmulateFullscreenWhenWindowed = checkEmulateFullscreen.CheckBox.Checked;

            //sync controls with config
            RocksmithConfigRead(rocksmith);
            rocksmith.ConfigRead += RocksmithConfigRead;

            //rocksmith instance detected
            rocksmith.Running += (rs) =>
            {
                running = true;
                this.Execute(() =>
                {
                    foreach (var c in disabledWhileRunning)
                        c.Enabled = false;
                    foreach (var c in disabledWhileRunningToolStrip)
                        c.Enabled = false;
                    lblStatus.Text = string.Format("{0} Running.", Path.GetFileNameWithoutExtension(rs.GamePath));
                    pbStatus.Image = App.Images.Resource("rstk_64", App.Assembly);
                    TrayIcon.Icon = Icon = App.Icons.Resource("rstk_icon", App.Assembly);
                }, false);
            };

            //rocksmith instance terminated
            rocksmith.Terminated += (rs) =>
            {
                this.Execute(() =>
                {
                    foreach (var c in disabledWhileRunning)
                        c.Enabled = true;
                    foreach (var c in disabledWhileRunningToolStrip)
                        c.Enabled = true;
                    lblStatus.Text = string.Format("Waiting for {0}", Path.GetFileNameWithoutExtension(rs.GamePath));
                    pbStatus.Image = App.Images.Resource("rstk_waiting_64", App.Assembly);
                    TrayIcon.Icon = Icon = App.Icons.Resource("rstk_waiting_icon", App.Assembly);
                }, false);
                running = false;
            };

            //update ui
            tbRocksmithPath.Text = fullPath;
            flowLayoutPanel.Visible
                = panLaunchButtons.Visible
                = tsLaunch.Visible
                = tsLaunchSteam.Visible
                = true;
            panRocksmithPath.Visible = false;
            lblStatus.Text = string.Format("Waiting for {0}", Path.GetFileNameWithoutExtension(fullPath));

            //write to config
            if (writeToConfig)
            {
                App.Config.User.Set("path", fullPath);
                App.Config.User.Flush();
            }
            return true;
        }

        private void RocksmithConfigRead(Rocksmith rs)
        {
            this.Execute(() =>
            {
                readingConfig = true;

                checkExclusiveMode.CheckBox.Checked = rs.ExclusiveMode;
                checkUltraLowLatencyMode.CheckBox.Checked = rs.Win32UltraLowLatencyMode;
                for (int i = 0; i < Rocksmith.SupportedResolutions.Count; ++i)
                {
                    if (Rocksmith.SupportedResolutions[i].Item1 == rs.Resolution.Width
                        && Rocksmith.SupportedResolutions[i].Item2 == rs.Resolution.Height)
                    {
                        cbResolution.ComboBox.SelectedIndex = i;
                        break;
                    }
                }
                cbFullscreenMode.ComboBox.SelectedIndex = (int)rs.FullscreenMode;
                trackLatencyBuffer.TrackBar.Value = (int)rs.LatencyBuffer;
                trackMaxOutputBufferSize.TrackBar.Value = (int)rs.MaxOutputBufferSize;
                checkMicrophone.CheckBox.Checked = rs.EnableMicrophone;
                checkDumpAudioLog.CheckBox.Checked = rs.DumpAudioLog;

                readingConfig = false;
            }, false);
        }

        private void btnLaunchSteam_Click(object sender, EventArgs e)
        {
            if (rocksmith == null || running)
                return;

            if (lastLaunchClickTimer.Seconds < 5.0)
                return;
            lastLaunchClickTimer.Reset();
            rocksmith.SetFastPollWindow(10);

            Process.Start(rocksmith.SteamRunCommand);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (rocksmith == null || running)
                return;

            if (lastLaunchClickTimer.Seconds < 5.0)
                return;
            lastLaunchClickTimer.Reset();
            rocksmith.SetFastPollWindow(10);

            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = rocksmith.GamePath;
            pi.WorkingDirectory = rocksmith.GameDirectory;
            pi.UseShellExecute = true;
            Process.Start(pi);
        }

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel.SuspendLayout();

            var w = flowLayoutPanel.Width;
            foreach (var c in flowLayoutPanel.Controls)
                (c as Control).Width = w;
            if (flowLayoutPanel.VerticalScrollbarVisible())
            {
                foreach (var c in flowLayoutPanel.Controls)
                    (c as Control).Width -= SystemInformation.VerticalScrollBarWidth + 8;
            }

            flowLayoutPanel.ResumeLayout();
        }

        private void btnLaunch_EnabledChanged(object sender, EventArgs e)
        {
            btnLaunch.Image = App.Images.Resource("gamepad_24",
                (sender as Button).Enabled ? "" : "alpha", App.Assembly);
        }

        private void btnLaunchSteam_EnabledChanged(object sender, EventArgs e)
        {
            btnLaunchSteam.Image = App.Images.Resource("steam_24",
                (sender as Button).Enabled ? "" : "alpha", App.Assembly);
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            "https://github.com/marzer/RSTK/".LaunchWebsite();
        }

        private void SaveConfigChanges()
        {
            if (rocksmith == null || running || readingConfig)
                return;

            rocksmith.ExclusiveMode = checkExclusiveMode.CheckBox.Checked;
            rocksmith.Win32UltraLowLatencyMode = checkUltraLowLatencyMode.CheckBox.Checked;
            rocksmith.Resolution = new Size(
                (int)Rocksmith.SupportedResolutions[cbResolution.ComboBox.SelectedIndex].Item1,
                (int)Rocksmith.SupportedResolutions[cbResolution.ComboBox.SelectedIndex].Item2);
            rocksmith.FullscreenMode = (Rocksmith.FullscreenModes)cbFullscreenMode.ComboBox.SelectedIndex;
            rocksmith.LatencyBuffer = (uint)trackLatencyBuffer.TrackBar.Value;
            rocksmith.MaxOutputBufferSize = (uint)trackMaxOutputBufferSize.TrackBar.Value;
            rocksmith.EnableMicrophone = checkMicrophone.CheckBox.Checked;
            rocksmith.DumpAudioLog = checkDumpAudioLog.CheckBox.Checked;

            rocksmith.WriteConfig();
        }
    }
}
