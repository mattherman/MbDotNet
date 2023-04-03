using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace MbDotNet.Tests
{
	[TestClass, TestCategory("Unit")]
	public class MountebankRequestProxyTests
	{
		private Mock<IHttpClientWrapper> _mockClient;
		private MountebankRequestProxy _proxy;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockClient = new Mock<IHttpClientWrapper>();
			_proxy = new MountebankRequestProxy(_mockClient.Object);
		}

		[TestMethod]
		public async Task DeleteAllImposters_SendsRequest()
		{
			var expectedResource = "imposters";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteAllImpostersAsync().ConfigureAwait(false);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(MountebankException))]
		public async Task DeleteAllImposters_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.DeleteAllImpostersAsync().ConfigureAwait(false);
		}

		[TestMethod]
		public async Task DeleteImposter_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteImposterAsync(port).ConfigureAwait(false);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(MountebankException))]
		public async Task DeleteImposter_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.DeleteImposterAsync(123).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task CreateImposter_SendsRequest()
		{
			const string expectedResource = "imposters";

			var imposter = new HttpImposter(123, null, null);

			var response = GetResponse(HttpStatusCode.Created);

			HttpContent content = null;
			_mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response)
				.Callback<string, HttpContent, CancellationToken>((_, cont, _) => content = cont);

			await _proxy.CreateImposterAsync(imposter).ConfigureAwait(false);

			var json = await content.ReadAsStringAsync();
			var serializedImposter = JsonConvert.DeserializeObject<HttpImposter>(json);

			Assert.AreEqual(imposter.Port, serializedImposter.Port);
		}

		[TestMethod]
		public async Task CreateImposter_SendsRequest_ImposterWithNoPort()
		{
			var response = GetResponse(
				HttpStatusCode.Created,
				@"
                {
                    ""protocol"": ""http"",
                    ""port"": 12345,
                    ""numberOfRequests"": 0,
                    ""requests"": [],
                    ""stubs"": [],
                    ""_links"": {
                        ""self"": {
                            ""href"": ""http://localhost:2525/imposters/12345""
                        }
                    }
                }
                ");

			_mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			var imposter = new HttpImposter(null, null, null);

			await _proxy.CreateImposterAsync(imposter).ConfigureAwait(false);

			Assert.AreEqual(12345, imposter.Port);
		}

		[TestMethod]
		[ExpectedException(typeof(MountebankException))]
		public async Task CreateImposter_StatusCodeNotCreated_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await _proxy.CreateImposterAsync(new HttpImposter(123, null, null)).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task ReplaceStubsAsync_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/stubs";

			var stubs = new[] { new HttpStub() };
			var response = GetResponse(HttpStatusCode.OK);

			HttpContent content = null;
			_mockClient.Setup(x => x.PutAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response)
				.Callback<string, HttpContent, CancellationToken>((_, cont, _) => content = cont);

			await _proxy.ReplaceStubsAsync(port, stubs).ConfigureAwait(false);

			Assert.IsNotNull(content);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task ReplaceStubsAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await _proxy.ReplaceStubsAsync(123, new []{ new HttpStub() }).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task ReplaceStubAsync_SendsRequest()
		{
			const int port = 123;
			const int stubIndex = 1;
			var expectedResource = $"imposters/{port}/stubs/{stubIndex}";

			var stub = new HttpStub();
			var response = GetResponse(HttpStatusCode.OK);

			HttpContent content = null;
			_mockClient.Setup(x => x.PutAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response)
				.Callback<string, HttpContent, CancellationToken>((_, cont, _) => content = cont);

			await _proxy.ReplaceStubAsync(port, stub, stubIndex).ConfigureAwait(false);

			Assert.IsNotNull(content);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task ReplaceStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await _proxy.ReplaceStubAsync(123, new HttpStub(), 1).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task AddStubAsync_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/stubs";

			var stub = new HttpStub();
			var response = GetResponse(HttpStatusCode.OK);

			HttpContent content = null;
			_mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response)
				.Callback<string, HttpContent, CancellationToken>((_, cont, _) => content = cont);

			await _proxy.AddStubAsync(port, stub, null).ConfigureAwait(false);

			Assert.IsNotNull(content);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task AddStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await _proxy.AddStubAsync(123, new HttpStub(), null).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task RemoveStubAsync_SendsRequest()
		{
			const int port = 123;
			const int stubIndex = 1;
			var expectedResource = $"imposters/{port}/stubs/{stubIndex}";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.RemoveStubAsync(port, stubIndex).ConfigureAwait(false);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task RemoveStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.RemoveStubAsync(123, 1).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task GetHttpImposter_SendsRequest()
		{
			const int port = 12345;
			var expectedResource = $"imposters/{port}";

			var response = GetResponse(
				HttpStatusCode.OK,
				@"
                {
                    ""protocol"": ""http"",
                    ""port"": 12345,
                    ""numberOfRequests"": 0,
                    ""requests"": [],
                    ""stubs"": [],
                    ""_links"": {
                        ""self"": {
                            ""href"": ""http://localhost:2525/imposters/12345""
                        }
                    }
                }
                ");

			_mockClient.Setup(x => x.GetAsync(expectedResource, default))
				.ReturnsAsync(response);

			var imposter = await _proxy.GetHttpImposterAsync(port);

			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task GetHttpImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.GetHttpImposterAsync(9999).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task GetHttpsImposter_SendsRequest()
		{
			const int port = 12345;
			var expectedResource = $"imposters/{port}";

			var response = GetResponse(
				HttpStatusCode.OK,
				@"
                {
                    ""protocol"": ""https"",
                    ""port"": 12345,
                    ""numberOfRequests"": 0,
                    ""requests"": [],
                    ""stubs"": [],
                    ""_links"": {
                        ""self"": {
                            ""href"": ""http://localhost:2525/imposters/12345""
                        }
                    }
                }
                ");

			_mockClient.Setup(x => x.GetAsync(expectedResource, default))
				.ReturnsAsync(response);

			var imposter = await _proxy.GetHttpsImposterAsync(port);

			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task GetHttpsImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.GetHttpsImposterAsync(9999).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task GetTcpImposter_SendsRequest()
		{
			const int port = 12345;
			var expectedResource = $"imposters/{port}";

			var response = GetResponse(
				HttpStatusCode.OK,
				@"
                {
                    ""protocol"": ""tcp"",
                    ""port"": 12345,
                    ""numberOfRequests"": 0,
                    ""requests"": [],
                    ""stubs"": [],
                    ""_links"": {
                        ""self"": {
                            ""href"": ""http://localhost:2525/imposters/12345""
                        }
                    }
                }
                ");

			_mockClient.Setup(x => x.GetAsync(expectedResource, default))
				.ReturnsAsync(response);

			var imposter = await _proxy.GetTcpImposterAsync(port);

			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		[ExpectedException(typeof(ImposterNotFoundException))]
		public async Task GetTcpImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.GetTcpImposterAsync(9999).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task DeleteSavedRequests_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/savedRequests";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedRequestsAsync(port).ConfigureAwait(false);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(MountebankException))]
		public async Task DeleteSavedRequests_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedRequestsAsync(123).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task DeleteSavedProxyResponses_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/savedProxyResponses";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedProxyResponsesAsync(port).ConfigureAwait(false);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(MountebankException))]
		public async Task DeleteSavedProxyResponses_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedProxyResponsesAsync(123).ConfigureAwait(false);
		}

		private HttpResponseMessage GetResponse(HttpStatusCode statusCode, string content = null)
		{
			return new HttpResponseMessage
			{
				StatusCode = statusCode,
				Content = new StringContent(content ?? string.Empty)
			};
		}
	}
}
