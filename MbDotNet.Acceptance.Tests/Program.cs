using System;

namespace MbDotNet.Acceptance.Tests
{
    public class Program
    {
        public static MountebankClient Client { get; set; }

        public static void Main()
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
                AcceptanceTest.CanCreateAndGetHttpsImposter(Client);
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
