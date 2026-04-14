using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CyberSecurityChatbot.Services
{
    public class AudioPlayer
    {
        // P/Invoke to native winmm PlaySound (Windows). Non-Windows platforms will skip playback.
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);

        // Keep SND_SYNC so the const is not removed (avoids ENC0033 Edit-and-Continue warning).
        private const uint SND_SYNC = 0x0000;
        private const uint SND_ASYNC = 0x0001;
        private const uint SND_FILENAME = 0x00020000;
        private const uint SND_NODEFAULT = 0x0002;

        public void PlayGreeting()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

            if (!File.Exists(path))
            {
                // Don't treat missing audio as fatal; just inform the user.
                Console.WriteLine("Audio file not found.");
                return;
            }

            // Start playback on a background task so the console UI is not blocked.
            Task.Run(() =>
            {
                try
                {
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        // Optionally add cross-platform playback later; for now inform and return.
                        Console.WriteLine("Audio playback is supported only on Windows in this build.");
                        return;
                    }

                    // Use async playback so the console renders immediately.
                    bool ok = PlaySound(path, IntPtr.Zero, SND_FILENAME | SND_ASYNC | SND_NODEFAULT);
                    if (!ok)
                    {
                        Console.WriteLine("Audio error: failed to play file.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Audio error: " + ex.Message);
                }
            });
        }
    }
}