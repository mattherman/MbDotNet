using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    /// <summary>
    /// This file contains unit tests that show how to setup the same
    /// imposters described in the examples in the mountebank documentation.
    /// See the comment above each individual test to figure out which 
    /// example it is describing.
    /// 
    /// The [TestMethod] annotations are commented out because each of these
    /// requires mountebank to be running in order to run the tests. If you
    /// would like to try them out, start mountebank on your local machine,
    /// uncomment the [TestMethod] annotations, and run them like any of the
    /// other unit tests.
    /// </summary>
    internal class DocumentationTests : AcceptanceTest
    {
        private const int ImposterPort = 6000;

        public override async Task Run()
        {
            await RunExample(StubExample).ConfigureAwait(false);
            await RunExample(DynamicPortExample).ConfigureAwait(false);
            await RunExample(EqualsPredicateExample).ConfigureAwait(false);
            await RunExample(DeepEqualsPredicateExample).ConfigureAwait(false);
            await RunExample(ContainsPredicateExample).ConfigureAwait(false);
            await RunExample(StartsWithPredicateExample).ConfigureAwait(false);
            await RunExample(EndsWithPredicateExample).ConfigureAwait(false);
            await RunExample(MatchesPredicateExample).ConfigureAwait(false);
            await RunExample(NotPredicateExample).ConfigureAwait(false);
            await RunExample(AndPredicateExample).ConfigureAwait(false);
            await RunExample(OrPredicateExample).ConfigureAwait(false);
            await RunExample(JsonExample).ConfigureAwait(false);
        }

        private async Task RunExample(Func<Task> example)
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
            await example().ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the stub example
        /// at http://www.mbtest.org/docs/api/stubs.
        /// </summary>
        public async Task StubExample()
        {
            var imposter = _client.CreateHttpImposter(4545, "StubExample");
            imposter.AddStub().OnPathAndMethodEqual("/customers/123", Method.Post)
                .ReturnsXml(HttpStatusCode.Created, new Customer { Email = "customer@test.com" })
                .ReturnsBody(HttpStatusCode.BadRequest, "<error>Email already exists</error>");

            await _client.SubmitAsync(imposter);
        }

        /// <summary>
        /// This test shows how to setup the imposter with a dynamic port chosen by Mountebank
        /// See imposter resource at http://www.mbtest.org/docs/api/contracts for more information.
        /// </summary>
        public async Task DynamicPortExample()
        {
            var imposter = _client.CreateHttpImposter(null, "DynamicPort");

            await _client.SubmitAsync(imposter).ConfigureAwait(false);

            var portAssignedByMountebank = imposter.Port;
        }

        /// <summary>
        /// This test shows how to setup the imposter in the equals predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task EqualsPredicateExample()
        {
            var imposter = _client.CreateHttpImposter(4545, "EqualsPredicateExample");

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
                QueryParameters = new Dictionary<string, string> { { "first", "1" }, { "second", "2" } },
                Headers = new Dictionary<string, string> { { "Accept", "text/plain" } }
            };

            var complexPredicate = new EqualsPredicate<HttpPredicateFields>(complexPredicateFields);

            imposter.AddStub().On(complexPredicate).On(bodyPredicate).ReturnsStatus(HttpStatusCode.BadRequest);

            // Second stub
            var fields = new HttpPredicateFields
            {
                Headers = new Dictionary<string, string> { { "Accept", "application/xml" } }
            };

            imposter.AddStub().On(new EqualsPredicate<HttpPredicateFields>(fields)).ReturnsStatus(HttpStatusCode.NotAcceptable);

            // Third stub
            imposter.AddStub().OnMethodEquals(Method.Put).ReturnsStatus((HttpStatusCode)405);

            // Fourth stub
            imposter.AddStub().OnMethodEquals(Method.Put).ReturnsStatus((HttpStatusCode)500);

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the deepEquals predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task DeepEqualsPredicateExample()
        {
            var imposter = _client.CreateHttpImposter(4556, "DeepEqualsPredicateExample");

            // First stub
            var predicateFields = new HttpPredicateFields
            {
                QueryParameters = new Dictionary<string, string>()
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
                QueryParameters = new Dictionary<string, string> { { "first", "1" } }
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
                QueryParameters = new Dictionary<string, string> { { "first", "1" }, { "second", "2" } }
            };

            responseFields = new HttpResponseFields
            {
                ResponseObject = "third"
            };

            imposter.AddStub().On(new DeepEqualsPredicate<HttpPredicateFields>(predicateFields))
                .Returns(new IsResponse<HttpResponseFields>(responseFields));

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the contains predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task ContainsPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4547, "ContainsPredicateExample", TcpMode.Binary);

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

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the startsWith predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task StartsWithPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4548, "StartsWithPredicateExample");

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

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the endsWith predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task EndsWithPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4549, "EndsWithPredicateExample", TcpMode.Binary);

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

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the matches predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task MatchesPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4550, "MatchesPredicateExample");

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

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the not predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task NotPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4552, "NotPredicateExample");

            var predicate = new EqualsPredicate<TcpPredicateFields>(new TcpPredicateFields { Data = "test\n" });

            // First stub
            imposter.AddStub().On(new NotPredicate(predicate))
                .ReturnsData("not test");

            // Second stub
            imposter.AddStub().On(predicate)
                .ReturnsData("test");

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the or predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task OrPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4553, "OrPredicateExample");

            var startsWithFields = new TcpPredicateFields { Data = "start" };
            var startsWith = new StartsWithPredicate<TcpPredicateFields>(startsWithFields); 

            var endsWithFields = new TcpPredicateFields { Data = "end\n" };
            var endsWith = new EndsWithPredicate<TcpPredicateFields>(endsWithFields);

            var containsFields = new TcpPredicateFields { Data = "middle" };
            var contains = new ContainsPredicate<TcpPredicateFields>(containsFields);

            var predicate = new OrPredicate(new List<PredicateBase> { startsWith, endsWith, contains });

            imposter.AddStub().On(predicate)
                .ReturnsData("matches");

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the and predicate example
        /// at http://www.mbtest.org/docs/api/predicates.
        /// </summary>
        public async Task AndPredicateExample()
        {
            var imposter = _client.CreateTcpImposter(4554, "AndPredicateExample");

            var startsWithFields = new TcpPredicateFields { Data = "start" };
            var startsWith = new StartsWithPredicate<TcpPredicateFields>(startsWithFields); 

            var endsWithFields = new TcpPredicateFields { Data = "end\n" };
            var endsWith = new EndsWithPredicate<TcpPredicateFields>(endsWithFields);

            var containsFields = new TcpPredicateFields { Data = "middle" };
            var contains = new ContainsPredicate<TcpPredicateFields>(containsFields);

            var predicate = new AndPredicate(new List<PredicateBase> { startsWith, endsWith, contains });

            imposter.AddStub().On(predicate)
                .ReturnsData("matches");

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        /// <summary>
        /// This test shows how to setup the imposter in the json example
        /// at http://localhost:2525/docs/api/json.
        /// </summary>
        public async Task JsonExample()
        {
            var imposter = _client.CreateHttpImposter(4545, "JsonExample");

            var caseSensitiveFields = new HttpPredicateFields { RequestBody = new Book { Title = "Harry Potter" }};
            var caseSensitivePredicate = new EqualsPredicate<HttpPredicateFields>(caseSensitiveFields, true, null, null);

            var exceptFields = new HttpPredicateFields { RequestBody = new Book { Title = "POTTER" }};
            var exceptPredicate = new EqualsPredicate<HttpPredicateFields>(exceptFields, false, "HARRY ", null);

            var matchesFields = new HttpPredicateFields { RequestBody = new Book { Title = "^Harry" }};
            var matchesPredicate = new MatchesPredicate<HttpPredicateFields>(matchesFields);

            // Exists examples not provided since MbDotNet does not yet support checking specific object keys

            imposter.AddStub()
                .On(caseSensitivePredicate)
                .On(exceptPredicate)
                .On(matchesPredicate)
                .ReturnsJson(HttpStatusCode.OK, new BookResponse { Code = "SUCCESS", Author = "J.K. Rowling"});

            await _client.SubmitAsync(imposter).ConfigureAwait(false);
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
