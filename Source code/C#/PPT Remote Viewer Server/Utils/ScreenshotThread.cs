using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using PPTRemoteViewerServer.Utils.Observers.Subjects;
using PPTRemoteViewerServer.Utils.Statics;

namespace PPTRemoteViewerServer.Utils
{
    public class ScreenshotThread
    {
        private Thread thread = null;
        private bool isRunning = false;

        private Bitmap previousScreen = null;
        private Bitmap currentScreen = null;

        private ScreenRenewalSubject subject = null;

        public ScreenshotThread(ScreenRenewalSubject subject)
        {
            this.subject = subject;
            thread = new Thread(new ThreadStart(Run)) { IsBackground = true };
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                thread.Start();
            }
        }

        private void Run()
        {
            while (isRunning)
            {
                if (previousScreen != null)
                    previousScreen.Dispose();

                if (currentScreen != null)
                {
                    previousScreen = BitmapManager.CloneBitmap(currentScreen);
                    currentScreen.Dispose();
                }

                currentScreen = ScreenManager.Screenshot();
                bool sendScreen = false;

                if (previousScreen != null)
                {
                    if (BitmapManager.BitmapChanged(previousScreen, currentScreen, 1000000, 70))
                        sendScreen = true;
                }
                else sendScreen = true;

                if (sendScreen)
                    subject.Notify(BitmapManager.CloneBitmap(currentScreen));

                Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                thread.Abort();

                if (previousScreen != null)
                {
                    previousScreen.Dispose();
                    previousScreen = null;
                }

                if (currentScreen != null)
                {
                    currentScreen.Dispose();
                    currentScreen = null;
                }
            }
        }

        public Bitmap GetCurrentScreen()
        {
            return BitmapManager.CloneBitmap(currentScreen);
        }
    }
}
