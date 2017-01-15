using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSTK
{
    /// <summary>
    /// Extensions for the ini reader.
    /// </summary>
    public static class IniExtensions
    {
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
