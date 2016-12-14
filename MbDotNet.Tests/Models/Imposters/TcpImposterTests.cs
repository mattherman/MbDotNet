using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Stubs;
using MbDotNet.Enums;

namespace MbDotNet.Tests.Models.Imposters
{
    [TestClass]
    public class TcpImposterTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new TcpImposter(port, null, TcpMode.Text);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void Constructor_SetsProtocol()
        {
            var imposter = new TcpImposter(123, null, TcpMode.Text);
            Assert.AreEqual("tcp", imposter.Protocol);
        }

        [TestMethod]
        public void Constructor_SetsName()
        {
            const string expectedName = "Service";
            var imposter = new TcpImposter(123, expectedName, TcpMode.Text);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void Constructor_SetsMode()
        {
            const string expectedMode = "binary";
            var imposter = new TcpImposter(123, null, TcpMode.Binary);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void Constructor_InitializesStubsCollection()
        {
            var imposter = new TcpImposter(123, null, TcpMode.Text);
            Assert.IsNotNull(imposter.Stubs);
        }

        #endregion

        #region Stub Tests

        [TestMethod]
        public void AddStub_AddsStubToCollection()
        {
            var imposter = new TcpImposter(123, null, TcpMode.Binary);
            imposter.AddStub();
            Assert.AreEqual(1, imposter.Stubs.Count);
        }

        #endregion
    }
}
