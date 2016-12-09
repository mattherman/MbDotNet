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

            int resultCode = RunAcceptanceTests();

            Environment.Exit(resultCode);
        }

        private static int RunAcceptanceTests()
        {
            try
            {
                AcceptanceTest.CanCreateImposter(Client);
                AcceptanceTest.CanDeleteImposter(Client);
                AcceptanceTest.CanGetImposter(Client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
            return 0;
        }

        private static void SetupTestEnvironment()
        {
            Client = new MountebankClient();
        }
    }
}
