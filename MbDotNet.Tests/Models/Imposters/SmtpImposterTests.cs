using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
	[TestClass, TestCategory("Unit")]
	public class SmtpImposterTests
	{
		#region Constructor Tests

		[TestMethod]
		public void Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new SmtpImposter(port, null, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void Constructor_SetsProtocol()
		{
			var imposter = new SmtpImposter(123, null, null);
			Assert.AreEqual("smtp", imposter.Protocol);
		}

		[TestMethod]
		public void Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new SmtpImposter(123, expectedName, null);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void Constructor_AllowsNullPort()
		{
			var imposter = new SmtpImposter(null, null, null);
			Assert.AreEqual(default, imposter.Port);
		}

		[TestMethod]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new SmtpImposter(null, null, null);
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		[TestMethod]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new SmtpImposter(null, null,
				new SmtpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion
	}
}
