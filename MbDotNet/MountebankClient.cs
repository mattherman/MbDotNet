using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;

namespace MbDotNet
{
	/// <inheritdoc />
	public class MountebankClient : IClient
	{
		private readonly IRequestProxy _requestProxy;

		/// <summary>
		/// Create a new MountebankClient instance for a server at the default address of http://127.0.0.1:2525
		/// </summary>
		public MountebankClient() : this(new MountebankRequestProxy()) { }

		/// <summary>
		/// Create a new MountebankClient instance for a specific server URL
		/// </summary>
		/// <param name="mountebankUri"></param>
		public MountebankClient(Uri mountebankUri) : this(new MountebankRequestProxy(mountebankUri)) { }

		internal MountebankClient(IRequestProxy requestProxy)
		{
			_requestProxy = requestProxy;
		}

		/// <summary>
		/// Configures the imposter using imposterConfigurator before creating it in Mountebank.
		/// </summary>
		/// <param name="imposter">The imposter being created</param>
		/// <param name="imposterConfigurator">A callback used to configure the imposter</param>
		/// <param name="cancellationToken"></param>
		protected async Task<T> ConfigureAndCreateImposter<T>(T imposter, Action<T> imposterConfigurator, CancellationToken cancellationToken) where T: Imposter
		{
			imposterConfigurator(imposter);
			await _requestProxy.CreateImposterAsync(imposter, cancellationToken).ConfigureAwait(false);
			return imposter;
		}

		/// <inheritdoc />
		public async Task<HttpImposter> CreateHttpImposterAsync(HttpImposter imposter, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(imposter, _ => { }, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<HttpImposter> CreateHttpImposterAsync(int? port, string name, Action<HttpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new HttpImposter(port, name, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<HttpImposter> CreateHttpImposterAsync(int? port, Action<HttpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new HttpImposter(port, null, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<HttpsImposter> CreateHttpsImposterAsync(HttpsImposter imposter, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(imposter, _ => { }, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<HttpsImposter> CreateHttpsImposterAsync(int? port, string name, Action<HttpsImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new HttpsImposter(port, name, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<HttpsImposter> CreateHttpsImposterAsync(int? port, Action<HttpsImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new HttpsImposter(port, null, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<TcpImposter> CreateTcpImposterAsync(TcpImposter imposter, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(imposter, _ => { }, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<TcpImposter> CreateTcpImposterAsync(int? port, string name, Action<TcpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new TcpImposter(port, name, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<TcpImposter> CreateTcpImposterAsync(int? port, Action<TcpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new TcpImposter(port, null, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<SmtpImposter> CreateSmtpImposterAsync(SmtpImposter imposter, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(imposter, _ => { }, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<SmtpImposter> CreateSmtpImposterAsync(int? port, string name, Action<SmtpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new SmtpImposter(port, name, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<SmtpImposter> CreateSmtpImposterAsync(int? port, Action<SmtpImposter> imposterConfigurator, CancellationToken cancellationToken = default) =>
			await ConfigureAndCreateImposter(new SmtpImposter(port, null, null), imposterConfigurator, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task<IEnumerable<SimpleRetrievedImposter>> GetImpostersAsync(CancellationToken cancellationToken = default)
		{
			return await _requestProxy.GetImpostersAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default)
		{
			var imposter = await _requestProxy.GetHttpImposterAsync(port, cancellationToken).ConfigureAwait(false);

			ValidateRetrievedImposterProtocol(imposter, Protocol.Http);

			return imposter;
		}

		/// <inheritdoc />
		public async Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default)
		{
			var imposter = await _requestProxy.GetTcpImposterAsync(port, cancellationToken).ConfigureAwait(false);

			ValidateRetrievedImposterProtocol(imposter, Protocol.Tcp);

			return imposter;
		}

		/// <inheritdoc />
		public async Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default)
		{
			var imposter = await _requestProxy.GetHttpsImposterAsync(port, cancellationToken).ConfigureAwait(false);

			ValidateRetrievedImposterProtocol(imposter, Protocol.Https);

			return imposter;
		}

		/// <inheritdoc />
		public async Task<RetrievedSmtpImposter> GetSmtpImposterAsync(int port, CancellationToken cancellationToken = default)
		{
			var imposter = await _requestProxy.GetSmtpImposterAsync(port, cancellationToken).ConfigureAwait(false);

			ValidateRetrievedImposterProtocol(imposter, Protocol.Smtp);

			return imposter;
		}

		private static void ValidateRetrievedImposterProtocol<TRequest, TResponseFields>(
			RetrievedImposter<TRequest, TResponseFields> imposter, Protocol expectedProtocol)
			where TRequest : Request
			where TResponseFields : ResponseFields, new()
		{
			if (!string.Equals(imposter.Protocol, expectedProtocol.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidProtocolException(
					$"Expected a {expectedProtocol} imposter, but got a {imposter.Protocol} imposter.");
			}
		}

		/// <inheritdoc />
		public async Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default)
		{
			await _requestProxy.DeleteImposterAsync(port, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default)
		{
			await _requestProxy.DeleteAllImpostersAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task ReplaceHttpImposterStubsAsync(int port, IEnumerable<HttpStub> replacementStubs,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubsAsync(port, replacementStubs, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task ReplaceHttpsImposterStubsAsync(int port, IEnumerable<HttpStub> replacementStubs,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubsAsync(port, replacementStubs, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task ReplaceTcpImposterStubsAsync(int port, IEnumerable<TcpStub> replacementStubs,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubsAsync(port, replacementStubs, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task ReplaceHttpImposterStubAsync(int port, HttpStub replacementStub, int stubIndex,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubAsync(port, replacementStub, stubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task ReplaceHttpsImposterStubAsync(int port, HttpStub replacementStub, int stubIndex,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubAsync(port, replacementStub, stubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task ReplaceTcpImposterStubAsync(int port, TcpStub replacementStub, int stubIndex,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.ReplaceStubAsync(port, replacementStub, stubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task AddHttpImposterStubAsync(int port, HttpStub newStub, int? newStubIndex = null,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.AddStubAsync(port, newStub, newStubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task AddHttpsImposterStubAsync(int port, HttpStub newStub, int? newStubIndex = null,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.AddStubAsync(port, newStub, newStubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task AddTcpImposterStubAsync(int port, TcpStub newStub, int? newStubIndex = null,
			CancellationToken cancellationToken = default) =>
			await _requestProxy.AddStubAsync(port, newStub, newStubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task RemoveStubAsync(int port, int stubIndex, CancellationToken cancellationToken = default) =>
			await _requestProxy.RemoveStubAsync(port, stubIndex, cancellationToken).ConfigureAwait(false);

		/// <inheritdoc />
		public async Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default)
		{
			await _requestProxy.DeleteSavedRequestsAsync(port, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<Config> GetConfigAsync(CancellationToken cancellationToken = default)
		{
			return await _requestProxy.GetConfigAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<Home> GetEntryHypermediaAsync(CancellationToken cancellationToken = default)
		{
			return await _requestProxy.GetEntryHypermediaAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken = default)
		{
			return await _requestProxy.GetLogsAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
