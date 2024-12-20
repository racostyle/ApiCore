using System.Drawing;
using System.Windows.Forms;

namespace FormsUtils.Controls
{
    public class DarkListView : ListView
    {
        private Color _borderColor = CustomColors.BORDER_COLOR;

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public DarkListView()
        {
            Font = new Font("Arial", 9, FontStyle.Regular);
            BackColor = Color.Black;
            ForeColor = Color.WhiteSmoke;
            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int thickness = 1;
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(_borderColor, thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness, halfThickness,
                    ClientSize.Width - thickness - 1, ClientSize.Height - thickness - 1));
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_PAINT message
            const int WM_PAINT = 0xF;
            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    int thickness = 1;
                    using (Pen p = new Pen(_borderColor, thickness))
                    {
                        g.DrawRectangle(p, new Rectangle(0, 0, Width - thickness, Height - thickness));
                    }
                }
            }
        }
    }
}
