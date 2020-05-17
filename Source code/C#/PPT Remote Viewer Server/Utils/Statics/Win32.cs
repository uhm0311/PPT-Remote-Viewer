using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTRemoteViewerServer.Utils.Statics
{
    public class Win32
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public static void SendKey(Keys key)
        {
            keybd_event((byte)key, (byte)0, (uint)0, (UIntPtr)1); // KeyDown
            keybd_event((byte)key, (byte)0, 0x0002, (UIntPtr)1); // KeyUp
        }
    }
}
