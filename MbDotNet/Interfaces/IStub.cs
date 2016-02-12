using System.Collections.Generic;
using System.Net;

namespace MbDotNet.Interfaces
{
    public interface IStub
    {
        ICollection<IResponse> Responses { get; }

        IStub ReturnsStatus(HttpStatusCode statusCode);
        IStub ReturnsJson(HttpStatusCode statusCode, object responseObject);
        IStub ReturnsXml(HttpStatusCode statusCode, object responseObject);
        IStub Returns(IResponse response);
    }
}
