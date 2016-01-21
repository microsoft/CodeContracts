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

#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using Microsoft.Research.ClousotRegression;


#endregion

namespace BrianStrelioff
{
  public interface IValidationAttribute
  {
  }

  internal class Class5
  {
    public delegate MemberInfo[] InvocationHelper();

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=67,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=80,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=168,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=118,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=181,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=20,MethodILOffset=149)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 62, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 75, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 157, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 170, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 113, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 20, MethodILOffset = 138)]
#endif
    public static IList<KnownAttributes<TInfo>> LookupAttributes<TInfo>(
        object target,
        Dictionary<Type, IList<KnownAttributes<TInfo>>> known,
        InvocationHelper helper) where TInfo : MemberInfo
    {
      Contract.Requires(target != null);
      Contract.Requires(known != null);
      Contract.Requires(helper != null);

      IList<KnownAttributes<TInfo>> result;
      var type = target.GetType();
      lock (known)
      {
        if (!known.TryGetValue(type, out result))
        {
          var infos = helper();
          Contract.Assume(infos != null); // ensures infos is not null   
          result = new List<KnownAttributes<TInfo>>();
          foreach (TInfo info in infos) // possibly unboxing a null reference   
          {
            if (info != null)
            {
              var temp = new KnownAttributes<TInfo>(info); // Flagged as related   
            }
          }
          known[type] = result;
        }
      }
      return result;
    }
  }

  public class KnownAttributes<TInfo>
      where TInfo : MemberInfo
  {
    public KnownAttributes(TInfo info)
    {
      Contract.Requires(info != null); // flagged as unproven   

      _info = info;
      _attributes = new List<IValidationAttribute>();
    }

    [ContractInvariantMethod]
    private void ObjectInvariants()
    {
      Contract.Invariant(_attributes != null);
    }

    private readonly IList<IValidationAttribute> _attributes;
    private readonly TInfo _info;

    public IList<IValidationAttribute> Attributes
    {
      get
      {
        Contract.Assert(_attributes != null);
        return _attributes;
      }
    }

    public TInfo Info
    {
      get { return _info; }
    }
  }

  public class Class2<TProxy, TInterface>
    where TProxy : IDisposable, new()
    where TInterface : class
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 68, MethodILOffset = 0)]
    public Class2()
    {
      _proxyObject = new TProxy();
      _interfaceObject = _proxyObject as TInterface;
    }
    protected readonly TInterface _interfaceObject;

    protected readonly TProxy _proxyObject;
  }


}
