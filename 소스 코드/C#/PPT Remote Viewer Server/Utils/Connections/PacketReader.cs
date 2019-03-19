using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTRemoteViewerServer.Utils.Connections
{
    public class PacketReader
    {
        public static PacketType GetPacketType(byte[] buffer)
        {
            return (PacketType)buffer[0];
        }

        public static Keys GetKey(byte[] buffer)
        {
            Keys none = Keys.None;

            switch (buffer[0])
            {
                case 0: return Keys.Left;
                case 1: return Keys.Right;

                default: return none;
            }
        }
    }
}
