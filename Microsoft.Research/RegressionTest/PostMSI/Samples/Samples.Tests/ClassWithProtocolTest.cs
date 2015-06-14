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

// <copyright file="ClassWithProtocolTest.cs" company="Microsoft">Copyright © Microsoft 2009</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>This class contains parameterized unit tests for ClassWithProtocol</summary>
[TestClass]
[PexClass(typeof(global::ClassWithProtocol))]
[PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
[PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
public partial class ClassWithProtocolTest
{
    /// <summary>Test stub for Compute(String)</summary>
    [PexMethod]
    public bool Compute([PexAssumeUnderTest]global::ClassWithProtocol target, string prefix)
    {
        // TODO: add assertions to method ClassWithProtocolTest.Compute(ClassWithProtocol, String)
        bool result = target.Compute(prefix);
        return result;
    }

    /// <summary>Test stub for ComputedData</summary>
    [PexMethod, PexAllowedException("Samples", "System.Diagnostics.Contracts.__ContractsRuntime+ContractException")]
    public void ComputedDataGet([PexAssumeUnderTest]global::ClassWithProtocol target)
    {
        // TODO: add assertions to method ClassWithProtocolTest.ComputedDataGet(ClassWithProtocol)
        string result = target.ComputedData;
    }

    /// <summary>Test stub for .ctor()</summary>
    [PexMethod]
    public global::ClassWithProtocol Constructor()
    {
        // TODO: add assertions to method ClassWithProtocolTest.Constructor()
        global::ClassWithProtocol target = new global::ClassWithProtocol();
        return target;
    }

    /// <summary>Test stub for Data</summary>
    [PexMethod, PexAllowedException("Samples", "System.Diagnostics.Contracts.__ContractsRuntime+ContractException")]
    public void DataGet([PexAssumeUnderTest]global::ClassWithProtocol target)
    {
        // TODO: add assertions to method ClassWithProtocolTest.DataGet(ClassWithProtocol)
        string result = target.Data;
    }

    /// <summary>Test stub for Initialize(String)</summary>
    [PexMethod]
    public void Initialize([PexAssumeUnderTest]global::ClassWithProtocol target, string data)
    {
        // TODO: add assertions to method ClassWithProtocolTest.Initialize(ClassWithProtocol, String)
        target.Initialize(data);
    }

    /// <summary>Test stub for State</summary>
    [PexMethod]
    public void StateGet([PexAssumeUnderTest]global::ClassWithProtocol target)
    {
        // TODO: add assertions to method ClassWithProtocolTest.StateGet(ClassWithProtocol)
        global::ClassWithProtocol.S result = target.State;
    }
}
