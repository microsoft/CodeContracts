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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public abstract class MSILVisitor<Label, Local, Parameter, Method, Field, Type, Source, Dest, Data, Result> :
    IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Data, Result>
  {

    protected abstract Result Default(Label pc, Data data);


    #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Source,Dest,Data,Result> Members

    public virtual Result Arglist(Label pc, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Binary(Label pc, BinaryOperator op, Dest dest, Source s1, Source s2, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result BranchCond(Label pc, Label target, BranchOperator bop, Source value1, Source value2, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result BranchFalse(Label pc, Label target, Source cond, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result BranchTrue(Label pc, Label target, Source cond, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Branch(Label pc, Label target, bool leave, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Break(Label pc, Data data)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public virtual Result Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>
    {
      return Default(pc, data);
    }

    public virtual Result Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>
    {
      return Default(pc, data);
    }

    public virtual Result Ckfinite(Label pc, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Cpblk(Label pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Endfilter(Label pc, Source decision, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Endfinally(Label pc, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Initblk(Label pc, bool @volatile, Source destaddr, Source value, Source len, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Jmp(Label pc, Method method, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldarg(Label pc, Parameter argument, bool isOld, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldarga(Label pc, Parameter argument, bool isOld, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldconst(Label pc, object constant, Type type, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldnull(Label pc, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldftn(Label pc, Method method, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldind(Label pc, Type type, bool @volatile, Dest dest, Source ptr, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldloc(Label pc, Local local, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldloca(Label pc, Local local, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldstack(Label pc, int offset, Dest dest, Source source, bool isOld, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldstacka(Label pc, int offset, Dest dest, Source source, Type type, bool isOld, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Localloc(Label pc, Dest dest, Source size, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Pop(Label pc, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Return(Label pc, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Starg(Label pc, Parameter argument, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Stind(Label pc, Type type, bool @volatile, Source ptr, Source value, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Stloc(Label pc, Local local, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Switch(Label pc, Type type, IEnumerable<Pair<object,Label>> cases, Source value, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Box(Label pc, Type type, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>
    {
      return Default(pc, data);
    }

    public virtual Result Castclass(Label pc, Type type, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Cpobj(Label pc, Type type, Source destptr, Source srcptr, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Initobj(Label pc, Type type, Source ptr, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Isinst(Label pc, Type type, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldelem(Label pc, Type type, Dest dest, Source array, Source index, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldelema(Label pc, Type type, bool @readonly, Dest dest, Source array, Source index, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldfld(Label pc, Field field, bool @volatile, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldflda(Label pc, Field field, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldlen(Label pc, Dest dest, Source array, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldsfld(Label pc, Field field, bool @volatile, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldsflda(Label pc, Field field, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldtypetoken(Label pc, Type type, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldfieldtoken(Label pc, Field type, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldmethodtoken(Label pc, Method type, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldvirtftn(Label pc, Method method, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Mkrefany(Label pc, Type type, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Newarray<ArgList>(Label pc, Type type, Dest dest, ArgList lengths, Data data)
      where ArgList : IIndexable<Source>
    {
      return Default(pc, data);
    }

    public virtual Result Newobj<ArgList>(Label pc, Method ctor, Dest dest, ArgList args, Data data)
      where ArgList : IIndexable<Source>
    {
      return Default(pc, data);
    }

    public virtual Result Refanytype(Label pc, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Refanyval(Label pc, Type type, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Rethrow(Label pc, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Sizeof(Label pc, Type type, Dest dest, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Stelem(Label pc, Type type, Source array, Source index, Source value, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Stfld(Label pc, Field field, bool @volatile, Source obj, Source value, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Stsfld(Label pc, Field field, bool @volatile, Source value, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Throw(Label pc, Source exn, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Unbox(Label pc, Type type, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Unboxany(Label pc, Type type, Dest dest, Source obj, Data data)
    {
      return Default(pc, data);
    }

    #endregion

    #region IVisitSynthIL<Source,Data,Result> Members

    public virtual Result Assume(Label pc, string tag, Source condition, object provenance, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Nop(Label pc, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Assert(Label pc, string tag, Source condition, object provenance, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Entry(Label pc, Method method, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result BeginOld(Label pc, Label matchingEnd, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result EndOld(Label pc, Label matchingBegin, Type type, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    public virtual Result Ldresult(Label pc, Type type, Dest dest, Source source, Data data)
    {
      return Default(pc, data);
    }

    #endregion
  }

}