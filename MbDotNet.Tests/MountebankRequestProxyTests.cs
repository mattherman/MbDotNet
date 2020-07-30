using System.Net;
using System.Net.Http;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace MbDotNet.Tests
{
    [TestClass]
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
        public void DeleteAllImposters_SendsRequest()
        {
            var expectedResource = "imposters";

            var response = GetResponse(HttpStatusCode.OK);

            _mockClient.Setup(x => x.DeleteAsync(expectedResource))
                .ReturnsAsync(response);

            _proxy.DeleteAllImposters();

            _mockClient.Verify(x => x.DeleteAsync(expectedResource), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void DeleteAllImposters_StatusCodeNotOk_ThrowsMountebankException()
        {
            var response = GetResponse(HttpStatusCode.BadRequest);

            _mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(response);

            _proxy.DeleteAllImposters();
        }

        [TestMethod]
        public void DeleteImposter_SendsRequest_ImpostersResourceWithPort()
        {
            const int port = 123;
            var expectedResource = string.Format("imposters/{0}", port);

            var response = GetResponse(HttpStatusCode.OK);

            _mockClient.Setup(x => x.DeleteAsync(expectedResource))
                .ReturnsAsync(response);

            _proxy.DeleteImposter(port);

            _mockClient.Verify(x => x.DeleteAsync(expectedResource), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void DeleteImposter_StatusCodeNotOk_ThrowsMountebankException()
        {
            var response = GetResponse(HttpStatusCode.BadRequest);

            _mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(response);

            _proxy.DeleteImposter(123);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_ImpostersResource()
        {
            var expectedResource = "imposters";

            var response = GetResponse(HttpStatusCode.Created);

            _mockClient.Setup(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>()))
                .ReturnsAsync(response);

            _proxy.CreateImposter(new HttpImposter(123, null));

            _mockClient.Verify(x => x.PostAsync(expectedResource, It.IsAny<HttpContent>()), Times.Once);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_WithJsonBody()
        {
            var imposter = new HttpImposter(123, null);

            var response = GetResponse(HttpStatusCode.Created);

            HttpContent content = null;
            _mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(response)
                .Callback<string, HttpContent>((res, cont) => content = cont);

            _proxy.CreateImposter(new HttpImposter(123, null));

            var json = content.ReadAsStringAsync().Result;
            var serializedImposter = JsonConvert.DeserializeObject<HttpImposter>(json);

            Assert.AreEqual(imposter.Port, serializedImposter.Port);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_ImposterWithNoPort()
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

            _mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(response);

            var imposter = new HttpImposter(null, null);

            _proxy.CreateImposter(imposter);

            Assert.AreEqual(12345, imposter.Port);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void CreateImposter_StatusCodeNotCreated_ThrowsMountebankException()
        {
            var response = GetResponse(HttpStatusCode.BadRequest);

            _mockClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(response);

            _proxy.CreateImposter(new HttpImposter(123, null));
        }

        [TestMethod]
        public void DeleteSavedRequests_SendsRequest()
        {
            const int port = 123;
            var expectedResource = $"imposters/{port}/savedRequests";

            var response = GetResponse(HttpStatusCode.OK);

            _mockClient.Setup(x => x.DeleteAsync(expectedResource))
                .ReturnsAsync(response);

            _proxy.DeleteSavedRequests(port);

            _mockClient.Verify(x => x.DeleteAsync(expectedResource), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void DeleteSavedRequests_StatusCodeNotOk_ThrowsMountebankException()
        {
            var response = GetResponse(HttpStatusCode.BadRequest);

            _mockClient.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(response);

            _proxy.DeleteSavedRequests(123);
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
