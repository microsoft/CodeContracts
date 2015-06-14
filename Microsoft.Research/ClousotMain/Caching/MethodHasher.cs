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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Source = Int32;
  using Dest = Int32;

  /// <summary>
  /// Use one instance for each method.
  /// </summary>
  public class MethodHasher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Context, Expression, SymbolicValue> : IDisposable
    where Type : IEquatable<Type>
  {
    // Already hashed Subroutines 
    private readonly Set<Subroutine> hashedSubroutines = new Set<Subroutine>();

    // method-unique id for a subroutine
    private readonly Func<Subroutine, int> subroutineIdentifier;

    // Already hashed types
    private readonly Set<Type> referencedTypes = new Set<Type>();

    // method-unique id for a type
    private readonly Func<Type, int> typeIdentifier;
    
    // Types to be hashed
    private readonly Queue<Type> typesQueue = new Queue<Type>();

    // Subroutines to be hashed (method subroutine + for ForAll/Exists calls the delegate subroutines)
    private readonly Queue<Subroutine> subroutineQueue = new Queue<Subroutine>();

    // Compute hash for IL
    private readonly ILHasher ilHasher;

    // TextWriter that pipes a StreamWriter and a CryptoStream with an HashAlgorithm
    private readonly HashWriter tw;
   
    private readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver;
    private readonly AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> driver;

    public MethodHasher(
      AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> driver,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver,
      bool trace,
      Func<Subroutine, int> subroutineIdentifier = null,
      Func<Type, int> typeIdentifier = null
      )
    {
      this.driver = driver;
      this.mDriver = mDriver;

      this.tw = new HashWriter(trace);

      this.ilHasher = new ILHasher(this.mDriver.StackLayer, this.tw, this.ReferenceType, this.InlinedSubroutine);

      if (subroutineIdentifier != null)
        this.subroutineIdentifier = subroutineIdentifier;
      else
      {
        // unique ID for a subroutine (used to put warnings into contexts)
        var subroutineLocalIds = new BijectiveMap<Subroutine, int>();

        this.subroutineIdentifier = sub =>
        {
          int res;
          if (!subroutineLocalIds.TryGetValue(sub, out res))
            subroutineLocalIds.Add(sub, res = subroutineLocalIds.Count);
          return res;
        };
      }

      if (typeIdentifier != null)
        this.typeIdentifier = typeIdentifier;
      else
      {
        // unique ID for types (used for general serialization of boxed expressions)
        var referencedTypesLocalIds = new BijectiveMap<Type, int>();

        this.typeIdentifier = typ =>
        {
          int res;
          if (!referencedTypesLocalIds.TryGetValue(typ, out res))
            referencedTypesLocalIds.Add(typ, res = referencedTypesLocalIds.Count);
          return res;
        };
      }
    }

    /// <summary>
    /// Entry point for the method hashing
    /// </summary>
    public byte[] GetHash(byte[] AdditionalOptionsToHash)
    {
      var md = this.mDriver.MetaDataDecoder;
      var currMethod = this.mDriver.CurrentMethod;

      // Hashing the options
      this.tw.WriteLine(Convert.ToBase64String(AdditionalOptionsToHash));

      #region Hashing the method signature
      this.tw.WriteLine("{0} {1} {2} {3} {4} returnType:{5} {6}",
        md.IsStatic(currMethod) ? "static" : "",
        md.IsPropertyGetter(currMethod) ? "getter" : "",
        md.IsPropertySetter(currMethod) ? "setter" : "",
        md.IsCompilerGenerated(currMethod) ? "compilerGenerated" : "",
        md.IsVirtual(currMethod) ? "virtual" : "",
        this.ReferenceType(md.ReturnType(currMethod)),
        md.FullName(currMethod));

      this.tw.WriteLine("Is externally visible = {0}", md.IsVisibleOutsideAssembly(currMethod));

      this.tw.Write("Params : ");
      foreach (var param in md.Parameters(this.mDriver.CurrentMethod).Enumerate())
        this.tw.Write("{0} type:{1}\t", md.Name(param), this.ReferenceType(md.ParameterType(param)));
      this.tw.WriteLine();

      this.tw.Write("Locals : ");
      foreach (var local in md.Locals(this.mDriver.CurrentMethod).Enumerate())
        this.tw.Write("{0} type:{1}\t", md.Name(local), this.ReferenceType(md.LocalType(local)));
      this.tw.WriteLine();

      this.tw.Write("ImplementsOrOverrides : ");
      foreach (var method in md.OverriddenAndImplementedMethods(this.mDriver.CurrentMethod))
        this.tw.Write("{0}\t", md.FullName(method));
      this.tw.WriteLine();
      #endregion

      // Hashing the main subroutine and all the others
      this.subroutineQueue.Enqueue(this.mDriver.CFG.Subroutine);
      while (this.subroutineQueue.Count > 0)
      {
        this.HashSubroutine(this.subroutineQueue.Dequeue());
      }

      // Hashing the types
      while (this.typesQueue.Count > 0)
        this.HashTypeReference(this.typesQueue.Dequeue());

      return this.tw.GetHash();
    }

    private int ReferenceType(Type type)
    {
      if (this.referencedTypes.Add(type))
        this.typesQueue.Enqueue(type);
      return this.typeIdentifier(type);
    }

    private int InlinedSubroutine(Method method)
    {
      // specialized methods have no body
      method = this.driver.MetaDataDecoder.Unspecialized(method);
      if (this.driver.MetaDataDecoder.HasBody(method))
      {
        this.subroutineQueue.Enqueue(this.driver.MethodCache.GetCFG(method).Subroutine);
      }
      return this.subroutineQueue.Count;
    }

    private void HashTypeReference(Type type)
    {
      // TODO : 
      // md.GetAttributes ?
      // md.IsModified ?

      var md = this.mDriver.MetaDataDecoder;
      this.tw.Write("Type{0} : {1}", this.typeIdentifier(type), md.FullName(type));

      IIndexable<Type> @params;

      bool formal = md.IsFormalTypeParameter(type) || md.IsMethodFormalTypeParameter(type);
      if(formal)
      {
        if(md.IsMethodFormalTypeParameter(type)) this.tw.Write(" methodFormal{0}", md.MethodFormalTypeParameterIndex(type));
        if(md.IsFormalTypeParameter(type))       this.tw.Write(" typeFormal{0}", md.NormalizedFormalTypeParameterIndex(type));
        if(md.IsConstructorConstrained(type))    this.tw.Write(" constructorConstrained");
        if(md.IsValueConstrained(type))          this.tw.Write(" valueConstrained");
        if(md.IsReferenceConstrained(type))      this.tw.Write(" refConstrained");
        foreach(var c in md.TypeParameterConstraints(type))
          this.tw.Write(" constraint:{0}", this.ReferenceType(c));
      } else if(md.IsGeneric(type, out @params, true)) {
        this.tw.Write(" generic :");
        for(int i = 0; i < @params.Count; i++)
          this.tw.Write(" {0}", this.ReferenceType(@params[i]));
      } else if(md.IsSpecialized(type, out @params)) {
        this.tw.Write(" specialized :");
        for(int i = 0; i < @params.Count; i++)
          this.tw.Write(" {0}", this.ReferenceType(@params[i]));
      } else if(md.IsArray(type)) {
        this.tw.Write(" array : {0} {1}", md.Rank(type), this.ReferenceType(md.ElementType(type)));
      } else if(md.IsUnmanagedPointer(type)) {
        this.tw.Write(" unmanaged pointer : {0}", this.ReferenceType(md.ElementType(type)));
      } else if(md.IsManagedPointer(type)) {
        this.tw.Write(" managed pointer : {0}", this.ReferenceType(md.ElementType(type)));
      } else if(md.IsEnum(type)) {
        this.tw.Write(" enum : {0}", md.TypeEnum(type));
        List<int> enumValues;
        if(md.TryGetEnumValues(type, out enumValues))
        {
          this.tw.Write(" enum values: {0}", string.Join(",", enumValues));
        }
      } else if(md.IsDelegate(type)) {
        this.tw.Write(" delegate"); // Maybe we should hash a bit more
      } else if(md.IsInterface(type) || md.IsClass(type) || md.IsStruct(type)) {
        this.tw.Write(md.IsInterface(type) ? " interface" : md.IsStruct(type) ? " struct" : " class");
        this.tw.Write(" implements :");
        foreach(var i in md.Interfaces(type))
          this.tw.Write(" {0}", this.ReferenceType(i));
        if(md.IsClass(type) && md.HasBaseClass(type))
          this.tw.Write(" inherits : {0}", this.ReferenceType(md.BaseClass(type)));
      }

      if(md.IsCompilerGenerated(type))
        this.tw.Write(" compilerGenerated");

      if(md.IsPrimitive(type))
        this.tw.Write(" primitive");

      if(md.IsReferenceType(type))
        this.tw.Write(" referenceType");

      this.tw.Write(" typeSize:{0}", md.TypeSize(type));
      this.tw.WriteLine();
    }

    private void HashSubroutine(Subroutine subroutine)
    {
      var ilDecoder = this.mDriver.StackLayer.Decoder;
      if (hashedSubroutines.Contains(subroutine))
        return;
      
      this.hashedSubroutines.Add(subroutine);
      
      // Hash the name 
      this.tw.WriteLine("SUBROUTINE{0} {1}", this.subroutineIdentifier(subroutine), subroutine.Kind);

      // Hash all the blocks
      foreach (var block in subroutine.Blocks)
      {
        this.tw.WriteLine("Block {0}", block.Index);
        this.tw.WriteLine("  Handlers: ");
        
        // TODO : handlers - we ignore them now, as in Clousot's analysis we do it too!
        this.tw.WriteLine("  Code:");
        foreach (var apc in block.APCs())
        {
          ilDecoder.ForwardDecode<Unit, Unit, ILHasher>(apc, this.ilHasher, Unit.Value);
        }

        var subroutinesToVisit = new Set<Subroutine>(subroutine.UsedSubroutines());
        var stackCFG = this.mDriver.StackLayer.Decoder.Context.MethodContext.CFG;

        this.tw.WriteLine("  Successors:");
        foreach (var taggedSucc in subroutine.SuccessorEdgesFor(block))
        {
          this.tw.Write("({0},{1}", taggedSucc.One, taggedSucc.Two.Index);

          // The order of successors edges is not deterministic (why?), hence the OrderBy
          var edgesubs = stackCFG.GetOrdinaryEdgeSubroutines(block, taggedSucc.Two, null).GetEnumerable()
            .Select(edgesub => new Tuple<string, Subroutine>(String.Format(" SUBROUTINE{0}({1})", this.subroutineIdentifier(edgesub.Two), edgesub.One), edgesub.Two))
            .OrderBy(x => x.Item1);
                   
          foreach (var edgesub in edgesubs)
          {
            this.tw.Write(edgesub.Item1);
            subroutinesToVisit.Add(edgesub.Item2);
          }
          this.tw.Write(") ");
        }
        this.tw.WriteLine();

        // Go recursively
        //foreach (var usedSubroutine in subroutine.UsedSubroutines().OrderBy(s => this.subroutineIdentifier(s)))
        foreach (var usedSubroutine in subroutinesToVisit.OrderBy(s => this.subroutineIdentifier(s)))
          HashSubroutine(usedSubroutine);
      }
    }

    private class ILHasher : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Dest, Unit, Unit>
    {
      private readonly StreamWriter tw;
      private readonly ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Source, Dest, IStackContext<Field, Method>, Unit> codeLayer;
      private readonly Func<Type, int> typeReferencer;
      private readonly Func<Method, int> inlineMethod;
      private Converter<Source, string> source2String { get { return this.codeLayer.ExpressionToString; } }
      private Converter<Dest, string> dest2String { get { return this.codeLayer.VariableToString; } }
      private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder { get { return this.codeLayer.MetaDataDecoder; } }
      private IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder { get { return this.codeLayer.ContractDecoder; } }


      public ILHasher(
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Source, Dest, IStackContext<Field, Method>, Unit> codeLayer,
        StreamWriter tw,
        Func<Type, int> typeReferencer,
        Func<Method,int> inlineMethod
        )
      {
        this.codeLayer = codeLayer;
        this.tw = tw;
        this.typeReferencer = typeReferencer;
        this.inlineMethod = inlineMethod;
      }

      #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Source,Dest,Unit,Unit> Members

      public Unit Arglist(APC pc, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {0} = arglist", this.dest2String(dest));
        return Unit.Value;
      }

      // Should not be used in the stack based CFG.
      public Unit BranchCond(APC pc, APC target, BranchOperator bop, Source value1, Source value2, Unit data)
      {
        throw new NotImplementedException();
      }

      // Should not be used in the stack based CFG.
      public Unit BranchTrue(APC pc, APC target, Source cond, Unit data)
      {
        throw new NotImplementedException();
      }

      // Should not be used in the stack based CFG.
      public Unit BranchFalse(APC pc, APC target, Source cond, Unit data)
      {
        throw new NotImplementedException();
      }

      // Should not be used in the stack based CFG.
      public Unit Branch(APC pc, APC target, bool leave, Unit data)
      {
        throw new NotImplementedException();
      }

      // Should not be used in the stack based CFG.
      public Unit Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Source value, Unit data)
      {
        throw new NotImplementedException();
      }

      public Unit Break(APC pc, Unit data)
      {
        this.tw.WriteLine("    break");
        return Unit.Value;
      }

      public Unit Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Unit data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        // The purity attribute may have changed, therefore we hash it
        var isPure = this.contractDecoder.IsPure(method);
        var fullname = this.mdDecoder.FullName(method);

        this.tw.Write(isPure.ToString());
        // We need to hash the names of the methods because some built-in methods have special treatments
        this.tw.Write("    {3} = {0}call{2} {1}(", tail ? "tail." : null, fullname, virt ? "virt" : null, this.dest2String(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            this.tw.Write("{0} ", this.source2String(args[i]));
            if (this.mdDecoder.IsPure(method, i))
            {
              this.tw.Write(i.ToString());
            }
          }
          // if we call Contract.{ForAll, Exists} with a delegate that we decompile, we need to hash the delegate's il too
          var mName = mdDecoder.Name(method);
          if (isPure &&  ((mName == "ForAll") || (mName == "Exists")))
          {
            // last argument is closure
            Method quantifierClosure;
            if (this.codeLayer.Decoder.Context.StackContext.TryGetCallArgumentDelegateTarget(pc, args[args.Count - 1], out quantifierClosure))
            {
              this.tw.Write("QuantifierClosure{0}", this.inlineMethod(quantifierClosure).ToString());
            }
          }
        }
        this.tw.Write(")");
        if (extraVarargs != null)
        {
          this.tw.Write(" extra varargs types : ");
          for (int i = 0; i < extraVarargs.Count; i++)
            this.tw.Write("{0} ", this.typeReferencer(extraVarargs[i]));
        }

        this.tw.Write(" resultType : {0}", this.typeReferencer(this.mdDecoder.ReturnType(method)));

        this.tw.WriteLine();
        return Unit.Value;
      }

      public Unit ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, Unit data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        // The purity attribute may have changed, therefore we hash it
        var isPure = this.contractDecoder.IsPure(method);
        this.tw.Write(isPure.ToString());

        // We need to hash the names of the methods because some built-in methods have special treatments
        this.tw.Write("    {3} = {0}constrained({1}).callvirt {2}(", tail ? "tail." : null, this.typeReferencer(constraint), this.mdDecoder.FullName(method), this.dest2String(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            this.tw.Write("{0} ", this.source2String(args[i]));
            if (this.mdDecoder.IsPure(method, i))
            {
              this.tw.Write(i.ToString());
            }
          }
        }
        this.tw.Write(")");
        if (extraVarargs != null)
        {
          this.tw.Write(" extra varargs types : ");
          for (int i = 0; i < extraVarargs.Count; i++)
            this.tw.Write("{0} ", this.typeReferencer(extraVarargs[i]));
        }

        this.tw.Write(" resultType : {0}", this.typeReferencer(this.mdDecoder.ReturnType(method)));

        this.tw.WriteLine();
        return Unit.Value;
      }

      public Unit Jmp(APC pc, Method method, Unit data)
      {
        this.tw.WriteLine("    jmp {0}", this.mdDecoder.FullName(method));
        return Unit.Value;
      }

      public Unit Ldftn(APC pc, Method method, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {0} = ldftn {1}", this.dest2String(dest), this.mdDecoder.FullName(method));
        return Unit.Value;
      }

      public Unit Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, Unit data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        // We need to hash the names of the methods because some built-in methods have special treatments
        this.tw.Write("    {1} = {0}calli {2}(", tail ? "tail." : null, this.dest2String(dest), this.source2String(fp));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            this.tw.Write("{0} ", this.source2String(args[i]));
          }
        }
        this.tw.Write(")");
        this.tw.Write(" args types : ");
        for (int i = 0; i < argTypes.Count; i++)
          this.tw.Write("{0} ", this.typeReferencer(argTypes[i]));
        this.tw.WriteLine(" return type : {0}", this.typeReferencer(returnType));
        return Unit.Value;
      }

      public Unit Ckfinite(APC pc, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {0} = ckfinite {1}", this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      public Unit Cpblk(APC pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Unit data)
      {
        this.tw.WriteLine("    {0}cpblk {1} {2} {3}", @volatile ? "volatile." : null, this.source2String(destaddr), this.source2String(srcaddr), this.source2String(len));
        return Unit.Value;
      }

      public Unit Endfilter(APC pc, Source decision, Unit data)
      {
        this.tw.WriteLine("    endfilter {0}", this.source2String(decision));
        return Unit.Value;
      }

      public Unit Endfinally(APC pc, Unit data)
      {
        this.tw.WriteLine("    endfinally");
        return Unit.Value;
      }

      public Unit Initblk(APC pc, bool @volatile, Source destaddr, Source value, Source len, Unit data)
      {
        this.tw.WriteLine("    {0}initblk {1} {2} {3}", @volatile ? "volatile." : null, this.source2String(destaddr), this.source2String(value), this.source2String(len));
        return Unit.Value;
      }

      public Unit Ldarg(APC pc, Parameter argument, bool isOld, Dest dest, Unit data)
      {
        string isOldPrefix = isOld ? "old." : null;
        this.tw.WriteLine("    {1} = {2}ldarg {0}", this.mdDecoder.Name(argument), this.dest2String(dest), isOldPrefix);
        return Unit.Value;
      }

      public Unit Ldarga(APC pc, Parameter argument, bool isOld, Dest dest, Unit data)
      {
        string isOldPrefix = isOld ? "old." : null;
        this.tw.WriteLine("    {1} = {2}ldarga {0}", this.mdDecoder.Name(argument), this.dest2String(dest), isOldPrefix);
        return Unit.Value;
      }

      public Unit Ldind(APC pc, Type type, bool @volatile, Dest dest, Source ptr, Unit data)
      {
        this.tw.WriteLine("    {2} = {0}ldind {1} {3}", @volatile ? "volatile." : null, this.typeReferencer(type), this.dest2String(dest), this.source2String(ptr));
        return Unit.Value;
      }

      public Unit Ldloc(APC pc, Local local, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldloc {0}", this.mdDecoder.Name(local), this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Ldloca(APC pc, Local local, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldloca {0}", this.mdDecoder.Name(local), this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Localloc(APC pc, Dest dest, Source size, Unit data)
      {
        this.tw.WriteLine("    {0} = localloc {1}", this.dest2String(dest), this.source2String(size));
        return Unit.Value;
      }

      public Unit Nop(APC pc, Unit data)
      {
        this.tw.WriteLine("    nop");
        return Unit.Value;
      }

      public Unit Pop(APC pc, Source source, Unit data)
      {
        this.tw.WriteLine("    pop {0}", this.source2String(source));
        return Unit.Value;
      }

      public Unit Return(APC pc, Source source, Unit data)
      {
        this.tw.WriteLine("    ret {0}", this.source2String(source));
        return Unit.Value;
      }

      public Unit Starg(APC pc, Parameter argument, Source source, Unit data)
      {
        this.tw.WriteLine("    starg {0} {1}", this.mdDecoder.Name(argument), this.source2String(source));
        return Unit.Value;
      }

      public Unit Stind(APC pc, Type type, bool @volatile, Source ptr, Source value, Unit data)
      {
        this.tw.WriteLine("    {0}stind {1} {2} {3}", @volatile ? "volatile." : null, this.typeReferencer(type), this.source2String(ptr), this.source2String(value));
        return Unit.Value;
      }

      public Unit Stloc(APC pc, Local local, Source source, Unit data)
      {
        this.tw.WriteLine("    stloc {0} {1}", this.mdDecoder.Name(local), this.source2String(source));
        return Unit.Value;
      }

      public Unit Box(APC pc, Type type, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {1} = box {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      public Unit Castclass(APC pc, Type type, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = castclass {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      public Unit Cpobj(APC pc, Type type, Source destptr, Source srcptr, Unit data)
      {
        this.tw.WriteLine("    cpobj {0} {1} {2}", this.typeReferencer(type), this.source2String(destptr), this.source2String(srcptr));
        return Unit.Value;
      }

      public Unit Initobj(APC pc, Type type, Source ptr, Unit data)
      {
        this.tw.WriteLine("    initobj {0} {1}", this.typeReferencer(type), this.source2String(ptr));
        return Unit.Value;
      }

      public Unit Ldelem(APC pc, Type type, Dest dest, Source array, Source index, Unit data)
      {
        this.tw.WriteLine("    {1} = ldelem {0} {2}[{3}]", this.typeReferencer(type), this.dest2String(dest), this.source2String(array), this.source2String(index));
        return Unit.Value;
      }

      public Unit Ldelema(APC pc, Type type, bool @readonly, Dest dest, Source array, Source index, Unit data)
      {
        this.tw.WriteLine("    {2} = {0}ldelema {1} {3}[{4}]", @readonly ? "readonly." : null, this.typeReferencer(type), this.dest2String(dest), this.source2String(array), this.source2String(index));
        return Unit.Value;
      }

      public Unit Ldfld(APC pc, Field field, bool @volatile, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {2} = {0}ldfld {1} {3} type:{4}", @volatile ? "volatile." : null, GetStringForField(field), this.dest2String(dest), this.source2String(obj), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Ldflda(APC pc, Field field, Dest dest, Source obj, Unit data)
      {

        this.tw.WriteLine("    {1} = ldflda {0} {2} type:{3}", GetStringForField(field), this.dest2String(dest), this.source2String(obj), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Ldlen(APC pc, Dest dest, Source array, Unit data)
      {
        this.tw.WriteLine("    {0} = ldlen {1}", this.dest2String(dest), this.source2String(array));
        return Unit.Value;
      }

      public Unit Ldsfld(APC pc, Field field, bool @volatile, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {2} = {0}ldsfld {1} type:{3}", @volatile ? "volatile." : null, GetStringForField(field), this.dest2String(dest), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Ldsflda(APC pc, Field field, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldsflda {0} type:{2}", GetStringForField(field), this.dest2String(dest), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Ldtypetoken(APC pc, Type type, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldtoken {0}", this.typeReferencer(type), this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Ldfieldtoken(APC pc, Field field, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldtoken {0}", GetStringForField(field), this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Ldmethodtoken(APC pc, Method method, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldtoken {0}", this.mdDecoder.FullName(method), this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Ldvirtftn(APC pc, Method method, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = ldvirtftn {0} {2}", this.mdDecoder.FullName(method), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      public Unit Mkrefany(APC pc, Type type, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = mkrefany {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      public Unit Newarray<ArgList>(APC pc, Type type, Dest dest, ArgList len, Unit data)
        where ArgList : IIndexable<Source>
      {
        this.tw.Write("    {1} = newarray {0}[", this.typeReferencer(type), this.dest2String(dest));
        for (int i = 0; i < len.Count; i++)
        {
          this.tw.Write("{0} ", this.source2String(len[i]));
        }
        this.tw.WriteLine(")");
        return Unit.Value;
      }

      public Unit Newobj<ArgList>(APC pc, Method ctor, Dest dest, ArgList args, Unit data) where ArgList : IIndexable<Source>
      {
        this.tw.Write("    {1} = newobj {0}(", this.mdDecoder.FullName(ctor), this.dest2String(dest));
        if (args != null)
        {
          for (int i = 0; i < args.Count; i++)
          {
            this.tw.Write("{0} ", this.source2String(args[i]));
          }
        }
        this.tw.WriteLine(") type: {0}", this.typeReferencer(this.mdDecoder.DeclaringType(ctor)));
        return Unit.Value;
      }

      public Unit Refanytype(APC pc, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {0} = refanytype {1}", this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      public Unit Refanyval(APC pc, Type type, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {1} = refanyval {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      public Unit Rethrow(APC pc, Unit data)
      {
        this.tw.WriteLine("    rethrow");
        return Unit.Value;
      }

      public Unit Stelem(APC pc, Type type, Source array, Source index, Source value, Unit data)
      {
        this.tw.WriteLine("    stelem {0} {1}[{2}] = {3}", this.typeReferencer(type), this.source2String(array), this.source2String(index), this.source2String(value));
        return Unit.Value;
      }

      public Unit Stfld(APC pc, Field field, bool @volatile, Source obj, Source value, Unit data)
      {
        this.tw.WriteLine("    {0}stfld {1} {2} {3} type:{4}", @volatile ? "volatile." : null, GetStringForField(field), this.source2String(obj), this.source2String(value), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Stsfld(APC pc, Field field, bool @volatile, Source value, Unit data)
      {
        this.tw.WriteLine("    {0}stsfld {1} {2} type:{3}", @volatile ? "volatile." : null, GetStringForField(field), this.source2String(value), this.typeReferencer(this.mdDecoder.FieldType(field)));
        return Unit.Value;
      }

      public Unit Throw(APC pc, Source exn, Unit data)
      {
        this.tw.WriteLine("    throw {0}", this.source2String(exn));
        return Unit.Value;
      }

      public Unit Unbox(APC pc, Type type, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = unbox {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      public Unit Unboxany(APC pc, Type type, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = unbox_any {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      #endregion

      #region IVisitSynthIL<Label,Method,Type,Source,Dest,Unit,Unit> Members

      public Unit Entry(APC pc, Method method, Unit data)
      {
        this.tw.WriteLine("    method_entry {0}", this.mdDecoder.FullName(method));

        if (this.contractDecoder.IsPure(method))
        {
          this.tw.WriteLine("pure");
        }
        
        var md =this.mdDecoder; 
        var pos = this.mdDecoder.IsStatic(method) ? 0 : 1;
        foreach (var p in this.mdDecoder.Parameters(method).Enumerate())
        {
          if (md.IsPure(method, pos++))
          {
            this.tw.WriteLine(pos.ToString());
          }

          this.tw.WriteLine(GetEnumValuesIfAny(md.ParameterType(p)));
        }

        return Unit.Value;
      }

      public Unit Assume(APC pc, string tag, Source condition, object provenance, Unit data)
      {
        // TODO: do we need to hash the provenance ?
        this.tw.WriteLine("    assume({0}) {1}", tag, this.source2String(condition));
        return Unit.Value;
      }

      public Unit Assert(APC pc, string tag, Source condition, object provenance, Unit data)
      {
        // TODO: do we need to hash the provenance ?
        this.tw.WriteLine("    assert({0}) {1}", tag, this.source2String(condition));
        return Unit.Value;
      }

      public Unit Ldstack(APC pc, int offset, Dest dest, Source source, bool isOld, Unit data)
      {
        this.tw.WriteLine("    {0} = {3}ldstack.{1} {2}", this.dest2String(dest), offset, this.source2String(source), isOld ? "old." : "");
        return Unit.Value;
      }

      public Unit Ldstacka(APC pc, int offset, Dest dest, Source source, Type origParamType, bool isOld, Unit data)
      {
        this.tw.WriteLine("    {0} = {3}ldstacka.{1} {2}", this.dest2String(dest), offset, this.source2String(source), isOld ? "old." : "");
        return Unit.Value;
      }

      public Unit Ldresult(APC pc, Type type, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {0} = ldresult {1}", this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      public Unit BeginOld(APC pc, APC matchingEnd, Unit data)
      {
        this.tw.WriteLine("    begin.old");
        return Unit.Value;
      }

      public Unit EndOld(APC pc, APC matchingBegin, Type type, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {0} = end.old {1} {2}", this.dest2String(dest), this.source2String(source), this.typeReferencer(type));
        return Unit.Value;
      }

      #endregion

      #region IVisitExprIL<Label,Type,Source,Dest,Unit,Unit> Members

      public Unit Binary(APC pc, BinaryOperator op, Dest dest, Source s1, Source s2, Unit data)
      {
        this.tw.WriteLine("    {0} = {1} {2} {3}", this.dest2String(dest), this.source2String(s1), op.ToString(), this.source2String(s2));
        return Unit.Value;
      }

      public Unit Isinst(APC pc, Type type, Dest dest, Source obj, Unit data)
      {
        this.tw.WriteLine("    {1} = isinst {0} {2}", this.typeReferencer(type), this.dest2String(dest), this.source2String(obj));
        return Unit.Value;
      }

      public Unit Ldconst(APC pc, object constant, Type type, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {1} = ldc ({2})'{0}'", constant.ToString(), this.dest2String(dest), this.typeReferencer(type));
        return Unit.Value;
      }

      public Unit Ldnull(APC pc, Dest dest, Unit data)
      {
        this.tw.WriteLine("    {0} = ldnull", this.dest2String(dest));
        return Unit.Value;
      }

      public Unit Sizeof(APC pc, Type type, Dest dest, Unit data)
      {
        int actualSize = mdDecoder.TypeSize(type);
        this.tw.WriteLine("    {1} = sizeof {0} ({2})", this.typeReferencer(type), this.dest2String(dest), actualSize);
        return Unit.Value;
      }

      public Unit Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Source source, Unit data)
      {
        this.tw.WriteLine("    {3} = {2}{0}{1} {4}", overflow ? "_ovf" : null, unsigned ? "_un" : null, op.ToString(), this.dest2String(dest), this.source2String(source));
        return Unit.Value;
      }

      #endregion

      #region Helpers

      private string GetEnumValuesIfAny(Type enumType)
      {
        List<int> enumValues;
        if (this.mdDecoder.IsEnumWithoutFlagAttribute(enumType) && this.mdDecoder.TryGetEnumValues(enumType, out enumValues))
        {
          return string.Join(",", enumValues);
        }

        return String.Empty;
      }

      private string GetStringForField(Field field)
      {
        var isReadonly = mdDecoder.IsReadonly(field) ? "(readonly)" : null;
        return mdDecoder.FullName(field) + isReadonly;
      }

      #endregion
    }

    #region IDisposable Members

    public void Dispose()
    {
      this.tw.Dispose();
    }

    #endregion
  }
}
