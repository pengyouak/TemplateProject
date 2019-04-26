using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TemplateProject.Plugins.Toast;

namespace TemplateProject.Plugins.Toast
{
    public class Toast
    {
        static PopupControl.Popup pop;

        public static void ShowToast(Control parent, string message)
        {
            pop = new PopupControl.Popup(new FormToast(message))
            {
                ShowingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.TopToBottom,
                HidingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.BottomToTop
            };
            pop.Show(parent, new Point(parent.Width / 2 - pop.Width / 2, parent.Height / 2 - pop.Height / 2));
        }
    }
}
