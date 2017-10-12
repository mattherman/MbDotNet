using System;
using MbDotNet;
using System.Collections.Generic;

namespace MbDotNet.Acceptance.Tests
{
    public class Program
    {
        private static int _passed = 0;
        private static int _failed = 0;
        private static int _skipped = 0;

        public static void Main()
        {
            var tests = new List<Type>
            {
                typeof(AcceptanceTests.CanNotGetImposterThatDoesNotExist),
                typeof(AcceptanceTests.CanCreateAndGetHttpImposter),
                typeof(AcceptanceTests.CanCreateAndGetHttpsImposter),
                typeof(AcceptanceTests.CanCreateAndGetTcpImposter),
                typeof(AcceptanceTests.CanDeleteImposter),
                typeof(AcceptanceTests.CanVerifyCallsOnImposter),
                typeof(AcceptanceTests.CanCreateAndGetHttpImposterWithNoPort)
            };

            var runner = new AcceptanceTestRunner(tests, OnTestPassing, OnTestFailing, OnTestSkipped);
            runner.Execute();

            Console.WriteLine("\nFINISHED {0} passed, {1} failed, {2} skipped", _passed, _failed, _skipped);

            if (_failed > 0)
            {
                Environment.Exit(1);
            }
        }

        public static void OnTestPassing(string testName, long elapsed)
        {
            Console.WriteLine("PASS {0} ({1}ms)", testName, elapsed);
            _passed++;
        }

        public static void OnTestFailing(string testName, long elapsed, Exception ex)
        {
            Console.WriteLine("FAIL {0} ({1}ms)\n\t=> {2}", testName, elapsed, ex.Message);
            _failed++;
        }

        public static void OnTestSkipped(string testName, string reason)
        {
            Console.WriteLine("SKIP {0} [{1}]", testName, reason);
            _skipped++;
        }
    }
}
