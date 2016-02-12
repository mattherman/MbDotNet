using System.Collections.Generic;
using System.Linq;
using MbDotNet.Interfaces;
using MbDotNet.Enums;
using MbDotNet.Models;

namespace MbDotNet
{
    public class MountebankClient : IClient
    {
        private readonly IRequestProxy _requestProxy;

        public ICollection<IImposter> Imposters { get; private set; }

        public MountebankClient() : this(new MountebankRequestProxy()) { }

        public MountebankClient(IRequestProxy requestProxy)
        {
            Imposters = new List<IImposter>();
            _requestProxy = requestProxy;
        }

        public IImposter CreateImposter(int port, Protocol protocol)
        {
            var imposter = new Imposter(port, protocol);
            Imposters.Add(imposter);
            return imposter;
        }

        public void DeleteImposter(int port)
        {
            var imposter = Imposters.FirstOrDefault(imp => imp.Port == port);

            if (imposter != null)
            {
                _requestProxy.DeleteImposter(port);
                Imposters.Remove(imposter);
            }
        }

        public void DeleteAllImposters()
        {
            _requestProxy.DeleteAllImposters();
            Imposters = new List<IImposter>();
        }

        public void Submit()
        {
            foreach (var imposter in Imposters.Where(imp => imp.PendingSubmission))
            {
                _requestProxy.CreateImposter(imposter);
                imposter.PendingSubmission = false;
            }
        }
    }
}
