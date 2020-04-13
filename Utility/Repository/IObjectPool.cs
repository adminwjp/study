using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public interface IObjectPool:IDisposable
    {
         int MaxActive { get; set; }
         int MinActive { get; set; }
        int MaxIdea { get; set; }
        int MinIdea { get; set; }
        object Get();
        bool Remove(object obj);
        IObjectPool Build();
        object CreateInstace(Func<object> func);
    }
    public interface IObjectPool<T> : IObjectPool where T:class
    {

        new T Get();
        bool Remove(T obj);
        new IObjectPool<T> Build();
        T CreateInstace(Func<T> func);
    }
}
