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
        public static Packet Read(byte[] buffer)
        {
            PacketType packetType = (PacketType)buffer[0];
            Keys key = Keys.None;

            switch (buffer[1])
            {
                case 0:
                    key = Keys.Left;
                    break;

                case 1:
                    key = Keys.Right;
                    break;
            }

            return new Packet(packetType, key);
        }
    }
}
