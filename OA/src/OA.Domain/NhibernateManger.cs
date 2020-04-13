using Microsoft.Extensions.Logging;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Utility.Nhibernate;

namespace OA.Domain
{
    public class NhibernateManger
    {
        public static void Initial(ILoggerFactory loggerFactory)
        {
            NhibernateHelper.Init(config => {
                //config = config.Configure();
                config.Interceptor = new SQLWatcher(loggerFactory);
                //Enable validation(optional)
                NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
                //Here, we serialize all decorated classes(but you can also do it class by class)
                config.Properties["connection.driver_class"] = "NHibernate.Driver.MySqlDataDriver";
                config.Properties["connection.connection_string"] = "Database = oa; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;";
                config.Properties["dialect"] = "NHibernate.Dialect.MySQL5Dialect";
                config.Properties["use_sql_comments"] = "true";
                config.Properties["command_timeout"] = "30";
                config.Properties["adonet.batch_size"] = "100";
                config.Properties["order_inserts"] = "true";
                config.Properties["order_updates"] = "true";
                config.Properties["adonet.batch_versioned_data"] = "true";
                config.Properties["show_sql"] = "true";
                config.Properties["format_sql"] = "true";
                config.Properties["hbm2ddl.auto"] = "update";
                config.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(Assembly.Load("OA.Domain")));
                //HbmSerializer.Default.Serialize(Assembly.Load("OA.Domain"), "Config/hibernate-oa.cfg.xml");
                //config.AddXmlFile("Config/hibernate-oa.cfg.xml");
                config.SessionFactory().GenerateStatistics();
                //用NHibernate.Tool.hbm2ddl.SchemaExport生成表结构到\sql.sql文件当中
                SchemaExport export = new SchemaExport(config);
                export.SetOutputFile(Path.Combine(Directory.GetCurrentDirectory(), "sql.sql")); //设置输出目录
                // export.Drop(true, true);//设置生成表结构存在性判断,并删除
                export.Create(false, false);//设置是否生成脚本,是否导出来
            });
        }
    }
}
