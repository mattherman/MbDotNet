using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests
{
    internal class AcceptanceTestRunner
    {
        public delegate void TestPassed(string testName, long elapsed);
        public delegate void TestFailed(string testName, long elapsed, Exception ex);
        public delegate void TestSkipped(string testName, string reason);

        private TestPassed _testPassedHandler;
        private TestFailed _testFailedHandler;
        private TestSkipped _testSkippedHandler;

        private IList<Type> _tests; 

        public AcceptanceTestRunner(IList<Type> tests, TestPassed passHandler, TestFailed failHandler, TestSkipped skipHandler)
        {
            if (passHandler == null || failHandler == null || skipHandler == null)
            {
                throw new ArgumentNullException("Test result handlers cannot be null.");
            }

            _tests = tests ?? throw new ArgumentNullException("Test collection cannot be null.");
            _testPassedHandler = passHandler;
            _testFailedHandler = failHandler;
            _testSkippedHandler = skipHandler;
        }

        public async Task Execute()
        {
            foreach (var testClass in _tests)
            {
                if (!(Activator.CreateInstance(testClass) is AcceptanceTest testInstance))
                {
                    _testSkippedHandler(testClass.Name, "The test class did not inherit from AcceptanceTest.");
                    continue;
                }

                var stopwatch = Stopwatch.StartNew();
                bool success = false;
                Exception error = null;

                try
                {
                    await testInstance.Run().ConfigureAwait(false);
                    success = true;
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                finally
                {
                    stopwatch.Stop();
                }

                var elapsed = stopwatch.Elapsed.Milliseconds;
                if (success)
                {
                    _testPassedHandler(testClass.Name, elapsed); 
                }
                else
                {
                    _testFailedHandler(testClass.Name, elapsed, error);
                }
            }   
        }
    }
}
