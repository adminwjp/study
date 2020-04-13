using System;
using System.Collections.Generic;
using System.Text;
#if !NET20 && !NET30 && !NET35 && !NET40 && !NET45 && !NET451 && !NET452 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
using System.Threading.Tasks;
using System.Threading;
#endif

namespace Utility
{
    public class TaskUtils
    {
#if !NET20 && !NET30 && !NET35 && !NET40 && !NET45 && !NET451 && !NET452 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
        /// <summary>
        /// task wrapper 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public static Task<T> TaskCompletionSource<T>(Func<T> func, CancellationToken cancellation = default(CancellationToken))
        {
            return  TaskCompletionSourceAsync(func,cancellation);
        }
        /// <summary>
        /// task wrapper 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public static async Task<T> TaskCompletionSourceAsync<T>(Func<T> func, CancellationToken cancellation = default(CancellationToken))
        {
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
            if (cancellation.IsCancellationRequested) return await Task.FromCanceled<T>(cancellation);
            try
            {
                taskCompletionSource.SetResult(func());
            }
            catch (System.Exception e)
            {
                taskCompletionSource.SetException(e);
            }
            return await taskCompletionSource.Task;
        }
#endif
    }
}
