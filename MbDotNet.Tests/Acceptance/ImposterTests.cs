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
using MbDotNet.Exceptions;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json.Linq;

using Xunit;

namespace MbDotNet.Tests.Acceptance
{
	[Trait("Category", "Acceptance")]
	[Collection("Sequential")]
	public class ImposterTests : AcceptanceTestBase
	{
		private readonly HttpClient _httpClient;

		/// <summary>
		/// It act as test initialize in x unit
		/// at https://xunit.net/docs/comparisons
		/// </summary>
		public ImposterTests()
		{
			_httpClient = new HttpClient();

			_client.DeleteAllImpostersAsync().ConfigureAwait(false);
		}

		[Fact]
		public async Task CanCreateAndGetHttpImposter()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, _ => { });

			var retrievedImposter = await _client.GetHttpImposterAsync(port);
            Assert.NotNull(retrievedImposter);
		}

		[Fact]
		public async Task CanGetListOfImposters()
		{
			const int port1 = 6000;
			await _client.CreateHttpsImposterAsync(port1, _ => { });

			const int port2 = 5000;
			await _client.CreateHttpsImposterAsync(port2, _ => { });

			var result = await _client.GetImpostersAsync();

			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
		}

		[Fact]
		public async Task CanReplaceHttpImposterStubs()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Post)
				.ReturnsStatus(HttpStatusCode.Created);

			await _client.ReplaceHttpImposterStubsAsync(port, new []{ stub });

			var imposter = await _client.GetHttpImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanReplaceHttpImposterStub()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.ReplaceHttpImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetHttpImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanAddHttpImposterStub_AtTheEnd()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.AddHttpImposterStubAsync(port, stub);

			var imposter = await _client.GetHttpImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanAddHttpImposterStub_AtSpecificIndex()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.AddHttpImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetHttpImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanRemoveHttpImposterStub()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			await _client.RemoveStubAsync(port, 0);

			var imposter = await _client.GetHttpImposterAsync(port);

			Assert.Empty(imposter.Stubs);
		}

		[Fact]
		public async Task CanCreateAndGetHttpsImposter()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, _ => { });

			var retrievedImposter = await _client.GetHttpsImposterAsync(port);

			Assert.NotNull(retrievedImposter);
		}

		[Fact]
		public async Task CanReplaceHttpsImposterStubs()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Post)
				.ReturnsStatus(HttpStatusCode.Created);

			await _client.ReplaceHttpsImposterStubsAsync(port, new []{ stub });

			var imposter = await _client.GetHttpsImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanReplaceHttpsImposterStub()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.ReplaceHttpsImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetHttpsImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanAddHttpsImposterStub_AtTheEnd()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.AddHttpsImposterStubAsync(port, stub);

			var imposter = await _client.GetHttpsImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanAddHttpsImposterStub_AtSpecificIndex()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			var stub = new HttpStub()
				.OnMethodEquals(Method.Get)
				.ReturnsStatus(HttpStatusCode.BadRequest);

			await _client.AddHttpsImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetHttpsImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanRemoveHttpsImposterStub()
		{
			const int port = 6000;
			await _client.CreateHttpsImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			await _client.RemoveStubAsync(port, 0);

			var imposter = await _client.GetHttpsImposterAsync(port);

			Assert.Empty(imposter.Stubs);
		}

		[Fact]
		public async Task CanCreateAndGetTcpImposter()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, _ => { });

			var retrievedImposter = await _client.GetTcpImposterAsync(port);
			Assert.NotNull(retrievedImposter);
		}

		[Fact]
		public async Task CanReplaceTcpImposterStubs()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnDataEquals("abc")
					.ReturnsData("123");
			});

			var stub = new TcpStub()
				.OnDataEquals("def")
				.ReturnsData("456");

			await _client.ReplaceTcpImposterStubsAsync(port, new[] { stub });

			var imposter = await _client.GetTcpImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanReplaceTcpImposterStub()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnDataEquals("abc")
					.ReturnsData("123");
			});

			var stub = new TcpStub()
				.OnDataEquals("def")
				.ReturnsData("456");

			await _client.ReplaceTcpImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetTcpImposterAsync(port);

			Assert.Single(imposter.Stubs);
		}

		[Fact]
		public async Task CanAddTcpImposterStub_AtTheEnd()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnDataEquals("abc")
					.ReturnsData("123");
			});

			var stub = new TcpStub()
				.OnDataEquals("def")
				.ReturnsData("456");

			await _client.AddTcpImposterStubAsync(port, stub);

			var imposter = await _client.GetTcpImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanAddTcpImposterStub_AtSpecificIndex()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnDataEquals("abc")
					.ReturnsData("123");
			});

			var stub = new TcpStub()
				.OnDataEquals("def")
				.ReturnsData("456");

			await _client.AddTcpImposterStubAsync(port, stub, 0);

			var imposter = await _client.GetTcpImposterAsync(port);

			Assert.Equal(2, imposter.Stubs.Count);
		}

		[Fact]
		public async Task CanRemoveTcpImposterStub()
		{
			const int port = 6000;
			await _client.CreateTcpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.OnDataEquals("abc")
					.ReturnsData("123");
			});

			await _client.RemoveStubAsync(port, 0);

			var imposter = await _client.GetTcpImposterAsync(port);

			Assert.Empty(imposter.Stubs);
		}

		[Fact]
		public async Task CanCreateAndGetSmtpImposter()
		{
			const int port = 6000;
			const string name = "TestSmtp";

			await _client.CreateSmtpImposterAsync(port, name, imposter => imposter.RecordRequests = true);

			var retrievedImposter = await _client.GetSmtpImposterAsync(port);
			Assert.NotNull(retrievedImposter);
			Assert.Equal(name, retrievedImposter.Name);
		}

		[Fact]
		public async Task CanCreateHttpProxyImposter()
		{
			const int sourceImposterPort = 6000;
			const int proxyImposterPort = 6001;

			await _client.CreateHttpImposterAsync(sourceImposterPort, imposter =>
			{
				imposter.AddStub().ReturnsStatus(HttpStatusCode.OK);
			});

			await _client.CreateHttpImposterAsync(proxyImposterPort, imposter =>
			{
				var predicateGenerators = new List<MatchesPredicate<HttpBooleanPredicateFields>>
				{
					new(new HttpBooleanPredicateFields
					{
						QueryParameters = true
					})
				};

				imposter.AddStub().ReturnsProxy(
					new Uri($"http://localhost:{sourceImposterPort}"),
					ProxyMode.ProxyOnce, predicateGenerators);
			});

			// Make a request to the imposter to trigger the proxy
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{proxyImposterPort}/test?param=value");
			var response = await _httpClient.SendAsync(request);
			Assert.True(HttpStatusCode.OK == response.StatusCode, "Request to proxy imposter failed");

			var retrievedSourceImposter = await _client.GetHttpImposterAsync(sourceImposterPort);
			Assert.NotNull(retrievedSourceImposter);
			Assert.Equal(1, retrievedSourceImposter.NumberOfRequests);
		}

		[Fact]
		public async Task CanCreateTcpProxyImposter()
		{
			const int sourceImposterPort = 6000;
			const int proxyImposterPort = 6001;

			await _client.CreateTcpImposterAsync(sourceImposterPort, sourceImposter =>
			{
				sourceImposter.AddStub().ReturnsData("abc123");
			});

			await _client.CreateTcpImposterAsync(proxyImposterPort, proxyImposter =>
			{
				var predicateGenerators = new List<MatchesPredicate<TcpBooleanPredicateFields>>
				{
					new(new TcpBooleanPredicateFields
					{
						Data = true
					})
				};

				proxyImposter.AddStub().ReturnsProxy(
					new Uri($"tcp://localhost:{sourceImposterPort}"),
					ProxyMode.ProxyOnce, predicateGenerators);
			});

			// Make a request to the imposter to trigger the proxy
			using (var client = new TcpClient("localhost", proxyImposterPort))
			{
				var data = Encoding.ASCII.GetBytes("testdata");
				await using var stream = client.GetStream();

				await stream.WriteAsync(data);
				await stream.FlushAsync();
				var numberOfBytesRead = await stream.ReadAsync(new byte[6].AsMemory(0, 6));
				Assert.True(numberOfBytesRead > 0);
			}

			var retrievedSourceImposter = await _client.GetTcpImposterAsync(sourceImposterPort);
			Assert.NotNull(retrievedSourceImposter);
			Assert.Equal(1, retrievedSourceImposter.NumberOfRequests);
		}

		[Fact]
		public async Task CanDeleteSavedProxyResponses()
		{
			const int sourceImposterPort = 6000;
			const int proxyImposterPort = 6001;

			await _client.CreateHttpImposterAsync(sourceImposterPort, imposter =>
			{
				imposter.AddStub().ReturnsStatus(HttpStatusCode.OK);
			});

			await _client.CreateHttpImposterAsync(proxyImposterPort, imposter =>
			{
				var predicateGenerators = new List<MatchesPredicate<HttpBooleanPredicateFields>>
				{
					new(new HttpBooleanPredicateFields
					{
						QueryParameters = true
					})
				};

				imposter.AddStub().ReturnsProxy(
					new Uri($"http://localhost:{sourceImposterPort}"),
					ProxyMode.ProxyOnce, predicateGenerators);
			});

			// Make a request to the imposter to trigger the proxy
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{proxyImposterPort}/test?param=value");
			var response = await _httpClient.SendAsync(request);
			Assert.True(HttpStatusCode.OK == response.StatusCode, "Request to proxy imposter failed");

			var imposterBeforeDeletingResponses = await _client.GetHttpImposterAsync(proxyImposterPort);
			Assert.NotNull(imposterBeforeDeletingResponses);
			Assert.Equal(2, imposterBeforeDeletingResponses.Stubs.Count);

			await _client.DeleteSavedProxyResponsesAsync(proxyImposterPort);

			var imposterAfterDeletingResponses = await _client.GetHttpImposterAsync(proxyImposterPort);
			Assert.NotNull(imposterAfterDeletingResponses);
			Assert.Single(imposterAfterDeletingResponses.Stubs);
		}

		[Fact]
		public async Task CanOverwriteAllImposters()
		{
			await _client.CreateHttpImposterAsync(6000, _ => { });
			await _client.CreateHttpImposterAsync(6001, _ => { });

			var impostersBeforeReplacement = (await _client.GetImpostersAsync()).ToList();
			Assert.Equal(2, impostersBeforeReplacement.Count);

			var newImposters = new[] { new HttpImposter(6002, null, null) };
			await _client.OverwriteAllImposters(newImposters);

			var impostersAfterReplacement = (await _client.GetImpostersAsync()).ToList();
			Assert.Single(impostersAfterReplacement);
			Assert.Equal(6002, impostersAfterReplacement[0].Port);
		}

		[Fact]
		public async Task CanDeleteImposter()
		{
			const int port = 6000;

			await _client.CreateHttpImposterAsync(port, _ => { });

			await _client.DeleteImposterAsync(port);

			await Assert.ThrowsAsync<ImposterNotFoundException>(
				async () => await _client.GetHttpImposterAsync(port)
			);
		}

		[Fact]
		public async Task UnableToRetrieveImposterThatDoesNotExist()
		{
			await Assert.ThrowsAsync<ImposterNotFoundException>(
				async () => await _client.GetHttpImposterAsync(6000)
			);
		}

		[Fact]
		public async Task CanVerifyCallsOnImposter()
		{
			const int port = 6000;

			await _client.CreateHttpImposterAsync(port, imposter => imposter.RecordRequests = true);

			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123")
			{
				Content = new StringContent("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", Encoding.UTF8, "text/xml")
			};
			var response = await _httpClient.SendAsync(request);
			Assert.True(HttpStatusCode.OK == response.StatusCode, "Request to proxy imposter failed");

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.Equal(1, retrievedImposter.NumberOfRequests);

			// For the request field to be populated, mountebank must be run with the --mock parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			var receivedRequest = retrievedImposter.Requests[0];

			Assert.Equal("/customers", receivedRequest.Path);
			Assert.Equal("123", receivedRequest.QueryParameters["id"]);
			Assert.Equal("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", receivedRequest.Body);
			Assert.Equal(Method.Post, receivedRequest.Method);
			Assert.NotEqual(default, receivedRequest.Timestamp);
			Assert.NotEqual(string.Empty, receivedRequest.RequestFrom);
			Assert.Equal("text/xml; charset=utf-8", receivedRequest.Headers["Content-Type"]);
			Assert.Equal("75", receivedRequest.Headers["Content-Length"]);
		}

		[Fact]
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

			await _client.CreateSmtpImposterAsync(port, imposter => imposter.RecordRequests = true);

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

			Assert.Equal(1, retrievedImposter.NumberOfRequests);
			Assert.Equal(from, retrievedImposter.Requests.First().EnvelopeFrom);
			Assert.Equal(to1, retrievedImposter.Requests.First().EnvelopeTo.First());
			Assert.Equal(to2, retrievedImposter.Requests.First().EnvelopeTo.Last());
			Assert.Equal(subject, retrievedImposter.Requests.First().Subject);
			Assert.Equal(body, retrievedImposter.Requests.First().Text);
			Assert.Equal(attachmentContent1, Encoding.UTF8.GetString(retrievedImposter.Requests.First().Attachments.First().Content.Data));
			Assert.Equal(attachmentContent2, Encoding.UTF8.GetString(retrievedImposter.Requests.First().Attachments.Last().Content.Data));
		}

		[Fact]
		public async Task CanVerifyCallsOnImposterWithDuplicateQueryStringKey()
		{
			const int port = 6000;

			await _client.CreateHttpImposterAsync(port, imposter => imposter.RecordRequests = true);

			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123&id=456");
			var response = await _httpClient.SendAsync(request);
			Assert.True(HttpStatusCode.OK == response.StatusCode, "Request to proxy imposter failed");

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.Equal(1, retrievedImposter.NumberOfRequests);

			// For the request field to be populated, mountebank must be run with the --mock parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			var receivedRequest = retrievedImposter.Requests[0];
			var idQueryParameters = (JArray)receivedRequest.QueryParameters["id"];

			Assert.Equal("123", idQueryParameters[0]);
			Assert.Equal("456", idQueryParameters[1]);
		}

		[Fact]
		public async Task CanDeleteSavedRequestsForImposter()
		{
			const int port = 6000;

			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.RecordRequests = true;
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			// Make a request to the imposter to record a request
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:6000/test");
			_ = await _httpClient.SendAsync(request);

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.Single(retrievedImposter.Requests);

			await _client.DeleteSavedRequestsAsync(port);

			retrievedImposter = await _client.GetHttpImposterAsync(port);

			Assert.Empty(retrievedImposter.Requests);
		}

		[Fact]
		public async Task CanCheckMatchesForImposter()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.RecordRequests = true;
				imposter.AddStub()
					.OnMethodEquals(Method.Get)
					.ReturnsStatus(HttpStatusCode.OK);
			});

			// Make a request to the imposter to record a match
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{port}");
			_ = await _httpClient.SendAsync(request);

			var retrievedImposter = await _client.GetHttpImposterAsync(port);

			// For the request field to be populated, mountebank must be run with the --debug parameter
			// http://www.mbtest.org/docs/api/overview#get-imposter
			Assert.Single(retrievedImposter.Stubs);
			Assert.Single(retrievedImposter.Stubs.ElementAt(0).Matches);
		}

		[Fact]
		public async Task CanCreateAnImposterWithAFaultResponse()
		{
			const int port = 6000;
			await _client.CreateHttpImposterAsync(port, imposter =>
			{
				imposter.AddStub()
					.ReturnsFault(Fault.ConnectionResetByPeer);
			});

			var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{port}");
			await Assert.ThrowsAsync<HttpRequestException>(async () => await _httpClient.SendAsync(request));
		}
	}
}
