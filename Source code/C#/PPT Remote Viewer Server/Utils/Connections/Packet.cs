using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTRemoteViewerServer.Utils.Connections
{
    public class Packet
    {
        private PacketType type = PacketType.None;
        private Keys key = Keys.None;

        public Packet(PacketType type, Keys key)
        {
            this.type = type;
            this.key = key;
        }

        public PacketType Type
        {
            get { return type; }
        }

        public Keys Key
        {
            get { return key; }
        }
    }
}
