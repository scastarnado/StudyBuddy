using StudyBuddy.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StudyBuddy.Services
{
    public class FocusBlockerService
    {
        private AppSettings _settings;
        private Timer _monitorTimer;
        private bool _isBlocking;
        private static FocusBlockerService _instance;
        private string _hostsFilePath;
        private List<string> _originalHostsContent;

        public static FocusBlockerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FocusBlockerService();
                }
                return _instance;
            }
        }

        public FocusBlockerService()
        {
            _settings = SettingsManager.Instance.Load();
            _monitorTimer = new Timer(2000); // Check every 2 seconds
            _monitorTimer.Elapsed += MonitorTimer_Elapsed;
            _isBlocking = false;
            _hostsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), 
                @"drivers\etc\hosts");
            _originalHostsContent = new List<string>();
        }

        public void StartBlocking()
        {
            StartBlocking(false);
        }

        public void StartBlocking(bool force)
        {
            _settings = SettingsManager.Instance.Load();

            if ((_settings.EnableFocusBlocker || force) && !_isBlocking)
            {
                _isBlocking = true;
                _monitorTimer.Start();
                BlockWebsites();
                BlockApplications();
            }
        }

        public void StopBlocking()
        {
            _isBlocking = false;
            _monitorTimer.Stop();
            UnblockWebsites();
        }

        private void MonitorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_isBlocking)
                return;

            BlockApplications();
        }

        private void BlockApplications()
        {
            if (_settings.BlockedApplications == null || _settings.BlockedApplications.Count == 0)
                return;

            try
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    try
                    {
                        // Check if the process name matches any blocked application
                        if (_settings.BlockedApplications.Any(blocked => 
                            process.ProcessName.Equals(blocked.Replace(".exe", ""), 
                            StringComparison.OrdinalIgnoreCase)))
                        {
                            process.Kill();
                        }
                    }
                    catch
                    {
                        // Process might have already exited or we don't have permission
                        continue;
                    }
                }
            }
            catch
            {
                // Handle any general errors
            }
        }

        private void BlockWebsites()
        {
            try
            {
                // Read current hosts file
                if (File.Exists(_hostsFilePath))
                {
                    _originalHostsContent = File.ReadAllLines(_hostsFilePath).ToList();
                }

                var hostsEntries = new List<string>(_originalHostsContent);

                // Add blocking entries for each website
                hostsEntries.Add("");
                hostsEntries.Add("# StudyBuddy - Blocked websites during focus session");

                foreach (var website in _settings.BlockedWebsites)
                {
                    string cleanWebsite = website.Replace("https://", "").Replace("http://", "").Replace("www.", "");
                    hostsEntries.Add($"127.0.0.1 {cleanWebsite}");
                    hostsEntries.Add($"127.0.0.1 www.{cleanWebsite}");
                }

                // Write to hosts file (requires admin privileges)
                try
                {
                    File.WriteAllLines(_hostsFilePath, hostsEntries);
                    FlushDNS();
                }
                catch (UnauthorizedAccessException)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Cannot block websites. Please run StudyBuddy as Administrator to enable website blocking.",
                        "Admin Rights Required",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error blocking websites: {ex.Message}");
            }
        }

        private void UnblockWebsites()
        {
            try
            {
                if (_originalHostsContent.Count > 0)
                {
                    // Restore original hosts file
                    File.WriteAllLines(_hostsFilePath, _originalHostsContent);
                    FlushDNS();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error unblocking websites: {ex.Message}");
            }
        }

        private void FlushDNS()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "ipconfig",
                    Arguments = "/flushdns",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
            catch
            {
                // DNS flush failed, but continue anyway
            }
        }

        public void UpdateSettings(AppSettings settings)
        {
            _settings = settings;
        }

        public void AddBlockedApplication(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                return;

            string normalizedAppName = appName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
                ? appName
                : appName + ".exe";

            if (!_settings.BlockedApplications.Contains(normalizedAppName, StringComparer.OrdinalIgnoreCase))
            {
                _settings.BlockedApplications.Add(normalizedAppName);
                SettingsManager.Instance.Save(_settings);
            }
        }

        public void RemoveBlockedApplication(string appName)
        {
            var existingApplication = _settings.BlockedApplications
                .FirstOrDefault(app => string.Equals(app, appName, StringComparison.OrdinalIgnoreCase));

            if (existingApplication != null)
            {
                _settings.BlockedApplications.Remove(existingApplication);
                SettingsManager.Instance.Save(_settings);
            }
        }

        public void AddBlockedWebsite(string website)
        {
            if (!_settings.BlockedWebsites.Contains(website))
            {
                _settings.BlockedWebsites.Add(website);
                SettingsManager.Instance.Save(_settings);
            }
        }

        public void RemoveBlockedWebsite(string website)
        {
            if (_settings.BlockedWebsites.Contains(website))
            {
                _settings.BlockedWebsites.Remove(website);
                SettingsManager.Instance.Save(_settings);
            }
        }

        public List<string> GetBlockedApplications()
        {
            return new List<string>(_settings.BlockedApplications);
        }

        public List<string> GetBlockedWebsites()
        {
            return new List<string>(_settings.BlockedWebsites);
        }

        public bool IsBlocking
        {
            get { return _isBlocking; }
        }
    }
}
