using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet
{
    public class MountebankClient : IClient
    {
        private readonly IRequestProxy _requestProxy;

        /// <summary>
        /// A collection of all of the submitted imposters.
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
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        public HttpImposter CreateHttpImposter(int? port = null, string name = null, bool recordRequests = false, HttpResponseFields defaultResponse = null)
        {
            return new HttpImposter(port, name, recordRequests, defaultResponse);
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
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        public HttpsImposter CreateHttpsImposter(int? port = null, string name = null, string key = null,
            string cert = null, bool mutualAuthRequired = false, bool recordRequests = false,
            HttpResponseFields defaultResponse = null)
        {
            if (key != null && !IsPEMFormatted(key))
            {
                throw new InvalidOperationException("Provided key must be PEM-formatted");
            }

            if (cert != null && !IsPEMFormatted(cert))
            {
                throw new InvalidOperationException("Provided certificate must be PEM-formatted");
            }

            return new HttpsImposter(port, name, key, cert, mutualAuthRequired, recordRequests, defaultResponse);
        }

        private bool IsPEMFormatted(string value)
            => Regex.IsMatch(value, @"-----BEGIN CERTIFICATE-----[\S\s]*-----END CERTIFICATE-----");

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
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        public TcpImposter CreateTcpImposter(int? port = null, string name = null, TcpMode mode = TcpMode.Text,
            bool recordRequests = false, TcpResponseFields defaultResponse = null)
        {
            return new TcpImposter(port, name, mode, recordRequests, defaultResponse);
        }

        /// <summary>
        /// Retrieves an HttpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        public async Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            var imposter = await _requestProxy.GetHttpImposterAsync(port, cancellationToken).ConfigureAwait(false);

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
        public async Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            var imposter = await _requestProxy.GetTcpImposterAsync(port, cancellationToken).ConfigureAwait(false);

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
        public async Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            var imposter = await _requestProxy.GetHttpsImposterAsync(port, cancellationToken).ConfigureAwait(false);

            ValidateRetrievedImposterProtocol(imposter, Protocol.Https);

            return imposter;
        }

        private static void ValidateRetrievedImposterProtocol<TRequest, TResponseFields>(
            RetrievedImposter<TRequest, TResponseFields> imposter, Protocol expectedProtocol)
            where TRequest : Request
            where TResponseFields : ResponseFields, new()
        {
            if (!string.Equals(imposter.Protocol, expectedProtocol.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidProtocolException(
                    $"Expected a {expectedProtocol} imposter, but got a {imposter.Protocol} imposter.");
            }
        }

        /// <summary>
        /// Deletes a single imposter from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        /// <param name="port">The port number of the imposter to be removed</param>
        public async Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            var imposter = Imposters.FirstOrDefault(imp => imp.Port == port);
            await _requestProxy.DeleteImposterAsync(port, cancellationToken).ConfigureAwait(false);

            if (imposter != null)
            {
                Imposters.Remove(imposter);
            }
        }

        /// <summary>
        /// Deletes all imposters from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        public async Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default)
        {
            await _requestProxy.DeleteAllImpostersAsync(cancellationToken).ConfigureAwait(false);
            Imposters = new List<Imposter>();
        }

        /// <summary>
        /// Submits all pending imposters from the supplied collection to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        public async Task SubmitAsync(ICollection<Imposter> imposters, CancellationToken cancellationToken = default)
        {
            foreach (var imposter in imposters)
            {
                await _requestProxy.CreateImposterAsync(imposter, cancellationToken).ConfigureAwait(false);
                Imposters.Add(imposter);
            }
        }

        /// <summary>
        /// Submits imposter to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        public async Task SubmitAsync(Imposter imposter, CancellationToken cancellationToken = default)
        {
            await SubmitAsync(new [] { imposter }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Overwrites the stubs of an existing imposter without restarting it.
        /// </summary>
        /// <param name="imposter">The imposter to be updated with new stubs</param>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        public async Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default)
        {
            await _requestProxy.UpdateImposterAsync(imposter, cancellationToken);
        }

        /// <summary>
        /// Deletes previously saved requests for an imposter
        /// </summary>
        /// <param name="port">The port of the imposter to delete request history</param>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        public async Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default)
        {
            await _requestProxy.DeleteSavedRequestsAsync(port, cancellationToken);
        }
    }
}
