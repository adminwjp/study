using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Utility
{
    public class RandomService<T>
    {
        private readonly List<ServiceEntry> _service;
        private int _index;
        private int _weightIndex;
        private readonly ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();
        public RandomService(List<T> service)
        {
            this._service = new List<ServiceEntry>();
            foreach (var item in service)
            {
                this._service.Add(new ServiceEntry() { Value=item});
            }
        }
        public RandomService(List<ServiceEntry> service)
        {
            this._service = service;
        }
        public int Count
        {
            get {
                return this._service.Count;
            }
        }
        /// <summary>
        /// 完全随机轮询
        /// </summary>
        /// <returns></returns>
        public T FullRandom()
        {
            if (this.Count == 0)
            {
                return default(T);
            }
            return this._service[RandomUtils.Instance.Random.Next(this.Count)].Value;
        }
        /// <summary>
        /// 加权随机轮询
        /// </summary>
        /// <returns></returns>
        public T WeightRound()
        {
            if (this.Count == 0)
            {
                return default(T);
            }
            Interlocked.CompareExchange(ref this._index, 0, this.Count);
            int num = this._weightIndex % this.Count;
            T data = Get(num);
            Interlocked.Increment(ref this._weightIndex);
            return data;
        }
        /// <summary>
        /// 平滑加权轮询
        /// </summary>
        /// <returns></returns>
        public T SmoothWeightRound()
        {
            try
            {
                this._readerWriterLockSlim.EnterReadLock();
                ServiceEntry data = null;
                int allWeight = this._service.Select(it => it.Weight).Sum();
                foreach (var item in this._service)
                {
                    if (data == null || item.CurrentWeight > data.CurrentWeight)
                    {
                        data = item;
                    }
                }
                data.CurrentWeight -= allWeight;
                foreach (var item in this._service)
                {
                    data.CurrentWeight = data.CurrentWeight + data.Weight;
                }
                return data.Value;
            }
            finally
            {
                this._readerWriterLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 完全轮询
        /// </summary>
        /// <returns></returns>
        public T FullRound()
        {
            if (this.Count == 0)
            {
                return default(T);
            }
            Interlocked.CompareExchange(ref this._index, 0, this.Count);
            T data = Get(this._index);
            Interlocked.Increment(ref this._index);
            return data;
        }
        /// <summary>
        /// 负载均衡算法中的哈希算法 java  Map：TreeMap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T HashService(T data)
        {
            int clientHash = data.GetHashCode();
            return Get(Math.Abs(clientHash ==0? 0:this.Count % clientHash));
        }
        private T Get(int index)
        {
            try
            {
                this._readerWriterLockSlim.EnterReadLock();
                T data = this._service[index].Value;
                return data;
            }
            finally
            {
                this._readerWriterLockSlim.ExitReadLock();
            }
        }
        public class ServiceEntry
        {
            public int Weight { get; set; }
            public int CurrentWeight { get; set; }
            public T Value { get; set; }
        }
    }
}
