﻿using Microsoft.Extensions.Logging;
using OA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Utility.Domain.Uow;
using Utility.Ioc;
using Utility.ObjectMapping;
using Utility.Utils;
using Utility.Wpf.Attributes;
using OA.Wpf.ViewModels;

namespace Wpf.OA
{
    public class OANHibernateManager
    {
        public static void Initial()
        {
            AutoMapperObjectMapper.Empty.Init(it => {
                foreach (var item in typeof(FamousRaceViewModel).Module.GetTypes())
                {
                    var target = AttributeUtils.Get<MappTypeAttribute>(item.GetCustomAttributes(false));
                    if (target != null)
                    {
                        it.CreateMap(target.Type, item);  //oa model - viewmodel
                        it.CreateMap(item, target.Type);   //oa viewmodel - model
                    }
                }
            });
        }
        public static List<T> FindList<T>(int page,int size)where T:class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            var data = unitWork.Find<T>(null).Skip((page-1)*size).Take(size).ToList();
            return data??new List<T>();
        }
        public static int Count<T>() where T : class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            var data = unitWork.Count<T>(null);
            return data;
        }
        public static int Add<T>(T obj) where T : class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            var id = unitWork.Insert(obj);
            return (int)id;
        }

        public static void Save<T>(T obj) where T : class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            unitWork.Update(obj);
        }
        public static void Delete<T>(T obj) where T : class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            unitWork.Delete(obj);
        }
        public static void Delete<T>(T[] obj) where T : class
        {
            IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            unitWork.Delete(obj);
        }
    }
}
