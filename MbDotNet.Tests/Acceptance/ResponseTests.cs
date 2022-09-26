using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MbDotNet.Tests.Acceptance

{
	[TestClass, TestCategory("Acceptance")]
	public class ResponseTests : AcceptanceTestBase
	{
		private HttpClient _httpClient;
		public ResponseTests()
		{
			_httpClient = new HttpClient();
		}

		[TestInitialize]
		public async Task TestInitialize()
		{
			await _client.DeleteAllImpostersAsync();
		}

		[TestMethod]
		public async Task CanGetEntryHypermedia()
		{
			var result = await _client.GetEntryHypermediaAsync();
			Assert.IsNotNull(result);

		}

		[TestMethod]
		public async Task CanGetLogs()
		{
			var result = await _client.GetLogsAsync();
			Assert.IsNotNull(result);

		}


	}

}
