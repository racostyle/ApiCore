using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlays
{
    public partial class OverlayForm : Form
    {
        private bool isActive;
        private Form _formToCoverRef;

        public OverlayForm(Form formToCover, float opacity = .5f)
        {
            InitializeComponent();
            _formToCoverRef = formToCover;

            this.BackColor = Color.Black;
            this.Opacity = opacity;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.Owner = formToCover;
            this.StartPosition = FormStartPosition.Manual;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Location = _formToCoverRef.PointToScreen(Point.Empty);
            this.ClientSize = _formToCoverRef.ClientSize;
            this.FormClosing += OverlayForm_FormClosing;

            var size = this.Width / 9;
            var posY = this.Height / 2 - size;

            pnlProgress.Width = size;
            pnlProgress.Height = size;
            pnlProgress.Location = new Point(size, posY);

            var size2 = size * 1f;
            lblProgress.Text = "Working...";
            lblProgress.ForeColor = Color.White;
            lblProgress.Location = new Point(this.Width / 2 - lblProgress.Width / 2, pnlProgress.Location.Y + (int)size2);

            isActive = true;
            var counter = 0;

            _ = Task.Run(async () =>
            {
                while (isActive)
                {
                    try
                    {
                        switch (counter)
                        {
                            case 0:
                                SetPanelPosition(pnlProgress, size, posY);
                                break;
                            case 1:
                                SetPanelPosition(pnlProgress, size * 3, posY);
                                break;
                            case 2:
                                SetPanelPosition(pnlProgress, size * 5, posY);
                                break;
                            default:
                                SetPanelPosition(pnlProgress, size * 7, posY);
                                break;
                        }
                        counter = counter < 3 ? counter + 1 : 0;
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.Message);
                    }
                    finally
                    {
                        await Task.Delay(150);
                    }
                }
            });

            _formToCoverRef.Move += WhenFormToCoverMoves;
            _formToCoverRef.Resize += WhenFormToCoverResizes;

            Show();
        }

        private void WhenFormToCoverResizes(object sender, EventArgs e)
        {
            this.Location = _formToCoverRef.PointToScreen(Point.Empty);
            this.ClientSize = _formToCoverRef.ClientSize;
        }

        private void WhenFormToCoverMoves(Object sender, EventArgs e)
        {
            this.Location = _formToCoverRef.PointToScreen(Point.Empty);
        }

        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isActive = false;
            _formToCoverRef.Move -= WhenFormToCoverMoves;
            _formToCoverRef.Move -= WhenFormToCoverResizes;
        }

        private void SetPanelPosition(Panel panel, int posX, int posY)
        {
            if (panel.InvokeRequired)
            {
                panel.BeginInvoke((MethodInvoker)delegate ()
                {
                    panel.Location = new Point(posX, posY);
                });
            }
            else
            {
                panel.Location = new Point(posX, posY);
            }
        }

        public void SetText(string text)
        {
            if (lblProgress.InvokeRequired)
            {
                lblProgress.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblProgress.Text = text;
                });
            }
            else
            {
                lblProgress.Text = text;
            }
        }
    }
}
