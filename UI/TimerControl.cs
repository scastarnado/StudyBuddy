using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.UI
{
    public class TimerControl
    {
        private static TimerControl _instance;
        private StuddyBuddy _formReference;

        public static TimerControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimerControl();
                }
                return _instance;
            }
        }

        // Constructor que acepta referencia al formulario
        public TimerControl() { }
        
        public TimerControl(StuddyBuddy formReference) 
        { 
            _formReference = formReference;
        }

        public void UpdateDisplay(int remainingSeconds) 
        {
            if (_formReference != null)
            {
                int minutes = remainingSeconds / 60;
                int seconds = remainingSeconds % 60;
                string timeDisplay = $"{minutes:D2} : {seconds:D2}";

                _formReference.timerLabel.Text = timeDisplay;
            }
        }

        public void ShowCompletionNotification() { }

        public void ResetDisplay() { }

        public void SetSessionType(string sessionType) { }

        public void UpdateSessionCount(int currentSession, int totalSessions) { }
    }
}
