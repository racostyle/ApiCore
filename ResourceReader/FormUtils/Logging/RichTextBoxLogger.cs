using System.Text;

namespace FormsUtils.Logging
{
    public enum TextTag { None, Bold, Underline, Strikeout, Italic, Red, Green, Blue, Orange, Violet, Yellow, Cyan, Lime }

    public class RichTextBoxLogger : IDisposable, IRTBLogger
    {
        private readonly RichTextBox _textBox;
        private readonly StringBuilder _sb;
        private readonly Tags _tags;

        private bool isActive = false;
        private bool doUpdate = false;

        public RichTextBoxLogger(RichTextBox rtbOutputRich, int updateInterval = 1000)
        {
            isActive = true;
            _textBox = rtbOutputRich;
            _tags = new Tags();
            _sb = new StringBuilder();
            _ = Task.Run(() => DisplayInformationTask(updateInterval));
        }

        public void Log(string message, TextTag tag, bool newSection)
        {
            var text = $"{Environment.NewLine}{_tags.WrapInTag(tag, message)}";
            Log(text);
        }

        public void Log(string message, TextTag tag)
        {
            var text = _tags.WrapInTag(tag, message);
            Log(text);
        }

        public void Log(string message, bool newSection)
        {
            message = $"{Environment.NewLine}{message}";
            Log(message);
        }

        public void Log(string message)
        {
            doUpdate = true;
            if (string.IsNullOrEmpty(message))
                _sb.AppendLine(string.Empty);
            else if (message.StartsWith(Environment.NewLine))
            {
                _sb.AppendLine(string.Empty);
                message = message.Substring(Environment.NewLine.Length);
                _sb.AppendLine($"{DateTime.Now.ToString("HH:mm:ss")}  {message}");
            }
            else
                _sb.AppendLine($"{DateTime.Now.ToString("HH:mm:ss")}  {message}");
        }

        #region DISPLAY 
        private async Task DisplayInformationTask(int updateInterval)
        {
            while (isActive)
            {
                await Task.Delay(updateInterval);
                if (doUpdate)
                {
                    DisplayAndFormatText();
                    doUpdate = false;
                }
            }
        }

        private void DisplayAndFormatText()
        {
            if (_textBox.InvokeRequired)
            {
                _textBox.BeginInvoke((MethodInvoker)delegate ()
                {
                    UpdateTextWithMarkdowns();
                });
            }
            else
            {
                UpdateTextWithMarkdowns();
            }
        }

        private void UpdateTextWithMarkdowns()
        {
            ApplyMarkdowns();
            _textBox.Refresh();
            _textBox.SelectionStart = _textBox.Text.Length;
            _textBox.ScrollToCaret();
        }

        private void ApplyMarkdowns()
        {
            string[] lines = _sb.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            _textBox.Clear();

            var types = Enum.GetValues(typeof(TextTag));
            for (int i = 0; i < lines.Length; i++)
            {
                bool effects = false;
                TextTag? tagType = TextTag.None;

                foreach (TextTag value in types)
                {
                    var startTag = _tags.StartingTag(value);
                    if (lines[i].Contains(startTag))
                    {
                        tagType = value;
                        effects = true;
                        lines[i] = lines[i].Replace(startTag, string.Empty);
                        lines[i] = lines[i].Replace(_tags.EndingTag(value), string.Empty);
                        break;
                    }
                }

                if (!effects)
                    _textBox.AppendText($"{lines[i]}{Environment.NewLine}");
                else
                {
                    switch (tagType)
                    {
                        case TextTag.Bold:
                            ChangeFont(lines[i], FontStyle.Bold);
                            break;
                        case TextTag.Italic:
                            ChangeFont(lines[i], FontStyle.Italic);
                            break;
                        case TextTag.Underline:
                            ChangeFont(lines[i], FontStyle.Underline);
                            break;
                        case TextTag.Strikeout:
                            ChangeFont(lines[i], FontStyle.Strikeout);
                            break;
                        case TextTag.Red:
                            ChangeColor(lines[i], Color.Red);
                            break;
                        case TextTag.Orange:
                            ChangeColor(lines[i], Color.Orange);
                            break;
                        case TextTag.Violet:
                            ChangeColor(lines[i], Color.Violet);
                            break;
                        case TextTag.Yellow:
                            ChangeColor(lines[i], Color.Yellow);
                            break;
                        case TextTag.Blue:
                            ChangeColor(lines[i], Color.Blue);
                            break;
                        case TextTag.Cyan:
                            ChangeColor(lines[i], Color.Cyan);
                            break;
                        case TextTag.Green:
                            ChangeColor(lines[i], Color.Green);
                            break;
                        case TextTag.Lime:
                            ChangeColor(lines[i], Color.LimeGreen);
                            break;
                        default:
                            _textBox.AppendText($"{lines[i]}{Environment.NewLine}");
                            break;
                    }
                }
            }
        }

        private void ChangeFont(string line, FontStyle style)
        {
            Font currentFont = _textBox.SelectionFont;
            if (currentFont == null)
                return;

            FontStyle newFontStyle = currentFont.Style ^ style;
            _textBox.SelectionFont = new Font(currentFont, newFontStyle);
            _textBox.AppendText($"{line}{Environment.NewLine}");
        }

        private void ChangeColor(string line, Color color)
        {
            _textBox.SelectionStart = _textBox.TextLength;
            _textBox.SelectionLength = 0;
            _textBox.SelectionColor = color;
            _textBox.AppendText($"{line}{Environment.NewLine}");
            _textBox.SelectionColor = _textBox.ForeColor;
        }

        public void Dispose()
        {
            DisplayAndFormatText();
            isActive = false;
        }

        public void Clear()
        {
            _sb.Clear();
            doUpdate = true;
        }
        #endregion
    }

    internal class Tags
    {
        internal string WrapInTag(TextTag tag, string text)
        {
            return $"{StartingTag(tag)}{text}{EndingTag(tag)}";
        }

        internal string StartingTag(TextTag tag)
        {
            return $"<{tag}>";
        }

        internal string EndingTag(TextTag tag)
        {
            return $"</{tag}>";
        }
    }
}
