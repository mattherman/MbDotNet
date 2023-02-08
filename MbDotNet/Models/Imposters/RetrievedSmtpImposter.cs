using System;
using System.Collections.Generic;
using System.Text;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// A retrieved imposters using the SMTP protocol
	/// </summary>
	public class RetrievedSmtpImposter : RetrievedImposter<SmtpRequest, SmtpResponseFields>
	{
	}
}
