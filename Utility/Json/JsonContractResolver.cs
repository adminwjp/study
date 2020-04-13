using Newtonsoft.Json.Serialization;
using System.Text;

namespace Utility
{
    /// <summary>
    /// json resolver
    /// </summary>
    public class JsonContractResolver: DefaultContractResolver
    {
        /// <summary>
        /// JsonResolverObject ABC a_b_c
        /// </summary>
        public static readonly IContractResolver JsonResolverObject = new JsonContractResolver(true);
        /// <summary>
        /// ObjectResolverJson a_b_c ABC
        /// </summary>
        public static readonly IContractResolver ObjectResolverJson = new JsonContractResolver();
        readonly bool _jsonResolverObject = false;
        /// <summary>
        /// constructor
        /// </summary>
        public JsonContractResolver()
        {

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="jsonResolverObject"></param>
        public JsonContractResolver(bool jsonResolverObject)
        {
            _jsonResolverObject = true;
        }
        /// <summary>
        /// property name resolver
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            string str=base.ResolvePropertyName(propertyName);
            if (this._jsonResolverObject)
            {
                return StringUtils.Parse(str,StringFormat.InitialLetterLowerCaseUpper);
               
            }
            else
            {
                return StringUtils.Parse(str, StringFormat.InitialLetterUpperCaseLower);
            }
        }
    }
}
