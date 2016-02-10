using System.Collections.Generic;
using System.Net;

namespace MbDotNet.Interfaces
{
    public interface IStub
    {
        ICollection<IResponse> Responses { get; }

        IStub Returns(HttpStatusCode statusCode);
        IStub Returns(HttpStatusCode statusCode, object responseObject);
    }
}
