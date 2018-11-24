using System;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests
{
    internal abstract class AcceptanceTest
    {

        protected readonly MountebankClient _client;

        public AcceptanceTest()
        {
            _client = new MountebankClient();
        }        

        public abstract Task Run();
    }
}
