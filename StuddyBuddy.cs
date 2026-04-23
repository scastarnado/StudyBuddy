using StudyBuddy.Services;
using StudyBuddy.Settings;
using StudyBuddy.UI;
using StudyBuddy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace StudyBuddy
{
    public partial class StuddyBuddy : Form
    {
        private static StuddyBuddy _instance;

        private TimerService _timerService;

        // Optimize: Use a static Random instance to avoid recreating it
        private static readonly Random _random = new Random();

        public Label timerLabel => this.TimerLabel;

        #region Settings Variables

        static int StudySessionMinutes;
        static int ShortBreakMinutes;
        static int LongBreakMinutes;
        static int SessionsToLongBreak;

        const int MAX_SESSION_NAME_LENGTH = 32;

        #endregion

        #region Timer Variables

        public enum TimerState
        {
            Idle,
            StudySession,
            ShortBreak,
            LongBreak
        }

        TimerState CurrentTimerState = TimerState.Idle;
        int CurrentTime;
        int CurrentSession = 1;
        
        // Optimize: Store timer values as integers instead of parsing from label
        private int _remainingMinutes;
        private int _remainingSeconds;

        #endregion

        #region Events

        public delegate void StudySessionFinishedEventHandler(TimerState CurrentTimerState);
        public event EventHandler StudySessionFinished;

        #endregion

        #region Banned Software and Websites Variables
        public static List<Process> BannedSoftware = new List<Process>();
        public static List<Process> BannedWebsites = new List<Process>();
        #endregion

        bool isRunning = false;
        bool isStopped = false;

        // TO-DO List Panel
        private TodoListPanel _todoListPanel;

        // Optimize: Cache motivational quotes array
        private static readonly string[] _motivationalQuotes = {
            "Success is the sum of small efforts, repeated day in and day out. - Robert Collier",
            "You are never too old to set another goal or to dream a new dream. - C.S. Lewis",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "Don't watch the clock; do what it does. Keep going. - Sam Levenson",
            "Your time is limited, don't waste it living someone else's life. - Steve Jobs",
            "The only way to do great work is to love what you do. - Steve Jobs",
            "Believe you can and you're halfway there. - Theodore Roosevelt",
            "The only place where success comes before work is in the dictionary. - Vidal Sassoon",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
            "Dream big and dare to fail. - Norman Vaughan",
            "The harder you work for something, the greater you'll feel when you achieve it. - Unknown",
            "You don't have to be great to start, but you have to start to be great. - Zig Ziglar",
            "Set your goals high, and don't stop till you get there. - Bo Jackson",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "Success is walking from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "Don't let what you cannot do interfere with what you can do. - John Wooden",
            "The secret of getting ahead is getting started. - Mark Twain",
            "The only way to achieve the impossible is to believe it is possible. - Charles Kingsleigh",
            "Challenges are what make life interesting and overcoming them is what makes life meaningful. - Joshua J. Marine",
            "You are braver than you believe, stronger than you seem, and smarter than you think. - A.A. Milne"
        };

        public StuddyBuddy()
        {
            InitializeComponent();

            var settings = SettingsManager.Instance.Load();
            HardStudyCheckBox.Checked = settings.EnableFocusBlocker;

            // Initialize TimerService with settings
            _timerService = new TimerService(settings);

            // Subscribe to timer events
            _timerService.TimerTick += OnTimerTick;
            _timerService.TimerCompleted += OnTimerCompleted;

            // Set the form reference in TimerControl singleton
            TimerControl.Instance.SetFormReference(this);

            // Initialize TO-DO List Panel
            InitializeTodoPanel();

            // Initialize Calendar features
            InitializeCalendar();

            // Initialize Banned Software/Website lists
            InitializeBannedLists();

            // Motivational Quotes Timer Stuff
            MotivationalQuotesTimer.Interval = 10000; // Set interval of 10 seconds. Guess its enough to read the quote(?)
            MotivationalQuotesTimer.Start(); // Start the timer at the beggining of the execution of the program
            MotivationalQuotesTimer.Tick += MotivationalQuotesTimer_Tick; // and each tiem it finishes, call the event to refresh the quote

            this.SessionList.AllowDrop = true;

            initializeSettingsVariables();

            SubscribeEvents();
        }

        public static StuddyBuddy Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new StuddyBuddy();
                }
                return _instance;
            }
        }

        public void SubscribeEvents()
        {
            StudySessionFinished += (sender, e) => OnStudySessionFinished(CurrentTimerState);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            int remainingSeconds = _timerService.GetRemainingSeconds();
            _remainingMinutes = remainingSeconds / 60;
            _remainingSeconds = remainingSeconds % 60;
            UpdateTimerDisplay();
        }

        private void OnTimerCompleted(object sender, EventArgs e)
        {
            StudySessionFinished?.Invoke(this, EventArgs.Empty);
        }

        public void OnStudySessionFinished(TimerState currentTimerState)
        {
            isRunning = false;

            // Stop focus blocker when session ends
            FocusBlockerService.Instance.StopBlocking();

            // Reset timer
            _remainingMinutes = 0;
            _remainingSeconds = 0;
            UpdateTimerDisplay();

            // Notify with a pop up and/or an audio alert
            MessageBox.Show("Study session finished! Time for a break!");

            // Update session count
            CurrentSession++;

            // If it is possible, underline or something the list of sessions that have been completed
            // Change the label of sessions - Optimize: Use string interpolation
            FocusSessionsLabel.Text = $"Focus Sessions ( {CurrentSession} / {SessionList.Items.Count} )";

            // Check if all sessions are completed
            if (CurrentSession > SessionList.Items.Count)
            {
                MessageBox.Show("Congratulations! All sessions completed!");
                CurrentSession = 1;
                CurrentTimerState = TimerState.Idle;
                StartStopBTN.Text = "START";
                BreakBTN.Enabled = false;
                return;
            }

            // Set the next state to be short or long break
            if (CurrentSession % SessionsToLongBreak == 0)
            {
                CurrentTimerState = TimerState.LongBreak;
                _remainingMinutes = LongBreakMinutes;
                _remainingSeconds = 0;
            }
            else
            {
                CurrentTimerState = TimerState.ShortBreak;
                _remainingMinutes = ShortBreakMinutes;
                _remainingSeconds = 0;
            }
            UpdateTimerDisplay();

            // Auto-start break if enabled
            var settings = SettingsManager.Instance.Load();
            if (settings.AutoStartNextSession)
            {
                _timerService.StartBreak(CurrentTimerState == TimerState.LongBreak);
                isRunning = true;
            }
        }

        void initializeSettingsVariables()
        {
            try
            {
                StudySessionMinutes = int.Parse(FocusTimeInput.Text);
                ShortBreakMinutes = int.Parse(ShortBreakInput.Text);
                LongBreakMinutes = int.Parse(LongBreakInput.Text);
                SessionsToLongBreak = int.Parse(SessionsToLongBreakInput.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Can't parse settings.");
            }
        }

        private void MotivationalQuotesTimer_Tick(object sender, EventArgs e)
        {
            PlayQuotes(); // Call PlayQuotes method on each tick to refresh the displayed quote
        }

        void PlayQuotes()
        {
            // Optimize: Use static Random instance and cached quotes array
            int randomIndex = _random.Next(_motivationalQuotes.Length);
            StudyQuotes.Text = _motivationalQuotes[randomIndex];
        }

        // Optimize: Helper method to update timer display from integer values
        private void UpdateTimerDisplay()
        {
            TimerLabel.Text = $"{_remainingMinutes:D2} : {_remainingSeconds:D2}";
        }

        private void AddSession_Click(object sender, EventArgs e)
        {
            string newSessionName = NewSessionName.Text.Trim();
            if (newSessionName.Length <= MAX_SESSION_NAME_LENGTH && newSessionName != "" && !SessionList.Items.Contains(newSessionName))
            {
                SessionList.Items.Add(newSessionName);
                // Optimize: Use string interpolation
                FocusSessionsLabel.Text = $"Focus Sessions ( 1 / {SessionList.Items.Count} )";
                SessionList.Refresh();
            }
            else
            {
                MessageBox.Show("Session name is too long or already exists.");
            }
        }
        private void DeleteSession_Click(object sender, EventArgs e)
        {
            string sessionToDelete = SessionList.SelectedItem.ToString();
            if (SessionList.Items.Contains(sessionToDelete))
            {
                SessionList.Items.Remove(sessionToDelete);
                SessionList.Refresh();
            }
            else
            {
                MessageBox.Show("Session name selected could not be found.");
            }
        }

        private void SessionList_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.SessionList.SelectedItem == null) return;
            this.SessionList.DoDragDrop(this.SessionList.SelectedItem, DragDropEffects.Move);
        }

        private void SessionList_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void SessionList_DragDrop(object sender, DragEventArgs e)
        {
            Point point = SessionList.PointToClient(new Point(e.X, e.Y));
            int index = this.SessionList.IndexFromPoint(point);
            if (index < 0) index = this.SessionList.Items.Count - 1;
            object data = e.Data.GetData(typeof(DateTime));
            this.SessionList.Items.Remove(data);
            this.SessionList.Items.Insert(index, data);
        }

        private void StartStopBTN_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                if (!isStopped && SessionList.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one session before starting the timer.");
                    return;
                }

                isRunning = true;

                if (isStopped)
                {
                    _timerService.Resume();
                    isStopped = false;
                }
                else
                {
                    CurrentTimerState = TimerState.StudySession;
                    _remainingMinutes = StudySessionMinutes;
                    _remainingSeconds = 0;
                    UpdateTimerDisplay();
                    _timerService.Start();
                }

                FocusBlockerService.Instance.StartBlocking(true);

                StartStopBTN.Text = "STOP";
                BreakBTN.Enabled = true;
            }
            else
            {
                // Stopping the session
                isRunning = false;
                _timerService.Stop();
                FocusBlockerService.Instance.StopBlocking();

                StartStopBTN.Text = "START";
                BreakBTN.Enabled = false;
            }
        }

        // TODO Implement the changes between focus and break periods
        private void FocusTimer_Tick(object sender, EventArgs e)
        {
            if (_remainingSeconds == 0)
            {
                if (_remainingMinutes == 0)
                {
                    FocusTimer.Stop();
                    StudySessionFinished?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("Time's up!");
                    return;
                }
                else
                {
                    _remainingMinutes--;
                    _remainingSeconds = 59;
                }
            }
            else
            {
                _remainingSeconds--;
            }

            UpdateTimerDisplay();
        }

        private void addBannedSoftwareButton_Click(object sender, EventArgs e)
        {
            using (ProcessForm processForm = new ProcessForm())
            {
                if (processForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadBannedSoftware();
                }
            }
        }

        private void SaveSettingsBTN_Click(object sender, EventArgs e)
        {
            try
            {
                // save settings to variables
                StudySessionMinutes = int.Parse(FocusTimeInput.Text);
                ShortBreakMinutes = int.Parse(ShortBreakInput.Text);
                LongBreakMinutes = int.Parse(LongBreakInput.Text);
                SessionsToLongBreak = int.Parse(SessionsToLongBreakInput.Text);

                // Save to settings manager
                var settings = SettingsManager.Instance.Load();
                settings.SessionMinutes = StudySessionMinutes;
                settings.ShortBreakMinutes = ShortBreakMinutes;
                settings.LongBreakMinutes = LongBreakMinutes;
                settings.SessionsBeforeLongBreak = SessionsToLongBreak;
                SettingsManager.Instance.Save(settings);

                // Update timer service with new settings
                _timerService = new TimerService(settings);
                _timerService.TimerTick += OnTimerTick;
                _timerService.TimerCompleted += OnTimerCompleted;

                // notify the user
                MessageBox.Show("Settings saved successfully!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for all settings.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private void BreakBTN_Click(object sender, EventArgs e)
        {
            if(!isRunning)
            {
                MessageBox.Show("You are not running any focus session...\n What are you trying to break? Huh?");
                return;
            }

            _timerService.Stop();
            FocusBlockerService.Instance.StopBlocking();

            isRunning = false;
            isStopped = true;

            BreakBTN.Enabled = isRunning;
            StartStopBTN.Enabled = !isRunning;

            StartStopBTN.Text = "RESUME";
        }

        #region TO-DO List Panel Methods

        private void InitializeTodoPanel()
        {
            _todoListPanel = new TodoListPanel
            {
                Location = new Point(8, 25),
                Size = new Size(360, 340)
            };
            panel5.Controls.Add(_todoListPanel);
        }

        #endregion

        #region Calendar Methods

        private void InitializeCalendar()
        {
            // Set calendar to bold dates with events
            monthCalendar1.DateChanged += MonthCalendar1_DateChanged;
            monthCalendar1.MouseDoubleClick += MonthCalendar1_MouseDoubleClick;

            // Add buttons for calendar management
            var addEventButton = new Button
            {
                Text = "Add Event",
                Location = new Point(10, 240),
                Size = new Size(100, 30)
            };
            addEventButton.Click += AddEventButton_Click;
            panel4.Controls.Add(addEventButton);

            var viewEventsButton = new Button
            {
                Text = "View Events",
                Location = new Point(120, 240),
                Size = new Size(100, 30)
            };
            viewEventsButton.Click += ViewEventsButton_Click;
            panel4.Controls.Add(viewEventsButton);

            var deadlinesButton = new Button
            {
                Text = "Deadlines",
                Location = new Point(230, 240),
                Size = new Size(100, 30)
            };
            deadlinesButton.Click += DeadlinesButton_Click;
            panel4.Controls.Add(deadlinesButton);

            UpdateCalendarDisplay();
        }

        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ShowEventsForSelectedDate();
        }

        private void MonthCalendar1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddEventButton_Click(sender, e);
        }

        private void AddEventButton_Click(object sender, EventArgs e)
        {
            var dialog = new CalendarEventDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                UpdateCalendarDisplay();
                MessageBox.Show("Event added successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ViewEventsButton_Click(object sender, EventArgs e)
        {
            ShowEventsForSelectedDate();
        }

        private void DeadlinesButton_Click(object sender, EventArgs e)
        {
            var upcomingDeadlines = CalendarService.Instance.GetUpcomingDeadlines(30);
            if (upcomingDeadlines.Count == 0)
            {
                MessageBox.Show("No upcoming deadlines in the next 30 days.", "Deadlines", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = "Upcoming Deadlines:\n\n";
            foreach (var deadline in upcomingDeadlines)
            {
                int daysUntil = (deadline.Date - DateTime.Today).Days;
                message += $"• {deadline.Title} - {deadline.Date:MMM dd} ({daysUntil} days)\n";
            }

            MessageBox.Show(message, "Upcoming Deadlines", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowEventsForSelectedDate()
        {
            var selectedDate = monthCalendar1.SelectionStart;
            var events = CalendarService.Instance.GetEventsByDate(selectedDate);

            if (events.Count == 0)
            {
                MessageBox.Show($"No events on {selectedDate:MMMM dd, yyyy}", "Events", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = $"Events on {selectedDate:MMMM dd, yyyy}:\n\n";
            foreach (var evt in events)
            {
                string status = evt.IsCompleted ? " ✓" : "";
                message += $"• [{evt.Type}] {evt.Title}{status}\n";
                if (!string.IsNullOrEmpty(evt.Description))
                {
                    message += $"  {evt.Description}\n";
                }
                message += "\n";
            }

            MessageBox.Show(message, "Events", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateCalendarDisplay()
        {
            // Get all events for the current month
            var firstDay = new DateTime(monthCalendar1.SelectionStart.Year, 
                monthCalendar1.SelectionStart.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            var events = CalendarService.Instance.GetEventsGroupedByDate(firstDay, lastDay);

            // Bold dates with events
            var boldDates = new List<DateTime>();
            foreach (var dateGroup in events.Keys)
            {
                boldDates.Add(dateGroup);
            }

            if (boldDates.Count > 0)
            {
                monthCalendar1.BoldedDates = boldDates.ToArray();
            }
        }

        #endregion

        #region Banned Software/Website Methods

        private void InitializeBannedLists()
        {
            LoadBannedSoftware();
            LoadBannedWebsites();

            // Wire up the remove buttons
            button1.Click += RemoveBannedSoftware_Click;
            button2.Click += RemoveBannedWebsite_Click;
            addBannedWebsiteButton.Click += AddBannedWebsite_Click;
        }

        private void LoadBannedSoftware()
        {
            bannedSoftwareList.Items.Clear();
            var blockedApps = FocusBlockerService.Instance.GetBlockedApplications();
            foreach (var app in blockedApps)
            {
                bannedSoftwareList.Items.Add(app);
            }
        }

        private void LoadBannedWebsites()
        {
            bannedWebsitesList.Items.Clear();
            var blockedSites = FocusBlockerService.Instance.GetBlockedWebsites();
            foreach (var site in blockedSites)
            {
                bannedWebsitesList.Items.Add(site);
            }
        }

        private void RemoveBannedSoftware_Click(object sender, EventArgs e)
        {
            if (bannedSoftwareList.SelectedItem != null)
            {
                string selectedApp = bannedSoftwareList.SelectedItem.ToString();
                FocusBlockerService.Instance.RemoveBlockedApplication(selectedApp);
                LoadBannedSoftware();
                MessageBox.Show($"Removed {selectedApp} from blocked applications.", "Success");
            }
        }

        private void RemoveBannedWebsite_Click(object sender, EventArgs e)
        {
            if (bannedWebsitesList.SelectedItem != null)
            {
                string selectedSite = bannedWebsitesList.SelectedItem.ToString();
                FocusBlockerService.Instance.RemoveBlockedWebsite(selectedSite);
                LoadBannedWebsites();
                MessageBox.Show($"Removed {selectedSite} from blocked websites.", "Success");
            }
        }

        private void AddBannedWebsite_Click(object sender, EventArgs e)
        {
            using (var inputForm = new Form())
            {
                inputForm.Text = "Add Blocked Website";
                inputForm.Size = new Size(350, 150);
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                var label = new Label
                {
                    Text = "Enter website to block (e.g., facebook.com):",
                    Location = new Point(10, 20),
                    AutoSize = true
                };

                var textBox = new TextBox
                {
                    Location = new Point(10, 45),
                    Size = new Size(310, 25)
                };

                var okButton = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(170, 75),
                    Size = new Size(75, 30)
                };

                var cancelButton = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(250, 75),
                    Size = new Size(75, 30)
                };

                inputForm.Controls.AddRange(new Control[] { label, textBox, okButton, cancelButton });
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                if (inputForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    string website = textBox.Text.Trim();
                    FocusBlockerService.Instance.AddBlockedWebsite(website);
                    LoadBannedWebsites();
                    MessageBox.Show($"Added {website} to blocked websites.\n\nNote: Website blocking requires administrator rights.", "Success");
                }
            }
        }

        #endregion
    }
}
