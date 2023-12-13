using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Xunit;

namespace MbDotNet.Tests.Models.Imposters
{
	[Trait("Category", "Unit")]
	public class TcpImposterTests
	{
		#region Constructor Tests

		[Fact]
		public void TcpImposter_Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new TcpImposter(port, null, null);
			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public void TcpImposter_Constructor_SetsProtocol()
		{
			var imposter = new TcpImposter(123, null, null);
			Assert.Equal("tcp", imposter.Protocol);
		}

		[Fact]
		public void TcpImposter_Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new TcpImposter(123, expectedName, null);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public void TcpImposter_Constructor_SetsMode()
		{
			const TcpMode expectedMode = TcpMode.Binary;
			var imposter = new TcpImposter(123, null, new TcpImposterOptions { Mode = expectedMode });
			Assert.Equal(expectedMode, imposter.Mode);
		}

		[Fact]
		public void TcpImposter_Constructor_AllowsNullPort()
		{
			var imposter = new TcpImposter(null, null, null);
			Assert.Equal(default, imposter.Port);
		}

		[Fact]
		public void TcpImposter_Constructor_InitializesStubsCollection()
		{
			var imposter = new TcpImposter(123, null, null);
			Assert.NotNull(imposter.Stubs);
		}

		[Fact]
		public void TcpImposter_Constructor_InitializesDefaultResponse()
		{
			var expectedDefaultResponse = new TcpResponseFields();
			var imposter = new TcpImposter(123, null,
				new TcpImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.Equal(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[Fact]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new TcpImposter(null, null, null);
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		[Fact]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new TcpImposter(null, null,
				new TcpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[Fact]
		public void TcpImposter_AddStub_AddsStubToCollection()
		{
			var imposter = new TcpImposter(123, null, null);
			imposter.AddStub();
			Assert.Single(imposter.Stubs);
		}

		#endregion
	}
}
