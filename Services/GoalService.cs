using StudyBuddy.Data;
using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyBuddy.Services
{
    public class GoalService
    {
        private List<Goal> _goals;
        private static GoalService _instance;

        public static GoalService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GoalService();
                }
                return _instance;
            }
        }

        public GoalService()
        {
            _goals = LocalStorage.Instance.LoadGoals();
        }

        public void AddGoal(Goal goal)
        {
            if (goal != null)
            {
                _goals.Add(goal);
                LocalStorage.Instance.SaveGoals(_goals);
            }
        }

        public void RemoveGoal(Goal goal)
        {
            _goals.Remove(goal);
            LocalStorage.Instance.SaveGoals(_goals);
        }

        public void UpdateGoal(int index, Goal updatedGoal)
        {
            if (index >= 0 && index < _goals.Count && updatedGoal != null)
            {
                _goals[index] = updatedGoal;
                LocalStorage.Instance.SaveGoals(_goals);
            }
        }

        public void CompleteGoal(int index)
        {
            if (index >= 0 && index < _goals.Count)
            {
                _goals[index].Complete();
                LocalStorage.Instance.SaveGoals(_goals);
            }
        }

        public List<Goal> GetAllGoals()
        {
            return new List<Goal>(_goals);
        }

        public List<Goal> GetActiveGoals()
        {
            return _goals.Where(g => !g.IsCompleted).ToList();
        }

        public List<Goal> GetCompletedGoals()
        {
            return _goals.Where(g => g.IsCompleted).ToList();
        }

        public List<Goal> GetGoalsByType(GoalType type)
        {
            return _goals.Where(g => g.Type == type && !g.IsCompleted).ToList();
        }

        public Goal GetTodayGoal()
        {
            return _goals.FirstOrDefault(g => 
                g.Type == GoalType.Daily && 
                g.CreatedDate.Date == DateTime.Today && 
                !g.IsCompleted);
        }

        public Goal GetCurrentWeekGoal()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            return _goals.FirstOrDefault(g => 
                g.Type == GoalType.Weekly && 
                g.CreatedDate.Date >= startOfWeek && 
                !g.IsCompleted);
        }

        public Goal GetCurrentMonthGoal()
        {
            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return _goals.FirstOrDefault(g => 
                g.Type == GoalType.Monthly && 
                g.CreatedDate.Date >= startOfMonth && 
                !g.IsCompleted);
        }

        public void IncrementGoalProgress(GoalType type, int pomodoros = 1)
        {
            Goal goal = null;
            
            switch (type)
            {
                case GoalType.Daily:
                    goal = GetTodayGoal();
                    break;
                case GoalType.Weekly:
                    goal = GetCurrentWeekGoal();
                    break;
                case GoalType.Monthly:
                    goal = GetCurrentMonthGoal();
                    break;
            }

            if (goal != null)
            {
                goal.CompletedPomodoros += pomodoros;
                
                if (goal.CompletedPomodoros >= goal.TargetPomodoros)
                {
                    goal.Complete();
                }
                
                LocalStorage.Instance.SaveGoals(_goals);
            }
        }

        public void CreateDefaultDailyGoal()
        {
            if (GetTodayGoal() == null)
            {
                var goal = new Goal("Daily Focus Goal", GoalType.Daily, 8, DateTime.Today);
                AddGoal(goal);
            }
        }

        public void ClearCompletedGoals()
        {
            _goals.RemoveAll(g => g.IsCompleted);
            LocalStorage.Instance.SaveGoals(_goals);
        }
    }
}
