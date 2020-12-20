using System;
using System.Globalization;

namespace DiscordRipoff.Utils {
    public class MyTimeFormat {
        static public string Short(DateTime dt) {
            return dt.ToString(provider: CultureInfo.CreateSpecificCulture("en-UK"));
        }
    }
}