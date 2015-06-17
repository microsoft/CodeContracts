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
using System.IO;
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  class Clousot2SWorkerFactory : RemoteClousotWorkerFactory<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference>
  {
    private static FList<string> AddExtraArgs(FList<string> args, string serviceAddress)
    {
      if (!String.IsNullOrWhiteSpace(serviceAddress) && serviceAddress != "*") // if not specified and not wildcard, use default address
        args = args.Cons("-serviceAddress:" + serviceAddress);
      return args;
    }

    public Clousot2SWorkerFactory(IScheduler scheduler, FList<string> args, ISimpleLineWriter output, string serviceAddress)
      : base(scheduler, AddExtraArgs(args, serviceAddress), output)
    { }

    // Here we invoke the remote service
    protected override int CallClousotMain(string[] args, TextWriter output)
    {
      string[] dummy;
      return ClousotViaService.Main2(args, output, out dummy);
    }
  }
}
