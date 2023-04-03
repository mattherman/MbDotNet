namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the SMTP protocol
	/// </summary>
	public class SmtpImposter : Imposter
	{
		/// <summary>
		/// Create a new SmtpImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="options">Options for configuring the imposter</param>
		public SmtpImposter(int? port, string name, SmtpImposterOptions options)
			: base(port, Imposters.Protocol.Smtp, name, options?.RecordRequests ?? false)
		{
		}
	}
}
