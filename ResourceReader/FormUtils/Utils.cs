using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FormsUtils
{
    public static class Utils
    {
        /// <summary>
        /// Example: Utils.StartForm(() => new DbLoader.BackupDbForm(tbSqlServer.Text, SETTINGS_FILE));
        /// </summary>
        public static T StartForm<T>(Func<T> formFactory) where T : Form
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(T)) // or form is T
                {
                    form.BringToFront();
                    return (T)form;
                }
            }
            var newForm = formFactory();
            newForm.Show();
            return newForm;
        }

        public static IEnumerable<Control> GetAllControlsInForm(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;

                // If the control has child controls (e.g., it's a Panel, GroupBox, etc.)
                if (control.HasChildren)
                {
                    foreach (Control childControl in GetAllControlsInForm(control))
                        yield return childControl;
                }
            }
        }
    }
}
