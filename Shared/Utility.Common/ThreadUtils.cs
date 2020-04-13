#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
namespace Utility
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 线程 公共类 不支持 netstandard 1.0 - 1.6
    /// </summary>
    public class ThreadUtils
    {
        public readonly static Action EmptyAction = () => { };
    
        /// <summary>
        /// 内部类
        /// </summary>
        class InnerThread
        {
            ///<summary>
            ///声明并初始化
            /// </summary>
            public static readonly ThreadUtils ThreadObject = new ThreadUtils();
        }
        /// <summary>
        /// 初始化ThreadUtils对象 饿汉式 单例模式 
        /// </summary>
        public static ThreadUtils Instance
        {
            get
            {
                return InnerThread.ThreadObject;
            }
        }
        public bool Complete { 
            get {
                if (this.Threads != null&& this.Threads.Count>0)
                {
                    foreach (var item in this.Threads)
                    {
                        if (!item.Value.Status)
                            return false;
                    }
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 获取线程
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual System.Threading.Thread this[string key] {
            get
            {
                return _threads.ContainsKey(key) ? Threads[key].Thread : (System.Threading.Thread)null;
            }
        }
        /// <summary>
        /// 获取线程
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual System.Threading.Thread this[int index] {
            get
            {
                 if(_threads.Count>index)
                 {
                    int i=0;
                    foreach (var item in _threads)
                    {
                        if(i==index){
                            return item.Value.Thread;
                        }
                        i++;
                    }  
                } 
                return (System.Threading.Thread)null;
            }
        }
        private readonly IDictionary<string, ThreadEntity> _threads = new Dictionary<string, ThreadEntity>();//线程集合类
        public class ThreadEntity
        {
            public CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

            public Thread Thread { get; set; }
            public bool Status { get; set; }
        }
        /// <summary>
        /// 获取线程集合
        /// </summary>
        public virtual IDictionary<string, ThreadEntity> Threads => _threads;
        /// <summary>
        /// 创建线程并启动
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <param name="thread">线程对学校</param>
        /// <exception cref="ExistsException"></exception>
        public virtual void Create(string name, ThreadEntity thread)
        {
            if (Threads.ContainsKey(name))
            {
                throw new Exception($"key {nameof(name)} exists");
            }
            else
            {
                thread.Thread.Start();
                Threads.Add(name, thread);
            }
        }
        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <param name="thread">线程对学校</param>
        /// <exception cref="ExistsException"></exception>
        public virtual void Create(string name)
        {
            if (Threads.ContainsKey(name))
            {
                throw new Exception($"key {nameof(name)} exists");
            }
            else
            {
                Threads.Add(name, new ThreadEntity() { Thread= new Thread(() => { ThreadUtils.EmptyAction(); }) });
            }
        }
        public virtual void StartTask(string name)
        {
            if (!Threads.ContainsKey(name))
            {
                throw new Exception($"key {nameof(name)} not exists");
            }
            var thread = this[name];
            thread.Start();
        }
  
        /// <summary>
        /// 创建线程并启动
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <param name="action">线程执行方法</param>
        public virtual void Create(string name, Action action)
        {
            Create(name, new ThreadEntity() { Thread = new System.Threading.Thread(() => { action(); }) });
        }
        /// <summary>
        /// 创建线程并启动
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <param name="action">线程执行方法</param>
        public virtual void Create(string name, Action<object> action)
        {
            Create(name, new ThreadEntity() { Thread= new System.Threading.Thread((obj) => { action(obj); }) });
        }
        /// <summary>
        /// 创建线程并启动
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <param name="action">线程执行方法</param>
        public virtual void CreateState(string name, Action<object> action)
        {
            var thread = new ThreadEntity() {  };
            thread.Thread  = new System.Threading.Thread((obj) => { action(thread); });
            Create(name, thread);
        }
        /// <summary>
        /// 终止线程
        /// <para>
        /// 不支持netcoreapp 1.0 - 1.1
        /// </para>
        /// </summary>
        /// <param name="name">线程名称</param>
        /// <exception cref="NotExistsException"></exception>
        public virtual void Abort(string name)
        {
#if !NETCOREAPP1_0  && !NETCOREAPP1_1 
            if (!Threads.ContainsKey(name))
            {
                throw new Exception($"key {nameof(name)} exists");
            }
            else
            {
                try
                {
                    Threads[name].Thread.Abort();
                }
                catch (Exception)
                {
                }
            }
#endif
        }
        /// <summary>
        /// 阻塞线程
        /// </summary>
        /// <param name="name"></param>
        public virtual void Join(string name)
        {
            var thread = this[name];
            if (thread != null)
            {
                thread.Join();
            }
        }
    }
}
#endif
