using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet
{
	/// <inheritdoc />
	[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "CORS is an abbreviation")]
	public class MountebankClient : IClient
	{
		private readonly IRequestProxy _requestProxy;

		/// <inheritdoc />
		public ICollection<Imposter> Imposters { get; private set; }

		/// <summary>
		/// Create a new MountebankClient instance for a server at the default address of http://127.0.0.1:2525
		/// </summary>
		public MountebankClient() : this(new MountebankRequestProxy()) { }

		/// <summary>
		/// Create a new MountebankClient instance for a specific server URL
		/// </summary>
		/// <param name="mountebankUrl"></param>
		public MountebankClient(string mountebankUrl) : this(new MountebankRequestProxy(mountebankUrl)) { }

		internal MountebankClient(IRequestProxy requestProxy)
		{
			Imposters = new List<Imposter>();
			_requestProxy = requestProxy;
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

		/// <inheritdoc />
		public async Task<HttpImposter> CreateHttpImposterAsync(int? port, string name, Action<HttpImposter> imposterConfigurator)
		{
			var imposter = new HttpImposter(port, name);
			imposterConfigurator(imposter);
			await SubmitAsync(imposter);
			return imposter;
		}

		/// <inheritdoc />
		public async Task<HttpImposter> CreateHttpImposterAsync(int? port, Action<HttpImposter> imposterConfigurator) =>
			await CreateHttpImposterAsync(port, null, imposterConfigurator);

		/// <inheritdoc />
		public HttpsImposter CreateHttpsImposter(int? port = null, string name = null, string key = null,
			string cert = null, bool mutualAuthRequired = false, bool recordRequests = false,
			HttpResponseFields defaultResponse = null, bool allowCORS = false)
		{
			if (key != null && !IsPEMFormatted(key))
			{
				throw new InvalidOperationException("Provided key must be PEM-formatted");
			}

			if (cert != null && !IsPEMFormatted(cert))
			{
				throw new InvalidOperationException("Provided certificate must be PEM-formatted");
			}

			return new HttpsImposter(port, name, key, cert, mutualAuthRequired, recordRequests, defaultResponse, allowCORS);
		}

		private static bool IsPEMFormatted(string value)
			=> Regex.IsMatch(value, @"-----BEGIN CERTIFICATE-----[\S\s]*-----END CERTIFICATE-----");

		/// <inheritdoc />
		public TcpImposter CreateTcpImposter(int? port = null, string name = null, TcpMode mode = TcpMode.Text,
			bool recordRequests = false, TcpResponseFields defaultResponse = null)
		{
			return new TcpImposter(port, name, mode, recordRequests, defaultResponse);
		}

		/// <inheritdoc />
		public SmtpImposter CreateSmtpImposter(int? port = null, string name = null, bool recordRequests = false)
		{
			return new SmtpImposter(port, name, recordRequests);
		}

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
			var imposter = Imposters.FirstOrDefault(imp => imp.Port == port);
			await _requestProxy.DeleteImposterAsync(port, cancellationToken).ConfigureAwait(false);

			if (imposter != null)
			{
				Imposters.Remove(imposter);
			}
		}

		/// <inheritdoc />
		public async Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default)
		{
			await _requestProxy.DeleteAllImpostersAsync(cancellationToken).ConfigureAwait(false);
			Imposters = new List<Imposter>();
		}

		/// <inheritdoc />
		public async Task SubmitAsync(ICollection<Imposter> imposters, CancellationToken cancellationToken = default)
		{
			foreach (var imposter in imposters)
			{
				await _requestProxy.CreateImposterAsync(imposter, cancellationToken).ConfigureAwait(false);
				Imposters.Add(imposter);
			}
		}

		/// <inheritdoc />
		public async Task SubmitAsync(Imposter imposter, CancellationToken cancellationToken = default)
		{
			await SubmitAsync(new[] { imposter }, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default)
		{
			await _requestProxy.UpdateImposterAsync(imposter, cancellationToken);
		}

		/// <inheritdoc />
		public async Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default)
		{
			await _requestProxy.DeleteSavedRequestsAsync(port, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<Config> GetConfigAsync(CancellationToken cancellationToken = default)
		{
			return await _requestProxy.GetConfigAsync(cancellationToken);
		}
	}
}
