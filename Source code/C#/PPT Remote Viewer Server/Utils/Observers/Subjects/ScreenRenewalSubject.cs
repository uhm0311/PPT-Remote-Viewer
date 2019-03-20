using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTRemoteViewerServer.Utils.Observers.Subjects
{
    public abstract class ScreenRenewalSubject
    {
        protected List<ScreenRenewalObserver> observers = new List<ScreenRenewalObserver>();

        public void AddObserver(ScreenRenewalObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(ScreenRenewalObserver observer)
        {
            observers.Remove(observer);
        }

        public abstract void Notify(object parameter);
    }
}
