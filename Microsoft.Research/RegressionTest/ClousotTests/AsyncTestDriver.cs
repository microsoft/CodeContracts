// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  public class AsyncTestDriver
  {
    delegate void IsolatedAction<in T>(T obj, out Exception exceptionThrown, out string dataReceived);

    public static readonly uint MaxWaitHandles_Default = Math.Max(1, Math.Min(4, (uint)(Environment.ProcessorCount - 1)));
    public static readonly uint MaxWaitHandles_AllButOne = Math.Max(1, (uint)(Environment.ProcessorCount - 1));

    private static readonly int SingleTestMaxWait = 200000;

    private readonly Action<Options, Output> action;
    private readonly IsolatedAction<Options> actionDelegate;
    private readonly Func<Options, bool> skipTest;
    private Dictionary<string, IAsyncResult> testAsyncResults;
    private readonly uint maxWaitHandles;
    private WaitHandle[] waitHandles;
    private int nbWaitHandles;
    private bool beginTestsProcessed = false;
    private bool orderReversed = false;

    public string BeginMessage;

    public AsyncTestDriver(Action<Options, Output> action, Func<Options, bool> skipTest)
      : this(action, skipTest, MaxWaitHandles_Default)
    { }

    public AsyncTestDriver(Action<Options, Output> action, Func<Options, bool> skipTest, uint maxWaitHandles)
    {
      this.action = action;
      this.actionDelegate = this.ActionAsIsolated;
      this.skipTest = skipTest;
      this.maxWaitHandles = maxWaitHandles;
    }

    // We have no control on the order of the tests, so we make sure
    // to always call Begin before End

    public void BeginTest(Options options)
    {
      if (this.skipTest(options))
        return;

      this.beginTestsProcessed = true;

      if (this.orderReversed)
        this.EndTestInternal(options);
      else
        this.BeginTestInternal(options);
    }

    public void EndTest(Options options)
    {
      if (this.skipTest(options))
        return;

      if (!this.beginTestsProcessed)
        this.orderReversed = true;

      if (this.orderReversed)
        this.BeginTestInternal(options);
      else
        this.EndTestInternal(options);
    }

    private void BeginTestInternal(Options options)
    {
      try
      {
        if (this.testAsyncResults == null)
          this.testAsyncResults = new Dictionary<string, IAsyncResult>();

        if (this.waitHandles == null)
          this.waitHandles = new WaitHandle[this.maxWaitHandles];

        var index = nbWaitHandles;
        if (index == waitHandles.Length)
        {
          index = WaitHandle.WaitAny(waitHandles, waitHandles.Length * SingleTestMaxWait);
          Assert.AreNotEqual(index, WaitHandle.WaitTimeout, "Previous tests timed out");
          this.nbWaitHandles--;
        }

        Exception dummyOutException;
        string dummyOutString;
        var asyncResult = this.actionDelegate.BeginInvoke(options, out dummyOutException, out dummyOutString, null, null);
        this.testAsyncResults.Add(options.TestName, asyncResult);
        this.waitHandles[index] = asyncResult.AsyncWaitHandle;
        this.nbWaitHandles++;

        Console.WriteLine(this.BeginMessage);
      }
      catch (Exception e)
      {
        Console.WriteLine("EXCEPTION: {0}", e.Message);
        Assert.Fail("Exception caught");
      }
    }

    private void EndTestInternal(Options options)
    {
      Assert.IsNotNull(this.testAsyncResults, "Begin part of the test not selected");

      IAsyncResult asyncResult;
      if (!this.testAsyncResults.TryGetValue(options.TestName, out asyncResult))
        Assert.Fail("Begin part of the test not run");

      this.testAsyncResults.Remove(options.TestName);

      Assert.IsTrue(asyncResult.AsyncWaitHandle.WaitOne(SingleTestMaxWait), "Test timed out");

      Exception exceptionThrown;
      string dataReceived;
      this.actionDelegate.EndInvoke(out exceptionThrown, out dataReceived, asyncResult);

      Console.WriteLine();
      Console.WriteLine("This test case was performed {0}synchronously", asyncResult.CompletedSynchronously ? "" : "a");
      Console.WriteLine();

      Console.Write(dataReceived);
      if (exceptionThrown != null)
        throw exceptionThrown;
    }



    private void ActionAsIsolated(Options options, out Exception exceptionThrown, out string dataReceived)
    {
      using (var stringWriter = new StringWriter())
      {
        var output = new Output(String.Format("Isolated::{0}", options.TestName), stringWriter);
        exceptionThrown = null;
        try
        {
          this.action(options, output);
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
