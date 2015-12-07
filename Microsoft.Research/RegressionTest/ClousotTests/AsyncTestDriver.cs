// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class AsyncTestDriver
    {
        private delegate void IsolatedAction<in T>(T obj, out Exception exceptionThrown, out string dataReceived);

        public static readonly uint MaxWaitHandles_Default = Math.Max(1, Math.Min(4, (uint)(Environment.ProcessorCount - 1)));
        public static readonly uint MaxWaitHandles_AllButOne = Math.Max(1, (uint)(Environment.ProcessorCount - 1));

        private static readonly int SingleTestMaxWait = 200000;

        private readonly ITestOutputHelper testOutputHelper;
        private readonly Action<ITestOutputHelper, Options, Output> action;
        private readonly IsolatedAction<Options> actionDelegate;
        private readonly Func<Options, bool> skipTest;
        private Dictionary<string, IAsyncResult> testAsyncResults;
        private readonly uint maxWaitHandles;
        private WaitHandle[] waitHandles;
        private int nbWaitHandles;
        private bool beginTestsProcessed = false;
        private bool orderReversed = false;

        public string BeginMessage;

        public AsyncTestDriver(ITestOutputHelper testOutputHelper, Action<ITestOutputHelper, Options, Output> action, Func<Options, bool> skipTest)
          : this(testOutputHelper, action, skipTest, MaxWaitHandles_Default)
        { }

        public AsyncTestDriver(ITestOutputHelper testOutputHelper, Action<ITestOutputHelper, Options, Output> action, Func<Options, bool> skipTest, uint maxWaitHandles)
        {
            this.testOutputHelper = testOutputHelper;
            this.action = action;
            this.actionDelegate = this.ActionAsIsolated;
            this.skipTest = skipTest;
            this.maxWaitHandles = maxWaitHandles;
        }

        // We have no control on the order of the tests, so we make sure
        // to always call Begin before End

        public void BeginTest(Options options)
        {
            if (skipTest(options))
                return;

            beginTestsProcessed = true;

            if (orderReversed)
                this.EndTestInternal(options);
            else
                this.BeginTestInternal(options);
        }

        public void EndTest(Options options)
        {
            if (skipTest(options))
                return;

            if (!beginTestsProcessed)
                orderReversed = true;

            if (orderReversed)
                this.BeginTestInternal(options);
            else
                this.EndTestInternal(options);
        }

        private void BeginTestInternal(Options options)
        {
            try
            {
                if (testAsyncResults == null)
                    testAsyncResults = new Dictionary<string, IAsyncResult>();

                if (waitHandles == null)
                    waitHandles = new WaitHandle[maxWaitHandles];

                var index = nbWaitHandles;
                if (index == waitHandles.Length)
                {
                    index = WaitHandle.WaitAny(waitHandles, waitHandles.Length * SingleTestMaxWait);
                    Assert.NotEqual(index, WaitHandle.WaitTimeout);
                    nbWaitHandles--;
                }

                Exception dummyOutException;
                string dummyOutString;
                var asyncResult = actionDelegate.BeginInvoke(options, out dummyOutException, out dummyOutString, null, null);
                testAsyncResults.Add(options.TestName, asyncResult);
                waitHandles[index] = asyncResult.AsyncWaitHandle;
                nbWaitHandles++;

                testOutputHelper.WriteLine(this.BeginMessage);
            }
            catch (Exception e)
            {
                testOutputHelper.WriteLine("EXCEPTION: {0}", e.Message);
                Assert.True(false, "Exception caught");
            }
        }

        private void EndTestInternal(Options options)
        {
            Assert.NotNull(testAsyncResults);

            IAsyncResult asyncResult;
            if (!testAsyncResults.TryGetValue(options.TestName, out asyncResult))
                Assert.True(false, "Begin part of the test not run");

            testAsyncResults.Remove(options.TestName);

            Assert.True(asyncResult.AsyncWaitHandle.WaitOne(SingleTestMaxWait), "Test timed out");

            Exception exceptionThrown;
            string dataReceived;
            actionDelegate.EndInvoke(out exceptionThrown, out dataReceived, asyncResult);

            testOutputHelper.WriteLine(string.Empty);
            testOutputHelper.WriteLine("This test case was performed {0}synchronously", asyncResult.CompletedSynchronously ? "" : "a");
            testOutputHelper.WriteLine(string.Empty);

            testOutputHelper.WriteLine(dataReceived);
            if (exceptionThrown != null)
                throw exceptionThrown;
        }



        private void ActionAsIsolated(Options options, out Exception exceptionThrown, out string dataReceived)
        {
            using (var stringWriter = new StringWriter())
            {
                var output = new Output(testOutputHelper, String.Format("Isolated::{0}", options.TestName), stringWriter);
                exceptionThrown = null;
                try
                {
                    action(testOutputHelper, options, output);
                }
                catch (Exception e)
                {
                    exceptionThrown = e;
                }
                dataReceived = stringWriter.ToString();
            }
        }
    }
}
