using System;

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
                AcceptanceTest.CanGetImposter(Client);
                AcceptanceTest.CanNotGetImposterThatDoesNotExist(Client);
                AcceptanceTest.CanCreateImposter(Client);
                AcceptanceTest.CanCreateHttpsImposter(Client);
                AcceptanceTest.CanDeleteImposter(Client);
                AcceptanceTest.CanVerifyCallsOnImposter(Client);
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
