using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// Options for configuring HTTP imposters.
	/// </summary>
	public class HttpImposterOptions
	{
		/// <inheritdoc cref="HttpImposter.DefaultResponse" />
		public HttpResponseFields DefaultResponse { get; set; }

		/// <inheritdoc cref="HttpImposter.AllowCORS" />
		public bool AllowCORS { get; set; }

		/// <inheritdoc cref="HttpImposter.RecordRequests" />
		public bool RecordRequests { get; set; }
	}
}
