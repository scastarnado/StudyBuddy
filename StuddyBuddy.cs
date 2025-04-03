using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyBuddy
{
    public partial class StuddyBuddy : Form
    {
        #region Settings Variables

        static int StudySessionMinutes;
        static int ShortBreakMinutes;
        static int LongBreakMinutes;
        static int SessionsToLongBreak;

        const int MAX_SESSION_NAME_LENGTH = 32;

        #endregion

        #region Timer Variables

        int StartingTime;
        int CurrentTime;
        int TotalSessions;
        int CurrentSession = 1;

        #endregion

        public StuddyBuddy()
        {
            InitializeComponent();

            // Motivational Quotes Timer Stuff
            MotivationalQuotesTimer.Interval = 10000; // Set interval of 10 seconds. Guess its enough to read the quote(?)
            MotivationalQuotesTimer.Start(); // Start the timer at the beggining of the execution of the program
            MotivationalQuotesTimer.Tick += MotivationalQuotesTimer_Tick; // and each tiem it finishes, call the event to refresh the quote

            this.SessionList.AllowDrop = true;

            initializeSettingsVariables();
        }

        void initializeSettingsVariables()
        {
            try
            {
                StudySessionMinutes = int.Parse(StudySessionInput.Text);
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
            // Will be adding more quotes as I can
            string[] motivationalQuotes = {
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
            Random rnd = new Random();
            int randomIndex = rnd.Next(motivationalQuotes.Length);
            StudyQuotes.Text = motivationalQuotes[randomIndex];
        }

        private void StudySessionInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StudySessionMinutes = int.Parse(StudySessionInput.Text);
                TimerLabel.Text = StudySessionMinutes + " : 00";
            }
            catch (FormatException)
            {
                MessageBox.Show("Can't parse Study Session Minutes from settings.");
            }
        }

        private void AddSession_Click(object sender, EventArgs e)
        {
            string newSessionName = NewSessionName.Text.Trim();
            if (newSessionName.Length <= MAX_SESSION_NAME_LENGTH && newSessionName != "" && !SessionList.Items.Contains(newSessionName))
            {
                SessionList.Items.Add(newSessionName, CheckState.Unchecked);
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
    }
}
