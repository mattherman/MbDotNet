using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Xunit;

namespace MbDotNet.Tests.Models.Imposters
{
	/// <summary>
	/// Summary description for ImposterTests
	/// </summary>
	[Trait("Category", "Unit")]
	public class HttpImposterTests
	{
		#region Constructor Tests

		[Fact]
		public void Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new HttpImposter(port, null, null);
			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public void Constructor_SetsProtocol()
		{
			var imposter = new HttpImposter(123, null, null);
			Assert.Equal("http", imposter.Protocol);
		}

		[Fact]
		public void Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new HttpImposter(123, expectedName, null);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public void Constructor_AllowsNullPort()
		{
			var imposter = new HttpImposter(null, null, null);
			Assert.Equal(default, imposter.Port);
		}

		[Fact]
		public void Constructor_InitializesStubsCollection()
		{
			var imposter = new HttpImposter(123, null, null);
			Assert.NotNull(imposter.Stubs);
		}

		[Fact]
		public void Constructor_InitialDefaultResponse()
		{
			var imposter = new HttpImposter(null, null, null);
			Assert.Null(imposter.DefaultResponse);
		}

		[Fact]
		public void Constructor_SetsDefaultResponse()
		{
			var expectedDefaultResponse = new HttpResponseFields();
			var imposter = new HttpImposter(123, null,
				new HttpImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.Equal(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[Fact]
		public void Constructor_InitialAllowCORS()
		{
			const bool expectedAllowCORS = false;
			var imposter = new HttpImposter(null, null, null);
			Assert.Equal(expectedAllowCORS, imposter.AllowCORS);
		}

		[Fact]
		public void Constructor_AllowCORSTrue()
		{
			const bool expectedAllowCORS = true;
			var imposter = new HttpImposter(null, null,
				new HttpImposterOptions { AllowCORS = expectedAllowCORS });
			Assert.Equal(expectedAllowCORS, imposter.AllowCORS);
		}

		[Fact]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new HttpImposter(null, null, null);
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		[Fact]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new HttpImposter(null, null,
				new HttpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[Fact]
		public void AddStub_AddsStubToCollection()
		{
			var imposter = new HttpImposter(123, null, null);
			imposter.AddStub();
			Assert.Single(imposter.Stubs);
		}

		#endregion
	}
}
