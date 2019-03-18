using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTRemoteViewerServer.Utils;

namespace PPTRemoteViewerServer
{
    public abstract class Command
    {
        protected static ScreenManager screenManager = null;

        protected Command()
        {
            if (screenManager == null)
                screenManager = new ScreenManager(1000000, 70);
        }

        public abstract object Execute();
    }
}
