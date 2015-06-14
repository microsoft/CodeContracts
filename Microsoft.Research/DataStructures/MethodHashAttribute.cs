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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  [Flags]
  public enum MethodHashAttributeFlags
  {
    Default = 0,
    OnlyFromCache = 1, // means that the result of the analysis can only be read from cache but shouldn't be computed by clousot (e.g. method with empty body)
    Reuse = 2, // means that we should try to get the latest available result for the id of this method (ignoring its hash)
    IdIsHash = 4, // means that the method id should also be considered is hash (e.g. method with empty body)
    IgnoreRegression = 8, // means that we should ignore the regression attribute for this method
    ReplayOnlyInferredContracts = 16, // means that we read from the cache, outcomes, suggestions, stats, etc. should not be replayed (e.g. APCs coming from other assemblies)
    FirstInOrder = 32, // means that the methodOrder will put these methods first

    ForDependenceMethod = OnlyFromCache | Reuse | IdIsHash | IgnoreRegression | ReplayOnlyInferredContracts | FirstInOrder,
  }

  public class MethodHashAttribute : Attribute
  {
    private readonly ByteArray methodId;
    private readonly MethodHashAttributeFlags flags;

    public MethodHashAttribute(ByteArray methodId, MethodHashAttributeFlags flags)
    {
      this.methodId = methodId;
      this.flags = flags;
    }

    public static MethodHashAttribute FromDecoder(IIndexable<object> args)
    {
      if (args == null || args.Count != 2)
        return null;

      var hashObj = args[0]; // it can be null, when generated at the Slicer level
      var flagsObj = args[1];

      if ((hashObj == null || hashObj is string) && (flagsObj is int))
      {
        var flags = (MethodHashAttributeFlags)(int)flagsObj;
        var methodHash = new ByteArray(hashObj as string);

        return new MethodHashAttribute(methodHash, flags);
      }
      else
      {
        return null;
      }
    }

    public ByteArray MethodId { get { return this.methodId; } }
    public MethodHashAttributeFlags Flags { get { return this.flags; } }

    // For the code writer
    public string Arg1 { get { return this.methodId == null ? "" : this.methodId.ToString(); } }
    public int Arg2 { get { return (int)this.flags; } }
  }
}
