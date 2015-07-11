using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Nega.WpfCommon
{

    public static class TaskHelper
    {

        public static void ExcuteOnUIThread(this Task task,
            Action success = null,
            Action<AggregateException> failure = null)
        {
            ExcuteOnUIThread(task, Application.Current, success, failure);
        }

        public static void ExcuteOnUIThread(this Task task,
            DispatcherObject dispatcherObject,
            Action success = null,
            Action<AggregateException> failure = null)
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
                    UIThreadHelper.BeginInvoke(dispatcherObject,
                        () =>
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
                        });
                });
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            Action<T> success = null,
            Action<AggregateException> failure = null)
        {
            ExcuteOnUIThread<T>(task, Application.Current, success, failure);
        }

        public static void ExcuteOnUIThread<T>(this Task<T> task,
            DispatcherObject dispatcherObject,
            Action<T> success = null,
            Action<AggregateException> failure = null)
        {
            if (task == null)
            {
                return;
            }
            task.ContinueWith(
                (e) =>
                {
                    UIThreadHelper.BeginInvoke(dispatcherObject,
                        () =>
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
                        });
                });
        }

        public static void HandleWebException(AggregateException ex)
        {
            if (ex != null && ex.InnerExceptions != null)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    WebException webException = inner as WebException;
                    if (webException != null)
                    {
                        WebExceptionHelper.Handle(webException);
                    }
                }
                
            }
        }

    }

}
