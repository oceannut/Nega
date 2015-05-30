using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nega.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class LimitQueue<T>
    {

        /// <summary>
        /// 当消息可用时引发的事件。
        /// </summary>
        public event Action<T> Available;

        //限制并发访问资源的线程数。
        private readonly SemaphoreSlim counter;
        //队列。
        private readonly ConcurrentQueue<T> queue;

        //控制队列访问的任务。
        private Task queueTask;

        private CancellationTokenSource cancellationTokenSource;


        /// <summary>
        /// 构建一个消息队列。
        /// </summary>
        /// <param name="boundedCapacity">允许并发处理的消息最大个数。</param>
        public LimitQueue(int boundedCapacity)
        {
            if (boundedCapacity < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "boundedCapacity", boundedCapacity,
                    "");
            }

            this.queue = new ConcurrentQueue<T>();
            this.counter = new SemaphoreSlim(0, boundedCapacity);

            this.cancellationTokenSource = new CancellationTokenSource();
            this.queueTask = new Task(
                () =>
                {
                    while (true)
                    {
                        if (this.cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                        T item;
                        //查询队列
                        if (queue.TryDequeue(out item))
                        {
                            if (Available != null)
                            {
                                Available(item);
                            }
                        }
                        else
                        {
                            //线程等待信号，由Add入队列并给予信号
                            this.counter.Wait(this.cancellationTokenSource.Token);
                        }
                    }
                },
                this.cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning);
            this.queueTask.Start();
        }

        /// <summary>
        /// 往队列添加一个消息。
        /// </summary>
        /// <param name="message">消息。</param>
        public void Enqueue(T item)
        {
            if (item == null)
            {
                return;
            }
            this.queue.Enqueue(item);
            try
            {
                this.counter.Release();
            }
            catch (SemaphoreFullException)
            {
                System.Threading.Thread.SpinWait(1);
                ReleaseWait(2);
            }
        }

        public void Dispose()
        {
            this.cancellationTokenSource.Cancel();
            this.Available = null;
            if (this.counter != null)
            {
                this.counter.Dispose();
            }
            if (this.queueTask != null)
            {
                this.queueTask.Dispose();
            }
        }

        private void ReleaseWait(int wait)
        {
            try
            {
                this.counter.Release();
            }
            catch (SemaphoreFullException)
            {
                System.Threading.Thread.SpinWait(wait);
                ReleaseWait(wait * 2);
            }
        }

    }
}
