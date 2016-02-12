using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class EqualsPredicate : IPredicate
    {
        [JsonProperty("equals")]
        private readonly EqualsPredicateDetail _detail;

        [JsonIgnore]
        public string Path { get { return _detail.Path; } }

        [JsonIgnore]
        public string RequestBody { get { return _detail.RequestBody; } }

        [JsonIgnore]
        public string Method { get { return _detail.Method; } }

        [JsonIgnore]
        public Dictionary<string, string> Headers { get { return _detail.Headers; } }
 
        [JsonIgnore]
        public Dictionary<string, string> QueryParameters { get { return _detail.QueryParameters; } } 

        public EqualsPredicate(string path, Method? method, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParameters)
        {
            _detail = new EqualsPredicateDetail
            {
                Path = path,
                Method = method == null ? null : method.ToString().ToUpper(),
                RequestBody = body,
                Headers = headers,
                QueryParameters = queryParameters
            };
        }

        internal class EqualsPredicateDetail
        {
            [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
            public string Path { get; set; }

            [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
            public string RequestBody { get; set; }

            [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
            public string Method { get; set; }

            [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
            public Dictionary<string, string> Headers { get; set; }

            [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
            public Dictionary<string, string> QueryParameters { get; set; }
        }
    }
}
