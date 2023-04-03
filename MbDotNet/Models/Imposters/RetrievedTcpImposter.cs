using System;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// A retrieved imposter using the TCP protocol
	/// </summary>
	public class RetrievedTcpImposter : RetrievedImposter<TcpRequest, TcpResponseFields>
	{
		[JsonProperty("mode")]
		internal string RawMode { get; set; }

		/// <summary>
		/// The configured encoding for request and response strings
		/// </summary>
		public TcpMode Mode => string.Equals(RawMode, "text", StringComparison.CurrentCultureIgnoreCase) ? TcpMode.Text : TcpMode.Binary;
	}
}
