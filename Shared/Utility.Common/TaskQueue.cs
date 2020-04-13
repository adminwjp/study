using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public class TaskQueue
    {
        private readonly System.Collections.Concurrent.ConcurrentQueue<Action> _queue = new System.Collections.Concurrent.ConcurrentQueue<Action>();
        private CancellationTokenSource _cancellationTokenSource;
        private Task _mainTask;
        public ThreadUtils _threadUtils { get; set; } = ThreadUtils.Instance;
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        public int MinTask { get; set; } = 5;
        public int MaxTask { get; set; } = 10;
        public int MinIdeaTask { get; set; } = 7;
        public int MaxIdeaTask { get; set; } = 7;
        public int MinSleep { get; set; } = 100;
        public int MaxSleep { get; set; } = 500;
        public bool PullTaskEnd { get; set; }
        public bool TaskComplete { get {
                if (this.PullTaskEnd&&this._threadUtils != null&&this._threadUtils.Complete)
                {
                    return true;
                }
                return false;
            }
        }
        public int Count
        {
            get
            {
                return this._queue.Count;
            }
        }
        private Action Pop()
        {
             this._queue.TryDequeue(out Action action);
             return action;
        }
        public void Push(Action action)
        {
            this._queue.Enqueue(action);
        }
        public void Start()
        {
            if (this._cancellationTokenSource != null && !this._cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }
            this._cancellationTokenSource = new CancellationTokenSource();
            this._mainTask = Task.Factory.StartNew(()=> {
                this.Execute();
            }, this._cancellationTokenSource.Token);
        }
        public void Stop()
        {
            this._cancellationTokenSource.Cancel();
        }
       
        public void Execute()
        {
            Initial();
            while (!this._cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    if(this._queue.Count>0)
                    {
                        //分配任务
                        this._waitHandle.Set();
                    }
                    Thread.Sleep(RandomUtils.Instance.Random.Next(this.MinSleep/10, this.MaxSleep/10));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}{e.StackTrace}");
                }
            }
        }
        private void DefaultTask(object obj)
        {
            ThreadUtils.ThreadEntity thread = obj as ThreadUtils.ThreadEntity;
            if (thread == null)
            {
                throw new ArgumentNullException("thread is not null");
            }
            while (!thread.CancellationToken.IsCancellationRequested)
            {
                try
                {
                 
                    this._waitHandle.WaitOne(10*500);//等待分配任务
                
                    var task = this.Pop();
                    if (task != null)
                    {
                        thread.Status = false;
                        task?.Invoke();
                        thread.Status = true;
                    }
                    else
                    {
                        thread.Status = true;
                    }
                    Thread.Sleep(RandomUtils.Instance.Random.Next(this.MinSleep, this.MaxSleep));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}{e.StackTrace}");
                }
            }
        }
        private void Initial()
        {
            if (this._threadUtils.Threads.Count == 0)
            {
                for (int i = 0; i < this.MaxTask; i++)
                {
                    this._threadUtils.CreateState($"task{i + 1}", (it=> { this.DefaultTask(it); }));
                }
            }
        }
    }
}
