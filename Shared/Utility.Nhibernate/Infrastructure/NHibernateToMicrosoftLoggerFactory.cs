#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1

using NHibernate;
using System;

namespace Utility.Nhibernate.Infrastructure
{
	public class NHibernateToMicrosoftLoggerFactory : INHibernateLoggerFactory
	{
		private readonly Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;

		public NHibernateToMicrosoftLoggerFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
		}

		public INHibernateLogger LoggerFor(string keyName)
		{
			var msLogger = _loggerFactory.CreateLogger(keyName);
			return new NHibernateToMicrosoftLogger(msLogger);
		}

		public INHibernateLogger LoggerFor(System.Type type)
		{
            //return LoggerFor(Microsoft.Extensions.Logging.Abstractions.Internal.TypeNameHelper.GetTypeDisplayName(type));
            return LoggerFor(type.Name);
        }
	}
}
#endif
