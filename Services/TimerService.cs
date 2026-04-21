using StudyBuddy.Settings;
using System;
using System.Windows.Forms;

namespace StudyBuddy.Services
{
    internal class TimerService
    {
        AppSettings _settings;

        private Timer timer;
        public event EventHandler TimerTick;
        public event EventHandler TimerCompleted;
        private int _remainingSeconds;

        public TimerService(AppSettings settings)
        {
            _settings = settings;
            timer = new Timer();
            timer.Interval = 1000; // Set the timer interval to 1 second
            timer.Tick += OnTimerTick;
        }

        public void Start()
        {
            _remainingSeconds = _settings.SessionMinutes * 60;
            timer.Start();
        }

        public void Resume()
        {
            if (_remainingSeconds <= 0)
            {
                _remainingSeconds = _settings.SessionMinutes * 60;
            }

            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Reset()
        {
            timer.Stop();
            _remainingSeconds = 0;
        }

        public void StartBreak(bool isLongBreak)
        {
            _remainingSeconds = isLongBreak ? _settings.LongBreakMinutes * 60 : _settings.ShortBreakMinutes * 60;
            timer.Start();
        }

        public int GetRemainingSeconds()
        {
            return _remainingSeconds;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                TimerTick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                timer.Stop();
                TimerCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
