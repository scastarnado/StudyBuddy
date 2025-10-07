using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Settings
{
    public class AppSettings
    {
        public int SessionAmount { get; set; } = 4;
        public int CurrentSession { get; set; } = 0;
        public int SessionMinutes { get; set; } = 25;
        public int ShortBreakMinutes { get; set; } = 5;
        public int LongBreakMinutes { get; set; } = 15;
        public int SessionsBeforeLongBreak { get; set; } = 4;
        public bool EnableFocusBlocker { get; set; } = false;
        
        public bool AutoStartNextSession { get; set; } = true;

        public List<string> BlockedWebsites { get; set; } = new List<string>
        {
            "facebook.com",
            "twitter.com",
            "instagram.com",
            "reddit.com",
            "youtube.com"
        };
        public List<string> BlockedApplications { get; set; } = new List<string>
        {
            "chrome.exe",
            "firefox.exe",
            "spotify.exe",
            "steam.exe"
        };
    }
}
