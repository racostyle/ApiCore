using FormsUtils.Controls;

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
            btnSortBymachineName = new DarkButton();
            tbMachineName = new DarkTextBox();
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
            // btnSortBymachineName
            // 
            btnSortBymachineName.BackColor = Color.Black;
            btnSortBymachineName.BorderColor = Color.FromArgb(50, 50, 50);
            btnSortBymachineName.FlatStyle = FlatStyle.Flat;
            btnSortBymachineName.Font = new Font("Arial", 8F);
            btnSortBymachineName.ForeColor = Color.WhiteSmoke;
            btnSortBymachineName.Location = new Point(12, 12);
            btnSortBymachineName.Name = "btnSortBymachineName";
            btnSortBymachineName.Size = new Size(204, 29);
            btnSortBymachineName.TabIndex = 1;
            btnSortBymachineName.Text = "Sort By Machine Name";
            btnSortBymachineName.UseVisualStyleBackColor = true;
            btnSortBymachineName.Click += btnGenerateSortData;
            // 
            // tbMachineName
            // 
            tbMachineName.BackColor = Color.Black;
            tbMachineName.BorderColor = Color.FromArgb(50, 50, 50);
            tbMachineName.BorderStyle = BorderStyle.FixedSingle;
            tbMachineName.Font = new Font("Arial", 9F);
            tbMachineName.ForeColor = Color.WhiteSmoke;
            tbMachineName.Location = new Point(12, 47);
            tbMachineName.Name = "tbMachineName";
            tbMachineName.Size = new Size(204, 25);
            tbMachineName.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 555);
            Controls.Add(tbMachineName);
            Controls.Add(btnSortBymachineName);
            Controls.Add(rtbOutput);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkRichTextBox rtbOutput;
        private DarkButton btnSortBymachineName;
        private DarkTextBox tbMachineName;
    }
}
