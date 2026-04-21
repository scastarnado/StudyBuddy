using StudyBuddy.Data;
using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StudyBuddy.Services
{
    public class CalendarService
    {
        private List<CalendarEvent> _events;
        private static CalendarService _instance;

        public static CalendarService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CalendarService();
                }
                return _instance;
            }
        }

        public CalendarService()
        {
            _events = LocalStorage.Instance.LoadCalendarEvents();
        }

        public void AddEvent(CalendarEvent calendarEvent)
        {
            if (calendarEvent != null)
            {
                _events.Add(calendarEvent);
                LocalStorage.Instance.SaveCalendarEvents(_events);
            }
        }

        public void RemoveEvent(CalendarEvent calendarEvent)
        {
            _events.Remove(calendarEvent);
            LocalStorage.Instance.SaveCalendarEvents(_events);
        }

        public void RemoveEvent(string eventId)
        {
            var eventToRemove = _events.FirstOrDefault(e => e.Id == eventId);
            if (eventToRemove != null)
            {
                _events.Remove(eventToRemove);
                LocalStorage.Instance.SaveCalendarEvents(_events);
            }
        }

        public void UpdateEvent(CalendarEvent updatedEvent)
        {
            var existingEvent = _events.FirstOrDefault(e => e.Id == updatedEvent.Id);
            if (existingEvent != null)
            {
                int index = _events.IndexOf(existingEvent);
                _events[index] = updatedEvent;
                LocalStorage.Instance.SaveCalendarEvents(_events);
            }
        }

        public void CompleteEvent(string eventId)
        {
            var eventToComplete = _events.FirstOrDefault(e => e.Id == eventId);
            if (eventToComplete != null)
            {
                eventToComplete.IsCompleted = true;
                LocalStorage.Instance.SaveCalendarEvents(_events);
            }
        }

        public List<CalendarEvent> GetAllEvents()
        {
            return new List<CalendarEvent>(_events);
        }

        public List<CalendarEvent> GetEventsByDate(DateTime date)
        {
            return _events.Where(e => e.Date.Date == date.Date).OrderBy(e => e.Priority).ToList();
        }

        public List<CalendarEvent> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _events.Where(e => e.Date.Date >= startDate.Date && e.Date.Date <= endDate.Date)
                          .OrderBy(e => e.Date)
                          .ThenBy(e => e.Priority)
                          .ToList();
        }

        public List<CalendarEvent> GetUpcomingDeadlines(int days = 7)
        {
            var today = DateTime.Today;
            var futureDate = today.AddDays(days);
            return _events.Where(e => e.Type == EventType.Deadline && 
                                     e.Date.Date >= today && 
                                     e.Date.Date <= futureDate &&
                                     !e.IsCompleted)
                          .OrderBy(e => e.Date)
                          .ToList();
        }

        public List<CalendarEvent> GetTodayEvents()
        {
            return GetEventsByDate(DateTime.Today);
        }

        public Dictionary<DateTime, List<CalendarEvent>> GetEventsGroupedByDate(DateTime startDate, DateTime endDate)
        {
            var result = new Dictionary<DateTime, List<CalendarEvent>>();
            var events = GetEventsByDateRange(startDate, endDate);

            foreach (var evt in events)
            {
                if (!result.ContainsKey(evt.Date.Date))
                {
                    result[evt.Date.Date] = new List<CalendarEvent>();
                }
                result[evt.Date.Date].Add(evt);
            }

            return result;
        }

        public bool HasEventsOnDate(DateTime date)
        {
            return _events.Any(e => e.Date.Date == date.Date);
        }

        public Color GetDateDisplayColor(DateTime date)
        {
            var eventsOnDate = GetEventsByDate(date);
            if (!eventsOnDate.Any())
                return Color.White;

            // Priority order: Urgent > High > Deadline > others
            if (eventsOnDate.Any(e => e.Priority == EventPriority.Urgent))
                return Color.Red;
            if (eventsOnDate.Any(e => e.Priority == EventPriority.High))
                return Color.Orange;
            if (eventsOnDate.Any(e => e.Type == EventType.Deadline && !e.IsCompleted))
                return Color.Yellow;
            if (eventsOnDate.Any(e => e.Type == EventType.Exam))
                return Color.LightCoral;

            return eventsOnDate.First().DisplayColor;
        }

        public void ClearOldEvents(int daysToKeep)
        {
            var cutoffDate = DateTime.Today.AddDays(-daysToKeep);
            _events.RemoveAll(e => e.Date.Date < cutoffDate && e.IsCompleted);
            LocalStorage.Instance.SaveCalendarEvents(_events);
        }

        public void ClearAllEvents()
        {
            _events.Clear();
            LocalStorage.Instance.SaveCalendarEvents(_events);
        }

        public int GetEventCount()
        {
            return _events.Count;
        }

        public int GetUpcomingDeadlineCount(int days = 7)
        {
            return GetUpcomingDeadlines(days).Count;
        }
    }
}
