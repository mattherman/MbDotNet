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
                AcceptanceTest.CanNotGetImposterThatDoesNotExist(Client);
                AcceptanceTest.CanCreateAndGetHttpImposter(Client);
                // This will not succeed until a GetHttpsImposter method is created
                // AcceptanceTest.CanCreateAndGetHttpsImposter(Client);
                AcceptanceTest.CanCreateAndGetTcpImposter(Client);
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
