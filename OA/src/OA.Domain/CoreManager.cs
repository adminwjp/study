using Autofac;
using OA.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;

namespace OA.Domain
{
    public class CoreManager
    {
        public static void RegisterAutofac(bool aspnetcore = false)
        {
            if (!aspnetcore)
            {
                AutofacUtils.Builder.RegisterType<Microsoft.Extensions.Logging.LoggerFactory>().As<Microsoft.Extensions.Logging.ILoggerFactory>().SingleInstance();
            }
            AutofacUtils.Builder.RegisterType<AppSessionFactory>().SingleInstance();
            AutofacUtils.Builder.Register<NHibernate.IStatelessSession>(it=>it.Resolve<AppSessionFactory>().OpenSession()).InstancePerLifetimeScope();
            AutofacUtils.Builder.RegisterGeneric(typeof(NhibernateRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
       
        //public void InitModule()
        //{
        //    List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
        //    moduleInfos.AddRange(new ModuleInfo[] {
        //        new ModuleInfo()
        //        {
        //            Name="人事管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="档案管理"},
        //                new ModuleInfo(){ Name="考勤管理"},
        //                new ModuleInfo(){ Name="奖惩管理"},
        //                new ModuleInfo(){ Name="培训管理"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="待遇管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="账套管理"},
        //                new ModuleInfo(){ Name="人员设置"},
        //                new ModuleInfo(){ Name="统计报表"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="系统维护",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="企业架构"},
        //                new ModuleInfo(){ Name="基本资料"},
        //                new ModuleInfo(){ Name="初始化系统"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="用户管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="新增用户"}
        //            }
        //        },
        //         new ModuleInfo()
        //        {
        //            Name="系统工具",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="打开计算器"},
        //                new ModuleInfo(){ Name="打开Word"},
        //                new ModuleInfo(){ Name="打开Excel"}
        //            }
        //        }
        //    }
        //    );
        //}
    }
}
