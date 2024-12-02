using System;
using System.Windows.Forms;

namespace FormsUtils
{
    public class InputForm : Form
    {
        private TextBox textBox;
        private Button okButton;
        private Button cancelButton;
        private Label label;

        public InputForm(string prompt, string autofillContent = "", int width = 400)
        {
            this.Width = Math.Max(width, 400);

            double aspectRatio = .714;
            this.Height = (int)(this.Width * aspectRatio);

            // Calculate positions and sizes based on the dynamic dimensions
            int margin = 15;
            int labelHeight = 40;
            int textBoxHeight = (int)(this.Height * 0.3); // 30% of form height
            int buttonWidth = 100;
            int buttonHeight = 40;

            label = new Label
            {
                Left = margin,
                Top = margin,
                Text = prompt,
                Width = this.Width - 2 * margin,
                Height = labelHeight,
                AutoSize = false
            };

            textBox = new TextBox
            {
                Left = margin,
                Top = label.Top + label.Height + margin,
                Width = this.Width - (3 * margin),
                Height = textBoxHeight,
                Text = autofillContent,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            int buttonTop = textBox.Top + textBox.Height + margin;

            cancelButton = new Button
            {
                Text = "Cancel",
                Left = (this.Width * 2 / 3) - (buttonWidth / 2),
                Width = buttonWidth,
                Height = buttonHeight,
                Top = buttonTop,
                DialogResult = DialogResult.Cancel
            };

            okButton = new Button
            {
                Text = "OK",
                Left = (this.Width / 3) - (buttonWidth / 2),
                Width = buttonWidth,
                Height = buttonHeight,
                Top = buttonTop,
                DialogResult = DialogResult.OK
            };


            // Add controls to the form
            Controls.Add(textBox);
            Controls.Add(label);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            // Form settings
            AcceptButton = okButton;
            CancelButton = cancelButton;

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Text = "Input Box";
        }

        public string GetInputValue()
        {
            return textBox.Text;
        }

        public static (string content, DialogResult dialogResult) ShowDialog(string prompt, string autofill = "")
        {
            InputForm form = new InputForm(prompt, autofill);
            DialogResult result = form.ShowDialog();
            return (form.GetInputValue(), result);
        }
    }
}
