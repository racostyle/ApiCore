using FormsUtils;

namespace ResourceReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.BackColor = CustomColors.BACKGROUND_COLOR;
            var controls = Utils.GetAllControlsInForm(this);
            foreach (Control c in controls)
            {
                if (c.Name.StartsWith("lbl"))
                {
                    c.BackColor = CustomColors.BACKGROUND_COLOR;
                    c.ForeColor = Color.White;
                }
                if (c.Name.StartsWith("chb"))
                    c.ForeColor = Color.White;
            }
        }
    }
}
