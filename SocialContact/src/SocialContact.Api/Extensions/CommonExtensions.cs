using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Extensions
{
    public static  class CommonExtensions
    {
        public static IDictionary<string, List<string>> Errors(this ModelStateDictionary dictionary)
        {
            IDictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
            using (IEnumerator<string> keys = (IEnumerator<string>)dictionary.Keys.GetEnumerator())
            {
                using (IEnumerator<ModelStateEntry> values = (IEnumerator<ModelStateEntry>)dictionary.Values.GetEnumerator())
                {
                    while (keys.MoveNext() && values.MoveNext())
                    {
                        string key = keys.Current;
                        errors.Add(key, new List<string>());
                        foreach (var item in values.Current.Errors)
                        {
                            errors[key].Add(item.ErrorMessage);
                        }
                    }
                }
            }
            return errors;
        }
        public static IDictionary<string, List<string>> Errors(this SerializableError dictionary)
        {
            IDictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
            using (IEnumerator<string> keys = (IEnumerator<string>)dictionary.Keys.GetEnumerator())
            {
                using (IEnumerator<object> values = (IEnumerator<object>)dictionary.Values.GetEnumerator())
                {
                    while (keys.MoveNext() && values.MoveNext())
                    {
                        string key = keys.Current;
                        errors.Add(key, new List<string>());
                        errors[key].AddRange(values.Current as string[]);
                    }
                }
            }
            return errors;
        }
    }
}
