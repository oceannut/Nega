using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Nega.WpfCommon
{

    public static class UIThreadHelper
    {

        public static void BeginInvoke(System.Action action,
            params object[] args)
        {
            BeginInvoke(Application.Current, action, args);
        }

        public static void BeginInvoke(DispatcherObject dispatcherObject, 
            System.Action action,
            params object[] args)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException();
            }

            dispatcherObject.Dispatcher.BeginInvoke(action, args);
        }

    }

}
