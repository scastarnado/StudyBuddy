using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyBuddy.Data
{
    public class LocalStorage
    {
        private readonly string _dataPath;
        private static LocalStorage _instance;

        public static LocalStorage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalStorage();
                }
                return _instance;
            }
        }

        public LocalStorage()
        {
            _dataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "StudyBuddy",
                "data"
            );

            // Ensure directory exists
            Directory.CreateDirectory(_dataPath);
        }

        public void SaveTasks(List<TaskItem> tasks)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "tasks.json");
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save tasks: {ex.Message}");
            }
        }

        public List<TaskItem> LoadTasks()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "tasks.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load tasks: {ex.Message}");
            }

            return new List<TaskItem>();
        }

        public void SaveSessions(List<PomodoroSession> sessions)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "sessions.json");
                string json = JsonSerializer.Serialize(sessions, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save sessions: {ex.Message}");
            }
        }

        public List<PomodoroSession> LoadSessions()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "sessions.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<PomodoroSession>>(json) ?? new List<PomodoroSession>();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load sessions: {ex.Message}");
            }

            return new List<PomodoroSession>();
        }

        public void SaveGoals(List<Goal> goals)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "goals.json");
                string json = JsonSerializer.Serialize(goals, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save goals: {ex.Message}");
            }
        }

        public List<Goal> LoadGoals()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "goals.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<Goal>>(json) ?? new List<Goal>();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load goals: {ex.Message}");
            }

            return new List<Goal>();
        }

        public void SaveCalendarEvents(List<CalendarEvent> events)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "calendar.json");
                string json = JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save calendar events: {ex.Message}");
            }
        }

        public List<CalendarEvent> LoadCalendarEvents()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "calendar.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<CalendarEvent>>(json) ?? new List<CalendarEvent>();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load calendar events: {ex.Message}");
            }

            return new List<CalendarEvent>();
        }

        public void ClearAllData()
        {
            try
            {
                if (Directory.Exists(_dataPath))
                {
                    Directory.Delete(_dataPath, true);
                    Directory.CreateDirectory(_dataPath);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to clear data: {ex.Message}");
            }
        }
    }
}
