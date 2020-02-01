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
        public async Task DeleteImposter_SendsRequest_ImpostersResourceWithPort()
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
        public async Task CreateImposter_SendsRequest_ImpostersResource()
        {
            var expectedResource = "imposters";

            var response = GetResponse(HttpStatusCode.Created);

            _mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default))
                .ReturnsAsync(response);

            await _proxy.CreateImposterAsync(new HttpImposter(123, null)).ConfigureAwait(false);

            _mockClient.Verify(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>(), default), Times.Once);
        }

        [TestMethod]
        public async Task CreateImposter_SendsRequest_WithJsonBody()
        {
            var imposter = new HttpImposter(123, null);

            var response = GetResponse(HttpStatusCode.Created);

            HttpContent content = null;
            _mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), default))
                .ReturnsAsync(response)
                .Callback<string, HttpContent, CancellationToken>((res, cont, _) => content = cont);

            await _proxy.CreateImposterAsync(new HttpImposter(123, null)).ConfigureAwait(false);

            var json = content.ReadAsStringAsync().Result;
            var serializedImposter = JsonConvert.DeserializeObject<HttpImposter>(json);

            Assert.AreEqual(imposter.Port, serializedImposter.Port);
        }

        [TestMethod]
        public async Task CreateImposter_SendsRequest_ImposterWithNoPort()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            response.Content = new StringContent(@"
            {
                ""protocol"": ""http"",
                ""port"": 12345,
                ""numberOfRequests"": 0,
                ""requests"": [],
                ""stubs"": [],
                ""_links"": {
                    ""self"": {
                        ""href"": ""http://localhost:2525/imposters/64735""
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

        private HttpResponseMessage GetResponse(HttpStatusCode statusCode)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = statusCode;
            response.Content = new StringContent(string.Empty);
            return response;
        }
    }
}
