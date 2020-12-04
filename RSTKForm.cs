using Marzersoft.Forms;
using System;
using System.Threading;
using System.Windows.Forms;
using Marzersoft;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using Marzersoft.Controls;
using System.Text.RegularExpressions;
using System.Linq;

namespace RSTK
{
    public partial class RSTKForm : MainForm
    {
        private Rocksmith rocksmith = null;
        private volatile bool running = false, readingConfig = false, showOnRocksmithExit = false, dirtyConfig = false;
        private readonly Marzersoft.Timer lastLaunchTimer
            = new Marzersoft.Timer(-5.0);
        private readonly List<Control> disabledWhileRunning
            = new List<Control>();
        private readonly List<ToolStripItem> disabledWhileRunningToolStrip
            = new List<ToolStripItem>();
        private ToolStripItem tsLaunch, tsLaunchSteam, tsOpenFolder;


        private static readonly List<String> DefaultGamePaths = new List<string>()
        {
            @"<DRIVE>:\Program Files\Steam\steamapps\common\Rocksmith2014\Rocksmith2014.exe",
            @"<DRIVE>:\Program Files (x86)\Steam\steamapps\common\Rocksmith2014\Rocksmith2014.exe",
            @"<DRIVE>:\Games\SteamLibrary\steamapps\common\Rocksmith2014\Rocksmith2014.exe",
            @"<DRIVE>:\SteamLibrary\steamapps\common\Rocksmith2014\Rocksmith2014.exe"
        };

        private static readonly List<Size> DefaultSupportedResolutions = new List<Size>
        {
            new Size( 640,480 ),
            new Size( 720,480 ),
            new Size( 720,576 ),
            new Size( 800,600 ),
            new Size( 1024,768 ),
            new Size( 1152,864 ),
            new Size( 1280,720 ), //default
            new Size( 1280,768 ),
            new Size( 1280,800 ),
            new Size( 1280,960 ),
            new Size( 1280,1024 ),
            new Size( 1360,768 ),
            new Size( 1366,768 ),
            new Size( 1440,900 ),
            new Size( 1600,900 ),
            new Size( 1600,1024 ),
            new Size( 1680,1050 ),
            new Size( 1920,1080 )
        };

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

            //resolutions
            var resolutions = new List<Size>();
            try
            {
                var displays = Devices.EnumerateAttachedDisplayDevices(true);
                var modes = Devices.EnumerateSupportedDisplayModes(displays[0]);
                foreach (var mode in modes)
                {
                    if (mode.dmPelsWidth < 640 || mode.dmPelsHeight < 480)
                        continue;
                    resolutions.Add(new Size(mode.dmPelsWidth, mode.dmPelsHeight));
                }
            }
            catch (Exception e)
            {
                Logger.Ex(e);
                resolutions.AddRange(DefaultSupportedResolutions);
            }
            resolutions = resolutions.Distinct().ToList();
            resolutions.Sort((a, b) =>
            {
                return (a.Width * 10000 + a.Height)
                    .CompareTo(b.Width * 10000 + b.Height);
            });
            foreach (var res in resolutions)
                cbResolution.ComboBox.Items.Add(string.Format("{0} x {1}", res.Width, res.Height));
            cbResolution.SelectedIndex = cbResolution.ComboBox.Items.IndexOf("1280 x 720");
            cbResolution.ComboBox.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbResolution);

            //fullscreen mode
            cbFullscreenMode.ComboBox.Items.AddRange(Enum.GetNames(typeof(Rocksmith.FullscreenModes)));
            cbFullscreenMode.ComboBox.SelectedIndex = 2;
            cbFullscreenMode.ComboBox.SelectedIndexChanged += (s, e) => { SaveConfigChanges(); };
            disabledWhileRunning.Add(cbFullscreenMode);

