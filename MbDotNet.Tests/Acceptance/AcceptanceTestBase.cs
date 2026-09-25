using System;

namespace MbDotNet.Tests.Acceptance
{
	public class AcceptanceTestBase : IDisposable
	{
		protected readonly MountebankClient _client;

		public AcceptanceTestBase()
		{
			_client = new MountebankClient();
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
