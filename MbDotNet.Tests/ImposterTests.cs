using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using Moq;
using RestSharp;

namespace MbDotNet.Tests
{
    /// <summary>
    /// Summary description for ImposterTests
    /// </summary>
    [TestClass]
    public class ImposterTests
    {
        private Mock<IRestClient> _mockRestClient;
        private Mock<Imposter> _mockImposter;
        private Imposter _imposter;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRestClient = new Mock<IRestClient>();
            _mockImposter = new Mock<Imposter>(123, Protocol.Http, _mockRestClient.Object) {CallBase = true};
            _imposter = _mockImposter.Object;
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new Imposter(port, Protocol.Http);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void Constructor_SetsProtocol()
        {
            var imposter = new Imposter(123, Protocol.Http);
            Assert.AreEqual("http", imposter.Protocol);
        }

        [TestMethod]
        public void Constructor_PendingSubmissionUponCreation()
        {
            var imposter = new Imposter(123, Protocol.Http);
            Assert.IsTrue(imposter.PendingSubmission);
        }

        #endregion

        #region Submit Tests

        [TestMethod]
        public void Submit_NoLongerPendingSubmission()
        {
            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.Created });

            var imposter = new Imposter(123, Protocol.Http, _mockRestClient.Object);
            imposter.Submit();

            Assert.IsFalse(imposter.PendingSubmission);
        }

        [TestMethod]
        public void Submit_ConvertsImposterToJsonAndSendsRequest()
        {
            IRestRequest request = null;
            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r).Returns(new RestResponse { StatusCode = HttpStatusCode.Created });

            var imposter = new Imposter(123, Protocol.Http, _mockRestClient.Object);
            imposter.Submit();

            Assert.IsTrue(request.Parameters[0].ToString().Contains(imposter.Port.ToString()));
            Assert.IsTrue(request.Parameters[0].ToString().Contains(imposter.Protocol.ToLower()));
        }

        [TestMethod]
        public void Submit_ThrowsExceptionIfResponseNot201()
        {
            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.BadRequest });

            try
            {
                var imposter = new Imposter(123, Protocol.Http, _mockRestClient.Object);
                imposter.Submit();
                Assert.Fail("Expected MountebankException to be thrown.");
            }
            catch (MountebankException)
            {
            }
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_PendingSubmission_DoesNotSendRequest()
        {
            _mockImposter.SetupGet(x => x.PendingSubmission).Returns(true);
            _imposter.Delete();

            _mockRestClient.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Never);
        }

        [TestMethod]
        public void Delete_SendsRequest()
        {
            IRestRequest request = null;
            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r).Returns(new RestResponse { StatusCode = HttpStatusCode.OK });

            _mockImposter.SetupGet(x => x.PendingSubmission).Returns(false);
            _imposter.Delete();

            Assert.IsTrue(request.Resource.Contains(_imposter.Port.ToString()));
        }

        [TestMethod]
        public void Delete_ThrowsExceptionIfResponseNot200()
        {
            _mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.BadRequest });

            try
            {
                _mockImposter.SetupGet(x => x.PendingSubmission).Returns(false);
                _imposter.Delete();
                Assert.Fail("Expected MountebankException to be thrown.");
            }
            catch (MountebankException)
            {
            }
        }

        #endregion
    }
}
