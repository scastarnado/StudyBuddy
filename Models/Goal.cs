using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Models
{
    public class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; }
        public int TargetPomodoros { get; set; }
        public int CompletedPomodoros { get; set; }
        public GoalType Type { get; set; }

        public Goal()
        {
            CreatedDate = DateTime.Now;
            IsCompleted = false;
            Type = GoalType.Daily;
            TargetPomodoros = 8;
            CompletedPomodoros = 0;
        }

        public Goal(string name, GoalType type, int targetPomodoros, DateTime? targetDate = null)
        {
            Name = name;
            Type = type;
            TargetPomodoros = targetPomodoros;
            TargetDate = targetDate;
            CreatedDate = DateTime.Now;
            IsCompleted = false;
            CompletedPomodoros = 0;
        }

        public double GetProgress()
        {
            if (TargetPomodoros == 0)
                return 0;

            return (double)CompletedPomodoros / TargetPomodoros * 100;
        }

        public void Complete()
        {
            IsCompleted = true;
            CompletedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} ({CompletedPomodoros}/{TargetPomodoros})";
        }
    }

    public enum GoalType
    {
        Daily,
        Weekly,
        Monthly,
        Custom
    }
}
