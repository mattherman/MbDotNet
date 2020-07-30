using System;
using System.Collections.Generic;
using System.Linq;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Requests;

namespace MbDotNet
{
    public class MountebankClient : IClient
    {
        private readonly IRequestProxy _requestProxy;

        /// <summary>
        /// A collection of all of the current imposters. The imposter in this
        /// collection may or may not have been added to mountebank. See IImposter.PendingSubmission
        /// for more information.
        /// </summary>
        public ICollection<Imposter> Imposters { get; private set; }

        public MountebankClient() : this(new MountebankRequestProxy()) { }

		public MountebankClient(string mountebankUrl) : this(new MountebankRequestProxy(mountebankUrl)) { }

		internal MountebankClient(IRequestProxy requestProxy)
        {
            Imposters = new List<Imposter>();
            _requestProxy = requestProxy;
        }

        /// <summary>
        /// Creates a new imposter on the specified port with the HTTP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <returns>The newly created imposter</returns>
        public HttpImposter CreateHttpImposter(int? port = null, string name = null, bool recordRequests = false)
        {
            return new HttpImposter(port, name, recordRequests);
        }

        /// <summary>
        /// Creates a new imposter on the specified port with the HTTPS protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// 
        /// The key and cert parameters MUST be valid PEM-formatted strings.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="key">The private key the imposter will use, MUST be a PEM-formatted string</param>
        /// <param name="cert">The public certificate the imposer will use, MUST be a PEM-formatted string</param>
        /// <param name="mutualAuthRequired">Whether or not the server will require mutual auth</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <returns>The newly created imposter</returns>
        public HttpsImposter CreateHttpsImposter(int? port = null, string name = null, string key = null, string cert = null, bool mutualAuthRequired = false, bool recordRequests = false)
        {
            return new HttpsImposter(port, name, key, cert, mutualAuthRequired, recordRequests);
        }

        /// <summary>
        /// Creates a new imposter on the specified port with the TCP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="mode">The mode of the imposter, text or binary. This defines the encoding for request/response data</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <returns>The newly created imposter</returns>
        public TcpImposter CreateTcpImposter(int? port = null, string name = null, TcpMode mode = TcpMode.Text, bool recordRequests = false)
        {
            return new TcpImposter(port, name, mode, recordRequests);
        }

        /// <summary>
        /// Retrieves an HttpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        public RetrievedHttpImposter GetHttpImposter(int port)
        {
            var imposter = _requestProxy.GetHttpImposter(port);

            ValidateRetrievedImposterProtocol(imposter, Protocol.Http);

            return imposter;
        }

        /// <summary>
        /// Retrieves a TcpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        public RetrievedTcpImposter GetTcpImposter(int port)
        {
            var imposter = _requestProxy.GetTcpImposter(port);

            ValidateRetrievedImposterProtocol(imposter, Protocol.Tcp);

            return imposter;
        }

        /// <summary>
        /// Retrieves an HttpsImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        public RetrievedHttpsImposter GetHttpsImposter(int port)
        {
            var imposter = _requestProxy.GetHttpsImposter(port);

            ValidateRetrievedImposterProtocol(imposter, Protocol.Https);

            return imposter;
        }

        private static void ValidateRetrievedImposterProtocol<T>(RetrievedImposter<T> imposter, Protocol expectedProtocol) where T: Request
        {
            if (!string.Equals(imposter.Protocol, expectedProtocol.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidProtocolException($"Expected a {expectedProtocol} imposter, but got a {imposter.Protocol} imposter.");
            }
        }

        /// <summary>
        /// Deletes a single imposter from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        /// <param name="port">The port number of the imposter to be removed</param>
        public void DeleteImposter(int port)
        {
            var imposter = Imposters.FirstOrDefault(imp => imp.Port == port);
            _requestProxy.DeleteImposter(port);

            if (imposter != null)
            {
                Imposters.Remove(imposter);
            }
        }

        /// <summary>
        /// Deletes all imposters from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        public void DeleteAllImposters()
        {
            _requestProxy.DeleteAllImposters();
            Imposters = new List<Imposter>();
        }

        /// <summary>
        /// Submits all pending imposters from the supplied collection to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        public void Submit(ICollection<Imposter> imposters)
        {
            foreach (var imposter in imposters)
            {
                _requestProxy.CreateImposter(imposter);
                Imposters.Add(imposter);
            }
        }

        /// <summary>
        /// Submits imposter to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        public void Submit(Imposter imposter)
        {
            Submit(new [] { imposter });
        }

        /// <summary>
        /// Deletes previously saved requests for an imposter
        /// </summary>
        /// <param name="port">The port of the imposter to delete request history</param>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        public void DeleteSavedRequests(int port)
        {
            _requestProxy.DeleteSavedRequests(port);
        }
    }
}
