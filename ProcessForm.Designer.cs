namespace StudyBuddy
{
    partial class ProcessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.currentProcessLabel = new System.Windows.Forms.Label();
            this.currentProcessCheckboxList = new System.Windows.Forms.CheckedListBox();
            this.saveBannedSoftwares = new System.Windows.Forms.Button();
            this.openFileExplorerBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // currentProcessLabel
            // 
            this.currentProcessLabel.AutoSize = true;
            this.currentProcessLabel.Location = new System.Drawing.Point(12, 9);
            this.currentProcessLabel.Name = "currentProcessLabel";
            this.currentProcessLabel.Size = new System.Drawing.Size(136, 13);
            this.currentProcessLabel.TabIndex = 1;
            this.currentProcessLabel.Text = "Current Running Processes";
            // 
            // currentProcessCheckboxList
            // 
            this.currentProcessCheckboxList.FormattingEnabled = true;
            this.currentProcessCheckboxList.Location = new System.Drawing.Point(12, 25);
            this.currentProcessCheckboxList.Name = "currentProcessCheckboxList";
            this.currentProcessCheckboxList.Size = new System.Drawing.Size(331, 409);
            this.currentProcessCheckboxList.TabIndex = 2;
            // 
            // saveBannedSoftwares
            // 
            this.saveBannedSoftwares.Location = new System.Drawing.Point(349, 54);
            this.saveBannedSoftwares.Name = "saveBannedSoftwares";
            this.saveBannedSoftwares.Size = new System.Drawing.Size(136, 23);
            this.saveBannedSoftwares.TabIndex = 3;
            this.saveBannedSoftwares.Text = "Save Banned Softwares";
            this.saveBannedSoftwares.UseVisualStyleBackColor = true;
            this.saveBannedSoftwares.Click += new System.EventHandler(this.saveBannedSoftwares_Click);
            // 
            // openFileExplorerBTN
            // 
            this.openFileExplorerBTN.Location = new System.Drawing.Point(349, 25);
            this.openFileExplorerBTN.Name = "openFileExplorerBTN";
            this.openFileExplorerBTN.Size = new System.Drawing.Size(109, 23);
            this.openFileExplorerBTN.TabIndex = 4;
            this.openFileExplorerBTN.Text = "Open File Explorer";
            this.openFileExplorerBTN.UseVisualStyleBackColor = true;
            this.openFileExplorerBTN.Click += new System.EventHandler(this.openFileExplorerBTN_Click);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.openFileExplorerBTN);
            this.Controls.Add(this.saveBannedSoftwares);
            this.Controls.Add(this.currentProcessCheckboxList);
            this.Controls.Add(this.currentProcessLabel);
            this.Name = "ProcessForm";
            this.Text = "Ban Softwares";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label currentProcessLabel;
        private System.Windows.Forms.CheckedListBox currentProcessCheckboxList;
        private System.Windows.Forms.Button saveBannedSoftwares;
        private System.Windows.Forms.Button openFileExplorerBTN;
    }
}