            //emulated fullscreen in windowed mode
            checkEmulateFullscreen.Checked = App.Config.User.Get("emulated_fullscreen", false);
            checkEmulateFullscreen.Image = checkEmulateFullscreen.Checked
                ? App.Images.Resource(!App.IsAdministrator ? "uac_24" : "warning_24", App.Assembly) : null;
            checkEmulateFullscreen.CheckBox.CheckedChanged += (s, e) =>
            {
                if (rocksmith != null)
                    rocksmith.EmulateFullscreenWhenWindowed = checkEmulateFullscreen.Checked;
                checkEmulateFullscreen.Image = checkEmulateFullscreen.Checked
                    ? App.Images.Resource(!App.IsAdministrator ? "uac_24" : "warning_24", App.Assembly) : null;
                App.Config.User.Set("emulated_fullscreen", checkEmulateFullscreen.Checked);
                dirtyConfig = true;
            };

            //exclusive mode
            checkExclusiveMode.CheckBox.CheckedChanged += (s, e) =>
            {
                checkExclusiveMode.Image = !checkExclusiveMode.Checked ? App.Images.Resource("warning_24", App.Assembly) : null;
                RefreshEffectiveLatency();
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkExclusiveMode);

            //win32ultralowlatency
            checkUltraLowLatencyMode.CheckBox.CheckedChanged += (s, e) =>
            {
                checkUltraLowLatencyMode.Image = !checkUltraLowLatencyMode.Checked ? App.Images.Resource("warning_24", App.Assembly) : null;
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
                checkDumpAudioLog.Image = checkDumpAudioLog.Checked ? App.Images.Resource("warning_24", App.Assembly) : null;
                SaveConfigChanges();
            };
            disabledWhileRunning.Add(checkDumpAudioLog);

            //run at startup
            checkStartup.Checked = App.RunAtStartup;
            checkStartup.CheckBox.CheckedChanged += (s, e) =>
            {
                App.RunAtStartup = checkStartup.Checked;
            };

            //hide rstk while rocksmith is running
            checkAutoHide.Checked = App.Config.User.Get("auto_hide", true);
            checkAutoHide.CheckBox.CheckedChanged += (s, e) =>
            {
                App.Config.User.Set("auto_hide", checkAutoHide.Checked);
                dirtyConfig = true;
            };

            //start minimized
            checkStartMinimized.Checked = App.Config.User.Get("start_hidden", false);
            checkStartMinimized.CheckBox.CheckedChanged += (s, e) =>
            {
                App.Config.User.Set("start_hidden", checkStartMinimized.Checked);
                dirtyConfig = true;
            };

            //cycle displays on exit
            checkCycleDisplays.Checked = App.Config.User.Get("cycle_displays", false);
            checkCycleDisplays.Image = checkCycleDisplays.Checked
                ? App.Images.Resource(!App.IsAdministrator ? "uac_24" : "warning_24", App.Assembly) : null;
            checkCycleDisplays.CheckBox.CheckedChanged += (s, e) =>
            {
                checkCycleDisplays.Image = checkCycleDisplays.Checked
                    ? App.Images.Resource(!App.IsAdministrator ? "uac_24" : "warning_24", App.Assembly) : null;
                App.Config.User.Set("cycle_displays", checkCycleDisplays.Checked);
                dirtyConfig = true;
            };

            //cycle displays on exit
            checkExitWhenRocksmithTerminated.Checked = App.Config.User.Get("exit_on_terminate", false);
            checkExitWhenRocksmithTerminated.Image
                = checkExitWhenRocksmithTerminated.Checked ? App.Images.Resource("warning_24", App.Assembly) : null;
            checkExitWhenRocksmithTerminated.CheckBox.CheckedChanged += (s, e) =>
            {
                checkExitWhenRocksmithTerminated.Image
                = checkExitWhenRocksmithTerminated.Checked ? App.Images.Resource("warning_24", App.Assembly) : null;
                App.Config.User.Set("exit_on_terminate", checkExitWhenRocksmithTerminated.Checked);
                dirtyConfig = true;
            };

