using MbDotNet.Acceptance.Tests.AcceptanceTests;

namespace MbDotNet.Acceptance.Tests
{
    internal static class AcceptanceTest
    {
        public static void CanCreateAndGetHttpImposter(MountebankClient client) => new CanCreateAndGetHttpImposter(client).Run();
        public static void CanCreateAndGetHttpsImposter(MountebankClient client) => new CanCreateAndGetHttpsImposter(client).Run();
        public static void CanCreateAndGetTcpImposter(MountebankClient client) => new CanCreateAndGetTcpImposter(client).Run();
        public static void CanDeleteImposter(MountebankClient client) => new CanDeleteImposter(client).Run();
        public static void CanNotGetImposterThatDoesNotExist(MountebankClient client) => new CanNotGetImposterThatDoesNotExist(client).Run();
        public static void CanVerifyCallsOnImposter(MountebankClient client) => new CanVerifyCallsOnImposter(client).Run();
        public static void CanCreateAndGetHttpImposterWithNoPort(MountebankClient client) => new CanCreateAndGetHttpImposterWithNoPort(client).Run();
    }
}