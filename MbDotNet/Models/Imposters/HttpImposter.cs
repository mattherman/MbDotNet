using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the HTTP protocol
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "CORS is an abbreviation")]
	public class HttpImposter : Imposter, IWithStubs<HttpStub>, IWithResponseFields<HttpResponseFields>
	{
		/// <summary>
		/// The stubs defined for this imposter
		/// </summary>
		[JsonProperty("stubs")]
		public ICollection<HttpStub> Stubs { get; private set; }

		/// <inheritdoc />
		[JsonProperty("defaultResponse")]
		public HttpResponseFields DefaultResponse { get; }

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
		public HttpImposter(int? port, string name, bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false)
			: base(port, Enums.Protocol.Http, name, recordRequests)
		{
			Stubs = new List<HttpStub>();
			AllowCORS = allowCORS;
			DefaultResponse = defaultResponse;
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
