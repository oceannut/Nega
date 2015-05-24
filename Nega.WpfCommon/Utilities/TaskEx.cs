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
            Action success = null,
            Action<Exception> failure = null)
        {
            ExcuteOnUIThread(task, Application.Current, success, failure);
        }

        public static void ExcuteOnUIThread(this Task task,
            DispatcherObject dispatcherObject,
            Action success = null,
            Action<Exception> failure = null)
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
                                if (success != null)
                                {
                                    success();
                                }
                            }
                            else
                            {
                                if (failure != null)
                                {
                                    failure(e.Exception);
                                }
                            }
                        }));
                });
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            Action<T> success = null,
            Action<Exception> failure = null)
        {
            ExcuteOnUIThread<T>(task, Application.Current, success, failure);
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            DispatcherObject dispatcherObject,
            Action<T> success = null,
            Action<Exception> failure = null)
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
                                if (success != null)
                                {
                                    success(e.Result);
                                }
                            }
                            else
                            {
                                if (failure != null)
                                {
                                    failure(e.Exception);
                                }
                            }
                        }));
                });
        }

    }
}
