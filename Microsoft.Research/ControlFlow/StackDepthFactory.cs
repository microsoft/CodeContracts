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

using StackTemp = System.Int32;

namespace Microsoft.Research.CodeAnalysis
{
  using SubroutineContext = FList<STuple<CFGBlock, CFGBlock, string>>;
  using SubroutineEdge = STuple<CFGBlock, CFGBlock, string>;
  using System.Diagnostics.Contracts;

  public interface IStackContext<Field, Method> : IMethodContext<Field, Method>
  {
    IStackContextData<Field, Method> StackContext { get; }
  }

  public interface IStackContextData<Field, Method>
  {
    /// <summary>
    /// Returns the evaluation stack depth prior to the given program point
    /// </summary>
    int StackDepth(APC at);

    /// <summary>
    /// Returns the evaluation stack depth prior to the given program point ignoring the context
    /// </summary>
    int LocalStackDepth(APC at);

    /// <summary>
    /// If APC is a call site, and some of the call arguments are delegates with known method targets,
    /// then this method gives access to these method targets.
    /// </summary>
    bool TryGetCallArgumentDelegateTarget(APC at, int stackOffset, out Method method);
  }

  public static class MetadataExtensions
  {
    public static Method FindBaseOrImplementingMethod<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      Method called,
      Method targetMethod)
    {
      Contract.Requires(mdDecoder != null);// F: Added as of Clousot suggestion

      var calledType = mdDecoder.DeclaringType(called);
      var targetMethodType = mdDecoder.DeclaringType(targetMethod);

      // no instance (same generic context)
      if (mdDecoder.Equal(calledType, targetMethodType)) return called;

      // direct instance
      if (mdDecoder.Equal(mdDecoder.Unspecialized(calledType), targetMethodType))
      {
        return called;
      }

      // indirect instance because contract is inherited
      // find out what method called is implementing
      foreach (var candidate in mdDecoder.ImplementedMethods(called).AssumeNotNull())
      {
        var candidateType = mdDecoder.DeclaringType(candidate);
        if (mdDecoder.Equal(mdDecoder.Unspecialized(candidateType), targetMethodType))
        {
          return candidate;
        }
      }

      FList<Method> baseMethodsToCheck = FList<Method>.Cons(called, null);
      while (baseMethodsToCheck != null)
      {
        var baseMethod = baseMethodsToCheck.Head;
        baseMethodsToCheck = baseMethodsToCheck.Tail;

        foreach (var candidate in mdDecoder.OverriddenMethods(baseMethod).AssumeNotNull())
        {
          baseMethodsToCheck = baseMethodsToCheck.Cons(candidate);
          var candidateType = mdDecoder.DeclaringType(candidate);
          if (mdDecoder.Equal(mdDecoder.Unspecialized(candidateType), targetMethodType))
          {
            return candidate;
          }
        }
      }
      // couldn't find the instantiation
      return called;
    }

  }

  public static class StackDepthFactory
  {
    public static IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>
      Create<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Context>(
        IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, Context, Unit> decoder,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
      )
      where Type : IEquatable<Type>
      where Context : IMethodContext<Field, Method>
    {
      Contract.Requires(decoder != null);// F: Suggested by Clousot
      Contract.Requires(mdDecoder != null); // F: Suggested by Clousot

      return new StackDepthProvider<Local,Parameter,Method,Field,Property,Event,Type,Context,Attribute,Assembly>(decoder, mdDecoder);
    }
  }

  #region Helper Structs
  struct StackInfo
  {
    /// <summary>
    /// the object pushed is either null, or "true" to indicte that the value is == "this", or
    /// it is a method token of type Method to indicate either a ldftn ldvirtfn, or result of a delegate
    /// construction from such a token.
    /// </summary>
    StackInfo<object> stack;

    public StackInfo(int depth, int capacity)
    {
      this.stack = new StackInfo<object>(depth, capacity);
    }
    private StackInfo(StackInfo<object> copy)
    {
      this.stack = copy;
    }

    public int Depth { get { return this.stack.Depth; } }
    public object this[int offset] { get { return this.stack[offset]; } }
    public StackInfo Pop(int slots) {
      Contract.Assume(slots <= this.stack.Depth); // F: If it fails something went wrong, very likely in the inference
      return new StackInfo(this.stack.Pop(slots)); }
    public StackInfo Push() { this.stack.Push(null); return this; }
    public StackInfo PushThis() { this.stack.Push(true); return this; }
    public StackInfo Push<Method>(Method method) { this.stack.Push(method); return this; }
    public void Adjust(int delta)
    {
      Contract.Requires(delta != Int32.MinValue);

      if (delta == 0) return;
      if (delta < 0)
      {
        // pop
        stack.Pop(-delta);
      }
      for (var i = 0; i < delta; i++)
      {
        this.Push();
      }
    }

    public bool IsThis(int offset)
    {
      return As<bool>(offset);
    }

    public bool TryGetTarget<TargetType>(int offset, out TargetType target)
    {
      if (this[offset] is TargetType)
      {
        target = (TargetType)this[offset];
        return true;
      }
      target = default(TargetType);
      return false;
    }

    public TargetType As<TargetType>(int offset)
    {
      if (this[offset] is TargetType)
      {
        return (TargetType)this[offset];
      }
      return default(TargetType);
    }

    public StackInfo Copy()
    {
      return new StackInfo(new StackInfo<object>(this.stack));
    }

    public override string ToString()
    {
      return stack.ToString();
    }
  }

  struct StackInfo<T>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(stack != null); // F: Added as of Clousot suggestions of preconditions
    }


    int depth; // number of elements on stack
    T[] stack;

    public int Depth
    {
      get { return this.depth; }
    }

    public StackInfo(int depth, int capacity)
    {
      this.depth = depth;
      this.stack = new T[capacity];
    }

    // Deep copy constructor
    public StackInfo(StackInfo<T> that)
    {
      this.depth = that.Depth;
      this.stack = (T[])that.stack.Clone();
    }

    public T this[int offset]
    {
      get
      {
        var top = depth - 1;
        var index = top - offset;
        if (index >= 0 && index < this.stack.Length)
        {
          return this.stack[index];
        }
        return default(T);
      }
    }

    public StackInfo<T> Pop(int slots)
    {
      Contract.Requires(slots <= this.Depth); 

      Debug.Assert(slots <= depth);
      // clear stack info
      var newDepth = depth - slots;
      for (var i = newDepth; i < depth; i++)
      {
        if (i < stack.Length)
        {
          stack[i] = default(T);
        }
      }
      depth = Math.Max(0, depth - slots);
      return this;
    }

    public void Push(T info)
    {
      var index = this.depth;
      if (index < stack.Length)
      {
        stack[index] = info;
      }
      depth++;
    }

    public override string ToString()
    {
      return this.depth.ToString();
    }
  }

  #endregion 

  /// <summary>
  /// This class computes a stack depth at each program point of a subroutine and caches the result in a
  /// typed property of the subroutine itself.
  /// It then provides a way to traverse the full control flow of a CFG and obtain absolute stack positions
  /// (temporaries) for each stack access.
  /// Note that subroutines such as pre/post conditions are inserted in various positions with possibly
  /// distinct stack depths. Thus the stack depth info per subroutine is parametric and we add the stack
  /// depth of the surrounding edge to the parameteric depth when needed.
  /// </summary>
  public class StackDepthProvider<Local, Parameter, Method, Field, Property, Event, Type, ContextData, Attribute, Assembly> :
    IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>,
    IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>,
    IStackContext<Field, Method>,
    IStackContextData<Field, Method>,
    IMethodContextData<Field, Method>,
    ICFG,
    IStackInfo
    where ContextData : IMethodContext<Field, Method>
    where Type : IEquatable<Type>
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.mdDecoder != null); // F: added because the many precondition suggestion
      Contract.Invariant(this.ilDecoder != null);// F: added because the many precondition suggestion
    }


    #region privates
    IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, ContextData, Unit> ilDecoder;
    IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    ICFG cfg { get { return this.ilDecoder.Context.MethodContext.CFG; } }

    private StackInfo ComputeBlockStartDepth(CFGBlock block)
    {
      Contract.Requires(block != null);// F: Added as of Clousot suggestion

      if (block.Subroutine.IsCatchFilterHeader(block))
      {
        // starts an exception handler, thus the exception is on the stack
        return new StackInfo(1, 2);
      }
      return new StackInfo(0, 4);
    }

    private const string StackDepthKey = "stackDepthKey";
    private const string StackTypesKey = "stackTypesKey";

    private APCMap<int> stackDepthMirrorForEndOld;

    private ILPrinter<APC> printer;

    /// <summary>
    /// Avoid specialization during stack computation itself.
    /// </summary>
    bool recursionGuard = false;

    private APCMap<int> ComputeStackDepth(Subroutine subroutine)
    {
      Contract.Requires(subroutine != null);

      Contract.Assume(subroutine.Blocks != null, "should be a postcondition");
      Contract.Assert(subroutine.BlockCount >= 0);

      ArrayMap<StackInfo> startDepth = new ArrayMap<StackInfo>(subroutine.BlockCount);
      // subroutine specific map
      APCMap<int> stackDepth = stackDepthMirrorForEndOld = new APCMap<int>(subroutine);

      StackInfo currentDepth;

      foreach (CFGBlock block in subroutine.Blocks)
      {
        if (!startDepth.TryGetValue(block.Index, out currentDepth))
        {
          currentDepth = ComputeBlockStartDepth(block);
        }
        foreach (APC lab in block.APCs())
        {
          if (this.debugStack)
          {
            Console.WriteLine("Depth before {0}", currentDepth);
            this.printer(lab, "  ", Console.Out);
          }
          stackDepth.Add(lab, currentDepth.Depth); // If you get an exception here, it means the underlying labels are not unique in a method.
          currentDepth = this.ilDecoder.ForwardDecode<StackInfo, StackInfo, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>>(lab, this, currentDepth);
          if (this.debugStack)
          {
            Console.WriteLine("Stack depth after {0}", currentDepth);
          }
        }
        // also record last apc (past last instruction in block), unless present
        if (!stackDepth.ContainsKey(block.Last))
        {
          stackDepth.Add(block.Last, currentDepth.Depth);
        }

        foreach (CFGBlock succ in subroutine.SuccessorBlocks(block))
        {
          // consider stack depth changes done by subroutines
          bool isExn;
          bool savedRecursionGuard = this.recursionGuard;
          this.recursionGuard = true;
          try
          {
            foreach (var sub in subroutine.EdgeSubroutinesOuterToInner(block, succ, out isExn, null).GetEnumerable())
            {
              currentDepth.Adjust(sub.Two.StackDelta);
            }
          }
          finally
          {
            this.recursionGuard = savedRecursionGuard;
          }
          AddStartDepth(startDepth, succ, currentDepth);
        }
      }
      return stackDepth;
    }

    private static void AddStartDepth(ArrayMap<StackInfo> dict, CFGBlock block, StackInfo startDepth)
    {
      Contract.Requires(block != null);// F: Added as of Clousot suggestion
      Contract.Requires(dict != null);// F: Added as of Clousot suggestion

      Contract.Assert(block.Index >= 0, "assuming the invariant");

      StackInfo oldDepth;    
      if (dict.TryGetValue(block.Index, out oldDepth))
      {
        Contract.Assume(oldDepth.Depth == startDepth.Depth);
      }
      else
      {
        Contract.Assume(!dict.ContainsKey(block.Index), "assuming the behavior of Dictionary");
        dict.Add(block.Index, startDepth.Copy());
      }
    }

    /// <summary>
    /// Specialized map for APCs without finally contexts
    /// </summary>
    class APCMap<T>
    {

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.blockMap != null);// F: Added as of Clousot suggestions as precondition in many places
        Contract.Invariant(this.callOnThisMap != null);// F: Added as of Clousot suggestions as precondition in many places
        Contract.Invariant(this.callArgumentDelegateTargets != null);
      }


      ArrayMap<T>[] blockMap;
      IFunctionalIntMap<bool> callOnThisMap; //indexed by block index, returns true if that call block is on this
      DoubleFunctionalMap<int, int, Method> callArgumentDelegateTargets; // indexed by block index, then local stack offset

      public APCMap(Subroutine parent)
      {
        Contract.Requires(parent != null);

        blockMap = new ArrayMap<T>[parent.BlockCount];
        callOnThisMap = FunctionalIntMap<bool>.EmptyMap;
        callArgumentDelegateTargets = DoubleFunctionalMap<int, int, Method>.Empty(i => i);
      }

      public void Add(APC pc, T value)
      {
        Contract.Assume(pc.Block != null, "Assuming the object invariant");
        Contract.Assume(pc.Index >= 0, "Assuming the object invariant");
        ArrayMap<T> indexMap = blockMap[pc.Block.Index];
        if (indexMap == null)
        {
          indexMap = new ArrayMap<T>(pc.Block.Count + 1); // including one past last
          Contract.Assert(pc.Block.Index < blockMap.Length);
          blockMap[pc.Block.Index] = indexMap;
        }
        Contract.Assume(pc.Index < indexMap.Count);
        Contract.Assume(!indexMap.ContainsKey(pc.Index));
        indexMap.Add(pc.Index, value);
      }

      public void RecordCallOnThis(APC pc)
      {
        Contract.Requires(pc.Block != null);  // F: As of Clousot suggestion
        callOnThisMap = callOnThisMap.Add(pc.Block.Index, true);
      }

      public void RecordCallArgument(APC pc, int stackoffset, Method target)
      {
        Contract.Assume(pc.Block != null);
        callArgumentDelegateTargets = callArgumentDelegateTargets.Add(pc.Block.Index, stackoffset, target);
      }

      public T this[APC key]
      {
        get
        {
          T result;
          if (!TryGetValue(key, out result))
          {
            throw new NotSupportedException("key not in table");
          }
          return result;
        }
      }

      public bool IsCallOnThis(APC key)
      {
        Contract.Assume(key.Block != null);
        return this.callOnThisMap[key.Block.Index];
      }

      public bool TryGetArgumentDelegateTarget(APC key, int stackOffset, out Method method)
      {
        Contract.Assume(key.Block != null);
        method = callArgumentDelegateTargets[key.Block.Index, stackOffset];
        return callArgumentDelegateTargets.Contains(key.Block.Index, stackOffset);
      }

      public bool TryGetValue(APC pc, out T value)
      {
        Contract.Assume(pc.Block != null);
        ArrayMap<T> indexMap = blockMap[pc.Block.Index];
        if (indexMap == null)
        {
          value = default(T);
          return false;
        }
        Contract.Assume(pc.Index < indexMap.Count);
        return indexMap.TryGetValue(pc.Index, out value);
      }

      public bool ContainsKey(APC pc)
      {
        Contract.Assume(pc.Block != null, "Assuming object invariant");
        ArrayMap<T> indexMap = blockMap[pc.Block.Index];
        if (indexMap == null)
        {
          return false;
        }
        Contract.Assume(pc.Index < indexMap.Count);
        return indexMap.ContainsKey(pc.Index);
      }
    }

    #endregion

    /// <summary>
    /// Computes the stack depth for the given method CFG main blocks. Subroutine stack depth is computed on demand.
    /// </summary>
    public StackDepthProvider(IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, ContextData, Unit> ilDecoder,
                              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Requires(mdDecoder != null); // F: As of Clousot suggestion
      Contract.Requires(ilDecoder != null);// F: As of Clousot suggestion

      this.ilDecoder = ilDecoder;
      this.mdDecoder = mdDecoder;
      // The stack depth is computed on demand
    }

    private bool DebugStack
    {
      get { return this.debugStack; }
      set
      {
        this.debugStack = value;
        if (value)
        {
          this.printer = PrinterFactory.Create(ilDecoder, mdDecoder, delegate(Unit i) { return ""; }, delegate(Unit i) { return ""; });
        }
        else
        {
          this.printer = null;
        }
      }
    }

    #region IVisitMSIL<APC,Variable,Constant,Method,Field,Type,Unit,Unit,int,int> Members

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Assert(APC pc, string tag, Unit condition, object provenance, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Assume(APC pc, string tag, Unit source, object provenance, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Arglist(APC pc, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Binary(APC pc, BinaryOperator op, Unit dest, Unit s1, Unit s2, StackInfo data)
    {
      return data.Pop(2).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.BranchCond(APC pc, APC target, BranchOperator bop, Unit value1, Unit value2, StackInfo data)
    {
      return data.Pop(2);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.BranchTrue(APC pc, APC target, Unit cond, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.BranchFalse(APC pc, APC target, Unit cond, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Branch(APC pc, APC target, bool leave, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Break(APC pc, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, StackInfo data)
      //where TypeList : IIndexable<Type>
      //where ArgList : IIndexable<Unit>
    {
      int pops = mdDecoder.Parameters(method).AssumeNotNull().Count + (extraVarargs == null ? 0 : extraVarargs.Count);
      if (!mdDecoder.IsStatic(method))
      {
        // receiver
        // record call on this
        if (data.IsThis(pops))
        {
          // call on this
          this.stackDepthMirrorForEndOld.RecordCallOnThis(pc);
        }
        pops++;
      }
      else
      {
        for (int i = 0; i < pops; i++)
        {
          // see if we are passing any method delegates with known targets
          Method m;
          if (data.TryGetTarget(i, out m))
          {
            this.stackDepthMirrorForEndOld.RecordCallArgument(pc, data.Depth - i - 1, m);
          }
        }
      }
      data = data.Pop(pops);
      if (mdDecoder.IsVoid(mdDecoder.ReturnType(method)))
      {
        return data;
      }
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Unit dest, Unit fp, ArgList args, StackInfo data)
      //where TypeList : IIndexable<Type>
      //where ArgList : IIndexable<Unit>
    {
      int pops = 1; // function pointer
      if (isInstance) pops++; // instance this (not represented in type list)
      pops += argTypes == null ? 0 : argTypes.Count;
      data = data.Pop(pops);
      if (mdDecoder.IsVoid(returnType))
      {
        return data;
      }
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ckfinite(APC pc, Unit dest, Unit source, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Cpblk(APC pc, bool @volatile, Unit dest, Unit src, Unit len, StackInfo data)
    {
      return data.Pop(3);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Endfilter(APC pc, Unit source, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Endfinally(APC pc, StackInfo data)
    {
      return new StackInfo(0, 0);
    }

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Entry(APC pc, Method method, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Initblk(APC pc, bool @volatile, Unit destaddr, Unit value, Unit size, StackInfo data)
    {
      return data.Pop(3);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Jmp(APC pc, Method method, StackInfo data)
    {
      return new StackInfo(0, 0);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldarg(APC pc, Parameter argument, bool isOld, Unit dest, StackInfo data)
    {
      if (!mdDecoder.IsStatic(mdDecoder.DeclaringMethod(argument)))
      {
        if (mdDecoder.ArgumentIndex(argument) == 0)
        {
          // this
          return data.PushThis();
        }
      }
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldarga(APC pc, Parameter argument, bool isOld, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Ldconst(APC pc, Object constant, Type type, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Ldnull(APC pc, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldftn(APC pc, Method method, Unit dest, StackInfo data)
    {
      return data.Push(method);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldind(APC pc, Type type, bool @volatile, Unit dest, Unit ptr, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldloc(APC pc, Local local, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldloca(APC pc, Local local, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Ldstack(APC pc, int offset, Unit dest, Unit source, bool old, StackInfo data)
    {
      return data.Push(data[offset]);
    }

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Ldstacka(APC pc, int offset, Unit dest, Unit source, Type type, bool old, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Localloc(APC pc, Unit dest, Unit size, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Nop(APC pc, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Pop(APC pc, Unit source, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Return(APC pc, Unit source, StackInfo data)
    {
      // we leave the return value on the stack here, as we consume it in the Exit block
      // or leave it on the stack if inlining is used
      int expectedExitStackDepth = pc.Block.Subroutine.StackDelta;

      if (pc.Block.Subroutine.HasReturnValue)
      {
        expectedExitStackDepth++;
      }
      Contract.Assume(data.Depth == expectedExitStackDepth); // F: made an assume
      return data; // leave stack
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Starg(APC pc, Parameter argument, Unit source, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Stind(APC pc, Type type, bool @volatile, Unit ptr, Unit value, StackInfo data)
    {
      return data.Pop(2);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Stloc(APC pc, Local local, Unit source, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Unit value, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Unit dest, Unit source, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Box(APC pc, Type type, Unit dest, Unit source, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, StackInfo data)
      //where TypeList : IIndexable<Type>
      //where ArgList : IIndexable<Unit>
    {
      int pops = mdDecoder.Parameters(method).AssumeNotNull().Count + (extraVarargs == null ? 0 : extraVarargs.Count);
      if (!mdDecoder.IsStatic(method))
      {
        // receiver

        // record call on this
        if (data.IsThis(pops))
        {
          this.stackDepthMirrorForEndOld.RecordCallOnThis(pc);
        }
        pops++;
      }
      data = data.Pop(pops);
      if (mdDecoder.IsVoid(mdDecoder.ReturnType(method)))
      {
        return data;
      }
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Castclass(APC pc, Type type, Unit dest, Unit obj, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Cpobj(APC pc, Type type, Unit destptr, Unit srcptr, StackInfo data)
    {
      return data.Pop(2);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Initobj(APC pc, Type type, Unit ptr, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Isinst(APC pc, Type type, Unit dest, Unit obj, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldelem(APC pc, Type type, Unit dest, Unit array, Unit index, StackInfo data)
    {
      return data.Pop(2).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldelema(APC pc, Type type, bool @readonly, Unit dest, Unit array, Unit index, StackInfo data)
    {
      return data.Pop(2).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldfld(APC pc, Field field, bool @volatile, Unit dest, Unit obj, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldflda(APC pc, Field field, Unit dest, Unit obj, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldlen(APC pc, Unit dest, Unit array, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldsfld(APC pc, Field field, bool @volatile, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldsflda(APC pc, Field field, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldtypetoken(APC pc, Type type, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldfieldtoken(APC pc, Field field, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldmethodtoken(APC pc, Method method, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Ldvirtftn(APC pc, Method method, Unit dest, Unit obj, StackInfo data)
    {
      return data.Pop(1).Push(method);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Mkrefany(APC pc, Type type, Unit dest, Unit obj, StackInfo data)
    {
      return data;
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Newarray<ArgList>(APC pc, Type type, Unit dest, ArgList lengths, StackInfo data)
      //where ArgList : IIndexable<Unit>
    {
      return data.Pop(lengths.Count).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Newobj<ArgList>(APC pc, Method ctor, Unit dest, ArgList args, StackInfo data)
      //where ArgList : IIndexable<Unit>
    {
      int pops = mdDecoder.Parameters(ctor).AssumeNotNull().Count;
      if (pops == 2 && mdDecoder.IsDelegate(mdDecoder.DeclaringType(ctor)))
      {
        var mtoken = data.As<Method>(0); // last thing pushed was fn pointer
        return data.Pop(2).Push(mtoken);
      }
      else
      {
        return data.Pop(pops).Push();
      }
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Refanytype(APC pc, Unit dest, Unit source, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Refanyval(APC pc, Type type, Unit dest, Unit source, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Rethrow(APC pc, StackInfo data)
    {
      return new StackInfo(0, 0);
    }

    StackInfo IVisitExprIL<APC, Type, Unit, Unit, StackInfo, StackInfo>.Sizeof(APC pc, Type type, Unit dest, StackInfo data)
    {
      return data.Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Stelem(APC pc, Type type, Unit array, Unit index, Unit value, StackInfo data)
    {
      return data.Pop(3);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Stfld(APC pc, Field field, bool @volatile, Unit obj, Unit value, StackInfo data)
    {
      return data.Pop(2);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Stsfld(APC pc, Field field, bool @volatile, Unit value, StackInfo data)
    {
      return data.Pop(1);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Throw(APC pc, Unit exn, StackInfo data)
    {
      return new StackInfo(0, 0);
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Unbox(APC pc, Type type, Unit dest, Unit obj, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    StackInfo IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, StackInfo, StackInfo>.Unboxany(APC pc, Type type, Unit dest, Unit obj, StackInfo data)
    {
      return data.Pop(1).Push();
    }

    #region IVisitSynthIL<APC,Method,Unit,Unit,int,int> Members

    int OldStartDepth(Subroutine subroutine)
    {
      Contract.Requires(subroutine != null);// F: Added as of Clousot suggestion

      // must be an ensures which implements IMethodInfo
      IMethodInfo<Method> mi = (IMethodInfo<Method>)subroutine;

      int offset = this.mdDecoder.Parameters(mi.Method).AssumeNotNull().Count;
      if (!this.mdDecoder.IsConstructor(mi.Method) && !this.mdDecoder.IsStatic(mi.Method)) offset++;
      //if (!this.mdDecoder.IsVoidMethod(mi.Method)) offset--;
      return offset;
    }

    /// <summary>
    /// Here we have to switch to the stack depth present at call-site to the method of which we are analyzing
    /// the post condition. During evaluation, the APC context will however be the exiting edge from the call-site,
    /// thus the context stack depth will not contain the argument count, only the result count (0 or 1) depending on the 
    /// Voidness of the method result.
    /// In order to obtain the correct stack depth, we thus start the local stack depth with #args - (IsVoidMethod?0:1), so
    /// that the proper offsets on the evaluation stack are going to be used without trashing the stack contents present
    /// at the call site.
    /// For the application of this ensures within its own method, the stack is initially empty, as the arguments are accessed
    /// with ldarg, not with ldstack. Thus, having an offset other than starting at 0 is okay, except that we get into the corner
    /// case of having to use stack offset -1, which can happen for a static method without arguments but a return value.
    /// </summary>
    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.BeginOld(APC pc, APC matchingEnd, StackInfo data)
    {
      return data; // we don't adjust the stack here.
      //int offset = OldStartDepth(pc.Block.Subroutine);
      //return new StackInfo(offset, 4);
    }

    /// <summary>
    /// Continue with stack depth at matching begin + 1 for the old value.
    ///
    /// NOTE: the stack depth associated with pc is the stack depth at the end of the old expression, not the new
    /// stack depth.
    /// </summary>
    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.EndOld(APC pc, APC matchingBegin, Type type, Unit dest, Unit source, StackInfo data)
    {
      // this is a pop and a push, so even.
      return data.Pop(1).Push();
      //int previousDepth = this.stackDepthMirrorForEndOld[matchingBegin];
      //return new StackInfo(previousDepth + 1, 4);
    }

    StackInfo IVisitSynthIL<APC, Method, Type, Unit, Unit, StackInfo, StackInfo>.Ldresult(APC pc, Type type, Unit dest, Unit source, StackInfo data)
    {
      return data.Push();
    }

    #endregion

    #endregion



    #region IMSILDecoder<Label,Local,Parameter,Method,Field,Type,int,int> Members

    APCMap<int> GetStackDepthMap(Subroutine subroutine)
    {
      Contract.Requires(subroutine != null);
      APCMap<int> localStackDepth;
      if (!subroutine.TryGetValue(new TypedKey<APCMap<int>>(StackDepthKey), out localStackDepth))
      {
        try
        {
          localStackDepth = ComputeStackDepth(subroutine);
        }
        catch (Exception)
        {
#if DEBUG
          Console.WriteLine("Probably bad code in subroutine: {0} ({1})", subroutine.Name, subroutine.Kind);
          Console.WriteLine("running stack analysis again with debug");
          System.Diagnostics.Debugger.Launch();
          this.DebugStack = true;
          this.ComputeStackDepth(subroutine);
#endif
          throw;
        }
        // store the stack depth map it in the subroutine properties
        subroutine.Add(new TypedKey<APCMap<int>>(StackDepthKey), localStackDepth);
      }
      return localStackDepth;
    }

    APCMap<int> localStackDepthCache;
    int cachedSubroutine;
    private bool debugStack;

    APCMap<int> LocalStackMap(Subroutine subroutine)
    {
      Contract.Requires(subroutine != null);
      Contract.Ensures(Contract.Result<APCMap<int>>() != null);
      
      if (localStackDepthCache == null || cachedSubroutine != subroutine.Id)
      {
        APCMap<int> localStackDepth = GetStackDepthMap(subroutine);
        localStackDepthCache = localStackDepth;
        cachedSubroutine = subroutine.Id;
      }

      Contract.Assume(localStackDepthCache != null);
      return localStackDepthCache;
    }

    public int LocalStackDepth(APC apc)
    {
      Contract.Assume(apc.Block != null);
      APCMap<int> localStackDepth = LocalStackMap(apc.Block.Subroutine);
      return localStackDepth[apc];
    }

    /// <summary>
    /// Compute the stack depth at the given apc
    /// </summary>
    int Lookup(APC apc)
    {
      int localDepth = LocalStackDepth(apc);
      if (apc.SubroutineContext == null || !apc.Block.Subroutine.HasContextDependentStackDepth)
      {
        return localDepth;
      }
      CFGBlock contextBlock = apc.SubroutineContext.Head.One;
      return localDepth + Lookup(APC.ForEnd(contextBlock, apc.SubroutineContext.Tail));
    }

    struct StackDecoder<Data, Result, Visitor> :
      IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>
      where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, int, int, Data, Result>
    {

      #region invariants

      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.parent != null);
      }


      #endregion

      #region Privates
      private Visitor delegatee;

      [Pure]
      private Data Convert(Data data)
      {
        return data;
      }

      StackDepthProvider<Local, Parameter, Method, Field, Property, Event, Type, ContextData, Attribute, Assembly> parent;

      #endregion

      public StackDecoder(StackDepthProvider<Local, Parameter, Method, Field, Property, Event, Type, ContextData, Attribute, Assembly> parent,
                          Visitor delegatee)
      {
        Contract.Requires(parent != null); // F: added because of Clousot suggestion
        this.delegatee = delegatee;
        this.parent = parent;
      }

      int Pop(APC l, int offset)
      {
        Contract.Ensures(this.parent != null);

        Contract.Assume(l.Block != null);

        int top = this.parent.Lookup(l) - 1;
        int result = top - offset;
        Contract.Assume(l.Block.Subroutine.HasContextDependentStackDepth || result >= 0);
        return result;
      }

      int Push(APC l, int args, Type type)
      {
        Contract.Assume(this.parent.mdDecoder != null, "Assumption form the base class object invariant");
        if (parent.mdDecoder.IsVoid(type))
        {
          return -1;
        }
        return Push(l, args);
      }

      int Push(APC l, int args)
      {
        Contract.Assume(l.Block != null);
        int top = this.parent.Lookup(l);
        int result = top - args;
        Contract.Assume(l.Block.Subroutine.HasContextDependentStackDepth || result >= 0);
        return result;
      }

      int ComputeArgs(Method m, int extraVarargs)
      {
        Contract.Assume(this.parent.mdDecoder != null, "Assumption form the base class object invariant");

        int count = extraVarargs;
        IIndexable<Parameter> pars = this.parent.mdDecoder.Parameters(m).AssumeNotNull();
        count += pars.Count;
        if (!this.parent.mdDecoder.IsStatic(m))
        {
          count++;
        }
        return count;
      }

      struct SequenceGenerator : IIndexable<int>, IEnumerable<int>
      {
        short from, count;
        // Generates an indexable starting at from, from+1, ..., for count elements
        public SequenceGenerator(int from, int count)
        {
          this.from = checked((short)from);
          this.count = checked((short)count);
        }


        #region IIndexable<int> Members

        int IIndexable<int>.Count
        {
          get { return this.count; }
        }

        public int this[int index]
        {
          get { return this.from + index; }
        }

        #endregion

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
          for (int i = 0; i < this.count; i++)
          {
            yield return this[i];
          }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
          return this.GetEnumerator();
        }

        #endregion
      }

      SequenceGenerator PopSequence(APC l, int args, int offset)
      {
        int from = this.parent.Lookup(l) - args - offset;
        return new SequenceGenerator(from, args);
      }

      #region IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Unit,Unit,Pair<Pair<APC,Data>,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,Data,Result>>,Result> Members

      public Result Assert(APC pc, string tag, Unit condition, object provenance, Data data)
      {
        return delegatee.Assert(pc, tag, Pop(pc, 0), provenance, Convert(data));
      }

      public Result Assume(APC pc, string tag, Unit source, object provenance, Data data)
      {
        // Code either contains the conditional branch/switch or the assume, but not both, so this is a pop
        return delegatee.Assume(pc, tag, Pop(pc, 0), provenance, Convert(data));
      }

      public Result Arglist(APC pc, Unit dest, Data data)
      {
        return delegatee.Arglist(pc, Push(pc, 0), Convert(data));
      }

      public Result Binary(APC pc, BinaryOperator op, Unit dest, Unit s1, Unit s2, Data data)
      {
        return delegatee.Binary(pc, op, Push(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result BranchCond(APC pc, APC target, BranchOperator bop, Unit value1, Unit value2, Data data)
      {
        return delegatee.BranchCond(pc, target, bop, Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result BranchTrue(APC pc, APC target, Unit cond, Data data)
      {
        return delegatee.BranchTrue(pc, target, Pop(pc, 0), Convert(data));
      }

      public Result BranchFalse(APC pc, APC target, Unit cond, Data data)
      {
        return delegatee.BranchFalse(pc, target, Pop(pc, 0), Convert(data));
      }

      public Result Branch(APC pc, APC target, bool leave, Data data)
      {
        return delegatee.Branch(pc, target, leave, Convert(data));
      }

      public Result Break(APC pc, Data data)
      {
        return delegatee.Break(pc, Convert(data));
      }

      enum AssertAssumeKind
      {
        Assert, Assume, None
      }

      public Result Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Unit>
      {
        var mdDecoder = this.parent.mdDecoder;
        var declaringType = mdDecoder.DeclaringType(method);
        var methodName = mdDecoder.Name(method);
        int numArgs = ComputeArgs(method, extraVarargs.Count);
        // special treatment of Assert methods. We treat all methods ending in Assert that have a boolean arg as an assume of that bool
        if (this.parent.mdDecoder.IsVoid(this.parent.mdDecoder.ReturnType(method)))
        {
          var parentTypeName = mdDecoder.FullName(declaringType).AssumeNotNull();
          if (parentTypeName.Contains("Contract") || parentTypeName == "System.Diagnostics.Debug")
          {
            AssertAssumeKind kind = AssertAssumeKind.None;
            if (methodName.EndsWith("Assert")) { kind = AssertAssumeKind.Assert; }
            else if (methodName.EndsWith("Assume")) { kind = AssertAssumeKind.Assume; }
            if (kind != AssertAssumeKind.None)
            {
              IIndexable<Parameter> pars = mdDecoder.Parameters(method).AssumeNotNull();
              for (int i = 0; i < pars.Count; i++)
              {
                if (mdDecoder.ParameterType(pars[i]).Equals(mdDecoder.System_Boolean))
                {
                  int condition = Pop(pc, mdDecoder.ArgumentStackIndex(pars[i]));
                  if (kind == AssertAssumeKind.Assert)
                  {
                    return delegatee.Assert(pc, "assert", condition, null, data);
                  }
                  else
                  {
                    return delegatee.Assume(pc, "assume", condition, null, data);
                  }
                }
              }
            }
          }
        }
        #region explicit == operator overloads
        if (args.Count > 1 && (mdDecoder.IsReferenceType(declaringType) || mdDecoder.IsNativePointerType(declaringType)))
        {
          if (methodName.EndsWith("op_Inequality"))
          {
            return this.Binary(pc, BinaryOperator.Cne_Un, dest, args[0], args[1], data);
          }
          else if (methodName.EndsWith("op_Equality"))
          {
            if (mdDecoder.IsNativePointerType(declaringType))
            {
              return this.Binary(pc, BinaryOperator.Ceq, dest, args[0], args[1], data);
            }
            else
            {
              return this.Binary(pc, BinaryOperator.Cobjeq, dest, args[0], args[1], data);
            }
          }
        }
        #endregion
        #region Identity functions
        if ((mdDecoder.Equal(declaringType, mdDecoder.System_UIntPtr)
          || mdDecoder.Equal(declaringType, mdDecoder.System_IntPtr))
          && methodName.StartsWith("op_Explicit"))
        {
          Contract.Assert(0 <= args.Count);
          return this.Ldstack(pc, 0, dest, args[0], false, data);
        }
        // VB's funny copy method that copies boxed objects to avoid aliasing by accident
        if (methodName == "GetObjectValue" && mdDecoder.Name(declaringType) == "RuntimeHelpers")
        {
          return this.Ldstack(pc, 0, dest, args[0], false, data);
        }
        #endregion
        return delegatee.Call(pc, method, tail, virt, extraVarargs, Push(pc, numArgs, parent.mdDecoder.ReturnType(method)), PopSequence(pc, numArgs, 0), Convert(data));
      }

      public Result Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Unit dest, Unit fp, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Unit>
      {
        int numArgs = argTypes.Count + (isInstance ? 1 : 0);
        return delegatee.Calli(pc, returnType, argTypes, tail, isInstance, Push(pc, numArgs + 1, returnType), Pop(pc, 0), PopSequence(pc, numArgs, 1), Convert(data));

      }

      public Result Ckfinite(APC pc, Unit dest, Unit source, Data data)
      {
        return delegatee.Ckfinite(pc, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Cpblk(APC pc, bool @volatile, Unit destaddr, Unit srcaddr, Unit len, Data data)
      {
        return delegatee.Cpblk(pc, @volatile, Pop(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Endfilter(APC pc, Unit decision, Data data)
      {
        return delegatee.Endfilter(pc, Pop(pc, 0), Convert(data));
      }

      public Result Endfinally(APC pc, Data data)
      {
        return delegatee.Endfinally(pc, Convert(data));
      }

      public Result Entry(APC pc, Method method, Data data)
      {
        return delegatee.Entry(pc, method, Convert(data));
      }

      public Result Initblk(APC pc, bool @volatile, Unit destaddr, Unit value, Unit len, Data data)
      {
        return delegatee.Initblk(pc, @volatile, Pop(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Jmp(APC pc, Method method, Data data)
      {
        return delegatee.Jmp(pc, method, Convert(data));
      }

      /// <summary>
      /// This case is complicated by the fact that when requires/ensures/invariant/subroutines
      /// are called around method calls, the argument loading must be translated into loads from
      /// the evaluation stack.
      /// Further complications arise due to constructors, where the constructor method pretends that 
      /// "this" is the first argument, but for the caller, it is actually the result of the call.
      /// Finally, more complications arise due to inheritance of pre/post conditions in that the 
      /// argument identity is not identical to the argument of the method where the subroutine is called.
      /// We thus need to remap.
      ///
      /// We have to distinguish 6 cases:
      ///
      /// 0) InsideRequiresAroundConstructorCall
      ///    - rewire to proper offset on evaluation stack, taking into account that "this" is
      ///      not on the caller stack
      ///
      /// 1) InsideRequiresAroundCall
      ///     - rewire to proper offset on evaluation stack
      ///
      /// 2) InsideEnsuresOrInvariantAfterConstructorCall
      ///     - access to "this" is ldresult
      ///     - other parameter accesses are offset by 1 onto the stack because there is no
      ///       this parameter.
      ///
      /// 3) InsideEnsuresOrInvariantAfterCall
      ///     - find proper offset on the evaluation stack
      ///
      /// 4) Inside Contract subroutine at method boundaries, but requires.Method and calling Method differ
      ///     - map to proper argument and delegate to ldarg
      ///
      /// 5) Normal ldarg otherwise
      /// </summary>
      public Result Ldarg(APC pc, Parameter argument, bool ignoredIsOld, Unit dest, Data data)
      {
        bool isLdResult;
        int ldStackOffset;
        bool isOld;
        APC lookupPC;

        Parameter oldArgument = argument;

        if (RemapParameterToLdStack(pc, ref argument, out isLdResult, out ldStackOffset, out isOld, out lookupPC))
        {
          // ldstack
          return delegatee.Ldstack(pc, ldStackOffset, Push(pc, 0), Pop(lookupPC, ldStackOffset), isOld, data);
        }

        // F: TODOTODO Awfull fix!!!! But I do not really understand the code, I should talk with MAF about it
        if (argument == null)
        {
          argument = oldArgument;
        }
        // F: End

        if (isLdResult)
        {
          // ldresult
          // special case: if the parameter is typed address of struct, then we must be
          // referring to the post state of a struct constructor. In this case, map it to ldstacka
          if (this.parent.mdDecoder.IsStruct(this.parent.mdDecoder.DeclaringType(this.parent.mdDecoder.DeclaringMethod(argument))))
          {
            return delegatee.Ldstacka(pc, ldStackOffset, Push(pc, 0), Pop(pc, ldStackOffset), this.parent.mdDecoder.ParameterType(argument), isOld, data);
          }
          return delegatee.Ldresult(pc, this.parent.mdDecoder.ParameterType(argument), Push(pc, 0), Pop(pc, ldStackOffset), data);
        }
        return delegatee.Ldarg(pc, argument, isOld, Push(pc, 0), Convert(data));
      }

      /// <summary>
      /// Remaps a parameter referenced in a subroutine to the parameter used in the calling context
      /// This is needed e.g. when inheriting requires/ensures/invariant subroutines, as the parameter
      /// identities are different in each override.
      /// </summary>
      /// <param name="p">Parameter as appearing in the subroutine</param>
      /// <param name="parentMethodBlock">block in the method calling the subroutine</param>
      /// <param name="subroutineBlock">block in the subroutine</param>
      /// <returns>The equivalent parameter in the calling method's context</returns>
      Parameter RemapParameter(Parameter p, CFGBlock parentMethodBlock, CFGBlock subroutineBlock)
      {
        Contract.Requires(subroutineBlock != null);// F: added because of Clousot suggestion
        Contract.Requires(parentMethodBlock != null);// F: added because of Clousot suggestion

        Contract.Assume(this.parent.mdDecoder != null, "Assumption from the base class object invariant");


        IMethodInfo<Method> parentMethodInfo = (IMethodInfo<Method>)parentMethodBlock.Subroutine;
        IMethodInfo<Method> subroutineMethodInfo = (IMethodInfo<Method>)subroutineBlock.Subroutine;

        if (this.parent.mdDecoder.Equal(parentMethodInfo.Method, subroutineMethodInfo.Method))
        {
          return p;
        }

        int argIndex = this.parent.mdDecoder.ArgumentIndex(p);

        if (this.parent.mdDecoder.IsStatic(parentMethodInfo.Method))
        {
          return this.parent.mdDecoder.Parameters(parentMethodInfo.Method).AssumeNotNull()[argIndex];
        }
        else
        {
          if (argIndex == 0)
          {
            // refers to "this"
            return this.parent.mdDecoder.This(parentMethodInfo.Method);
          }
          Contract.Assume(argIndex -1 >= 0);
          return this.parent.mdDecoder.Parameters(parentMethodInfo.Method).AssumeNotNull()[argIndex - 1];
        }
      }

      /// <summary>
      /// Implementes the logic outlined in Ldarg by mapping a parameter x APC context into one of
      /// three cases:
      ///   - ldresult         (indicated by setting isLdResult true and setting ldStackOffset)
      ///   - ldstack offset   (indicated by returning true, and setting ldStackOffset)
      ///   - ldarg P'         (otherwise, where the parameter ref contains p' on exit)
      /// </summary>
      /// <param name="isOld">set to true if the resulting instruction should execute in the pre-state of the
      /// method.</param>
      /// <param name="lookupPC">Set to the pc at which we need to consider the ldStackOffset. Valid on true return.</param>
      /// <returns>true if the parameter access maps to ldresult</returns>
      bool RemapParameterToLdStack(APC pc, ref Parameter p, out bool isLdResult, out int ldStackOffset, out bool isOld, out APC lookupPC)
      {
        if (pc.SubroutineContext != null)
        {
          #region Requires
          if (pc.Block.Subroutine.IsRequires)
          {
            // find whether calling context is entry, newObj, or call. In the first case, also remap 
            // the parameter if necessary

            isLdResult = false;
            isOld = false;
            lookupPC = pc;

            for (SubroutineContext scontext = pc.SubroutineContext; scontext != null; scontext = scontext.Tail)
            {
              var callTag = scontext.Head.Three;
              Contract.Assume(callTag != null);
              if (callTag == "entry")
              {
                // keep as ldarg, but figure out whether we need to remap
                Contract.Assume(scontext.Head.One != null);
                p = RemapParameter(p, scontext.Head.One, pc.Block);
                ldStackOffset = 0;
                return false;
              }
              if (callTag.StartsWith("before")) // beforeCall | beforeNewObj
              {
                int localStackDepth = this.parent.LocalStackDepth(pc);
                ldStackOffset = this.parent.mdDecoder.ArgumentStackIndex(p) + localStackDepth;
                return true;
              }
            }
            throw new NotImplementedException();
          }
          #endregion
          #region Ensures
          else if (pc.Block.Subroutine.IsEnsuresOrOld)
          {
            // find whether calling context is exit, newObj, or call. In the first case, also remap 
            // the parameter if necessary
            isOld = true;

            for (SubroutineContext scontext = pc.SubroutineContext; scontext != null; scontext = scontext.Tail)
            {
              string callTag = scontext.Head.Three;

              if (callTag == "exit")
              {
                // keep as ldarg, but figure out whether we need to remap
                Contract.Assume(scontext.Head.One != null);
                p = RemapParameter(p, scontext.Head.One, pc.Block);
                isLdResult = false;
                ldStackOffset = 0;
                lookupPC = pc; // irrelevant
                return false;
              }
              if (callTag == "afterCall")
              {
                // we need to compute the offset to the "parameter" on the stack. For this we must
                // know what method is being called.
                ldStackOffset = this.parent.mdDecoder.ArgumentStackIndex(p);

                // no need to correct for local stack depth, as we lookup the stack in the old-state
                isLdResult = false;
                lookupPC = new APC(scontext.Head.One, 0, scontext.Tail);
                return true;
              }
              if (callTag == "afterNewObj")
              {
                // here we have to deal with the special case of referencing argument 0, which is the result
                // of the construction. In that case we have to return "ldresult"
                int parameterIndex = this.parent.mdDecoder.ArgumentIndex(p);
                if (parameterIndex == 0)
                {
                  ldStackOffset = this.parent.LocalStackDepth(pc);
                  isLdResult = true;
                  lookupPC = pc; // irrelevant
                  isOld = false;
                  return false;
                }

                // we need to compute the offset to the "parameter" on the stack. For this we must
                // know what method is being called.
                ldStackOffset = this.parent.mdDecoder.ArgumentStackIndex(p);

                // no need to correct for local stack depth, as we lookup the stack in the old-state
                isLdResult = false;
                lookupPC = new APC(scontext.Head.One, 0, scontext.Tail);
                return true;
              }

              if (callTag == "oldmanifest")
              {
                // used to manifest the old values on entry to a method
                // keep as ldarg, but figure out whether we need to remap
                Contract.Assume(scontext.Tail.Head.One != null);

                p = RemapParameter(p, scontext.Tail.Head.One, pc.Block);
                isOld = false;
                isLdResult = false;
                ldStackOffset = 0;
                lookupPC = pc; // irrelevant
                return false;
              }

              if (callTag == "afterCallAssume")
              {
                // nothing to do as it won't refer to callee parameters
                isOld = false;
                isLdResult = false;
                ldStackOffset = 0;
                lookupPC = pc;
                return false;
              }
            }
            throw new NotImplementedException();
          }
          #endregion
          #region Invariant
          else if (pc.Block.Subroutine.IsInvariant)
          {
            for (SubroutineContext scontext = pc.SubroutineContext; scontext != null; scontext = scontext.Tail)
            {
              string callTag = scontext.Head.Three;
              Contract.Assume(this.parent.mdDecoder != null);
              // must be "this"
              Contract.Assume(this.parent.mdDecoder.ArgumentIndex(p) == 0);

              if (callTag == "entry" || callTag == "exit")
              {
                // keep as ldarg, but remap to this of containing method
                Method containingMethod;
                if (pc.TryGetContainingMethod(out containingMethod))
                {
                  p = this.parent.mdDecoder.This(containingMethod);
                  isLdResult = false;
                  ldStackOffset = 0;
                  isOld = (callTag == "exit");
                  lookupPC = pc; // irrelevant
                  return false;
                }
                else
                {
                  Contract.Assume(false, "Not in a method context");
                  // dont'r remap
                  isLdResult = false;
                  ldStackOffset = 0;
                  isOld = false;
                  lookupPC = pc;
                  return false;
                }
              }
              if (callTag == "afterCall")
              {
                // we need to compute the offset to the "this" receiver on the stack. For this we must
                // know what method is being called.

                Method calledMethod;
                bool isNewObj;
                bool isVirtualCall;
                bool success = scontext.Head.One.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall);
                Contract.Assume(success); // must be a method call blcok
                int parameterCount = this.parent.mdDecoder.Parameters(calledMethod).AssumeNotNull().Count;

                ldStackOffset = parameterCount; // 0 is top, 1 is 1 step below top of stack, etc...

                // no need to correct for local stack depth, as we lookup the stack in the old-state
                isLdResult = false;
                isOld = true;
                lookupPC = new APC(scontext.Head.One, 0, scontext.Tail);
                return true;
              }
              if (callTag == "afterNewObj")
              {
                // we need to map "this" to ldresult
                isLdResult = true;
                ldStackOffset = this.parent.LocalStackDepth(pc);
                isOld = false;
                lookupPC = pc;
                return false;
              }
              if (callTag == "assumeInvariant")
              {
                // we need to map "this" to ldstack.0 just prior to call (object must be on top of stack)
                isLdResult = false;
                ldStackOffset = 0;
                isOld = true;
                lookupPC = new APC(scontext.Head.One, 0, scontext.Tail);
                return true;
              }
              if (callTag == "beforeCall")
              {
                throw new InvalidOperationException("This should never happen");
              }
              if (callTag == "beforeNewObj")
              {
                throw new InvalidOperationException("This should never happen");
              }
            }
            throw new NotImplementedException();
          }
          #endregion

        }
        isLdResult = false;
        ldStackOffset = 0;
        isOld = false;
        lookupPC = pc;
        return false;
      }

      /// <summary>
      /// rewire to ldstacka.i if we are in a contract around a call contract
      /// </summary>
      public Result Ldarga(APC pc, Parameter argument, bool dummyOld, Unit dest, Data data)
      {
        bool isLdResult;
        int ldStackOffset;
        bool isOld;
        APC lookupPC;
        if (RemapParameterToLdStack(pc, ref argument, out isLdResult, out ldStackOffset, out isOld, out lookupPC))
        {
          // ldstacka
          // Since we are not actually loading memory, just an address, we just compute the proper
          // stack offset, that's it.
          return delegatee.Ldstacka(pc, ldStackOffset, Push(pc, 0), Pop(lookupPC, ldStackOffset), this.parent.mdDecoder.ParameterType(argument), isOld, data);
        }
        if (isLdResult)
        {
          // ldresulta
          throw new NotImplementedException();
        }
        return delegatee.Ldarga(pc, argument, isOld, Push(pc, 0), Convert(data));
      }

      public Result Ldconst(APC pc, object constant, Type type, Unit dest, Data data)
      {
        return delegatee.Ldconst(pc, constant, type, Push(pc, 0), Convert(data));
      }

      public Result Ldnull(APC pc, Unit dest, Data data)
      {
        return delegatee.Ldnull(pc, Push(pc, 0), Convert(data));
      }

      public Result Ldftn(APC pc, Method method, Unit dest, Data data)
      {
        return delegatee.Ldftn(pc, method, Push(pc, 0), Convert(data));
      }

      public Result Ldind(APC pc, Type type, bool @volatile, Unit dest, Unit ptr, Data data)
      {
        return delegatee.Ldind(pc, type, @volatile, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldloc(APC pc, Local local, Unit dest, Data data)
      {
        return delegatee.Ldloc(pc, local, Push(pc, 0), Convert(data));
      }

      public Result Ldloca(APC pc, Local local, Unit dest, Data data)
      {
        return delegatee.Ldloca(pc, local, Push(pc, 0), Convert(data));
      }

      /// <summary>
      /// Ldresult instructions prior to stack analysis are intended to access the result in a method post condition.
      /// </summary>
      public Result Ldresult(APC pc, Type type, Unit dest, Unit source, Data data)
      {
        int depth = this.parent.LocalStackDepth(pc);
        return delegatee.Ldresult(pc, type, Push(pc, 0), Pop(pc, depth), Convert(data));
      }

      public Result Ldstack(APC pc, int offset, Unit dest, Unit source, bool isOld, Data data)
      {
        return delegatee.Ldstack(pc, offset, Push(pc, 0), Pop(pc, offset), isOld, Convert(data));
      }

      public Result Ldstacka(APC pc, int offset, Unit dest, Unit source, Type type, bool old, Data data)
      {
        return delegatee.Ldstacka(pc, offset, Push(pc, 0), Pop(pc, offset), type, old, Convert(data));
      }

      public Result Localloc(APC pc, Unit dest, Unit size, Data data)
      {
        return delegatee.Localloc(pc, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Nop(APC pc, Data data)
      {
        return delegatee.Nop(pc, Convert(data));
      }

      public Result Pop(APC pc, Unit source, Data data)
      {
        return delegatee.Pop(pc, Pop(pc, 0), Convert(data));
      }

      /// <summary>
      /// Strip out returns except for common return on exti block (inserted by ForwardDecode)
      /// </summary>
      public Result Return(APC pc, Unit source, Data data)
      {
        return delegatee.Nop(pc, Convert(data));
      }

      public Result Starg(APC pc, Parameter argument, Unit source, Data data)
      {
        return delegatee.Starg(pc, argument, Pop(pc, 0), Convert(data));
      }

      public Result Stind(APC pc, Type type, bool @volatile, Unit ptr, Unit value, Data data)
      {
        return delegatee.Stind(pc, type, @volatile, Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Stloc(APC pc, Local local, Unit source, Data data)
      {
        return delegatee.Stloc(pc, local, Pop(pc, 0), Convert(data));
      }

      public Result Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Unit value, Data data)
      {
        return delegatee.Switch(pc, type, cases, Pop(pc, 0), Convert(data));
      }

      public Result Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Unit dest, Unit source, Data data)
      {
        return delegatee.Unary(pc, op, overflow, unsigned, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Box(APC pc, Type type, Unit dest, Unit source, Data data)
      {
        type = GetSpecializedType(pc, type);
        if (this.IsReferenceType(pc, type))
        {
          return delegatee.Nop(pc, data);
        }
        return delegatee.Box(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      /// <summary>
      /// Inside subroutines, we may use pc context to find the instantiation of
      /// type parameters to provide more accurate classification of reference types
      /// </summary>
      private bool IsReferenceType(APC pc, Type type)
      {
        Contract.Assume(this.parent.mdDecoder != null, "Assumption form the base class object invariant");

        return this.parent.mdDecoder.IsReferenceType(type);
      }

      private Type GetSpecializedType(APC pc, Type type)
      {
        Contract.Assume(pc.Block != null);
        IMethodInfo<Method> calleeMI = pc.Block.Subroutine as IMethodInfo<Method>;
        if (calleeMI == null) return type; // can't figure out current method
        var target = calleeMI.Method;
        
        SubroutineContext context = pc.SubroutineContext;
        while (context != null)
        {
          bool isNewObj;
          bool isVirtualCall;
          Method contextMethod;
          SubroutineEdge edge = context.Head;
          context = context.Tail;
          var tag = edge.Three;
          Contract.Assume(tag != null);
          if (tag.StartsWith("after") || tag.StartsWith("assumeInvariant"))
          {
            if (!edge.One.IsMethodCallBlock(out contextMethod, out isNewObj, out isVirtualCall))
            {
              return type; // no context found
            }
          }
          else if (tag.StartsWith("before"))
          {
            if (!edge.Two.IsMethodCallBlock(out contextMethod, out isNewObj, out isVirtualCall))
            {
              return type; // no context found
            }
          }
          else if (tag == "inherited" || tag == "extra") {
            var mi = edge.Two.Subroutine as IMethodInfo<Method>;
            if (mi == null) return type; // no context found
            contextMethod = mi.Method;
          }
          else if (tag == "exit" || tag == "entry")
          {
            var mi = edge.Two.Subroutine as IMethodInfo<Method>;
            if (mi == null) return type; // no context found
            contextMethod = mi.Method;
          }
          else
          {
            // unrecognized context that we can specialize
            return type;
          }
          var specialized = InstantiateTypeAtCalls(contextMethod, target, type);

          // specialize further
          type = specialized;
          target = contextMethod;
        }
        return type;
      }

      private Type InstantiateTypeAtCalls(Method called, Method targetMethod, Type type)
      {
        Contract.Assume(this.parent.mdDecoder != null, "Assumption form the base class object invariant");

        var mdDecoder = this.parent.mdDecoder;

        if (mdDecoder.IsFormalTypeParameter(type))
        {
          var normalizedTypeParamIndex = this.parent.mdDecoder.NormalizedFormalTypeParameterIndex(type);
          Contract.Assume(normalizedTypeParamIndex >= 0, "Problem in picking up the contract");
          var calledType = mdDecoder.DeclaringType(called);
          var targetMethodType = mdDecoder.DeclaringType(targetMethod);

          // no instance (same generic context)
          if (mdDecoder.Equal(calledType, targetMethodType)) return type;

          // direct instance
          if (mdDecoder.Equal(mdDecoder.Unspecialized(calledType), targetMethodType)) {
            var actuals = mdDecoder.NormalizedActualTypeArguments(calledType);
            Contract.Assume(actuals != null, "Missing contract");
            if (normalizedTypeParamIndex < actuals.Count) {
              return actuals[normalizedTypeParamIndex];
            }
            // something is wrong, don't crash
            return type;
          }

          // indirect instance because contract is inherited
          // find out what method called is implementing
          foreach (var candidate in mdDecoder.ImplementedMethods(called).AssumeNotNull())
          {
            var candidateType = mdDecoder.DeclaringType(candidate);
            if (mdDecoder.Equal(mdDecoder.Unspecialized(candidateType), targetMethodType))
            {
              var actuals = mdDecoder.NormalizedActualTypeArguments(candidateType);
              Contract.Assume(actuals != null);
              if (normalizedTypeParamIndex < actuals.Count)
              {
                return actuals[normalizedTypeParamIndex];
              }
              // something is wrong, don't crash
              return type;
            }
          }

          FList<Method> baseMethodsToCheck = FList<Method>.Cons(called, null);
          while (baseMethodsToCheck != null)
          {
            var baseMethod = baseMethodsToCheck.Head;
            baseMethodsToCheck = baseMethodsToCheck.Tail;

            foreach (var candidate in mdDecoder.OverriddenMethods(baseMethod).AssumeNotNull())
            {
              baseMethodsToCheck = baseMethodsToCheck.Cons(candidate);
              var candidateType = mdDecoder.DeclaringType(candidate);
              if (mdDecoder.Equal(mdDecoder.Unspecialized(candidateType), targetMethodType))
              {
                var actuals = mdDecoder.NormalizedActualTypeArguments(candidateType);
                Contract.Assume(actuals != null);
                if (normalizedTypeParamIndex < actuals.Count)
                {
                  return actuals[normalizedTypeParamIndex];
                }
                // something is wrong, don't crash
                return type;
              }
            }
          }
          // couldn't find the instantiation
          return type;
        }
        else if (mdDecoder.IsMethodFormalTypeParameter(type))
        {
          int normalizedMethodParamIndex = this.parent.mdDecoder.MethodFormalTypeParameterIndex(type);
          Contract.Assume(normalizedMethodParamIndex >= 0);

          var actuals = mdDecoder.ActualTypeArguments(called);
          Contract.Assume(actuals != null);
          if (normalizedMethodParamIndex < actuals.Count)
          {
            return actuals[normalizedMethodParamIndex];
          }
          // something is wrong, don't crash
          return type;
        }
        // not a type variable
        return type;
      }

      /// <summary>
      /// searches the context for "before" or "after" calls. Leaves context pointing to the
      /// remainder context, so it can be called again.
      /// </summary>
      private bool TryGetCalledInstanceMethod(ref SubroutineContext context, out Method m)
      {
        for (; context != null; )
        {
          bool isNewObj;
          bool isVirtualCall;
          SubroutineEdge edge = context.Head;
          context = context.Tail;
          var tag = edge.Three;
          Contract.Assume(tag != null);
          if (tag.StartsWith("after"))
          {
            return (edge.One.IsMethodCallBlock(out m, out isNewObj, out isVirtualCall));
          }
          if (tag.StartsWith("before"))
          {
            return (edge.Two.IsMethodCallBlock(out m, out isNewObj, out isVirtualCall));
          }
          // TODO handle other call-like tags
        }
        m = default(Method);
        return false;
      }

      public Result ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Unit>
      {
        int numargs = ComputeArgs(method, extraVarargs.Count);
        return delegatee.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, Push(pc, numargs, parent.mdDecoder.ReturnType(method)), PopSequence(pc, numargs, 0), Convert(data));
      }

      public Result Castclass(APC pc, Type type, Unit dest, Unit obj, Data data)
      {
        return delegatee.Castclass(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Cpobj(APC pc, Type type, Unit destptr, Unit srcptr, Data data)
      {
        return delegatee.Cpobj(pc, type, Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Initobj(APC pc, Type type, Unit ptr, Data data)
      {
        return delegatee.Initobj(pc, type, Pop(pc, 0), Convert(data));
      }

      public Result Isinst(APC pc, Type type, Unit dest, Unit obj, Data data)
      {
        return delegatee.Isinst(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldelem(APC pc, Type type, Unit dest, Unit array, Unit index, Data data)
      {
        return delegatee.Ldelem(pc, type, Push(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldelema(APC pc, Type type, bool @readonly, Unit dest, Unit array, Unit index, Data data)
      {
        return delegatee.Ldelema(pc, type, @readonly, Push(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldfld(APC pc, Field field, bool @volatile, Unit dest, Unit obj, Data data)
      {
        return delegatee.Ldfld(pc, field, @volatile, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldflda(APC pc, Field field, Unit dest, Unit obj, Data data)
      {
        return delegatee.Ldflda(pc, field, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldlen(APC pc, Unit dest, Unit array, Data data)
      {
        return delegatee.Ldlen(pc, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Ldsfld(APC pc, Field field, bool @volatile, Unit dest, Data data)
      {
        return delegatee.Ldsfld(pc, field, @volatile, Push(pc, 0), Convert(data));
      }

      public Result Ldsflda(APC pc, Field field, Unit dest, Data data)
      {
        return delegatee.Ldsflda(pc, field, Push(pc, 0), Convert(data));
      }

      public Result Ldtypetoken(APC pc, Type type, Unit dest, Data data)
      {
        return delegatee.Ldtypetoken(pc, type, Push(pc, 0), Convert(data));
      }

      public Result Ldfieldtoken(APC pc, Field field, Unit dest, Data data)
      {
        return delegatee.Ldfieldtoken(pc, field, Push(pc, 0), Convert(data));
      }

      public Result Ldmethodtoken(APC pc, Method method, Unit dest, Data data)
      {
        return delegatee.Ldmethodtoken(pc, method, Push(pc, 0), Convert(data));
      }

      public Result Ldvirtftn(APC pc, Method method, Unit dest, Unit obj, Data data)
      {
        return delegatee.Ldvirtftn(pc, method, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Mkrefany(APC pc, Type type, Unit dest, Unit obj, Data data)
      {
        return delegatee.Mkrefany(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Newarray<ArgList>(APC pc, Type type, Unit dest, ArgList lengths, Data data)
        where ArgList : IIndexable<Unit>
      {
          var numargs = lengths.Count;
        return delegatee.Newarray(pc, type, Push(pc, numargs), PopSequence(pc, numargs, 0), Convert(data));
      }

      public Result Newobj<ArgList>(APC pc, Method ctor, Unit dest, ArgList args, Data data)
        where ArgList : IIndexable<Unit>
      {
        int numargs = ComputeArgs(ctor, 0);
        numargs--; // subtract receiver, as it is counted as a method parameter for construtors.
        return delegatee.Newobj(pc, ctor, Push(pc, numargs), PopSequence(pc, numargs, 0), Convert(data));
      }

      public Result Refanytype(APC pc, Unit dest, Unit source, Data data)
      {
        return delegatee.Refanytype(pc, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Refanyval(APC pc, Type type, Unit dest, Unit source, Data data)
      {
        return delegatee.Refanyval(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Rethrow(APC pc, Data data)
      {
        return delegatee.Rethrow(pc, Convert(data));
      }

      public Result Sizeof(APC pc, Type type, Unit dest, Data data)
      {
        return delegatee.Sizeof(pc, type, Push(pc, 0), Convert(data));
      }

      public Result Stelem(APC pc, Type type, Unit array, Unit index, Unit value, Data data)
      {
        return delegatee.Stelem(pc, type, Pop(pc, 2), Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Stfld(APC pc, Field field, bool @volatile, Unit obj, Unit value, Data data)
      {
        return delegatee.Stfld(pc, field, @volatile, Pop(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Stsfld(APC pc, Field field, bool @volatile, Unit value, Data data)
      {
        return delegatee.Stsfld(pc, field, @volatile, Pop(pc, 0), Convert(data));
      }

      public Result Throw(APC pc, Unit exn, Data data)
      {
        return delegatee.Throw(pc, Pop(pc, 0), Convert(data));
      }

      public Result Unbox(APC pc, Type type, Unit dest, Unit obj, Data data)
      {
        type = GetSpecializedType(pc, type);
        return delegatee.Unbox(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      public Result Unboxany(APC pc, Type type, Unit dest, Unit obj, Data data)
      {
        type = GetSpecializedType(pc, type);
        return delegatee.Unboxany(pc, type, Push(pc, 1), Pop(pc, 0), Convert(data));
      }

      #endregion

      #region IVisitSynthIL<APC,Method,Unit,Unit,Data,Result> Members


      public Result BeginOld(APC pc, APC matchingEnd, Data data)
      {
        return delegatee.BeginOld(pc, matchingEnd, Convert(data));
      }

      /// <summary>
      /// Note: stack depth associated with this pc is the stack depth at the end of the old expression
      /// in the context of the method entry. Thus we can pop normally, but to push to the correct offset
      /// we need to get the depth at the matching begin.
      /// </summary>
      public Result EndOld(APC pc, APC matchingBegin, Type type, Unit dest, Unit source, Data data)
      {
        if (pc.InsideOldManifestation)
        {
          return delegatee.Ldstack(pc, 1, Push(matchingBegin, 0), Pop(pc, 0), false, Convert(data));
        }

        return delegatee.EndOld(pc, matchingBegin, type, Push(matchingBegin, 0), Pop(pc, 0), Convert(data));
      }

      #endregion
    }

    public Result ForwardDecode<Data, Result, Visitor>(APC lab, Visitor visitor, Data data) where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, int, int, Data, Result>
    {
      // if this is an exit block, index is 0, insert a Return instruction (we remove all ordinary returns)
      if (lab.Index == 0 && lab.SubroutineContext == null && lab.Block == lab.Block.Subroutine.Exit && lab.Block.Subroutine.IsMethod)
      {
        if (!lab.Block.Subroutine.HasReturnValue)
        {
          return visitor.Return(lab, -1, data); // dummy arg
        }
        else
        {
          int topOfStack = this.Lookup(lab) - 1;
          return visitor.Return(lab, topOfStack, data);
        }
      }
      return ilDecoder.ForwardDecode<Data, Result, StackDecoder<Data, Result, Visitor>>(
        lab, new StackDecoder<Data, Result, Visitor>(this, visitor), data);
    }

#if false
    public Transformer<APC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<APC, Local, Parameter, Method, Field, Type, StackTemp, StackTemp, Data, Result> visitor)
    {
      return ilDecoder.CacheForwardDecoder<Data, Result>(new StackDecoder<Data, Result, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, StackTemp, StackTemp, Data, Result>>(this, visitor));
    }
#endif

    public IStackContext<Field, Method> Context { get { return this; } }

    public bool IsUnreachable(APC pc) { return false; }
    #endregion


    #region IStackContext and IMethodContext<Label,Field, Method> Members

    /// <summary>
    /// We don't directly delegate to MethodContext, because we want to return a different CFG
    /// </summary>
    public IMethodContextData<Field, Method> MethodContext { get { return this; } }
    public IStackContextData<Field, Method> StackContext { get { return this; } }
    public int StackDepth(APC pc)
    {
      return this.Lookup(pc);
    }

    public bool TryGetCallArgumentDelegateTarget(APC pc, int stackOffset, out Method method)
    {
      int adjustedStackOffset = stackOffset;
      var context = pc.SubroutineContext;
      while (context != null)
      {
        adjustedStackOffset -= this.LocalStackDepth(new APC(context.Head.One, 0, null));
        context = context.Tail;
      }
      APCMap<int> map = LocalStackMap(pc.Block.Subroutine);

      return map.TryGetArgumentDelegateTarget(pc, adjustedStackOffset, out method);
    }

    public IEnumerable<Field> Modifies(Method method) { return this.ilDecoder.Context.MethodContext.Modifies(method); }
    public IEnumerable<Method> AffectedGetters(Field field) { return this.ilDecoder.Context.MethodContext.AffectedGetters(field); }

    #endregion

    #region ICFG<Type,APC> Members

    APC ICFG.Entry
    {
      get { return cfg.Entry; }
    }

    APC ICFG.EntryAfterRequires
    {
      get { return cfg.EntryAfterRequires; }
    }


    APC ICFG.NormalExit
    {
      get { return cfg.NormalExit; }
    }

    APC ICFG.ExceptionExit
    {
      get { return cfg.ExceptionExit; }
    }

    APC ICFG.Post(APC pc)
    {
      ICFG cfg = this;
      APC succ;
      if (cfg.HasSingleSuccessor(pc, out succ))
      {
        return succ;
      }
      return pc;
    }

    bool ICFG.HasSingleSuccessor(APC ppoint, out APC singleSuccessor)
    {
      CallAdaption.Push(this);
      try
      {
        return cfg.HasSingleSuccessor(ppoint, out singleSuccessor);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    IEnumerable<APC> ICFG.Successors(APC ppoint)
    {
      CallAdaption.Push(this);
      try
      {
        foreach (var succ in cfg.Successors(ppoint))
        {
          yield return succ; // expanded to keep call adaption in MoveNext
        }
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    bool ICFG.HasSinglePredecessor(APC ppoint, out APC singlePredecessor, bool skipContracts)
    {
      CallAdaption.Push(this);
      try
      {
        return cfg.HasSinglePredecessor(ppoint, out singlePredecessor, skipContracts);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    IEnumerable<APC> ICFG.Predecessors(APC ppoint, bool skipContracts)
    {
      CallAdaption.Push(this);
      try
      {
        foreach (var pred in cfg.Predecessors(ppoint, skipContracts))
        {
          yield return pred; // expanded to keep call adaption
        }
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    APC ICFG.PredecessorPCPriorToRequires(APC ppoint)
    {
      CallAdaption.Push(this);
      try
      {
        return cfg.PredecessorPCPriorToRequires(ppoint);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    FList<Pair<string, Subroutine>> ICFG.GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context)
    {
      CallAdaption.Push(this);
      try
      {
        return from.Subroutine.GetOrdinaryEdgeSubroutines(from, to, context);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }


    bool ICFG.IsJoinPoint(APC ppoint)
    {
      return cfg.IsJoinPoint(ppoint);
    }

    bool ICFG.IsSplitPoint(APC ppoint)
    {
      return cfg.IsSplitPoint(ppoint);
    }

    bool ICFG.IsForwardBackEdgeTarget(APC ppoint)
    {
      return cfg.IsForwardBackEdgeTarget(ppoint);
    }

    bool ICFG.IsBackwardBackEdgeTarget(APC ppoint)
    {
      return cfg.IsBackwardBackEdgeTarget(ppoint);
    }

    bool ICFG.IsForwardBackEdge(APC from, APC to)
    {
      return cfg.IsForwardBackEdge(from, to);
    }

    bool ICFG.IsBackwardBackEdge(APC from, APC to)
    {
      return cfg.IsBackwardBackEdge(from, to);
    }

    bool ICFG.IsBlockStart(APC ppoint)
    {
      return cfg.IsBlockStart(ppoint);
    }

    bool ICFG.IsBlockEnd(APC ppoint)
    {
      return cfg.IsBlockEnd(ppoint);
    }

    IEnumerable<APC> ICFG.ExceptionHandlers<Type2, Data>(APC ppoint, Data data, IHandlerFilter<Type2, Data> handlerPredicate)
    {
      return cfg.ExceptionHandlers(ppoint, data, handlerPredicate);
    }

    Microsoft.Research.Graphs.IGraph<APC, Unit> ICFG.AsForwardGraph(bool includeExceptionEdges)
    {
      return cfg.AsForwardGraph(includeExceptionEdges);
    }

    Microsoft.Research.Graphs.IGraph<APC, Unit> ICFG.AsBackwardGraph(bool includeExceptionEdges, bool skipContracts)
    {
      return cfg.AsBackwardGraph(includeExceptionEdges, skipContracts);
    }

    IDecodeMSIL<APC, Local2, Parameter2, Method2, Field2, Type2, Unit, Unit, IMethodContext<Field2, Method2>, Unit> ICFG.GetDecoder<Local2, Parameter2, Method2, Field2, Property2, Event2, Type2, Attribute2, Assembly2>(IDecodeMetaData<Local2, Parameter2, Method2, Field2, Property2, Event2, Type2, Attribute2, Assembly2> mdDecoder)
    {
      return cfg.GetDecoder(mdDecoder);
    }

    Subroutine ICFG.Subroutine
    {
      get { return cfg.Subroutine; }
    }

    string ICFG.ToString(APC pc)
    {
      return cfg.ToString(pc);
    }

    void ICFG.Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> edgePrinter, Func<CFGBlock, IEnumerable<FList<STuple<CFGBlock, CFGBlock, string>>>> contextLookup, FList<STuple<CFGBlock, CFGBlock, string>> context)
    {
      CallAdaption.Push(this);
      try
      {
        cfg.Print(tw, ilPrinter, edgePrinter, contextLookup, context);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    IEnumerable<CFGBlock> ICFG.LoopHeads
    {
      get { return cfg.LoopHeads; }
    }

#if false
    void ICFG.EmitTransferEquations(TextWriter tw, InvariantQuery<APC> invariantDB, AssumptionFinder<APC> assumptionFinder, CrossBlockRenamings<APC> renamings, RenamedVariables<APC> renamed)
    {
      cfg.EmitTransferEquations(tw, invariantDB, assumptionFinder, renamings, renamed);
    }
#endif
    #endregion



    #region IStackInfo Members

    bool IStackInfo.IsCallOnThis(APC pc)
    {
      if (this.recursionGuard) return false;

      // Specialize for ensures specialization
      APCMap<int> map = LocalStackMap(pc.Block.Subroutine);
      return map.IsCallOnThis(pc);
    }

    #endregion

    #region IMethodContextData<Field,Method> Members

    Method IMethodContextData<Field, Method>.CurrentMethod
    {
      get { return this.ilDecoder.Context.MethodContext.CurrentMethod; }
    }

    ICFG IMethodContextData<Field, Method>.CFG
    {
      get { return this; }
    }

    string IMethodContextData<Field, Method>.SourceContext(APC label)
    {
      return this.ilDecoder.Context.MethodContext.SourceContext(label);
    }

    string IMethodContextData<Field, Method>.SourceDocument(APC pc)
    {
      return this.ilDecoder.Context.MethodContext.SourceDocument(pc);
    }

    StackTemp IMethodContextData<Field, Method>.SourceStartLine(APC pc)
    {
      return this.ilDecoder.Context.MethodContext.SourceStartLine(pc);
    }

    StackTemp IMethodContextData<Field, Method>.SourceEndLine(APC pc)
    {
      return this.ilDecoder.Context.MethodContext.SourceEndLine(pc);
    }

    StackTemp IMethodContextData<Field, Method>.SourceStartColumn(APC pc)
    {
      return this.ilDecoder.Context.MethodContext.SourceStartColumn(pc);
    }

    StackTemp IMethodContextData<Field, Method>.SourceEndColumn(APC pc)
    {
      return this.ilDecoder.Context.MethodContext.SourceEndColumn(pc);
    }

    IEnumerable<Field> IMethodContextData<Field, Method>.Modifies(Method method)
    {
      return this.ilDecoder.Context.MethodContext.Modifies(method);
    }

    IEnumerable<Method> IMethodContextData<Field, Method>.AffectedGetters(Field field)
    {
      return this.ilDecoder.Context.MethodContext.AffectedGetters(field);
    }

    #endregion

    #region IDecodeMSIL<APC,Local,Parameter,Method,Field,Type,int,int,IStackContext<Field,Method>,Unit> Members


    public Unit EdgeData(APC from, APC to)
    {
      return Unit.Value;
    }

    public void Display(TextWriter tw, string prefix, Unit data) { }

    #endregion
  }

}