using StudyBuddy.Data;
using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyBuddy.Services
{
    public class SessionHistoryService
    {
        private List<PomodoroSession> _sessions;
        private static SessionHistoryService _instance;

        public static SessionHistoryService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionHistoryService();
                }
                return _instance;
            }
        }

        public SessionHistoryService()
        {
            _sessions = LocalStorage.Instance.LoadSessions();
        }

        public void AddSession(PomodoroSession session)
        {
            if (session != null)
            {
                _sessions.Add(session);
                LocalStorage.Instance.SaveSessions(_sessions);
            }
        }

        public void CompleteCurrentSession(string taskName = "")
        {
            if (_sessions.Count > 0)
            {
                var lastSession = _sessions.Last();
                if (!lastSession.IsCompleted)
                {
                    lastSession.Complete();
                    if (!string.IsNullOrEmpty(taskName))
                    {
                        lastSession.TaskName = taskName;
                    }
                    LocalStorage.Instance.SaveSessions(_sessions);
                }
            }
        }

        public List<PomodoroSession> GetAllSessions()
        {
            return new List<PomodoroSession>(_sessions);
        }

        public List<PomodoroSession> GetSessionsByDate(DateTime date)
        {
            return _sessions.Where(s => s.StartTime.Date == date.Date).ToList();
        }

        public List<PomodoroSession> GetSessionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _sessions.Where(s => s.StartTime.Date >= startDate.Date && s.StartTime.Date <= endDate.Date).ToList();
        }

        public int GetTodaySessionCount()
        {
            return _sessions.Count(s => s.StartTime.Date == DateTime.Today && s.Type == SessionType.Focus);
        }

        public int GetTodayFocusMinutes()
        {
            return _sessions
                .Where(s => s.StartTime.Date == DateTime.Today && s.Type == SessionType.Focus && s.IsCompleted)
                .Sum(s => s.DurationMinutes);
        }

        public int GetWeekSessionCount()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            return _sessions.Count(s => s.StartTime.Date >= startOfWeek && s.Type == SessionType.Focus);
        }

        public int GetMonthSessionCount()
        {
            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return _sessions.Count(s => s.StartTime.Date >= startOfMonth && s.Type == SessionType.Focus);
        }

        public Dictionary<DateTime, int> GetSessionsGroupedByDay(int days)
        {
            var result = new Dictionary<DateTime, int>();
            var startDate = DateTime.Today.AddDays(-days);

            var groupedSessions = _sessions
                .Where(s => s.StartTime.Date >= startDate && s.Type == SessionType.Focus)
                .GroupBy(s => s.StartTime.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            for (int i = 0; i <= days; i++)
            {
                var date = startDate.AddDays(i);
                result[date] = groupedSessions.ContainsKey(date) ? groupedSessions[date] : 0;
            }

            return result;
        }

        public void ClearHistory()
        {
            _sessions.Clear();
            LocalStorage.Instance.SaveSessions(_sessions);
        }

        public void ClearOldSessions(int daysToKeep)
        {
            var cutoffDate = DateTime.Today.AddDays(-daysToKeep);
            _sessions.RemoveAll(s => s.StartTime.Date < cutoffDate);
            LocalStorage.Instance.SaveSessions(_sessions);
        }
    }
}
