using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet
{
    public interface IClient
    {
        /// <summary>
        /// A collection of all of the current imposters. The imposters in this
        /// collection may or may not have been added to mountebank. See IImposter.PendingSubmission
        /// for more information.
        /// </summary>
        ICollection<Imposter> Imposters { get; }

        /// <summary>
        /// Creates a new imposter on the specified port with the HTTP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank. If the port
        /// is blank, Mountebank will assign one which can be retrieved after Submit.
        /// </summary>
        /// <param name="port">
        /// The port the imposter will be set up to receive requests on, or null to allow
        /// Mountebank to set the port.
        /// </param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        HttpImposter CreateHttpImposter(int? port = null, string name = null, bool recordRequests = false, HttpResponseFields defaultResponse = null);

        /// <summary> 
        /// Creates a new imposter on the specified port with the HTTPS protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank. If the port
        /// is blank, Mountebank will assign one which can be retrieved after Submit.
        /// </summary>
        /// <param name="port">
        /// The port the imposter will be set up to receive requests on, or null to allow
        /// Mountebank to set the port.
        /// </param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="key">The private key the imposter will use</param>
        /// <param name="cert">The public certificate the imposer will use</param>
        /// <param name="mutualAuthRequired">Whether or not the server requires mutual auth</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        HttpsImposter CreateHttpsImposter(int? port = null, string name = null, string key = null, string cert = null,
            bool mutualAuthRequired = false, bool recordRequests = false, HttpResponseFields defaultResponse = null);

        /// <summary>
        /// Creates a new imposter on the specified port with the TCP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank. If the port
        /// is blank, Mountebank will assign one which can be retrieved after Submit.
        /// </summary>
        /// <param name="port">
        /// The port the imposter will be set up to receive requests on, or null to allow
        /// Mountebank to set the port.
        /// </param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="mode">The mode of the imposter, text or binary. This defines the encoding for request/response data</param>
        /// <param name="recordRequests">
        /// Enables recording requests to use the imposter as a mock. See
        /// <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank
        /// verification.
        /// </param>
        /// <param name="defaultResponse">The default response to send if no predicate matches</param>
        /// <returns>The newly created imposter</returns>
        TcpImposter CreateTcpImposter(int? port = null, string name = null, TcpMode mode = TcpMode.Text,
            bool recordRequests = false, TcpResponseFields defaultResponse = null);

        /// <summary>
        /// Retrieves an HttpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a TcpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an HttpsImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a single imposter from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        /// <param name="port">The port number of the imposter to be removed</param>
        Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all imposters from mountebank. Will also remove the imposters from the collection
        /// of imposters that the client maintains.
        /// </summary>
        Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Submits all pending imposters from the supplied collection to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        /// <param name="imposters">The imposters being submitted to mountebank</param>
        Task SubmitAsync(ICollection<Imposter> imposters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Submits imposter to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        /// <param name="imposter">The imposter being submitted to mountebank</param>
        Task SubmitAsync(Imposter imposter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Overwrites the stubs of an existing imposter without restarting it.
        /// </summary>
        /// <param name="imposter">The imposter to be updated with new stubs</param>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes previously saved requests for an imposter
        /// </summary>
        /// <param name="port">The port of the imposter to delete request history</param>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default);
    }
}
