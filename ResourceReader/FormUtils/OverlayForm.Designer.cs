namespace Overlays
{
    partial class OverlayForm
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
            lblProgress = new System.Windows.Forms.Label();
            pnlProgress = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Font = new System.Drawing.Font("Arial", 15.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblProgress.ForeColor = System.Drawing.SystemColors.InfoText;
            lblProgress.Location = new System.Drawing.Point(358, 320);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new System.Drawing.Size(87, 30);
            lblProgress.TabIndex = 0;
            lblProgress.Text = "label1";
            // 
            // pnlProgress
            // 
            pnlProgress.BackColor = System.Drawing.SystemColors.HighlightText;
            pnlProgress.Location = new System.Drawing.Point(263, 115);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new System.Drawing.Size(250, 125);
            pnlProgress.TabIndex = 1;
            // 
            // OverlayForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(pnlProgress);
            Controls.Add(lblProgress);
            Name = "OverlayForm";
            Text = "OverlayForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlProgress;
    }
}