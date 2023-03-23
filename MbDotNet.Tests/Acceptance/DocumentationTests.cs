using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Acceptance
{
	/// <summary>
	/// This file contain acceptance tests that show how to setup the same
	/// imposters described in the examples in the mountebank documentation.
	/// See the comment above each individual test to figure out which
	/// example it is describing.
	/// </summary>
	[TestClass, TestCategory("Acceptance")]
	public class DocumentationTests : AcceptanceTestBase
	{
		private const int ImposterPort = 6000;

		[TestInitialize]
		public async Task TestInitialize()
		{
			await _client.DeleteAllImpostersAsync();
		}

		/// <summary>
		/// This test shows how to setup the imposter in the stub example
		/// at http://www.mbtest.org/docs/api/stubs.
		/// </summary>
		[TestMethod]
		public async Task StubExample()
		{
			await _client.CreateHttpImposterAsync(4545, "StubExample", imposter =>
			{
				imposter.AddStub().OnPathAndMethodEqual("/customers/123", Method.Post)
					.ReturnsXml(HttpStatusCode.Created, new Customer { Email = "customer@test.com" })
					.ReturnsBody(HttpStatusCode.BadRequest, "<error>Email already exists</error>");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter with a dynamic port chosen by Mountebank
		/// See imposter resource at http://www.mbtest.org/docs/api/contracts for more information.
		/// </summary>
		[TestMethod]
		public async Task DynamicPortExample()
		{
			await _client.CreateHttpImposterAsync(null, "DynamicPort", _ => { });
		}

		/// <summary>
		/// This test shows how to setup the imposter in the equals predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task EqualsPredicateExample()
		{
			await _client.CreateHttpImposterAsync(4545, "EqualsPredicateExample", imposter =>
			{
				// First stub
				var bodyPredicateFields = new HttpPredicateFields
				{
					RequestBody = "hello, world"
				};
				var bodyPredicate = new EqualsPredicate<HttpPredicateFields>(bodyPredicateFields, true, "$!", null);

				var complexPredicateFields = new HttpPredicateFields
				{
					Method = Method.Post,
					Path = "/test",
					QueryParameters = new Dictionary<string, object> { { "first", "1" }, { "second", "2" } },
					Headers = new Dictionary<string, object> { { "Accept", "text/plain" } }
				};

				var complexPredicate = new EqualsPredicate<HttpPredicateFields>(complexPredicateFields);

				imposter.AddStub().On(complexPredicate).On(bodyPredicate).ReturnsStatus(HttpStatusCode.BadRequest);

				// Second stub
				var fields = new HttpPredicateFields
				{
					Headers = new Dictionary<string, object> { { "Accept", "application/xml" } }
				};

				imposter.AddStub().On(new EqualsPredicate<HttpPredicateFields>(fields)).ReturnsStatus(HttpStatusCode.NotAcceptable);

				// Third stub
				imposter.AddStub().OnMethodEquals(Method.Put).ReturnsStatus((HttpStatusCode)405);

				// Fourth stub
				imposter.AddStub().OnMethodEquals(Method.Put).ReturnsStatus((HttpStatusCode)500);
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the deepEquals predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task DeepEqualsPredicateExample()
		{
			await _client.CreateHttpImposterAsync(4556, "DeepEqualsPredicateExample", imposter =>
			{
				// First stub
				var predicateFields = new HttpPredicateFields
				{
					QueryParameters = new Dictionary<string, object>()
				};

				var responseFields = new HttpResponseFields
				{
					ResponseObject = "first"
				};

				imposter.AddStub().On(new DeepEqualsPredicate<HttpPredicateFields>(predicateFields))
					.Returns(new IsResponse<HttpResponseFields>(responseFields));

				// Second stub
				predicateFields = new HttpPredicateFields
				{
					QueryParameters = new Dictionary<string, object> { { "first", "1" } }
				};

				responseFields = new HttpResponseFields
				{
					ResponseObject = "second"
				};

				imposter.AddStub().On(new DeepEqualsPredicate<HttpPredicateFields>(predicateFields))
					.Returns(new IsResponse<HttpResponseFields>(responseFields));

				// Third stub
				predicateFields = new HttpPredicateFields
				{
					QueryParameters = new Dictionary<string, object> { { "first", "1" }, { "second", "2" } }
				};

				responseFields = new HttpResponseFields
				{
					ResponseObject = "third"
				};

				imposter.AddStub().On(new DeepEqualsPredicate<HttpPredicateFields>(predicateFields))
					.Returns(new IsResponse<HttpResponseFields>(responseFields));
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the contains predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task ContainsPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4547, "ContainsPredicateExample", TcpMode.Binary, imposter =>
			{
				// First stub
				var predicateFields = new TcpPredicateFields
				{
					Data = "AgM="
				};

				imposter.AddStub().On(new ContainsPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("Zmlyc3QgcmVzcG9uc2U=");

				// Second stub
				predicateFields = new TcpPredicateFields
				{
					Data = "Bwg="
				};

				imposter.AddStub().On(new ContainsPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("c2Vjb25kIHJlc3BvbnNl");

				// Third stub
				predicateFields = new TcpPredicateFields
				{
					Data = "Bwg="
				};

				imposter.AddStub().On(new ContainsPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("dGhpcmQgcmVzcG9uc2U=");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the startsWith predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task StartsWithPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4548, "StartsWithPredicateExample", imposter =>
			{
				// First stub
				var predicateFields = new TcpPredicateFields
				{
					Data = "first"
				};

				imposter.AddStub().On(new StartsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("first response");

				// Second stub
				predicateFields = new TcpPredicateFields
				{
					Data = "second"
				};

				imposter.AddStub().On(new StartsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("second response");

				// Third stub
				predicateFields = new TcpPredicateFields
				{
					Data = "second"
				};

				imposter.AddStub().On(new StartsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("third response");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the endsWith predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task EndsWithPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4549, "EndsWithPredicateExample", TcpMode.Binary, imposter =>
			{
				// First stub
				var predicateFields = new TcpPredicateFields
				{
					Data = "AwQ="
				};

				imposter.AddStub().On(new EndsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("Zmlyc3QgcmVzcG9uc2U=");

				// Second stub
				predicateFields = new TcpPredicateFields
				{
					Data = "BQY="
				};

				imposter.AddStub().On(new EndsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("c2Vjb25kIHJlc3BvbnNl");

				// Third stub
				predicateFields = new TcpPredicateFields
				{
					Data = "BQY="
				};

				imposter.AddStub().On(new EndsWithPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("dGhpcmQgcmVzcG9uc2U=");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the matches predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task MatchesPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4550, "MatchesPredicateExample", imposter =>
			{
				// First stub
				var predicateFields = new TcpPredicateFields
				{
					Data = "^first\\Wsecond"
				};

				imposter.AddStub().On(new MatchesPredicate<TcpPredicateFields>(predicateFields, true, null, null))
					.ReturnsData("first response");

				// Second stub
				predicateFields = new TcpPredicateFields
				{
					Data = "second\\s+request"
				};

				imposter.AddStub().On(new MatchesPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("second response");

				// Third stub
				predicateFields = new TcpPredicateFields
				{
					Data = "second\\s+request"
				};

				imposter.AddStub().On(new MatchesPredicate<TcpPredicateFields>(predicateFields))
					.ReturnsData("third response");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the exists predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task ExistsPredicateExample()
		{
			await _client.CreateHttpImposterAsync(4550, "ExistsPredicateExample", imposter =>
			{
				// First stub
				var predicateFields = new HttpPredicateFields
				{
					RequestBody = new
					{
						Message = true
					}
				};

				imposter.AddStub().On(new ExistsPredicate<HttpPredicateFields>(predicateFields))
					.ReturnsBody(HttpStatusCode.OK, "Success");

				// Second stub
				predicateFields = new HttpPredicateFields
				{
					RequestBody = new
					{
						Message = false
					}
				};

				imposter.AddStub().On(new ExistsPredicate<HttpPredicateFields>(predicateFields))
					.ReturnsBody(HttpStatusCode.BadRequest, "You need to add a message parameter");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the not predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task NotPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4552, "NotPredicateExample", imposter =>
			{
				var predicate = new EqualsPredicate<TcpPredicateFields>(new TcpPredicateFields { Data = "test\n" });

				// First stub
				imposter.AddStub().On(new NotPredicate(predicate))
					.ReturnsData("not test");

				// Second stub
				imposter.AddStub().On(predicate)
					.ReturnsData("test");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the or predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task OrPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4553, "OrPredicateExample", imposter =>
			{
				var startsWithFields = new TcpPredicateFields { Data = "start" };
				var startsWith = new StartsWithPredicate<TcpPredicateFields>(startsWithFields);

				var endsWithFields = new TcpPredicateFields { Data = "end\n" };
				var endsWith = new EndsWithPredicate<TcpPredicateFields>(endsWithFields);

				var containsFields = new TcpPredicateFields { Data = "middle" };
				var contains = new ContainsPredicate<TcpPredicateFields>(containsFields);

				var predicate = new OrPredicate(new List<Predicate> { startsWith, endsWith, contains });

				imposter.AddStub().On(predicate)
					.ReturnsData("matches");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the and predicate example
		/// at http://www.mbtest.org/docs/api/predicates.
		/// </summary>
		[TestMethod]
		public async Task AndPredicateExample()
		{
			await _client.CreateTcpImposterAsync(4554, "AndPredicateExample", imposter =>
			{
				var startsWithFields = new TcpPredicateFields { Data = "start" };
				var startsWith = new StartsWithPredicate<TcpPredicateFields>(startsWithFields);

				var endsWithFields = new TcpPredicateFields { Data = "end\n" };
				var endsWith = new EndsWithPredicate<TcpPredicateFields>(endsWithFields);

				var containsFields = new TcpPredicateFields { Data = "middle" };
				var contains = new ContainsPredicate<TcpPredicateFields>(containsFields);

				var predicate = new AndPredicate(new List<Predicate> { startsWith, endsWith, contains });

				imposter.AddStub().On(predicate)
					.ReturnsData("matches");
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the json example
		/// at http://localhost:2525/docs/api/json.
		/// </summary>
		[TestMethod]
		public async Task JsonExample()
		{
			await _client.CreateHttpImposterAsync(4545, "JsonExample", imposter =>
			{
				var caseSensitiveFields = new HttpPredicateFields { RequestBody = new Book { Title = "Harry Potter" } };
				var caseSensitivePredicate = new EqualsPredicate<HttpPredicateFields>(caseSensitiveFields, true, null, null);

				var exceptFields = new HttpPredicateFields { RequestBody = new Book { Title = "POTTER" } };
				var exceptPredicate = new EqualsPredicate<HttpPredicateFields>(exceptFields, false, "HARRY ", null);

				var matchesFields = new HttpPredicateFields { RequestBody = new Book { Title = "^Harry" } };
				var matchesPredicate = new MatchesPredicate<HttpPredicateFields>(matchesFields);

				// Exists examples not provided since MbDotNet does not yet support checking specific object keys

				imposter.AddStub()
					.On(caseSensitivePredicate)
					.On(exceptPredicate)
					.On(matchesPredicate)
					.ReturnsJson(HttpStatusCode.OK, new BookResponse { Code = "SUCCESS", Author = "J.K. Rowling" });
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the stub with wait behavior
		/// at https://www.mbtest.org/docs/api/behaviors#behavior-wait
		/// </summary>
		[TestMethod]
		public async Task WaitBehaviorExample()
		{
			await _client.CreateHttpImposterAsync(4546, "WaitBehaviorExample", imposter =>
			{
				imposter.AddStub().Returns(HttpStatusCode.OK, new Dictionary<string, object>(),
					"This took at least half a second to send", latencyInMilliseconds: 500);
			});
		}

		/// <summary>
		/// This test shows how to setup the imposter in the inject predicate example
		/// at http://www.mbtest.org/docs/api/injection.
		/// </summary>
		[TestMethod]
		public async Task HttpInjectPredicateExample()
		{
			await _client.CreateHttpImposterAsync(4547, "HttpInjectPredicateExample", imposter =>
			{
				const string injectedFunction =
					"function (config) {\r\n\r\n    function hasXMLProlog () {\r\n        return config.request.body.indexOf('<?xml') === 0;\r\n    }\r\n\r\n    if (config.request.headers['Content-Type'] === 'application/xml') {\r\n        return !hasXMLProlog();\r\n    }\r\n    else {\r\n        return hasXMLProlog();\r\n    }\r\n}";
				imposter.AddStub().OnInjectedFunction(injectedFunction).ReturnsStatus(HttpStatusCode.BadRequest);
			});
		}
	}

	public class Book
	{
		public string Title { get; set; }
	}

	public class BookResponse
	{
		public string Code { get; set; }
		public string Author { get; set; }
	}

	public class Customer
	{
		public string Email { get; set; }
	}
}
