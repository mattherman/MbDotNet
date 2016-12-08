using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests
{
    class Program
    {
        public static MountebankClient Client { get; set; }

        static void Main(string[] args)
        {
            SetupTestEnvironment();

            RunAcceptanceTests();
        }

        private static void RunAcceptanceTests()
        {
            AcceptanceTest.CanCreateImposter(Client);
            AcceptanceTest.CanDeleteImposter(Client);
        }

        private static void SetupTestEnvironment()
        {
            Client = new MountebankClient();
        }
    }
}
