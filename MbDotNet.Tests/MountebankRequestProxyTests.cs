using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
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
            var expectedResource = string.Format("imposters/{0}", port);

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

            var imposter = new HttpImposter(123, null);

            var response = GetResponse(HttpStatusCode.Created);

            HttpContent content = null;
            _mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default))
                .ReturnsAsync(response)
                .Callback<string, HttpContent, CancellationToken>((res, cont, _) => content = cont);

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

            var imposter = new HttpImposter(null, null);

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

            await _proxy.CreateImposterAsync(new HttpImposter(123, null)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task UpdateImposter_SendsRequest()
        {
            const int port = 123;
            var expectedResource = $"imposters/{port}/stubs";

            var response = GetResponse(HttpStatusCode.OK);

            var imposter = new HttpImposter(port, null);

            HttpContent content = null;
            _mockClient.Setup(x => x.PutAsync(expectedResource, It.IsAny<HttpContent>(), default))
                .ReturnsAsync(response)
                .Callback<string, HttpContent, CancellationToken>((res, cont, _) => content = cont);

            await _proxy.UpdateImposterAsync(imposter).ConfigureAwait(false);

            var json = await content.ReadAsStringAsync();
            var serializedImposter = JsonConvert.DeserializeObject<HttpImposter>(json);

            Assert.AreEqual(imposter.Port, serializedImposter.Port);
        }

        [TestMethod]
        [ExpectedException(typeof(ImposterNotFoundException))]
        public async Task UpdateImposter_StatusCodeNotOk_ThrowsImposterNotFoundException()
        {
            var response = GetResponse(HttpStatusCode.NotFound);

            _mockClient.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
                .ReturnsAsync(response);

            await _proxy.UpdateImposterAsync(new HttpImposter(123, null)).ConfigureAwait(false);
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
