using System.Collections.Generic;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the HTTP protocol
	/// </summary>
	public class HttpImposter : Imposter, IWithStubs<HttpStub>, IWithResponseFields<HttpResponseFields>
	{
		/// <summary>
		/// The stubs defined for this imposter
		/// </summary>
		[JsonProperty("stubs")]
		public ICollection<HttpStub> Stubs { get; private set; }

		/// <inheritdoc />
		[JsonProperty("defaultResponse")]
		public HttpResponseFields DefaultResponse { get; set;  }

		/// <summary>
		/// Enables CORS requests when set to true, false by default
		/// </summary>
		[JsonProperty("allowCORS")]
		public bool AllowCORS { get; set; }

		/// <summary>
		/// Create a new HttpImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="options">Options for configuring the imposter</param>
		public HttpImposter(int? port, string name, HttpImposterOptions options)
			: base(port, Enums.Protocol.Http, name, options?.RecordRequests ?? false)
		{
			Stubs = new List<HttpStub>();
			AllowCORS = options?.AllowCORS ?? false;
			DefaultResponse = options?.DefaultResponse;
		}

		/// <inheritdoc />
		public HttpStub AddStub()
		{
			var stub = new HttpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
