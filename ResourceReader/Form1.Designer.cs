﻿using FormsUtils.Controls;

namespace ResourceReader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtbOutput = new DarkRichTextBox();
            btnFetchData = new DarkButton();
            tbMachineName = new DarkTextBox();
            btnDisplayAll = new DarkButton();
            btnDisplaySelected = new DarkButton();
            SuspendLayout();
            // 
            // rtbOutput
            // 
            rtbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbOutput.BackColor = Color.Black;
            rtbOutput.BorderColor = Color.FromArgb(50, 50, 50);
            rtbOutput.BorderStyle = BorderStyle.None;
            rtbOutput.Font = new Font("Arial", 9F);
            rtbOutput.ForeColor = Color.WhiteSmoke;
            rtbOutput.Location = new Point(239, 12);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(709, 531);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            rtbOutput.VScroll += RtbOutput_VScroll;
            // 
            // btnFetchData
            // 
            btnFetchData.BackColor = Color.Black;
            btnFetchData.BorderColor = Color.FromArgb(50, 50, 50);
            btnFetchData.FlatStyle = FlatStyle.Flat;
            btnFetchData.Font = new Font("Arial", 8F);
            btnFetchData.ForeColor = Color.WhiteSmoke;
            btnFetchData.Location = new Point(12, 12);
            btnFetchData.Size = new Size(204, 65);
            btnFetchData.TabIndex = 1;
            btnFetchData.Text = "Fetch Data";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += OnFetchData;
            // 
            // tbMachineName
            // 
            tbMachineName.BackColor = Color.Black;
            tbMachineName.BorderColor = Color.FromArgb(50, 50, 50);
            tbMachineName.BorderStyle = BorderStyle.FixedSingle;
            tbMachineName.Font = new Font("Arial", 9F);
            tbMachineName.ForeColor = Color.WhiteSmoke;
            tbMachineName.Location = new Point(12, 108);
            tbMachineName.Name = "tbMachineName";
            tbMachineName.Size = new Size(204, 25);
            tbMachineName.TabIndex = 2;
            // 
            // btnDisplayAll
            // 
            btnDisplayAll.BackColor = Color.Black;
            btnDisplayAll.BorderColor = Color.FromArgb(50, 50, 50);
            btnDisplayAll.FlatStyle = FlatStyle.Flat;
            btnDisplayAll.Font = new Font("Arial", 8F);
            btnDisplayAll.ForeColor = Color.WhiteSmoke;
            btnDisplayAll.Location = new Point(12, 478);
            btnDisplayAll.Size = new Size(204, 65);
            btnDisplayAll.TabIndex = 3;
            btnDisplayAll.Text = "Display All";
            btnDisplayAll.UseVisualStyleBackColor = true;
            btnDisplayAll.Click += OnDisplayAll_Click;
            // 
            // btnDisplaySelected
            // 
            btnDisplaySelected.BackColor = Color.Black;
            btnDisplaySelected.BorderColor = Color.FromArgb(50, 50, 50);
            btnDisplaySelected.FlatStyle = FlatStyle.Flat;
            btnDisplaySelected.Font = new Font("Arial", 8F);
            btnDisplaySelected.ForeColor = Color.WhiteSmoke;
            btnDisplaySelected.Location = new Point(12, 397);
            btnDisplaySelected.Size = new Size(204, 65);
            btnDisplaySelected.TabIndex = 4;
            btnDisplaySelected.Text = "Display Selected";
            btnDisplaySelected.UseVisualStyleBackColor = true;
            btnDisplaySelected.Click += OnDisplaySelected_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 555);
            Controls.Add(btnDisplaySelected);
            Controls.Add(btnDisplayAll);
            Controls.Add(tbMachineName);
            Controls.Add(btnFetchData);
            Controls.Add(rtbOutput);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkRichTextBox rtbOutput;
        private DarkButton btnFetchData;
        private DarkTextBox tbMachineName;
        private DarkButton btnDisplayAll;
        private DarkButton btnDisplaySelected;
    }
}
