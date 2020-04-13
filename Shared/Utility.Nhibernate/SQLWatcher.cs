#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Nhibernate
{
    public class SQLWatcher : EmptyInterceptor
    {
        private readonly Microsoft.Extensions.Logging.ILogger<SQLWatcher> _logger;
        public SQLWatcher(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SQLWatcher>();
        }
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            _logger.LogInformation($"sql语句:{sql}" );
            return base.OnPrepareStatement(sql);
        }
    }
}
#endif