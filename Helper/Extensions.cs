using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltnCrossAPI.Helper
{
    public static class Extensions
    {
        public static List<KeyValuePair<string, StringValues>> ToKvps(this System.Collections.Specialized.NameValueCollection qs)
        {
            Dictionary<string, string> parameters = qs.Keys.Cast<string>().ToDictionary(key => key, value => qs[value]);
            var kvps = new List<KeyValuePair<string, StringValues>>();

            parameters.ToList().ForEach(x =>
            {
                kvps.Add(new KeyValuePair<string, StringValues>(x.Key, new StringValues(x.Value)));
            });

            return kvps;
        }
    }
}