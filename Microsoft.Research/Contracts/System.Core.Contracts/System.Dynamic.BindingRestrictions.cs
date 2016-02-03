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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;

namespace System.Dynamic
{
  // Summary:
  //     Represents a set of binding restrictions on the System.Dynamic.DynamicMetaObject
  //     under which the dynamic binding is valid.
  public abstract class BindingRestrictions
  {
     private BindingRestrictions() { }

    // Summary:
    //     Represents an empty set of binding restrictions. This field is read only.
    public static readonly BindingRestrictions Empty;

    // Summary:
    //     Combines binding restrictions from the list of System.Dynamic.DynamicMetaObject
    //     instances into one set of restrictions.
    //
    // Parameters:
    //   contributingObjects:
    //     The list of System.Dynamic.DynamicMetaObject instances from which to combine
    //     restrictions.
    //
    // Returns:
    //     The new set of binding restrictions.
    public static BindingRestrictions Combine(IList<DynamicMetaObject> contributingObjects)
    {
      Contract.Ensures(Contract.Result<BindingRestrictions>() != null);

      return null;
    }
    //
    // Summary:
    //     Creates the binding restriction that checks the expression for arbitrary
    //     immutable properties.
    //
    // Parameters:
    //   expression:
    //     The expression representing the restrictions.
    //
    // Returns:
    //     The new binding restrictions.
    public static BindingRestrictions GetExpressionRestriction(Expression expression)
    {
      Contract.Ensures(Contract.Result<BindingRestrictions>() != null);

      return default(BindingRestrictions);
    }
    //
    // Summary:
    //     Creates the binding restriction that checks the expression for object instance
    //     identity.
    //
    // Parameters:
    //   expression:
    //     The expression to test.
    //
    //   instance:
    //     The exact object instance to test.
    //
    // Returns:
    //     The new binding restrictions.
    public static BindingRestrictions GetInstanceRestriction(Expression expression, object instance)
    {
      Contract.Requires(expression != null);

      Contract.Ensures(Contract.Result<BindingRestrictions>() != null);

      return default(BindingRestrictions);
    }

    //
    // Summary:
    //     Creates the binding restriction that check the expression for runtime type
    //     identity.
    //
    // Parameters:
    //   expression:
    //     The expression to test.
    //
    //   type:
    //     The exact type to test.
    //
    // Returns:
    //     The new binding restrictions.
    public static BindingRestrictions GetTypeRestriction(Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);

      Contract.Ensures(Contract.Result<BindingRestrictions>() != null);

      return default(BindingRestrictions);
    }

    //
    // Summary:
    //     Merges the set of binding restrictions with the current binding restrictions.
    //
    // Parameters:
    //   restrictions:
    //     The set of restrictions with which to merge the current binding restrictions.
    //
    // Returns:
    //     The new set of binding restrictions.
    public BindingRestrictions Merge(BindingRestrictions restrictions)
    {
      Contract.Requires(restrictions != null);

      Contract.Ensures(Contract.Result<BindingRestrictions>() != null);

      return null;
    }
    //
    // Summary:
    //     Creates the System.Linq.Expressions.Expression representing the binding restrictions.
    //
    // Returns:
    //     The expression tree representing the restrictions.
    public Expression ToExpression()
    {
      Contract.Ensures(Contract.Result<Expression>() != null);

      return null;
    }

  }
}
#endif