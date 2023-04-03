using System.Collections.Generic;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the TCP protocol
	/// </summary>
	public class TcpImposter : Imposter, IWithStubs<TcpStub>, IWithResponseFields<TcpResponseFields>
	{
		/// <inheritdoc />
		[JsonProperty("stubs")]
		public IList<TcpStub> Stubs { get; private set; }

		[JsonProperty("mode")]
		internal string ModeAsText;

		/// <summary>
		/// The encoding used for request and response strings
		/// </summary>
		public TcpMode Mode
		{
			get
			{
				switch (ModeAsText)
				{
					case "binary":
						return TcpMode.Binary;
					default:
						return TcpMode.Text;
				}
			}
			set => ModeAsText = value.ToString().ToLower();
		}

		/// <inheritdoc />
		[JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
		public TcpResponseFields DefaultResponse { get; set; }

		/// <summary>
		/// Create a new TcpImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="options">Options for configuring the imposter</param>
		public TcpImposter(int? port, string name, TcpImposterOptions options)
			: base(port, Imposters.Protocol.Tcp, name, options?.RecordRequests ?? false)
		{
			Mode = options?.Mode ?? TcpMode.Text;
			DefaultResponse = options?.DefaultResponse;
			Stubs = new List<TcpStub>();
		}

		/// <inheritdoc />
		public TcpStub AddStub()
		{
			var stub = new TcpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
