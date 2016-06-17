using System.Net;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Method = RestSharp.Method;

namespace MbDotNet.Tests
{
    [TestClass]
    public class MountebankRequestProxyTests
    {
        private Mock<IRestClient> _mockRestClient;
        private MountebankRequestProxy _proxy;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRestClient = new Mock<IRestClient>();
            _proxy = new MountebankRequestProxy(_mockRestClient.Object);
        }

        [TestMethod]
        public void DeleteAllImposters_SendsRequest_DeleteMethod()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.OK };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.DeleteAllImposters();

            Assert.AreEqual(Method.DELETE, requestSent.Method);
        }

        [TestMethod]
        public void DeleteAllImposters_SendsRequest_ImpostersResource()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.OK };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.DeleteAllImposters();

            Assert.AreEqual("imposters", requestSent.Resource);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void DeleteAllImposters_StatusCodeNotOk_ThrowsMountebankException()
        {
            var response = new RestResponse {StatusCode = HttpStatusCode.BadRequest};

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response);

            _proxy.DeleteAllImposters();
        }

        [TestMethod]
        public void DeleteImposter_SendsRequest_DeleteMethod()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.OK };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.DeleteImposter(123);

            Assert.AreEqual(Method.DELETE, requestSent.Method);
        }

        [TestMethod]
        public void DeleteImposter_SendsRequest_ImpostersResourceWithPort()
        {
            const int port = 123;
            var response = new RestResponse { StatusCode = HttpStatusCode.OK };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.DeleteImposter(port);

            Assert.AreEqual(string.Format("imposters/{0}", port), requestSent.Resource);
        }

        [TestMethod]
        [ExpectedException(typeof(MountebankException))]
        public void DeleteImposter_StatusCodeNotOk_ThrowsMountebankException()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.BadRequest };

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response);

            _proxy.DeleteImposter(123);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_PostMethod()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.Created };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.CreateImposter(new HttpImposter(123, null));

            Assert.AreEqual(Method.POST, requestSent.Method);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_ImpostersResource()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.Created };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.CreateImposter(new HttpImposter(123, null));

            Assert.AreEqual("imposters", requestSent.Resource);
        }

        [TestMethod]
        public void CreateImposter_SendsRequest_WithJsonBody()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.Created };
            IRestRequest requestSent = null;

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response)
                .Callback<IRestRequest>(req => requestSent = req);

            _proxy.CreateImposter(new HttpImposter(123, null));

            Assert.AreEqual(DataFormat.Json, requestSent.RequestFormat);
            Assert.AreEqual(1, requestSent.Parameters.Count);
        }

        [TestMethod]
        [ExpectedException(typeof (MountebankException))]
        public void CreateImposter_StatusCodeNotCreated_ThrowsMountebankException()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.BadRequest };

            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response);

            _proxy.CreateImposter(new HttpImposter(123, null));
        }
    }
}
