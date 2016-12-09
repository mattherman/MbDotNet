using MbDotNet.Acceptance.Tests.AcceptanceTests;

namespace MbDotNet.Acceptance.Tests
{
    internal static class AcceptanceTest
    {
        public static void CanCreateImposter(MountebankClient client) => new CanCreateImposterTest(client).Run();
        public static void CanDeleteImposter(MountebankClient client) => new CanDeleteImposter(client).Run();
        public static void CanGetImposter(MountebankClient client) => new CanGetImposter(client).Run();
        public static void CanNotGetImposterThatDoesNotExist(MountebankClient client) => new CanNotGetImposterThatDoesNotExist(client).Run();
    }
}