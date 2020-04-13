#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;

namespace Utility.Nhibernate.Infrastructure
{
    public class AppSessionFactory
	{
        private  NHibernate.EmptyInterceptor _interceptor;
        public AppSessionFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
		{
			NHibernate.NHibernateLogger.SetLoggersFactory(new NHibernateToMicrosoftLoggerFactory(loggerFactory));
            _interceptor = new SQLWatcher(loggerFactory);
        }

		public IStatelessSession OpenSession()
		{
            return Utility.Nhibernate.NhibernateHelper.Session;
        }
        public  ISession Session()
        {
            return Utility.Nhibernate.NhibernateHelper.OpenSession(_interceptor);
        }
    }
}
#endif