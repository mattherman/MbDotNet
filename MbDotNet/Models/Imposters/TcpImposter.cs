using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the TCP protocol
	/// </summary>
	public class TcpImposter : Imposter
	{
		/// <summary>
		/// The stubs defined for this imposter
		/// </summary>
		[JsonProperty("stubs")]
		public ICollection<TcpStub> Stubs { get; private set; }

		/// <summary>
		/// The encoding used for request and response strings
		/// </summary>
		[JsonProperty("mode")]
		public string Mode { get; private set; }

		/// <summary>
		/// Optional default response that imposter sends back if no predicate matches a request
		/// </summary>
		[JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
		public TcpResponseFields DefaultResponse { get; private set; }

		/// <summary>
		/// Create a new TcpImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="mode">The encoding used for request and response strings</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		/// <param name="defaultResponse">An optional default response for when no predicates match a request</param>
		public TcpImposter(int? port, string name, TcpMode mode, bool recordRequests = false, TcpResponseFields defaultResponse = null)
			: base(port, Enums.Protocol.Tcp, name, recordRequests)
		{
			Stubs = new List<TcpStub>();
			Mode = mode.ToString().ToLower();
			DefaultResponse = defaultResponse;
		}

		/// <summary>
		/// Add an empty stub to this imposter
		/// </summary>
		/// <returns>The new stub</returns>
		public TcpStub AddStub()
		{
			var stub = new TcpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
