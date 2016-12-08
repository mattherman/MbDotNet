using System.Collections.Generic;
using System.Linq;
using MbDotNet.Interfaces;
using MbDotNet.Enums;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;

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
        /// <returns>The newly created imposter</returns>
        public HttpImposter CreateHttpImposter(int port, string name = null)
        {
            return new HttpImposter(port, name);
        }

        /// <summary>
        /// Creates a new imposter on the specified port with the TCP protocol. The Submit method
        /// must be called on the client in order to submit the imposter to mountebank.
        /// </summary>
        /// <param name="port">The port the imposter will be set up to receive requests on</param>
        /// <param name="name">The name the imposter will recieve, useful for debugging/logging purposes</param>
        /// <param name="mode">The mode of the imposter, text or binary. This defines the encoding for request/response data</param>
        /// <returns>The newly created imposter</returns>
        public TcpImposter CreateTcpImposter(int port, string name = null, TcpMode mode = TcpMode.Text)
        {
            return new TcpImposter(port, name, mode);
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
        /// Submits all pending imposters to be created in mountebank. Will throw a MountebankException
        /// if unable to create the imposter for any reason.
        /// </summary>
        public void Submit()
        {
            foreach (var imposter in Imposters.Where(imp => imp.PendingSubmission))
            {
                _requestProxy.CreateImposter(imposter);
                imposter.PendingSubmission = false;
            }
        }

        /// <summary>
        /// Submits all pending imposters from the supplied collection to be created in mountebank. 
        /// Will throw a MountebankException if unable to create the imposter for any reason.
        /// </summary>
        public void Submit(ICollection<Imposter> imposters)
        {
            foreach (var imposter in imposters.Where(imp => imp.PendingSubmission))
            {
                _requestProxy.CreateImposter(imposter);
                imposter.PendingSubmission = false;
                Imposters.Add(imposter);
            }
        }

        /// <summary>
        /// Submits imposter if pending to be created in mountebank. 
        /// Will throw a MountebankException if unable to create the imposter for any reason.
        /// </summary>
        public void Submit(Imposter imposter)
        {
            Submit(new [] { imposter });
        }
    }
}
