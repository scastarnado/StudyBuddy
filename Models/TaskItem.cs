using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Models
{
    public class TaskItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int EstimatedPomodoros { get; set; }
        public int CompletedPomodoros { get; set; }
        public TaskPriority Priority { get; set; }

        public TaskItem()
        {
            CreatedDate = DateTime.Now;
            IsCompleted = false;
            EstimatedPomodoros = 1;
            CompletedPomodoros = 0;
            Priority = TaskPriority.Medium;
        }

        public TaskItem(string name, string description = "", int estimatedPomodoros = 1)
        {
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
            IsCompleted = false;
            EstimatedPomodoros = estimatedPomodoros;
            CompletedPomodoros = 0;
            Priority = TaskPriority.Medium;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Urgent
    }
}
