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
using Microsoft.Pex.Framework;

public static partial class ClassWithProtocolFactory
{
  [PexFactoryMethod(typeof(global::ClassWithProtocol))]
  public static global::ClassWithProtocol Create(string data_s, string data_c, int cases)
  {
    global::ClassWithProtocol classWithProtocol = new global::ClassWithProtocol();
    switch (cases)
    {
      case 1:
        classWithProtocol.Initialize(data_s);
        return classWithProtocol;
      case 2:
        classWithProtocol.Initialize(data_s);
        classWithProtocol.Compute(data_c);
        return classWithProtocol;
        
      default:
        return classWithProtocol;
    }
    // TODO: Edit factory method of ClassWithProtocol
    // This method should be able to configure the object in all possible ways.
    // Add as many parameters as needed,
    // and assign their values to each field by using the API.
  }
}
