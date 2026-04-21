using System;
using System.Drawing;

namespace StudyBuddy.Models
{
    public class CalendarEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public Color DisplayColor { get; set; }
        public bool IsCompleted { get; set; }
        public EventPriority Priority { get; set; }

        public CalendarEvent()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Today;
            Type = EventType.General;
            DisplayColor = Color.LightBlue;
            IsCompleted = false;
            Priority = EventPriority.Normal;
        }

        public CalendarEvent(string title, DateTime date, EventType type, Color color)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Date = date;
            Type = type;
            DisplayColor = color;
            IsCompleted = false;
            Priority = EventPriority.Normal;
        }

        public bool IsDeadline => Type == EventType.Deadline;
        
        public bool IsImportant => Priority == EventPriority.High || Priority == EventPriority.Urgent;

        public override string ToString()
        {
            return $"{Title} - {Date:yyyy-MM-dd}";
        }
    }

    public enum EventType
    {
        General,
        Deadline,
        Exam,
        Meeting,
        Birthday,
        Holiday,
        Custom
    }

    public enum EventPriority
    {
        Low,
        Normal,
        High,
        Urgent
    }
}
