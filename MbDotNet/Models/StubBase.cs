using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Interfaces;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;

namespace MbDotNet.Models
{
    public abstract class StubBase
    {
        /// <summary>
        /// A collection of all of the responses set up on this stub.
        /// </summary>
        public ICollection<PredicateBase> Predicates { get; set; }

        /// <summary>
        /// A collection of all of the predicates set up on this stub.
        /// </summary>
        public ICollection<ResponseBase> Responses { get; set; }
    }
}
