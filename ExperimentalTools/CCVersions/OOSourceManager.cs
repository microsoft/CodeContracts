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
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace CCVersions
{
  class OOSourceManagerException : Exception
  {
    public OOSourceManagerException()
    {
    }

    public OOSourceManagerException(string message)
      : base(message)
    {
    }

    public OOSourceManagerException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }

  class OOSourceManager : ISourceManager
  {
    public static OOSourceManager Factory(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      return new OOSourceManager(xConfig);
    }

    private OOSourceManager(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      var xLocalId = xConfig.Element("localid");
      if (xLocalId != null)
      {
        this.Id = xLocalId.Value;
      }
    }

    public string Id { get; private set; }

    public string WorkingDirectory { get; set; }

    public ISourceManagerVersionSpec ParseVersionSpec(string value)
    {
      throw new OOSourceManagerException(String.Format("{0} is not a real source manager", this.Id));
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter = null)
    {
      return new ISourceManagerVersionSpec[0];
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(int countPrior, ISourceManagerVersionSpec Last)
    {
      return new ISourceManagerVersionSpec[0];
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, int countAfter)
    {
      return new ISourceManagerVersionSpec[0];
    }

    public bool GetSources(ISourceManagerVersionSpec Version)
    {
      return false;
    }

    public bool Build(IEnumerable<string> ProjectsPath, long VersionId, string OutputDirectory)
    {
      return false;
    }
  }
}
