#if NET20 || NET30
#region func
/// <summary>
/// 委托方法 func 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
public delegate T Func<T>();
/// <summary>
/// 委托方法 func 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <param name="t"></param>
/// <returns></returns>
public delegate T1 Func<T, T1>(T t);
/// <summary>
/// 委托方法 func 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <returns></returns>
public delegate T2 Func<T, T1, T2>(T t, T1 t1);
/// <summary>
/// 委托方法 func 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <returns></returns>
public delegate T3 Func<T, T1, T2, T3>(T t, T1 t1, T2 t2);
/// <summary>
/// 委托方法 func 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <returns></returns>
public delegate T4 Func<T, T1, T2, T3,T4>(T t, T1 t1, T2 t2,T3 t3);
/// <summary>
/// 委托方法 func
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <returns></returns>
public delegate T5 Func<T, T1, T2, T3, T4,T5>(T t, T1 t1, T2 t2, T3 t3,T4 t4);
/// <summary>
/// 委托方法 func
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <typeparam name="T6"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <param name="t5"></param>
/// <returns></returns>
public delegate T6 Func<T, T1, T2, T3, T4, T5,T6>(T t, T1 t1, T2 t2, T3 t3, T4 t4,T5 t5);
/// <summary>
/// 委托方法 func
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <typeparam name="T6"></typeparam>
/// <typeparam name="T7"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <param name="t5"></param>
/// <param name="t6"></param>
/// <returns></returns>
public delegate T7 Func<T, T1, T2, T3, T4, T5, T6,T7>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5,T6 t6);
#endregion func



#region action
/// <summary>
/// 委托方法 Action
/// </summary>
public delegate void Action();
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="t"></param>
public delegate void Action<T>(T t);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
public delegate void Action<T, T1>(T t,T1 t1);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
///<param name="t2"></param>
public delegate void Action<T, T1, T2>(T t, T1 t1,T2 t2);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
public delegate void Action<T, T1, T2, T3>(T t, T1 t1, T2 t2,T3 t3);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
public delegate void Action<T, T1, T2, T3, T4>(T t, T1 t1, T2 t2, T3 t3,T4 t4);
/// <summary>
/// 委托方法 func
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <param name="t5"></param>
public delegate void Action<T, T1, T2, T3, T4, T5>(T t, T1 t1, T2 t2, T3 t3, T4 t4,T5 t5);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <typeparam name="T6"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <param name="t5"></param>
///  <param name="t6"></param>
public delegate void Action<T, T1, T2, T3, T4, T5, T6>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5,T6 t6);
/// <summary>
/// 委托方法 Action
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// <typeparam name="T4"></typeparam>
/// <typeparam name="T5"></typeparam>
/// <typeparam name="T6"></typeparam>
/// <typeparam name="T7"></typeparam>
/// <param name="t"></param>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <param name="t3"></param>
/// <param name="t4"></param>
/// <param name="t5"></param>
/// <param name="t6"></param>
/// <param name="t7"></param>
public delegate void Action<T, T1, T2, T3, T4, T5, T6, T7>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6,T7 t7);
#endregion action

#endif


#region tuple
///// <summary>
///// 
///// </summary>
///// <typeparam name="T"></typeparam>
//public class Tuple<T> {
//    /// <summary>
//    /// 
//    /// </summary>
//    public T T { get; private set; }
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="t"></param>
//    public Tuple(T t)
//    {
//        this.T = t;
//    }
//}
#endregion tuple

/// <summary>
/// 数据库改变
/// </summary>
public delegate void DatabaeChannged();
/// <summary>
/// 默认方法
/// </summary>
internal delegate void DefaultMethod();
