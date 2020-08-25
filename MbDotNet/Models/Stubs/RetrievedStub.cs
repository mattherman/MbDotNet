using System.Collections.Generic;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Stubs
{
    /// <summary>
    /// The base class for a retrieved stub.
    /// </summary>
    /// <typeparam name="TRequest">The request type this stub contains</typeparam>
    /// <typeparam name="TResponseFields">The response fields type this stub contains</typeparam>
    public class RetrievedStub<TRequest, TResponseFields>
        where TRequest : Request
        where TResponseFields : ResponseFields, new()
    {
        /// <summary>
        /// An collection of all activity by this stub.
        /// </summary>
        [JsonProperty("matches", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Match<TRequest, TResponseFields>> Matches { get; set; }

        public RetrievedStub()
        {
            Matches = new List<Match<TRequest, TResponseFields>>();
        }
    }
}