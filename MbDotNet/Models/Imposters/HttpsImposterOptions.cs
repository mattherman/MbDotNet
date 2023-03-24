using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// Options for configuring HTTP imposters.
	/// </summary>
	public class HttpsImposterOptions
	{
		/// <inheritdoc cref="HttpsImposter.DefaultResponse" />
		public HttpResponseFields DefaultResponse { get; set; }

		/// <inheritdoc cref="HttpsImposter.AllowCORS" />
		public bool AllowCORS { get; set; }

		/// <inheritdoc cref="HttpsImposter.RecordRequests" />
		public bool RecordRequests { get; set; }

		/// <inheritdoc cref="HttpsImposter.Key" />
		public string Key { get; set; }

		/// <inheritdoc cref="HttpsImposter.Cert" />
		public string Cert { get; set; }

		/// <inheritdoc cref="HttpsImposter.MutualAuthRequired" />
		public bool MutualAuthRequired { get; set; }
	}
}
