using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Stubs;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace MbDotNet.Tests
{
	[Trait("Category", "Unit")]
	public class MountebankRequestProxyTests
	{
		private Mock<IHttpClientWrapper> _mockClient;
		private MountebankRequestProxy _proxy;

		public MountebankRequestProxyTests()
		{
			_mockClient = new Mock<IHttpClientWrapper>();
			_proxy = new MountebankRequestProxy(_mockClient.Object);
		}

		[Fact]
		public async Task DeleteAllImposters_SendsRequest()
		{
			var expectedResource = "imposters";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteAllImpostersAsync();

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[Fact]
		public async Task DeleteAllImposters_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.DeleteAllImpostersAsync();
			});
		}

		[Fact]
		public async Task DeleteImposter_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteImposterAsync(port);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[Fact]
		public async Task DeleteImposter_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.DeleteImposterAsync(123);
			});
		}

		[Fact]
		public async Task CreateImposter_SendsRequest()
		{
			const string expectedResource = "imposters";

			var imposter = new HttpImposter(123, null, null);

			var response = GetResponse(HttpStatusCode.Created);

			HttpContent content = null;
			_mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response)
				.Callback<string, HttpContent, CancellationToken>((_, cont, _) => content = cont);

			await _proxy.CreateImposterAsync(imposter);

			var json = await content.ReadAsStringAsync();
			var serializedImposter = JsonConvert.DeserializeObject<HttpImposter>(json);

			Assert.Equal(imposter.Port, serializedImposter.Port);
		}

		[Fact]
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

			await _proxy.CreateImposterAsync(imposter);

			Assert.Equal(12345, imposter.Port);
		}

		[Fact]
		public async Task CreateImposter_StatusCodeNotCreated_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.CreateImposterAsync(new HttpImposter(123, null, null));
			});
		}

		[Fact]
		public async Task OverwriteAllImposters_SendsRequest()
		{
			const string expectedResource = "imposters";

			var imposters = new[] { new HttpImposter(123, null, null), new HttpImposter(456, null, null) };

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.PutAsync(expectedResource, It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await _proxy.OverwriteAllImpostersAsync(imposters);
		}

		[Fact]
		public async Task OverwriteAllImposters_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.OverwriteAllImpostersAsync(new[] { new HttpImposter(123, null, null) });
			});
		}

		[Fact]
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

			await _proxy.ReplaceStubsAsync(port, stubs);

			Assert.NotNull(content);
		}

		[Fact]
		public async Task ReplaceStubsAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.ReplaceStubsAsync(123, new[] { new HttpStub() });
			});
		}

		[Fact]
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

			await _proxy.ReplaceStubAsync(port, stub, stubIndex);

			Assert.NotNull(content);
		}

		[Fact]
		public async Task ReplaceStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.ReplaceStubAsync(123, new HttpStub(), 1);
			});
		}

		[Fact]
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

			await _proxy.AddStubAsync(port, stub, null);

			Assert.NotNull(content);
		}

		[Fact]
		public async Task AddStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.AddStubAsync(123, new HttpStub(), null);
			});
		}

		[Fact]
		public async Task RemoveStubAsync_SendsRequest()
		{
			const int port = 123;
			const int stubIndex = 1;
			var expectedResource = $"imposters/{port}/stubs/{stubIndex}";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.RemoveStubAsync(port, stubIndex);
		}

		[Fact]
		public async Task RemoveStubAsync_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.RemoveStubAsync(123, 1);
			});
		}

		[Fact]
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

			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public async Task GetHttpImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.GetHttpImposterAsync(9999);
			});
		}

		[Fact]
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

			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public async Task GetHttpsImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.GetHttpsImposterAsync(9999);
			});
		}

		[Fact]
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

			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public async Task GetTcpImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
		{
			var response = GetResponse(HttpStatusCode.NotFound);

			_mockClient.Setup(x => x.GetAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<ImposterNotFoundException>(async () =>
			{
				await _proxy.GetTcpImposterAsync(9999);
			});
		}

		[Fact]
		public async Task DeleteSavedRequests_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/savedRequests";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedRequestsAsync(port);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[Fact]
		public async Task DeleteSavedRequests_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.DeleteSavedRequestsAsync(123);
			});
		}

		[Fact]
		public async Task DeleteSavedProxyResponses_SendsRequest()
		{
			const int port = 123;
			var expectedResource = $"imposters/{port}/savedProxyResponses";

			var response = GetResponse(HttpStatusCode.OK);

			_mockClient.Setup(x => x.DeleteAsync(expectedResource, default))
				.ReturnsAsync(response);

			await _proxy.DeleteSavedProxyResponsesAsync(port);

			_mockClient.Verify(x => x.DeleteAsync(expectedResource, default), Times.Once);
		}

		[Fact]
		public async Task DeleteSavedProxyResponses_StatusCodeNotOk_ThrowsMountebankException()
		{
			var response = GetResponse(HttpStatusCode.BadRequest);

			_mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>(), default))
				.ReturnsAsync(response);

			await Assert.ThrowsAsync<MountebankException>(async () =>
			{
				await _proxy.DeleteSavedProxyResponsesAsync(123);
			});
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
