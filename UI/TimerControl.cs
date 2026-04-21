using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public TimerControl() { }

        public TimerControl(StuddyBuddy formReference) 
        { 
            _formReference = formReference;
        }

        public void SetFormReference(StuddyBuddy formReference)
        {
            _formReference = formReference;
        }

        public void UpdateDisplay(int remainingSeconds) 
        {
            if (_formReference != null && _formReference.timerLabel != null)
            {
                int minutes = remainingSeconds / 60;
                int seconds = remainingSeconds % 60;
                string timeDisplay = $"{minutes:D2} : {seconds:D2}";

                // Ensure UI update is done on the UI thread
                if (_formReference.InvokeRequired)
                {
                    _formReference.Invoke(new Action(() => _formReference.timerLabel.Text = timeDisplay));
                }
                else
                {
                    _formReference.timerLabel.Text = timeDisplay;
                }
            }
        }

        public void ShowCompletionNotification(string message = "Time's up!") 
        {
            if (_formReference != null)
            {
                if (_formReference.InvokeRequired)
                {
                    _formReference.Invoke(new Action(() => MessageBox.Show(message, "StudyBuddy", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }
                else
                {
                    MessageBox.Show(message, "StudyBuddy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ResetDisplay() 
        {
            UpdateDisplay(0);
        }

        public void SetSessionType(string sessionType) 
        {
            // Future implementation: Update UI to show current session type
        }

        public void UpdateSessionCount(int currentSession, int totalSessions) 
        {
            if (_formReference != null && _formReference.InvokeRequired)
            {
                _formReference.Invoke(new Action(() => UpdateSessionCountInternal(currentSession, totalSessions)));
            }
            else
            {
                UpdateSessionCountInternal(currentSession, totalSessions);
            }
        }

        private void UpdateSessionCountInternal(int currentSession, int totalSessions)
        {
            // Future implementation: Update UI to show session progress
        }
    }
}
