using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyBuddy
{
    public partial class Form1 : Form
    {
        #region Settings Variables

        static int StudySessionMinutes = 25;
        int ShortBreakMinutes = 5;
        int LongBreakMinutes = 15;
        int SessionsToLongBreak = 4;

        #endregion

        #region Timer Variables

        int StartingTime = StudySessionMinutes;
        int CurrentTime;
        int TotalSessions;
        int CurrentSession = 1;

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void StudySessionInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StudySessionMinutes = Int32.Parse(StudySessionInput.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Can't parse Study Session Minutes from settings.");
            }
        }

        private void AddSession_Click(object sender, EventArgs e)
        {
            string newSessionName = NewSessionName.Text.Trim();
            const int MaxSessionNameLength = 32;
            if (newSessionName.Length <= MaxSessionNameLength && newSessionName != "" && !SessionList.Items.Contains(newSessionName))
            {
                SessionList.Items.Add(newSessionName, CheckState.Unchecked);
                SessionList.Refresh();
            }
            else
            {
                MessageBox.Show("Session name is too long or already exists.");
            }

        }
    }
}
