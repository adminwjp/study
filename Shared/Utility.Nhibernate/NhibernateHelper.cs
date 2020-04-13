#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

namespace Utility.Nhibernate
{
    /// <summary>
    /// NHibernate 帮助类
    /// </summary>
    public class NhibernateHelper
    {
        private static Configuration configuration;//NHibernate 配置
        private static ISessionFactory sessionFactory;//NHibernate ISessionFactory
        private static readonly object obj = new object();//锁
        private static readonly IInterceptor _interceptor = new SqlInterceptor();// NHibernate 拦截器

        /// <summary>
        ///NHibernate ISessionFactory
        /// </summary>
        public static ISessionFactory SessionFactory {
            get {
                return sessionFactory;
            }
        }
        

        /// <summary>
        /// 初始化 配置信息
        /// </summary>
        /// <param name="action"></param>
        public static void Init(Action<Configuration> action)
        {
            if (sessionFactory == null)
            {
                lock (obj)
                {
                    if (sessionFactory == null)
                    {
                        var config = new Configuration();
                        action(config); 
                        config.Interceptor = _interceptor;
                        sessionFactory =config.BuildSessionFactory();
                    }
                }
            }
        }
        /// <summary>
        /// 初始化 配置信息
        /// </summary>
        /// <param name="assembly"></param>
        public static void Init(System.Reflection.Assembly assembly)
        {
            var mapper = new ModelMapper();
            foreach (Module item in assembly.Modules)
            {
                foreach (Type type in item.GetTypes())
                {
                    if (type.IsClass)
                    {
                        if (type.Name.EndsWith("Map"))
                        {
                            mapper.AddMapping(type);
                        }
                    }
                }
            }
            configuration = new Configuration();
            configuration.DataBaseIntegration(it =>
            {
                it.ConnectionString = "Data Source=localhost;Initial Catalog=gibson;Integrated Security=True;Connect Timeout=150;Encrypt=False;uid=sa;pwd=wjp930514*;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                //控制台程序 https://www.cnblogs.com/kissdodog/p/4564204.html
                it.LogFormattedSql = true;
                it.LogSqlInConsole = true;
                it.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                it.Driver<SqlClientDriver>();
                it.Dialect<MsSql2012Dialect>();
                it.SchemaAction = SchemaAutoAction.Update;
               
            })
            .AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            configuration.Interceptor = _interceptor;
            configuration.SessionFactory().GenerateStatistics();
            //用NHibernate.Tool.hbm2ddl.SchemaExport生成表结构到D:\sql.sql文件当中
            SchemaExport export = new SchemaExport(configuration);
            export.SetOutputFile("D:\\sql.sql"); //设置输出目录
            // export.Drop(true, true);//设置生成表结构存在性判断,并删除
            //export.Create(false, false);//设置是否生成脚本,是否导出来
            sessionFactory = configuration.BuildSessionFactory();
        }
        /// <summary>获取Session会话 </summary>
        /// <returns></returns>
        public static IStatelessSession Session
        {
            get
            {
                return SessionFactory.OpenStatelessSession();
            }
        }   /// <summary>获取Session会话 </summary>
            /// <returns></returns>
        public static ISession OpenSession(EmptyInterceptor interceptor)
        {
            return SessionFactory.OpenSession(interceptor);
        }
        /// <summary>
        /// 获取Session会话
        /// </summary>
        public static IStatelessSessionBuilder SessionBuilder
        {
            get
            {
                return SessionFactory.WithStatelessOptions();
            }
        }
        /// <summary>
        /// 获取Session会话
        /// </summary>
        /// <param name="autoJoinTransaction"></param>
        /// <returns></returns>
        public static IStatelessSessionBuilder GetSessionBuilder(bool autoJoinTransaction)
        {
            return SessionBuilder.AutoJoinTransaction(autoJoinTransaction);
        }
        /// <summary>
        /// 获取Session会话
        /// </summary>
        /// <param name="autoJoinTransaction"></param>
        /// <returns></returns>
        public static IStatelessSession GetSession(bool autoJoinTransaction)
        {
            return GetSessionBuilder(autoJoinTransaction).OpenStatelessSession();
        }
    }
}
#endif
