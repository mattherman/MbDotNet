using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// A retrieved imposter using the HTTPS protocol
	/// </summary>
	public class RetrievedHttpsImposter : RetrievedImposter<HttpRequest, HttpResponseFields>
	{
		/// <summary>
		/// The configured SSL private key
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("key")]
		public string Key { get; internal set; }

		/// <summary>
		/// The configured SSL certificate
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("cert")]
		public string Cert { get; internal set; }

		/// <summary>
		/// The configured mutual auth setting
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("mutualAuth")]
		public bool MutualAuthRequired { get; internal set; }
	}
}
