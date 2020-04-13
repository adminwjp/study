using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class CollectionUtils<T>
    {
        public static readonly List<T> ListEmpty = new List<T>(new T[0]); 
    }
    public class CollectionUtils
    {
        
        public static List<T> List<T>(IEnumerable<T> data)
        {
            if (data == null)
            {
                return CollectionUtils<T>.ListEmpty;
            }
            List<T> datas = new List<T>(data);
            return datas;
            //using (IEnumerator<T> enumerator = objs.GetEnumerator())
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        datas.Add(enumerator.Current);
            //    }
            //    return datas;
            //}
        }
        public static void Remove<T>(ISet<T> data,T obj)
        {
            data.Remove(obj);
        }
        public static bool Remove<T>(IList<T> data, T obj)
        {
            int index = data.IndexOf(obj);
            if (index > -1)
            {
                data.RemoveAt(index);
            }
            return index > -1;
           
        }
        /// <summary>
        ///objs contains obj 
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Contains<T>(T[] objs, T obj)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (Comparer<T>.Default.Compare(objs[i], obj) == 0)
                    return true;
            }
            return false;
        }
    }
}
