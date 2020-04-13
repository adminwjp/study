using System;
using System.Text;

namespace Utility
{
    /// <summary>
    /// js 调用帮助类 >net4.5  >netcore 2.0 > netstandard 2.0 
    /// </summary>
    public class ScriptUtils
    {
        /// <summary>
        /// 内部类
        /// </summary>
        class InnerScript
        {
            public static readonly ScriptUtils ScriptObject = new ScriptUtils();
            /// <summary>
            /// js 调用帮助类 net4.5 - net 4.8 netcore 2.0 -3.0 netstandard 2.0 
            /// </summary>
            ///<exception cref="DllNotFoundException">Jint</exception>
            ///<exception cref="ArgumentNullException">Jint</exception>
            static InnerScript()
            {
                type = Type.GetType("Jint.Engine,Jint");
                if(type!=null) engine = Activator.CreateInstance(type);
                else  if (type == null) 
#if !NETSTANDARD1_0
                        throw new DllNotFoundException("Jint");
#else
                     throw new ArgumentNullException("Jint");
#endif

            }
        }
        private static object engine;//声明Engine对象
        private static Type type;
        /// <summary>
        /// 获取实例 饿汉式单例模式
        /// </summary>
        ///<exception cref="DllNotFoundException">Jint</exception>
        public static ScriptUtils Instance
        {
            get
            {
               return InnerScript.ScriptObject;
            }
        }
        /// <summary>
        /// 执行的脚本文件 单脚本执行
        /// </summary>
        /// <param name="file"></param>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public ScriptUtils Add(string file)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ArgumentsUtils.CheckArgumentNull("file", file);
            engine=type.GetMethod("Execute").Invoke(engine,new object[] { System.IO.File.ReadAllText(file, Encoding.UTF8) });
            return this;
#else
            throw new NotSupportedException();
#endif
        }
        /// <summary>
        /// 执行函数 单脚本执行
        /// </summary>
        /// <param name="funName">函数名称</param>
        /// <param name="objs">参数</param>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public object Execute(string funName, params object[] objs)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ArgumentsUtils.CheckArgumentNull("funName", funName);
            //ArgumentsUtils.CheckArgumentNull("objs", objs);
            return type.GetMethod("Invoke").Invoke(engine, new object[] { funName, objs });
#else
            throw new NotSupportedException();
#endif
        }
        /// <summary>
        /// 执行函数 多 单脚本执行
        /// </summary>
        /// <param name="file">执行的脚本文件</param>
        /// <param name="funName">函数名称</param>
        /// <param name="objs">参数</param>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public object Execute(string file, string funName, params object[] objs)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ArgumentsUtils.CheckArgumentNull("file", file);
            ArgumentsUtils.CheckArgumentNull("funName", funName);
            //ArgumentsUtils.CheckArgumentNull("objs", objs);
            object obj = Activator.CreateInstance(type);
            obj=type.GetMethod("Execute").Invoke(obj, new object[] { System.IO.File.ReadAllText(file, Encoding.UTF8) });
            return type.GetMethod("Invoke").Invoke(obj, new object[] { funName, objs });
#else
            throw new NotSupportedException();
#endif
        }
        /// <summary>
        /// 执行的脚本文件 多 单脚本执行
        /// </summary>
        /// <param name="file"></param>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public object Execute(string file)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ArgumentsUtils.CheckArgumentNull("file", file);
            object obj = Activator.CreateInstance(type);
            return obj=type.GetMethod("Execute").Invoke(obj, new object[] { System.IO.File.ReadAllText(file, Encoding.UTF8) });
#else
            throw new NotSupportedException();
#endif
        }
        /// <summary>
        /// 执行函数 多 单脚本执行
        /// </summary>
        /// <param name="engine">执行的脚本对象</param>
        /// <param name="funName">函数名称</param>
        /// <param name="objs">参数</param>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public object Execute(object engine, string funName, params object[] objs)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ArgumentsUtils.CheckArgumentObjectNull("engine", engine);
            ArgumentsUtils.CheckArgumentNull("funName", funName);
            //ArgumentsUtils.CheckArgumentNull("objs", objs);
            return type.GetMethod("Invoke").Invoke(engine, new object[] { funName, objs });
#else
            throw new NotSupportedException();
#endif
        }
    }
}
