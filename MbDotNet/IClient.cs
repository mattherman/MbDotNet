using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Stubs;

namespace MbDotNet
{
	/// <summary>
	/// A client for interacting with the Mountebank API
	/// </summary>
	public interface IClient
	{
		/// <summary>
		/// Creates a new HTTP imposter.
		/// </summary>
		/// <param name="imposter">The imposter to create.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<HttpImposter> CreateHttpImposterAsync(HttpImposter imposter, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="name">The name the imposter will receive, useful for debugging/logging purposes.</param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpImposter> CreateHttpImposterAsync(int? port, string name, Action<HttpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpImposter> CreateHttpImposterAsync(int? port, Action<HttpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTP imposter.
		/// </summary>
		/// <param name="imposter">The imposter to create.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<HttpsImposter> CreateHttpsImposterAsync(HttpsImposter imposter, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTPS imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow
		/// Mountebank to set the port.
		/// </param>
		/// <param name="name">The name the imposter will receive, useful for debugging/logging purposes.</param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpsImposter> CreateHttpsImposterAsync(int? port, string name, Action<HttpsImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTPS imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpsImposter> CreateHttpsImposterAsync(int? port, Action<HttpsImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new TCP imposter.
		/// </summary>
		/// <param name="imposter">The imposter to create.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TcpImposter> CreateTcpImposterAsync(TcpImposter imposter, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new TCP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="name">The name the imposter will receive, useful for debugging/logging purposes.</param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<TcpImposter> CreateTcpImposterAsync(int? port, string name, Action<TcpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new TCP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<TcpImposter> CreateTcpImposterAsync(int? port, Action<TcpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new SMTP imposter.
		/// </summary>
		/// <param name="imposter">The imposter to create.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<SmtpImposter> CreateSmtpImposterAsync(SmtpImposter imposter, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new SMTP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow
		/// Mountebank to set the port.
		/// </param>
		/// <param name="name">The name the imposter will receive, useful for debugging/logging purposes.</param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<SmtpImposter> CreateSmtpImposterAsync(int? port, string name, Action<SmtpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new HTTPS imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow Mountebank
		/// to set the port.
		/// </param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <param name="cancellationToken"></param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<SmtpImposter> CreateSmtpImposterAsync(int? port, Action<SmtpImposter> imposterConfigurator, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves the list of imposters
		/// </summary>
		/// <returns>The list of retrieved imposters</returns>
		Task<IEnumerable<SimpleRetrievedImposter>> GetImpostersAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves an HttpImposter along with information about requests made to that
		/// imposter if mountebank is running with the "--mock" flag.
		/// </summary>
		/// <param name="port">The port number of the imposter to retrieve</param>
		/// <param name="cancellationToken"></param>
		/// <returns>The retrieved imposter</returns>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		/// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
		Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves a TcpImposter along with information about requests made to that
		/// imposter if mountebank is running with the "--mock" flag.
		/// </summary>
		/// <param name="port">The port number of the imposter to retrieve</param>
		/// <param name="cancellationToken"></param>
		/// <returns>The retrieved imposter</returns>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		/// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
		Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves an HttpsImposter along with information about requests made to that
		/// imposter if mountebank is running with the "--mock" flag.
		/// </summary>
		/// <param name="port">The port number of the imposter to retrieve</param>
		/// <param name="cancellationToken"></param>
		/// <returns>The retrieved imposter</returns>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		/// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an HTTP imposter</exception>
		Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves a SmtpImposter along with information about requests made to that
		/// imposter if Mountebank is running with the "--mock" flag.
		/// </summary>
		/// <param name="port">The port number of the imposter to retrieve</param>
		/// <param name="cancellationToken"></param>
		/// <returns>The retrieved imposter</returns>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		/// <exception cref="MbDotNet.Exceptions.InvalidProtocolException">Thrown if the retrieved imposter was not an SMTP imposter</exception>
		Task<RetrievedSmtpImposter> GetSmtpImposterAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes a single imposter from mountebank. Will also remove the imposter from the collection
		/// of imposters that the client maintains.
		/// </summary>
		/// <param name="port">The port number of the imposter to be removed</param>
		/// <param name="cancellationToken"></param>
		Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes all imposters from mountebank. Will also remove the imposters from the collection
		/// of imposters that the client maintains.
		/// </summary>
		Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default);

		Task ReplaceHttpImposterStubsAsync(int port, ICollection<HttpStub> replacementStubs, CancellationToken cancellationToken = default);

		Task ReplaceHttpsImposterStubsAsync(int port, ICollection<HttpStub> replacementStubs, CancellationToken cancellationToken = default);

		Task ReplaceTcpImposterStubsAsync(int port, ICollection<TcpStub> replacementStubs, CancellationToken cancellationToken = default);

		Task ReplaceHttpImposterStubAsync(int port, HttpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		Task ReplaceHttpsImposterStubAsync(int port, HttpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		Task ReplaceTcpImposterStubAsync(int port, TcpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		Task AddHttpImposterStubAsync(int port, HttpStub newStub, int? newStubIndex, CancellationToken cancellationToken = default);

		Task AddHttpsImposterStubAsync(int port, HttpStub newStub, int? newStubIndex, CancellationToken cancellationToken = default);

		Task AddTcpImposterStubAsync(int port, TcpStub newStub, int? newStubIndex, CancellationToken cancellationToken = default);

		Task RemoveStubAsync(int port, int stubIndex, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes previously saved requests for an imposter
		/// </summary>
		/// <param name="port">The port of the imposter to delete request history</param>
		/// <param name="cancellationToken"></param>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the configuration information of Mountebank
		/// </summary>
		/// <returns>A Config object containing the configuration of Mountebank</returns>
		Task<Config> GetConfigAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the entry hypermedia
		/// </summary>
		/// <returns>The Home object which contains entry hypermedia</returns>
		Task<Home> GetEntryHypermediaAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the Mountebank server logs
		/// </summary>
		/// <returns>The list of logs</returns>
		Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken = default);
	}
}
