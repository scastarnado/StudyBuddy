using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;

namespace StudyBuddy.Settings
{
    public class SettingsManager
    {
        private readonly string SettingsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StudyBuddy", "settings.json");

        public AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    string json = File.ReadAllText(SettingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch
            {
                if (!File.Exists(SettingsPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                    File.WriteAllText(SettingsPath, JsonSerializer.Serialize(new AppSettings(), new JsonSerializerOptions { WriteIndented = true }));
                }
                MessageBox.Show("Settings folder did not exist or was not found. Creating it in: " + Path.GetDirectoryName(SettingsPath));
            }

            return new AppSettings(); // return defaults if file missing or corrupted
        }

        public void Save(AppSettings settings)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save settings: " + ex.Message);
            }
        }
    }
}
