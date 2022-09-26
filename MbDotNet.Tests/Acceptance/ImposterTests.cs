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
using Newtonsoft.Json.Linq;

namespace MbDotNet.Tests.Acceptance
{
	[TestClass, TestCategory("Acceptance")]
	public class ImposterTests : AcceptanceTestBase
	{
		private HttpClient _httpClient;

		public ImposterTests()
		{
			_httpClient = new HttpClient();
		}

		[TestInitialize]
		public async Task TestInitialize()
		{
			await _client.DeleteAllImpostersAsync();
		}

		[TestMethod]
		public async Task CanCreateAndGetHttpImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpImposter(port);

			await _client.SubmitAsync(imposter);

			var retrievedImposter = await _client.GetHttpImposterAsync(port);
			Assert.IsNotNull(retrievedImposter);
		}

		[TestMethod]
		public async Task CanGetListOfImposters()
		{
			const int port1 = 6000;
			var imposter1 = _client.CreateHttpsImposter(port1);
			await _client.SubmitAsync(imposter1);

			const int port2 = 5000;
			var imposter2 = _client.CreateHttpsImposter(port2);
			await _client.SubmitAsync(imposter2);

			var result = await _client.GetImpostersAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task CanUpdateHttpImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpImposter(port);
			imposter.AddStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.OK);

			await _client.SubmitAsync(imposter);

			imposter.AddStub()
				.OnMethodEquals(Method.Post)
				.ReturnsStatus(HttpStatusCode.Created);

