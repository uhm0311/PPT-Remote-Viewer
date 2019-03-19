using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTRemoteViewerServer.Utils.Observers.Subjects
{
    public class ScreenRenewalNotifier : ScreenRenewalSubject
    {
        public override void Notify(object parameter)
        {
            Bitmap temp = null;

            if (parameter is Bitmap)
                temp = parameter as Bitmap;

             for (int i = 0; i < observers.Count; i++)
                 observers[i].OnScreenChanged(temp);
        }
    }
}
