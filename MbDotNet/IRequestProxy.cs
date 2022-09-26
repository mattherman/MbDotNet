using MbDotNet.Models.Imposters;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MbDotNet.Models.Responses;
using MbDotNet.Models;

namespace MbDotNet
{
	internal interface IRequestProxy
	{
		Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default);
		Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default);
		Task CreateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default);
		Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default);
		Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default);
		Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default);
		Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default);
		Task<RetrievedSmtpImposter> GetSmtpImposterAsync(int port, CancellationToken cancellationToken = default);
		Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default);
		Task<IEnumerable<SimpleRetrievedImposter>> GetImpostersAsync(CancellationToken cancellationToken = default);
		Task<Home> GetEntryHypermediaAsync(CancellationToken cancellationToken = default);
		Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken = default);
		Task<Config> GetConfigAsync(CancellationToken cancellationToken = default);
	}
}
