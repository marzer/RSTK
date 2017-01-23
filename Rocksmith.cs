using IniParser;
using IniParser.Model;
using Marzersoft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Marzersoft.Native;

namespace RSTK
{
    /// <summary>
    /// Class representing a Rocksmith executable.
    /// </summary>
    public sealed class Rocksmith : IStatefulDisposable
    {
        /////////////////////////////////////////////////////////////////////
        // EVENTS
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event invoked when the config values are updated by reading them from Rocksmith.ini.
        /// </summary>
        public event Action<Rocksmith> ConfigRead;

        /// <summary>
        /// Event invoked when the rocksmith executable is detected as currently running.
        /// </summary>
        public event Action<Rocksmith> Running;

        /// <summary>
        /// Event invoked when the rocksmith executable which was running is detected to have been terminated.
        /// </summary>
        public event Action<Rocksmith> Terminated;

        /////////////////////////////////////////////////////////////////////
        // PROPERTIES/VARIABLES
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Has this wrapper object been disposed?
        /// </summary>
        public bool IsDisposed => disposed;
        private volatile bool disposed = false;

        /// <summary>
        /// Fullscreen modes supported by Rocksmith.
        /// </summary>
        public enum FullscreenModes : uint
        {
            Windowed = 0,
            WindowedFullscreen = 1,
            ExclusiveFullscreen = 2
        };

        /// <summary>
        /// Full path to the game's executable.
        /// </summary>
        public readonly string GamePath;

        /// <summary>
        /// Full path to the folder containing game's executable and ini file.
        /// </summary>
        public readonly string GameDirectory;

        /// <summary>
        /// Full path to the game's ini file.
        /// </summary>
        public readonly string ConfigPath;

        /// <summary>
        /// Steam app id for this version of rocksmith.
        /// </summary>
        public readonly uint AppID;

        /// <summary>
        /// Command to run this version of rocksmith via Steam.
        /// </summary>
        public string SteamRunCommand => string.Format("steam://run/{0}", AppID);

        private Thread thread = null;
        private readonly object threadLock = new object();

        /// <summary>
        /// Rocksmith game process.
        /// </summary>
        private Process GameProcess
        {
            get { return process; }
            set
            {
                if (process == value)
                    return;

                var old = process;
                process = value;
                if (old != null)
                {
                    lastRunningTime = runningTimer.Seconds;
                    old.Dispose();
                    Terminated?.Invoke(this);
                }

                if (process != null)
                {
                    runningTimer.Reset();
                    Running?.Invoke(this);
                }
            }
        }
        private Process process;

        /// <summary>
        /// Rocksmith.ini Fullscreen
        /// default: ExclusiveFullscreen (2)
        /// </summary>
        public FullscreenModes FullscreenMode
        {
            get { return fullscreenMode; }
            set
            {
                this.DisposeCheck();
                if (value > FullscreenModes.ExclusiveFullscreen)
                    throw new ArgumentOutOfRangeException("FullscreenMode");

                fullscreenMode = value;
            }
        }
        private FullscreenModes fullscreenMode = FullscreenModes.ExclusiveFullscreen;

        /// <summary>
        /// All screen resolutions supported by rocksmith.
        /// </summary>
        public static IReadOnlyList<Tuple<uint, uint>> SupportedResolutions
        {
            get { return supportedResolutions.AsReadOnly(); }
        }
        private static List<Tuple<uint, uint>> supportedResolutions = new List<Tuple<uint, uint>>
        {
            new Tuple<uint, uint>( 640,480 ),
            new Tuple<uint, uint>( 720,480 ),
            new Tuple<uint, uint>( 720,576 ),
            new Tuple<uint, uint>( 800,600 ),
            new Tuple<uint, uint>( 1024,768 ),
            new Tuple<uint, uint>( 1152,864 ),
            new Tuple<uint, uint>( 1280,720 ), //default
            new Tuple<uint, uint>( 1280,768 ),
            new Tuple<uint, uint>( 1280,800 ),
            new Tuple<uint, uint>( 1280,960 ),
            new Tuple<uint, uint>( 1280,1024 ),
            new Tuple<uint, uint>( 1360,768 ),
            new Tuple<uint, uint>( 1366,768 ),
            new Tuple<uint, uint>( 1440,900 ),
            new Tuple<uint, uint>( 1600,900 ),
            new Tuple<uint, uint>( 1600,1024 ),
            new Tuple<uint, uint>( 1680,1050 ),
            new Tuple<uint, uint>( 1920,1080 )
        };

