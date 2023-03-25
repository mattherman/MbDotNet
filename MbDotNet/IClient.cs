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

		/// <summary>
		/// Replaces all stubs on an imposter with the replacement stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStubs">The stubs that should replace the existing stubs</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceHttpImposterStubsAsync(int port, IEnumerable<HttpStub> replacementStubs, CancellationToken cancellationToken = default);

		/// <summary>
		/// Replaces all stubs on an imposter with the replacement stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStubs">The stubs that should replace the existing stubs</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceHttpsImposterStubsAsync(int port, IEnumerable<HttpStub> replacementStubs, CancellationToken cancellationToken = default);

		/// <summary>
		/// Replaces all stubs on an imposter with the replacement stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStubs">The stubs that should replace the existing stubs</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceTcpImposterStubsAsync(int port, IEnumerable<TcpStub> replacementStubs, CancellationToken cancellationToken = default);

		/// <summary>
		/// Replaces a single stub on an imposter with the replacement stub. The stub to replace is specified by the index.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStub">The stub that should replace the existing stub</param>
		/// <param name="stubIndex">The index of the stub being replaced</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceHttpImposterStubAsync(int port, HttpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		/// <summary>
		/// Replaces a single stub on an imposter with the replacement stub. The stub to replace is specified by the index.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStub">The stub that should replace the existing stub</param>
		/// <param name="stubIndex">The index of the stub being replaced</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceHttpsImposterStubAsync(int port, HttpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		/// <summary>
		/// Replaces a single stub on an imposter with the replacement stub. The stub to replace is specified by the index.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="replacementStub">The stub that should replace the existing stub</param>
		/// <param name="stubIndex">The index of the stub being replaced</param>
		/// <param name="cancellationToken"></param>
		Task ReplaceTcpImposterStubAsync(int port, TcpStub replacementStub, int stubIndex, CancellationToken cancellationToken = default);

		/// <summary>
		/// Adds a stub to an imposter at the end of the stub collection. Alternatively, the index can be used to add the stub at a
		/// specific index within the existing stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="newStub">The stub being added</param>
		/// <param name="newStubIndex">
		/// An optional index specifying where to insert the stub, if unspecified the stub is included at the end of the stub collection
		/// </param>
		/// <param name="cancellationToken"></param>
		Task AddHttpImposterStubAsync(int port, HttpStub newStub, int? newStubIndex = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Adds a stub to an imposter at the end of the stub collection. Alternatively, the index can be used to add the stub at a
		/// specific index within the existing stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="newStub">The stub being added</param>
		/// <param name="newStubIndex">
		/// An optional index specifying where to insert the stub, if unspecified the stub is included at the end of the stub collection
		/// </param>
		/// <param name="cancellationToken"></param>
		Task AddHttpsImposterStubAsync(int port, HttpStub newStub, int? newStubIndex = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Adds a stub to an imposter at the end of the stub collection. Alternatively, the index can be used to add the stub at a
		/// specific index within the existing stubs.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="newStub">The stub being added</param>
		/// <param name="newStubIndex">
		/// An optional index specifying where to insert the stub, if unspecified the stub is included at the end of the stub collection
		/// </param>
		/// <param name="cancellationToken"></param>
		Task AddTcpImposterStubAsync(int port, TcpStub newStub, int? newStubIndex = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Remove a single stub from an imposter.
		/// </summary>
		/// <param name="port">The port of the imposter being updated</param>
		/// <param name="stubIndex">The index of the stub being removed</param>
		/// <param name="cancellationToken"></param>
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
