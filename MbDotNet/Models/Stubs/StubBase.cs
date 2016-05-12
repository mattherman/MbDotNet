using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Interfaces;
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

        /// <summary>
        /// Adds a predicate to the stub
        /// </summary>
        /// <param name="predicate">The predicate object designating what the stub will match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public StubBase On(PredicateBase predicate)
        {
            Predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Adds a response to the stub.
        /// </summary>
        /// <param name="response">The response object designating what the stub will return</param>
        /// <returns>The stub that the response was added to</returns>
        public StubBase Returns(ResponseBase response)
        {
            Responses.Add(response);
            return this;
        }
    }
}
