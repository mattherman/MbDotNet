using System.Collections;
using System.Collections.Generic;
using System.Net;
using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IImposter
    {
        int Port { get; }
        Protocol Protocol { get; }
        bool PendingSubmission { get; }
        ICollection<Response> Responses { get; }

        Imposter Returns(HttpStatusCode statusCode);
        Imposter Returns(HttpStatusCode statusCode, object responseObject);

        void Submit();
        void Delete();
    }
}
