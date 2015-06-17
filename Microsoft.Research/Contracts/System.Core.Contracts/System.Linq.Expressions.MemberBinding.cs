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
using System.Reflection;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Linq.Expressions
{
  // Summary:
  //     Describes the binding types that are used in System.Linq.Expressions.MemberInitExpression
  //     objects.
  public enum MemberBindingType
  {
    // Summary:
    //     A binding that represents initializing a member with the value of an expression.
    Assignment = 0,
    //
    // Summary:
    //     A binding that represents recursively initializing members of a member.
    MemberBinding = 1,
    //
    // Summary:
    //     A binding that represents initializing a member of type System.Collections.IList
    //     or System.Collections.Generic.ICollection<T> from a list of elements.
    ListBinding = 2,
  }

  // Summary:
  //     Provides the base class from which the classes that represent bindings that
  //     are used to initialize members of a newly created object derive.
  public abstract class MemberBinding
  {
    // Summary:
    //     Initializes a new instance of the System.Linq.Expressions.MemberBinding class.
    //
    // Parameters:
    //   type:
    //     The System.Linq.Expressions.MemberBindingType that discriminates the type
    //     of binding that is represented.
    //
    //   member:
    //     The System.Reflection.MemberInfo that represents a field or property to be
    //     initialized.

    protected MemberBinding(MemberBindingType type, MemberInfo member)
    {
      Contract.Requires(member != null);
    }

    // Summary:
    //     Gets the type of binding that is represented.
    //
    // Returns:
    //     One of the System.Linq.Expressions.MemberBindingType values.
    extern public MemberBindingType BindingType { get; }
    //
    // Summary:
    //     Gets the field or property to be initialized.
    //
    // Returns:
    //     The System.Reflection.MemberInfo that represents the field or property to
    //     be initialized.
    public MemberInfo Member
    {
      get
      {
        Contract.Ensures(Contract.Result<MemberInfo>() != null);
        return default(MemberInfo);
      }
    }

  }
}