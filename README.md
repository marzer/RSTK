# RSTK  
A launcher and configuration editor for Rocksmith and Rocksmith 2014.  

![RSTK Screenshot](/screenshot.png)  

## Downloads  
[RSTK Releases](https://github.com/marzer/RSTK/releases)  

## Setup
1. Extract the contents of the zip file to your computer  
2. Run `RSTK.exe` (preferably as an Administrator)  
3. Use the "open" button under "Rocksmith Location" to tell RSTK where Rocksmith is on your computer  
    a. *Optionally*: Add RSTK to startup via scheduled tasks or a shortcut in Startup (I'll add this as a setting inside RSTK itself eventually)  

## Usage
Once RSTK is running, it sits in your task tray (don't worry, it's resource usage is practically negligible). Double-click it to show the RSTK window so you can make quick edits to your `Rocksmith.ini` settings, or right-click it to quickly launch Rocksmith.  

For detailed explanation of the settings available within RSTK, see the tooltips shown when you hover over a setting.  

## Troubleshooting
If RSTK is not detecting Rocksmith when you launch it, or not enforcing `Emulated Fullscreen`, or not 
executing `Cycle displays when Rocksmith exits`, run RSTK as an administrator.  

For reporting crashes/bugs/feature requests, [go here](https://github.com/marzer/RSTK/issues).  

## Roadmap 
Here's some stuff I'd like to implement:  
* Automatic handling of DebugAudioLog as part of a "debugging" launch mode (handling DebugAudioLog on/off and capturing log file automatically)  
* Importing/exporting ini files  
* Adding more of the video options to the available controls  
* Auto-detection of 'best' latency settings for current machine  
* Automatic setting of correct audio format for Realtone cable  
