namespace MbDotNet.Tests.Acceptance
{
	public class AcceptanceTestBase
	{
		protected readonly MountebankClient _client;

		public AcceptanceTestBase()
		{
			_client = new MountebankClient();
		}
	}
}