            //launch from anywhere hotkey
            checkLaunchAnywhere.Checked = App.Config.User.Get("launch_anywhere_hotkey", false);
            checkLaunchAnywhere.CheckBox.CheckedChanged += (s, e) =>
            {
                App.Config.User.Set("launch_anywhere_hotkey", checkLaunchAnywhere.Checked);
                dirtyConfig = true;
            };

            //pre-launch command
            tbLaunchCommand.Text = App.Config.User.Get("launch_commands", "").Trim();
            tbLaunchCommand.Image
                = tbLaunchCommand.Text.Trim().Length > 0 ? App.Images.Resource("warning_24", App.Assembly) : null;
            tbLaunchCommand.TextBox.TextChanged += (s, e) =>
            {
                tbLaunchCommand.Image
                    = tbLaunchCommand.Text.Trim().Length > 0 ? App.Images.Resource("warning_24", App.Assembly) : null;
                App.Config.User.Set("launch_commands", tbLaunchCommand.Text.Trim());
                dirtyConfig = true;
            };

            //launch buttons
            disabledWhileRunning.Add(panLaunchButtons);

            //'cog' title bar button
            var tb = new TitleBarButton(this);
            tb.Image.Add(App.Images.Resource("settings_16", App.Assembly));
            tb.Click += (b) =>
            {
                TrayIconMenu.Show(this.WindowToScreen(b.Bounds.BottomLeft()));
            };

