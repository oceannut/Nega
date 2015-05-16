using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Nega.WpfCommon
{

    public static class TaskEx
    {

        public static void ExcuteOnUIThread(this Task task,
            Action completion = null,
            Action<Exception> error = null)
        {
            ExcuteOnUIThread(task, Application.Current, completion, error);
        }

        public static void ExcuteOnUIThread(this Task task,
            DispatcherObject dispatcherObject,
            Action completion = null,
            Action<Exception> error = null)
        {
            if (task == null)
            {
                return;
            }
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException();
            }
            task.ContinueWith(
                (e) =>
                {
                    dispatcherObject.Dispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            if (e.Exception == null)
                            {
                                if (completion != null)
                                {
                                    completion();
                                }
                            }
                            else
                            {
                                if (error != null)
                                {
                                    error(e.Exception);
                                }
                            }
                        }));
                });
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            Action<T> completion = null,
            Action<Exception> error = null)
        {
            ExcuteOnUIThread<T>(task,  Application.Current, completion, error);
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            DispatcherObject dispatcherObject,
            Action<T> completion = null, 
            Action<Exception> error = null)
        {
            if (task == null)
            {
                return;
            }
            task.ContinueWith(
                (e) =>
                {
                    dispatcherObject.Dispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            if (e.Exception == null)
                            {
                                if (completion != null)
                                {
                                    completion(e.Result);
                                }
                            }
                            else
                            {
                                if (error != null)
                                {
                                    error(e.Exception);
                                }
                            }
                        }));
                });
        }

    }
}
