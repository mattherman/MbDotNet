using System.Collections.Generic;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	public class HttpsImposter : Imposter
	{
		[JsonProperty("stubs")]
		public ICollection<HttpStub> Stubs { get; private set; }

		[JsonProperty("cert", NullValueHandling = NullValueHandling.Ignore)]
		public string Cert { get; private set; }

		[JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key { get; private set; }

		[JsonProperty("mutualAuth")]
		public bool MutualAuthRequired { get; private set; }

		/// <summary>
		/// Optional default response that imposter sends back if no predicate matches a request
		/// </summary>
		[JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
		public HttpResponseFields DefaultResponse { get; private set; }

		/// <summary>
		/// Enables CORS requests when set to true, false by default
		/// </summary>
		[JsonProperty("allowCORS")]
		public bool AllowCORS { get; private set; }

		public HttpsImposter(int? port, string name, bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false)
			: this(port, name, null, null, false, recordRequests, defaultResponse, allowCORS)
		{
		}

		public HttpsImposter(int? port, string name, string key, string cert, bool mutualAuthRequired,
			bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false) : base(port, Enums.Protocol.Https, name, recordRequests)
		{
			Cert = cert;
			Key = key;
			MutualAuthRequired = mutualAuthRequired;
			Stubs = new List<HttpStub>();
			DefaultResponse = defaultResponse;
			AllowCORS = allowCORS;
		}

		public HttpStub AddStub()
		{
			var stub = new HttpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
