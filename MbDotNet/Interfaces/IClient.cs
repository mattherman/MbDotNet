using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Interfaces
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
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <returns>The newly created imposter</returns>
        HttpImposter CreateHttpImposter(int port, string name = null);

        /// <summary>
        /// Creates a new imposter on the specified port with the TCP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="mode">The mode of the imposter, text or binary. This defines the encoding for request/response data</param>
        /// <returns>The newly created imposter</returns>
        TcpImposter CreateTcpImposter(int port, string name = null, TcpMode mode = TcpMode.Text);

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
        /// Submits all pending imposters to be created in mountebank. Will throw a MountebankException
        /// if unable to create the imposter for any reason.
        /// </summary>
        void Submit();
    }
}
