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

// Some helper classes for tracing the behavior of the abstract domain, and the definition of the exception to be thrown when some error occured in the abstract domains

namespace Microsoft.Research.AbstractDomains
{
  using System;
  using System.IO;
  using System.Text;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.CodeDom.Compiler;
  using System.Xml;
  using System.Runtime.InteropServices;
  using Microsoft.Research.CodeAnalysis;
  using System.Diagnostics.Contracts;
 


  /// <summary>
  /// A class that does nothing....
  /// </summary>
#if SUBPOLY_ONLY
  internal
#else
  public 
#endif
    class NullTextWriter : TextWriter
  {
    public override Encoding Encoding
    {
      get { return Encoding.ASCII; }
    }
  }

  /// <summary>
  /// Exception to be thrown when an error occured in the abstract domains
  /// </summary>
 [Serializable]
  public class AbstractInterpretationException : Exception
  {
    public AbstractInterpretationException(string  msg)
      : base(msg)
    {
      Contract.Requires(msg != null);
    }

    public AbstractInterpretationException()
      : base()
    {
    }
  }

  /// <summary>
  /// Exception to be thrown when there is something not implemented yet
  /// </summary>
  [Serializable]
  public class AbstractInterpretationTODOException : Exception
  {
    public AbstractInterpretationTODOException(string msg)
      : base(msg)
    {
      Contract.Requires(msg != null);
    }

    public AbstractInterpretationTODOException()
      : base()
    {
    }
  }


#if SUBPOLY_ONLY
  public class TimeoutExceptionFixpointComputation : Exception
  {
  }
#endif
}