			await _client.UpdateImposterAsync(imposter);
		}

		[TestMethod]
		public async Task CanCreateAndGetHttpsImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpsImposter(port);

			await _client.SubmitAsync(imposter);

			var retrievedImposter = await _client.GetHttpsImposterAsync(port);

			Assert.IsNotNull(retrievedImposter);
		}

		[TestMethod]
		public async Task CanUpdateHttpsImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpsImposter(port);
			imposter.AddStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.OK);

			await _client.SubmitAsync(imposter);

			imposter.AddStub()
				.OnMethodEquals(Method.Post)
				.ReturnsStatus(HttpStatusCode.Created);

			await _client.UpdateImposterAsync(imposter);
		}

		[TestMethod]
		public async Task CanCreateAndGetTcpImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateTcpImposter(port);

			await _client.SubmitAsync(imposter);

			var retrievedImposter = await _client.GetTcpImposterAsync(port);
			Assert.IsNotNull(retrievedImposter);
		}

		[TestMethod]
		public async Task CanUpdateTcpImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateTcpImposter(port);
			imposter.AddStub()
				.OnDataEquals("abc")
				.ReturnsData("123");

			await _client.SubmitAsync(imposter);

			imposter.AddStub()
				.OnDataEquals("def")
				.ReturnsData("456");

			await _client.UpdateImposterAsync(imposter);
		}

		[TestMethod]
		public async Task CanCreateAndGetSmtpImposter()
		{
			const int port = 6000;
			const string name = "TestSmtp";

			var imposter = _client.CreateSmtpImposter(port, name, true);

			await _client.SubmitAsync(imposter);

			var retrievedImposter = await _client.GetSmtpImposterAsync(port);
			Assert.IsNotNull(retrievedImposter);
			Assert.AreEqual(name, retrievedImposter.Name);
		}

		[TestMethod]
		public async Task CanCreateHttpProxyImposter()
		{
			const int sourceImposterPort = 6000;
			const int proxyImposterPort = 6001;

			var sourceImposter = _client.CreateHttpImposter(sourceImposterPort);
			sourceImposter.AddStub().ReturnsStatus(System.Net.HttpStatusCode.OK);
			await _client.SubmitAsync(sourceImposter);

			var proxyImposter = _client.CreateHttpImposter(proxyImposterPort);
			var predicateGenerators = new List<MatchesPredicate<HttpBooleanPredicateFields>>
			{
				new MatchesPredicate<HttpBooleanPredicateFields>(new HttpBooleanPredicateFields
				{
					QueryParameters = true
				})
			};

			proxyImposter.AddStub().ReturnsProxy(
				new System.Uri($"http://localhost:{sourceImposterPort}"),
				ProxyMode.ProxyOnce, predicateGenerators);

			await _client.SubmitAsync(proxyImposter);

			// Make a request to the imposter to trigger the proxy
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{proxyImposterPort}/test?param=value");
			var response = await _httpClient.SendAsync(request);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Request to proxy imposter failed");

			var retrievedSourceImposter = await _client.GetHttpImposterAsync(sourceImposterPort);
			Assert.IsNotNull(retrievedSourceImposter);
			Assert.AreEqual(1, retrievedSourceImposter.NumberOfRequests);
		}

		[TestMethod]
		public async Task CanCreateTcpProxyImposter()
		{
			const int sourceImposterPort = 6000;
			const int proxyImposterPort = 6001;

			var sourceImposter = _client.CreateTcpImposter(sourceImposterPort);
			sourceImposter.AddStub().ReturnsData("abc123");
			await _client.SubmitAsync(sourceImposter);

			var proxyImposter = _client.CreateTcpImposter(proxyImposterPort);
			var predicateGenerators = new List<MatchesPredicate<TcpBooleanPredicateFields>>
			{
				new MatchesPredicate<TcpBooleanPredicateFields>(new TcpBooleanPredicateFields
				{
					Data = true
				})
			};

			proxyImposter.AddStub().ReturnsProxy(
				new System.Uri($"tcp://localhost:{sourceImposterPort}"),
				ProxyMode.ProxyOnce, predicateGenerators);

			await _client.SubmitAsync(proxyImposter);

			// Make a request to the imposter to trigger the proxy
			using (var client = new TcpClient("localhost", proxyImposterPort))
			{
				var data = Encoding.ASCII.GetBytes("testdata");
				using (var stream = client.GetStream())
				{
					await stream.WriteAsync(data, 0, data.Length);
					await stream.FlushAsync();
					await stream.ReadAsync(new byte[6], 0, 6);
				}
			}

			var retrievedSourceImposter = await _client.GetTcpImposterAsync(sourceImposterPort);
			Assert.IsNotNull(retrievedSourceImposter);
			Assert.AreEqual(1, retrievedSourceImposter.NumberOfRequests);
		}

		[TestMethod]
		public async Task CanDeleteImposter()
		{
			const int port = 6000;

			var imposter = _client.CreateHttpImposter(port);

			await _client.DeleteImposterAsync(port);

			await Assert.ThrowsExceptionAsync<ImposterNotFoundException>(
				async () => await _client.GetHttpImposterAsync(port)
			);
		}

		[TestMethod]
		public async Task UnableToRetrieveImposterThatDoesNotExist()
		{
			await Assert.ThrowsExceptionAsync<ImposterNotFoundException>(
				async () => await _client.GetHttpImposterAsync(6000)
			);
		}

		[TestMethod]
		public async Task CanVerifyCallsOnImposter()
		{
			const int port = 6000;

			var imposter = _client.CreateHttpImposter(port);
			await _client.SubmitAsync(imposter);

			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123")
			{
				Content = new StringContent("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", Encoding.UTF8, "text/xml")
			};
			var response = await _httpClient.SendAsync(request);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Request to proxy imposter failed");

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.AreEqual(1, retrievedImposter.NumberOfRequests);

			// For the request field to be populated, mountebank must be run with the --mock parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			var receivedRequest = retrievedImposter.Requests[0];

			Assert.AreEqual("/customers", receivedRequest.Path);
			Assert.AreEqual("123", receivedRequest.QueryParameters["id"]);
			Assert.AreEqual("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", receivedRequest.Body);
			Assert.AreEqual(Method.Post, receivedRequest.Method);
			Assert.AreNotEqual(default(DateTime), receivedRequest.Timestamp);
			Assert.AreNotEqual(string.Empty, receivedRequest.RequestFrom);
			Assert.AreEqual("text/xml; charset=utf-8", receivedRequest.Headers["Content-Type"]);
			Assert.AreEqual("75", receivedRequest.Headers["Content-Length"]);
		}

		[TestMethod]
		public async Task CanVerifyCallsOnSmtpImposter()
		{
			const int port = 6000;
			const string from = "sender@test.com";
			const string to1 = "recipient1@test.com";
			const string to2 = "recipient2@test.com";
			const string subject = "Test Subject";
			const string body = "Test Body";
			const string attachmentContent1 = "Test Content1";
			const string attachmentContent2 = "Test Content2";

			var imposter = _client.CreateSmtpImposter(port, recordRequests: true);
			await _client.SubmitAsync(imposter);

			var mail = new MailMessage
			{
				From = new MailAddress(from),
				Subject = subject,
				Body = body
			};
			mail.To.Add(to1);
			mail.To.Add(to2);

			var attachment1 = Attachment.CreateAttachmentFromString(attachmentContent1, new ContentType("text/plain"));
			var attachment2 = Attachment.CreateAttachmentFromString(attachmentContent2, new ContentType("text/plain"));

			mail.Attachments.Add(attachment1);
			mail.Attachments.Add(attachment2);

			var smtpClient = new SmtpClient("localhost")
			{
				Port = port
			};

			await smtpClient.SendMailAsync(mail);

			var retrievedImposter = await _client.GetSmtpImposterAsync(port);

			Assert.AreEqual(1, retrievedImposter.NumberOfRequests);
			Assert.AreEqual(from, retrievedImposter.Requests.First().EnvelopeFrom);
			Assert.AreEqual(to1, retrievedImposter.Requests.First().EnvelopeTo.First());
			Assert.AreEqual(to2, retrievedImposter.Requests.First().EnvelopeTo.Last());
			Assert.AreEqual(subject, retrievedImposter.Requests.First().Subject);
			Assert.AreEqual(body, retrievedImposter.Requests.First().Text);
			Assert.AreEqual(attachmentContent1, Encoding.UTF8.GetString(retrievedImposter.Requests.First().Attachments.First().Content.Data));
			Assert.AreEqual(attachmentContent2, Encoding.UTF8.GetString(retrievedImposter.Requests.First().Attachments.Last().Content.Data));
		}

		[TestMethod]
		public async Task CanVerifyCallsOnImposterWithDuplicateQueryStringKey()
		{
			const int port = 6000;

			var imposter = _client.CreateHttpImposter(port);
			await _client.SubmitAsync(imposter);

			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123&id=456");
			var response = await _httpClient.SendAsync(request);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Request to proxy imposter failed");

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.AreEqual(1, retrievedImposter.NumberOfRequests);

			// For the request field to be populated, mountebank must be run with the --mock parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			var receivedRequest = retrievedImposter.Requests[0];
			var idQueryParameters = (JArray)receivedRequest.QueryParameters["id"];

			Assert.AreEqual("123", idQueryParameters[0]);
			Assert.AreEqual("456", idQueryParameters[1]);
		}

		[TestMethod]
		public async Task CanDeleteSavedRequestsForImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpImposter(port);
			imposter.AddStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.OK);

			await _client.SubmitAsync(imposter);

			// Make a request to the imposter to record a request
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{port}/test");
			_ = await _httpClient.SendAsync(request);

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.AreEqual(retrievedImposter.Requests.Length, 1);

			await _client.DeleteSavedRequestsAsync(port);

			retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.AreEqual(retrievedImposter.Requests.Length, 0);
		}

		[TestMethod]
		public async Task CanCheckMatchesForImposter()
		{
			const int port = 6000;
			var imposter = _client.CreateHttpImposter(port);
			imposter.AddStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.OK);

			await _client.SubmitAsync(imposter);

			// Make a request to the imposter to record a match
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{port}");
			_ = await _httpClient.SendAsync(request);

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			// For the request field to be populated, mountebank must be run with the --debug parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			Assert.AreEqual(retrievedImposter.Stubs.Count, 1);
			Assert.AreEqual(retrievedImposter.Stubs.ElementAt(0).Matches.Count, 1);
		}
	}
}
