using System;
using System.Collections.Generic;
using System.Text;

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
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		public SmtpImposter(int? port, string name, bool recordRequests = false)
			: base(port, Enums.Protocol.Smtp, name, recordRequests)
		{
		}
	}
}