        /// <summary>
        /// Rocksmith.ini ScreenWidth and ScreenHeight
        /// default: 1280, 720
        /// </summary>
        public Size Resolution
        {
            get { return resolution; }
            set
            {
                this.DisposeCheck();

                if (value.Width < 640)
                    throw new ArgumentOutOfRangeException("Resolution.Width", "Must be >= 640");
                if (value.Height < 480)
                    throw new ArgumentOutOfRangeException("Resolution.Height", "Must be >= 480");

                bool found = false;
                foreach (var res in supportedResolutions)
                {
                    if (res.Item1 == value.Width && res.Item2 == value.Height)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    throw new ArgumentOutOfRangeException("Resolution",
                        string.Format("{0} x {1} is not a resolution supported by Rocksmith",value.Width,value.Height));

                resolution = value;
            }
        }
        private Size resolution = new Size(1280, 720);

        /// <summary>
        /// Rocksmith.ini ExclusiveMode
        /// default: true
        /// </summary>
        public bool ExclusiveMode
        {
            get { return exclusive; }
            set
            {
                this.DisposeCheck();
                exclusive = value;
            }
        }
        private bool exclusive = true;

        /// <summary>
        /// Rocksmith.ini MaxOutputBufferSize
        /// default: 0
        /// </summary>
        public uint MaxOutputBufferSize
        {
            get { return maxOutputBufferSize; }
            set
            {
                this.DisposeCheck();
                maxOutputBufferSize = value;
            }
        }
        private uint maxOutputBufferSize = 0;

        /// <summary>
        /// Rocksmith.ini LatencyBuffer
        /// default: 4
        /// </summary>
        public uint LatencyBuffer
        {
            get { return latencyBuffer; }
            set
            {
                this.DisposeCheck();
                if (value == 0 || value > 16)
                    throw new ArgumentOutOfRangeException("LatencyBuffer", "must be between 1 and 16 (inclusive)");
                latencyBuffer = value;
            }
        }
        private uint latencyBuffer = 4;

        /// <summary>
        /// Rocksmith.ini Win32UltraLowLatencyMode
        /// default: true
        /// </summary>
        public bool Win32UltraLowLatencyMode
        {
            get { return win32LowLatency; }
            set
            {
                this.DisposeCheck();
                win32LowLatency = value;
            }
        }
        private bool win32LowLatency = true;

        /// <summary>
        /// Approximate effective total latency in ms.
        /// (see: http://forums.ubi.com/showthread.php/719817-A-guide-to-achieving-low-latency-in-Rocksmith-on-PC-Forums)
        /// </summary>
        public static double CalulateEffectiveLatency(uint maxOutputBufferSize, uint latencyBuffer)
        {
            return ((maxOutputBufferSize == 0 ? 1024 : maxOutputBufferSize) * latencyBuffer) / 16.0;
        }

        /// <summary>
        /// Approximate effective total latency in ms for the game given the current settings.
        /// (see: http://forums.ubi.com/showthread.php/719817-A-guide-to-achieving-low-latency-in-Rocksmith-on-PC-Forums)
        /// </summary>
        public double EffectiveLatency
        {
            get { return CalulateEffectiveLatency(maxOutputBufferSize, latencyBuffer); }
        }

        /// <summary>
        /// Remove game window borders when in Windowed?
        /// </summary>
        public bool EmulateFullscreenWhenWindowed
        {
            get { return emulatedFullscreen; }
            set
            {
                this.DisposeCheck();
                emulatedFullscreen = value;
            }
        }
        private volatile bool emulatedFullscreen = false;
        private Size emulatedSizeDelta = Size.Empty;

        /// <summary>
        /// Rocksmith.ini EnableMicrophone
        /// default: true
        /// </summary>
        public bool EnableMicrophone
        {
            get { return enableMicrophone; }
            set
            {
                this.DisposeCheck();
                enableMicrophone = value;
            }
        }
        private bool enableMicrophone = true;

        /// <summary>
        /// Rocksmith.ini DumpAudioLog
        /// default: false
        /// </summary>
        public bool DumpAudioLog
        {
            get { return dumpAudioLog; }
            set
            {
                this.DisposeCheck();
                dumpAudioLog = value;
            }
        }
        private bool dumpAudioLog = false;

        /// <summary>
        /// Gets/sets the window styles of the rocksmith window.
        /// </summary>
        private WindowStyles WindowStyles
        {
            get { return process.MainWindowHandle.GetStyles(); }
            set { process.MainWindowHandle.SetStyles(value); }
        }

        /// <summary>
        /// Gets/sets the window styles Ex of the rocksmith window.
        /// </summary>
        private WindowStylesEx WindowStylesEx
        {
            get { return process.MainWindowHandle.GetStylesEx(); }
            set { process.MainWindowHandle.SetStylesEx(value); }
        }

        /// <summary>
        /// Gets/sets the bounds of the rocksmith window.
        /// </summary>
        private Rectangle WindowBounds
        {
            get { return process.MainWindowHandle.GetWindowRect(); }
            set
            {
                if (!value.Equals(WindowBounds))
                {
                    process.MainWindowHandle.SetWindowPos(IntPtr.Zero, value.Left, value.Top, value.Width, value.Height,
                        SetWindowPosFlags.NoZOrder | SetWindowPosFlags.FrameChanged | SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoCopyBits);
                }
            }
        }

        /// <summary>
        /// Gets/sets the size of the rocksmith window.
        /// </summary>
        private Size WindowSize
        {
            get { return process.MainWindowHandle.GetWindowRect().Size; }
            set
            {
                if (!value.Equals(WindowSize))
                {
                    process.MainWindowHandle.SetWindowPos(IntPtr.Zero, 0, 0, value.Width, value.Height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder | SetWindowPosFlags.FrameChanged
                    | SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoCopyBits);
                }
            }
        }

        /// <summary>
        /// Gets/sets the position of the rocksmith window.
        /// </summary>
        private Point WindowPosition
        {
            get { return process.MainWindowHandle.GetWindowRect().Location; }
            set
            {
                if (!value.Equals(WindowPosition))
                {
                    process.MainWindowHandle.SetWindowPos(IntPtr.Zero, value.X, value.Y, 0, 0,
                        SetWindowPosFlags.NoSize | SetWindowPosFlags.NoZOrder
                        | SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoCopyBits);
                }
            }
        }

        /// <summary>
        /// Gets/sets the topmost state of the rocksmith window.
        /// </summary>
        private bool TopMost
        {
            get { return WindowStylesEx.HasFlag(WindowStylesEx.TopMost); }
            set
            {
                if (value == TopMost)
                    return;

                if (!value)
                {
                    WindowStylesEx &= ~(WindowStylesEx.TopMost);
                    process.MainWindowHandle.SetWindowPos(new IntPtr(-2), 0, 0, 0, 0,
                        SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove
                        | SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoCopyBits);
                }
                else
                {
                    WindowStylesEx |= WindowStylesEx.TopMost;
                    process.MainWindowHandle.SetWindowPos(new IntPtr(-1), 0, 0, 0, 0,
                        SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove
                        | SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoCopyBits);
                }

            }
        }

        /// <summary>
        /// Remaining duration of high-frequency polling period.
        /// </summary>
        private volatile int highFrequencyPollMilliseconds = 0;

        /// <summary>
        /// Directory watcher for monitoring Rocksmith.ini changes.
        /// </summary>
        private readonly FileSystemWatcher directoryWatcher;

        /// <summary>
        /// Amount of time, in seconds, rocksmith has been running for.
        /// If Rocksmith is not running, returns the length of the most recent session.
        /// </summary>
        public double RunningTime
        {
            get { return process != null ? runningTimer.Seconds : lastRunningTime; }
        }
        private readonly Marzersoft.Timer runningTimer = new Marzersoft.Timer();
        private double lastRunningTime = 0.0;

        /////////////////////////////////////////////////////////////////////
        // CONSTRUCTION/INITIALIZATION/DESTRUCTION
        /////////////////////////////////////////////////////////////////////

        public Rocksmith(string gameExecutablePath)
        {
            //paths
            if ((gameExecutablePath ?? "").Trim().Length == 0)
                throw new ArgumentOutOfRangeException("gameExecutablePath", "cannot be blank");
            if (!File.Exists(GamePath = Path.GetFullPath(gameExecutablePath)))
                throw new FileNotFoundException("game executable not found", GamePath);
            var path = GamePath.ToLower();
            if (!Path.HasExtension(path) || !Path.GetExtension(path).Equals(".exe"))
                throw new ArgumentOutOfRangeException("gameExecutablePath", "must be an exe");
            var exe = Path.GetFileNameWithoutExtension(path);
            if ((AppID = exe.Equals("rocksmith") ? 205190u : (exe.Equals("rocksmith2014") ? 221680u : 0u)) == 0u)
                throw new ArgumentOutOfRangeException("gameExecutablePath", "must be Rocksmith or Rocksmith2014");
            if (!Directory.Exists(GameDirectory = Path.GetDirectoryName(GamePath))) //shouldn't happen (?)
                throw new DirectoryNotFoundException("game directory not found");
            ConfigPath = Path.Combine(GameDirectory, "Rocksmith.ini");

            //read intial config from ini file (or load defaults)
            ReadConfig();

            //create watcher to monitor for external config changes (by the game)
            directoryWatcher = new FileSystemWatcher(GameDirectory, "Rocksmith.ini");
            directoryWatcher.IncludeSubdirectories = false;
            directoryWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            directoryWatcher.Created += ConfigChanged;
            directoryWatcher.Changed += ConfigChanged;
            directoryWatcher.Deleted += ConfigChanged;
            directoryWatcher.EnableRaisingEvents = true;

            //launch monitoring thread
            thread = new Thread(() =>
            {
                //initial sleep so caller can bind events etc
                //(ugly but meh)
                Thread.Sleep(250);
                if (disposed)
                    return;

                //poll
                while (!disposed)
                {
                    //sleep for 500ms
                    ThreadExtensions.Sleep(500, () => disposed);
                    if (disposed)
                        break;

                    //refresh process
                    bool wasRunning = process != null;
                    RefreshProcess();
                    bool running = process != null;

                    //if no process, sleep for another 4500 milliseconds
                    //(so we only poll every 5 seconds when no game is running)
                    //...unless highFrequencyPollSeconds is > 0;
                    if (!running)
                    {
                        if (highFrequencyPollMilliseconds > 0)
                            highFrequencyPollMilliseconds -= 500;
                        else
                        {
                            ThreadExtensions.Sleep(4500, () => disposed);
                            if (disposed)
                                break;
                        }

                        //if the game just exited, refresh config one last time
                        //if (wasRunning)
                          //  ReadConfig();

                        continue;
                    }

                    //refresh ini if the game was just launched
                    //ReadConfig();

                    //emulated fullscreen
                    if (fullscreenMode == FullscreenModes.Windowed)
                    {
                        if (emulatedFullscreen)
                        {
                            //calculate size differential (for reverting)
                            if (emulatedSizeDelta.IsEmpty)
                            {
                                var size = WindowSize;
                                emulatedSizeDelta = Size.Subtract(size, resolution);
                            }

                            //change frame and z-order
                            WindowStyles = WindowStyles.ClipSiblings | WindowStyles.Visible | WindowStyles.PopupWindow;
                            TopMost = true;
                            WindowStylesEx = WindowStylesEx.TopMost;
                            
                            //move and resize if necessary
                            var currentBounds = WindowBounds;
                            var screenBounds = Screen.FromRectangle(currentBounds).Bounds;
                            var targetBounds = new Rectangle(0, 0, resolution.Width + 2, resolution.Height + 2);
                            WindowBounds = targetBounds.AlignCenter(screenBounds.Center());
                        }
                        else
                            RevertEmulatedFullscreen();
                    }
                }

                //revert emulated fullscreen on exit
                if (fullscreenMode == FullscreenModes.Windowed)
                    RevertEmulatedFullscreen();
            });
            thread.IsBackground = false;
            thread.Start();
        }

        private void RevertEmulatedFullscreen()
        {
            if (GameProcess == null || emulatedSizeDelta.IsEmpty)
                return;

            WindowStyles = WindowStyles.MinimizeBox | WindowStyles.SystemMenu | WindowStyles.Caption
                | WindowStyles.ClipSiblings | WindowStyles.Visible;
            TopMost = false;
            WindowStylesEx = WindowStylesEx.WindowEdge;
            WindowSize = Size.Add(resolution, emulatedSizeDelta);
        }

        /////////////////////////////////////////////////////////////////////
        // FINDING ROCKSMITH PROCESS
        /////////////////////////////////////////////////////////////////////

        private void RefreshProcess()
        {
            //refresh existing process
            if (process != null)
            {
                if (!process.HasExited)
                    process.Refresh();
                if (process.HasExited)
                {
                    Logger.I("Rocksmith[{0}] has exited.", GameProcess.Id);
                    GameProcess = null; //property fires events
                }
            }
            if (process != null)
            {
                highFrequencyPollMilliseconds = 0;
                return;
            }

            //get processes
            var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(GamePath));
            if (processes.Length == 0)
            {
                GameProcess = null;
                return;
            }

            //get rocksmith process
            var path = GamePath.ToLower();
            GameProcess = processes.Where((p) =>
            {
                try
                { 
                    return p.MainWindowHandle != IntPtr.Zero
                    && !p.HasExited
                    && p.MainModule != null
                    && p.MainModule.FileName.ToLower().Equals(path);
                } catch (Exception) { return false; }
            }).FirstOrDefault();

            //cleanup
            foreach (var p in processes)
                if (p != GameProcess)
                    p.Dispose();
        }

