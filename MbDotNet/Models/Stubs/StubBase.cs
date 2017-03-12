using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
using Newtonsoft.Json;

namespace MbDotNet.Models.Stubs
{
    public abstract class StubBase
    {
        /// <summary>
        /// A collection of all of the responses set up on this stub.
        /// </summary>
        [JsonProperty("predicates", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<PredicateBase> Predicates { get; set; }

        /// <summary>
        /// A collection of all of the predicates set up on this stub.
        /// </summary>
        [JsonProperty("responses", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ResponseBase> Responses { get; set; }

        public StubBase()
        {
            Responses = new List<ResponseBase>();
            Predicates = new List<PredicateBase>();
        }
    }
}
