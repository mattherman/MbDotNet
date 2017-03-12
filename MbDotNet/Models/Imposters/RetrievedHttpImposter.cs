using MbDotNet.Models.Requests;

namespace MbDotNet.Models.Imposters
{
    public class RetrievedHttpImposter : RetrievedImposter<HttpRequest>
    {
        public override HttpRequest[] Requests { get; protected set; }
    }
}
