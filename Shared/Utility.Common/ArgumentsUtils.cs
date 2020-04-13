using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utility;

namespace Utility
{
    /// <summary>
    /// argument  check 
    /// </summary>
    public class ArgumentsUtils
    {
        /// <summary>
        /// check argument
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckArgumentNull(string name,string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                throw new ArgumentNullException(name);
            }
        }
        /// <summary>
        /// check argument
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckArgumentObjectNull<T>(string name, T value)where T:class
        {
            if (value==null)
            {
                throw new ArgumentNullException(name);
            }
        }
        /// <summary>
        /// check argument is null or length eq 0
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckArgumentArrayNull<T>(string name, T[] value)
        {
            if (value == null||value.Length==0)
            {
                throw new ArgumentNullException(name);
            }
        }
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        public static ResponseApi CheckError(Type type, object obj,int flag=0,Language language= Language.Chinese)
        {
            Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
            var properties = type.GetProperties();
            Action<string,string> action = (name, msg) =>{
                if (!errors.ContainsKey(name))
                    errors.Add(name, new List<string>());
                errors[name].Add(msg);
            };
             for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var value = property.GetValue(obj,null);
#if !(NET20 || NET30 || NET35 || NET40)
                var required = property.GetCustomAttribute<RequiredAttribute>();
#else
                var required=AttributeUtils.Get<RequiredAttribute>(property.GetCustomAttributes(true));
#endif
                string str = value?.ToString();
                if (required != null&&(value==null|| string.IsNullOrEmpty(str)))
                {
                    action(property.Name, required.Message);
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var range = property.GetCustomAttribute<RangeAttribute>();
#else
                var range=AttributeUtils.Get<RangeAttribute>(property.GetCustomAttributes(true));
#endif
                if (range != null&&(value == null ||  (string.IsNullOrEmpty(str) && !(str.Length >= range.Min && str.Length <= range.Max))))
                {
                    action(property.Name, range.Message);
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var optionValidator = property.GetCustomAttribute<OptionValidatorAttribute>();
#else
                var optionValidator=AttributeUtils.Get<OptionValidatorAttribute>(property.GetCustomAttributes(true));
#endif
                if (optionValidator != null && optionValidator.Options
                    && (value == null || string.IsNullOrEmpty(str)))
                {
                    if (optionValidator.AddError != null)
                    {
                        errors = optionValidator.AddError(errors);
                    }
                    if (optionValidator.Way == (ValidatorWay)flag)
                    {
                        action(property.Name, optionValidator.Message);
                    }
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var validator = property.GetCustomAttribute<ValidatorAttribute>();
#else
                var validator=AttributeUtils.Get<ValidatorAttribute>(property.GetCustomAttributes(true));
#endif
                Exception exception = null;
                if (validator != null&&(exception = validator.Validator?.Invoke(value))!=null)
                {
                    action(property.Name, exception.Message);
                }
            }

            var fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var property = fields[i];
                var value = property.GetValue(obj);
#if !(NET20 || NET30 || NET35 || NET40)
                var required = property.GetCustomAttribute<RequiredAttribute>();
#else
                var required=AttributeUtils.Get<RequiredAttribute>(property.GetCustomAttributes(true));
#endif
                string str = value?.ToString();
                if (required != null && (value == null || string.IsNullOrEmpty(str)))
                {
                    action(property.Name, required.Message);
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var range = property.GetCustomAttribute<RangeAttribute>();
#else
                var range=AttributeUtils.Get<RangeAttribute>(property.GetCustomAttributes(true));
#endif
                if (range != null && (value == null || (string.IsNullOrEmpty(str) && !(str.Length >= range.Min && str.Length <= range.Max))))
                {
                    action(property.Name, range.Message);
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var optionValidator = property.GetCustomAttribute<OptionValidatorAttribute>();
#else
                var optionValidator=AttributeUtils.Get<OptionValidatorAttribute>(property.GetCustomAttributes(true));
#endif
                if (optionValidator != null && optionValidator.Options
                    && (value == null || string.IsNullOrEmpty(str)))
                {
                    if (optionValidator.AddError != null)
                    {
                        errors = optionValidator.AddError(errors);
                    }
                    if (optionValidator.Way == (ValidatorWay)flag)
                    {
                        action(property.Name, optionValidator.Message);
                    }
                }
#if !(NET20 || NET30 || NET35 || NET40)
                var validator = property.GetCustomAttribute<ValidatorAttribute>();
#else
                var validator=AttributeUtils.Get<ValidatorAttribute>(property.GetCustomAttributes(true));
#endif
                Exception exception = null;
                if (validator != null && (exception = validator.Validator?.Invoke(value)) != null)
                {
                    action(property.Name, exception.Message);
                }
            }
            if (errors.Count > 0)
            {
                Utility.ResponseApi response = ResponseApiUtils.GetResponse(language, Utility.Code.ParamError);
                response.Data = new Dictionary<string, Dictionary<string, List<string>>>() { ["Error"] = errors };
                return response;
            }
            return  null ;
        }
#endif
    }
}
