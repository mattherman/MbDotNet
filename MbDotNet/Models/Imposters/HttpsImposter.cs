using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the HTTPS protocol
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "CORS is an abbreviation")]
	public class HttpsImposter : Imposter
	{
		/// <summary>
		/// The stubs defined for this imposter
		/// </summary>
		[JsonProperty("stubs")]
		public ICollection<HttpStub> Stubs { get; private set; }

		/// <summary>
		/// An optional SSL certificate used by the imposter
		/// </summary>
		[JsonProperty("cert", NullValueHandling = NullValueHandling.Ignore)]
		public string Cert { get; private set; }

		/// <summary>
		/// An optional SSL private key used by the imposter
		/// </summary>
		[JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key { get; private set; }

		/// <summary>
		/// The server will request a client certificate if enabled
		/// </summary>
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

		/// <summary>
		/// Create a new HttpImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		/// <param name="defaultResponse">An optional default response for when no predicates match a request</param>
		/// <param name="allowCORS">Enables CORS requests, defaults to false</param>
		public HttpsImposter(int? port, string name, bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false)
			: this(port, name, null, null, false, recordRequests, defaultResponse, allowCORS)
		{
		}

		/// <summary>
		/// Create a new HttpsImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="mutualAuthRequired">Forces the server to request a client certificate, defaults to false</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		/// <param name="defaultResponse">An optional default response for when no predicates match a request</param>
		/// <param name="allowCORS">Enables CORS requests, defaults to false</param>
		/// <param name="key">An optional SSL private key used by the imposter</param>
		/// <param name="cert">An optional SSL certificate used by the imposter</param>
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

		/// <summary>
		/// Add an empty stub to this imposter
		/// </summary>
		/// <returns>The new stub</returns>
		public HttpStub AddStub()
		{
			var stub = new HttpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
