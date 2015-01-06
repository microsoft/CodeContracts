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

// Investigation for bug 845105
// Customer wants a postcondition saying Guid.NewGuid() != Guid.Empty.
//   -->  Raymond Chen says CoCreateGuid should never return Guid.Empty or it 
//        would violate its attempt at a uniqueness guarantee.
// Customer wants the static checker to understand String.IsNullOrWhiteSpace
//        This test passes the static checker when it should theoretically fail.  
//        Manuel says the static checker doesn't look at the content of a String,
//        only the length & non-nullness.
// Customer has a predicate involving booleans that doesn't work with the byte
//   value 0 for some reason, unless we carefully write the check to deal with 0 explicitly.
namespace SpecificContractTest
{
  class Program
  {
    //private static void GuidTest(Guid guid)
    //{
    //  Contract.Requires(guid != Guid.Empty);
    //  Console.WriteLine("Got a guid: {0}", guid);
    //}

    //private static void WhitespaceTest(String s)
    //{
    //  Contract.Requires(!String.IsNullOrWhiteSpace(s), "want a non-null and non-whitespace string");
    //  Console.WriteLine("Got a string: \"{0}\"", s);
    //}

    public static void SomeMethod(byte displayOrder)
    {
      Contract.Requires(IsValidDisplayOrder(displayOrder));
    }

    [Pure]
    public static bool IsValidDisplayOrder(byte value)
    {


      // Customer reports that the first two of these forms cause the static checker to complain that the
      // precondition is not proven (when you pass in 0).  The third works - if you explicitly test for 0.
      Contract.Ensures(Contract.Result<bool>() == !(value < 0 || value > 19));
      
      //Contract.Ensures(Contract.Result<bool>() == (value >= 0 && value <= 19));
  
      //Contract.Ensures(Contract.Result<bool>() == ((value == 0) || (value > 0 && value <= 19));
    
      return (value >= 0 && value <= 19);
    }

    static void Main(string[] args)
    {
      //Guid g = Guid.NewGuid();
      //GuidTest(g);

      // BUG: If the next two lines are left in, the static checker throws an exception on this program.
      // If you comment them out, then you can see the problem with IsValidDisplayOrder.
      //String s = " ";
      //WhitespaceTest(s);

      // Should be valid.
      SomeMethod(150);
      SomeMethod(1);
    }
  }
}
