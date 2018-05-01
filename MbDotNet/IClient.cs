using System.Collections.Generic;

using MbDotNet.Enums;
using MbDotNet.Models.Imposters;

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
        /// <returns>The newly created imposter</returns>
        HttpImposter CreateHttpImposter(int? port = null, string name = null, bool recordRequests = false);

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
        /// <returns>The newly created imposter</returns>
        HttpsImposter CreateHttpsImposter(int? port = null, string name = null, string key = null, string cert = null, bool mutualAuthRequired = false, bool recordRequests = false);

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
        /// <returns>The newly created imposter</returns>
        TcpImposter CreateTcpImposter(int? port = null, string name = null, TcpMode mode = TcpMode.Text, bool recordRequests = false);

        /// <summary>
        /// Retrieves an HttpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        RetrievedHttpImposter GetHttpImposter(int port);

        /// <summary>
        /// Retrieves a TcpImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        RetrievedTcpImposter GetTcpImposter(int port);

        /// <summary>
        /// Retrieves an HttpsImposter along with information about requests made to that
        /// imposter if mountebank is running with the "--mock" flag.
        /// </summary>
        /// <param name="port">The port number of the imposter to retrieve</param>
        /// <returns>The retrieved imposter</returns>
        /// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
        /// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
        RetrievedHttpsImposter GetHttpsImposter(int port);

        /// <summary>
        /// Deletes a single imposter from mountebank. Will also remove the imposter from the collection
        /// of imposters that the client maintains.
        /// </summary>
        /// <param name="port">The port number of the imposter to be removed</param>
        void DeleteImposter(int port);

        /// <summary>
        /// Deletes all imposters from mountebank. Will also remove the imposters from the collection
        /// of imposters that the client maintains.
        /// </summary>
        void DeleteAllImposters();

        /// <summary>
        /// Submits all pending imposters from the supplied collection to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        void Submit(ICollection<Imposter> imposters);

        /// <summary>
        /// Submits imposter to be created in mountebank. 
        /// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
        /// </summary>
        void Submit(Imposter imposter);
    }
}
