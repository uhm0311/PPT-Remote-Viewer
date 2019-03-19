using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTRemoteViewerServer.Utils.Statics;

namespace PPTRemoteViewerServer.Utils.Connections
{
    public class PacketFactory
    {
        public static byte[] CreateScreenPacket(Bitmap screen)
        {
            List<byte> packet = new List<byte>();
            packet.Add((byte)PacketType.Screen);

            byte[] jpegEncoded = BitmapManager.EncodeToJPEG(screen, 0.5, 0.5, 0.25);
            packet.AddRange(BitConverter.GetBytes(jpegEncoded.Length));
            packet.AddRange(jpegEncoded);

            return packet.ToArray();
        }
    }
}
