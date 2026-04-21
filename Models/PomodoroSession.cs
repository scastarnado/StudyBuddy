using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Models
{
    public class PomodoroSession
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public SessionType Type { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }

        public PomodoroSession()
        {
            StartTime = DateTime.Now;
            IsCompleted = false;
            Type = SessionType.Focus;
        }

        public PomodoroSession(SessionType type, int durationMinutes, string taskName = "")
        {
            StartTime = DateTime.Now;
            Type = type;
            DurationMinutes = durationMinutes;
            TaskName = taskName;
            IsCompleted = false;
        }

        public void Complete()
        {
            EndTime = DateTime.Now;
            IsCompleted = true;
        }

        public TimeSpan GetActualDuration()
        {
            if (EndTime.HasValue)
            {
                return EndTime.Value - StartTime;
            }
            return TimeSpan.Zero;
        }
    }

    public enum SessionType
    {
        Focus,
        ShortBreak,
        LongBreak
    }
}
