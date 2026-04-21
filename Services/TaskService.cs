using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Services
{
    public class TaskService
    {
        private List<TaskItem> _tasks;
        private static TaskService _instance;

        public static TaskService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskService();
                }
                return _instance;
            }
        }

        public TaskService()
        {
            _tasks = new List<TaskItem>();
        }

        public void AddTask(TaskItem task)
        {
            if (task != null && !string.IsNullOrWhiteSpace(task.Name))
            {
                _tasks.Add(task);
            }
        }

        public void RemoveTask(TaskItem task)
        {
            _tasks.Remove(task);
        }

        public void RemoveTaskAt(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks.RemoveAt(index);
            }
        }

        public List<TaskItem> GetAllTasks()
        {
            return new List<TaskItem>(_tasks);
        }

        public TaskItem GetTask(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                return _tasks[index];
            }
            return null;
        }

        public void UpdateTask(int index, TaskItem updatedTask)
        {
            if (index >= 0 && index < _tasks.Count && updatedTask != null)
            {
                _tasks[index] = updatedTask;
            }
        }

        public void CompleteTask(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks[index].IsCompleted = true;
                _tasks[index].CompletedDate = DateTime.Now;
            }
        }

        public void ClearCompletedTasks()
        {
            _tasks.RemoveAll(t => t.IsCompleted);
        }

        public int GetTaskCount()
        {
            return _tasks.Count;
        }

        public int GetCompletedTaskCount()
        {
            return _tasks.Count(t => t.IsCompleted);
        }

        public int GetPendingTaskCount()
        {
            return _tasks.Count(t => !t.IsCompleted);
        }
    }
}