            //apply themes
            App.ThemeChanged += (t) =>
            {
                this.Execute(() =>
                {
                    splitter.Panel2.BackColor = t.Controls.HighContrast.Colour;
                    tbLaunchCommand.TextBox.Font = t.Monospaced.Regular;
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

            //about menu icon
            var icon = AddTrayIconMenuItem("About");
            icon.Image = App.Images.Resource("info");
            icon.Click += (s, ev) =>
            {
                "https://github.com/marzer/RSTK/".LaunchWebsite();
            };
            AddTrayIconMenuSeparator();

            //open rocksmith directory in explorer
            tsOpenFolder = icon = AddTrayIconMenuItem("Open Rocksmith directory");
            icon.Visible = false;
            icon.Image = App.Images.Resource("open");
            icon.Click += (s, ev) =>
            {
                rocksmith.GameDirectory.OpenFolder();
            };
            disabledWhileRunningToolStrip.Add(icon);
            

            //launch game menu icon
            tsLaunch = icon = AddTrayIconMenuItem("Play (direct)");
            icon.Visible = false;
            icon.Image = App.Images.Resource("gamepad_24", "invert", App.Assembly);
            icon.Click += btnLaunch_Click;
            disabledWhileRunningToolStrip.Add(icon);

            //launch game via steam menu icon
            tsLaunchSteam = icon = AddTrayIconMenuItem("Play (via Steam)");
            icon.Visible = false;
            icon.Image = App.Images.Resource("steam_24", "invert", App.Assembly);
            icon.Click += btnLaunchSteam_Click;
            disabledWhileRunningToolStrip.Add(icon);

            //check command line params
            string path = "";
            bool pathOK = false;
            if (App.Arguments.Value("path", ref path)
                && File.Exists(path = path.Trim()))
                pathOK = SetRocksmithPath(Path.GetFullPath(path), false);

            //check user's config
            if (!pathOK
                &&(path = App.Config.User.Get("path", "").Trim()).Length > 0
                && File.Exists(path = path.Trim()))
                pathOK = SetRocksmithPath(Path.GetFullPath(path), false);

            //check rocksmith location presets
            if (!pathOK)
            {
                for (var drive = 'C'; !pathOK && drive <= 'Z'; drive++)
                {
                    foreach (var p in DefaultGamePaths)
                    {
                        var pp = p.Replace("<DRIVE>", drive.ToString());
                        if (!File.Exists(pp))
                            continue;
                        if (pathOK = SetRocksmithPath(Path.GetFullPath(pp)))
                            break;
                    }
                }

            }

            //global hotkey
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.R);

            //show/hide properly
            this.Execute(() =>
            {
                if (checkStartMinimized.Checked)
                    Hide();
                else
                    BringToFront();
                Opacity = 1.0;
            });
        }

        private void RefreshEffectiveLatency()
        {
            var lat = Rocksmith.CalulateEffectiveLatency((uint)trackMaxOutputBufferSize.TrackBar.Value,
                (uint)trackLatencyBuffer.TrackBar.Value);
            if (!checkExclusiveMode.Checked)
                lat *= 1.5;
            if (!checkUltraLowLatencyMode.Checked)
                lat *= 1.5;
            lblEffectiveLatency.Text = string.Format("{0:0} ms", lat);
            lblEffectiveLatency.Image = lat < 10.0 || lat > 75.0 ? App.Images.Resource("warning_24", App.Assembly) : null;
        }

        private void LatencyTrackBarValueChanged(object sender, EventArgs args)
        {
            trackLatencyBuffer.Image = !trackLatencyBuffer.TrackBar.Value.IsBetween(1, 4) ? App.Images.Resource("warning_24", App.Assembly) : null;
            trackMaxOutputBufferSize.Image = !trackMaxOutputBufferSize.TrackBar.Value.IsBetween(100, 400) ? App.Images.Resource("warning_24", App.Assembly) : null;
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
                this.Execute(() => //asyncronously
                {
                    Logger.ErrorMessage(this, "Error instantiating Rocksmith manager:\r\n\r\nPath: {0}\r\nMessage: {1}",
                    fullPath, e.Message);
                });
                return false;
            }

            //clean up old instance
            if (rocksmith != null)
                rocksmith.Dispose();

            //assign new instance
            rocksmith = newRocksmith;
            rocksmith.EmulateFullscreenWhenWindowed =  checkEmulateFullscreen.Checked;
            rocksmith.SetFastPollWindow(10.0);

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
                    if (checkAutoHide.Checked && Visible)
                    {
                        Hide();
                        showOnRocksmithExit = true;
                    }

                }, false);
            };

            //rocksmith instance terminated
            rocksmith.Terminated += (rs) =>
            {
                this.Execute(() =>
                {
                    //cycle adapters
                    if (App.IsAdministrator
                        && checkCycleDisplays.Checked
                        && rs.FullscreenMode == Rocksmith.FullscreenModes.ExclusiveFullscreen)
                    {
                        //get adapters (sorted by primary first)
                        string[] displayAdapters = null;
                        try
                        {
                            displayAdapters = Devices.EnumerateDisplayAdapters(true);
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            if (Debugger.IsAttached)
                                throw;
#endif
                            this.Execute(() => //asyncronously
                            {
                                Logger.ErrorMessage(this, "Error querying system displays:\r\n\r\n{0}",
                                ex.Message);
                            });
                        }

                        if (displayAdapters.Length > 0)
                        {
#if DEBUG
                            Logger.V("Display Adapters by ID:\n{0}", string.Join(",\n", displayAdapters));
#endif
                            //wait for game
                            Thread.Sleep(250);

                            //disable all
                            List<string> successfullyDisabledAdapters = new List<string>();
                            foreach (var displayAdapter in displayAdapters)
                            {
                                try
                                {
                                    Devices.DisableDevice(Devices.ClassGuid_DisplayAdapters, displayAdapter);
                                    successfullyDisabledAdapters.Add(displayAdapter);
                                }
                                catch (Exception ex)
                                {
#if DEBUG
                                    if (Debugger.IsAttached)
                                        throw;
#endif
                                    Logger.E("DisableDevice failed:\r\n{0}\r\n{1}", displayAdapter, ex.Message);
                                }
                            }

                            //enable all
                            foreach (var displayAdapter in successfullyDisabledAdapters)
                            {
                                try
                                {
                                    Devices.EnableDevice(Devices.ClassGuid_DisplayAdapters, displayAdapter);
                                }
                                catch (Exception ex)
                                {
#if DEBUG
                                    if (Debugger.IsAttached)
                                        throw;
#endif
                                    Logger.E("EnableDevice failed:\r\n{0}\r\n{1}", displayAdapter, ex.Message);
                                }
                            }
                        }
                    }

                    //exit if user has setting applied
                    if (checkExitWhenRocksmithTerminated.Checked && rs.RunningTime >= 30.0)
                    {
                        ThreadPool.QueueUserWorkItem((state) =>
                        {
                            Thread.Sleep(500);
                            this.Execute(() =>
                            {
                                HideOnClose = false;
                                PreventUserClose = false;
                                Close();
                            });
                        });
                        return;
                    }

                    //re-enable controls
                    foreach (var c in disabledWhileRunning)
                        c.Enabled = true;
                    foreach (var c in disabledWhileRunningToolStrip)
                        c.Enabled = true;

                    //update labels and icons
                    lblStatus.Text = "Waiting for Rocksmith";
                    pbStatus.Image = App.Images.Resource("rstk_waiting_64", App.Assembly);
                    TrayIcon.Icon = Icon = App.Icons.Resource("rstk_waiting_icon", App.Assembly);

                    //show window
                    if (checkAutoHide.Checked && !Visible && showOnRocksmithExit)
                    {
                        Show();
                        BringToFront();
                    }

                    //repaint window
                    if (Visible)
                        RefreshNonClient();
                }, false);
                running = false;
            };

            //update ui
            tbRocksmithPath.Text = fullPath;
            flowLayoutPanel.Visible
                = panLaunchButtons.Visible
                = tsLaunch.Visible
                = tsLaunchSteam.Visible
                = tsOpenFolder.Visible
                = true;
            panRocksmithPath.Visible = false;
            lblStatus.Text = "Waiting for Rocksmith";
            flowLayoutPanel.FitAllChildren();

            //write to config
            if (writeToConfig)
            {
                App.Config.User.Set("path", fullPath);
                WriteConfig();
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

                var resString = string.Format("{0} x {1}", rs.Resolution.Width, rs.Resolution.Height);
                var resIndex = cbResolution.ComboBox.Items.IndexOf(resString);
                if (resIndex < 0)
                {
                    cbResolution.ComboBox.Items.Add(resString);
                    resIndex = cbResolution.ComboBox.Items.Count - 1;
                }
                cbResolution.SelectedIndex = resIndex;
                
                cbFullscreenMode.ComboBox.SelectedIndex = (int)rs.FullscreenMode;
                trackLatencyBuffer.TrackBar.Value = (int)rs.LatencyBuffer;
                trackMaxOutputBufferSize.TrackBar.Value = (int)rs.MaxOutputBufferSize;
                checkMicrophone.Checked = rs.EnableMicrophone;
                checkDumpAudioLog.Checked = rs.DumpAudioLog;

                readingConfig = false;
            }, false);
        }

        private void LaunchRocksmith(bool viaSteam = true)
        {
            if (rocksmith == null || running || lastLaunchTimer.Seconds < 5.0)
                return;
            
            //parse pre-launch commands, convert to batch script
            var batchLinesArray = tbLaunchCommand.Text
                .NormalizeLineEndings("\n")
                .Trim()
                .Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            if (batchLinesArray.Length > 0)
            {
                List<string> batchLines = new List<string>();
                for (int i = 0; i < batchLinesArray.Length; ++i)
                {
                    var cmd = batchLinesArray[i].Trim();
                    if (cmd.Length == 0)
                        continue;
                    batchLines.Add(cmd);
                }

                if (batchLines.Count > 0)
                {
                    //write as batch file
                    var batchPath = Path.Combine(App.ExecutableDirectory, "prelaunch.bat");
                    using (var stream = new FileStream(batchPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("@ECHO OFF");
                        writer.WriteLine("@setlocal enableextensions enabledelayedexpansion");
                        writer.WriteLine("CD /D \"{0}\"", rocksmith.GameDirectory);
                        writer.WriteLine("REM //////////////// BEGIN USER SCRIPT ////////////////");
                        foreach (var line in batchLines)
                            writer.WriteLine(
                                Regex.Replace(line, "[%]ROCKSMITH_DIR[%]", rocksmith.GameDirectory,
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase));
                        writer.WriteLine("REM ////////////////// END USER SCRIPT ////////////////");
                        writer.WriteLine("@endlocal");
                        writer.WriteLine("EXIT /B 0");
                    }

                    //launch batch file process
                    using (var process = new Process())
                    {
                        ProcessStartInfo si = new ProcessStartInfo();
                        //si.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        //si.CreateNoWindow = true;
                        si.FileName = batchPath;
                        si.UseShellExecute = false;
                        si.WorkingDirectory = rocksmith.GameDirectory;
                        process.StartInfo = si;
                        process.Start();
                        process.WaitForExit();

                        if (process.ExitCode != 0)
                        {
                            Logger.ErrorMessage(this, "Pre-launch commands exited with code {0}; aborting launch.",
                                process.ExitCode);
                            return;
                        }
                    }
                }
            }

            //launch rocksmith
            lastLaunchTimer.Reset();
            rocksmith.SetFastPollWindow(10.0);
            if (viaSteam)
                Process.Start(rocksmith.SteamRunCommand);
            else
            {
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.FileName = rocksmith.GamePath;
                pi.WorkingDirectory = rocksmith.GameDirectory;
                pi.UseShellExecute = true;
                Process.Start(pi);
            }
        }

        private void btnLaunchSteam_Click(object sender, EventArgs e)
        {
            this.Execute(() => { WriteConfig(); });
            LaunchRocksmith();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            this.Execute(() => { WriteConfig(); });
            LaunchRocksmith(false);
        }

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel.FitAllChildren();
        }

        private void btnLaunch_EnabledChanged(object sender, EventArgs e)
        {
            btnLaunch.Image = App.Images.Resource("gamepad_24",
                (sender as Button).Enabled ? "" : "30% alpha", App.Assembly);
        }

        private void btnLaunchSteam_EnabledChanged(object sender, EventArgs e)
        {
            btnLaunchSteam.Image = App.Images.Resource("steam_24",
                (sender as Button).Enabled ? "" : "30% alpha", App.Assembly);
        }

        private void SaveConfigChanges()
        {
            if (rocksmith == null || running || readingConfig)
                return;

            rocksmith.ExclusiveMode = checkExclusiveMode.Checked;
            rocksmith.Win32UltraLowLatencyMode = checkUltraLowLatencyMode.Checked;

            var resString = (cbResolution.ComboBox.SelectedItem as string).SplitWhitespace();
            rocksmith.Resolution = new Size(int.Parse(resString[0]), int.Parse(resString[2]));

            rocksmith.FullscreenMode = (Rocksmith.FullscreenModes)cbFullscreenMode.ComboBox.SelectedIndex;
            rocksmith.LatencyBuffer = (uint)trackLatencyBuffer.TrackBar.Value;
            rocksmith.MaxOutputBufferSize = (uint)trackMaxOutputBufferSize.TrackBar.Value;
            rocksmith.EnableMicrophone = checkMicrophone.Checked;
            rocksmith.DumpAudioLog = checkDumpAudioLog.Checked;

            rocksmith.WriteConfig();
        }

        private void flushTimer_Tick(object sender, EventArgs e)
        {
            if (dirtyConfig)
                this.Execute(() => { WriteConfig(); });
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            showOnRocksmithExit = false;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            showOnRocksmithExit = false;
        }

        protected override void OnHotkeyPress(HotkeyEventArgs args)
        {
            if (checkLaunchAnywhere.Checked && args.KeyData == (Keys.Control | Keys.Shift | Keys.R))
            {
                LaunchRocksmith();
                args.Handled = true;
                return;
            }
            base.OnHotkeyPress(args);
        }

        private void WriteConfig()
        {
            File.WriteAllText(App.UserConfigPath, App.Config.User.ToString());
            dirtyConfig = false;
        }
    }
}
