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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System
{
#if !SILVERLIGHT_4_0_WP
  namespace Runtime.InteropServices
  {
    [ContractClass(typeof(_ExceptionContract))]
    public interface _Exception
    {
      Type GetType();
    }

    [ContractClassFor(typeof(_Exception))]
    abstract class _ExceptionContract : _Exception
    {
      new public Type GetType()
      {
        Contract.Ensures(Contract.Result<Type>() != null);

        return default(Type);
      }
    }
  }
#endif

  public class Exception 
#if !SILVERLIGHT_4_0_WP
    : _Exception
#endif
  {
#if !SILVERLIGHT
    extern public virtual string HelpLink
    {
      get;
      set;
    }
#endif

    extern new public virtual Type GetType();

    /// <summary>
    /// May be null if exception has not been thrown
    /// </summary>
    public virtual string StackTrace
    {
      get
      {
        return default(string);
      }
    }

#if !SILVERLIGHT
    extern public virtual string Source
    {
      get;
      set;
    }
#endif

    extern public virtual Exception InnerException
    {
      get;
    }

    public virtual string Message
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

#if false
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);

    }
#endif

    public virtual Exception GetBaseException()
    {
      return default(Exception);
    }
    public Exception(string message, Exception innerException)
    {
    }
    public Exception(string message)
    {
    }
    public Exception()
    {
    }
  }
}