        /////////////////////////////////////////////////////////////////////
        // READ CONFIG FILE
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event handler for detecting config file changes made when the game is running.
        /// </summary>
        private void ConfigChanged(object watcher, FileSystemEventArgs e)
        {
            if (process == null)
                return;
            ReadConfig();
        }

        /// <summary>
        /// Reads rocksmith.ini and updates config values accordingly.
        /// </summary>
        private void ReadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                var ini = ConfigPath.TryReadIniFile();

                enableMicrophone = ini.ReadBool("Audio", "EnableMicrophone", true);
                dumpAudioLog = ini.ReadBool("Audio", "DumpAudioLog", false);
                exclusive = ini.ReadBool("Audio", "ExclusiveMode", true);
                latencyBuffer = ini.ReadUint("Audio", "LatencyBuffer", 4);
                maxOutputBufferSize = ini.ReadUint("Audio", "MaxOutputBufferSize", 0);
                win32LowLatency = ini.ReadBool("Audio", "Win32UltraLowLatencyMode", true);
                fullscreenMode = (FullscreenModes)ini.ReadUint("Renderer.Win32", "Fullscreen",
                    (uint)FullscreenModes.ExclusiveFullscreen);
                resolution = new Size(
                    (int)ini.ReadUint("Renderer.Win32", "ScreenWidth", 0),
                    (int)ini.ReadUint("Renderer.Win32", "ScreenHeight", 0));
                if (resolution.Width == 0 || resolution.Height == 0)
                    resolution = new Size(1280, 720);
            }
            else
            {
                enableMicrophone = true;
                dumpAudioLog = false;
                exclusive = true;
                latencyBuffer = 4;
                maxOutputBufferSize = 0;
                win32LowLatency = true;
                fullscreenMode = FullscreenModes.ExclusiveFullscreen;
                resolution = new Size(1280, 720);
            }
#if DEBUG
            Logger.V("Rocksmith.ini read.");
#endif
            ConfigRead?.Invoke(this);
        }


        /////////////////////////////////////////////////////////////////////
        // WRITING CONFIG FILE
        /////////////////////////////////////////////////////////////////////

        public void WriteConfig()
        {
            //create ini file
            var ini = new IniData();
            ini.Configuration.AssigmentSpacer = "";
            ini.Sections.AddSection("Audio");
            ini["Audio"]["EnableMicrophone"] = enableMicrophone ? "1" : "0";
            ini["Audio"]["DumpAudioLog"] = dumpAudioLog ? "1" : "0";
            ini["Audio"]["ExclusiveMode"] = exclusive ? "1" : "0";
            ini["Audio"]["LatencyBuffer"] = latencyBuffer.ToString();
            ini["Audio"]["MaxOutputBufferSize"] = maxOutputBufferSize.ToString();
            ini["Audio"]["Win32UltraLowLatencyMode"] = win32LowLatency ? "1" : "0";
            ini.Sections.AddSection("Renderer.Win32");
            ini["Renderer.Win32"]["Fullscreen"] = ((uint)fullscreenMode).ToString();
            ini["Renderer.Win32"]["ScreenWidth"] = resolution.Width.ToString();
            ini["Renderer.Win32"]["ScreenHeight"] = resolution.Height.ToString();

            //merge with existing ini if it exists
            if (File.Exists(ConfigPath))
            {
                var existing = ConfigPath.TryReadIniFile();
                existing.Merge(ini);
                ini = existing;
                ini.Configuration.AssigmentSpacer = "";
            }

            //write to disk
            var parser = new FileIniDataParser();
            parser.WriteFile(ConfigPath, ini, Encoding.GetEncoding(1252));//new UTF8Encoding(false));
        }


        /////////////////////////////////////////////////////////////////////
        // HIGH FREQUENCY POLL WINDOW
        /////////////////////////////////////////////////////////////////////

        public void SetFastPollWindow(double sec)
        {
            this.DisposeCheck();
            if (process != null)
                return;
            highFrequencyPollMilliseconds = (int)Math.Min(Math.Max(sec,0.0) * 1000.0, 10000.0);
        }

        /////////////////////////////////////////////////////////////////////
        // DISPOSE
        /////////////////////////////////////////////////////////////////////

        public void Dispose()
        {
            disposed = true;
            if (thread != null)
                thread.Join();
            GameProcess = null;
            directoryWatcher.Dispose();
            Running = null;
            Terminated = null;
            ConfigRead = null;
        }
    }
}
