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


#if NETFRAMEWORK_4_0

using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Threading;


namespace System
{
  // Summary:
  //     Provides support for lazy initialization.
  //
  // Type parameters:
  //   T:
  //     Specifies the type of object that is being lazily initialized.
  public class Lazy<T>
  {



    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class. When lazy initialization
    //     occurs, the default constructor of the target type is used.
    extern public Lazy();
    //
    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class. When lazy initialization
    //     occurs, the default constructor of the target type and the specified initialization
    //     mode are used.
    //
    // Parameters:
    //   isThreadSafe:
    //     true to make this instance usable concurrently by multiple threads; false
    //     to make the instance usable by only one thread at a time.
    public Lazy(bool isThreadSafe) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class. When lazy initialization
    //     occurs, the specified initialization function is used.
    //
    // Parameters:
    //   valueFactory:
    //     The delegate that is invoked to produce the lazily initialized value when
    //     it is needed.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     valueFactory is null.
    public Lazy(Func<T> valueFactory)
    {
      Contract.Requires(valueFactory != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class that uses the default
    //     constructor of T and the specified thread-safety mode.
    //
    // Parameters:
    //   mode:
    //     The thread safety mode.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     mode contains an invalid value.
    public Lazy(LazyThreadSafetyMode mode)
    {
      Contract.Requires(mode == LazyThreadSafetyMode.ExecutionAndPublication || mode == LazyThreadSafetyMode.PublicationOnly || mode == LazyThreadSafetyMode.None);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class. When lazy initialization
    //     occurs, the specified initialization function and initialization mode are
    //     used.
    //
    // Parameters:
    //   valueFactory:
    //     The delegate that is invoked to produce the lazily initialized value when
    //     it is needed.
    //
    //   isThreadSafe:
    //     true to make this instance usable concurrently by multiple threads; false
    //     to make this instance usable by only one thread at a time.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     valueFactory is null.
    public Lazy(Func<T> valueFactory, bool isThreadSafe)
    {
      Contract.Requires(valueFactory != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Lazy<T> class that uses the specified
    //     initialization function and thread-safety mode.
    //
    // Parameters:
    //   valueFactory:
    //     The delegate that is invoked to produce the lazily initialized value when
    //     it is needed.
    //
    //   mode:
    //     The thread safety mode.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     mode contains an invalid value.
    //
    //   System.ArgumentNullException:
    //     valueFactory is null.
    public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
    {
      Contract.Requires(valueFactory != null);
      Contract.Requires(mode == LazyThreadSafetyMode.ExecutionAndPublication || mode == LazyThreadSafetyMode.PublicationOnly || mode == LazyThreadSafetyMode.None);
    }

    // Summary:
    //     Gets a value that indicates whether a value has been created for this System.Lazy<T>
    //     instance.
    //
    // Returns:
    //     true if a value has been created for this System.Lazy<T> instance; otherwise,
    //     false.
    extern public bool IsValueCreated { get; }
    //
    // Summary:
    //     Gets the lazily initialized value of the current System.Lazy<T> instance.
    //
    // Returns:
    //     The lazily initialized value of the current System.Lazy<T> instance.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     The System.Lazy<T> instance is initialized to use the default constructor
    //     of the type that is being lazily initialized, and permissions to access the
    //     constructor are missing.
    //
    //   System.MissingMemberException:
    //     The System.Lazy<T> instance is initialized to use the default constructor
    //     of the type that is being lazily initialized, and that type does not have
    //     a public, parameterless constructor.
    //
    //   System.InvalidOperationException:
    //     The initialization function tries to access System.Lazy<T>.Value on this
    //     instance.
    //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
    extern public T Value { get; }
  }
}

namespace System.Threading
{
  public enum LazyThreadSafetyMode
  {
    None,
    PublicationOnly,
    ExecutionAndPublication
  }
}

#endif