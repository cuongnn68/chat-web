using System.Diagnostics;

namespace DiscordRipoff.Utils {
    public class VueUtil { // very basic functional TODO: check port, run on linux, ...
        public static void RunServe() {
            // Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo {
                FileName = "cmd",
                Arguments = "/c npm run serve", // cant run without /c ????
                WorkingDirectory = System.IO.Directory.GetCurrentDirectory() + "\\client-app"
            };
            // process.Start(info);
            Process.Start(info);
        }
    }
}