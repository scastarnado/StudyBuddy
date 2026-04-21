using StudyBuddy.Models;
using StudyBuddy.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudyBuddy.UI
{
    public class CalendarEventDialog : Form
    {
        private TextBox titleTextBox;
        private TextBox descriptionTextBox;
        private DateTimePicker datePicker;
        private ComboBox eventTypeComboBox;
        private ComboBox priorityComboBox;
        private Button colorButton;
        private Button saveButton;
        private Button cancelButton;
        private Color selectedColor = Color.LightBlue;
        private CalendarEvent _event;

        public CalendarEventDialog(CalendarEvent existingEvent = null)
        {
            _event = existingEvent;
            InitializeComponents();
            if (existingEvent != null)
            {
                LoadEvent(existingEvent);
            }
        }

        private void InitializeComponents()
        {
            this.Text = _event == null ? "Add Event/Deadline" : "Edit Event";
            this.Size = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title Label
            var titleLabel = new Label
            {
                Text = "Title:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Title TextBox
            titleTextBox = new TextBox
            {
                Location = new Point(20, 45),
                Size = new Size(340, 25)
            };

            // Description Label
            var descLabel = new Label
            {
                Text = "Description:",
                Location = new Point(20, 80),
                AutoSize = true
            };

            // Description TextBox
            descriptionTextBox = new TextBox
            {
                Location = new Point(20, 105),
                Size = new Size(340, 60),
                Multiline = true
            };

            // Date Label
            var dateLabel = new Label
            {
                Text = "Date:",
                Location = new Point(20, 175),
                AutoSize = true
            };

            // Date Picker
            datePicker = new DateTimePicker
            {
                Location = new Point(20, 200),
                Size = new Size(200, 25),
                Format = DateTimePickerFormat.Short
            };

            // Event Type Label
            var typeLabel = new Label
            {
                Text = "Type:",
                Location = new Point(230, 175),
                AutoSize = true
            };

            // Event Type ComboBox
            eventTypeComboBox = new ComboBox
            {
                Location = new Point(230, 200),
                Size = new Size(130, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventTypeComboBox.Items.AddRange(new object[] 
            { 
                "General", "Deadline", "Exam", "Meeting", "Birthday", "Holiday", "Custom" 
            });
            eventTypeComboBox.SelectedIndex = 0;

            // Priority Label
            var priorityLabel = new Label
            {
                Text = "Priority:",
                Location = new Point(20, 235),
                AutoSize = true
            };

            // Priority ComboBox
            priorityComboBox = new ComboBox
            {
                Location = new Point(80, 232),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            priorityComboBox.Items.AddRange(new object[] { "Low", "Normal", "High", "Urgent" });
            priorityComboBox.SelectedIndex = 1;

            // Color Button
            colorButton = new Button
            {
                Text = "Choose Color",
                Location = new Point(200, 230),
                Size = new Size(160, 30),
                BackColor = selectedColor
            };
            colorButton.Click += ColorButton_Click;

            // Save Button
            saveButton = new Button
            {
                Text = "Save",
                Location = new Point(190, 275),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            saveButton.Click += SaveButton_Click;

            // Cancel Button
            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(280, 275),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                titleLabel, titleTextBox,
                descLabel, descriptionTextBox,
                dateLabel, datePicker,
                typeLabel, eventTypeComboBox,
                priorityLabel, priorityComboBox,
                colorButton,
                saveButton, cancelButton
            });

            this.AcceptButton = saveButton;
            this.CancelButton = cancelButton;
        }

        private void LoadEvent(CalendarEvent evt)
        {
            titleTextBox.Text = evt.Title;
            descriptionTextBox.Text = evt.Description;
            datePicker.Value = evt.Date;
            eventTypeComboBox.SelectedIndex = (int)evt.Type;
            priorityComboBox.SelectedIndex = (int)evt.Priority;
            selectedColor = evt.DisplayColor;
            colorButton.BackColor = selectedColor;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = selectedColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedColor = colorDialog.Color;
                    colorButton.BackColor = selectedColor;
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Please enter a title for the event.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            if (_event == null)
            {
                _event = new CalendarEvent();
            }

            _event.Title = titleTextBox.Text.Trim();
            _event.Description = descriptionTextBox.Text.Trim();
            _event.Date = datePicker.Value.Date;
            _event.Type = (EventType)eventTypeComboBox.SelectedIndex;
            _event.Priority = (EventPriority)priorityComboBox.SelectedIndex;
            _event.DisplayColor = selectedColor;

            if (_event.Id == null || _event.Id == Guid.Empty.ToString())
            {
                CalendarService.Instance.AddEvent(_event);
            }
            else
            {
                CalendarService.Instance.UpdateEvent(_event);
            }
        }

        public CalendarEvent GetEvent()
        {
            return _event;
        }
    }
}
