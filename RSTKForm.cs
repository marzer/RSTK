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
        private readonly List<Control> disabledWhileRunning
            = new List<Control>();

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
                App.Config.User.Set("emulated_fullscreen", checkEmulatedFullscreen.Checked);
                App.Config.User.Flush();
            };

            //exclusive mode
            checkExclusiveMode.CheckedChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(checkExclusiveMode);

            //win32ultralowlatency
            checkUltraLowLatencyMode.CheckedChanged += (s, e) => { SaveConfigChanges(); };
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

            //set icon
            Icon = App.Icons.Resource("rstk_waiting_icon", App.Assembly);

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

            //check command line params
            string path = "";
            if (App.Arguments.Value("path", ref path)
                && File.Exists(path = path.Trim())
                && SetRocksmithPath(Path.GetFullPath(path), false))
                return;

            //check system-wide config (reasonable to assume there's only one rocksmith variant installed on the machine)
            if ((path = App.Config.Shared.Get("path", "").Trim()).Length > 0
                && File.Exists(path = path.Trim()))
                SetRocksmithPath(Path.GetFullPath(path), false);
        }

        private void RefreshEffectiveLatency()
        {
            lblEffectiveLatency.Text = string.Format("{0} ms",
                Rocksmith.CalulateEffectiveLatency((uint)trackMaxOutputBufferSize.Value, (uint)trackLatencyBuffer.Value));
        }

        private void LatencyTrackBarValueChanged(object sender, EventArgs args)
        {
            ((sender as TrackBar).Tag as Label).Text = (sender as TrackBar).Value.ToString();
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
                    lblStatus.Text = string.Format("{0} Running.", Path.GetFileNameWithoutExtension(rs.GamePath));
                    imgStatus.Image = App.Images.Resource("rstk_64", App.Assembly);
                }, false);
            };

            //rocksmith instance terminated
            rocksmith.Terminated += (rs) =>
            {
                this.Execute(() =>
                {
                    foreach (var c in disabledWhileRunning)
                        c.Enabled = true;
                    lblStatus.Text = string.Format("Waiting for {0}", Path.GetFileNameWithoutExtension(rs.GamePath));
                    imgStatus.Image = App.Images.Resource("rstk_waiting_64", App.Assembly);
                }, false);
                running = false;
            };

            //update ui
            tbRocksmithPath.Text = fullPath;
            tableLayoutPanel.Visible = panLaunchButtons.Visible = true;
            lblStatus.Text = string.Format("Waiting for {0}", Path.GetFileNameWithoutExtension(fullPath));

            //write to config
            if (writeToConfig)
            {
                App.Config.Shared.Set("path", fullPath);
                App.Config.Shared.Flush();
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
            if (rocksmith == null)
                return;
            Process.Start(rocksmith.SteamRunCommand);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (rocksmith == null)
                return;
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = rocksmith.GamePath;
            pi.WorkingDirectory = rocksmith.GameDirectory;
            pi.UseShellExecute = true;
            Process.Start(pi);
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

        /*
        //set up data grid view
        dgvConfig.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvConfig.Columns[0].DefaultCellStyle.Padding = new Padding(5, 0, 10, 0);
        ((DataGridViewImageColumn)dgvConfig.Columns[2]).DefaultCellStyle.NullValue = null;
        dgvConfig.AddRow("Resolution", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("Fullscreen", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("ExclusiveMode", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("Win32UltraLowLatencyMode", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("MaxOutputBufferSize", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("LatencyBuffer", "", App.Images.Resource("warning"));
        dgvConfig.AddRow("Latency (approx)", "", App.Images.Resource("warning"));
        dgvConfig.CellDoubleClick += (sender, ev) =>
        {
            if (rocksmith == null || ev.RowIndex < 0 || ev.ColumnIndex < 0)
                return;

            if (ev.RowIndex >= 1 && ev.RowIndex < 4)
            {
                lock (rocksmithLock)
                {
                    if (rocksmith == null)
                        return;
                    switch (ev.RowIndex)
                    {
                        case 1: //fullscreen
                            rocksmith.NewFullscreen = (Rocksmith.FullscreenModes)((((int)rocksmith.NewFullscreen.GetValueOrDefault(rocksmith.Fullscreen)) + 1) % 3);
                            break;

                        case 2: //exclusive
                            rocksmith.NewExclusiveMode = !rocksmith.NewExclusiveMode.GetValueOrDefault(rocksmith.ExclusiveMode);
                            break;

                        case 3: //win32ultralowlatency
                            rocksmith.NewWin32UltraLowLatencyMode = !rocksmith.NewWin32UltraLowLatencyMode.GetValueOrDefault(rocksmith.Win32UltraLowLatencyMode);
                            break;
                    }
                }
            }
            else if (ev.RowIndex >= 4) //latency vars
            {
                int maxOutputBufferSize;
                int latencyBuffer;
                lock (rocksmithLock)
                {
                    if (rocksmith == null)
                        return;
                    maxOutputBufferSize = rocksmith.NewMaxOutputBufferSize.GetValueOrDefault(rocksmith.MaxOutputBufferSize);
                    latencyBuffer = rocksmith.NewLatencyBuffer.GetValueOrDefault(rocksmith.LatencyBuffer);
                }

                using (EffectiveLatencyForm dlg = new EffectiveLatencyForm(maxOutputBufferSize, latencyBuffer))
                {
                    if (dlg.ShowDialog() == DialogResult.OK && rocksmith != null)
                    {
                        lock (rocksmithLock)
                        {
                            if (rocksmith != null)
                            {
                                rocksmith.NewMaxOutputBufferSize = dlg.MaxOutputBufferSize;
                                rocksmith.NewLatencyBuffer = dlg.LatencyBuffer;
                            }
                        }
                    }
                }
            }
        };

        if (App.Arguments.Key("fakefullscreen"))
            checkEmulatedFullscreen.Checked = true;
        checkEmulatedFullscreen.CheckedChanged += (s, ea) =>
        {
            lock (rocksmithLock)
            {
                if (rocksmith != null)
                {
                    rocksmith.FakeFullscreen = checkEmulatedFullscreen.Checked;
                }
            }
        };
        RocksmithStateChanged(null);

        (thread = new Thread(() =>
        {
            while (!quitting && !HasClosed)
            {
                //sleep
                ThreadExtensions.Sleep(1000, () => { return quitting || HasClosed; });
                if (quitting || HasClosed)
                    break;

                lock (rocksmithLock)
                {
                    if (quitting || HasClosed)
                        break;

                    //check current rocksmith process
                    if (rocksmith != null && rocksmith.HasExited)
                    {
                        rocksmith.Dispose();
                        RocksmithStateChanged(rocksmith = null);
                    }

                    //look for new rocksmith processes
                    if (rocksmith == null)
                    {
                        var processes = System.Diagnostics.Process.GetProcesses();
                        var proc = processes
                            .Where((p) =>
                            {
                                return p.ProcessName.ToLower().CompareTo("rocksmith2014") == 0
                                    && p.Id != 0
                                    && !p.HasExited
                                    && p.MainWindowHandle != IntPtr.Zero
                                    //&& (rocksmithPath == "" || rocksmithPath.CompareTo(p.MainModule.FileName) == 0)
                                    ;
                            })
                            .FirstOrDefault();
                        if (proc != null)
                        {
                            try
                            {
                                rocksmith = new Rocksmith(proc);
                                rocksmith.FakeFullscreen = checkEmulatedFullscreen.Checked;
                                //  rocksmithPath = proc.MainModule.FileName;
                                rocksmith.ConfigChanged += RocksmithStateChanged;
                                rocksmith.Exited += RocksmithStateChanged;
                                rocksmith.WindowBoundsChanged += RocksmithStateChanged;
                                RocksmithStateChanged(rocksmith);
                            }
                            catch (RocksmithInitializationException) { } //eat these
                        }

                        foreach (System.Diagnostics.Process p in processes)
                        {
                            if (proc != null && p == proc)
                                continue;
                            p.Dispose();
                        }
                    }
                }
            }

            //dispose of wrapper
            if (rocksmith != null)
            {
                lock (rocksmithLock)
                {
                    if (rocksmith != null)
                    {
                        rocksmith.Dispose();
                        rocksmith = null;
                    }
                }
            }

        })).Start();

        if (App.Arguments.Key("starthidden"))
            Hide();
            */

        /////////////////////////////////////////////////////////////////////
        // UPDATING UI
        /////////////////////////////////////////////////////////////////////



        /*
    private enum StateIcons
    {
        Error,
        Pending,
        Ready
    };
    private StateIcons Icons
    {
        set
        {
            if (value == StateIcons.Error)
            {
                TrayIcon.Icon = App.Icons.Resource("rocksmith_error", App.Assembly);
                image.Image = App.Images.Resource("rocksmith_error_large", App.Assembly);
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (value == StateIcons.Pending)
            {
                TrayIcon.Icon = App.Icons.Resource("rocksmith_pending", App.Assembly);
                image.Image = App.Images.Resource("rocksmith_pending_large", App.Assembly);
                lblStatus.ForeColor = System.Drawing.Color.Orange;
            }
            else if (value == StateIcons.Ready)
            {
                TrayIcon.Icon = App.Icons.Resource("rocksmith_ready", App.Assembly);
                image.Image = App.Images.Resource("rocksmith_ready_large", App.Assembly);
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }
    }

    private void RocksmithStateChanged(Rocksmith rs)
    {
        this.Execute(() =>
        {
            if (rs == null || rs.HasExited || rs.IsDisposed)
            {
                Icons = StateIcons.Pending;
                lblStatus.Text = "Waiting for game";
                lblStatus.ForeColor = System.Drawing.Color.Orange;
                dgvConfig.Columns[1].Visible = dgvConfig.Columns[2].Visible = false;
                for (int i = 0; i < 7; ++i)
                    Update(i, "");
            }
            else
            {
                //Resolution
                var res = rs.Resolution;
                var sz = rocksmith.WindowSize;
                Update(0, string.Format("{0} x {1} (window: {2} x {3})", res.Width, res.Height, sz.Width, sz.Height));
                //Fullscreen
                var fs = rs.NewFullscreen.GetValueOrDefault(rs.Fullscreen);
                Update(1, string.Format("{0}", fs), false, rs.NewFullscreen.HasValue);
                //ExclusiveMode
                var ex = rs.NewExclusiveMode.GetValueOrDefault(rs.ExclusiveMode);
                Update(2, string.Format("{0}", ex), !ex, rs.NewExclusiveMode.HasValue);
                //Win32UltraLowLatencyMode
                var ull = rs.NewWin32UltraLowLatencyMode.GetValueOrDefault(rs.Win32UltraLowLatencyMode);
                Update(3, string.Format("{0}", ull), !ull, rs.NewWin32UltraLowLatencyMode.HasValue);
                //MaxOutputBufferSize
                int maxOutputBufferSize = rs.NewMaxOutputBufferSize.GetValueOrDefault(rs.MaxOutputBufferSize);
                Update(4, string.Format("{0} samples", maxOutputBufferSize), maxOutputBufferSize >= 256, rs.NewMaxOutputBufferSize.HasValue);
                //LatencyBuffer
                int latencyBuffer = rs.NewLatencyBuffer.GetValueOrDefault(rs.LatencyBuffer);
                Update(5, string.Format("{0}", latencyBuffer), rs.LatencyBuffer == 0 || rs.LatencyBuffer >= 4, rs.NewLatencyBuffer.HasValue);
                //Effective Latency
                int effectiveLatency = rs.NewEffectiveLatency.GetValueOrDefault(rs.EffectiveLatency);
                Update(6, string.Format("{0} ms", effectiveLatency), effectiveLatency >= 50, rs.NewEffectiveLatency.HasValue);

                dgvConfig.Columns[1].Visible = dgvConfig.Columns[2].Visible = true;
                Icons = StateIcons.Ready;
                lblStatus.Text = "Monitoring";
                lblStatus.ForeColor = System.Drawing.Color.LightGreen;
            }
        });
    }
    */
    }
}
