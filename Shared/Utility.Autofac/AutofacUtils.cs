using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public  class AutofacUtils
    {
        private static ContainerBuilder _builder;
        public static bool Register { get; set; } = true;
        public static ContainerBuilder Builder//申明容器
        {
            get
            {
                if (_builder == null)
                {
                    _builder = new ContainerBuilder();//实例化
                }
                return _builder;
            }
        }
        public static void Registe(Action<ContainerBuilder> action)
        {
            action?.Invoke(AutofacUtils.Builder);
        }
        private static IContainer _container;//申明一个字段这个字段用来对接容器

        public static IContainer Container //将对接的内容传输入这个属性！
        {
            get
            {
                if (_container == null)
                {
                    _container = Builder.Build();
                }
                return _container;
            }
        }
        public static T Resolve<T>()//定义一个方法在外部调用
        {
            T t = Container.Resolve<T>();//回传已经被注册在容器内的类
            return t;
        }
    }
}
