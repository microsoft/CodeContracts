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
// using System.Runtime.ConstrainedExecution;

namespace System.Runtime.CompilerServices
{
  // Summary:
  //     Provides a set of static methods and properties that provide support for
  //     compilers. This class cannot be inherited.
  public static class RuntimeHelpers
  {
    // Summary:
    //     Gets the offset in bytes to the data in the given string.
    //
    // Returns:
    //     The byte offset, from the start of the System.String object to the first
    //     character in the string.
    public static int OffsetToStringData 
    {
      get
      {
        // F: in my installation is 12, but I guess it changes with the versions of the framework
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    // Summary:
    //     Determines whether the specified System.Object instances are considered equal.
    //
    // Parameters:
    //   o1:
    //     The first System.Object to compare.
    //
    //   o2:
    //     The second System.Object to compare.
    //
    // Returns:
    //     true if the o1 parameter is the same instance as the o2 parameter or if both
    //     are null or if o1.Equals(o2) returns true; otherwise, false.
    //public static bool Equals(object o1, object o2);
    //
    // Summary:
    //     Executes code using a System.Delegate while using another System.Delegate
    //     to execute additional code in case of an exception.
    //
    // Parameters:
    //   code:
    //     A System.Delegate to the code to try.
    //
    //   backoutCode:
    //     A System.Delegate to the code to run if a exception occurs.
    //
    //   userData:
    //     The data to pass to code and backoutCode.
    //public static void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);
    //
    // Summary:
    //     Serves as a hash function for a particular type, suitable for use in hashing
    //     algorithms and data structures such as a hash table.
    //
    // Parameters:
    //   o:
    //     An System.Object to retrieve the hash code for.
    //
    // Returns:
    //     A hash code for the System.Object identified by the o parameter.
    public static int GetHashCode(object o)
    {
      Contract.Requires(o != null);

      return default(int);
    }
    //
    // Summary:
    //     Boxes a value type.
    //
    // Parameters:
    //   obj:
    //     The value type to be boxed.
    //
    // Returns:
    //     Returns a boxed copy of obj if it is a value class; otherwise obj itself
    //     is returned.
    //public static object GetObjectValue(object obj);
    //
    // Summary:
    //     Provides a fast way to initialize an array from data stored in a module.
    //
    // Parameters:
    //   array:
    //     The array to be initialized.
    //
    //   fldHandle:
    //     A System.RuntimeFieldHandle specifying the location of the data used to initialize
    //     the array.
    //public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle);
    //
    // Summary:
    //     Designates a body of code as a constrained execution region (CER).
    //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    //public static void PrepareConstrainedRegions();
    //
    // Summary:
    //     Designates a body of code as a constrained execution region (CER) without
    //     performing any probing.
    //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    //public static void PrepareConstrainedRegionsNoOP();
    //
    // Summary:
    //     Indicates that the specified delegate be prepared for inclusion in a constrained
    //     execution region (CER).
    //
    // Parameters:
    //   d:
    //     The System.Delegate type to prepare.
    //public static void PrepareDelegate(Delegate d);
    //
    // Summary:
    //     Prepares a method for inclusion in a constrained execution region (CER).
    //
    // Parameters:
    //   method:
    //     A System.RuntimeMethodHandle to the method to prepare.
    //public static void PrepareMethod(RuntimeMethodHandle method);
    //
    // Summary:
    //     Prepares a method for inclusion in a constrained execution region (CER) with
    //     the specified instantiation.
    //
    // Parameters:
    //   method:
    //     A System.RuntimeMethodHandle to the method to prepare.
    //
    //   instantiation:
    //     A System.RuntimeTypeHandle instantiation to pass.
    //public static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation);
    //
    // Summary:
    //     This method is used by the Constrained Execution Region (CER) infrastructure,
    //     when running in hosts that are resilient to stack overflow such as Microsoft
    //     SQL & Microsoft Exchange. It probes for a certain amount of stack space,
    //     for the purpose of ensuring that a stack overflow cannot happen within a
    //     following block of code (assuming that your code itself only uses a finite
    //     and moderate amount of stack space). This method currently probes for 48K
    //     of stack space on x86, but the exact amount may change over time & vary on
    //     other platforms. This method is not recommended. Instead, you should use
    //     a normal CER (ie, a try/finally or try/catch block proceeded with a call
    //     to System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions()),
    //     if you're going to use a moderate amount of stack space. If you are calling
    //     a recursive method or will use a lot of stack space, then you must use System.Runtime.CompilerServices.RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(System.Runtime.CompilerServices.RuntimeHelpers.TryCode,System.Runtime.CompilerServices.RuntimeHelpers.CleanupCode,System.Object).
    //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    //public static void ProbeForSufficientStack();
    //
    // Summary:
    //     Runs a specified class constructor method.
    //
    // Parameters:
    //   type:
    //     A System.RuntimeTypeHandle specifying the class constructor method to run.
    //
    // Exceptions:
    //   System.TypeInitializationException:
    //     The class initializer threw an exception.
    //public static void RunClassConstructor(RuntimeTypeHandle type);
    //
    // Summary:
    //     Runs a specified module constructor method.
    //
    // Parameters:
    //   module:
    //     A System.ModuleHandle object specifying the module constructor method to
    //     run.
    //
    // Exceptions:
    //   System.TypeInitializationException:
    //     The module constructor threw an exception.
    //public static void RunModuleConstructor(ModuleHandle module);

    // Summary:
    //     Represents a method to run when an exception occurs.
    //
    // Parameters:
    //   userData:
    //     Data to pass to the delegate.
    //
    //   exceptionThrown:
    //     true to express that an exception was thrown; otherwise, false.
    //public delegate void CleanupCode(object userData, bool exceptionThrown);

    // Summary:
    //     Represents a delegate to code that should be run in a try block..
    //
    // Parameters:
    //   userData:
    //     Data to pass to the delegate.
    //public delegate void TryCode(object userData);
  }
}
