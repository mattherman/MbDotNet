using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet
{
	/// <summary>
	/// A client for interacting with the Mountebank API
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "CORS is an abbreviation")]
	public interface IClient
	{
		/// <summary>
		/// A collection of all of the current imposters. The imposters in this
		/// collection may or may not have been submitted to mountebank.
		/// </summary>
		ICollection<Imposter> Imposters { get; }

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpImposter> CreateHttpImposterAsync(int? port, string name, Action<HttpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpImposter> CreateHttpImposterAsync(int? port, Action<HttpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpsImposter> CreateHttpsImposterAsync(int? port, string name, Action<HttpsImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<HttpsImposter> CreateHttpsImposterAsync(int? port, Action<HttpsImposter> imposterConfigurator);

		/// <summary>
		/// Creates a new TCP imposter on the specified port, configures it with the imposterConfigurator callback, and
		/// then submits it to Mountebank. If port is null, Mountebank will assign a random port that can be accessed on
		/// the response.
		/// </summary>
		/// <param name="port">
		/// The port the imposter will be set up to receive requests on, or null to allow
		/// Mountebank to set the port.
		/// </param>
		/// <param name="name">The name the imposter will receive, useful for debugging/logging purposes.</param>
		/// <param name="mode">The mode of the imposter, text or binary. This defines the encoding for request/response data.</param>
		/// <param name="imposterConfigurator">
		/// A callback function that will be used to configure the created imposter. This is where stubs should be
		/// added and any imposter-specific settings specified.
		/// </param>
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<TcpImposter> CreateTcpImposterAsync(int? port, string name, TcpMode mode, Action<TcpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<TcpImposter> CreateTcpImposterAsync(int? port, string name, Action<TcpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<TcpImposter> CreateTcpImposterAsync(int? port, Action<TcpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<SmtpImposter> CreateSmtpImposterAsync(int? port, string name, Action<SmtpImposter> imposterConfigurator);

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
		/// <returns>The imposter that was created in Mountebank</returns>
		Task<SmtpImposter> CreateSmtpImposterAsync(int? port, Action<SmtpImposter> imposterConfigurator);

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

		/// <summary>
		/// Submits all pending imposters from the supplied collection to be created in mountebank.
		/// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
		/// </summary>
		/// <param name="imposters">The imposters being submitted to mountebank</param>
		/// <param name="cancellationToken"></param>
		Task SubmitAsync(ICollection<Imposter> imposters, CancellationToken cancellationToken = default);

		/// <summary>
		/// Submits imposter to be created in mountebank.
		/// <exception cref="MbDotNet.Exceptions.MountebankException">Thrown if unable to create the imposter.</exception>
		/// </summary>
		/// <param name="imposter">The imposter being submitted to mountebank</param>
		/// <param name="cancellationToken"></param>
		Task SubmitAsync(Imposter imposter, CancellationToken cancellationToken = default);

		/// <summary>
		/// Overwrites the stubs of an existing imposter without restarting it.
		/// </summary>
		/// <param name="imposter">The imposter to be updated with new stubs</param>
		/// <param name="cancellationToken"></param>
		/// <exception cref="MbDotNet.Exceptions.ImposterNotFoundException">Thrown if no imposter was found on the specified port.</exception>
		Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default);

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
	}
}
