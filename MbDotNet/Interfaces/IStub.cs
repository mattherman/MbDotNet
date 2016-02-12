using System.Collections.Generic;
using System.Net;

namespace MbDotNet.Interfaces
{
    public interface IStub
    {
        ICollection<IResponse> Responses { get; }

        IStub ReturnsStatus(HttpStatusCode statusCode);
        IStub ReturnsJson<T>(HttpStatusCode statusCode, T responseObject);
        IStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject);
        IStub Returns(IResponse response);
    }
}
