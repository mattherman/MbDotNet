using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Interfaces;
using MbDotNet.Models.Predicates;

namespace MbDotNet.Models
{
    public abstract class StubBase
    {
        public ICollection<PredicateBase> Predicates { get; set; }
        public ICollection<IResponse> Responses { get; set; }
    }
}
