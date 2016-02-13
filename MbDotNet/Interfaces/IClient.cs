using System.Collections.Generic;
using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IClient
    {
        /// <summary>
        /// A collection of all of the current imposters. The imposters in this
        /// collection may or may not have been added to mountebank. See IImposter.PendingSubmission
        /// for more information.
        /// </summary>
        ICollection<IImposter> Imposters { get; }

        /// <summary>
        /// Creates a new imposter on the specified port with the specified protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="protocol">The protocol the imposter will be set up to receive requests through</param>
        /// <returns>The newly created imposter</returns>
        IImposter CreateImposter(int port, Protocol protocol);

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
