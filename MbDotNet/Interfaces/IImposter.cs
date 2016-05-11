using System.Collections.Generic;
using MbDotNet.Models;

namespace MbDotNet.Interfaces
{
    public interface IImposter
    {
        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// The protocol the imposter is set up to accept requests through.
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// Whether or not the imposter has been added to mountebank.
        /// </summary>
        bool PendingSubmission { get; set; }
    }
}
