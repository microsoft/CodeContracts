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
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{

  public static class PrinterFactory
  {

    public static ILPrinter<Label> Create<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>(
      IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      Converter<Source, string> sourceName,
      Converter<Dest, string> destName
    )
    {
      Contract.Requires(ilDecoder != null);// F: Added as of Clousot suggestion

      ILPrinter<Label> printer = new Printer<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>(ilDecoder, mdDecoder, sourceName, destName).PrintCodeAt;
      return printer;
    }

    private class Printer<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData> :
      IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, TextWriter, Unit>
    {
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(ilDecoder != null); // F: Added as of Clousot suggestions of preconditions
      }

      string prefix = "";
      IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder;
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
      Converter<Source, string> sourceName;
      Converter<Dest, string> destName;

      public Printer(
        IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        Converter<Source, string> sourceName,
        Converter<Dest, string> destName
      )
      {
        Contract.Requires(ilDecoder != null);// F: As of Clousot suggestion

        this.ilDecoder = ilDecoder;
        this.mdDecoder = mdDecoder;
        this.sourceName = sourceName;
        this.destName = destName;
      }

      public void PrintCodeAt(Label label, string prefix, TextWriter writer)
      {
        this.prefix = prefix;
        this.ilDecoder.ForwardDecode<TextWriter, Unit, Printer<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>>(label, this, writer);
      }

      #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Source,Dest,TextWriter,Unit> Members

      [Pure]
      private string/*?*/ SourceName(Source src)
      {
        if (this.sourceName != null) { return this.sourceName(src); }
        return null;
      }

      [Pure]
      private string/*?*/ DestName(Dest dest)
      {
        if (this.destName != null) { return this.destName(dest); }
        return null;
      }

      public Unit Binary(Label pc, BinaryOperator op, Dest dest, Source s1, Source s2, TextWriter data)
      {
        data.WriteLine("{0}{1} = {2} {3} {4}", prefix, DestName(dest), SourceName(s1), op.ToString(), SourceName(s2));
        return Unit.Value;
      }

      public Unit Assert(Label pc, string tag, Source s, object provenance, TextWriter tw)
      {
        tw.WriteLine("{0}assert({1}) {2}", prefix, tag, SourceName(s));
        return Unit.Value;
      }

      public Unit Assume(Label pc, string tag, Source s, object provenance, TextWriter tw)
      {
        tw.WriteLine("{0}assume({1}) {2}", prefix, tag, SourceName(s));
        return Unit.Value;
      }

      public Unit Arglist(Label pc, Dest d, TextWriter data)
      {
        data.WriteLine("{0}{1} = arglist", prefix, DestName(d));
        return Unit.Value;
      }

      public Unit BranchCond(Label pc, Label target, BranchOperator bop, Source value1, Source value2, TextWriter data)
      {
        data.WriteLine("{0}br.{1} {2},{3}", prefix, bop, SourceName(value1), SourceName(value2));
        return Unit.Value;
      }

      public Unit BranchTrue(Label pc, Label target, Source cond, TextWriter data)
      {
        data.WriteLine("{0}br.true {1}", prefix, SourceName(cond));
        return Unit.Value;
      }

      public Unit BranchFalse(Label pc, Label target, Source cond, TextWriter data)
      {
        data.WriteLine("{0}br.false {1}", prefix, SourceName(cond));
        return Unit.Value;
      }

      public Unit Branch(Label pc, Label target, bool leave, TextWriter data)
      {
        data.WriteLine("{0}branch", prefix);
        return Unit.Value;
      }

      public Unit Break(Label pc, TextWriter data)
      {
        data.WriteLine("{0}break", prefix);
        return Unit.Value;
      }

      public Unit Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, TextWriter data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        data.Write("{0}{4} = {1}call{3} {2}(", prefix, tail ? "tail." : null, mdDecoder.FullName(method), virt ? "virt" : null, DestName(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            var tmp = string.Format("{0} ", SourceName(args[i]));
            data.Write(tmp);
          }
        }
        data.WriteLine(")");
        return Unit.Value;
      }

      public Unit Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, TextWriter data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        data.Write("{0}{2} = {1}calli {3}(", prefix, tail ? "tail." : null, DestName(dest), SourceName(fp));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            data.Write("{0} ", SourceName(args[i]));
          }
        }
        data.WriteLine(")");
        return Unit.Value;
      }

      public Unit Ckfinite(Label pc, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{1} = ckinite {2}", prefix, DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Unary(Label pc, UnaryOperator op, bool ovf, bool unsigned, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{4} = {3}{1}{2} {5}", prefix, ovf ? "_ovf" : null, unsigned ? "_un" : null, op.ToString(), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Cpblk(Label pc, bool @volatile, Source destaddr, Source srcaddr, Source len, TextWriter data)
      {
        data.WriteLine("{0}{1}cpblk {2} {3} {4}", prefix, @volatile ? "volatile." : null, SourceName(destaddr), SourceName(srcaddr), SourceName(len));
        return Unit.Value;
      }

      public Unit Endfilter(Label pc, Source condition, TextWriter data)
      {
        data.WriteLine("{0}endfilter {1}", prefix, SourceName(condition));
        return Unit.Value;
      }

      public Unit Endfinally(Label pc, TextWriter data)
      {
        data.WriteLine("{0}endfinally", prefix);
        return Unit.Value;
      }

      public Unit Entry(Label pc, Method method, TextWriter data)
      {
        data.WriteLine("{0}method_entry {1}", prefix, this.mdDecoder.FullName(method));
        return Unit.Value;
      }

      public Unit Initblk(Label pc, bool @volatile, Source destaddr, Source value, Source len, TextWriter data)
      {
        data.WriteLine("{0}{1}initblk {2} {3} {4}", prefix, @volatile ? "volatile." : null, SourceName(destaddr), SourceName(value), SourceName(len));
        return Unit.Value;
      }

      public Unit Jmp(Label pc, Method method, TextWriter data)
      {
        data.WriteLine("{0}jmp {1}", prefix, mdDecoder.FullName(method));
        return Unit.Value;
      }

      public Unit Ldarg(Label pc, Parameter argument, bool isOld, Dest dest, TextWriter data)
      {
        string isOldPrefix = isOld ? "old." : null;
        data.WriteLine("{0}{2} = {3}ldarg {1}", prefix, mdDecoder.Name(argument), DestName(dest), isOldPrefix);
        return Unit.Value;
      }

      public Unit Ldarga(Label pc, Parameter argument, bool isOld, Dest dest, TextWriter data)
      {
        string isOldPrefix = isOld ? "old." : null;
        data.WriteLine("{0}{2} = {3}ldarga {1}", prefix, mdDecoder.Name(argument), DestName(dest), isOldPrefix);
        return Unit.Value;
      }

      public Unit Ldconst(Label pc, object constant, Type type, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldc ({3})'{1}'", prefix, constant.ToString(), DestName(dest), mdDecoder.FullName(type));
        return Unit.Value;
      }

      public Unit Ldnull(Label pc, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{1} = ldnull", prefix, DestName(dest));
        return Unit.Value;
      }

      public Unit Ldftn(Label pc, Method method, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldftn {1}", prefix, mdDecoder.FullName(method), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldind(Label pc, Type type, bool @volatile, Dest dest, Source ptr, TextWriter data)
      {
        data.WriteLine("{0}{3} = {1}ldind {2} {4}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(type), DestName(dest), SourceName(ptr));
        return Unit.Value;
      }

      public Unit Ldloc(Label pc, Local local, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldloc {1}", prefix, mdDecoder.Name(local), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldloca(Label pc, Local local, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldloca {1}", prefix, mdDecoder.Name(local), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldresult(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{1} = ldresult {2}", prefix, DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Ldstack(Label pc, int offset, Dest dest, Source s, bool isOld, TextWriter data)
      {
        if (isOld)
        {
          data.WriteLine("{0}{1} = old.ldstack.{2} {3}", prefix, DestName(dest), offset, SourceName(s));
        }
        else
        {
          data.WriteLine("{0}{1} = ldstack.{2} {3}", prefix, DestName(dest), offset, SourceName(s));
        }
        return Unit.Value;
      }

      public Unit Ldstacka(Label pc, int offset, Dest dest, Source s, Type type, bool old, TextWriter data)
      {
        if (old)
        {
          data.WriteLine("{0}{1} = old.ldstacka.{2} {3}", prefix, DestName(dest), offset, SourceName(s));
        }
        else
        {
          data.WriteLine("{0}{1} = ldstacka.{2} {3}", prefix, DestName(dest), offset, SourceName(s));
        }
        return Unit.Value;
      }

      public Unit Localloc(Label pc, Dest dest, Source size, TextWriter data)
      {
        data.WriteLine("{0}{1} = localloc {2}", prefix, DestName(dest), SourceName(size));
        return Unit.Value;
      }


      public Unit Nop(Label pc, TextWriter data)
      {
        data.WriteLine("{0}nop", prefix);
        return Unit.Value;
      }

      public Unit Pop(Label pc, Source source, TextWriter data)
      {
        data.WriteLine("{0}pop {1}", prefix, SourceName(source));
        return Unit.Value;
      }

      public Unit Return(Label pc, Source source, TextWriter data)
      {
        data.WriteLine("{0}ret {1}", prefix, SourceName(source));
        return Unit.Value;
      }

      public Unit Starg(Label pc, Parameter argument, Source source, TextWriter data)
      {
        data.WriteLine("{0}starg {1} {2}", prefix, mdDecoder.Name(argument), SourceName(source));
        return Unit.Value;
      }

      public Unit Stind(Label pc, Type type, bool @volatile, Source ptr, Source value, TextWriter data)
      {
        data.WriteLine("{0}{1}stind {2} {3} {4}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(type), SourceName(ptr), SourceName(value));
        return Unit.Value;
      }

      public Unit Stloc(Label pc, Local local, Source value, TextWriter data)
      {
        data.WriteLine("{0}stloc {1} {2}", prefix, mdDecoder.Name(local), SourceName(value));
        return Unit.Value;
      }

      public Unit Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, Source value, TextWriter data)
      {
        data.WriteLine("{0}switch {1}", prefix, SourceName(value));
        return Unit.Value;
      }

      public Unit Box(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = box {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, TextWriter data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        data.Write("{0}{4} = {1}constrained({2}).callvirt {3}(", prefix, tail ? "tail." : null, mdDecoder.FullName(constraint), mdDecoder.FullName(method), DestName(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            data.Write("{0} ", SourceName(args[i]));
          }
        }
        data.WriteLine(")");
        return Unit.Value;
      }

      public Unit Castclass(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = castclass {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Cpobj(Label pc, Type type, Source destptr, Source srcptr, TextWriter data)
      {
        data.WriteLine("{0}cpobj {1} {2} {3}", prefix, mdDecoder.FullName(type), SourceName(destptr), SourceName(srcptr));
        return Unit.Value;
      }

      public Unit Initobj(Label pc, Type type, Source ptr, TextWriter data)
      {
        data.WriteLine("{0}initobj {1} {2}", prefix, mdDecoder.FullName(type), SourceName(ptr));
        return Unit.Value;
      }

      public Unit Isinst(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = isinst {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Ldelem(Label pc, Type type, Dest dest, Source array, Source index, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldelem {1} {3}[{4}]", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(array), SourceName(index));
        return Unit.Value;
      }

      public Unit Ldelema(Label pc, Type type, bool @readonly, Dest dest, Source array, Source index, TextWriter data)
      {
        data.WriteLine("{0}{3} = {1}ldelema {2} {4}[{5}]", prefix, @readonly ? "readonly." : null, mdDecoder.FullName(type), DestName(dest), SourceName(array), SourceName(index));
        return Unit.Value;
      }

      public Unit Ldfld(Label pc, Field field, bool @volatile, Dest dest, Source ptr, TextWriter data)
      {
        data.WriteLine("{0}{3} = {1}ldfld {2} {4}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(field), DestName(dest), SourceName(ptr));
        return Unit.Value;
      }

      public Unit Ldflda(Label pc, Field field, Dest dest, Source ptr, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldflda {1} {3}", prefix, mdDecoder.FullName(field), DestName(dest), SourceName(ptr));
        return Unit.Value;
      }

      public Unit Ldlen(Label pc, Dest dest, Source array, TextWriter data)
      {
        data.WriteLine("{0}{1} = ldlen {2}", prefix, DestName(dest), SourceName(array));
        return Unit.Value;
      }

      public Unit Ldsfld(Label pc, Field field, bool @volatile, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{3} = {1}ldsfld {2}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(field), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldsflda(Label pc, Field field, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldsflda {1}", prefix, mdDecoder.FullName(field), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldtypetoken(Label pc, Type type, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldtoken {1}", prefix, mdDecoder.FullName(type), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldfieldtoken(Label pc, Field field, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldtoken {1}", prefix, mdDecoder.FullName(field), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldmethodtoken(Label pc, Method method, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldtoken {1}", prefix, mdDecoder.FullName(method), DestName(dest));
        return Unit.Value;
      }

      public Unit Ldvirtftn(Label pc, Method method, Dest dest, Source obj, TextWriter data)
      {
        data.WriteLine("{0}{2} = ldvirtftn {1} {3}", prefix, mdDecoder.FullName(method), DestName(dest), SourceName(obj));
        return Unit.Value;
      }

      public Unit Mkrefany(Label pc, Type type, Dest dest, Source value, TextWriter data)
      {
        data.WriteLine("{0}{2} = mkrefany {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(value));
        return Unit.Value;
      }

      public Unit Newarray<ArgList>(Label pc, Type type, Dest dest, ArgList lengths, TextWriter data)
        where ArgList : IIndexable<Source>
      {
        data.Write("{0}{2} = newarray {1}[", prefix, mdDecoder.FullName(type), DestName(dest));
        for (int i = 0; i < lengths.Count; i++)
        {
          data.Write("{0} ", SourceName(lengths[i]));
        }
        data.WriteLine("]");
        return Unit.Value;
      }

      public Unit Newobj<ArgList>(Label pc, Method ctor, Dest dest, ArgList args, TextWriter data)
        where ArgList : IIndexable<Source>
      {
        data.Write("{0}{2} = newobj {1}(", prefix, mdDecoder.FullName(ctor), DestName(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            data.Write("{0} ", SourceName(args[i]));
          }
        }
        data.WriteLine(")");
        return Unit.Value;
      }

      public Unit Refanytype(Label pc, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{1} = refanytype {2}", prefix, DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Refanyval(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = refanyval {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Rethrow(Label pc, TextWriter data)
      {
        data.WriteLine("{0}rethrow", prefix);
        return Unit.Value;
      }

      public Unit Sizeof(Label pc, Type type, Dest dest, TextWriter data)
      {
        data.WriteLine("{0}{2} = sizeof {1}", prefix, mdDecoder.FullName(type), DestName(dest));
        return Unit.Value;
      }

      public Unit Stelem(Label pc, Type type, Source array, Source index, Source value, TextWriter data)
      {
        data.WriteLine("{0}stelem {1} {2}[{3}] = {4}", prefix, mdDecoder.FullName(type), SourceName(array), SourceName(index), SourceName(value));
        return Unit.Value;
      }

      public Unit Stfld(Label pc, Field field, bool @volatile, Source ptr, Source value, TextWriter data)
      {
        data.WriteLine("{0}{1}stfld {2} {3} {4}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(field), SourceName(ptr), SourceName(value));
        return Unit.Value;
      }

      public Unit Stsfld(Label pc, Field field, bool @volatile, Source value, TextWriter data)
      {
        data.WriteLine("{0}{1}stsfld {2} {3}", prefix, @volatile ? "volatile." : null, mdDecoder.FullName(field), SourceName(value));
        return Unit.Value;
      }

      public Unit Throw(Label pc, Source value, TextWriter data)
      {
        data.WriteLine("{0}throw {1}", prefix, SourceName(value));
        return Unit.Value;
      }

      public Unit Unbox(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = unbox {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      public Unit Unboxany(Label pc, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{2} = unbox_any {1} {3}", prefix, mdDecoder.FullName(type), DestName(dest), SourceName(source));
        return Unit.Value;
      }

      #endregion

      #region IVisitSynthIL<Label,Method,Source,Dest,TextWriter,Unit> Members


      public Unit BeginOld(Label pc, Label matchingEnd, TextWriter data)
      {
        data.WriteLine("{0}begin.old", prefix);
        return Unit.Value;
      }

      public Unit EndOld(Label pc, Label matchingBegin, Type type, Dest dest, Source source, TextWriter data)
      {
        data.WriteLine("{0}{1} = end.old {2}", prefix, DestName(dest), SourceName(source));
        return Unit.Value;
      }

      #endregion
    }

  }

}