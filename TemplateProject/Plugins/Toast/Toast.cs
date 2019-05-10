using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindCommon.Plugins.Toast;
using WindCommon.Controls;
using WindCommon.Controls.PopupControl;

namespace WindCommon.Plugins.Toast
{
    public class Toast
    {
        static Popup pop;

        public static void ShowToast(Control parent, string message)
        {
            pop = new Popup(new FormToast(message))
            {
                ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom,
                HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop
            };
            pop.Show(parent, new Point(parent.Width / 2 - pop.Width / 2, parent.Height / 2 - pop.Height / 2));
        }
    }
}
