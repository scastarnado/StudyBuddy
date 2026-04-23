using StudyBuddy.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace StudyBuddy
{
    public partial class ProcessForm : Form
    {
        private sealed class ProcessListItem
        {
            public string DisplayName { get; set; }
            public string ProcessName { get; set; }

            public override string ToString()
            {
                return DisplayName;
            }
        }

        private List<Process> processes = new List<Process>();

        public ProcessForm()
        {
            InitializeComponent();

            LoadRunningProcesses();
        }

        private void LoadRunningProcesses()
        {
            currentProcessCheckboxList.Items.Clear();

            processes = Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero)
                .ToList();

            var processItems = new List<ProcessListItem>();

            foreach (var process in processes)
            {
                string windowTitle = process.MainWindowTitle == null ? string.Empty : process.MainWindowTitle.Trim();
                string processName = process.ProcessName;

                if (string.IsNullOrWhiteSpace(windowTitle))
                {
                    continue;
                }

                string displayName = string.Equals(windowTitle, processName, StringComparison.OrdinalIgnoreCase)
                    ? windowTitle
                    : string.Format("{0} ({1})", windowTitle, processName);

                processItems.Add(new ProcessListItem
                {
                    DisplayName = displayName,
                    ProcessName = processName
                });
            }

            foreach (var item in processItems
                .GroupBy(p => new { p.DisplayName, p.ProcessName })
                .Select(g => g.First())
                .OrderBy(p => p.DisplayName))
            {
                currentProcessCheckboxList.Items.Add(item);
            }
        }

        private void openFileExplorerBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "(*.exe)|*.exe";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    Process.Start(fileName);
                }
            }
        }

        private void saveBannedSoftwares_Click(object sender, EventArgs e)
        {
            foreach (ProcessListItem selectedItem in currentProcessCheckboxList.CheckedItems.Cast<object>().OfType<ProcessListItem>())
            {
                FocusBlockerService.Instance.AddBlockedApplication(selectedItem.ProcessName);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
