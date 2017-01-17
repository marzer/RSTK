using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marzersoft;
using System.IO;
using IniParser.Parser;
using System.Threading;

namespace RSTK
{
    /// <summary>
    /// Extensions for the ini reader.
    /// </summary>
    public static class IniExtensions
    {
        public static IniData TryReadIniFile(this string filePath, uint maxAttempts = 3, uint attemptDelay = 250)
        {
            maxAttempts = maxAttempts.Clamp(1, 10);
            uint attempts = 0;
            while (true)
            {
                try
                {
                    //open file stream
                    FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    try
                    {
                        //read contents of file
                        var fileData = new byte[stream.Length];
                        stream.Read(fileData, 0, fileData.Length);

                        //parse ini
                        IniDataParser parser = new IniDataParser();
                        return parser.Parse(fileData.DetectEncoding().GetString(fileData));
                    }
                    finally
                    {
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    if (++attempts >= maxAttempts)
                        throw;
                    if (attemptDelay > 0)
                        Thread.Sleep((int)attemptDelay);
                }
            }
        }

        public static bool ReadBool(this IniData ini, string section, string key, bool defaultValue)
        {
            if (ini == null)
                throw new ArgumentNullException("ini");
            var val = (ini[section][key] ?? "").Trim().ToLower();
            if (val.Length == 0)
                return defaultValue;
            if (int.TryParse(val, out var parsed))
                return parsed != 0;
            if (val.Equals("true") || val.Equals("on") || val.Equals("yes"))
                return true;
            if (val.Equals("false") || val.Equals("off") || val.Equals("no"))
                return false;
            return defaultValue;
        }

        public static uint ReadUint(this IniData ini, string section, string key, uint defaultValue)
        {
            if (ini == null)
                throw new ArgumentNullException("ini");
            var val = (ini[section][key] ?? "").Trim().ToLower();
            if (val.Length == 0)
                return defaultValue;
            if (uint.TryParse(val, out var parsed))
                return parsed;
            return defaultValue;
        }
    }
}
