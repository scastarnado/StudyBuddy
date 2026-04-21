using StudyBuddy.Models;
using StudyBuddy.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudyBuddy.UI
{
    public partial class TodoListPanel : UserControl
    {
        private ListBox todoListBox;
        private TextBox newTodoTextBox;
        private Button addTodoButton;
        private Button deleteTodoButton;
        private Button completeTodoButton;
        private ComboBox priorityComboBox;
        private Label titleLabel;
        private Label statsLabel;

        public TodoListPanel()
        {
            InitializeComponents();
            LoadTodos();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(360, 340);
            this.BorderStyle = BorderStyle.FixedSingle;

            // Title
            titleLabel = new Label
            {
                Text = "TO-DO LIST",
                Location = new Point(5, 5),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Stats Label
            statsLabel = new Label
            {
                Text = "Pending: 0 | Completed: 0",
                Location = new Point(5, 25),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            // TodoListBox
            todoListBox = new ListBox
            {
                Location = new Point(10, 50),
                Size = new Size(335, 190),
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 20
            };
            todoListBox.DrawItem += TodoListBox_DrawItem;
            todoListBox.SelectedIndexChanged += TodoListBox_SelectedIndexChanged;

            // Priority ComboBox
            priorityComboBox = new ComboBox
            {
                Location = new Point(10, 245),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            priorityComboBox.Items.AddRange(new object[] { "Low", "Medium", "High", "Urgent" });
            priorityComboBox.SelectedIndex = 1;

            // New Todo TextBox
            newTodoTextBox = new TextBox
            {
                Location = new Point(115, 245),
                Size = new Size(230, 25),
                Text = "Enter new task..."
            };
            newTodoTextBox.GotFocus += (s, e) =>
            {
                if (newTodoTextBox.Text == "Enter new task...")
                    newTodoTextBox.Text = "";
            };
            newTodoTextBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(newTodoTextBox.Text))
                    newTodoTextBox.Text = "Enter new task...";
            };
            newTodoTextBox.KeyPress += NewTodoTextBox_KeyPress;

            // Add Button
            addTodoButton = new Button
            {
                Text = "Add",
                Location = new Point(10, 275),
                Size = new Size(80, 30)
            };
            addTodoButton.Click += AddTodoButton_Click;

            // Complete Button
            completeTodoButton = new Button
            {
                Text = "Complete",
                Location = new Point(95, 275),
                Size = new Size(80, 30)
            };
            completeTodoButton.Click += CompleteTodoButton_Click;

            // Delete Button
            deleteTodoButton = new Button
            {
                Text = "Delete",
                Location = new Point(180, 275),
                Size = new Size(80, 30)
            };
            deleteTodoButton.Click += DeleteTodoButton_Click;

            // Add controls
            this.Controls.Add(titleLabel);
            this.Controls.Add(statsLabel);
            this.Controls.Add(todoListBox);
            this.Controls.Add(priorityComboBox);
            this.Controls.Add(newTodoTextBox);
            this.Controls.Add(addTodoButton);
            this.Controls.Add(completeTodoButton);
            this.Controls.Add(deleteTodoButton);
        }

        private void TodoListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            
            var task = todoListBox.Items[e.Index] as TaskItem;
            if (task != null)
            {
                Brush textBrush = task.IsCompleted ? Brushes.Gray : Brushes.Black;
                Font font = task.IsCompleted ? 
                    new Font(e.Font, FontStyle.Strikeout) : 
                    e.Font;

                // Priority color indicator
                Color priorityColor = GetPriorityColor(task.Priority);
                using (var priorityBrush = new SolidBrush(priorityColor))
                {
                    e.Graphics.FillRectangle(priorityBrush, e.Bounds.X, e.Bounds.Y, 5, e.Bounds.Height);
                }

                string displayText = task.Name;
                if (task.EstimatedPomodoros > 0)
                {
                    displayText += $" ({task.CompletedPomodoros}/{task.EstimatedPomodoros} 🍅)";
                }

                e.Graphics.DrawString(displayText, font, textBrush, e.Bounds.X + 8, e.Bounds.Y + 2);
            }

            e.DrawFocusRectangle();
        }

        private Color GetPriorityColor(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.Urgent: return Color.Red;
                case TaskPriority.High: return Color.Orange;
                case TaskPriority.Medium: return Color.Yellow;
                case TaskPriority.Low: return Color.Green;
                default: return Color.Gray;
            }
        }

        private void TodoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = todoListBox.SelectedIndex >= 0;
            completeTodoButton.Enabled = hasSelection;
            deleteTodoButton.Enabled = hasSelection;
        }

        private void NewTodoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                AddTodoButton_Click(sender, e);
            }
        }

        private void AddTodoButton_Click(object sender, EventArgs e)
        {
            string taskName = newTodoTextBox.Text.Trim();
            if (string.IsNullOrEmpty(taskName) || taskName == "Enter new task...") return;

            var priority = (TaskPriority)priorityComboBox.SelectedIndex;
            var task = new TaskItem(taskName, "", 1)
            {
                Priority = priority
            };

            TaskService.Instance.AddTask(task);
            todoListBox.Items.Add(task);

            newTodoTextBox.Text = "Enter new task...";
            newTodoTextBox.Focus();
            UpdateStats();
        }

        private void CompleteTodoButton_Click(object sender, EventArgs e)
        {
            if (todoListBox.SelectedIndex < 0) return;

            var task = todoListBox.SelectedItem as TaskItem;
            if (task != null)
            {
                int index = TaskService.Instance.GetAllTasks().IndexOf(task);
                TaskService.Instance.CompleteTask(index);
                todoListBox.Refresh();
                UpdateStats();
            }
        }

        private void DeleteTodoButton_Click(object sender, EventArgs e)
        {
            if (todoListBox.SelectedIndex < 0) return;

            var task = todoListBox.SelectedItem as TaskItem;
            if (task != null)
            {
                TaskService.Instance.RemoveTask(task);
                todoListBox.Items.Remove(task);
                UpdateStats();
            }
        }

        private void LoadTodos()
        {
            todoListBox.Items.Clear();
            var tasks = TaskService.Instance.GetAllTasks();
            foreach (var task in tasks)
            {
                todoListBox.Items.Add(task);
            }
            UpdateStats();
        }

        private void UpdateStats()
        {
            int pending = TaskService.Instance.GetPendingTaskCount();
            int completed = TaskService.Instance.GetCompletedTaskCount();
            statsLabel.Text = $"Pending: {pending} | Completed: {completed}";
        }

        public void RefreshList()
        {
            LoadTodos();
        }
    }
}
