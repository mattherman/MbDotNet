using MbDotNet.Enums;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// Options for configuring TCP imposters.
	/// </summary>
	public class TcpImposterOptions
	{
		/// <inheritdoc cref="TcpImposter.DefaultResponse" />
		public TcpResponseFields DefaultResponse { get; set; }

		/// <inheritdoc cref="TcpImposter.RecordRequests" />
		public bool RecordRequests { get; set; }

		/// <inheritdoc cref="TcpImposter.Mode" />
		public TcpMode Mode { get; set; }
	}
}
