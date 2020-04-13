using System;
using System.Collections.Generic;

namespace Utility
{
    public class ObjectPool<T> : ObjectPool,IObjectPool<T> where T : class
    {
        protected new List<ObjectEntry<T>> ObjectEntries = new List<ObjectEntry<T>>();
        
        public new IObjectPool<T> Build()
        {
            return this;
        }

        public T CreateInstace(Func<T> func)
        {
            return func();
        }

        public new T Get()
        {
            for (int i = 0; i < ObjectEntries.Count; i++)
            {
                if(ObjectEntries[i].State== ObjectState.Idea)
                {
                    ObjectEntries[i].State = ObjectState.Active;
                    return ObjectEntries[i].Object;
                }
            }
            return default(T);
        }

        public  bool Remove(T obj)
        {
            for (int i = 0; i < ObjectEntries.Count; i++)
            {
                if (object.ReferenceEquals(ObjectEntries[i].Object,obj))
                {
                    ObjectEntries[i].State = ObjectState.Idea;
                    return true;
                }
            }
            return false;
        }

        object IObjectPool.Get()
        {
            return base.Get();
        }
        bool IObjectPool.Remove(object obj)
        {
            return base.Remove(obj);
        }
    }
    public class ObjectPool: IObjectPool
    {
        protected  List<ObjectEntry> ObjectEntries = new List<ObjectEntry>();
        public int MaxActive { get; set; }
        public int MinActive { get; set; }
        public int MaxIdea { get; set; }
        public int MinIdea { get; set; }

        public IObjectPool Build()
        {
            return this;
        }

        public object CreateInstace(Func<object> func)
        {
            return func();
        }

        public void Dispose()
        {
            ObjectEntries.Clear();
        }

        public object Get()
        {
            for (int i = 0; i < ObjectEntries.Count; i++)
            {
                if (ObjectEntries[i].State == ObjectState.Idea)
                {
                    ObjectEntries[i].State = ObjectState.Active;
                    return ObjectEntries[i].Object;
                }
            }
            return default(object);
        }

        public bool Remove(object obj)
        {
            for (int i = 0; i < ObjectEntries.Count; i++)
            {
                if (object.ReferenceEquals(ObjectEntries[i].Object, obj))
                {
                    ObjectEntries[i].State = ObjectState.Idea;
                    return true;
                }
            }
            return false;
        }
    }
}
