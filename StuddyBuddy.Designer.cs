namespace StudyBuddy
{
    partial class StuddyBuddy
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.StartStopBTN = new System.Windows.Forms.Button();
            this.BreakBTN = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.StudyQuotes = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.addBannedWebsiteButton = new System.Windows.Forms.Button();
            this.addBannedSoftwareButton = new System.Windows.Forms.Button();
            this.bannedWebsitesList = new System.Windows.Forms.ListBox();
            this.bannedSoftwareList = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SessionList = new System.Windows.Forms.ListBox();
            this.DeleteSession = new System.Windows.Forms.Button();
            this.AddSession = new System.Windows.Forms.Button();
            this.NewSessionName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ClockLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.HardStudyCheckBox = new System.Windows.Forms.CheckBox();
            this.FocusSessionsLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SaveSettingsBTN = new System.Windows.Forms.Button();
            this.SessionsToLongBreakInput = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LongBreakInput = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ShortBreakInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FocusTimeInput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.MotivationalQuotesTimer = new System.Windows.Forms.Timer(this.components);
            this.FocusTimer = new System.Windows.Forms.Timer(this.components);
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.Location = new System.Drawing.Point(58, 241);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(343, 108);
            this.TimerLabel.TabIndex = 2;
            this.TimerLabel.Text = "00 : 00";
            // 
            // StartStopBTN
            // 
            this.StartStopBTN.Location = new System.Drawing.Point(268, 556);
            this.StartStopBTN.Name = "StartStopBTN";
            this.StartStopBTN.Size = new System.Drawing.Size(177, 56);
            this.StartStopBTN.TabIndex = 1;
            this.StartStopBTN.Text = "START";
            this.StartStopBTN.UseVisualStyleBackColor = true;
            this.StartStopBTN.Click += new System.EventHandler(this.StartStopBTN_Click);
            // 
            // BreakBTN
            // 
            this.BreakBTN.Enabled = false;
            this.BreakBTN.Location = new System.Drawing.Point(6, 556);
            this.BreakBTN.Name = "BreakBTN";
            this.BreakBTN.Size = new System.Drawing.Size(167, 56);
            this.BreakBTN.TabIndex = 0;
            this.BreakBTN.Text = "BREAK";
            this.BreakBTN.UseVisualStyleBackColor = true;
            this.BreakBTN.Click += new System.EventHandler(this.BreakBTN_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.monthCalendar1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(849, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(403, 343);
            this.panel4.TabIndex = 3;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(5, 26);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "CALENDAR";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(12, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(366, 360);
            this.panel5.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "SESSION TO DO";
            // 
            // StudyQuotes
            // 
            this.StudyQuotes.AutoSize = true;
            this.StudyQuotes.Location = new System.Drawing.Point(512, 9);
            this.StudyQuotes.Name = "StudyQuotes";
            this.StudyQuotes.Size = new System.Drawing.Size(159, 13);
            this.StudyQuotes.TabIndex = 5;
            this.StudyQuotes.Text = "I think you have to study my boy";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.button2);
            this.panel7.Controls.Add(this.button1);
            this.panel7.Controls.Add(this.addBannedWebsiteButton);
            this.panel7.Controls.Add(this.addBannedSoftwareButton);
            this.panel7.Controls.Add(this.bannedWebsitesList);
            this.panel7.Controls.Add(this.bannedSoftwareList);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Location = new System.Drawing.Point(849, 378);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(403, 291);
            this.panel7.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(203, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Remove Website";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Remove Software";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // addBannedWebsiteButton
            // 
            this.addBannedWebsiteButton.Location = new System.Drawing.Point(203, 237);
            this.addBannedWebsiteButton.Name = "addBannedWebsiteButton";
            this.addBannedWebsiteButton.Size = new System.Drawing.Size(99, 23);
            this.addBannedWebsiteButton.TabIndex = 4;
            this.addBannedWebsiteButton.Text = "Add Website";
            this.addBannedWebsiteButton.UseVisualStyleBackColor = true;
            // 
            // addBannedSoftwareButton
            // 
            this.addBannedSoftwareButton.Location = new System.Drawing.Point(7, 235);
            this.addBannedSoftwareButton.Name = "addBannedSoftwareButton";
            this.addBannedSoftwareButton.Size = new System.Drawing.Size(99, 23);
            this.addBannedSoftwareButton.TabIndex = 3;
            this.addBannedSoftwareButton.Text = "Add Software";
            this.addBannedSoftwareButton.UseVisualStyleBackColor = true;
            this.addBannedSoftwareButton.Click += new System.EventHandler(this.addBannedSoftwareButton_Click);
            // 
            // bannedWebsitesList
            // 
            this.bannedWebsitesList.FormattingEnabled = true;
            this.bannedWebsitesList.Location = new System.Drawing.Point(203, 21);
            this.bannedWebsitesList.Name = "bannedWebsitesList";
            this.bannedWebsitesList.Size = new System.Drawing.Size(195, 212);
            this.bannedWebsitesList.TabIndex = 2;
            // 
            // bannedSoftwareList
            // 
            this.bannedSoftwareList.FormattingEnabled = true;
            this.bannedSoftwareList.Location = new System.Drawing.Point(7, 21);
            this.bannedSoftwareList.Name = "bannedSoftwareList";
            this.bannedSoftwareList.Size = new System.Drawing.Size(190, 212);
            this.bannedSoftwareList.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "BANNED SOFTWARE";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SessionList);
            this.panel2.Controls.Add(this.DeleteSession);
            this.panel2.Controls.Add(this.AddSession);
            this.panel2.Controls.Add(this.NewSessionName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 378);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 291);
            this.panel2.TabIndex = 7;
            // 
            // SessionList
            // 
            this.SessionList.FormattingEnabled = true;
            this.SessionList.Location = new System.Drawing.Point(13, 21);
            this.SessionList.Name = "SessionList";
            this.SessionList.Size = new System.Drawing.Size(339, 212);
            this.SessionList.TabIndex = 5;
            // 
            // DeleteSession
            // 
            this.DeleteSession.Location = new System.Drawing.Point(94, 263);
            this.DeleteSession.Name = "DeleteSession";
            this.DeleteSession.Size = new System.Drawing.Size(75, 23);
            this.DeleteSession.TabIndex = 4;
            this.DeleteSession.Text = "Delete";
            this.DeleteSession.UseVisualStyleBackColor = true;
            this.DeleteSession.Click += new System.EventHandler(this.DeleteSession_Click);
            // 
            // AddSession
            // 
            this.AddSession.Location = new System.Drawing.Point(13, 263);
            this.AddSession.Name = "AddSession";
            this.AddSession.Size = new System.Drawing.Size(75, 23);
            this.AddSession.TabIndex = 3;
            this.AddSession.Text = "Add Session";
            this.AddSession.UseVisualStyleBackColor = true;
            this.AddSession.Click += new System.EventHandler(this.AddSession_Click);
            // 
            // NewSessionName
            // 
            this.NewSessionName.Location = new System.Drawing.Point(13, 237);
            this.NewSessionName.Name = "NewSessionName";
            this.NewSessionName.Size = new System.Drawing.Size(339, 20);
            this.NewSessionName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "SESSIONS";
            // 
            // ClockLabel
            // 
            this.ClockLabel.AutoSize = true;
            this.ClockLabel.Location = new System.Drawing.Point(1217, 9);
            this.ClockLabel.Name = "ClockLabel";
            this.ClockLabel.Size = new System.Drawing.Size(34, 13);
            this.ClockLabel.TabIndex = 8;
            this.ClockLabel.Text = "11:16";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(384, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(459, 644);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.HardStudyCheckBox);
            this.tabPage1.Controls.Add(this.FocusSessionsLabel);
            this.tabPage1.Controls.Add(this.TimerLabel);
            this.tabPage1.Controls.Add(this.StartStopBTN);
            this.tabPage1.Controls.Add(this.BreakBTN);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(451, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Timer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // HardStudyCheckBox
            // 
            this.HardStudyCheckBox.AutoSize = true;
            this.HardStudyCheckBox.Location = new System.Drawing.Point(9, 25);
            this.HardStudyCheckBox.Name = "HardStudyCheckBox";
            this.HardStudyCheckBox.Size = new System.Drawing.Size(79, 17);
            this.HardStudyCheckBox.TabIndex = 4;
            this.HardStudyCheckBox.Text = "Hard Study";
            this.HardStudyCheckBox.UseVisualStyleBackColor = true;
            // 
            // FocusSessionsLabel
            // 
            this.FocusSessionsLabel.AutoSize = true;
            this.FocusSessionsLabel.Location = new System.Drawing.Point(6, 9);
            this.FocusSessionsLabel.Name = "FocusSessionsLabel";
            this.FocusSessionsLabel.Size = new System.Drawing.Size(119, 13);
            this.FocusSessionsLabel.TabIndex = 3;
            this.FocusSessionsLabel.Text = "Focus Sessions ( 0 / 0 )";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SaveSettingsBTN);
            this.tabPage2.Controls.Add(this.SessionsToLongBreakInput);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.LongBreakInput);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.ShortBreakInput);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.FocusTimeInput);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(451, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SaveSettingsBTN
            // 
            this.SaveSettingsBTN.Location = new System.Drawing.Point(274, 305);
            this.SaveSettingsBTN.Name = "SaveSettingsBTN";
            this.SaveSettingsBTN.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsBTN.TabIndex = 8;
            this.SaveSettingsBTN.Text = "Save";
            this.SaveSettingsBTN.UseVisualStyleBackColor = true;
            this.SaveSettingsBTN.Click += new System.EventHandler(this.SaveSettingsBTN_Click);
            // 
            // SessionsToLongBreakInput
            // 
            this.SessionsToLongBreakInput.Location = new System.Drawing.Point(179, 85);
            this.SessionsToLongBreakInput.Name = "SessionsToLongBreakInput";
            this.SessionsToLongBreakInput.Size = new System.Drawing.Size(177, 20);
            this.SessionsToLongBreakInput.TabIndex = 7;
            this.SessionsToLongBreakInput.Text = "3";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "How many sessions for long break";
            // 
            // LongBreakInput
            // 
            this.LongBreakInput.Location = new System.Drawing.Point(179, 59);
            this.LongBreakInput.Name = "LongBreakInput";
            this.LongBreakInput.Size = new System.Drawing.Size(177, 20);
            this.LongBreakInput.TabIndex = 5;
            this.LongBreakInput.Text = "15";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Long Break";
            // 
            // ShortBreakInput
            // 
            this.ShortBreakInput.Location = new System.Drawing.Point(179, 33);
            this.ShortBreakInput.Name = "ShortBreakInput";
            this.ShortBreakInput.Size = new System.Drawing.Size(177, 20);
            this.ShortBreakInput.TabIndex = 3;
            this.ShortBreakInput.Text = "5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Short Break";
            // 
            // FocusTimeInput
            // 
            this.FocusTimeInput.Location = new System.Drawing.Point(179, 7);
            this.FocusTimeInput.Name = "FocusTimeInput";
            this.FocusTimeInput.Size = new System.Drawing.Size(177, 20);
            this.FocusTimeInput.TabIndex = 1;
            this.FocusTimeInput.Text = "25";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Focus Time";
            // 
            // MotivationalQuotesTimer
            // 
            this.MotivationalQuotesTimer.Interval = 10000;
            // 
            // FocusTimer
            // 
            this.FocusTimer.Interval = 1000;
            this.FocusTimer.Tick += new System.EventHandler(this.FocusTimer_Tick);
            // 
            // StuddyBuddy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ClockLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.StudyQuotes);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Name = "StuddyBuddy";
            this.Text = "StudyBuddy";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Button StartStopBTN;
        private System.Windows.Forms.Button BreakBTN;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label StudyQuotes;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ClockLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox SessionsToLongBreakInput;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox LongBreakInput;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ShortBreakInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FocusTimeInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label FocusSessionsLabel;
        private System.Windows.Forms.CheckBox HardStudyCheckBox;
        private System.Windows.Forms.Button DeleteSession;
        private System.Windows.Forms.Button AddSession;
        private System.Windows.Forms.TextBox NewSessionName;
        private System.Windows.Forms.Timer MotivationalQuotesTimer;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Timer FocusTimer;
        private System.Windows.Forms.ListBox bannedWebsitesList;
        private System.Windows.Forms.ListBox bannedSoftwareList;
        private System.Windows.Forms.Button addBannedWebsiteButton;
        private System.Windows.Forms.Button addBannedSoftwareButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox SessionList;
        private System.Windows.Forms.Button SaveSettingsBTN;
    }
}

