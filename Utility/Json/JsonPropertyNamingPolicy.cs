#if NETCORE2_0 || NETCORE2_1 || NETCORE2_2 || NETCORE3_0 || NETCORE3_1 || NETCORE3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utility
{
    public class JsonPropertyNamingPolicy : JsonNamingPolicy
    {
        public static new JsonNamingPolicy CamelCase
        {
            get { return ToupperNotParseLine ? JsonResolverObject : ObjectResolverJson; }
        }
        public override string ConvertName(string name)
        {
            return ResolvePropertyName(name);
        }
        public static bool ToupperNotParseLine;
        /// <summary>
        /// JsonResolverObject
        /// </summary>
        public static readonly JsonNamingPolicy JsonResolverObject = new JsonPropertyNamingPolicy(true);
        /// <summary>
        /// ObjectResolverJson
        /// </summary>
        public static readonly JsonNamingPolicy ObjectResolverJson = new JsonPropertyNamingPolicy(false);
        readonly bool _jsonResolverObject;
        /// <summary>
        /// constructor
        /// </summary>
        public JsonPropertyNamingPolicy()
        {
            _jsonResolverObject = false;
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="jsonResolverObject"></param>
        public JsonPropertyNamingPolicy(bool jsonResolverObject)
        {
            _jsonResolverObject = jsonResolverObject;
        }
        /// <summary>
        /// property name resolver
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected  string ResolvePropertyName(string propertyName)
        {
            string str = propertyName;
            if (this._jsonResolverObject)
            {
                return StringUtils.Parse(str, StringFormat.InitialLetterLowerCaseUpper);

            }
            else
            {
                return StringUtils.Parse(str, StringFormat.InitialLetterUpperCaseLower);
            }
        }
    }
}
#endif