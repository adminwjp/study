#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Internal;
using NHibernate;

namespace Utility.Nhibernate.Infrastructure
{
	public class NHibernateToMicrosoftLogger : INHibernateLogger
	{
		private readonly ILogger _msLogger;

		public NHibernateToMicrosoftLogger(ILogger msLogger)
		{
			_msLogger = msLogger ?? throw new ArgumentNullException(nameof(msLogger));
		}

		private static readonly Dictionary<NHibernateLogLevel, Microsoft.Extensions.Logging.LogLevel> MapLevels = new Dictionary<NHibernateLogLevel, Microsoft.Extensions.Logging.LogLevel>
		{
			{ NHibernateLogLevel.Trace, Microsoft.Extensions.Logging.LogLevel.Trace },
			{ NHibernateLogLevel.Debug, Microsoft.Extensions.Logging.LogLevel.Debug },
			{ NHibernateLogLevel.Info, Microsoft.Extensions.Logging.LogLevel.Information },
			{ NHibernateLogLevel.Warn, Microsoft.Extensions.Logging.LogLevel.Warning },
			{ NHibernateLogLevel.Error, Microsoft.Extensions.Logging.LogLevel.Error },
			{ NHibernateLogLevel.Fatal, Microsoft.Extensions.Logging.LogLevel.Critical },
			{ NHibernateLogLevel.None, Microsoft.Extensions.Logging.LogLevel.None },
		};

		public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
		{
            //_msLogger.Log(MapLevels[logLevel], 0, new FormattedLogValues(state.Format, state.Args), exception, MessageFormatter);
            _msLogger.Log(MapLevels[logLevel], 0, (object)state, exception, MessageFormatter);
        }

		public bool IsEnabled(NHibernateLogLevel logLevel)
		{
			return _msLogger.IsEnabled(MapLevels[logLevel]);
		}

		private static string MessageFormatter(object state, Exception error)
		{
			return state.ToString();
		}
	}
}
#endif