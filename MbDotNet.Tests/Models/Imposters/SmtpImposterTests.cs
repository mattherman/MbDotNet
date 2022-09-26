using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
			var imposter = new SmtpImposter(port, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void Constructor_SetsProtocol()
		{
			var imposter = new SmtpImposter(123, null);
			Assert.AreEqual("smtp", imposter.Protocol);
		}

		[TestMethod]
		public void Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new SmtpImposter(123, expectedName);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void Constructor_AllowsNullPort()
		{
			var imposter = new SmtpImposter(null, null);
			Assert.AreEqual(default(int), imposter.Port);
		}

		#endregion
	}
}
