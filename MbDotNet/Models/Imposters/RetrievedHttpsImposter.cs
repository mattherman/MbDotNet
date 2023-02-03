using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

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
		[JsonProperty("key")]
		public string Key { get; internal set; }

		/// <summary>
		/// The configured SSL certificate
		/// </summary>
		[JsonProperty("cert")]
		public string Cert { get; internal set; }

		/// <summary>
		/// The configured mutual auth setting
		/// </summary>
		[JsonProperty("mutualAuth")]
		public bool MutualAuthRequired { get; internal set; }
	}
}
