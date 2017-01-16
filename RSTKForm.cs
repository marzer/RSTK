using Marzersoft.Forms;
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
                cbResolution.Items.Add(string.Format("{0} x {1}", res.Item1, res.Item2));
                if (res.Item1 == 1280 && res.Item2 == 720)
                    cbResolution.SelectedIndex = cbResolution.Items.Count - 1;
            }
            cbResolution.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbResolution);

            //fullscreen mode
            cbFullscreenMode.Items.AddRange(Enum.GetNames(typeof(Rocksmith.FullscreenModes)));
            cbFullscreenMode.SelectedIndex = 2;
            cbFullscreenMode.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbFullscreenMode);

            //emulated fullscreen in windowed mode
            checkEmulatedFullscreen.Checked = App.Config.User.Get("emulated_fullscreen", false);
            checkEmulatedFullscreen.CheckedChanged += (s, e) =>
            {
                if (rocksmith != null)
                    rocksmith.EmulateFullscreenWhenWindowed = checkEmulatedFullscreen.Checked;
                pbEmulatedFullscreen.Visible = checkEmulatedFullscreen.Checked;
                App.Config.User.Set("emulated_fullscreen", checkEmulatedFullscreen.Checked);
                App.Config.User.Flush();
            };

            //exclusive mode
            checkExclusiveMode.CheckedChanged += (s, e) =>
            {
                pbExclusiveMode.Visible = !checkExclusiveMode.Checked;
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkExclusiveMode);

            //win32ultralowlatency
            checkUltraLowLatencyMode.CheckedChanged += (s, e) =>
            {
                pbUltraLowLatency.Visible = !checkUltraLowLatencyMode.Checked;
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkUltraLowLatencyMode);

            //latency buffer
            trackLatencyBuffer.Tag = labLatencyBuffer;
            trackLatencyBuffer.ValueChanged += LatencyTrackBarValueChanged;
            disabledWhileRunning.Add(trackLatencyBuffer);

            //max output buffer size          
            trackMaxOutputBufferSize.Tag = labMaxOutputBufferSize;
            trackMaxOutputBufferSize.ValueChanged += LatencyTrackBarValueChanged;
            RefreshEffectiveLatency();
            disabledWhileRunning.Add(trackMaxOutputBufferSize);

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
            if ((App.Roles & App.AccountRole.Administrator) != App.AccountRole.Administrator)
            {
                Logger.WarningMessage(this, "Oh my, it appears you're running RSTK as a regular user." +
                    "\r\n\r\nRSTK requires elevated permissions to monitor the state of the Rocksmith process " +
                    "(and potentially more, depending on your system setup)." +
                    "\r\n\r\nFor best results (and less unpredictable weirdness), consider relaunching RSTK " +
                    "with elevated permissions (\"Run as Administrator\").");
            }

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
        }

        private void RefreshEffectiveLatency()
        {
            var lat = (uint)Rocksmith.CalulateEffectiveLatency((uint)trackMaxOutputBufferSize.Value,
                (uint)trackLatencyBuffer.Value);
            lblEffectiveLatency.Text = string.Format("{0} ms", lat);
            pbEffectiveLatency.Visible = lat < 10 || lat > 75;
        }

        private void LatencyTrackBarValueChanged(object sender, EventArgs args)
        {
            ((sender as TrackBar).Tag as Label).Text = (sender as TrackBar).Value.ToString();
            pbLatencyBuffer.Visible = !trackLatencyBuffer.Value.IsBetween(1, 4);
            pbMaxOutputBufferSize.Visible = !trackMaxOutputBufferSize.Value.IsBetween(100, 400);
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
                Logger.ErrorMessage(this, "Error instantiating Rocksmith manager:\r\n\r\nPath: {0}\r\nMessage: {1}",
                    fullPath, e.Message);
                return false;
            }

            //clean up old instance
            if (rocksmith != null)
                rocksmith.Dispose();

            //assign new instance
            rocksmith = newRocksmith;
            rocksmith.EmulateFullscreenWhenWindowed = checkEmulatedFullscreen.Checked;

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
            tableLayoutPanel.Visible
                = panLaunchButtons.Visible
                = tsLaunch.Visible
                = tsLaunchSteam.Visible
                = true;
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

                checkExclusiveMode.Checked = rs.ExclusiveMode;
                checkUltraLowLatencyMode.Checked = rs.Win32UltraLowLatencyMode;
                for (int i = 0; i < Rocksmith.SupportedResolutions.Count; ++i)
                {
                    if (Rocksmith.SupportedResolutions[i].Item1 == rs.Resolution.Width
                        && Rocksmith.SupportedResolutions[i].Item2 == rs.Resolution.Height)
                    {
                        cbResolution.SelectedIndex = i;
                        break;
                    }
                }
                cbFullscreenMode.SelectedIndex = (int)rs.FullscreenMode;
                trackLatencyBuffer.Value = (int)rs.LatencyBuffer;
                trackMaxOutputBufferSize.Value = (int)rs.MaxOutputBufferSize;

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

        private void lblAbout_Click(object sender, EventArgs e)
        {
            "https://github.com/marzer/RSTK/".LaunchWebsite();
        }

        private void SaveConfigChanges()
        {
            if (rocksmith == null || running || readingConfig)
                return;

            rocksmith.ExclusiveMode = checkExclusiveMode.Checked;
            rocksmith.Win32UltraLowLatencyMode = checkUltraLowLatencyMode.Checked;
            rocksmith.Resolution = new Size(
                (int)Rocksmith.SupportedResolutions[cbResolution.SelectedIndex].Item1,
                (int)Rocksmith.SupportedResolutions[cbResolution.SelectedIndex].Item2);
            rocksmith.FullscreenMode = (Rocksmith.FullscreenModes)cbFullscreenMode.SelectedIndex;
            rocksmith.LatencyBuffer = (uint)trackLatencyBuffer.Value;
            rocksmith.MaxOutputBufferSize = (uint)trackMaxOutputBufferSize.Value;

            rocksmith.WriteConfig();
        }
    }
}
