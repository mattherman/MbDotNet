using System;
using System.Collections.Generic;
using System.Text;

namespace MbDotNet.Models.Imposters
{
    public class SmtpImposter: Imposter
    {
        public SmtpImposter(int? port, string name, bool recordRequests = false)
            : base(port, Enums.Protocol.Smtp, name, recordRequests)
        {
        }
    }
}
