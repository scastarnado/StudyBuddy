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
        private List<Process> processes = new List<Process>();

        public ProcessForm()
        {
            InitializeComponent();

            // Fix: Add a valid condition to filter processes with a MainWindowHandle that is not IntPtr.Zero  
            processes = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero).ToList();
            foreach (var process in processes)
            {
                if (process.MainWindowTitle.Trim() != "")
                    currentProcessCheckboxList.Items.Add(process.ProcessName);
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
            StuddyBuddy.BannedSoftware.AddRange(currentProcessCheckboxList.CheckedItems.Cast<string>()
                            .Select(name => Process.GetProcessesByName(name))
                            .SelectMany(p => p)
                            .ToList());
        }
    }
}
