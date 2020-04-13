#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0  || NETCOREAPP3_1 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using AutoMapper;

namespace Utility
{
    public  class AutoMapperUtils
    {
        private static  IMapper _mapper;
        public static Mapper Mapper => (Mapper)_mapper;
        public static void Init(Action<IMapperConfigurationExpression> configure)
        {
            IConfigurationProvider _config = new MapperConfiguration(cfg => {
                configure.Invoke(cfg);
            });
            _mapper = _config.CreateMapper();
        }

        /// <summary>
        ///  类型映射
        /// </summary>
        public static T MapTo<T>(object obj)
        {
            if (obj == null) return default(T);
            return _mapper.Map<T>(obj);
        }
    }
}
#endif