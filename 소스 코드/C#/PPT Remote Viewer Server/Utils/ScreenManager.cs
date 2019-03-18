using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTRemoteViewerServer.Utils
{
    public class ScreenManager
    {
        public static Bitmap Screenshot()
        {
            Size screenSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap screen = new Bitmap(screenSize.Width, screenSize.Height);

            using (Graphics screenGraphics = Graphics.FromImage(screen))
            {
                screenGraphics.CopyFromScreen(0, 0, 0, 0, screenSize);
                return screen;
            }
        }
    }
}
