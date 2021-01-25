using System;
using System.Globalization;

namespace DiscordRipoff.Utils {
    public class MyTimeFormat {
        public static string Short(DateTime dt) {
            return dt.ToString(provider: CultureInfo.CreateSpecificCulture("en-UK"));
        }
    }
}