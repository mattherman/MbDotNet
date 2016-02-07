using System.Collections.Generic;
using System.Linq;
using MbDotNet.Interfaces;
using MbDotNet.Enums;

namespace MbDotNet
{
    public class MountebankClient : IClient
    {
        public MountebankClient()
        {
            Imposters = new List<IImposter>();
        }

        public ICollection<IImposter> Imposters { get; private set; }

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
                imposter.Delete();
                Imposters.Remove(imposter);
            }
        }

        public void DeleteAllImposters()
        {
            foreach (var imposter in Imposters)
            {
                imposter.Delete();
            }

            Imposters = new List<IImposter>();
        }

        public void SubmitAll()
        {
            foreach (var imposter in Imposters.Where(imp => imp.PendingSubmission))
            {
                imposter.Submit();
            }
        }
    }
}
