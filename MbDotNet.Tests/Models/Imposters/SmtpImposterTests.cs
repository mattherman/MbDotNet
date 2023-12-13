using MbDotNet.Models.Imposters;
using Xunit;

namespace MbDotNet.Tests.Models.Imposters
{
	[Trait("Category", "Unit")]
	public class SmtpImposterTests
	{
		#region Constructor Tests

		[Fact]
		public void Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new SmtpImposter(port, null, null);
			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public void Constructor_SetsProtocol()
		{
			var imposter = new SmtpImposter(123, null, null);
			Assert.Equal("smtp", imposter.Protocol);
		}

		[Fact]
		public void Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new SmtpImposter(123, expectedName, null);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public void Constructor_AllowsNullPort()
		{
			var imposter = new SmtpImposter(null, null, null);
			Assert.Equal(default, imposter.Port);
		}

		[Fact]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new SmtpImposter(null, null, null);
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		[Fact]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new SmtpImposter(null, null,
				new SmtpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion
	}
}
