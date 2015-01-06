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
using System.Reflection;

namespace System
{

  public class Delegate
  {
#if SILVERLIGHT_4_0_WP
    internal Delegate() { }
#else
    private Delegate() { }
#endif

#if false
    extern public System.Reflection.MethodInfo Method
    {
      get;
    }
#endif
    extern public object Target
    {
      get;
    }

    public MethodInfo Method
    {
      get
      {
        Contract.Ensures(Contract.Result<MethodInfo>() != null);
        return default(MethodInfo);
      }
    }
#if false
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {

    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator !=(Delegate d1, Delegate d2)
    {

      return default(bool);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator ==(Delegate d1, Delegate d2)
    {

      return default(bool);
    }
#if false
    public static Delegate CreateDelegate(Type type, System.Reflection.MethodInfo method)
    {
      Contract.Requires(type != null);
      Contract.Requires(method != null);

      return default(Delegate);
    }
#endif
    [Pure]
    public static Delegate CreateDelegate(Type type, Type target, string method)
    {
      Contract.Requires(type != null);
      Contract.Requires(target != null);
      Contract.Requires(method != null);

      return default(Delegate);
    }
    [Pure]
    public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
    {
      Contract.Requires(type != null);
      Contract.Requires(target != null);
      Contract.Requires(method != null);

      return default(Delegate);
    }
    [Pure]
    public static Delegate CreateDelegate(Type type, object target, string method)
    {
      Contract.Requires(type != null);
      Contract.Requires(target != null);
      Contract.Requires(method != null);

      return default(Delegate);
    }

#if !SILVERLIGHT
    public static Delegate RemoveAll(Delegate source, Delegate value)
    {
      return default(Delegate);
    }
#endif
    [Pure]
    public static Delegate Remove(Delegate a, Delegate b)
    {
      //Contract.Ensures(((object)a) == null && ((object)b) == null ==> ((object)result) == null);
      //Contract.Ensures(((object)a) == null && ((object)b) != null ==> ((object)result) == null);
      //Contract.Ensures(((object)a) != null && ((object)b) == null ==> ((object)result) == (object)a);
      //Contract.Ensures(((object)a) != null && ((object)b) != null ==> ((object)result) != null && result.GetType() == a.GetType() && Owner.Same(result, a));
      Contract.Ensures(!(((object)a) == null && ((object)b) == null) || ((object)Contract.Result<Delegate>()) == null);
      Contract.Ensures(!(((object)a) == null && ((object)b) != null) || ((object)Contract.Result<Delegate>()) == (object)b);
      Contract.Ensures(!(((object)a) != null && ((object)b) == null) || ((object)Contract.Result<Delegate>()) == (object)a);
      Contract.Ensures(!(((object)a) != null && ((object)b) != null) || ((object)Contract.Result<Delegate>()) != null && Contract.Result<Delegate>().GetType() == a.GetType());

      return default(Delegate);
    }
    [Pure]
    public virtual Delegate[] GetInvocationList()
    {

      return default(Delegate[]);
    }
#if !SILVERLIGHT
    [Pure]
    public static Delegate Combine(Delegate[] delegates)
    {

      return default(Delegate);
    }
#endif
    [Pure]
    public static Delegate Combine(Delegate a, Delegate b)
    {
      //Contract.Ensures(((object)a) == null && ((object)b) == null ==> ((object)result) == null);
      //Contract.Ensures(((object)a) == null && ((object)b) != null ==> ((object)result) == null);
      //Contract.Ensures(((object)a) != null && ((object)b) == null ==> ((object)result) == (object)a);
      //Contract.Ensures(((object)a) != null && ((object)b) != null ==> ((object)result) != null && result.GetType() == a.GetType() && Owner.Same(result, a));
      Contract.Ensures(!(((object)a) == null && ((object)b) == null) || ((object)Contract.Result<Delegate>()) == null);
      Contract.Ensures(!(((object)a) == null && ((object)b) != null) || ((object)Contract.Result<Delegate>()) == (object)b);
      Contract.Ensures(!(((object)a) != null && ((object)b) == null) || ((object)Contract.Result<Delegate>()) == (object)a);
      Contract.Ensures(!(((object)a) != null && ((object)b) != null) || ((object)Contract.Result<Delegate>()) != null && Contract.Result<Delegate>().GetType() == a.GetType());

      return default(Delegate);
    }

    public object DynamicInvoke(Object[] args)
    {
      return default(object);
    }
  }
}
