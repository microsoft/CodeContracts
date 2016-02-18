// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using System.IO;

using Tag = System.String;
using UnitSource = Microsoft.Research.DataStructures.Unit;
using UnitDest = Microsoft.Research.DataStructures.Unit;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Graphs;


namespace Microsoft.Research.CodeAnalysis
{
    using SubroutineEdge = STuple<CFGBlock, CFGBlock, string>;
    using SubroutineContext = FList<STuple<CFGBlock, CFGBlock, string>>;

    [ContextAdapter]
    public interface IStackInfo
    {
        /// <summary>
        /// Return true if the pc is a call site and the call is on the current "this" object of the method
        /// </summary>
        bool IsCallOnThis(APC pc);
    }

    public static class ControlFlowFactory
    {
        public static MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> Create<Local, Parameter, Method, Field, Type, Property, Event, Attribute, Assembly>
           (IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
            IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
            ErrorHandler output
           )
        {
            Contract.Requires(mdDecoder != null);// F: Added as of Clousot suggestion
            Contract.Requires(contractDecoder != null);// F: Added as of Clousot suggestion

            return new MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly>(mdDecoder, contractDecoder, output);
        }
    }

    public static class APCExtensions
    {
        public static string ExtractAssertionCondition(this APC pc)
        {
            Contract.Assume(pc.Block != null, "Assuming the object invariant");
            return pc.Block.SourceAssertionCondition(pc);
        }

        public static APC PrimaryMethodLocation(this APC candidate)
        {
            if (candidate.Block == null)
            {
                return candidate;
            }

            Contract.Assert(candidate.Block != null);
            if (candidate.Block.Subroutine.IsMethod || candidate.Block.Subroutine.IsFaultFinally) return candidate;
            bool isRequiresContext = candidate.Block.Subroutine.IsRequires;

            for (var context = candidate.SubroutineContext; context != null; context = context.Tail)
            {
                if (context.Head.One.Subroutine.IsMethod || context.Head.One.Subroutine.IsFaultFinally)
                {
                    APC methodContext;
                    if (isRequiresContext)
                    {
                        // use target of edge in case of subroutine context
                        var contextBlock = context.Head.Two;
                        methodContext = contextBlock.First;
                    }
                    else
                    {
                        var contextBlock = context.Head.One;
                        methodContext = contextBlock.PriorLast;
                    }
                    return methodContext;
                }
            }
            return candidate; // don't know better. There should always be a method
        }

        public static IEnumerable<APC> NonPrimaryRelatedLocations(this APC candidate)
        {
            bool skippedMethodPrimary = false;
            if (candidate.Block.Subroutine.IsContract)
            {
                // that is the first related one
                yield return candidate;
            }
            else
            {
                Debug.Assert(candidate.Block.Subroutine.IsMethod || candidate.Block.Subroutine.IsFaultFinally);
                skippedMethodPrimary = true;
            }

            for (var subroutineContext = candidate.SubroutineContext; subroutineContext != null; subroutineContext = subroutineContext.Tail)
            {
                if (!skippedMethodPrimary && (subroutineContext.Head.One.Subroutine.IsMethod || subroutineContext.Head.One.Subroutine.IsFaultFinally))
                {
                    skippedMethodPrimary = true;
                    continue;
                }

                CFGBlock contextBlock = subroutineContext.Head.One;
                yield return contextBlock.First;
            }
        }
    }


    /// <summary>
    /// An abstract program point representing a particular control point including possible finally continuations
    ///
    /// The point is an index into a CFGBlock. If the block has a label at that index, the point represents the program
    /// point just PRIOR to the execution of the code at that label. 
    /// If the point is past the sequence of labels in that CFGBlock, then the program point represents the excecution
    /// point just after the last instruction in the block (or predecessor blocks).
    /// </summary>
    [ContractVerification(false)] // F: turning off the verification as something should be changed in the code so to differentiate between the Dummy struct (when this.Block == null) and all the others (when this.Block != null)
    [Serializable]
    public struct APC : IEquatable<APC>
    {
        public static readonly APC Dummy = new APC(null, 0, null); // Thread-safe
                                                                   /// <summary>
                                                                   /// The logical block containing this abstract PC
                                                                   /// </summary>
        public readonly CFGBlock Block;

        /// <summary>
        /// The index within the block of this abstract PC
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Fault/Finally edges. (from,to) that are currently being traversed.
        /// The current block/index is executing on some handler of the first edge in the
        /// finally edges. If the current handler ends(endfinally), the next handler on the
        /// edge is retrieved and made the current program point. If no more handlers are on the
        /// edge, the edge is popped and the target of the edge becomes the new current point.
        /// NOTE: if the edge targets a catch/filter handler entry point, then fault handlers
        /// are visited, otherwise only finally handlers.
        /// </summary>
        public readonly SubroutineContext/*?*/ SubroutineContext;

        /// <summary>
        /// Construct APC
        /// </summary>
        public APC(CFGBlock block, int index,
                   SubroutineContext/*?*/ subroutineContext)
        {
            this.Block = block;
            this.Index = index;
            this.SubroutineContext = subroutineContext;
#if DEBUG
            for (; subroutineContext != null; subroutineContext = subroutineContext.Tail)
            {
                Debug.Assert(block.Subroutine != subroutineContext.Head.One.Subroutine);
            }
#endif
        }


        public APC Post()
        {
            if (this.Index < this.Block.Count)
            {
                return new APC(this.Block, this.Index + 1, this.SubroutineContext);
            }
            return this;
        }

        public static APC ForEnd(CFGBlock block,
                                 SubroutineContext subroutineContext)
        {
            Contract.Requires(block != null);
            return new APC(block, block.Count, subroutineContext);
        }

        public static APC ForStart(CFGBlock block,
                                 SubroutineContext subroutineContext)
        {
            Contract.Requires(block != null); // F: As of Clousot suggestion
            return new APC(block, 0, subroutineContext);
        }

        public APC LastInBlock()
        {
            return ForEnd(this.Block, this.SubroutineContext);
        }

        public APC FirstInBlock()
        {
            return ForStart(this.Block, this.SubroutineContext);
        }

        public string AlternateSourceContext()
        {
            return String.Format("{0}[0x{1:x}]", this.Block.Subroutine.Name, this.Block.ILOffset(this));
        }

        public bool HasRealSourceContext
        {
            get
            {
                string realSC = this.Block.SourceContext(this);
                return realSC != null;
            }
        }
        public string PrimarySourceContext()
        {
            if (this.Equals(APC.Dummy))
            {
                return "";
            }

            string realSC = this.Block.SourceContext(this);
            if (realSC != null) return realSC;
            return AlternateSourceContext();
        }


        public int ILOffset
        {
            get
            {
                return this.Block.ILOffset(this);
            }
        }
        /// <summary>
        /// Copy constructor that removes subroutine context
        /// </summary>
        private APC(APC from)
        {
            Contract.Assume(from.Block != null, "assuming object invariant");

            this.Index = from.Index;
            this.Block = from.Block;
            this.SubroutineContext = null;
        }

        public APC WithoutContext
        {
            get
            {
                return new APC(this);
            }
        }

        [ContractVerification(false)]
        public static void ToString(StringBuilder sb, SubroutineContext finallyContext)
        {
            bool seenFirst = false;
            while (finallyContext != null)
            {
                if (!seenFirst) { sb.Append(" {"); seenFirst = true; } else { sb.Append(", "); }
                sb.AppendFormat("(SR{2} {0},{1}) [{3}]", finallyContext.Head.One.Index, finallyContext.Head.Two.Index, finallyContext.Head.One.Subroutine.Id, finallyContext.Head.Three);
                finallyContext = finallyContext.Tail;
            }
            if (seenFirst)
            {
                sb.Append("}");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.AppendFormat("SR{2} {0},{1}", this.Block.Index, this.Index, this.Block.Subroutine.Id);
            ToString(sb, this.SubroutineContext);
            sb.Append("]");
            return sb.ToString();
        }


        #region IEquatable<APC> Members

        //^ [StateIndependent]
        public bool Equals(APC that)
        {
            if (this.Block != that.Block) return false;
            if (this.Index != that.Index) return false;
            if (this.SubroutineContext != that.SubroutineContext) return false;
            return true;
        }

        #endregion

        public bool InsideRequiresAtCallInsideContract
        {
            [ContractVerification(false)]
            get
            {
                if (this.Block.Subroutine.IsRequires)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "entry") { return false; }
                            if (context.Head.Three.StartsWith("before"))
                            {
                                // find if we are inside another contract
                                Subroutine sub = context.Head.One.Subroutine;
                                return sub.IsEnsuresOrOld || sub.IsRequires || sub.IsInvariant;
                            }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool InsideRequiresAtCall
        {
            get
            {
                if (this.Block.Subroutine.IsRequires)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "entry") { return false; }
                            if (context.Head.Three.StartsWith("before")) { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool InsideEnsuresAtCall
        {
            get
            {
                if (this.Block.Subroutine.IsEnsuresOrOld)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return false; }
                            if (context.Head.Three == "oldmanifest") { return false; }
                            if (context.Head.Three.StartsWith("after")) { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool IsInsideEnsuresAtCall(out bool callInsideContract)
        {
            if (this.Block.Subroutine.IsEnsuresOrOld)
            {
                if (this.SubroutineContext != null)
                {
                    for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                    {
                        if (context.Head.Three == "exit") { callInsideContract = false; return false; }
                        if (context.Head.Three == "oldmanifest") { callInsideContract = false; return false; }
                        if (context.Head.Three.StartsWith("after")) { callInsideContract = context.Head.One.Subroutine.IsContract; return true; }
                    }
                    callInsideContract = false;
                    return false;
                }
            }
            callInsideContract = false;
            return false;
        }

        public bool InsideEnsuresInMethod
        {
            get
            {
                if (this.Block.Subroutine.IsEnsuresOrOld)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return true; }
                            if (context.Head.Three == "entry") { return true; }
                            if (context.Head.Three.StartsWith("after")) { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool InsideInvariantAtCall
        {
            get
            {
                if (this.Block.Subroutine.IsInvariant)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return false; }
                            if (context.Head.Three == "entry") { return false; }
                            if (context.Head.Three.StartsWith("after")) { return true; }
                            if (context.Head.Three == "assumeInvariant") { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool IsInsideInvariantAtCall(out bool callInsideContract)
        {
            if (this.Block.Subroutine.IsInvariant)
            {
                if (this.SubroutineContext != null)
                {
                    for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                    {
                        if (context.Head.Three == "exit") { callInsideContract = false; return false; }
                        if (context.Head.Three == "entry") { callInsideContract = false; return false; }
                        if (context.Head.Three.StartsWith("after")) { callInsideContract = context.Head.One.Subroutine.IsContract; return true; }
                        if (context.Head.Three == "assumeInvariant") { callInsideContract = false; return true; }
                    }
                    callInsideContract = false; return false;
                }
            }
            callInsideContract = false; return false;
        }
        public bool InsideNecessaryAssumption
        {
            get
            {
                return this.Block.Subroutine.IsNecessaryAssumption;
            }
        }

        public bool InsideContractAtCall
        {
            get
            {
                if (this.Block.Subroutine.IsContract || this.Block.Subroutine.IsOldValue)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return false; }
                            if (context.Head.Three == "entry") { return false; }
                            if (context.Head.Three == "oldmanifest") { return false; }
                            if (context.Head.Three.StartsWith("before")) { return true; }
                            if (context.Head.Three.StartsWith("after")) { return true; }
                            if (context.Head.Three == "assumeInvariant") { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool InsideInvariantInMethod
        {
            get
            {
                if (this.Block.Subroutine.IsInvariant)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return true; }
                            if (context.Head.Three == "entry") { return true; }
                            if (context.Head.Three.StartsWith("after")) { return true; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        internal bool InsideInvariantOnExit
        {
            get
            {
                if (this.Block.Subroutine.IsInvariant)
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.Three == "exit") { return true; }
                            if (context.Head.Three == "entry") { return false; }
                            if (context.Head.Three.StartsWith("after")) { return false; }
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        public bool InsideContract
        {
            get
            {
                Subroutine subr = this.Block.Subroutine;
                if (subr.IsContract || subr.IsOldValue) return true;
                return false;
            }
        }

        public bool InsideConstructor
        {
            get
            {
                var context = this.SubroutineContext;
                var current = this.Block;
                while (current != null)
                {
                    Subroutine subr = current.Subroutine;
                    if (subr.IsConstructor) return true;
                    if (subr.IsMethod) return false;
                    if (context != null)
                    {
                        current = context.Head.One;
                        context = context.Tail;
                    }
                    else
                    {
                        current = null;
                    }
                }
                return false;
            }
        }

        public bool TryGetContainingMethod<Method>(out Method method)
        {
            var context = this.SubroutineContext;
            var current = this.Block;
            while (current != null)
            {
                var mi = current.Subroutine as IMethodInfo<Method>;
                if (mi != null)
                {
                    method = mi.Method;
                    return true;
                }
                if (context != null)
                {
                    current = context.Head.One;
                    context = context.Tail;
                }
                else
                {
                    current = null;
                }
            }
            method = default(Method);
            return false;
        }

        public bool TryGetContainingType<Type>(out Type type)
        {
            var context = this.SubroutineContext;
            var current = this.Block;
            while (current != null)
            {
                var mi = current.Subroutine as ITypeInfo<Type>;
                if (mi != null)
                {
                    type = mi.AssociatedType;
                    return true;
                }
                if (context != null)
                {
                    current = context.Head.One;
                    context = context.Tail;
                }
                else
                {
                    current = null;
                }
            }
            type = default(Type);
            return false;
        }

        public bool InsideOldManifestation
        {
            get
            {
                Subroutine subr = this.Block.Subroutine;
                if (subr.IsOldValue)
                {
                    for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                    {
                        if (context.Head.Three == "oldmanifest") return true;
                    }
                }
                return false;
            }
        }

        public bool IsCompilerGenerated
        {
            [ContractVerification(false)]
            get
            {
                if (this.Block.Subroutine.IsCompilerGenerated)
                {
                    return true;
                }
                else
                {
                    if (this.SubroutineContext != null)
                    {
                        for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
                        {
                            if (context.Head.One.Subroutine.IsCompilerGenerated) return true;
                        }
                    }
                }
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is APC)) return false;
            APC that = (APC)obj;

            return this.Index == that.Index && this.Block == that.Block && this.SubroutineContext == that.SubroutineContext;
        }

        public override int GetHashCode()
        {
            return this.Index + this.Block.Index * 10 + this.SubroutineContext.Length() * 100;
        }
    }

    public interface IMethodInfo<M>
    {
        M Method { get; }
    }
    public interface ITypeInfo<Type>
    {
        Type AssociatedType { get; }
    }

    /// <summary>
    /// Represents a region of code that has a single entry and exit, and control flow enters
    /// only at the given entry and exits only at the given exit. Exceptions can escape from any
    /// point however.
    /// We use subroutines to model entire methods, finally and fault regions within methods,
    /// and injected code, such as pre-condition and post-condition evaluations around method calls.
    ///
    /// The ITypedProperties interface provdes a type safe property map on subroutines so that analyses
    /// (e.g., stack depth) can store info on each subroutine
    /// </summary>
    [Serializable]
    public abstract partial class Subroutine : ITypedProperties, IEquatable<Subroutine>
    {
        private static int subroutineIdGen;
        protected readonly int subroutineId = subroutineIdGen++;

        public int Id { get { return this.subroutineId; } }
        public abstract string Kind { get; }
        public abstract CFGBlock Entry { get; }
        public abstract CFGBlock EntryAfterRequires { get; }
        public abstract CFGBlock Exit { get; }
        public abstract CFGBlock ExceptionExit { get; }
        public abstract string Name { get; }
        public abstract bool HasReturnValue { get; }
        /// <summary>
        /// true for pre/post condition subroutines and methods (if they can be inlined), but not for 
        /// fault/finally, as these always have 0 initial stack depth
        /// </summary>
        public abstract bool HasContextDependentStackDepth { get; }

        /// <summary>
        /// Block level normal successors
        /// </summary>
        [Pure]
        public abstract IEnumerable<CFGBlock> SuccessorBlocks(CFGBlock block);

        /// <summary>
        /// Block level normal successors (tagged).
        /// </summary>
        [Pure]
        public IEnumerable<Pair<Tag, CFGBlock>> SuccessorEdgesFor(CFGBlock block)
        {
            return this.SuccessorEdges[block];
        }

        /// <summary>
        /// Block level normal predecessors
        /// </summary>
        [Pure]
        public abstract IEnumerable<CFGBlock> PredecessorBlocks(CFGBlock block);

        /// <summary>
        /// Blocks are ordered from 0 - n, so arrays can be used for lookup maps
        /// </summary>
        public abstract int BlockCount { get; }

        /// <summary>
        /// Block level view of CFG
        /// </summary>
        public abstract IEnumerable<CFGBlock> Blocks { get; }

        [Pure]
        public IEnumerable<Subroutine> UsedSubroutines()
        {
            var alreadyFound = new Set<int>();
            return UsedSubroutines(alreadyFound);
        }

        [Pure]
        public IEnumerable<Subroutine> SubroutineTree()
        {
            var alreadyFound = new Set<int>();
            return SubroutineTree(alreadyFound);
        }

        private IEnumerable<Subroutine> SubroutineTree(Set<int> alreadyFound)
        {
            alreadyFound.Add(this.Id);
            yield return this;

            foreach (var immediate in this.UsedSubroutines(alreadyFound))
            {
                foreach (var sub in immediate.SubroutineTree(alreadyFound))
                {
                    yield return sub;
                }
            }
        }

        internal abstract IEnumerable<Subroutine> UsedSubroutines(IMutableSet<int> alreadySeen);

        [Pure]
        public abstract void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, IMutableSet<Pair<Subroutine, SubroutineContext>> printed);

        /// <summary>
        /// Returns true, if the block starts an exception or filter handler, false otherwise.
        /// NOTE: blocks relating to fault or finally handlers never return true, as these are expanded out in the CFG abstraction
        /// </summary>
        [Pure]
        public virtual bool IsCatchFilterHeader(CFGBlock block) { return false; }
        public virtual bool IsRequires { get { return false; } }
        public virtual bool IsEnsures { get { return false; } }
        public virtual bool IsModelEnsures { get { return false; } }
        public bool IsEnsuresOrOld { get { return IsEnsures || IsOldValue; } }
        public virtual bool IsOldValue { get { return false; } }
        public virtual bool IsMethod { get { return false; } }
        public virtual bool IsConstructor { get { return false; } }
        public virtual bool IsInvariant { get { return false; } }
        public virtual bool IsFaultFinally { get { return false; } }
        public virtual bool IsContract { get { return false; } }
        public bool IsNecessaryAssumption { get; set; }
        [Pure]
        public abstract bool IsSubroutineStart(CFGBlock block);
        [Pure]
        public abstract bool IsSubroutineEnd(CFGBlock block);
        [Pure]
        public abstract bool IsJoinPoint(CFGBlock block);
        [Pure]
        public abstract bool IsSplitPoint(CFGBlock block);
        internal virtual bool IsCompilerGenerated { get { return false; } }

        /// <summary>
        /// Returns the delta of the evaluation stack after evaluating this subroutine.
        /// Usually 0, but subroutines such as old subroutines push 1 value onto the stack.
        /// </summary>
        [Pure]
        public abstract int StackDelta { get; }

        [Pure]
        internal abstract APC ComputeTargetFinallyContext(APC ppoint, CFGBlock succ, DConsCache consCache);

        internal abstract EdgeMap<Tag> SuccessorEdges { get; }
        internal abstract EdgeMap<Tag> PredecessorEdges { get; }
        [Pure]
        internal abstract bool HasSingleSuccessor(APC ppoint, out APC next, DConsCache consCache);
        [Pure]
        internal abstract bool HasSinglePredecessor(APC ppoint, out APC next, DConsCache consCache, bool skipContracts);
        [Pure]
        internal abstract APC PredecessorPCPriorToRequires(APC ppoint, DConsCache consCache);
        [Pure]
        internal abstract IEnumerable<APC> Predecessors(APC ppoint, DConsCache consCache, bool skipContracts);
        [Pure]
        internal abstract IEnumerable<APC> Successors(APC ppoint, DConsCache consCache);
        internal abstract DepthFirst.Visitor<CFGBlock, Unit>/*?*/ EdgeInfo { get; }
        /// <summary>
        /// Enumerates the local exception handler start blocks protecting the given ppoint and surrounding fault/finally subroutines also protecting ppoint. If the exception can escape the current subroutine
        /// then the last enumerated block is the subroutine exception exit block.
        /// </summary>
        /// <typeparam name="Data">Handler predicate specific type</typeparam>
        /// <typeparam name="Type">Type of exceptions</typeparam>
        /// <param name="ppoint">Current point at which exception is thrown</param>
        /// <param name="currentSubroutine">if non-null, a subroutine already executing as a transfer from ppoint. Reduces the set of
        /// handlers to those surrounding the execution of the given subroutine from ppoint.</param>
        /// <param name="data">Handler filter specific data</param>
        /// <param name="handlerPredicate">Called with each potential handler. Can determine if handler is chosen and if search continues</param>
        /// <returns>All selected handlers within the subroutine ppoint.Block.Container.</returns>
        [Pure]
        internal abstract IEnumerable<CFGBlock> ExceptionHandlers<Data, Type>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<Type, Data> handlerPredicate);
        public abstract void AddEdgeSubroutine(CFGBlock from, CFGBlock to, Subroutine subroutine, string callTag);

        /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
        [Pure]
        public abstract FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context);

        [Pure]
        public abstract FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context);

        /// <summary>
        /// The constructor only builds the subroutine, but does not really traverse all the code to avoid 
        /// infinite recursions in the cache lookup.
        /// This method is called by the cache constructor after the subroutine is entered into the cache.
        /// </summary>
        internal abstract void Initialize();

        #region ITypedProperties Members

        private readonly TypedProperties propertyMap = new TypedProperties();

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(propertyMap != null);
        }

        [Pure]
        public bool Contains<T>(TypedKey<T> key)
        {
            return propertyMap.Contains(key);
        }

        public void Add<T>(TypedKey<T> key, T value)
        {
            propertyMap.Add(key, value);
        }

        [Pure]
        public bool TryGetValue<T>(TypedKey<T> key, out T value)
        {
            return propertyMap.TryGetValue(key, out value);
        }

        #endregion

        [Pure]
        internal static int GetKey(Subroutine s)
        {
            Contract.Requires(s != null);
            return s.Id;
        }


        #region IEquatable<Subroutine> Members

        public bool Equals(Subroutine that)
        {
            return this == that;
        }

        #endregion
    }

    #region Subroutine contract binding
    [ContractClass(typeof(SubroutineContract))]
    public abstract partial class Subroutine { }

    [ContractClassFor(typeof(Subroutine))]
    internal abstract class SubroutineContract : Subroutine
    {
        public override Tag Kind
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
        }

        public override CFGBlock Entry
        {
            get
            {
                Contract.Ensures(Contract.Result<CFGBlock>() != null);
                throw new NotImplementedException();
            }
        }

        public override CFGBlock EntryAfterRequires
        {
            get
            {
                Contract.Ensures(Contract.Result<CFGBlock>() != null);
                throw new NotImplementedException();
            }
        }

        public override CFGBlock Exit
        {
            get
            {
                Contract.Ensures(Contract.Result<CFGBlock>() != null);
                throw new NotImplementedException();
            }
        }

        public override CFGBlock ExceptionExit
        {
            get
            {
                Contract.Ensures(Contract.Result<CFGBlock>() != null);
                throw new NotImplementedException();
            }
        }

        public override Tag Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
        }

        public override bool HasReturnValue
        {
            get { throw new NotImplementedException(); }
        }

        public override bool HasContextDependentStackDepth
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<CFGBlock> SuccessorBlocks(CFGBlock block)
        {
            Contract.Ensures(Contract.Result<IEnumerable<CFGBlock>>() != null);
            throw new NotImplementedException();
        }

        public override IEnumerable<CFGBlock> PredecessorBlocks(CFGBlock block)
        {
            Contract.Ensures(Contract.Result<IEnumerable<CFGBlock>>() != null);
            throw new NotImplementedException();
        }

        public override int BlockCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<CFGBlock> Blocks
        {
            get { throw new NotImplementedException(); }
        }

        internal override IEnumerable<Subroutine> UsedSubroutines(IMutableSet<int> alreadySeen)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Subroutine>>() != null);
            throw new NotImplementedException();
        }

        public override void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, IMutableSet<Pair<Subroutine, SubroutineContext>> printed)
        {
            throw new NotImplementedException();
        }

        public override bool IsSubroutineStart(CFGBlock block)
        {
            throw new NotImplementedException();
        }

        public override bool IsSubroutineEnd(CFGBlock block)
        {
            throw new NotImplementedException();
        }

        public override bool IsJoinPoint(CFGBlock block)
        {
            throw new NotImplementedException();
        }

        public override bool IsSplitPoint(CFGBlock block)
        {
            throw new NotImplementedException();
        }

        public override int StackDelta
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() != Int32.MinValue);

                throw new NotImplementedException();
            }
        }

        internal override APC ComputeTargetFinallyContext(APC ppoint, CFGBlock succ, DConsCache consCache)
        {
            Contract.Requires(consCache != null);

            return default(APC);
        }

        internal override EdgeMap<Tag> SuccessorEdges
        {
            get
            {
                Contract.Ensures(Contract.Result<EdgeMap<Tag>>() != null);
                throw new NotImplementedException();
            }
        }

        internal override EdgeMap<Tag> PredecessorEdges
        {
            get
            {
                Contract.Ensures(Contract.Result<EdgeMap<Tag>>() != null);
                throw new NotImplementedException();
            }
        }

        internal override bool HasSingleSuccessor(APC ppoint, out APC next, DConsCache consCache)
        {
            Contract.Requires(consCache != null);

            throw new NotImplementedException();
        }

        internal override bool HasSinglePredecessor(APC ppoint, out APC next, DConsCache consCache, bool skipContracts)
        {
            Contract.Requires(consCache != null);

            throw new NotImplementedException();
        }

        internal override APC PredecessorPCPriorToRequires(APC ppoint, DConsCache consCache)
        {
            Contract.Requires(consCache != null);

            throw new NotImplementedException();
        }

        internal override IEnumerable<APC> Predecessors(APC ppoint, DConsCache consCache, bool skipContracts)
        {
            Contract.Requires(consCache != null);
            Contract.Ensures(Contract.Result<IEnumerable<APC>>() != null);

            throw new NotImplementedException();
        }

        internal override IEnumerable<APC> Successors(APC ppoint, DConsCache consCache)
        {
            Contract.Requires(consCache != null);
            Contract.Ensures(Contract.Result<IEnumerable<APC>>() != null);
            throw new NotImplementedException();
        }

        internal override DepthFirst.Visitor<CFGBlock, UnitDest> EdgeInfo
        {
            get
            {
                Contract.Ensures(Contract.Result<DepthFirst.Visitor<CFGBlock, UnitDest>>() != null);

                throw new NotImplementedException();
            }
        }

        internal override IEnumerable<CFGBlock> ExceptionHandlers<Data, Type>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<Type, Data> handlerPredicate)
        {
            Contract.Ensures(Contract.Result<IEnumerable<CFGBlock>>() != null);
            throw new NotImplementedException();
        }

        public override void AddEdgeSubroutine(CFGBlock from, CFGBlock to, Subroutine subroutine, Tag callTag)
        {
            throw new NotImplementedException();
        }

        public override FList<Pair<Tag, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context)
        {
            throw new NotImplementedException();
        }

        public override FList<Pair<Tag, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context)
        {
            throw new NotImplementedException();
        }

        internal override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    internal delegate SubroutineContext DConsCache(SubroutineEdge edge, SubroutineContext/*?*/ tail);

    [ContractClass(typeof(CFGBlockContracts))]
    [Serializable]
    public abstract class CFGBlock : IEquatable<CFGBlock>
    {
        #region Private/Internal
        private int id;
        private int reversePostOrder;
        private readonly Subroutine subroutine;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(subroutine != null);
            Contract.Invariant(id >= 0); // F: I am guessing this invariant should hold
        }

        internal void SetReversePostOrderIndex(int index)
        {
            reversePostOrder = index;
        }

        protected CFGBlock(Subroutine container, ref int idGen)
        {
            Contract.Requires(container != null);
            Contract.Requires(idGen >= 0); // Clousot suggestion

            Contract.Ensures(idGen >= 0);
            Contract.Ensures(Contract.OldValue(idGen) + 1 == idGen);

            id = idGen++;
            subroutine = container;
        }

        internal void Renumber(ref int idGen)
        {
            Contract.Requires(idGen >= 0); // Clousot suggestion

            id = idGen++;
        }


        #endregion

        public int Index
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return id;
            }
        }

        /// <summary>
        /// Index in reverse post order. This is the preferred order for forward data flow analysis
        /// </summary>
        public int ReversePostOrderIndex { get { return reversePostOrder; } }

        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Returns the list of APCs represented by this block (without finally contexts)
        /// </summary>
        public IEnumerable<APC> APCs()
        {
            return APCs(null);
        }

        /// <summary>
        /// Returns the list of APCs represented by this block (without finally contexts)
        /// </summary>
        public IEnumerable<APC> APCs(SubroutineContext context)
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return new APC(this, i, context);
            }
        }


        public APC First
        {
            get { return new APC(this, 0, null); }
        }

        /// <summary>
        /// Returns program point after last statement in this block
        /// </summary>
        public APC Last
        {
            get
            {
                return APC.ForEnd(this, null);
            }
        }

        /// <summary>
        /// Returns program point prior to last statement in this block
        /// </summary>
        public APC PriorLast
        {
            get
            {
                int priorToLast = this.Count - 1;
                if (priorToLast < 0) { priorToLast = 0; }
                return new APC(this, priorToLast, null);
            }
        }

        public abstract int ILOffset(APC pc);

        public abstract string SourceContext(APC pc);
        public abstract string SourceDocument(APC pc);
        public abstract int SourceStartLine(APC pc);
        public abstract int SourceEndLine(APC pc);
        public abstract int SourceStartColumn(APC pc);
        public abstract int SourceEndColumn(APC pc);
        public abstract int SourceStartIndex(APC pc);
        public abstract int SourceLength(APC pc);
        public abstract string SourceAssertionCondition(APC pc);

        public Subroutine Subroutine
        {
            get
            {
                Contract.Ensures(Contract.Result<Subroutine>() != null);
                return subroutine;
            }
        }

        /// <summary>
        /// Returns true if the block is a method call block or newObj call block
        /// </summary>
        /// <param name="parameterCount">on success holds the parameter count of the called method (not including "this")</param>
        public virtual bool IsMethodCallBlock<Method>(out Method calledMethod, out bool isNewObj, out bool isVirtual)
        {
            calledMethod = default(Method);
            isNewObj = false;
            isVirtual = false;
            return false;
        }

        #region IEquatable<CFGBlock> Members

        public bool Equals(CFGBlock other)
        {
            return id == other.id;
        }

        #endregion
    }

    #region Contracts for CFGBlock

    [ContractClassFor(typeof(CFGBlock))]
    internal abstract class CFGBlockContracts : CFGBlock
    {
        // To please C#
        private CFGBlockContracts(int r)
          : base(null, ref r)
        { }

        public override int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return 0;
            }
        }

        public override int ILOffset(APC pc)
        {
            throw new NotImplementedException();
        }

        public override Tag SourceContext(APC pc)
        {
            throw new NotImplementedException();
        }

        public override Tag SourceDocument(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceStartLine(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceEndLine(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceStartColumn(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceEndColumn(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceStartIndex(APC pc)
        {
            throw new NotImplementedException();
        }

        public override int SourceLength(APC pc)
        {
            throw new NotImplementedException();
        }

        public override Tag SourceAssertionCondition(APC pc)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
    /// <summary>
    /// Caches method CFGs, pre and post condition subroutines
    /// </summary>
    public class MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly>
     : IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Subroutine>
    {
        #region ------------ Fields -------------

        public readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetadataDecoder;
        public readonly IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder;

        private readonly Dictionary<Method, ControlFlow<Method, Type>> methodCache = new Dictionary<Method, ControlFlow<Method, Type>>();
        private readonly Dictionary<Method, Set<Field>> methodModifies = new Dictionary<Method, Set<Field>>();
        private readonly Dictionary<Method, Set<Field>> methodReads = new Dictionary<Method, Set<Field>>();
        private readonly Dictionary<Field, Set<Method>> propertyReads = new Dictionary<Field, Set<Method>>();
        private readonly Dictionary<Method, bool> methodWitnesses; // TODO: Make it more generic
        private readonly InvariantCache invariantCache;
        private readonly RequiresCache requiresCache;
        private readonly EnsuresCache ensuresCache;
        private readonly ModelEnsuresCache modelEnsuresCache;
        private readonly AssumeCache assumeCache;
        /// <summary>
        /// Used to allow inserting the same invariant twice on entry around a requires. The second one has to be wrapped
        /// so the control flow logic for edge subroutines does not get confused (can't have the same subroutine twice on an edge).
        /// </summary>
        private readonly Dictionary<Subroutine, Subroutine> redundantInvariants = new Dictionary<Subroutine, Subroutine>();

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(methodCache != null);
            Contract.Invariant(methodModifies != null);
            Contract.Invariant(methodReads != null);
            Contract.Invariant(methodWitnesses != null);
            Contract.Invariant(propertyReads != null);
            Contract.Invariant(invariantCache != null);
            Contract.Invariant(modelEnsuresCache != null);
            Contract.Invariant(ensuresCache != null);
            Contract.Invariant(requiresCache != null);
            Contract.Invariant(MetadataDecoder != null);
            Contract.Invariant(ContractDecoder != null);
            Contract.Invariant(redundantInvariants != null);
            Contract.Invariant(assumeCache != null);
        }

        #endregion

        public MethodCache(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
                           IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
                           ErrorHandler output
                          )
        {
            Contract.Requires(mdDecoder != null);
            Contract.Requires(contractDecoder != null);

            this.MetadataDecoder = mdDecoder;
            this.ContractDecoder = contractDecoder;
            methodWitnesses = new Dictionary<Method, bool>();
            requiresCache = new RequiresCache(this, output);
            ensuresCache = new EnsuresCache(this);
            modelEnsuresCache = new ModelEnsuresCache(this);
            invariantCache = new InvariantCache(this);
            assumeCache = new AssumeCache(this);
        }

        [ContractVerification(false)]
        public ICFG GetCFG(Method method)
        {
            Contract.Ensures(Contract.Result<ICFG>() != null);

            if (methodCache.ContainsKey(method))
            {
                return methodCache[method].AssumeNotNull();
            }
            ControlFlow<Method, Type> result;
            if (MetadataDecoder.HasBody(method))
            {
                Subroutine sb = MetadataDecoder.AccessMethodBody(method, this, Unit.Value);

                Contract.Assume(sb != null, " Axiom HasBody(method) ==> AccesMethodBody(method ,... ) != null");
                result = new ControlFlow<Method, Type>(sb, this);
            }
            else
            {
                throw new InvalidOperationException("Method has no body");
            }
            //methodCache.Add(method, result);
            return result;
        }

        /// <summary>
        /// Returns null if method has no requires (and no inherited requires)
        /// </summary>
        internal Subroutine GetRequires(Method method)
        {
            // Experimental
            //   To get contracts on generic/specialized method instances, let's grab the unspecialized method
            //   here.
            method = this.MetadataDecoder.Unspecialized(method);
            // End Experimental
            return requiresCache.Get(method);
        }


        /// <summary>
        /// Returns null if method has no ensures (and no inherited ensures)
        /// </summary>
        internal Subroutine GetEnsures(Method method)
        {
            // Experimental
            //   To get contracts on generi/specialized method instances, let's grab the unspecialized method
            //   here.
            method = this.MetadataDecoder.Unspecialized(method);
            // End Experimental
            return ensuresCache.Get(method);
        }

        /// <summary>
        /// Returns null if method has no model ensures (and no inherited model ensures)
        /// </summary>
        internal Subroutine GetModelEnsures(Method method)
        {
            // Experimental
            //   To get contracts on generic/specialized method instances, let's grab the unspecialized method
            //   here.
            method = this.MetadataDecoder.Unspecialized(method);
            // End Experimental
            return modelEnsuresCache.Get(method);
        }

        /// <summary>
        /// Returns null if no invariant is specified
        /// </summary>
        public Subroutine GetInvariant(Type type)
        {
            // we have to make sure we are not confused by the particular instantiation of the type
            type = this.MetadataDecoder.Unspecialized(type);
            return invariantCache.Get(type);
        }

        [ContractVerification(false)]
        public Subroutine GetRedundantInvariant(Subroutine existingInvariant, Type type)
        {
            Subroutine result;
            if (redundantInvariants.TryGetValue(existingInvariant, out result))
            {
                return result;
            }
            result = new InvariantSubroutine<Unit>(this, existingInvariant, type);
            redundantInvariants.Add(existingInvariant, result);
            return result;
        }

        #region Internal Access to Forward decoding
        internal Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>
        {
            BlockBase block = pc.Block as BlockBase;
            if (block != null)
            {
                return block.ForwardDecode<Data, Result, Visitor>(pc, visitor, data);
            }
            return visitor.Nop(pc, data);
        }
        #endregion

        public Result DecodeForReturnValue<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>
        {
            BlockBase block = pc.Block as BlockBase;
            if (block != null)
            {
                return block.ForwardDecode<Data, Result, Visitor>(pc, visitor, data);
            }
            return visitor.Nop(pc, data);
        }

        #region IMethodCodeConsumer<Local,Parameter,Method,Field,Type,Unit,Unit> Members

        Subroutine IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Subroutine>.Accept<Label, Handler>(
          IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider,
          Label entryPoint,
          Method method,
          Unit data)
        {
            SubroutineWithHandlersBuilder<Label, Handler> sb = new SubroutineWithHandlersBuilder<Label, Handler>(codeProvider, this, method, entryPoint);
            return new MethodSubroutine<Label, Handler>(this, method, sb, entryPoint);
        }

        #endregion

        #region ----------- Nested Types -------------

        private abstract class SubroutineFactory<Key, Data> : ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Subroutine>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(cache != null); // F: Added as of precondition suggestions ion many places
                Contract.Invariant(this.MethodCache != null);// F: Added as of precondition suggestions ion many places
            }

            private readonly Dictionary<Key, Subroutine> cache = new Dictionary<Key, Subroutine>();
            protected readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache;
            protected IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.MethodCache.ContractDecoder; } }
            protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetadataDecoder
            {
                get
                {
                    Contract.Assume(this.MethodCache.MetadataDecoder != null, "Should be made a postcondition");
                    return this.MethodCache.MetadataDecoder;
                }
            }

            public SubroutineFactory(
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache
            )
            {
                Contract.Requires(methodCache != null);

                this.MethodCache = methodCache;
            }

            [ContractVerification(false)]
            public Subroutine Get(Key key)
            {
                if (cache.ContainsKey(key))
                {
                    return cache[key];
                }
                Subroutine result = this.BuildNewSubroutine(key);
                cache.Add(key, result);
                // Perform the initialization after adding it to the cache due to recursion
                if (result != null) result.Initialize();
                return result;
            }

            [ContractVerification(false)]
            public void Install(Key key, Subroutine sr)
            {
                if (cache.ContainsKey(key))
                {
                    Contract.Assume(cache[key] == null);
                }
                cache[key] = sr;
            }

            [ContractVerification(false)]
            public bool Remove(Key key)
            {
                if (cache.ContainsKey(key))
                {
                    cache.Remove(key);

                    return true;
                }
                return false;
            }

            protected abstract Subroutine BuildNewSubroutine(Key key);

            protected abstract Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Data data);

            #region ICodeConsumer<Local,Parameter,Method,Field,Type,Unit,Subroutine> Members

            public Subroutine Accept<Label>(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
                                            Label entryPoint, Data data)
            {
                SimpleSubroutineBuilder<Label> sb = new SimpleSubroutineBuilder<Label>(codeProvider, this.MethodCache, entryPoint);

                Subroutine subr = Factory(sb, entryPoint, data);

                return subr;
            }

            #endregion
        }

        private class EnsuresCache : SubroutineFactory<Method, Pair<Method, IFunctionalSet<Subroutine>>>
        {
            private Method lastMethodWeAddedInferredEnsures;
            private Set<string> lastMethodInferredEnsures;

            public EnsuresCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion
            }

            public bool AlreadyInferred(Method method, string postCondition)
            {
                if (!MetadataDecoder.Equal(method, lastMethodWeAddedInferredEnsures))
                {
                    lastMethodWeAddedInferredEnsures = method;
                    lastMethodInferredEnsures = new Set<string>();
                }

                return !lastMethodInferredEnsures.AddQ(postCondition);
            }

            protected override Subroutine BuildNewSubroutine(Method method)
            {
                if (this.ContractDecoder != null)
                {
                    IFunctionalSet<Subroutine> inheritedEnsures = this.GetInheritedEnsures(method);
                    if (this.ContractDecoder.HasEnsures(method))
                    {
                        return this.ContractDecoder.AccessEnsures(method, this, new Pair<Method, IFunctionalSet<Subroutine>>(method, inheritedEnsures));
                    }
                    else if (inheritedEnsures.Count > 0)
                    {
                        if (inheritedEnsures.Count > 1)
                        {
                            // make up a label
                            return new EnsuresSubroutine<Unit>(this.MethodCache, method, inheritedEnsures);
                        }
                        else
                        {
                            return inheritedEnsures.Any;
                        }
                    }
                }
                // make up an empty Ensures for every method.
                return new EnsuresSubroutine<Unit>(this.MethodCache, method, null);
            }

            protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method, IFunctionalSet<Subroutine>> data)
            {
                return new EnsuresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
            }

            private IFunctionalSet<Subroutine> GetInheritedEnsures(Method method)
            {
                IFunctionalSet<Subroutine> result = FunctionalSet<Subroutine>.Empty(Subroutine.GetKey);
                if (MetadataDecoder.IsVirtual(method) && ContractDecoder.CanInheritContracts(method))
                {
                    foreach (Method baseMethod in MetadataDecoder.OverriddenAndImplementedMethods(method).AssumeNotNull())
                    {
                        Subroutine rs = this.Get(MetadataDecoder.Unspecialized(baseMethod));
                        if (rs != null)
                        {
                            result = result.Add(rs);
                        }
                    }
                }
                return result;
            }
        }

        private class ModelEnsuresCache : SubroutineFactory<Method, Pair<Method, IFunctionalSet<Subroutine>>>
        {
            public ModelEnsuresCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion
            }


            protected override Subroutine BuildNewSubroutine(Method method)
            {
                if (this.ContractDecoder != null)
                {
                    IFunctionalSet<Subroutine> inheritedEnsures = this.GetInheritedEnsures(method);
                    if (this.ContractDecoder.HasModelEnsures(method))
                    {
                        return this.ContractDecoder.AccessModelEnsures(method, this, new Pair<Method, IFunctionalSet<Subroutine>>(method, inheritedEnsures));
                    }
                    else if (inheritedEnsures.Count > 0)
                    {
                        if (inheritedEnsures.Count > 1)
                        {
                            // make up a label
                            return new ModelEnsuresSubroutine<Unit>(this.MethodCache, method, inheritedEnsures);
                        }
                        else
                        {
                            return inheritedEnsures.Any;
                        }
                    }
                }
                return null;
            }

            protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method, IFunctionalSet<Subroutine>> data)
            {
                return new ModelEnsuresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
            }

            private IFunctionalSet<Subroutine> GetInheritedEnsures(Method method)
            {
                IFunctionalSet<Subroutine> result = FunctionalSet<Subroutine>.Empty(Subroutine.GetKey);
                if (MetadataDecoder.IsVirtual(method) && ContractDecoder.CanInheritContracts(method))
                {
                    foreach (Method baseMethod in MetadataDecoder.OverriddenAndImplementedMethods(method).AssumeNotNull())
                    {
                        Subroutine rs = this.Get(MetadataDecoder.Unspecialized(baseMethod));
                        if (rs != null)
                        {
                            result = result.Add(rs);
                        }
                    }
                }
                return result;
            }
        }
        private class RequiresCache : SubroutineFactory<Method, Pair<Method, IFunctionalSet<Subroutine>>>
        {
            // for issuing warnings about inheritance of multiple requires
            private ErrorHandler output;
            // for inferred requires

            public RequiresCache(
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              ErrorHandler output
            )
              : base(methodCache)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion
                this.output = output;
            }


            protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method, IFunctionalSet<Subroutine>> data)
            {
                return new RequiresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
            }

            protected override Subroutine BuildNewSubroutine(Method method)
            {
                if (this.ContractDecoder != null)
                {
                    IFunctionalSet<Subroutine> inheritedRequires = this.GetInheritedRequires(method);
                    if (this.ContractDecoder.HasRequires(method))
                    {
                        return this.ContractDecoder.AccessRequires(method, this, new Pair<Method, IFunctionalSet<Subroutine>>(method, inheritedRequires));
                    }
                    else if (inheritedRequires.Count > 0)
                    {
                        if (inheritedRequires.Count == 1)
                        {
                            return inheritedRequires.Any;
                        }
                        return new RequiresSubroutine<Unit>(this.MethodCache, method, inheritedRequires);
                    }
                }
                return null;
            }

            private IFunctionalSet<Subroutine> GetInheritedRequires(Method method)
            {
                Contract.Ensures(Contract.Result<IFunctionalSet<Subroutine>>() != null);

                IFunctionalSet<Subroutine> result = FunctionalSet<Subroutine>.Empty(Subroutine.GetKey);
                Contract.Assert(result != null, "Let's make sure we have the contract");
                Contract.Assert(this.MetadataDecoder != null, "Should be a postcondition");
                if (this.MetadataDecoder.IsVirtual(method) && ContractDecoder.CanInheritContracts(method))
                {
                    Method rootMethod;
                    if (this.MetadataDecoder.TryGetRootMethod(method, out rootMethod))
                    {
                        Subroutine rs = this.Get(this.MetadataDecoder.Unspecialized(rootMethod));
                        if (rs != null)
                        {
                            result = result.Add(rs);
                        }
                    }
                    foreach (Method baseMethod in this.MetadataDecoder.ImplementedMethods(method).AssumeNotNull())
                    {
                        var unspecBaseMethod = this.MetadataDecoder.Unspecialized(baseMethod);
                        Subroutine rs = this.Get(this.MetadataDecoder.Unspecialized(baseMethod));
                        if (rs != null)
                        {
                            result = result.Add(rs);
                        }
                    }
                }
                return result;
            }
        }


        private class AssumeCache : SubroutineFactory<Method, Pair<Method, IFunctionalSet<Subroutine>>>
        {
            public AssumeCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null);
                //Contract.Assert(false, "assume cache unimplemented");
            }

            [ContractVerification(false)]
            protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method, IFunctionalSet<Subroutine>> data)
            {
                Contract.Assert(false, "untested/wrong");

                return new RequiresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
            }

            [ContractVerification(false)]
            protected override Subroutine BuildNewSubroutine(Method method)
            {
                Contract.Assert(false, "untested/wrong");
                if (this.ContractDecoder != null)
                {
                    IFunctionalSet<Subroutine> inheritedRequires = null;//this.GetInheritedRequires(method);
                    if (this.ContractDecoder.HasRequires(method))
                    {
                        return this.ContractDecoder.AccessRequires(method, this, new Pair<Method, IFunctionalSet<Subroutine>>(method, inheritedRequires));
                    }
                    else if (inheritedRequires.Count > 0)
                    {
                        if (inheritedRequires.Count == 1)
                        {
                            return inheritedRequires.Any;
                        }
                        return new RequiresSubroutine<Unit>(this.MethodCache, method, inheritedRequires);
                    }
                }
                return null;
            }
        }

        private class InvariantCache : SubroutineFactory<Type, Pair<Type, Subroutine>>
        {
            public InvariantCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion
            }

            protected override Subroutine BuildNewSubroutine(Type type)
            {
                if (this.ContractDecoder != null)
                {
                    Subroutine baseInv = GetInheritedInvariant(type);

                    if (this.ContractDecoder.HasInvariant(type))
                    {
                        return this.ContractDecoder.AccessInvariant(type, this, new Pair<Type, Subroutine>(type, baseInv));
                    }
                    else
                    {
                        return baseInv;
                    }
                }
                else
                {
                    return null;
                }
            }

            protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Type, Subroutine> data)
            {
                var baseInv = data.Two;
                var typekey = data.One;
                return new InvariantSubroutine<Label>(MethodCache, sb, entry, baseInv, typekey);
            }


            private Subroutine GetInheritedInvariant(Type type)
            {
                if (this.MetadataDecoder.HasBaseClass(type) && this.ContractDecoder.CanInheritContracts(type))
                {
                    Type baseClass = this.MetadataDecoder.BaseClass(MetadataDecoder.Unspecialized(type));
                    Subroutine baseInv = this.MethodCache.GetInvariant(baseClass);
                    return baseInv;
                }
                return null;
            }
        }



        /// <summary>
        /// This data structure contains data used during the construction of a Subroutine. For methods, this structure
        /// is used to hold handler information and is shared among multiple subroutine, namely the method subroutine and
        /// all the fault/finally subroutine within it.
        /// </summary>
        internal abstract class SubroutineBuilder<Label>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(targetLabels != null);// F: added as of many Clousot precondition suggestions and code inspection
                Contract.Invariant(labelsStartingBlocks != null); // F: added as of many Clousot precondition suggestions and code inspection
                Contract.Invariant(this.MethodCache != null);// F: added as of many Clousot precondition suggestions and code inspection
            }

            #region Data
            internal readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache;
            private readonly Set<Label> labelsStartingBlocks = new Set<Label>();
            private readonly Set<Label> targetLabels = new Set<Label>();
            private OnDemandMap<Label, Pair<Method, bool>> labelsForCallSites;
            private OnDemandMap<Label, Method> labelsForNewObjSites;

            internal readonly ICodeProvider<Label, Local, Parameter, Method, Field, Type> CodeProvider;
            internal IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetadataDecoder
            {
                get
                {
                    Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);

                    return this.MethodCache.MetadataDecoder.AssumeNotNull();
                }
            }
            internal IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.MethodCache.ContractDecoder; } }
            #endregion

            protected SubroutineBuilder(
              ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              Label entry
            )
            {
                Contract.Requires(methodCache != null);

                this.CodeProvider = codeProvider;
                this.MethodCache = methodCache;
                this.AddTargetLabel(entry);
            }

            protected void AddTargetLabel(Label target)
            {
                this.AddBlockStart(target);
                targetLabels.Add(target);
            }

            protected void AddBlockStart(Label target)
            {
                labelsStartingBlocks.Add(target);
            }


            protected void Initialize(Label entry)
            {
                new BlockStartGatherer(this).TraceAggregateSequentially(entry);
            }

            protected abstract SubroutineBase<Label> CurrentSubroutine { get; }

            public bool IsBlockStart(Label label)
            {
                return labelsStartingBlocks.Contains(label);
            }

            internal virtual void RecordBlockInfoSameAsOtherBlock(BlockWithLabels<Label> ab, BlockWithLabels<Label> otherblock)
            {
            }

            internal bool IsMethodCallSite(Label label, out Pair<Method, bool> methodVirtPair)
            {
                return labelsForCallSites.TryGetValue(label, out methodVirtPair);
            }

            internal bool IsNewObjSite(Label label, out Method constructor)
            {
                return labelsForNewObjSites.TryGetValue(label, out constructor);
            }

            /// <summary>
            /// Returns the last block
            /// </summary>
            protected BlockWithLabels<Label> BuildBlocks(Label start)
            {
                return BlockBuilder.BuildBlocks(start, this);
            }

            protected BlockWithLabels<Label> BuildBlocksWithFixedStartBlock(Label lbl, BlockWithLabels<Label> startBlk)
            {
                return BlockBuilder.BuildBlocksWithFixedStartBlock(lbl, startBlk, this);
            }

            protected virtual BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label>/*?*/ previousBlock)
            {
                Contract.Ensures(Contract.Result<BlockWithLabels<Label>>() != null);

                Contract.Assume(CurrentSubroutine != null, "Made an Assume, but should it be a postcondition?");

                BlockWithLabels<Label> newBlock = CurrentSubroutine.GetBlock(currentLabel);

                if (previousBlock != null)
                {
                    var postConditionTarget = newBlock;
                    var preConditionSource = previousBlock;
                    // - if we have 2 call blocks back to back, we insert a dummy block in the middle.
                    if (newBlock is MethodCallBlock<Label> && previousBlock is MethodCallBlock<Label>)
                    {
                        var intermediateBlock = this.CurrentSubroutine.NewBlock();
                        this.RecordBlockInfoSameAsOtherBlock(intermediateBlock, previousBlock);
                        postConditionTarget = intermediateBlock;
                        preConditionSource = intermediateBlock;
                        CurrentSubroutine.AddSuccessor(previousBlock, "fallthrough", intermediateBlock);
                        CurrentSubroutine.AddSuccessor(intermediateBlock, "fallthrough", newBlock);
                    }
                    else
                    {
                        CurrentSubroutine.AddSuccessor(previousBlock, "fallthrough", newBlock);
                    }
                    // insert pre/post

                    // make sure to insert post condition first, as there may be a post from a previous call, followed by a pre from the subsequent call.
                    //
                    InsertPostConditionEdges(previousBlock, postConditionTarget);
                    InsertPreConditionEdges(preConditionSource, newBlock);
                }
                return newBlock;
            }

            private void InsertPreConditionEdges(BlockWithLabels<Label> previousBlock, BlockWithLabels<Label> newBlock)
            {
                InsertPreConditionEdges(previousBlock, newBlock, this.CurrentSubroutine);
            }

            internal void InsertPreConditionEdges(BlockWithLabels<Label> previousBlock, BlockWithLabels<Label> newBlock, Subroutine subroutine)
            {
                MethodCallBlock<Label> newCallBlock = newBlock as MethodCallBlock<Label>;
                if (newCallBlock != null && !(subroutine.IsContract || subroutine.IsOldValue))
                {
                    if (subroutine.IsMethod)
                    {
                        IMethodInfo<Method> mi = subroutine as IMethodInfo<Method>;
                        if (mi != null && this.MetadataDecoder.IsConstructor(mi.Method))
                        {
                            if (this.MetadataDecoder.IsPropertySetter(newCallBlock.CalledMethod) && this.MetadataDecoder.IsAutoPropertyMember(newCallBlock.CalledMethod))
                            {
                                return; // don't insert preconditions of auto-prop setters within constructors
                            }
                        }
                    }
                    // insert pre-condition
                    string tag = newCallBlock.IsNewObj ? "beforeNewObj" : "beforeCall";
                    subroutine.AddEdgeSubroutine(previousBlock, newBlock, this.MethodCache.GetRequires(newCallBlock.CalledMethod), tag);
                }
            }

            private void InsertPostConditionEdges(BlockWithLabels<Label> previousBlock, BlockWithLabels<Label> newBlock)
            {
                MethodCallBlock<Label> previousCallBlock = previousBlock as MethodCallBlock<Label>;
                if (previousCallBlock != null)
                {
                    Contract.Assume(this.CurrentSubroutine != null);
                    if (this.CurrentSubroutine.IsMethod)
                    {
                        IMethodInfo<Method> mi = this.CurrentSubroutine as IMethodInfo<Method>;
                        if (mi != null && this.MetadataDecoder.IsConstructor(mi.Method))
                        {
                            if (this.MetadataDecoder.IsPropertyGetter(previousCallBlock.CalledMethod) && this.MetadataDecoder.IsAutoPropertyMember(previousCallBlock.CalledMethod))
                            {
                                return; // don't insert postconditions of auto-prop getters within constructors
                            }
                        }
                    }
                    string tag = previousCallBlock.IsNewObj ? "afterNewObj" : "afterCall";
                    // insert post-conditions
                    Subroutine post = this.MethodCache.GetEnsures(previousCallBlock.CalledMethod);
                    Contract.Assume(this.CurrentSubroutine != null);
                    CurrentSubroutine.AddEdgeSubroutine(previousBlock, newBlock, post, tag);
                    // insert model post-conditions
                    Subroutine modelpost = this.MethodCache.GetModelEnsures(previousCallBlock.CalledMethod);
                    CurrentSubroutine.AddEdgeSubroutine(previousBlock, newBlock, modelpost, tag);
                    // NOTE: invariants are inserted on-demand when we know receiver is "this"
                }
            }

            private class BlockStartGatherer :
               MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, bool>,
               ICodeQuery<Label, Local, Parameter, Method, Field, Type, Unit, bool>
            {
                [ContractInvariantMethod]
                private void ObjectInvariant()
                {
                    Contract.Invariant(parent != null); // F: Added as of Clousot suggestions in many places of preconditions
                }

                private SubroutineBuilder<Label> parent;

                public BlockStartGatherer(SubroutineBuilder<Label> parent)
                {
                    Contract.Requires(parent != null);// F: Added as of Clousot suggestion

                    this.parent = parent;
                }

                /// <returns>true, if next label starts a new block</returns>
                public bool TraceAggregateSequentially(Label current)
                {
                    bool nextInstructionStartsNewBlock = false;
                    bool more = true;
                    do
                    {
                        Contract.Assume(parent.CodeProvider != null);
                        nextInstructionStartsNewBlock = parent.CodeProvider.Decode<BlockStartGatherer, Unit, bool>(current, this, Unit.Value);

                        more = parent.CodeProvider.Next(current, out current);
                        if (more && nextInstructionStartsNewBlock)
                        {
                            this.AddBlockStart(current);
                        }
                    }
                    while (more);
                    return nextInstructionStartsNewBlock;
                }

                private void AddBlockStart(Label target) { parent.AddBlockStart(target); }
                private void AddTargetLabel(Label target) { parent.AddTargetLabel(target); }

                /// <summary>
                /// Next instruction does NOT start a new block
                /// </summary>
                protected override bool Default(Label pc, Unit data)
                {
                    return false;
                }

                public override bool Branch(Label pc, Label target, bool leave, Unit data)
                {
                    this.AddTargetLabel(target);
                    return true; // next instruction starts a new block
                }

                public override bool BranchCond(Label pc, Label target, BranchOperator bop, Unit value1, Unit value2, Unit data)
                {
                    this.AddTargetLabel(target);
                    return true; // next instruction starts a new block
                }

                public override bool BranchFalse(Label pc, Label target, Unit cond, Unit data)
                {
                    this.AddTargetLabel(target);
                    return true; // next instruction starts a new block
                }

                public override bool BranchTrue(Label pc, Label target, Unit cond, Unit data)
                {
                    this.AddTargetLabel(target);
                    return true; // next instruction starts a new block
                }

                public override bool Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, Unit value, Unit data)
                {
                    foreach (var label in cases)
                    {
                        this.AddTargetLabel(label.Two);
                    }
                    return true;
                }

                public override bool Throw(Label pc, Unit exn, Unit data)
                {
                    return true;
                }
                public override bool Rethrow(Label pc, Unit data)
                {
                    return true;
                }
                public override bool Endfinally(Label pc, Unit data)
                {
                    return true;
                }
                public override bool Return(Label pc, Unit source, Unit data)
                {
                    return true;
                }
                public bool Aggregate(Label current, Label aggregateStart, bool canBeBranchTarget, Unit data)
                {
                    // recurse
                    return TraceAggregateSequentially(aggregateStart);
                }
                public override bool Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
                {
                    return CallHelper(pc, method, false, virt);
                }

                public override bool ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
                {
                    return CallHelper(pc, method, false, true);
                }

                private bool CallHelper(Label current, Method method, bool newObj, bool isVirtual)
                {
                    // if we insert pre/post conditions, we want separate blocks between the call and the preceding and succeeding instructions
                    // so we can add edge subroutines.

                    this.AddBlockStart(current);
                    if (newObj)
                    {
                        parent.labelsForNewObjSites[current] = method; // okay to double add
                    }
                    else
                    {
                        parent.labelsForCallSites[current] = new Pair<Method, bool>(method, isVirtual); // okay to double add
                    }


                    return true; // next instruction starts a new block
                }

                public override bool Newobj<ArgList>(Label pc, Method ctor, Unit dest, ArgList args, Unit data)
                {
                    return CallHelper(pc, ctor, true, false);
                }

                public override bool BeginOld(Label pc, Label matchingEnd, Unit data)
                {
                    // start a new block
                    this.AddTargetLabel(pc); // It's a target block as it starts a new subroutine
                    parent.BeginOldHook(pc);
                    return false;
                }

                public override bool EndOld(Label pc, Label matchingBegin, Type type, Unit dest, Unit source, Unit data)
                {
                    parent.EndOldHook(pc);
                    return true; // ends a block because we insert it as a subroutine.
                }
            }

            private class BlockBuilder :
               MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, BlockWithLabels<Label>, bool>,
               ICodeQuery<Label, Local, Parameter, Method, Field, Type, BlockWithLabels<Label>, bool>
            /*,
                    IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>
            */
            {
                [ContractInvariantMethod]
                private void ObjectInvariant()
                {
                    Contract.Invariant(parent != null); // F: Added as of Clousot suggestion as precondition in many places
                }

                private SubroutineBuilder<Label> parent;
                private BlockWithLabels<Label>/*?*/ currentBlock = null;

                private SubroutineBase<Label> CurrentSubroutine
                {
                    get
                    {
                        Contract.Ensures(Contract.Result<SubroutineBase<Label>>() != null); // F: added

                        Contract.Assume(parent.CurrentSubroutine != null); // F: should add as postcondition to the parent, but because of the generics we have some problems in picking it
                        return parent.CurrentSubroutine;
                    }
                }

                private BlockBuilder(SubroutineBuilder<Label> builder)
                {
                    Contract.Requires(builder != null);// F: As of Clousot suggestion
                    parent = builder;
                }

                internal static BlockWithLabels<Label> BuildBlocks(Label start, SubroutineBuilder<Label> builder)
                {
                    Contract.Requires(builder != null); // F: As of Clousot suggestion

                    BlockBuilder b = new BlockBuilder(builder);
                    b.TraceAggregateSequentially(start);

                    // check if we are falling off the end
                    if (b.currentBlock != null)
                    {
                        // add edge to exit
                        b.CurrentSubroutine.AddSuccessor(b.currentBlock, "fallthrough-return", b.CurrentSubroutine.Exit);
                        // Callback
                        b.CurrentSubroutine.AddReturnBlock(b.currentBlock);

                        return b.currentBlock;
                    }
                    return null;
                }

                internal static BlockWithLabels<Label> BuildBlocksWithFixedStartBlock(Label start, BlockWithLabels<Label> startBlk, SubroutineBuilder<Label> builder)
                {
                    BlockBuilder b = new BlockBuilder(builder);
                    b.TraceAggregateSequentially(start);
                    Contract.Assume(b.currentBlock != null);
                    b.CurrentSubroutine.AddSuccessor(startBlk, "assumeSetup", b.currentBlock);
                    // check if we are falling off the end
                    //if (b.currentBlock != null) // F: follows from the assume
                    {
                        // add edge to exit
                        b.CurrentSubroutine.AddSuccessor(b.currentBlock, "fallthrough-return", b.CurrentSubroutine.Exit);
                        // Callback
                        b.CurrentSubroutine.AddReturnBlock(b.currentBlock);

                        return b.currentBlock;
                    }
                    //return null;
                }

                /// <summary>
                /// Builds
                ///   - Protecting handler map
                ///   - Successor relation
                ///   - CFG blocks
                /// Temporary structures
                ///   - current protecting handlers
                ///   - currentBlock
                /// </summary>
                /// <param name="currentLabel"></param>
                private void TraceAggregateSequentially(Label currentLabel)
                {
                    bool more = true;
                    do
                    {
                        if (parent.IsBlockStart(currentLabel))
                        {
                            // starts a block
                            currentBlock = parent.RecordInformationForNewBlock(currentLabel, currentBlock);
                        }
                        Contract.Assume(currentBlock != null); // f: made an assume

                        // we add the currentLabel to the block in the decoded to Methods
                        // this allows optimizing intermediate labels away

                        Contract.Assume(parent.CodeProvider != null, "At this point expecting the CodeProvider != null");

                        // NOTE: this decode can recurse and cause this.currentBlock to change.
                        bool noFallThrough = parent.CodeProvider.Decode<BlockBuilder, BlockWithLabels<Label>, bool>(currentLabel, this, currentBlock);
                        if (noFallThrough)
                        {
                            currentBlock = null;
                        }

                        Contract.Assert(parent.CodeProvider != null, "should we make it a postcondition?");
                        more = parent.CodeProvider.Next(currentLabel, out currentLabel);
                    }
                    while (more);
                }

                protected override bool Default(Label pc, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    return false; // by default, we fall through
                }

                public override bool Branch(Label pc, Label target, bool leave, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    CurrentSubroutine.AddSuccessor(currentBlock, "branch", CurrentSubroutine.GetTargetBlock(target));
                    return true; // no fall through
                }

                public override bool BranchCond(Label pc, Label target, BranchOperator bop, Unit value1, Unit value2, BlockWithLabels<Label> currentBlock)
                {
                    return HandleCondBranch(pc, target, true, currentBlock);
                }

                public override bool BranchFalse(Label pc, Label target, Unit cond, BlockWithLabels<Label> data)
                {
                    return HandleCondBranch(pc, target, false, data);
                }

                public override bool BranchTrue(Label pc, Label target, Unit cond, BlockWithLabels<Label> data)
                {
                    return HandleCondBranch(pc, target, true, data);
                }

                private bool HandleCondBranch(Label pc, Label target, bool trueBranch, BlockWithLabels<Label> currentBlock)
                {
                    Contract.Requires(currentBlock != null); // F: Added as of Clousot suggestion

                    currentBlock.Add(pc);

                    string takenLabel = trueBranch ? "true" : "false";
                    string notTakenLabel = trueBranch ? "false" : "true";

                    // insert a special assume block for taken branch
                    AssumeBlock<Label> abTaken = CurrentSubroutine.NewAssumeBlock(pc, takenLabel);
                    parent.RecordBlockInfoSameAsOtherBlock(abTaken, this.currentBlock);
                    CurrentSubroutine.AddSuccessor(currentBlock, takenLabel, abTaken);
                    CurrentSubroutine.AddSuccessor(abTaken, "fallthrough", CurrentSubroutine.GetTargetBlock(target));

                    // insert a special assume block for untaken branch
                    AssumeBlock<Label> abElse = CurrentSubroutine.NewAssumeBlock(pc, notTakenLabel);
                    parent.RecordBlockInfoSameAsOtherBlock(abElse, this.currentBlock);
                    CurrentSubroutine.AddSuccessor(currentBlock, notTakenLabel, abElse);

                    this.currentBlock = abElse;
                    return false; // has fall through
                }

                public override bool Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, Unit value, BlockWithLabels<Label> currentBlock)
                {
                    List<object> patterns = new List<object>();
                    currentBlock.Add(pc);
                    foreach (Pair<object, Label> target in cases)
                    {
                        patterns.Add(target.One);
                        // insert a special switch case assume block            
                        SwitchCaseAssumeBlock<Label> ab = CurrentSubroutine.NewSwitchCaseAssumeBlock(pc, target.One, type);
                        parent.RecordBlockInfoSameAsOtherBlock(ab, this.currentBlock);
                        CurrentSubroutine.AddSuccessor(currentBlock, "switch", ab);
                        CurrentSubroutine.AddSuccessor(ab, "fallthrough", CurrentSubroutine.GetTargetBlock(target.Two));
                    }

                    // we assume we fall into the default. Insert special default assume block
                    SwitchDefaultAssumeBlock<Label> dab = CurrentSubroutine.NewSwitchDefaultAssumeBlock(pc, patterns, type);
                    parent.RecordBlockInfoSameAsOtherBlock(dab, this.currentBlock);
                    CurrentSubroutine.AddSuccessor(currentBlock, "default", dab);

                    this.currentBlock = dab;
                    return false; // has fall through
                }

                public override bool Throw(Label pc, Unit exn, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    return true; // no fall through
                }
                public override bool Rethrow(Label pc, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    return true; // no fall through
                }
                public override bool Endfinally(Label pc, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    CurrentSubroutine.AddSuccessor(currentBlock, "endsub", CurrentSubroutine.Exit);
                    return true; // no fall through
                }

                public override bool Return(Label pc, Unit source, BlockWithLabels<Label> currentBlock)
                {
                    currentBlock.Add(pc);
                    CurrentSubroutine.AddSuccessor(currentBlock, "return", CurrentSubroutine.Exit);
                    // Callback
                    CurrentSubroutine.AddReturnBlock(currentBlock);
                    return true; // no fall through
                }

                public bool Aggregate(Label pc, Label nestedAggregate, bool canBeBranchTarget, BlockWithLabels<Label> currentBlock)
                {
                    // recurse
                    this.TraceAggregateSequentially(nestedAggregate);
                    return false; // recursion would have already set current block to null if there is no fall through.
                }

                public override bool Nop(Label pc, BlockWithLabels<Label> data)
                {
                    // ignore this label
                    return false; // has fall through
                }


                /// <summary>
                /// Give current subroutine a chance to record information about matching begin/old old labels
                /// </summary>
                public override bool EndOld(Label pc, Label matchingBegin, Type type, Unit dest, Unit source, BlockWithLabels<Label> data)
                {
                    currentBlock.Add(pc);
                    CurrentSubroutine.AddSuccessor(currentBlock, "endold", CurrentSubroutine.Exit);
                    return false; // indicate fall-through, otherwise we don't properly record the end of the old scope.
                }

                public override bool Stfld(Label pc, Field field, bool @volatile, Unit obj, Unit value, BlockWithLabels<Label> currentBlock)
                {
                    if (this.CurrentSubroutine.IsMethod)
                    {
                        IMethodInfo<Method> mi = (IMethodInfo<Method>)this.CurrentSubroutine;
                        if (parent.MetadataDecoder.IsPropertySetter(mi.Method))
                        {
                            parent.MethodCache.AddModifies(mi.Method, field);
                        }
                    }
                    this.currentBlock.Add(pc);
                    return false; // has fall through
                }

                public override bool Ldflda(Label pc, Field field, UnitDest dest, UnitDest obj, BlockWithLabels<Label> data)
                {
                    // for setter modifies analysis, treat as modifies
                    if (this.CurrentSubroutine.IsMethod)
                    {
                        IMethodInfo<Method> mi = (IMethodInfo<Method>)this.CurrentSubroutine;
                        if (parent.MetadataDecoder.IsPropertySetter(mi.Method))
                        {
                            parent.MethodCache.AddModifies(mi.Method, field);
                        }
                    }
                    currentBlock.Add(pc);
                    return false; // has fall through
                }

                public override bool Ldfld(Label pc, Field field, bool @volatile, Unit obj, Unit value, BlockWithLabels<Label> currentBlock)
                {
                    if (this.CurrentSubroutine.IsMethod)
                    {
                        IMethodInfo<Method> mi = (IMethodInfo<Method>)this.CurrentSubroutine;
                        if (parent.MetadataDecoder.IsPropertyGetter(mi.Method))
                        {
                            parent.MethodCache.AddReads(mi.Method, field);
                        }
                    }
                    this.currentBlock.Add(pc);
                    return false; // has fall through
                }
            }


            internal virtual void BeginOldHook(Label current)
            {
            }

            internal virtual void EndOldHook(Label current)
            {
            }

            internal bool IsTargetLabel(Label label)
            {
                return targetLabels.Contains(label);
            }
        }

        internal class SubroutineWithHandlersBuilder<Label, Handler> : SubroutineBuilder<Label>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.CodeProvider != null); // F: Added as of Clousot suggestions as precondition in many places
            }


            protected override SubroutineBase<Label> CurrentSubroutine { get { return subroutineStack.Head; } }

            private FList<SubroutineWithHandlers<Label, Handler>> subroutineStack;

            protected SubroutineWithHandlers<Label, Handler> CurrentSubroutineWithHandler
            {
                get
                {
                    Contract.Assume(subroutineStack != null, "Seems that when calling CurrentSubroutineStack we know the assumption holds");

                    return subroutineStack.Head;
                }
            }

            private OnDemandMap<Label, Stack<Handler>> tryStartList;
            private OnDemandMap<Label, Queue<Handler>> tryEndList;
            private OnDemandMap<Label, Queue<Handler>> subroutineHandlerEndList;
            private OnDemandMap<Label, Handler> handlerStartingAt;
            protected readonly Method method;

            internal new readonly IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> CodeProvider;


            public SubroutineWithHandlersBuilder(
              IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider,
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              Method method,
              Label entry
            )
              : base(codeProvider, methodCache, entry)
            {
                Contract.Requires(codeProvider != null); // F: Added as of Clousot suggestion
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion

                this.CodeProvider = codeProvider;
                this.method = method;
                ComputeTryBlockStartAndEndInfo(method);
                base.Initialize(entry);
            }

            public CFGBlock BuildBlocks(Label entry, SubroutineWithHandlers<Label, Handler> subroutine)
            {
                subroutineStack = FList<SubroutineWithHandlers<Label, Handler>>.Cons(subroutine, null);
                return base.BuildBlocks(entry);
            }

            internal override void RecordBlockInfoSameAsOtherBlock(BlockWithLabels<Label> ab, BlockWithLabels<Label> otherblock)
            {
                FList<Handler>/*?*/ protHandlers;
                if (this.CurrentSubroutineWithHandler.ProtectingHandlers.TryGetValue(otherblock, out protHandlers))
                {
                    this.CurrentSubroutineWithHandler.ProtectingHandlers.Add(ab, protHandlers);
                }
            }

            private FList<Handler> CurrentProtectingHandlers
            {
                get
                {
                    Contract.Assume(this.CurrentSubroutineWithHandler != null);
                    return this.CurrentSubroutineWithHandler.CurrentProtectingHandlers;
                }
                set
                {
                    Contract.Assume(this.CurrentSubroutineWithHandler != null);
                    this.CurrentSubroutineWithHandler.CurrentProtectingHandlers = value;
                }
            }

            protected override BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label>/*?*/ previousBlock)
            {
                BlockWithLabels<Label>/*?*/ newBlock = null;

                #region Pop subroutine handlers off stack whose scope ends here (must be first)
                Queue<Handler> handlerEnds = this.GetHandlerEnd(currentLabel);
                if (handlerEnds != null)
                {
                    foreach (Handler eh in handlerEnds)
                    {
                        // TODO: not enough info to make sure we are popping the correct subroutine
                        SubroutineBase<Label> endingSubroutine = subroutineStack.Head;
                        endingSubroutine.Commit();

                        subroutineStack = subroutineStack.Tail;
                        // can't fall from one subroutine into another
                        previousBlock = null;
                    }
                }
                #endregion

                #region Pop protecting handlers off stack whose scope ends here

                Queue<Handler> tryEnds = this.GetTryEnd(currentLabel);
                if (tryEnds != null)
                {
                    foreach (Handler eh in tryEnds)
                    {
                        if (Object.Equals(eh, CurrentProtectingHandlers.Head))
                        {
                            CurrentProtectingHandlers = CurrentProtectingHandlers.Tail; // Pop handler
                        }
                        else
                        {
                            throw new ApplicationException("wrong handler");
                        }
                    }
                }
                #endregion

                #region Check if this is a fault/finally subroutine start or an exception handler
                Handler handlerStarting;
                if (this.IsHandlerStart(currentLabel, out handlerStarting))
                {
                    if (this.IsFaultOrFinally(handlerStarting))
                    {
                        SubroutineWithHandlers<Label, Handler> subroutine;
                        if (this.CodeProvider.IsFaultHandler(handlerStarting))
                        {
                            subroutine = new FaultSubroutine<Label, Handler>(this.MethodCache, this, currentLabel);
                        }
                        else
                        {
                            subroutine = new FinallySubroutine<Label, Handler>(this.MethodCache, this, currentLabel);
                        }
                        this.CurrentSubroutineWithHandler.FaultFinallySubroutines.Add(handlerStarting, subroutine);

                        // push new current subroutine
                        subroutineStack = FList<SubroutineWithHandlers<Label, Handler>>.Cons(subroutine, subroutineStack);

                        previousBlock = null; // can't fall into here.
                    }
                    else
                    {
                        // This is an exception handler start. Make sure we use a special entry block for it.
                        newBlock = CurrentSubroutineWithHandler.CreateCatchFilterHeader(handlerStarting, currentLabel);
                    }
                }
                #endregion


                #region Get new block and record fall-through edge if necessary
                if (newBlock == null)
                {
                    newBlock = base.RecordInformationForNewBlock(currentLabel, previousBlock);
                }

                #endregion

                #region Push protecting handlers on stack whose scope starts here
                Stack<Handler> starts = this.GetTryStart(currentLabel);
                if (starts != null)
                {
                    foreach (Handler eh in starts)
                    {
                        // push this handler on top of current block enclosing handlers
                        CurrentProtectingHandlers = FList<Handler>.Cons(eh, CurrentProtectingHandlers); // Push handler
                    }
                }
                #endregion


                #region Record handlers for new block
                CurrentSubroutineWithHandler.ProtectingHandlers.Add(newBlock, this.CurrentProtectingHandlers);
                #endregion

                return newBlock;
            }


            private void ComputeTryBlockStartAndEndInfo(Method method)
            {
                foreach (Handler eh in this.CodeProvider.TryBlocks(method).AssumeNotNull())
                {
                    if (this.CodeProvider.IsFilterHandler(eh))
                    {
                        this.AddTargetLabel(CodeProvider.FilterDecisionStart(eh));
                    }
                    this.AddTargetLabel(CodeProvider.HandlerStart(eh));
                    this.AddTargetLabel(CodeProvider.HandlerEnd(eh));

                    AddTryStart(eh);
                    AddTryEnd(eh);
                    AddHandlerEnd(eh);
                    handlerStartingAt.Add(CodeProvider.HandlerStart(eh), eh);
                }
            }

            private void AddTryStart(Handler eh)
            {
                Label tryStart = this.CodeProvider.TryStart(eh);
                Stack<Handler>/*?*/ starts;
                tryStartList.TryGetValue(tryStart, out starts);
                if (starts == null)
                {
                    starts = new Stack<Handler>();
                    tryStartList[tryStart] = starts;
                }
                starts.Push(eh);

                // also update block start
                this.AddTargetLabel(tryStart);
            }

            private void AddTryEnd(Handler eh)
            {
                Label tryEnd = this.CodeProvider.TryEnd(eh);
                Queue<Handler>/*?*/ ends;
                tryEndList.TryGetValue(tryEnd, out ends);
                if (ends == null)
                {
                    ends = new Queue<Handler>();
                    tryEndList[tryEnd] = ends;
                }
                ends.Enqueue(eh);

                // also update block start
                this.AddTargetLabel(tryEnd);
            }

            private void AddHandlerEnd(Handler eh)
            {
                if (!this.IsFaultOrFinally(eh)) return;
                // only record subroutine handlers
                Label ehEnd = this.CodeProvider.HandlerEnd(eh);
                Queue<Handler>/*?*/ ends;
                subroutineHandlerEndList.TryGetValue(ehEnd, out ends);
                if (ends == null)
                {
                    ends = new Queue<Handler>();
                    subroutineHandlerEndList[ehEnd] = ends;
                }
                ends.Enqueue(eh);

                // also update block start
                this.AddTargetLabel(ehEnd);
            }

            public bool IsHandlerStart(Label label, out Handler handler)
            {
                return (handlerStartingAt.TryGetValue(label, out handler));
            }

            public bool IsFaultOrFinally(Handler handler)
            {
                return this.CodeProvider.IsFaultHandler(handler) || this.CodeProvider.IsFinallyHandler(handler);
            }

            public Queue<Handler> GetHandlerEnd(Label label)
            {
                Queue<Handler>/*?*/ result;
                subroutineHandlerEndList.TryGetValue(label, out result);
                return result;
            }

            public Queue<Handler> GetTryEnd(Label label)
            {
                Queue<Handler>/*?*/ result;
                tryEndList.TryGetValue(label, out result);
                return result;
            }

            public Stack<Handler> GetTryStart(Label label)
            {
                Stack<Handler>/*?*/ result;
                tryStartList.TryGetValue(label, out result);
                return result;
            }
        }

        internal class AssumeAsPostConditionSubroutineBuilder<Label> : SubroutineBuilder<Label>
        {
            private SubroutineBase<Label> currentSubroutine;

            public AssumeAsPostConditionSubroutineBuilder(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
                                        MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                        Label entry
         )
           : base(codeProvider, methodCache, entry)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion

                base.Initialize(entry);
            }

            public BlockWithLabels<Label> BuildBlocks(Label entry, BlockWithLabels<Label> startBlk, SubroutineBase<Label> subroutine)
            {
                currentSubroutine = subroutine;
                return base.BuildBlocksWithFixedStartBlock(entry, startBlk);
            }

            protected override SubroutineBase<Label> CurrentSubroutine
            {
                get
                {
                    return currentSubroutine;
                }
            }
        }

        internal class SimpleSubroutineBuilder<Label> : SubroutineBuilder<Label>
        {
            private SubroutineBase<Label> currentSubroutine;

            public SimpleSubroutineBuilder(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
                                           MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                           Label entry
            )
              : base(codeProvider, methodCache, entry)
            {
                Contract.Requires(methodCache != null); // F: As of Clousot suggestion
                base.Initialize(entry);
            }

            public BlockWithLabels<Label> BuildBlocks(Label entry, SubroutineBase<Label> subroutine)
            {
                currentSubroutine = subroutine;
                return base.BuildBlocks(entry);
            }

            protected override SubroutineBase<Label> CurrentSubroutine
            {
                get
                {
                    if (this.currentOldSubroutine != null) return this.currentOldSubroutine;
                    return currentSubroutine;
                }
            }

            protected OldValueSubroutine<Label> currentOldSubroutine;
            protected BlockWithLabels<Label> blockPriorToOld;

            protected override BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label> previousBlock)
            {
                Label lastLabel;
                BlockWithLabels<Label> newBlock;
                if (previousBlock != null && previousBlock.HasLastLabel(out lastLabel) && endOldStart.Contains(lastLabel))
                {
                    // end the old subroutine here.
                    var endingOldSubroutine = this.currentOldSubroutine;
                    endingOldSubroutine.Commit(previousBlock);

                    // now change the context to previous subroutine (ensures)
                    this.currentOldSubroutine = null;
                    Contract.Assume(this.CurrentSubroutine != null);
                    Contract.Assume(this.CurrentSubroutine.IsEnsures);
                    // We need to fall into the new block from the block prior to the old subroutine
                    Contract.Assume(blockPriorToOld.Subroutine == this.CurrentSubroutine);
                    newBlock = base.RecordInformationForNewBlock(currentLabel, blockPriorToOld);
                    Contract.Assume(newBlock.Subroutine == this.CurrentSubroutine);
                    this.CurrentSubroutine.AddEdgeSubroutine(blockPriorToOld, newBlock, endingOldSubroutine, "old");
                    return newBlock;
                }
                if (beginOldStart.Contains(currentLabel))
                {
                    // start of an old subroutine
                    Contract.Assume(this.currentOldSubroutine == null); // F: made an assume
                    this.currentOldSubroutine = new OldValueSubroutine<Label>(this.MethodCache, ((CallingContractSubroutine<Label>)currentSubroutine).Method, this, currentLabel);
                    this.blockPriorToOld = previousBlock;

                    // don't fall into this block
                    newBlock = base.RecordInformationForNewBlock(currentLabel, null);
                    this.currentOldSubroutine.RegisterBeginBlock(newBlock);
                    return newBlock;
                }
                newBlock = base.RecordInformationForNewBlock(currentLabel, previousBlock);

                return newBlock;
            }

            private IMutableSet<Label> beginOldStart = new Set<Label>();
            private IMutableSet<Label> endOldStart = new Set<Label>();

            internal override void BeginOldHook(Label label)
            {
                beginOldStart.Add(label);
            }

            internal override void EndOldHook(Label label)
            {
                endOldStart.Add(label);
            }
        }


        internal abstract class BlockBase : CFGBlock
        {
            internal BlockBase(Subroutine container, ref int idGen) : base(container, ref idGen)
            {
                Contract.Requires(idGen >= 0);// F: added because of Clousot suggestion
                Contract.Requires(container != null); // F: added because of Clousot suggestion

                Contract.Ensures(idGen >= 0);
            }

            internal abstract Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
              where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>;
        }
        /// <summary>
        /// CFG blocks group consecutive instructions/code fragments to reduce the number of edges
        /// in the graph. A Block has a single entry point, meaning all control transfers go to the head
        /// never to an interior label. The only control transfer out of the block is at the last instruction.
        /// </summary>
        /// <typeparam name="Label"></typeparam>
        internal class BlockWithLabels<Label> : BlockBase, IEquatable<BlockWithLabels<Label>>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.subroutine != null); // F: made an object invariant
                Contract.Invariant(Labels != null);// F: made an object invariant
            }


            #region Private/Internal

            private readonly List<Label> Labels;


            internal BlockWithLabels(SubroutineBase<Label> container, ref int idGen)
              : base(container, ref idGen)
            {
                Contract.Requires(container != null); // F: As of Clousot suggestion
                Contract.Requires(idGen >= 0); // F: As of Clousot suggestion

                Contract.Ensures(idGen >= 0);

                Labels = new List<Label>();
                this.subroutine = container;
            }

            internal void Add(Label label)
            {
                Labels.Add(label);
            }

            #endregion

            internal readonly SubroutineBase<Label> subroutine;

            #region public API
            public override int Count
            {
                get { return Labels.Count; }
            }

            internal bool HasLastLabel(out Label label)
            {
                if (Labels.Count > 0)
                {
                    label = Labels[Labels.Count - 1];
                    return true;
                }
                label = default(Label);
                return false;
            }

            internal virtual bool UnderlyingLabelForward(int index, out Label/*?*/ label)
            {
                Contract.Requires(index >= 0);

                if (index < Labels.Count)
                {
                    label = Labels[index];
                    return true;
                }
                else
                {
                    label = default(Label);
                    return false;
                }
            }

            public override string SourceAssertionCondition(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceAssertionCondition(label);
                }
                return null;
            }

            public override string SourceContext(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceContext(label);
                }
                return null;
            }

            public override string SourceDocument(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceDocument(label);
                }
                return null;
            }

            public override int SourceStartLine(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceStartLine(label);
                }
                return 0;
            }

            public override int SourceEndLine(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceEndLine(label);
                }
                return 0;
            }

            public override int SourceStartColumn(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceStartColumn(label);
                }
                return 0;
            }

            public override int SourceEndColumn(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceEndColumn(label);
                }
                return 0;
            }

            public override int SourceStartIndex(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceStartIndex(label);
                }
                return 0;
            }

            public override int SourceLength(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.SourceLength(label);
                }
                return 0;
            }

            public override int ILOffset(APC pc)
            {
                Label label;
                if (this.UnderlyingLabelForward(pc.Index, out label))
                {
                    return this.subroutine.ILOffset(label);
                }
                return 0;
            }

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                Label/*?*/ lab;
                if (this.UnderlyingLabelForward(pc.Index, out lab))
                {
                    var decoder = this.subroutine.CodeProvider;
                    return decoder.Decode<LabelAdapter<Data, Result, Visitor>, Data, Result>(lab, new LabelAdapter<Data, Result, Visitor>(visitor, pc), data);
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
            }

            #region APC -> Label adapter visitor. Also turns Aggregate into Nops, i.e., adapts ICodeQuery to IVisitMSIL

            protected struct LabelAdapter<Data, Result, Visitor> :
              ICodeQuery<Label, Local, Parameter, Method, Field, Type, Data, Result>
              where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, UnitSource, UnitDest, Data, Result>
            {
                private APC originalPC;
                private Visitor visitor;

                public LabelAdapter(
                  Visitor visitor,
                  APC origPC
                )
                {
                    this.visitor = visitor;
                    originalPC = origPC;
                }

                private APC ConvertLabel(Label pc)
                {
                    // we return the stored pc here, as the underlying decoder only dealt with a label
                    // this works because we never translate branch target labels.
                    return originalPC;
                }

                /// <summary>
                /// Map a label occurring in a begin_old/end_old to an apc.
                /// </summary>
                private APC ConvertMatchingBeginLabel(Label underlying)
                {
                    Contract.Assume(originalPC.Block != null); // F: made it an assumption

                    OldValueSubroutine<Label> subroutine = (OldValueSubroutine<Label>)originalPC.Block.Subroutine;

                    return subroutine.BeginOldAPC(originalPC.SubroutineContext);
                }
                private APC ConvertMatchingEndLabel(Label underlying)
                {
                    Contract.Assume(originalPC.Block != null); // F: made it an assumption
                    OldValueSubroutine<Label> subroutine = (OldValueSubroutine<Label>)originalPC.Block.Subroutine;

                    return subroutine.EndOldAPC(originalPC.SubroutineContext);
                }

                public Result Aggregate(Label pc, Label nested, bool branchTarget, Data data)
                {
                    return visitor.Nop(ConvertLabel(pc), data);
                }

                public Result Assume(Label pc, string tag, UnitSource source, object provenance, Data data)
                {
                    return visitor.Assume(ConvertLabel(pc), tag, source, provenance, data);
                }

                public Result Assert(Label pc, string tag, UnitSource source, object provenance, Data data)
                {
                    return visitor.Assert(ConvertLabel(pc), tag, source, provenance, data);
                }

                public Result Arglist(Label pc, UnitDest dest, Data data)
                {
                    return visitor.Arglist(ConvertLabel(pc), dest, data);
                }

                public Result Binary(Label pc, BinaryOperator op, UnitDest dest, UnitSource s1, UnitSource s2, Data data)
                {
                    return visitor.Binary(ConvertLabel(pc), op, dest, s1, s2, data);
                }

                public Result BranchCond(Label pc, Label target, BranchOperator bop, UnitSource value1, UnitSource value2, Data data)
                {
                    return visitor.BranchCond(ConvertLabel(pc), ConvertLabel(target), bop, value1, value2, data);
                }

                public Result BranchTrue(Label pc, Label target, UnitSource cond, Data data)
                {
                    return visitor.BranchTrue(ConvertLabel(pc), ConvertLabel(target), cond, data);
                }

                public Result BranchFalse(Label pc, Label target, UnitSource cond, Data data)
                {
                    return visitor.BranchFalse(ConvertLabel(pc), ConvertLabel(target), cond, data);
                }

                public Result Branch(Label pc, Label target, bool leave, Data data)
                {
                    return visitor.Branch(ConvertLabel(pc), ConvertLabel(target), leave, data);
                }

                public Result Break(Label pc, Data data)
                {
                    return visitor.Break(ConvertLabel(pc), data);
                }

                public Result Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<UnitSource>
                {
                    return visitor.Call(ConvertLabel(pc), method, tail, virt, extraVarargs, dest, args, data);
                }

                public Result Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, UnitDest dest, UnitSource fp, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<UnitSource>
                {
                    return visitor.Calli(ConvertLabel(pc), returnType, argTypes, tail, isInstance, dest, fp, args, data);
                }

                public Result Ckfinite(Label pc, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Ckfinite(ConvertLabel(pc), dest, source, data);
                }

                public Result Cpblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource srcaddr, UnitSource len, Data data)
                {
                    return visitor.Cpblk(ConvertLabel(pc), @volatile, destaddr, srcaddr, len, data);
                }

                public Result Endfilter(Label pc, UnitSource decision, Data data)
                {
                    return visitor.Endfilter(ConvertLabel(pc), decision, data);
                }

                public Result Endfinally(Label pc, Data data)
                {
                    return visitor.Endfinally(ConvertLabel(pc), data);
                }

                public Result Entry(Label pc, Method method, Data data)
                {
                    return visitor.Entry(ConvertLabel(pc), method, data);
                }

                public Result Initblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource value, UnitSource len, Data data)
                {
                    return visitor.Initblk(ConvertLabel(pc), @volatile, destaddr, value, len, data);
                }

                public Result Jmp(Label pc, Method method, Data data)
                {
                    return visitor.Jmp(ConvertLabel(pc), method, data);
                }

                /// <summary>
                /// Could be access to "this" in an invariant code sequence. Map to "this" in surrounding using method.
                /// </summary>
                public Result Ldarg(Label pc, Parameter argument, bool isOld, UnitDest dest, Data data)
                {
                    return visitor.Ldarg(ConvertLabel(pc), argument, isOld, dest, data);
                }

                public Result Ldarga(Label pc, Parameter argument, bool isOld, UnitDest dest, Data data)
                {
                    return visitor.Ldarga(ConvertLabel(pc), argument, isOld, dest, data);
                }

                public Result Ldconst(Label pc, object constant, Type type, UnitDest dest, Data data)
                {
                    return visitor.Ldconst(ConvertLabel(pc), constant, type, dest, data);
                }

                public Result Ldnull(Label pc, UnitDest dest, Data data)
                {
                    return visitor.Ldnull(ConvertLabel(pc), dest, data);
                }

                public Result Ldftn(Label pc, Method method, UnitDest dest, Data data)
                {
                    return visitor.Ldftn(ConvertLabel(pc), method, dest, data);
                }

                public Result Ldind(Label pc, Type type, bool @volatile, UnitDest dest, UnitSource ptr, Data data)
                {
                    return visitor.Ldind(ConvertLabel(pc), type, @volatile, dest, ptr, data);
                }

                public Result Ldloc(Label pc, Local local, UnitDest dest, Data data)
                {
                    return visitor.Ldloc(ConvertLabel(pc), local, dest, data);
                }

                public Result Ldloca(Label pc, Local local, UnitDest dest, Data data)
                {
                    return visitor.Ldloca(ConvertLabel(pc), local, dest, data);
                }

                public Result Ldstack(Label pc, int offset, UnitDest dest, UnitSource source, bool isOld, Data data)
                {
                    return visitor.Ldstack(ConvertLabel(pc), offset, dest, source, isOld, data);
                }

                public Result Ldstacka(Label pc, int offset, UnitDest dest, UnitSource source, Type type, bool isOld, Data data)
                {
                    return visitor.Ldstacka(ConvertLabel(pc), offset, dest, source, type, isOld, data);
                }

                public Result Localloc(Label pc, UnitDest dest, UnitSource size, Data data)
                {
                    return visitor.Localloc(ConvertLabel(pc), dest, size, data);
                }

                public Result Nop(Label pc, Data data)
                {
                    return visitor.Nop(ConvertLabel(pc), data);
                }

                public Result Pop(Label pc, UnitSource source, Data data)
                {
                    return visitor.Pop(ConvertLabel(pc), source, data);
                }

                public Result Return(Label pc, UnitSource source, Data data)
                {
                    return visitor.Return(ConvertLabel(pc), source, data);
                }

                public Result Starg(Label pc, Parameter argument, UnitSource source, Data data)
                {
                    return visitor.Starg(ConvertLabel(pc), argument, source, data);
                }

                public Result Stind(Label pc, Type type, bool @volatile, UnitSource ptr, UnitSource value, Data data)
                {
                    return visitor.Stind(ConvertLabel(pc), type, @volatile, ptr, value, data);
                }

                public Result Stloc(Label pc, Local local, UnitSource source, Data data)
                {
                    return visitor.Stloc(ConvertLabel(pc), local, source, data);
                }

                public Result Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, UnitSource value, Data data)
                {
                    return visitor.Nop(originalPC, data);
                }

                public Result Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Unary(ConvertLabel(pc), op, overflow, unsigned, dest, source, data);
                }

                public Result Box(Label pc, Type type, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Box(ConvertLabel(pc), type, dest, source, data);
                }

                public Result ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<UnitSource>
                {
                    return visitor.ConstrainedCallvirt(ConvertLabel(pc), method, tail, constraint, extraVarargs, dest, args, data);
                }

                public Result Castclass(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Castclass(ConvertLabel(pc), type, dest, obj, data);
                }

                public Result Cpobj(Label pc, Type type, UnitSource destptr, UnitSource srcptr, Data data)
                {
                    return visitor.Cpobj(ConvertLabel(pc), type, destptr, srcptr, data);
                }

                public Result Initobj(Label pc, Type type, UnitSource ptr, Data data)
                {
                    return visitor.Initobj(ConvertLabel(pc), type, ptr, data);
                }

                public Result Isinst(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Isinst(ConvertLabel(pc), type, dest, obj, data);
                }

                public Result Ldelem(Label pc, Type type, UnitDest dest, UnitSource array, UnitSource index, Data data)
                {
                    return visitor.Ldelem(ConvertLabel(pc), type, dest, array, index, data);
                }

                public Result Ldelema(Label pc, Type type, bool @readonly, UnitDest dest, UnitSource array, UnitSource index, Data data)
                {
                    return visitor.Ldelema(ConvertLabel(pc), type, @readonly, dest, array, index, data);
                }

                public Result Ldfld(Label pc, Field field, bool @volatile, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Ldfld(ConvertLabel(pc), field, @volatile, dest, obj, data);
                }

                public Result Ldflda(Label pc, Field field, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Ldflda(ConvertLabel(pc), field, dest, obj, data);
                }

                public Result Ldlen(Label pc, UnitDest dest, UnitSource array, Data data)
                {
                    return visitor.Ldlen(ConvertLabel(pc), dest, array, data);
                }

                public Result Ldsfld(Label pc, Field field, bool @volatile, UnitDest dest, Data data)
                {
                    return visitor.Ldsfld(ConvertLabel(pc), field, @volatile, dest, data);
                }

                public Result Ldsflda(Label pc, Field field, UnitDest dest, Data data)
                {
                    return visitor.Ldsflda(ConvertLabel(pc), field, dest, data);
                }

                public Result Ldtypetoken(Label pc, Type type, UnitDest dest, Data data)
                {
                    return visitor.Ldtypetoken(ConvertLabel(pc), type, dest, data);
                }

                public Result Ldfieldtoken(Label pc, Field field, UnitDest dest, Data data)
                {
                    return visitor.Ldfieldtoken(ConvertLabel(pc), field, dest, data);
                }

                public Result Ldmethodtoken(Label pc, Method method, UnitDest dest, Data data)
                {
                    return visitor.Ldmethodtoken(ConvertLabel(pc), method, dest, data);
                }

                public Result Ldvirtftn(Label pc, Method method, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Ldvirtftn(ConvertLabel(pc), method, dest, obj, data);
                }

                public Result Mkrefany(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Mkrefany(ConvertLabel(pc), type, dest, obj, data);
                }

                public Result Newarray<ArgList>(Label pc, Type type, UnitDest dest, ArgList lengths, Data data)
                  where ArgList : IIndexable<UnitSource>
                {
                    return visitor.Newarray(ConvertLabel(pc), type, dest, lengths, data);
                }

                public Result Newobj<ArgList>(Label pc, Method ctor, UnitDest dest, ArgList args, Data data)
                  where ArgList : IIndexable<UnitSource>
                {
                    return visitor.Newobj(ConvertLabel(pc), ctor, dest, args, data);
                }

                public Result Refanytype(Label pc, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Refanytype(ConvertLabel(pc), dest, source, data);
                }

                public Result Refanyval(Label pc, Type type, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Refanyval(ConvertLabel(pc), type, dest, source, data);
                }

                public Result Rethrow(Label pc, Data data)
                {
                    return visitor.Rethrow(ConvertLabel(pc), data);
                }

                public Result Sizeof(Label pc, Type type, UnitDest dest, Data data)
                {
                    return visitor.Sizeof(ConvertLabel(pc), type, dest, data);
                }

                public Result Stelem(Label pc, Type type, UnitSource array, UnitSource index, UnitSource value, Data data)
                {
                    return visitor.Stelem(ConvertLabel(pc), type, array, index, value, data);
                }

                public Result Stfld(Label pc, Field field, bool @volatile, UnitSource obj, UnitSource value, Data data)
                {
                    return visitor.Stfld(ConvertLabel(pc), field, @volatile, obj, value, data);
                }

                public Result Stsfld(Label pc, Field field, bool @volatile, UnitSource value, Data data)
                {
                    return visitor.Stsfld(ConvertLabel(pc), field, @volatile, value, data);
                }

                public Result Throw(Label pc, UnitSource exn, Data data)
                {
                    return visitor.Throw(ConvertLabel(pc), exn, data);
                }

                public Result Unbox(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Unbox(ConvertLabel(pc), type, dest, obj, data);
                }

                public Result Unboxany(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
                {
                    return visitor.Unboxany(ConvertLabel(pc), type, dest, obj, data);
                }


                #region IVisitSynthIL<Label,Method2,Source,Dest,Data,Result> Members


                public Result BeginOld(Label pc, Label matchingEnd, Data data)
                {
                    // special treatment of beginold decoding in the context of an old manifestation.
                    if (originalPC.InsideOldManifestation)
                    {
                        return visitor.Nop(ConvertLabel(pc), data);
                    }

                    return visitor.BeginOld(ConvertLabel(pc), ConvertMatchingEndLabel(matchingEnd), data);
                }

                public Result EndOld(Label pc, Label matchingBegin, Type type, UnitDest dest, UnitSource source, Data data)
                {
                    // special treatment of beginold decoding is done in the stack decoder, as it 
                    // needs to know the specialness of endOld to generate the appropriate copy
                    return visitor.EndOld(ConvertLabel(pc), ConvertMatchingBeginLabel(matchingBegin), type, dest, source, data);
                }

                public Result Ldresult(Label pc, Type type, UnitDest dest, UnitSource source, Data data)
                {
                    return visitor.Ldresult(ConvertLabel(pc), type, dest, source, data);
                }

                #endregion
            }


            #endregion

            #endregion


            /// <summary>
            /// For debugging
            /// </summary>
            //^ [Confined]
            public override string/*!*/ ToString()
            {
                return this.Index.ToString();
            }

            #region IEquatable<CFGBlock<Label>> Members

            //^ [StateIndependent]
            public bool Equals(BlockWithLabels<Label> other)
            {
                return this == other;
            }

            #endregion
        }

        internal class EnsuresBlock<Label> : BlockWithLabels<Label>
        {
            private const int BeginOldMask = unchecked((int)0x80000000);
            private const int EndOldMask = 0x40000000;
            private const int Mask = unchecked((int)0xC0000000);

            /// <summary>
            /// The integers are used as follows:
            /// - if no Mask bits are set, then it's the index of a label in the underlying
            ///   label list
            /// - otherwise, the mask bits indicate if it is a begin old or an end old and
            ///   the remaining bits provide the index of the corresponding end/begin within
            ///   the corresponding block (which is stored on the subroutine).
            /// </summary>
            private List<int> overridingLabels;

            public EnsuresBlock(SubroutineBase<Label> container, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion
                Contract.Ensures(idgen >= 0);
            }

            internal bool UsesOverriding { get { return overridingLabels != null; } }

            private new EnsuresSubroutine<Label> Subroutine { get { return (EnsuresSubroutine<Label>)base.Subroutine; } }

            public override int Count
            {
                get
                {
                    if (overridingLabels != null) return overridingLabels.Count;
                    return base.Count;
                }
            }

            private bool IsOriginal(int index, out int originalOffset)
            {
                Contract.Requires(index >= 0);

                if (overridingLabels == null)
                {
                    originalOffset = index;
                    return true;
                }
                if (index < overridingLabels.Count && (overridingLabels[index] & Mask) == 0)
                {
                    originalOffset = overridingLabels[index] & ~Mask;
                    return true;
                }
                originalOffset = 0;
                return false;
            }

            private bool IsBeginOld(int index, out int endOldIndex)
            {
                Contract.Requires(index >= 0);

                if (overridingLabels == null || index >= overridingLabels.Count)
                {
                    endOldIndex = 0;
                    return false;
                }
                if ((overridingLabels[index] & BeginOldMask) != 0)
                {
                    endOldIndex = overridingLabels[index] & ~Mask;
                    return true;
                }
                endOldIndex = 0;
                return false;
            }

            private bool IsEndOld(int index, out int beginOldIndex)
            {
                Contract.Requires(index >= 0);

                if (overridingLabels == null || index >= overridingLabels.Count)
                {
                    beginOldIndex = 0;
                    return false;
                }
                if ((overridingLabels[index] & EndOldMask) != 0)
                {
                    beginOldIndex = overridingLabels[index] & ~Mask;
                    return true;
                }
                beginOldIndex = 0;
                return false;
            }

            internal override bool UnderlyingLabelForward(int index, out Label label)
            {
                Contract.Assert(index >= 0);

                int originalOffset;
                if (IsOriginal(index, out originalOffset))
                {
                    Contract.Assume(originalOffset >= 0); // F: suggested by Clousot
                    return base.UnderlyingLabelForward(originalOffset, out label);
                }
                label = default(Label);
                return false;
            }


            internal Result OriginalForwardDecode<Data, Result, Visitor>(int index, Visitor visitor, Data data)
              where Visitor : ICodeQuery<Label, Local, Parameter, Method, Field, Type, Data, Result>
            {
                Contract.Assume(index >= 0);
                Label/*?*/ lab;
                if (base.UnderlyingLabelForward(index, out lab))
                {
                    Contract.Assume(this.subroutine.CodeProvider != null); // F: made it an assumption
                    return this.subroutine.CodeProvider.Decode<Visitor, Data, Result>(lab, visitor, data);
                }
                throw new NotImplementedException();
            }

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                Label/*?*/ lab;
                if (this.UnderlyingLabelForward(pc.Index, out lab))
                {
                    return base.ForwardDecode<Data, Result, Visitor>(pc, visitor, data);
                }
                int endOldIndex;
                if (IsBeginOld(pc.Index, out endOldIndex))
                {
                    var endBlock = this.Subroutine.InferredBeginEndBijection(pc);
                    return visitor.BeginOld(pc, new APC(endBlock, endOldIndex, pc.SubroutineContext), data);
                }
                int beginOldIndex;
                if (IsEndOld(pc.Index, out beginOldIndex))
                {
                    Type endOldType;
                    var beginBlock = this.Subroutine.InferredBeginEndBijection(pc, out endOldType);
                    return visitor.EndOld(pc, new APC(beginBlock, beginOldIndex, pc.SubroutineContext), endOldType, Unit.Value, Unit.Value, data);
                }
                return visitor.Nop(pc, data);
            }


            internal void StartOverridingLabels()
            {
                Contract.Assume(overridingLabels == null); // F: as of Clousot Suggestion - but it seems something is wrong, so we make it an assume
                Debug.Assert(overridingLabels == null);
                overridingLabels = new List<int>();
            }

            internal void BeginOld(int index)
            {
                if (overridingLabels == null)
                {
                    StartOverridingLabels();
                    for (int i = 0; i < index; i++)
                    {
                        overridingLabels.Add(i); // original instructions up to but not including index
                    }
                }
                // temporary begin_old without corresponding end old index
                overridingLabels.Add(BeginOldMask);
            }

            internal void AddInstruction(int index)
            {
                Contract.Assume(overridingLabels != null); // F: As of Clousot suggestion - It seems something is wrong with visibility check, so we made it an assume
                                                           // add original instruction
                overridingLabels.Add(index);
            }


            internal void EndOld(int index, Type nextEndOldType)
            {
                AddInstruction(index);
                EndOldWithoutInstruction(nextEndOldType);
            }

            internal void EndOldWithoutInstruction(Type nextEndOldType)
            {
                Contract.Assume(overridingLabels != null); // F: As of Clousot suggestion - It seems something is wrong with visibility check, so we made it an assume

                int endOldIndex = overridingLabels.Count;
                CFGBlock beginBlock;
                int correspondingBeginOldIndex = PatchPriorBeginOld(this, endOldIndex, out beginBlock);
                overridingLabels.Add(EndOldMask | correspondingBeginOldIndex);
                this.Subroutine.AddInferredOldMap(this.Index, endOldIndex, beginBlock, nextEndOldType);
            }

            private int PatchPriorBeginOld(CFGBlock endBlock, int endOldIndex, out CFGBlock beginBlock)
            {
                var startIndex = (this == endBlock) ? endOldIndex - 2 : this.Count - 1;
                for (int i = startIndex; i >= 0; i--)
                {
                    int dummy;
                    if (IsBeginOld(i, out dummy))
                    {
                        Contract.Assume(dummy == 0);
                        Contract.Assume(i < overridingLabels.Count);
                        overridingLabels[i] = BeginOldMask | endOldIndex;
                        beginBlock = this;
                        this.Subroutine.AddInferredOldMap(this.Index, i, endBlock, default(Type));
                        return i;
                    }
                }
                // if we get here the begin/end spans multiple blocks:
                var preds = this.subroutine.PredecessorBlocks(this).GetEnumerator();
                if (preds.MoveNext())
                {
                    var beginOldIndex = PatchPriorBeginOld(endBlock, endOldIndex, preds.Current, out beginBlock);
                    var next = preds.MoveNext();
                    Contract.Assume(!next); // F: made an assume
                    return beginOldIndex;
                }
                throw new InvalidOperationException("missing begin_old");
            }

            private static int PatchPriorBeginOld(CFGBlock endBlock, int endOldIndex, CFGBlock current, out CFGBlock beginBlock)
            {
                Contract.Requires(current != null); // F: it seems it is a precondition?

                EnsuresBlock<Label> pred = current as EnsuresBlock<Label>;
                if (pred == null)
                {
                    // skip this block
                    // if we get here the begin/end spans multiple blocks:

                    var preds = current.Subroutine.PredecessorBlocks(current).GetEnumerator();
                    if (preds.MoveNext())
                    {
                        var beginOldIndex = PatchPriorBeginOld(endBlock, endOldIndex, preds.Current, out beginBlock);
                        bool next = preds.MoveNext();
                        Contract.Assume(!next); // F: made an assume
                        return beginOldIndex;
                    }
                    throw new InvalidOperationException("missing begin_old");
                }
                return pred.PatchPriorBeginOld(endBlock, endOldIndex, out beginBlock);
            }
        }


        /// <summary>
        /// Used for separate blocks that call methods for which we have pre/post conditions
        /// </summary>
        internal class MethodCallBlock<Label> : BlockWithLabels<Label>
        {
            public readonly Method CalledMethod;
            public readonly bool Virtual;
            private readonly int parameterCount;

            public MethodCallBlock(Method method, bool virtcall, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, SubroutineBase<Label> container, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(mdDecoder != null);// F: Added as of Clousot suggestion
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion
                Contract.Ensures(idgen >= 0);

                this.CalledMethod = method;
                this.Virtual = virtcall;
                parameterCount = mdDecoder.Parameters(method).AssumeNotNull().Count;
            }

            public override bool IsMethodCallBlock<Method2>(out Method2 calledMethod, out bool isNewObj, out bool isVirtual)
            {
                if (this.CalledMethod is Method2)
                {
                    calledMethod = (Method2)(object)this.CalledMethod;
                    isNewObj = this.IsNewObj;
                    isVirtual = this.Virtual;
                    return true;
                }
                calledMethod = default(Method2);
                isNewObj = false;
                isVirtual = false;
                return false;
            }

            internal virtual bool IsNewObj
            {
                get { return false; }
            }
        }


        internal class NewObjCallBlock<Label> : MethodCallBlock<Label>
        {
            public NewObjCallBlock(Method method, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, SubroutineBase<Label> container, ref int idgen)
              : base(method, false, mdDecoder, container, ref idgen)
            {
                Contract.Requires(mdDecoder != null);// F: As of Clousot suggestion
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion

                Contract.Ensures(idgen >= 0);
            }

            internal override bool IsNewObj
            {
                get
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// an assume block that stores the return value of a function in the appropriate local var first
        /// </summary>
        /// <typeparam name="Label"></typeparam>
        internal class AssumeAsPostConditionEntryBlock<Label> : BlockWithLabels<Label>
        {
            public readonly Tag Tag;
            public readonly Local LocalRetval;
            public readonly Parameter ParamRetval;
            public readonly Field FieldRetval;
            private readonly AssumeAsPostConditionSubroutine<Label>.RetvalType RetvalType;

            public AssumeAsPostConditionEntryBlock(SubroutineBase<Label> container, Local localRetval, Parameter paramRetval, Field fieldRetval, AssumeAsPostConditionSubroutine<Label>.RetvalType retvalType, string tag, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);
                Contract.Requires(idgen >= 0);

                Contract.Ensures(idgen >= 0);

                this.Tag = tag;
                this.LocalRetval = localRetval;
                this.ParamRetval = paramRetval;
                this.FieldRetval = fieldRetval;
                RetvalType = retvalType;
            }

            public override int Count { get { return 2; } } // synthetic instruction

            #region ISelfDecode<Label,Method,Type> Members

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                if (pc.Index == 0)
                {
                    return visitor.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);
                }
                else if (pc.Index == 1)
                {
                    switch (RetvalType)
                    {
                        case (AssumeAsPostConditionSubroutine<Label>.RetvalType.LocalRetval):
                            {
                                return visitor.Stloc(pc, this.LocalRetval, Unit.Value, data);
                            }
                        case (AssumeAsPostConditionSubroutine<Label>.RetvalType.ParameterRetval):
                            {
                                return visitor.Starg(pc, this.ParamRetval, Unit.Value, data);
                            }
                        case (AssumeAsPostConditionSubroutine<Label>.RetvalType.FieldRetval):
                            {
                                return visitor.Stfld(pc, this.FieldRetval, false, Unit.Value, Unit.Value, data);
                            }
                        default:
                            {
                                return visitor.Nop(pc, data);
                            }
                    }
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
            }
            #endregion
        }

        internal class AssumeAsPostConditionExitBlock<Label> : BlockWithLabels<Label>
        {
            public readonly Tag Tag;
            public readonly Local Retval;
            public readonly Label BranchLabel;
            public AssumeAsPostConditionExitBlock(SubroutineBase<Label> container, Local retval, Label label, string tag, ref int idgen)
                : base(container, ref idgen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion
                Contract.Ensures(idgen >= 0);

                this.Tag = tag;
                this.BranchLabel = label;
                this.Retval = retval;
            }

            public override int Count { get { return 1; } } // synthetic instruction

            #region ISelfDecode<Label,Method,Type> Members

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                if (pc.Index == 0)
                {
                    return visitor.Ldloc(pc, this.Retval, Unit.Value, data);
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
            }

            #endregion
        }

        internal class AssumeBlock<Label> : BlockWithLabels<Label>
        {
            public readonly Tag Tag;
            public readonly Label BranchLabel;
            public AssumeBlock(SubroutineBase<Label> container, Label label, string tag, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion

                Contract.Ensures(idgen >= 0);

                this.Tag = tag;
                this.BranchLabel = label;
            }

            public override int Count { get { return 1; } } // synthetic instruction



            #region ISelfDecode<Label,Method,Type> Members

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                if (pc.Index == 0)
                {
                    return visitor.Assume(pc, this.Tag, Unit.Value, null, data);
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
            }

            #endregion
        }


        /// <summary>
        /// Encodes 3 synthetic instructions
        ///
        ///   ldc c
        ///   ceq
        ///   assume
        ///
        /// </summary>
        internal class SwitchCaseAssumeBlock<Label> : BlockWithLabels<Label>
        {
            private readonly Label SwitchLabel;
            private readonly object Pattern;
            private readonly Type Type;

            public SwitchCaseAssumeBlock(SubroutineBase<Label> container, Label switchLabel, object pattern, Type type, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);
                Contract.Requires(idgen >= 0);
                Contract.Ensures(idgen >= 0);

                SwitchLabel = switchLabel;
                Pattern = pattern;
                Type = type;
            }

            public override int Count { get { return 3; } } // synthetic instructions


            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                if (pc.Index == 0)
                {
                    return visitor.Ldconst(pc, Pattern, Type, Unit.Value, data);
                }
                else if (pc.Index == 1)
                {
                    return visitor.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);
                }
                else if (pc.Index == 2)
                {
                    return visitor.Assume(pc, "true", Unit.Value, null, data);
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
            }
        }

        /// <summary>
        /// Compares switched value against all constants c_i suing the following structure
        /// </summary>
        internal class SwitchDefaultAssumeBlock<Label> : BlockWithLabels<Label>
        {
            #region Object invariant
            [ContractInvariantMethod]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
            private void ObjectInvariant()
            {
                Contract.Invariant(Patterns != null);
            }
            #endregion

            private readonly Label SwitchLabel;
            private readonly List<object> Patterns;
            private readonly Type Type;

            public SwitchDefaultAssumeBlock(SubroutineBase<Label> container, Label label, List<object> patterns, Type type, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion

                Contract.Ensures(idgen >= 0);

                SwitchLabel = label;
                Patterns = patterns;
                Type = type;
            }

            public override int Count { get { return Patterns.Count * 4 + 1; } } // synthetic instruction

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                int caseNum = pc.Index / 4;
                if (caseNum < Patterns.Count)
                {
                    int withinInstr = pc.Index % 4;
                    switch (withinInstr)
                    {
                        case 0:
                            return visitor.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);
                        case 1:
                            Contract.Assume(caseNum >= 0);
                            return visitor.Ldconst(pc, Patterns[caseNum], Type, Unit.Value, data);
                        case 2:
                            return visitor.Binary(pc, BinaryOperator.Cne_Un, Unit.Value, Unit.Value, Unit.Value, data);
                        case 3:
                            return visitor.Assume(pc, "true", Unit.Value, null, data);
                    }
                }
                else if (caseNum == Patterns.Count && pc.Index % 4 == 0)
                {
                    // finally pop it.
                    return visitor.Pop(pc, Unit.Value, data);
                }
                return visitor.Nop(pc, data);
            }
        }

        /// <summary>
        /// Special entry and exit block that has one program point for Entry synthetic instruction or Ret
        /// </summary>
        internal class EntryExitBlock<Label> : BlockWithLabels<Label>
        {
            public EntryExitBlock(SubroutineBase<Label> container, ref int idGen) : base(container, ref idGen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idGen >= 0);// F: As of Clousot suggestion
            }

            public override int Count { get { return 1; } } /// synthetic instruction
        }

        /// <summary>
        /// Special entry block that self decodes first instruction to Entry
        /// </summary>
        internal class EntryBlock<Label> : EntryExitBlock<Label>
        {
            public EntryBlock(SubroutineBase<Label> container, ref int idGen)
              : base(container, ref idGen)
            {
                Contract.Requires(container != null);
                Contract.Requires(idGen >= 0);

                Contract.Ensures(idGen >= 0);
            }

            internal override Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
            {
                if (pc.Index == 0 && pc.SubroutineContext == null && this.Subroutine.IsMethod)
                {
                    IMethodInfo<Method> ms = (IMethodInfo<Method>)this.Subroutine;
                    return visitor.Entry(pc, ms.Method, data);
                }
                return visitor.Nop(pc, data);
            }
        }

        /// <summary>
        /// Used to mark catch filter header blocks.
        /// </summary>
        internal class CatchFilterEntryBlock<Label> : BlockWithLabels<Label>
        {
            public CatchFilterEntryBlock(SubroutineBase<Label> container, ref int idgen)
              : base(container, ref idgen)
            {
                Contract.Requires(container != null);// F: As of Clousot suggestion
                Contract.Requires(idgen >= 0);// F: As of Clousot suggestion
            }
        }



        internal abstract class SubroutineBase<Label> : Subroutine, IGraph<CFGBlock, Unit>, IStackInfo, IEdgeSubroutineAdaptor
        {
            #region -------------- Fields ------------------------

            private BlockWithLabels<Label> entry;
            private readonly BlockWithLabels<Label> exit;
            private readonly CatchFilterEntryBlock<Label> exceptionExit;
            protected readonly Label startLabel;
            private BlockWithLabels<Label> entryAfterRequires; // possibly null

            protected int blockIdGenerator = 0;

            /// <summary>
            /// Holds the map from labels that start a block to their block
            /// </summary>
            protected Dictionary<Label, BlockWithLabels<Label>> BlockStart = new Dictionary<Label, BlockWithLabels<Label>>();

            protected readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache;
            protected SubroutineBuilder<Label> Builder;

            private readonly List<Pair<CFGBlock, Pair<string, CFGBlock>>> successors = new List<Pair<CFGBlock, Pair<string, CFGBlock>>>();

            internal readonly ICodeProvider<Label, Local, Parameter, Method, Field, Type> CodeProvider; // F: it seems it may be null

            protected CFGBlock[] blocks;

            /// <summary>
            /// Stored in last to first order (thus easy to append)
            ///
            /// The string is a tag for what kind of call-edge it is.
            /// </summary>
            protected OnDemandMap<Pair<CFGBlock, CFGBlock>, FList<Pair<string, Subroutine>>> edgeSubroutines;

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(successors != null);
                Contract.Invariant(this.MethodCache != null);
                Contract.Invariant(entry != null);
                Contract.Invariant(exit != null);
                Contract.Invariant(exceptionExit != null);
                Contract.Invariant(this.blockIdGenerator >= 0);
                Contract.Invariant(this.BlockStart != null || this.Builder == null);
            }

            #endregion // fields

            #region -------------- Private helpers --------------

            internal AssumeAsPostConditionEntryBlock<Label> NewAssumeAsPostConditionEntryBlock(Local localRetval, Parameter paramRetval, Field fieldRetval, AssumeAsPostConditionSubroutine<Label>.RetvalType retvalType, Tag tag)
            {
                return new AssumeAsPostConditionEntryBlock<Label>(this, localRetval, paramRetval, fieldRetval, retvalType, tag, ref this.blockIdGenerator);
            }

            internal AssumeAsPostConditionExitBlock<Label> NewAssumeAsPostConditionExitBlock(Local retval, Label assume, Tag tag)
            {
                return new AssumeAsPostConditionExitBlock<Label>(this, retval, assume, tag, ref this.blockIdGenerator);
            }

            internal AssumeBlock<Label> NewAssumeBlock(Label current, Tag tag)
            {
                return new AssumeBlock<Label>(this, current, tag, ref this.blockIdGenerator);
            }

            internal SwitchCaseAssumeBlock<Label> NewSwitchCaseAssumeBlock(Label current, object pattern, Type type)
            {
                return new SwitchCaseAssumeBlock<Label>(this, current, pattern, type, ref this.blockIdGenerator);
            }

            internal SwitchDefaultAssumeBlock<Label> NewSwitchDefaultAssumeBlock(Label current, List<object> patterns, Type type)
            {
                return new SwitchDefaultAssumeBlock<Label>(this, current, patterns, type, ref this.blockIdGenerator);
            }

            #endregion -------------------------------------------

            #region ------------ Protected Helpers -------------

            internal virtual BlockWithLabels<Label> NewBlock()
            {
                Contract.Ensures(Contract.Result<BlockWithLabels<Label>>() != null);

                return new BlockWithLabels<Label>(this, ref this.blockIdGenerator);
            }

            protected SubroutineBase(
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache
            )
            {
                Contract.Requires(methodCache != null);

                this.MethodCache = methodCache;
                entry = new EntryBlock<Label>(this, ref this.blockIdGenerator);
                exit = new EntryExitBlock<Label>(this, ref this.blockIdGenerator);
                exceptionExit = new CatchFilterEntryBlock<Label>(this, ref this.blockIdGenerator);
            }
            protected SubroutineBase(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                     Label startLabel,
                                     SubroutineBuilder<Label> builder)
              : this(methodCache)
            {
                Contract.Requires(methodCache != null);
                Contract.Requires(builder != null); // F: added because of Clousot suggestion

                this.startLabel = startLabel;
                this.Builder = builder;
                this.CodeProvider = builder.CodeProvider;

                // link entry block to start label
                entryAfterRequires = this.GetTargetBlock(startLabel);
                AddSuccessor(entry, "entry", entryAfterRequires);
            }

            internal abstract override void Initialize();

            internal virtual void Commit()
            {
                Contract.Assume(blocks == null); // make sure we don't double commit

                if (this.Builder != null)
                {
                    this.Builder.InsertPreConditionEdges(entry, entryAfterRequires, this);
                }
                // cleanup
                PostProcessBlocks();
            }

            internal void AddSuccessor(CFGBlock from, Tag tag, CFGBlock target)
            {
                Contract.Requires(from != null);
                Contract.Requires(target != null);
                Contract.Requires(from.Subroutine == target.Subroutine);

                Contract.Assume(from != this.Exit);// F: requires added because of Clousot suggestion. 
                                                   // F: updated: made an assumption as there are too many places were we should fix it. 
                                                   // TODO: think to a way of improving it

                Debug.Assert(from.Subroutine == target.Subroutine);
                this.AddNormalControlFlowEdge(successors, from, tag, target);
            }

            internal BlockWithLabels<Label> GetTargetBlock(Label label)
            {
                Contract.Ensures(Contract.Result<BlockWithLabels<Label>>() != null);

                Contract.Assume(this.Builder != null); // should not get called otherwise
                Contract.Assume(this.Builder.IsTargetLabel(label));
                return GetBlock(label);
            }

            [ContractVerification(false)]
            internal BlockWithLabels<Label> GetBlock(Label label)
            {
                Contract.Ensures(Contract.Result<BlockWithLabels<Label>>() != null);

                Contract.Assume(this.Builder != null); // should not get called otherwise

                BlockWithLabels<Label> newBlock;
                if (!this.BlockStart.TryGetValue(label, out newBlock))
                {
                    Pair<Method, bool> calledMethodVirtPair;
                    Method calledMethod;
                    if (this.Builder.IsMethodCallSite(label, out calledMethodVirtPair))
                    {
                        newBlock = new MethodCallBlock<Label>(calledMethodVirtPair.One, calledMethodVirtPair.Two, this.MethodCache.MetadataDecoder.AssumeNotNull(), this, ref this.blockIdGenerator);
                    }
                    else if (this.Builder.IsNewObjSite(label, out calledMethod))
                    {
                        newBlock = new NewObjCallBlock<Label>(calledMethod, this.MethodCache.MetadataDecoder.AssumeNotNull(), this, ref this.blockIdGenerator);
                    }
                    else
                    {
                        newBlock = this.NewBlock();
                    }
                    // only add branch targets to this map
                    Contract.Assume(this.Builder != null); // we need history invariants
                    if (this.Builder.IsTargetLabel(label))
                    {
                        this.BlockStart.Add(label, newBlock);
                    }
                }
                else
                {
                    Contract.Assume(newBlock != null);
                }
                return newBlock;
            }


            /// <summary>
            /// Note that subroutine can be null, in which case we add nothing.
            /// </summary>
            sealed public override void AddEdgeSubroutine(CFGBlock from, CFGBlock to, Subroutine subroutine, string callTag)
            {
                if (subroutine == null) return;
                FList<Pair<string, Subroutine>> sofar;
                Pair<CFGBlock, CFGBlock> edge = new Pair<CFGBlock, CFGBlock>(from, to);
                this.edgeSubroutines.TryGetValue(edge, out sofar);
                this.edgeSubroutines[edge] = FList<Pair<string, Subroutine>>.Cons(new Pair<string, Subroutine>(callTag, subroutine), sofar);
            }

            /// <summary>
            /// Returns registered edge subroutines in last to first order.
            ///
            /// The context is used to filter subroutines on this list as follows:
            /// - Any contract subroutines returned are distinct from the current subroutine and any subroutines in the context
            /// </summary>
            FList<Pair<string, Subroutine>> IEdgeSubroutineAdaptor.GetOrdinaryEdgeSubroutinesInternal(CFGBlock from, CFGBlock to, SubroutineContext context)
            {
                FList<Pair<string, Subroutine>> sofar;
                Pair<CFGBlock, CFGBlock> edge = new Pair<CFGBlock, CFGBlock>(from, to);
                this.edgeSubroutines.TryGetValue(edge, out sofar);
                if (sofar != null && context != null)
                {
                    sofar = sofar.Filter(FilterRecursiveContracts(to, context));
                }
                return sofar;
            }

            /// <summary>
            /// Used to specialize ensures subroutines based on calling context.
            /// We are looking for a stack of contract calls (ensures/requires) where each call is on "this" except maybe 
            /// the outermost one. Since the calls are on "this", we can use the declaring type of the called method as
            /// a possible improvement of the dynamic type other than the final declaring type. E.g., the ensures about to be
            /// inserted might be the ensures of ICollection.Count, called from within ICollection.CopyTo's requires.
            /// But the call to CopyTo happens to be a call to Array.CopyTo (inheriting ICollection.CopyTo's requires). Thus,
            /// we can determine that the Count ensures to be used is the one from Array, which specializes ICollection.Count's 
            /// ensures to relate the Count to the array length.
            /// </summary>
            [ContractVerification(false)]
            public override FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, FList<STuple<CFGBlock, CFGBlock, string>> context)
            {
                var toAPC = new APC(to, 0, context);
                CallAdaption.Push(this);
                try
                {
                    var origContext = context;
                    var result = CallAdaption.Dispatch<IEdgeSubroutineAdaptor>(this).GetOrdinaryEdgeSubroutinesInternal(from, to, context);

                    if (toAPC.InsideContract)
                    {
                        if (context != null)
                        {
                            if (result != null)
                            {
                                #region inside a contract
                                var mdDecoder = this.MethodCache.MetadataDecoder;
                                Method calledMethod;
                                bool isNewObj;
                                bool isVirtualCall;
                                if (from.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall) && isVirtualCall)
                                {
                                    // try to specialize any ensures based on calling context

                                    // find out if call is on "this" (using Stack provider information)
                                    if (CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(from, 0, null)))
                                    {
                                        Type bestType = mdDecoder.DeclaringType(calledMethod); // so far
                                                                                               // find best declaring type on outer calls and use it to specialize ensures.
                                        do
                                        {
                                            // this is really a do loop (as we tested for context != null on the outside
                                            if (context.Head.Three.StartsWith("inherited") || context.Head.Three.StartsWith("extra") || context.Head.Three.StartsWith("old")) { context = context.Tail; continue; }
                                            Method outerCalledMethod;
                                            bool dummy;
                                            bool dummy2;

                                            if (context.Head.Three.StartsWith("after") && context.Head.One.IsMethodCallBlock(out outerCalledMethod, out dummy, out dummy2))
                                            {
                                                Type candidateType = mdDecoder.DeclaringType(outerCalledMethod);
                                                if (mdDecoder.DerivesFromIgnoringTypeArguments(candidateType, bestType))
                                                {
                                                    bestType = candidateType;
                                                }
                                                // we can try for even better types if the outer call is also on this
                                                if (!CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(context.Head.One, 0, null)))
                                                {
                                                    break; // get out of do loop
                                                }
                                            }
                                            else if (context.Head.Three.StartsWith("before") && context.Head.Two.IsMethodCallBlock(out outerCalledMethod, out dummy, out dummy2))
                                            {
                                                Type candidateType = mdDecoder.DeclaringType(outerCalledMethod);
                                                if (mdDecoder.DerivesFromIgnoringTypeArguments(candidateType, bestType))
                                                {
                                                    bestType = candidateType;
                                                }
                                                // we can try for even better types if the outer call is also on this
                                                if (!CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(context.Head.Two, 0, null)))
                                                {
                                                    break; // get out of do loop
                                                }
                                            }
                                            else if (context.Head.Three == "exit")
                                            {
                                                // specialize by method we are in.
                                                var im = context.Head.One.Subroutine as IMethodInfo<Method>;
                                                if (im != null)
                                                {
                                                    var candidateType = mdDecoder.DeclaringType(im.Method);
                                                    if (mdDecoder.DerivesFromIgnoringTypeArguments(candidateType, bestType))
                                                    {
                                                        bestType = candidateType;
                                                    }
                                                }
                                                break; // get out of do loop
                                            }
                                            else if (context.Head.Three == "entry")
                                            {
                                                // specialize by method we are in.
                                                var im = context.Head.One.Subroutine as IMethodInfo<Method>;
                                                if (im != null)
                                                {
                                                    var candidateType = mdDecoder.DeclaringType(im.Method);
                                                    if (mdDecoder.DerivesFromIgnoringTypeArguments(candidateType, bestType))
                                                    {
                                                        bestType = candidateType;
                                                    }
                                                }
                                                break; // get out of do loop
                                            }
                                            else
                                            {
                                                // no specialization possible
                                                return result;
                                            }
                                            // loop around
                                            context = context.Tail;
                                        } while (context != null);
                                        // if we get here, we may have found a better type:
                                        if (!mdDecoder.Equal(bestType, mdDecoder.DeclaringType(calledMethod)))
                                        {
                                            Method specializedMethod;
                                            if (mdDecoder.TryGetImplementingMethod(bestType, calledMethod, out specializedMethod))
                                            {
                                                result = SpecializeEnsures(result, this.MethodCache.GetEnsures(calledMethod), this.MethodCache.GetEnsures(specializedMethod), from, origContext);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        // context could be finally
                        var mdDecoder = this.MethodCache.MetadataDecoder;
                        Method calledMethod;
                        bool isNewObj;
                        bool isVirtualCall;
                        if (from.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall))
                        {
                            // try to specialize any ensures/invariant based on current method
                            // find out if call is on "this" (using Stack provider information)
                            if (CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(from, 0, null)))
                            {
                                // specialize by method we are in.
                                var im = from.Subroutine as IMethodInfo<Method>;
                                if (im != null)
                                {
                                    var bestType = mdDecoder.DeclaringType(im.Method);
                                    if (isVirtualCall)
                                    {
                                        Method specializedMethod;
                                        if (mdDecoder.TryGetImplementingMethod(bestType, calledMethod, out specializedMethod))
                                        {
                                            result = SpecializeEnsures(result, this.MethodCache.GetEnsures(calledMethod), this.MethodCache.GetEnsures(specializedMethod), from, origContext);
                                        }
                                    }
                                    // insert invariant call if there is one and we are not calling an auto setter
                                    result = InsertInvariant(from, result, mdDecoder, calledMethod, im, ref bestType, context);
                                }
                            }
                            else if (this.MethodCache.IsMonitorWaitOrExit(calledMethod))
                            {
                                // special handling of Monitor.Exit and Monitor.Wait. These methods cause havocing of the
                                // running object and we'd like to reestablish the invariant
                                Method containingMethod;
                                if (toAPC.TryGetContainingMethod<Method>(out containingMethod))
                                {
                                    var candidate = this.MethodCache.GetInvariant(mdDecoder.DeclaringType(containingMethod));
                                    if (candidate != null)
                                    {
                                        // use entry tag to use current "this" as the target object
                                        result = result.Cons(new Pair<string, Subroutine>("entry", candidate));
                                    }
                                }
                            }
                            result = TryInsertAssumeInvariant(from, result, mdDecoder, calledMethod, context);
                        }
                    }
                    return result;
                }
                finally
                {
                    CallAdaption.Pop(this);
                }
            }

            private FList<Pair<string, Subroutine>> InsertInvariant(CFGBlock from, FList<Pair<string, Subroutine>> result, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, Method calledMethod, IMethodInfo<Method> im, ref Type bestType, SubroutineContext context)
            {
                Contract.Requires(from != null);
                Contract.Requires(mdDecoder != null);

                Contract.Assume(this.MethodCache.MetadataDecoder != null); // Should add a postcondition to MethodCache instead

                if (this.MethodCache.MetadataDecoder.IsPropertySetter(calledMethod) && (this.MethodCache.MetadataDecoder.IsAutoPropertyMember(calledMethod) || WithinConstructor(from, context)))
                {
                    return result;
                }
                // if the call is a ctor call, then this is the base call and we should not specialize
                if (mdDecoder.IsConstructor(calledMethod))
                {
                    bestType = mdDecoder.DeclaringType(calledMethod);
                }
                var candidate = this.MethodCache.GetInvariant(bestType);
                if (candidate != null)
                {
                    MethodCallBlock<Label> callBlock = from as MethodCallBlock<Label>;
                    if (callBlock != null)
                    {
                        string tag = callBlock.IsNewObj ? "afterNewObj" : "afterCall";

                        result = result.Cons(new Pair<string, Subroutine>(tag, candidate));
                    }
                }
                return result;
            }

            private FList<Pair<string, Subroutine>> TryInsertAssumeInvariant(CFGBlock from, FList<Pair<string, Subroutine>> result, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, Method calledMethod, SubroutineContext context)
            {
                Contract.Requires(from != null);
                Contract.Requires(mdDecoder != null);

                var unspecMethod = mdDecoder.Unspecialized(calledMethod);
                if (!mdDecoder.IsVoidMethod(unspecMethod)) return result;
                // if the call is a ctor call, then this is the base call and we should not specialize
                if (mdDecoder.IsConstructor(unspecMethod))
                {
                    return result;
                }

                if (mdDecoder.Name(unspecMethod) != "AssumeInvariant") return result;
                var parameters = mdDecoder.Parameters(calledMethod);
                Contract.Assume(parameters != null);
                if (parameters.Count != 1) return result;

                var p = parameters[0];
                var invariantType = this.MethodCache.MetadataDecoder.ParameterType(p);
                var candidate = this.MethodCache.GetInvariant(invariantType);
                if (candidate != null)
                {
                    MethodCallBlock<Label> callBlock = from as MethodCallBlock<Label>;
                    if (callBlock != null)
                    {
                        if (callBlock.IsNewObj) return result; // no insertion here
                        string tag = "assumeInvariant";

                        result = result.Cons(new Pair<string, Subroutine>(tag, candidate));
                    }
                }
                return result;
            }

            private bool WithinConstructor(CFGBlock from, SubroutineContext context)
            {
                Contract.Requires(from != null);// F: As of Clousot suggestion
                return new APC(from, 0, context).InsideConstructor;
            }

            /// <summary>
            /// Make sure not to specialize to a subroutine already in the context, otherwise we create a recursive context.
            /// </summary>
            private FList<Pair<string, Subroutine>> SpecializeEnsures(FList<Pair<string, Subroutine>> subs, Subroutine toReplace, Subroutine specializedEnsures, CFGBlock from, SubroutineContext context)
            {
                return subs.Map(pair =>
                {
                    var specialized = SpecializeEnsures(pair.Two, toReplace, specializedEnsures);
                    if (from.Subroutine == specialized) return pair;
                    for (var list = context; list != null; list = list.Tail)
                    {
                        if (list.Head.One.Subroutine == specialized) return pair;
                    }
                    return new Pair<string, Subroutine>(pair.One, specialized);
                });
            }

            /// <summary>
            /// Ensures can only be specialized if the call is virtual. Invariants, we always do.
            /// </summary>
            private Subroutine SpecializeEnsures(Subroutine sub, Subroutine toReplace, Subroutine specializedEnsures)
            {
                var mdDecoder = this.MethodCache.MetadataDecoder;
                if (sub == toReplace)
                {
                    return specializedEnsures;
                }
                return sub;
            }

            bool IStackInfo.IsCallOnThis(APC pc)
            {
                // default implementation does not try to refine
                return false;
            }

            private static Predicate<Pair<string, Subroutine>> FilterRecursiveContracts(CFGBlock from, SubroutineContext context)
            {
                return delegate (Pair<string, Subroutine> candidatePair)
                {
                    var candidate = candidatePair.Two;
                    if (!candidate.IsContract) return true;
                    if (candidate == from.Subroutine) return false; // direct recursion
                    for (var filter = context; filter != null; filter = filter.Tail)
                    {
                        if (candidate == filter.Head.One.Subroutine) return false;
                    }
                    return true;
                };
            }

            /// <summary>
            /// Call back when a return statement is found. Usually ignored but MethodSubroutine wants to know about it
            /// </summary>
            internal virtual void AddReturnBlock(BlockWithLabels<Label> block) { }

            #endregion ------------------------------------


            public override CFGBlock Entry
            {
                get { return entry; }
            }

            public void SetEntry(CFGBlock newEntry)
            {
                entry = newEntry as BlockWithLabels<Label>;
            }

            public override CFGBlock EntryAfterRequires
            {
                get { if (entryAfterRequires != null) { return entryAfterRequires; } else { return this.Entry; } }
            }

            public override CFGBlock Exit
            {
                get { return exit; }
            }

            public override CFGBlock ExceptionExit
            {
                get { return exceptionExit; }
            }

            public override bool HasReturnValue
            {
                get { return false; } // default
            }

            public override bool HasContextDependentStackDepth
            {
                get { return true; } // default
            }

            public override IEnumerable<CFGBlock> SuccessorBlocks(CFGBlock block)
            {
                foreach (Pair<Tag, CFGBlock> edge in this.SuccessorEdges[block])
                {
                    yield return edge.Two;
                }
            }

            public override IEnumerable<CFGBlock> PredecessorBlocks(CFGBlock block)
            {
                foreach (Pair<Tag, CFGBlock> edge in this.PredecessorEdges[block])
                {
                    yield return edge.Two;
                }
            }

            public override bool IsCatchFilterHeader(CFGBlock block)
            {
                return block is CatchFilterEntryBlock<Label>;
            }

            /// <summary>
            /// Number of distinct syntactic blocks.
            /// </summary>
            public override int BlockCount
            {
                get
                {
                    Contract.Assume(this.blocks != null, "otherwise Commit has not been called");

                    return this.blocks.Length;
                }
            }

            /// <summary>
            /// The blocks are numbered from 0-n. For syntactic analysis (ignoring finally context)
            /// an array can be used to map from block to information by using the block.index as key.
            /// </summary>
            public override IEnumerable<CFGBlock> Blocks { get { return this.blocks; } }

            public override string Name
            {
                get { return "SR" + this.Id.ToString(); }
            }

            #region ------------ Internal overrides ------------------


            public override bool IsSubroutineStart(CFGBlock block)
            {
                return block == entry;
            }

            public override bool IsSubroutineEnd(CFGBlock block)
            {
                return block == exit || block == exceptionExit;
            }

            public override bool IsJoinPoint(CFGBlock block)
            {
                // catch and filter handler starts are join points
                if (this.IsCatchFilterHeader(block)) return true;
                // prevents us from stepping in/out of straight line code across subroutines
                if (this.IsSubroutineStart(block)) return true;
                if (this.IsSubroutineEnd(block)) return true;
                return (this.PredecessorEdges[block].Count > 1);
            }

            public override bool IsSplitPoint(CFGBlock block)
            {
                // prevents us from stepping in/out of straight line code across subroutines
                if (this.IsSubroutineStart(block)) return true;
                if (this.IsSubroutineEnd(block)) return true;
                return (SuccessorEdges[block].Count > 1);
            }

            internal override DepthFirst.Visitor<CFGBlock, Unit>/*?*/ EdgeInfo
            {
                get
                {
                    Contract.Assume(edgeInfo != null, "otherwise PostProcess has not been called yet");
                    return edgeInfo;
                }
            }

            internal string SourceContext(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");

                if (this.CodeProvider.HasSourceContext(label))
                {
                    var len = this.CodeProvider.SourceLength(label);
                    if (0 < len)
                        return String.Format("{0}({1},{2}-{3},{4})",
                          this.CodeProvider.SourceDocument(label),
                          this.CodeProvider.SourceStartLine(label),
                          this.CodeProvider.SourceStartColumn(label),
                          this.CodeProvider.SourceEndLine(label),
                          this.CodeProvider.SourceEndColumn(label)
                          );
                    else
                        return String.Format("{0}({1},{2})", this.CodeProvider.SourceDocument(label), this.CodeProvider.SourceStartLine(label), this.CodeProvider.SourceStartColumn(label));
                }
                return null;
            }
            internal string SourceDocument(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");

                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceDocument(label);
                }
                return null;
            }
            internal int SourceStartLine(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceStartLine(label);
                }
                return 0;
            }
            internal int SourceEndLine(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceEndLine(label);
                }
                return 0;
            }
            internal int SourceStartColumn(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceStartColumn(label);
                }
                return 0;
            }
            internal int SourceEndColumn(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceEndColumn(label);
                }
                return 0;
            }
            internal int SourceStartIndex(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceStartIndex(label);
                }
                return 0;
            }
            internal int SourceLength(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                if (this.CodeProvider.HasSourceContext(label))
                {
                    return this.CodeProvider.SourceLength(label);
                }
                return 0;
            }

            internal int ILOffset(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                return this.CodeProvider.ILOffset(label);
            }

            public override int StackDelta
            {
                get { return 0; }
            }

            #endregion --------------------------------------

            #region Code scanning and block building



            private void AddNormalControlFlowEdge(List<Pair<CFGBlock, Pair<string, CFGBlock>>> succs,
                                                  CFGBlock from, string tag, CFGBlock to)
            {
                Contract.Requires(succs != null); // F: added because of Clousot suggestion
                Contract.Requires(from != this.Exit); // F: added because of Clousot suggestion

                Debug.Assert(from != this.Exit);
                succs.Add(new Pair<CFGBlock, Pair<string, CFGBlock>>(from, new Pair<string, CFGBlock>(tag, to)));
            }


            #endregion

            #region Subroutine computations
            internal override bool HasSingleSuccessor(APC ppoint, out APC next, DConsCache consCache)
            {
                Contract.Assert(consCache != null);
                //
                // This works as follows:
                //  1) determine if we are at the end of a block, if not, just provide the successor
                //  2) if this point ends a finally handler, find the next handler or pop the stack
                //  3) determine syntactic successor labels
                //     a) for each such label, determine finally nesting difference with current finally scope
                //     b) push executed finallies onto finally stack and make top be the new current PC
                // If this is an endfinally, there must be at least one label

                // We allow stepping past the last instruction in this block!
                if (ppoint.Index < ppoint.Block.Count)
                {
                    next = new APC(ppoint.Block, ppoint.Index + 1, ppoint.SubroutineContext);
                    return true;
                }

                if (IsSubroutineEnd(ppoint.Block))
                {
                    // Could be end of entire CFG
                    if (ppoint.SubroutineContext == null)
                    {
                        next = APC.Dummy;
                        return false;
                    }
                    next = ComputeSubroutineContinuation(ppoint, consCache);
                    return true;
                }

                BlockWithLabels<Label> singleSucc = null;
                foreach (BlockWithLabels<Label> succ in ppoint.Block.Subroutine.SuccessorBlocks(ppoint.Block))
                {
                    if (singleSucc == null)
                    {
                        singleSucc = succ;
                    }
                    else
                    {
                        // more than one
                        next = APC.Dummy;
                        return false;
                    }
                }
                if (singleSucc != null)
                {
                    next = ComputeTargetFinallyContext(ppoint, singleSucc, consCache);
                    return true;
                }

                // zero
                next = APC.Dummy;
                return false;
            }

            internal override bool HasSinglePredecessor(APC ppoint, out APC singlePredecessor, DConsCache consCache, bool skipContracts)
            {
                //
                // This works as follows:
                //  1) determine if we are at the beginning of a block, if not, just provide the predecessor
                //  2) if this point starts a finally handler, find the previous handler or pop the stack
                //     NOTE: if the popping is necessary and the target of the edge is a catch handler, then
                //           the predecessors are all indices in the block where the exception is thrown.
                //  3) determine syntactic predecessors
                //     a) for each such label, determine finally nesting difference with current finally scope
                //     b) push edge onto finally stack and make top be the new current PC
                if (ppoint.Index > 0)
                {
                    singlePredecessor = new APC(ppoint.Block, ppoint.Index - 1, ppoint.SubroutineContext);
                    return true;
                }

                // If this is the beginning of the subroutine, get the next one
                if (IsSubroutineStart(ppoint.Block))
                {
                    // could be beginning of entire CFG
                    if (ppoint.SubroutineContext == null)
                    {
                        singlePredecessor = APC.Dummy;
                        return false;
                    }
                    bool hasSinglePred;
                    singlePredecessor = this.ComputeSubroutinePreContinuation(ppoint, out hasSinglePred, consCache, skipContracts);
                    return hasSinglePred;
                }

                CFGBlock/*?*/ singlePred = null;
                foreach (CFGBlock pred in ppoint.Block.Subroutine.PredecessorBlocks(ppoint.Block))
                {
                    if (singlePred != null)
                    {
                        singlePredecessor = APC.Dummy;
                        return false;
                    }
                    singlePred = pred;
                }
                if (singlePred == null)
                {
                    // 0
                    singlePredecessor = APC.Dummy;
                    return false;
                }

                FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(singlePred, ppoint.Block, ppoint.SubroutineContext);

                diffs = SkipContracts(skipContracts, diffs);
                if (diffs == null)
                {
                    singlePredecessor = APC.ForEnd(singlePred, ppoint.SubroutineContext);
                    return true;
                }

                // now diff is in outermost to inner most order of fault/finallies we are traversing.
                // 1) push edge onto fault/finally context stack
                // 2) for each endfinally in innermost handler, yield a program point
                // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
                //

                Contract.Assert(consCache != null); // F: made the assumption explicit
                SubroutineContext newFinallyContext = consCache(new SubroutineEdge(singlePred, ppoint.Block, diffs.Head.One), ppoint.SubroutineContext);

                // 3) find outermost handler (Head) and its subroutine
                // 4) transfer control to end of first (outermost) fault/finally
                //     (NOTE: diffs only contains finallies unless ppoint.Block is a catch/filter header)

                Subroutine subroutine = diffs.Head.Two;
                Contract.Assume(subroutine != null);

                singlePredecessor = APC.ForEnd(subroutine.Exit, newFinallyContext);
                return true;
            }

            internal override APC PredecessorPCPriorToRequires(APC pc, DConsCache consCache)
            {
                // must be at head of block
                if (pc.Index != 0) return pc;

                var currBlock = pc.Block;
                Method dummy;
                bool dummyBool;
                bool dummyIsVirtual;
                if (currBlock.IsMethodCallBlock(out dummy, out dummyBool, out dummyIsVirtual))
                {
                    var preds = this.PredecessorBlocks(currBlock).AsIndexable(1);
                    if (preds.Count == 1)
                    {
                        var predBlock = preds[0];
                        Contract.Assume(predBlock != null);
                        return APC.ForEnd(predBlock, pc.SubroutineContext);
                    }
                }
                return pc;
            }

            /// <summary>
            /// Returns all normal control flow successors
            /// </summary>
            internal override IEnumerable<APC>/*!*/ Successors(APC ppoint, DConsCache consCache)
            {
                APC singleNext;
                if (HasSingleSuccessor(ppoint, out singleNext, consCache))
                {
                    yield return singleNext;
                    yield break;
                }
                //
                //  determine syntactic successor labels
                //     a) for each such label, determine finally nesting difference with current finally scope
                //     b) push executed finallies onto finally stack and make top be the new current PC

                int successors = 0;
                foreach (BlockWithLabels<Label> succ in ppoint.Block.Subroutine.SuccessorBlocks(ppoint.Block))
                {
                    successors++;
                    yield return ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint, succ, consCache);
                }

                if (successors == 0)
                {
                    // The following is too strong, as throw in a finally block aborts the finally continuation
                    // System.Diagnostics.Debug.Assert(ppoint.FaultFinallyContext == null);

                    // the following is too strong, as the current pc could also be a throw.
                    // However, we don't know how to compute this.
                    // System.Diagnostics.Debug.Assert(ppoint.Block == this.normalExit || ppoint.Block == this.exceptionExit);
                }
            }

            internal override IEnumerable<APC> Predecessors(APC ppoint, DConsCache consCache, bool skipContracts)
            {
                //
                // This works as follows:
                //  1) determine if we are at the beginning of a block, if not, just provide the predecessor
                //  2) if this point starts a finally handler, find the previous handler or pop the stack
                //  3) determine syntactic predecessors
                //     a) for each such label, determine finally nesting difference with current finally scope
                //     b) push edge onto finally stack and make top be the new current PC
                if (ppoint.Index > 0)
                {
                    yield return new APC(ppoint.Block, ppoint.Index - 1, ppoint.SubroutineContext);
                    yield break;
                }

                if (IsSubroutineStart(ppoint.Block))
                {
                    // Could be start of entire CFG
                    if (ppoint.SubroutineContext == null)
                    {
                        yield break;
                    }
                    // 
                    foreach (APC nestedpred in this.ComputeSubroutinePreContinuation(ppoint, consCache, skipContracts))
                    {
                        yield return nestedpred;
                    }
                    yield break;
                }

                foreach (CFGBlock pred in ppoint.Block.Subroutine.PredecessorBlocks(ppoint.Block))
                {
                    FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(pred, ppoint.Block, ppoint.SubroutineContext);

                    diffs = SkipContracts(skipContracts, diffs);
                    if (diffs == null)
                    {
                        yield return APC.ForEnd(pred, ppoint.SubroutineContext);
                    }
                    else
                    {
                        // now diff is in outermost to inner most order of fault/finallies we are traversing.
                        // 1) push edge onto fault/finally context stack
                        // 2) yield exit of subroutine as program point
                        // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
                        //
                        SubroutineContext newFinallyContext = consCache(new SubroutineEdge(pred, ppoint.Block, diffs.Head.One), ppoint.SubroutineContext);

                        // 2) find outermost handler (Head)
                        // 3) transfer control to end of first (outermost) fault/finally
                        Subroutine subroutine = diffs.Head.Two;
                        yield return APC.ForEnd(subroutine.Exit, newFinallyContext);
                    }
                }
            }

            private static FList<Pair<Tag, Subroutine>> SkipContracts(bool skipContracts, FList<Pair<string, Subroutine>> diffs)
            {
                if (skipContracts)
                {
                    while (diffs != null && (diffs.Head.Two.IsContract || diffs.Head.Two.IsOldValue))
                    {
                        diffs = diffs.Tail;
                    }
                }
                return diffs;
            }

            /// <summary>
            /// Called when ppoint is beginning of a subroutine on backwards walk.
            ///
            /// Finds next subroutine on current edge, or pops edge
            ///
            /// Note: we distinguish between edges that target an exception handler and thus need
            /// to execute fault and finally handlers and other edges that only execute finally handlers
            /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
            /// block. These blocks are needed, as the start of a catch handler could also be the normal
            /// goto/leave target of some try finally within the catch handler itself. Thus, the special
            /// block disinguishes between a throw target where faults must be traversed, and a normal
            /// leave target to the beginning of a catch handler.
            /// </summary>
            private IEnumerable<APC> ComputeSubroutinePreContinuation(APC ppoint, DConsCache consCache, bool skipContracts)
            // ^ requires ppoint.FinallyEdges != null;
            {
                SubroutineEdge edge = ppoint.SubroutineContext.Head;
                bool isHandlerEdge;
                FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, out isHandlerEdge, ppoint.SubroutineContext.Tail);
                Debug.Assert(diffs != null);
                while (diffs.Head.Two != this)
                {
                    diffs = diffs.Tail;
                }

                // skip this
                diffs = diffs.Tail;

                diffs = SkipContracts(skipContracts, diffs);
                // diffs.Head is new handler or none
                if (diffs == null)
                {
                    // If isHandlerEdge, then each label in the source block of the edge is a target
                    if (isHandlerEdge)
                    {
                        for (int index = 0; index == 0 || index < edge.One.Count; index++)
                        {
                            yield return new APC(edge.One, index, ppoint.SubroutineContext.Tail);
                        }
                    }
                    else
                    {
                        yield return APC.ForEnd(edge.One, ppoint.SubroutineContext.Tail);
                    }
                    yield break;
                }
                // predecessor is end of the next inner handler
                Subroutine nextSubroutine = diffs.Head.Two;
                yield return APC.ForEnd(nextSubroutine.Exit, consCache(new SubroutineEdge(edge.One, edge.Two, diffs.Head.One), ppoint.SubroutineContext.Tail));
            }

            /// <summary>
            /// Called when ppoint is beginning of a finally on backwards walk.
            ///
            /// Finds next handler on current edge, or pops edge
            ///
            /// Note: we distinguish between edges that target an exception handler and thus need
            /// to execute fault and finally handlers and other edges that only execute finally handlers
            /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
            /// block. These blocks are needed, as the start of a catch handler could also be the normal
            /// goto/leave target of some try finally within the catch handler itself. Thus, the special
            /// block disinguishes between a throw target where faults must be traversed, and a normal
            /// leave target to the beginning of a catch handler.
            /// </summary>
            [ContractVerification(false)]
            private APC ComputeSubroutinePreContinuation(APC ppoint, out bool hasSinglePred, DConsCache consCache, bool skipContracts)
            // ^ requires ppoint.FinallyEdges != null;
            {
                SubroutineEdge edge = ppoint.SubroutineContext.Head;
                bool isHandlerEdge;
                FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, out isHandlerEdge, ppoint.SubroutineContext.Tail);
                Contract.Assume(diffs != null);
                while (diffs.Head.Two != this)
                {
                    diffs = diffs.Tail;
                }
                diffs = diffs.Tail; // skip this

                diffs = SkipContracts(skipContracts, diffs);
                // diffs.Head is new handler or none
                if (diffs == null)
                {
                    // If isHandlerEdge, then each label in the source block of the edge is a target
                    if (isHandlerEdge && edge.One.Count > 1)
                    {
                        // no single pred
                        hasSinglePred = false;
                        return APC.Dummy;
                    }
                    hasSinglePred = true;
                    return APC.ForEnd(edge.One, ppoint.SubroutineContext.Tail);
                }
                // predecessor is end of the next inner handler
                Subroutine nextSubroutine = diffs.Head.Two;
                // only one end
                hasSinglePred = true;
                return APC.ForEnd(nextSubroutine.Exit, consCache(new SubroutineEdge(edge.One, edge.Two, diffs.Head.One), ppoint.SubroutineContext.Tail));
            }

            /// <summary>
            /// Called when ppoint is an endfinally.
            ///
            /// Finds next handler on current edge, or pops edge
            ///
            /// Note: we distinguish between edges that target an exception handler and thus need
            /// to execute fault and finally handlers and other edges that only execute finally handlers
            /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
            /// block. These blocks are needed, as the start of a catch handler could also be the normal
            /// goto/leave target of some try finally within the catch handler itself. Thus, the special
            /// block disinguishes between a throw target where faults must be traversed, and a normal
            /// leave target to the beginning of a catch handler.
            /// </summary>
            [ContractVerification(false)]
            private APC ComputeSubroutineContinuation(APC ppoint, DConsCache consCache)
            // ^ requires ppoint.SubroutineContext != null;
            {
                SubroutineEdge edge = ppoint.SubroutineContext.Head;

                FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, ppoint.SubroutineContext.Tail);
                Contract.Assume(diffs != null);
                if (diffs.Head.Two == this)
                {
                    // last handler executed.
                    // pop finally edge
                    return new APC(edge.Two, 0, ppoint.SubroutineContext.Tail);
                }
                while (diffs.Tail.Head.Two != this)
                {
                    diffs = diffs.Tail;
                }
                // diffs.Head is new handler
                Subroutine nextSubroutine = diffs.Head.Two;
                return new APC(nextSubroutine.Entry, 0, consCache(new SubroutineEdge(edge.One, edge.Two, diffs.Head.One), ppoint.SubroutineContext.Tail));
            }

            /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
            private FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, SubroutineContext context)
            {
                bool isExnHandler;
                return EdgeSubroutinesOuterToInner(current, succ, out isExnHandler, context);
            }

            /// <summary>
            /// Returns the difference in fault/finally handlers between the given program point and the
            /// given successor. Normally, the list only includes finally handlers, except in the case 
            /// where succ is a special catch/filter header block, indicating that this edge represents
            /// the execution on a throw, and thus fault handlers are included
            /// </summary>
            /// <param name="isExceptionHandlerEdge">is true, if the successor is a catch/filter handler</param>
            /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
            public override FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context)
            {
                if (current.Subroutine != this)
                {
                    // dispatch
                    return current.Subroutine.EdgeSubroutinesOuterToInner(current, succ, out isExceptionHandlerEdge, context);
                }

                bool includeFaults = IsCatchFilterHeader(succ);
                isExceptionHandlerEdge = includeFaults;

                return this.GetOrdinaryEdgeSubroutines(current, succ, context);
            }

            internal override APC ComputeTargetFinallyContext(APC ppoint, CFGBlock succ, DConsCache consCache)
            {
                FList<Pair<string, Subroutine>> diffs = EdgeSubroutinesOuterToInner(ppoint.Block, succ, ppoint.SubroutineContext);

                if (diffs == null)
                {
                    return new APC(succ, 0, ppoint.SubroutineContext);
                }
                // now diff is in outermost to inner most order of finallies we are traversing.
                // 1) push edge onto fault/finally context stack
                // 2) make innermost handler start current program point
                // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
                //

                // 2) find innermost handler
                while (diffs.Length() > 1)
                {
                    diffs = diffs.Tail;
                }

                Subroutine innerMost = diffs.Head.Two;
                Contract.Assume(innerMost != null);
                SubroutineContext newFinallyContext = consCache(new SubroutineEdge(ppoint.Block, succ, diffs.Head.One), ppoint.SubroutineContext);

                // 3) transfer control to last (innermost) finally
                return new APC(innerMost.Entry, 0, newFinallyContext);
            }

            /// <summary>
            /// Returns a set of program points that are possible execution continuations if 
            /// an exception is raised at the given ppoint.
            ///
            /// The supplied predicate allows the client to determine which handlers are possible
            /// candidates. The predicate is called from closest enclosing to outermost.
            /// </summary>
            /// <param name="data">Arbitrary client data passed to the handler predicate</param>
            /// <returns>A set of continuation program points that represent how execution continues to a chosen handler.
            /// The continuation includes Finally and Fault executions that intervene between the given ppoint and a
            /// chosen handler.
            ///
            /// Hack. This only works if Type2 is Type. We do this because we don't want to expose Type in Subroutine and thus
            /// CFGBlock.
            /// </returns>
            internal override IEnumerable<CFGBlock> ExceptionHandlers<Data, Type2>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<Type2, Data> handlerPredicateArg)
            {
                // This kind of subroutine has no handlers except the exceptional exit point
                yield return exceptionExit;
            }

            #endregion

            #region Post Cleanup

            private DepthFirst.Visitor<CFGBlock, Unit>/*?*/ edgeInfo = null;

            private bool IsEdgeUsed(Pair<CFGBlock, Pair<string, CFGBlock>> edge)
            {
                return (edge.One.Index != UnusedBlockIndex);
            }

            private EdgeMap<string> successorEdges;
            private EdgeMap<string> predecessorEdges; // cache

            internal override EdgeMap<string> SuccessorEdges
            {
                get { return successorEdges; }
            }

            internal override EdgeMap<string> PredecessorEdges
            {
                get
                {
                    if (predecessorEdges == null)
                    {
                        //  compute predecessor map on demand
                        predecessorEdges = this.SuccessorEdges.ReversedEdges();
                    }
                    return predecessorEdges;
                }
            }

            private const int UnusedBlockIndex = Int16.MaxValue - 10;


            /// <summary>
            /// Removes unreachable blocks and reorders them in reverse post order for easier reading
            /// and for analysis.
            /// </summary>
            [ContractVerification(false)] // because this is like Dispose and it violates some invariants.
            protected void PostProcessBlocks()
            {
                Stack<CFGBlock> blockStack = new Stack<CFGBlock>();

                successorEdges = new EdgeMap<string>(successors);

                // choose only reachable blocks
                edgeInfo = new DepthFirst.Visitor<CFGBlock, Unit>(
                   this,
                   null,
                   // we use the post visit for the post order
                   delegate (CFGBlock block)
                   {
                       // we use a stack to obtain reverse post order
                       blockStack.Push(block);

                       // we approximate the backwards reverse post order here as the forward pre-order
                       // block.SetBackwardReversePostOrderIndex(ref backwardIndexGen);
                   },
                   null);

                // Make sure that the exception exit block appears last and the normal exit block 2nd to last
                // This also makes sure that these nodes appear in our list.
                edgeInfo.VisitSubGraphNonRecursive(exceptionExit);
                edgeInfo.VisitSubGraphNonRecursive(exit);
                edgeInfo.VisitSubGraphNonRecursive(entry);

                //
                //  Now, we have unreachable blocks (and unused edges)
                //  1) unnumber all the blocks so we can recognize which ones are dead
                foreach (Pair<CFGBlock, Pair<Tag, CFGBlock>> edge in successorEdges)
                {
                    int unused = UnusedBlockIndex;
                    edge.One.Renumber(ref unused);
                }
                //  2) renumber the blocks in the reverse post order
                int blockRenumber = 0;
                foreach (BlockWithLabels<Label> block in blockStack)
                {
                    Contract.Assume(block != null);
                    block.Renumber(ref blockRenumber);
                }

                //  3) remove edges that are of unused blocks
                this.SuccessorEdges.Filter(IsEdgeUsed);

                // 3a) compute reversed graph
                predecessorEdges = this.SuccessorEdges.ReversedEdges();

                int finishTime = 0;
                // 3b) renumber according to reverse post order 
                var predvisit = new DepthFirst.Visitor<CFGBlock, string>(
                   predecessorEdges,
                   null,
                   delegate (CFGBlock block)
                   {
                       block.SetReversePostOrderIndex(finishTime++);
                   },
                   null);

                predvisit.VisitSubGraphNonRecursive(exit);

                foreach (var block in blockStack)
                {
                    predvisit.VisitSubGraphNonRecursive(block);
                }


                //  4) resort the edges according to the new numbering
                //
                this.SuccessorEdges.ReSort();

                //  5) persist blocks as array
                this.blocks = blockStack.ToArray();

                //  6) cleanup
                this.BlockStart = null;
                this.Builder = null;

                // for testing:
                // Dominators.Compute(this, this.entry);
            }
            #endregion

            #region IGraph<CFGBlock<Label>,Pair<CFGBlock<Label>,CFGBlock<Label>>> Members

            IEnumerable<CFGBlock>/*!*/ IGraph<CFGBlock, Unit>.Nodes
            {
                get { return this.blocks; }
            }

            bool IGraph<CFGBlock, Unit>.Contains(CFGBlock node)
            {
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (blocks[i] == node) return true;
                }
                return false;
            }

            /// <summary>
            /// Provide all successors, normal and exceptional
            /// </summary>
            public virtual IEnumerable<Pair<Unit, CFGBlock>>/*!*/ Successors(CFGBlock node)
            {
                foreach (Pair<Tag, CFGBlock> normalSucc in this.SuccessorEdges[node])
                {
                    yield return new Pair<Unit, CFGBlock>(Unit.Value, normalSucc.Two);
                }

                if (node != exceptionExit)
                {
                    yield return new Pair<Unit, CFGBlock>(Unit.Value, exceptionExit);
                }
            }


            #endregion

            public override void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, IMutableSet<Pair<Subroutine, SubroutineContext>> printed)
            {
                var identifier = new Pair<Subroutine, SubroutineContext>(this, context);
                if (printed.Contains(identifier)) return; // already printed
                printed.Add(identifier);
                IMutableSet<Subroutine> subs = new Set<Subroutine>();
                IMethodInfo<Method> mi = this as IMethodInfo<Method>;
                string extraSRIdentification = (mi != null) ? String.Format("({0})", this.MethodCache.MetadataDecoder.FullName(mi.Method)) : null;

                Contract.Assume(tw != null); // F: Added. Should it be a precondition?

                if (context == null)
                {
                    tw.WriteLine("Subroutine SR{0} {1} {2}", this.Id, this.Kind, extraSRIdentification);
                }
                else
                {
                    tw.WriteLine("Subroutine SR{0} {1} {2} {3}", this.Id, this.Kind, new APC(this.Entry, 0, context), extraSRIdentification);
                }
                tw.WriteLine("-----------------");

                foreach (BlockWithLabels<Label> block in this.Blocks.AssumeNotNull())
                {
                    tw.Write("Block {0} ({1})", block.Index, block.ReversePostOrderIndex);
                    // revisit this. How should we print CFGs with subroutines?
                    if (this.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge)
                    {
                        tw.WriteLine(" (target of backedge)");
                        Debug.Assert(this.IsJoinPoint(block));
                    }
                    else if (this.IsJoinPoint(block))
                    {
                        tw.WriteLine(" (join point)");
                    }
                    else
                    {
                        tw.WriteLine();
                    }
                    tw.Write("  Predecessors: ");
                    foreach (Pair<string, CFGBlock> taggedPred in block.Subroutine.PredecessorEdges[block])
                    {
                        tw.Write("({0}, {1}) ", taggedPred.One, taggedPred.Two.Index);
                    }
                    tw.WriteLine();
                    PrintHandlers(tw, block);
                    tw.WriteLine("  Code:");
                    foreach (APC apc in block.APCs(context))
                    {
                        string sc = apc.PrimarySourceContext();
                        if (sc != null) tw.WriteLine(sc);

                        Contract.Assume(ilPrinter != null);

                        ilPrinter(apc, "    ", tw);
                    }
                    tw.Write("  Successors: ");
                    foreach (Pair<Tag, CFGBlock> taggedSucc in this.SuccessorEdges[block])
                    {
                        tw.Write("({0},{1}", taggedSucc.One, taggedSucc.Two.Index);
                        if (this.EdgeInfo.IsBackEdge(block, Unit.Value, taggedSucc.Two))
                        {
                            tw.Write(" BE");
                        }
                        FList<Pair<string, Subroutine>> edgesubs = this.GetOrdinaryEdgeSubroutines(block, taggedSucc.Two, context);
                        while (edgesubs != null)
                        {
                            subs.Add(edgesubs.Head.Two);
                            tw.Write(" SR{0}({1})", edgesubs.Head.Two.Id, edgesubs.Head.One);
                            edgesubs = edgesubs.Tail;
                        }
                        tw.Write(") ");
                    }
                    tw.WriteLine();
                    if (blockInfoPrinter != null)
                    {
                        blockInfoPrinter(new APC(block, block.Last.Index, context), "  ", tw);
                    }
                    tw.WriteLine();
                }

                PrintReferencedSubroutines(tw, subs, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
            }

            [ContractVerification(false)] // We cannot prove the invariant this.BlockStart == null ==> this.Builder == null
            protected virtual void PrintReferencedSubroutines(TextWriter tw, IMutableSet<Subroutine> subs, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, IMutableSet<Pair<Subroutine, SubroutineContext>> printed)
            {
                Contract.Requires(subs != null);

                // print edge subroutines
                foreach (Subroutine sb in subs)
                {
                    if (contextLookup == null)
                    {
                        sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, null, printed);
                    }
                    else
                    {
                        foreach (SubroutineContext newContext in contextLookup(sb.Entry))
                        {
                            sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, newContext, printed);
                        }
                    }
                }
            }

            protected virtual void PrintHandlers(TextWriter tw, BlockWithLabels<Label> block)
            {
                Contract.Requires(tw != null);

                tw.Write("  Handlers: ");
                // print out method wide handler (implicitly added in the appropriate query on CFGs)
                if (block != exceptionExit)
                {
                    tw.Write("{0} ", exceptionExit.Index);
                }
                tw.WriteLine();
            }

            internal override IEnumerable<Subroutine> UsedSubroutines(IMutableSet<int> alreadyFound)
            {
                foreach (var sublist in this.edgeSubroutines.Values)
                {
                    var list = sublist;
                    for (; list != null; list = list.Tail)
                    {
                        var sub = list.Head.Two;
                        if (alreadyFound.Contains(sub.Id)) continue;
                        alreadyFound.Add(sub.Id);
                        yield return sub;
                    }
                }
            }

            internal virtual string SourceAssertionCondition(Label label)
            {
                Contract.Assume(this.CodeProvider != null, "At this point expecting the CodeProvider != null");
                return this.CodeProvider.SourceAssertionCondition(label);
            }
        }

        internal abstract class SubroutineWithHandlers<Label, Handler> : SubroutineBase<Label>
        {
            /// <summary>
            /// Used during scanning to compute the protecting handlers in this subroutine only
            /// </summary>
            internal FList<Handler> CurrentProtectingHandlers = FList<Handler>.Empty; // protecting the current block
                                                                                      /// <summary>
                                                                                      /// Holds the block starting the filter code for filter handlers.
                                                                                      /// Materialized on demand.
                                                                                      /// </summary>
            protected OnDemandMap<Handler, BlockWithLabels<Label>> FilterCodeBlocks;
            protected OnDemandMap<Handler, BlockWithLabels<Label>> CatchFilterHeaders;
            internal OnDemandMap<Handler, Subroutine> FaultFinallySubroutines;
            /// <summary>
            /// For each block, this provides the enclosing handlers (finally,fault, and exceptions) targeted by an exception raised in this block.
            /// </summary>
            internal OnDemandMap<CFGBlock, FList<Handler>> ProtectingHandlers;


            new protected IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> CodeProvider
            {
                get
                {
                    Contract.Ensures(Contract.Result<IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler>>() != null);

                    return (IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler>)base.CodeProvider;
                }
            }

            internal SubroutineWithHandlers(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache)
             : base(methodCache)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
            }

            internal SubroutineWithHandlers(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                            SubroutineWithHandlersBuilder<Label, Handler> builder,
                                            Label entry)
              : base(methodCache, entry, builder)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }


            private bool IsFault(Handler handler)
            {
                return this.CodeProvider.IsFaultHandler(handler);
            }

            private FList<Handler> ProtectingHandlersList(CFGBlock block)
            {
                FList<Handler>/*?*/ result;
                ProtectingHandlers.TryGetValue(block, out result);
                return result;
            }

            private IEnumerable<Handler> GetProtectingHandlers(CFGBlock block)
            {
                return ProtectingHandlersList(block).GetEnumerable();
            }

            [ContractVerification(false)]
            internal BlockWithLabels<Label> CreateCatchFilterHeader(Handler handler, Label label)
            {
                BlockWithLabels<Label> newBlock;
                if (!this.BlockStart.TryGetValue(label, out newBlock))
                {
                    newBlock = new CatchFilterEntryBlock<Label>(this, ref this.blockIdGenerator);
                    this.CatchFilterHeaders.Add(handler, newBlock);
                    this.BlockStart.Add(label, newBlock);
                    if (this.CodeProvider.IsFilterHandler(handler))
                    {
                        // cache the filter code block which we need to lookup during exception paths
                        var filterCodeBlock = GetTargetBlock(this.CodeProvider.FilterDecisionStart(handler));
                        this.FilterCodeBlocks.Add(handler, filterCodeBlock);
                    }
                }
                return newBlock;
            }

            internal override IEnumerable<Subroutine> UsedSubroutines(IMutableSet<int> alreadySeen)
            {
                foreach (Subroutine ffsr in this.FaultFinallySubroutines.Values)
                {
                    yield return ffsr;
                }
                foreach (var sub in BaseUsedSubroutines(alreadySeen))
                {
                    yield return sub;
                }
            }
            private IEnumerable<Subroutine> BaseUsedSubroutines(IMutableSet<int> alreadySeen)
            {
                return base.UsedSubroutines(alreadySeen);
            }

            /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
            public override FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context)
            {
                if (current.Subroutine != this)
                {
                    // dispatch
                    return current.Subroutine.EdgeSubroutinesOuterToInner(current, succ, out isExceptionHandlerEdge, context);
                }
                // determine finally nesting difference
                FList<Handler> currentHandlers = this.ProtectingHandlersList(current);
                FList<Handler> newHandlers = this.ProtectingHandlersList(succ);

                bool includeFaults = IsCatchFilterHeader(succ);
                isExceptionHandlerEdge = includeFaults;

                // now we could pop some, push some, or both (pop then push)
                FList<Pair<string, Subroutine>>/*?*/ subroutines = this.GetOrdinaryEdgeSubroutines(current, succ, context);
                while (currentHandlers != newHandlers)
                {
                    if (currentHandlers.Length() >= newHandlers.Length())
                    {
                        Contract.Assume(currentHandlers != null);
                        // definite pop
                        Handler eh = currentHandlers.Head;
                        if (this.IsFaultOrFinally(eh))
                        {
                            if (!IsFault(eh) || includeFaults)
                            {
                                subroutines = FList<Pair<string, Subroutine>>.Cons(new Pair<string, Subroutine>("finally", this.FaultFinallySubroutines[eh]), subroutines);
                            }
                        }
                        currentHandlers = currentHandlers.Tail;
                    }
                    else
                    {
                        // definite push
                        newHandlers = newHandlers.Tail;
                    }
                }
                return subroutines;
            }

            public bool IsFaultOrFinally(Handler handler)
            {
                return this.CodeProvider.IsFaultHandler(handler) || this.CodeProvider.IsFinallyHandler(handler);
            }

            internal override IEnumerable<CFGBlock> ExceptionHandlers<Data, Type2>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<Type2, Data> handlerPredicateArg)
            {
                // Hack: cast to expected Type.
                IHandlerFilter<Type, Data> handlerPredicate = (IHandlerFilter<Type, Data>)handlerPredicateArg;

                // First, find handlers that protect ppoint, while being further out than innerSubroutine
                FList<Handler> protectingHandlers = this.ProtectingHandlersList(ppoint);

                if (innerSubroutine != null && innerSubroutine.IsFaultFinally)
                {
                    while (protectingHandlers != null)
                    {
                        if (this.IsFaultOrFinally(protectingHandlers.Head) && this.FaultFinallySubroutines[protectingHandlers.Head] == innerSubroutine)
                        {
                            protectingHandlers = protectingHandlers.Tail;
                            // found remaining handlers
                            break;
                        }
                        protectingHandlers = protectingHandlers.Tail;
                    }
                }

                // now we have the remaining handlers that protect the program point

                while (protectingHandlers != null)
                {
                    Handler handler = protectingHandlers.Head;
                    if (!this.IsFaultOrFinally(handler))
                    {
                        if (handlerPredicate != null)
                        {
                            bool stopPropagation = false;
                            if (this.CodeProvider.IsCatchHandler(handler))
                            {
                                if (handlerPredicate.Catch(data, this.CodeProvider.CatchType(handler), out stopPropagation))
                                {
                                    yield return this.CatchFilterHeaders[handler];
                                }
                            }
                            else
                            {
                                Debug.Assert(this.CodeProvider.IsFilterHandler(handler));
                                if (handlerPredicate.Filter(data, new APC(this.FilterCodeBlocks[handler], 0, null), out stopPropagation))
                                {
                                    yield return this.CatchFilterHeaders[handler];
                                }
                            }
                            if (stopPropagation) yield break; // no further handlers
                        }
                        else
                        {
                            // no predicate, add all handlers
                            yield return this.CatchFilterHeaders[handler];
                        }
                        if (this.CodeProvider.IsCatchAllHandler(handler))
                        {
                            yield break; // no further handlers
                        }
                    }
                    protectingHandlers = protectingHandlers.Tail;
                }

                // if we get here, the exception can escape the subroutine
                yield return this.ExceptionExit;
            }

            /// <summary>
            /// Provide all successors, normal and exceptional
            /// </summary>
            public override IEnumerable<Pair<Unit, CFGBlock>>/*!*/ Successors(CFGBlock node)
            {
                foreach (Pair<Tag, CFGBlock> normalSucc in this.SuccessorEdges[node])
                {
                    yield return new Pair<Unit, CFGBlock>(Unit.Value, normalSucc.Two);
                }

                foreach (Handler handler in this.GetProtectingHandlers(node))
                {
                    // Careful: for fault finally the block belongs to a different subroutine, so we ignore it here
                    //          for filter/catch, it is the special header block
                    if (this.IsFaultOrFinally(handler))
                    {
                        continue;
                    }
                    else
                    {
                        yield return new Pair<Unit, CFGBlock>(Unit.Value, this.CatchFilterHeaders[handler]);
                    }
                }
                if (node != this.ExceptionExit)
                {
                    yield return new Pair<Unit, CFGBlock>(Unit.Value, this.ExceptionExit);
                }
            }

            protected override void PrintReferencedSubroutines(TextWriter tw, IMutableSet<Subroutine> subs, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> blockInfoPrinter, Func<CFGBlock, IEnumerable<FList<STuple<CFGBlock, CFGBlock, string>>>> contextLookup, SubroutineContext context, IMutableSet<Pair<Subroutine, SubroutineContext>> printed)
            {
                foreach (Subroutine sb in this.FaultFinallySubroutines.Values)
                {
                    if (contextLookup == null)
                    {
                        sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
                    }
                    else
                    {
                        foreach (SubroutineContext newContext in contextLookup(sb.Entry))
                        {
                            sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, newContext, printed);
                        }
                    }
                }
                base.PrintReferencedSubroutines(tw, subs, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
            }

            protected override void PrintHandlers(TextWriter tw, BlockWithLabels<Label> block)
            {
                tw.Write("  Handlers: ");
                foreach (Handler handler in this.GetProtectingHandlers(block))
                {
                    if (this.IsFaultOrFinally(handler))
                    {
                        Subroutine sb = this.FaultFinallySubroutines[handler];
                        Contract.Assume(sb != null, "Making the assumption explicit");
                        tw.Write("SR{0} ", sb.Id);
                    }
                    else
                    {
                        BlockWithLabels<Label> handlerStart = this.CatchFilterHeaders[handler];
                        Contract.Assume(handlerStart != null);
                        tw.Write("{0} ", handlerStart.Index);
                    }
                }
                // print out method wide handler (implicitly added in the appropriate query on CFGs)
                if (block != this.ExceptionExit)
                {
                    tw.Write("{0} ", this.ExceptionExit.Index);
                }
                tw.WriteLine();
            }
        }

        /// <summary>
        /// Represents an entire method
        /// </summary>
        internal class MethodSubroutine<Label, Handler> : SubroutineWithHandlers<Label, Handler>, IMethodInfo<Method>
        {
            #region Object invariant

            [ContractInvariantMethod]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.MethodCache.MetadataDecoder != null);
            }

            #endregion

            private Set<BlockWithLabels<Label>> blocksEndingInReturn;

            /// <summary>
            /// Dummy one without a body
            /// </summary>
            internal MethodSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache, Method method)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                this.method = method;
            }

            private Method method;

            [ContractVerification(false)] // We cannot prove the invariant this.BlockStart == null ==> this.Builder == null
            public MethodSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                    Method method,
                                    SubroutineWithHandlersBuilder<Label, Handler> builder,
                                    Label startLabel)
              : base(methodCache, builder, startLabel)
            {
                Contract.Requires(builder != null);// F: Added as of Clousot suggestion
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                this.method = method;

                builder.BuildBlocks(startLabel, this);

                BlockWithLabels<Label> startLabelBlock = this.GetTargetBlock(startLabel);

                this.Commit();
                Type declaringType = MethodCache.MetadataDecoder.DeclaringType(method);

                Subroutine invariant = MethodCache.GetInvariant(declaringType);

                if (invariant != null && !MethodCache.MetadataDecoder.IsConstructor(method) && !MethodCache.MetadataDecoder.IsStatic(method))
                {
                    this.AddEdgeSubroutine(this.Entry, startLabelBlock, invariant, "entry"); // first assume invariant as that might validate some implicit obligations in requires
                    var requires = MethodCache.GetRequires(method);
                    if (requires != null)
                    {
                        this.AddEdgeSubroutine(this.Entry, startLabelBlock, requires, "entry");
                        this.AddEdgeSubroutine(this.Entry, startLabelBlock, MethodCache.GetRedundantInvariant(invariant, declaringType), "entry"); // redudant invariant assumption to handle disjunctions implied by requires better
                    }
                }
                else
                {
                    this.AddEdgeSubroutine(this.Entry, startLabelBlock, MethodCache.GetRequires(method), "entry");
                }
                if (blocksEndingInReturn != null)
                {
                    var ensuresSub = MethodCache.GetEnsures(method);

                    foreach (BlockWithLabels<Label> retBlock in blocksEndingInReturn)
                    {
                        if (!MethodCache.MetadataDecoder.IsStatic(method) &&
                            !MethodCache.ContractDecoder.IsPure(method) &&
                            !MethodCache.MetadataDecoder.IsFinalizer(method) &&
                            !MethodCache.MetadataDecoder.IsDispose(method))
                        {
                            this.AddEdgeSubroutine(retBlock, this.Exit, invariant, "exit");
                        }
                        this.AddEdgeSubroutine(retBlock, this.Exit, ensuresSub, "exit");
                    }

                    // insert dummy Old subroutine calls at beginning to materialize for Clousot
                    if (ensuresSub != null)
                    {
                        foreach (var nestedEnsuresSub in ensuresSub.UsedSubroutines())
                        {
                            if (nestedEnsuresSub.IsOldValue)
                            {
                                this.AddEdgeSubroutine(this.Entry, startLabelBlock, Microsoft.Research.CodeAnalysis.ILCodeProvider.Predefined.OldEvalPopSubroutine(MethodCache, nestedEnsuresSub), "oldmanifest");
                            }
                        }
                    }
                    // cleanup
                    blocksEndingInReturn = null;
                }
            }

            public override string Kind
            {
                get { return "method"; }
            }
            /// <summary>
            /// Method subroutines are eagerly initialized
            /// </summary>
            internal override void Initialize()
            {
            }

            /// <summary>
            /// Record which blocks end in returns
            /// </summary>
            internal override void AddReturnBlock(BlockWithLabels<Label> block)
            {
                if (blocksEndingInReturn == null)
                {
                    blocksEndingInReturn = new Set<BlockWithLabels<Label>>();
                }
                blocksEndingInReturn.Add(block);
                base.AddReturnBlock(block);
            }

            public override bool HasReturnValue
            {
                get
                {
                    return !this.MethodCache.MetadataDecoder.AssumeNotNull().IsVoidMethod(method);
                }
            }

            public override bool IsMethod
            {
                get
                {
                    return true;
                }
            }

            public override bool IsConstructor
            {
                get
                {
                    return (this.MethodCache.MetadataDecoder.IsConstructor(method));
                }
            }

            internal override bool IsCompilerGenerated
            {
                get
                {
                    return this.MethodCache.MetadataDecoder.IsCompilerGenerated(method);
                }
            }

            public override string Name
            {
                get
                {
                    return this.MethodCache.MetadataDecoder.FullName(method);
                }
            }

            public Method Method { get { return method; } }
        }

        internal abstract class FaultFinallySubroutineBase<Label, Handler> : SubroutineWithHandlers<Label, Handler>
        {
            protected FaultFinallySubroutineBase(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                                 SubroutineWithHandlersBuilder<Label, Handler> builder,
                                                 Label startLabel)
              : base(methodCache, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }

            internal override void Initialize()
            {
                // nothing to do. MethodSubroutine finalizes the fault finally subroutines.
            }

            public override bool HasContextDependentStackDepth
            {
                get
                {
                    return false; // always starts at 0
                }
            }

            public override bool IsFaultFinally
            {
                get
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Represents a fault region within a method
        /// </summary>
        internal class FaultSubroutine<Label, Handler> : FaultFinallySubroutineBase<Label, Handler>
        {
            public FaultSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                   SubroutineWithHandlersBuilder<Label, Handler> builder,
                                   Label startLabel)
              : base(methodCache, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }

            public override string Kind
            {
                get { return "fault"; }
            }
        }


        /// <summary>
        /// Represents a finally region within a method
        /// </summary>
        internal class FinallySubroutine<Label, Handler> : FaultFinallySubroutineBase<Label, Handler>
        {
            public FinallySubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                     SubroutineWithHandlersBuilder<Label, Handler> builder,
                                     Label startLabel)
              : base(methodCache, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }

            public override string Kind
            {
                get { return "finally"; }
            }
        }

        internal abstract class CallingContractSubroutine<Label> : SubroutineBase<Label>, IMethodInfo<Method>
        {
            private Method methodWithThisContract;
            new protected SimpleSubroutineBuilder<Label> Builder;

            public CallingContractSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                             Method method)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null);// F: Added as of Clousot suggestion


                methodWithThisContract = method;
            }

            public CallingContractSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                             Method method,
                                             SimpleSubroutineBuilder<Label> builder,
                                             Label startLabel)
              : base(methodCache, startLabel, builder)
            {
                Contract.Requires(methodCache != null);// F: Added as of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion

                this.Builder = builder;
                methodWithThisContract = method;
            }

            #region IMethodInfo<Method> Members

            public Method Method
            {
                get { return methodWithThisContract; }
            }

            #endregion

            public override bool IsContract
            {
                get
                {
                    return true;
                }
            }
        }

        internal class RequiresSubroutine<Label> : CallingContractSubroutine<Label>, IEquatable<RequiresSubroutine<Label>>
        {
            public RequiresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                      Method method,
                                      SimpleSubroutineBuilder<Label> builder, Label startLabel, IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
                Contract.Requires(inherited != null);// F: As of Clousot suggestion
                this.AddBaseRequires(this.GetTargetBlock(startLabel), inherited);
            }

            public override string Kind
            {
                get { return "requires"; }
            }

            internal override void Initialize()
            {
                if (Builder == null) return;

                Builder.BuildBlocks(startLabel, this);
                this.Commit();
                Builder = null;
            }

            public RequiresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache, Method method, IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(inherited != null);// F: As of Clousot suggestion
                this.AddSuccessor(this.Entry, "entry", this.Exit);
                this.AddBaseRequires(this.Exit, inherited);
                this.Commit(); // do it here, since we have no builder it won't get called later
            }

            /// <summary>
            /// Add subroutine calls to
            ///  - non-abstract virtual methods
            ///  - abstract methods with HasRequires
            /// </summary>
            private void AddBaseRequires(CFGBlock targetOfEntry, IFunctionalSet<Subroutine> inherited)
            {
                Contract.Requires(inherited != null);// F: As of Clousot suggestion

                foreach (Subroutine rs in inherited.Elements)
                {
                    this.AddEdgeSubroutine(this.Entry, targetOfEntry, rs, "inherited");
                }
            }

            public override bool IsRequires
            {
                get { return true; }
            }

            public override bool IsContract
            {
                get { return true; }
            }

            public bool Equals(RequiresSubroutine<Label> that)
            {
                return this.Id == that.Id;
            }
        }

        internal class SimpleSubroutine<Label> : SubroutineBase<Label>
        {
            public SimpleSubroutine(
              int stackDelta,
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              Label startLabel,
              SimpleSubroutineBuilder<Label> builder
            )
              : base(methodCache, startLabel, builder)
            {
                Contract.Requires(builder != null);// F: Added as of Clousot suggestion
                Contract.Requires(methodCache != null);// F: Added as of Clousot suggestion

                this.stackDelta = stackDelta;
                builder.BuildBlocks(startLabel, this);

                this.Commit();
            }

            private int stackDelta;

            public override int StackDelta
            {
                get
                {
                    return stackDelta;
                }
            }

            internal override void Initialize()
            {
            }

            public override string Kind
            {
                get { return "simple"; }
            }
        }


        internal class AssumeAsPostConditionSubroutine<Label> : SubroutineBase<Label>
        {
            public enum RetvalType { LocalRetval, ParameterRetval, FieldRetval };

            public AssumeAsPostConditionSubroutine(
              int stackDelta,
              MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              Local retvalLocal,
              Parameter retvalParameter,
              Field retvalField,
              Label startLabel,
              AssumeAsPostConditionSubroutineBuilder<Label> builder,
              RetvalType retvalType
            )
                : base(methodCache, startLabel, builder)
            {
                Contract.Requires(builder != null);
                Contract.Requires(methodCache != null);

                this.stackDelta = stackDelta;
                // if a local / param is assigned the return value of a function, we must put the symbolic var for return value into the appropriate local in order to compute the correct assume result
                var assumeEntryBlk = base.NewAssumeAsPostConditionEntryBlock(retvalLocal, retvalParameter, retvalField, retvalType, "assumeAsPostEntry");
                // HACK! had to change Entry to be mutable. This is probably bad, but is the only solution I could get to work. Will investigate alternatives later.
                this.SetEntry(assumeEntryBlk);
                var blk = builder.BuildBlocks(startLabel, assumeEntryBlk, this); // force decoding of assume
                this.Commit();
            }

            private int stackDelta;

            public override int StackDelta
            {
                get
                {
                    return stackDelta;
                }
            }

            internal override void Initialize()
            {
            }

            public override string Kind
            {
                get { return "assumeAsPost"; }
            }

            public override bool IsContract
            {
                get
                {
                    return true;
                }
            }
        }

        internal class OldValueSubroutine<Label> : CallingContractSubroutine<Label>
        {
            private BlockWithLabels<Label> beginOldBlock;
            private BlockWithLabels<Label> endOldBlock;

            public OldValueSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                      Method method,
                                      SimpleSubroutineBuilder<Label> builder, Label startLabel)
              : base(methodCache, method, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }

            public override string Kind
            {
                get { return "old"; }
            }

            public override int StackDelta
            {
                get
                {
                    return 1;
                }
            }

            public override bool IsOldValue
            {
                get { return true; }
            }

            internal override void Initialize()
            {
                // nothing to do. EnsuresSubroutine finalizes this subroutines.
            }


            internal void Commit(BlockWithLabels<Label> endOldBlock)
            {
                this.endOldBlock = endOldBlock;
                this.Commit();
            }

            internal void RegisterBeginBlock(BlockWithLabels<Label> newBlock)
            {
                beginOldBlock = newBlock;
            }

            internal APC BeginOldAPC(SubroutineContext context)
            {
                return new APC(beginOldBlock, 0, context);
            }

            internal APC EndOldAPC(SubroutineContext context)
            {
                Contract.Assume(endOldBlock != null); // F: made an assumption
                return new APC(endOldBlock, endOldBlock.Count - 1, context);
            }
        }

        internal class EnsuresSubroutine<Label> : CallingContractSubroutine<Label>, IEquatable<EnsuresSubroutine<Label>>
        {
            /// <summary>
            /// Requires call to Initialize eventually before use
            /// </summary>
            public EnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                     Method method,
                                     SimpleSubroutineBuilder<Label> builder, Label startLabel, IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method, builder, startLabel)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion

                CFGBlock startBlock = this.GetTargetBlock(startLabel);
                this.AddBaseEnsures(this.Entry, startBlock, inherited);
            }

            public override string Kind
            {
                get { return "ensures"; }
            }

            internal override void Initialize()
            {
                if (Builder == null) return;

                Builder.BuildBlocks(startLabel, this);

                this.Commit();
                Builder = null;
            }

            public EnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                    Method method,
                                    IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion

                this.AddSuccessor(this.Entry, "entry", this.Exit);
                this.AddBaseEnsures(this.Entry, this.Exit, inherited);
                this.Commit();
            }


            internal override BlockWithLabels<Label> NewBlock()
            {
                return new EnsuresBlock<Label>(this, ref this.blockIdGenerator);
            }

            /// <summary>
            /// Add subroutine calls to
            ///  - non-abstract virtual methods
            ///  - abstract methods with HasRequires
            /// </summary>
            private void AddBaseEnsures(CFGBlock fromBlock, CFGBlock toBlock, IFunctionalSet<Subroutine> inherited)
            {
                if (inherited == null) return;
                foreach (Subroutine es in inherited.Elements)
                {
                    this.AddEdgeSubroutine(fromBlock, toBlock, es, "inherited");
                }
            }


            /// <summary>
            /// This map is used by inferred old regions to map (index lsh 16) + block_id to the corresponding 
            /// block and type (for end old)
            /// </summary>
            private OnDemandMap<int, Pair<CFGBlock, Type>> inferredOldLabelReverseMap;

            internal static int GetKey(EnsuresSubroutine<Label> rs)
            {
                Contract.Requires(rs != null);// F: As of Clousot suggestion
                return rs.Id;
            }

            public override bool IsEnsures
            {
                get { return true; }
            }

            public override bool IsContract
            {
                get { return true; }
            }

            public bool Equals(EnsuresSubroutine<Label> that)
            {
                return this.Id == that.Id;
            }

            #region Committing

            private enum ScanState { OutsideOld, InsideOld, InsertingOld, InsertingOldAfterCall }

            /// <summary>
            /// Fixes up sequences of ldarga ...  ... to wrap them with begin_old end_old
            /// Visitor returns true if we are adding instructions to underlying block. Argument is original block index.
            ///
            /// We have to be very careful when the old sequence ends in a method call. If the method has arguments
            /// other than the old address, then we can't evaluate the method call in the old scope, since we wouldn't find
            /// all the arguments to it.
            /// So we have to prematurely end the old scope at such method calls.
            /// </summary>
            private class CommitScanState : MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, int, bool>,
              ICodeQuery<Label, Local, Parameter, Method, Field, Type, int, bool>
            {
                private ScanState state;
                private Type nextEndOldType;
                private EnsuresBlock<Label> currentBlock;
                private EnsuresSubroutine<Label> parent;

                private OnDemandMap<CFGBlock, ScanState> blockStartState;

                public CommitScanState(EnsuresSubroutine<Label> parent)
                {
                    this.parent = parent;
                    state = ScanState.OutsideOld;
                }

                public void StartBlock(EnsuresBlock<Label> block)
                {
                    Contract.Requires(block != null);

                    if (!blockStartState.TryGetValue(block, out state))
                    {
                        state = ScanState.OutsideOld; // default
                        blockStartState.Add(block, state); // remember in case of bad control flow
                    }
                    if (state == ScanState.InsertingOld)
                    {
                        block.StartOverridingLabels();
                    }
                    if (state == ScanState.InsertingOldAfterCall)
                    {
                        // insert end old at head of block
                        block.StartOverridingLabels();
                        block.EndOldWithoutInstruction(nextEndOldType);
                        state = ScanState.OutsideOld;
                    }
                    currentBlock = block;
                }

                public void SetStartState(CFGBlock succ)
                {
                    // Debug.Assert(this.state != ScanState.InsertingOld); // can't insert old spanning blocks.
                    ScanState start;
                    if (!blockStartState.TryGetValue(succ, out start))
                    {
                        blockStartState.Add(succ, state);
                    }
                    else
                    {
                        Contract.Assume(start == state); // F: made an assume
                    }
                }

                #region Visitor
                protected override bool Default(Label pc, int index)
                {
                    if (state == ScanState.InsertingOld)
                    {
                        // something's odd: need to end old scope here. Could be loading only the address of
                        // a parameter and never dereferencing it, or passing it to a method among other parameters,
                        // in which case, we can't do much (mix of old and new in a method use).

                        // the end old type is actually a pointer
                        state = ScanState.OutsideOld;
                        var oldType = parent.MethodCache.AssumeNotNull().MetadataDecoder.ManagedPointer(nextEndOldType);
                        currentBlock.EndOldWithoutInstruction(oldType);

                        // still insert the current instruction (now after the end old)
                        return true;
                    }
                    return currentBlock.UsesOverriding;
                }

                public bool Aggregate(Label pc, Label aggStart, bool branchTarget, int data)
                {
                    return this.Nop(pc, data);
                }

                public override bool Nop(Label pc, int data)
                {
                    return currentBlock.UsesOverriding;
                }

                public override bool BeginOld(Label pc, Label matchingEnd, int index)
                {
                    Contract.Assume(state == ScanState.OutsideOld);
                    state = ScanState.InsideOld;
                    return currentBlock.UsesOverriding;
                }

                public override bool EndOld(Label pc, Label matchingBegin, Type type, Unit dest, Unit source, int index)
                {
                    Contract.Assume(state == ScanState.InsideOld);
                    state = ScanState.OutsideOld;
                    return currentBlock.UsesOverriding;
                }

                public override bool Ldarga(Label pc, Parameter argument, bool dummyIsOld, Unit dest, int index)
                {
                    if (state == ScanState.OutsideOld)
                    {
                        state = ScanState.InsertingOld;
                        currentBlock.BeginOld(index);
                        nextEndOldType = parent.MethodCache.AssumeNotNull().MetadataDecoder.ParameterType(argument);
                    }
                    return currentBlock.UsesOverriding;
                }

                public override bool Ldind(Label pc, Type type, bool @volatile, Unit dest, Unit ptr, int index)
                {
                    if (state == ScanState.InsertingOld)
                    {
                        // ends the old scope
                        state = ScanState.OutsideOld;
                        currentBlock.EndOld(index, nextEndOldType);
                        return false; // we already had to add this instruction before the end_old.
                    }
                    return currentBlock.UsesOverriding;
                }

                public override bool Ldfld(Label pc, Field field, bool @volatile, Unit dest, Unit obj, int index)
                {
                    if (state == ScanState.InsertingOld)
                    {
                        // ends the old scope
                        state = ScanState.OutsideOld;
                        currentBlock.EndOld(index, parent.MethodCache.AssumeNotNull().MetadataDecoder.FieldType(field));
                        return false; // we already had to add this instruction before the end_old.
                    }
                    return currentBlock.UsesOverriding;
                }

                public override bool Ldflda(Label pc, Field field, Unit dest, Unit obj, int data)
                {
                    if (state == ScanState.InsertingOld)
                    {
                        // keep track of eventual type in old
                        nextEndOldType = parent.MethodCache.AssumeNotNull().MetadataDecoder.FieldType(field);
                    }
                    return currentBlock.UsesOverriding;
                }

                /// <summary>
                /// Ends sequence of inserted old. Call is on struct address passed by value, thus in the old state
                /// Trickyness:
                ///  - Because Call statements appear in their own block, and post-conditions of the call need to be
                ///    evaluated also in the old state, we need to insert the EndOld statement in the next block
                ///  - The CFG construction is making sure that there are no back-to-back call blocks in ensures.
                /// </summary>
                public override bool Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, int index)
                {
                    Contract.Assume(false, "we should not be decoding method call blocks");
                    return false;
                }
                #endregion


                /// <summary>
                /// If we are inserting old, and this method has more than 1 parameter (including this), then
                /// we are out of luck. We cannot evaluate it in the oldstate, as the other parameters were pushed in 
                /// the new state.
                /// </summary>
                /// <param name="block"></param>
                internal void HandlePotentialCallBlock(MethodCallBlock<Label> block, EnsuresBlock<Label> priorBlock)
                {
                    // We comment this preconditon has it always brings to a violation. We did not saw it before because runtimechecking was not enabled for the ControlFlow Project
                    // Contract.Requires(priorBlock != null); 

                    if (block == null) return;

                    if (state == ScanState.InsertingOld)
                    {
                        // ends scope. Figure out if before call, or after call.
                        var paramCount = parent.MethodCache.AssumeNotNull().MetadataDecoder.AssumeNotNull().Parameters(block.CalledMethod).AssumeNotNull().Count;
                        if (!parent.MethodCache.AssumeNotNull().MetadataDecoder.AssumeNotNull().IsStatic(block.CalledMethod))
                        {
                            paramCount++;
                        }
                        Contract.Assert(parent.MethodCache != null);
                        Contract.Assume(parent.MethodCache.MetadataDecoder != null);
                        if (paramCount > 1)
                        {
                            if (priorBlock == null)
                            {
                                return; // F: nothing to do?
                            }

                            // end old in prior block (block before call block).
                            state = ScanState.OutsideOld;

                            // the end old type is actually a pointer
                            var oldType = parent.MethodCache.MetadataDecoder.ManagedPointer(nextEndOldType);
                            priorBlock.EndOldWithoutInstruction(oldType);
                        }
                        else
                        {
                            state = ScanState.InsertingOldAfterCall;
                            nextEndOldType = parent.MethodCache.MetadataDecoder.ReturnType(block.CalledMethod);
                        }
                    }
                }
            }

            [ContractVerification(false)] // We cannot prove the invariant this.BlockStart == null ==> this.Builder == null
            internal override void Commit()
            {
                base.Commit();
                var scan = new CommitScanState(this);

                EnsuresBlock<Label> priorBlock = null;

                foreach (var block in this.Blocks)
                {
                    // skip blocks that are not our special blocks.
                    EnsuresBlock<Label> b = block as EnsuresBlock<Label>;
                    if (b != null)
                    {
                        // save block in case next block is a call block. 
                        // (we assume that block order is sequential for straight line code)
                        priorBlock = b;
                        // scan block, keeping track if we are in old, inserting old, or outside old
                        // decode each instruction
                        // if ldarg or ldarga and we are not in old, add a begin_old bubble and start inserting_old
                        //   if inserting_old and current instruction is a memory deref (ldfld, ldind), add end_old after instruction 
                        //   and state is now outside old
                        // if reaching end of block while inserting old, insert an end_old (could be passing address by ref to a method)
                        int originalCount = b.Count; // do this before scan.StartBlock
                        scan.StartBlock(b);

                        for (int i = 0; i < originalCount; i++)
                        {
                            bool addInstruction = b.OriginalForwardDecode<int, bool, CommitScanState>(i, scan, i);
                            if (addInstruction)
                            {
                                b.AddInstruction(i);
                            }
                        }
                    }
                    else // might be a method call block
                    {
                        // Method call blocks end the old scope if we are in one
                        scan.HandlePotentialCallBlock(block as MethodCallBlock<Label>, priorBlock);
                    }
                    foreach (var succ in this.SuccessorBlocks(block))
                    {
                        scan.SetStartState(succ);
                    }
                }
            }
            #endregion


            internal void AddInferredOldMap(int blockIndex, int instructionIndex, CFGBlock otherBlock, Type endOldType)
            {
                inferredOldLabelReverseMap.Add(OverlayInstructionKey(blockIndex, instructionIndex), new Pair<CFGBlock, Type>(otherBlock, endOldType));
            }

            private static int OverlayInstructionKey(int blockIndex, int instruction)
            {
                var result = (instruction << 16) + blockIndex;
                return result;
            }

            internal CFGBlock InferredBeginEndBijection(APC pc)
            {
                Type dummy;
                return InferredBeginEndBijection(pc, out dummy);
            }

            internal CFGBlock InferredBeginEndBijection(APC pc, out Type endOldType)
            {
                Contract.Assume(pc.Block != null, "Assuming the object invariant");

                var key = OverlayInstructionKey(pc.Block.Index, pc.Index);
                Pair<CFGBlock, Type> data;
                if (!inferredOldLabelReverseMap.TryGetValue(key, out data))
                {
                    throw new ApplicationException("Fatal bug in ensures CFG begin/end old map");
                }
                endOldType = data.Two;
                return data.One;
            }
        }

        internal class ModelEnsuresSubroutine<Label> : EnsuresSubroutine<Label>
        {
            public ModelEnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                          Method method,
                                          SimpleSubroutineBuilder<Label> builder, Label startLabel, IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method, builder, startLabel, inherited)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion
            }

            public ModelEnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache, Method method, IFunctionalSet<Subroutine> inherited)
              : base(methodCache, method, inherited)
            {
                Contract.Requires(methodCache != null);// F: As of Clousot suggestion
            }

            public override bool IsModelEnsures
            {
                get
                {
                    return true;
                }
            }

            public override string Kind
            {
                get { return "model-ensures"; }
            }
        }

        internal class InvariantSubroutine<Label> : SubroutineBase<Label>, ITypeInfo<Type>
        {
            private new SimpleSubroutineBuilder<Label> Builder;
            private Type associatedType;

            public InvariantSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
                                       SimpleSubroutineBuilder<Label> builder, Label startLabel,
                                       Subroutine baseInv, Type associatedType
            )
              : base(methodCache, startLabel, builder)
            {
                Contract.Requires(methodCache != null);// F: Added as of Clousot suggestion
                Contract.Requires(builder != null);// F: As of Clousot suggestion

                Builder = builder;
                this.associatedType = associatedType;
                CFGBlock startBlock = this.GetTargetBlock(startLabel);
                this.AddBaseInvariant(this.Entry, startBlock, baseInv);
            }

            public InvariantSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache,
              Subroutine inherited, Type associatedType)
              : base(methodCache)
            {
                Contract.Requires(methodCache != null);// F: Added as of Clousot suggestion

                this.AddSuccessor(this.Entry, "entry", this.Exit);
                this.AddBaseInvariant(this.Entry, this.Exit, inherited);
                this.Commit(); // do it here, since we have no builder it won't get called later
            }

            public override string Kind
            {
                get { return "invariant"; }
            }

            internal override void Initialize()
            {
                if (Builder == null) return;

                Builder.BuildBlocks(startLabel, this);
                this.Commit();
                Builder = null;
            }

            /// <summary>
            /// Add subroutine calls to base invariant if non-null
            /// </summary>
            private void AddBaseInvariant(CFGBlock fromBlock, CFGBlock toBlock, Subroutine inherited)
            {
                this.AddEdgeSubroutine(fromBlock, toBlock, inherited, "inherited");
            }

            public override bool IsInvariant
            {
                get { return true; }
            }
            public override bool IsContract
            {
                get { return true; }
            }

            #region ITypeInfo<Type> Members

            Type ITypeInfo<Type>.AssociatedType
            {
                get { return associatedType; }
            }

            #endregion
        }

        #endregion // ------------- Nested Types --------------------

        /// <summary>
        /// Try to add a requires to a method. If the method inherits a requires, then this is not possible.
        /// </summary>
        /// <param name="identifying">Identifiying string (e.g. of pre-condition). Used to eliminate dups</param>
        /// <returns>true if added or already present</returns>
        public bool AddPreCondition<Label>(Method method, Label precondition, ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider)
        {
            // TODO: figure out whether we already analyzed a caller to this method. In that case, we can't strengthen the pre-condition.

            var original = GetRequires(method);

            if (original != null)
            {
                IMethodInfo<Method> mi = original as IMethodInfo<Method>;
                if (mi != null && !this.MetadataDecoder.Equal(mi.Method, method))
                {
                    // inherited, can't add
                    return false;
                }
            }

            var builder = new SimpleSubroutineBuilder<Label>(codeProvider, this, precondition);

            var newPre = new RequiresSubroutine<Label>(this, method, builder, precondition, FunctionalSet<Subroutine>.Empty());
            newPre.Initialize(); // not going through cache and lazy initialized
            if (original == null)
            {
                requiresCache.Install(method, newPre);
                return true;
            }
            else
            {
                foreach (var pred in original.PredecessorBlocks(original.Exit))
                {
                    original.AddEdgeSubroutine(pred, original.Exit, newPre, "extra");
                }
                return true;
            }
        }

        public bool RemovePreCondition(Method method)
        {
            return requiresCache.Remove(method);
        }

        public bool AddPostCondition<Label>(Method method, Label postCondition, ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider)
        {
            var original = GetEnsures(method);
            var builder = new SimpleSubroutineBuilder<Label>(codeProvider, this, postCondition);
            var newPost = new EnsuresSubroutine<Label>(this, method, builder, postCondition, FunctionalSet<Subroutine>.Empty());
            newPost.Initialize();

            if (original == null)
            {
                ensuresCache.Install(method, newPost);
                return true;
            }
            else
            {
                foreach (var pred in original.PredecessorBlocks(original.Exit))
                {
                    original.AddEdgeSubroutine(pred, original.Exit, newPost, "extra");
                }
                return true;
            }
        }

        [ContractVerification(false)]
        public bool AddWitness(Method method, bool mayReturnNull)
        {
            methodWitnesses[method] = mayReturnNull;

            return true; // Always succeed in this easy case
        }

        /// <summary>
        /// At the moment, we only say if a method may return null. TODO: expand to witnesses
        /// </summary>
        [ContractVerification(false)]
        public bool GetWitnessForMayReturnNull(Method method)
        {
            bool mayReturnNull;
            return methodWitnesses.TryGetValue(method, out mayReturnNull) && mayReturnNull;
        }

        public bool AddEntryAssume<Label>(ICFG cfg, Method caller, Label assume, ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider)
        {
            Contract.Requires(cfg != null);

            if (cfg.Subroutine.Blocks == null)
            {
                // Should not happen?
                return false;
            }

            bool installed = false;

            var from = cfg.Entry.Block.AssumeNotNull();
            var next = cfg.EntryAfterRequires.Block;

            var builder = new SimpleSubroutineBuilder<Label>(codeProvider, this, assume);
            var assumeSub = new RequiresSubroutine<Label>(this, caller, builder, assume, FunctionalSet<Subroutine>.Empty());
            assumeSub.Initialize();
            assumeSub.IsNecessaryAssumption = true;
            // install assume
            from.Subroutine.AddEdgeSubroutine(from, next, assumeSub, "entry");
            installed = true;
            return installed;
        }

        public bool AddCalleeAssumeAsPostCondition<Label>(ICFG cfg, Method caller, Method callee, Label assume, ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider)
        {
            Contract.Requires(cfg != null);

            if (cfg.Subroutine.Blocks == null)
            {
                // Should not happen?
                return false;
            }

            // (1) find the method that the assume is a postcondition of
            // (2) validate that the context is ok (vars reference in assume exist, are initialized, e.t.c) TODO
            // (3) install the assume 
            bool installed = false;
            // TODO: perform deeper traversal in case a subroutine contains our call of interest?
            foreach (var from in cfg.Subroutine.Blocks)
            {
                Method methodCall;
                bool constructor, virtualCall;
                if (from.IsMethodCallBlock(out methodCall, out constructor, out virtualCall))
                {
                    if (methodCall.Equals(callee)) // found method we were looking for
                    {
                        CFGBlock next = null;
                        // Hyp: there should be only one successor block, the ensures block
                        foreach (var cand in from.Subroutine.SuccessorBlocks(from)) // should be single successor here
                        {
                            next = cand;
                            break;
                        }
                        if (next == null) continue; // make sure we found *some* successor of the call; otherwise don't try to install

                        var builder = new SimpleSubroutineBuilder<Label>(codeProvider, this, assume);
                        var assumeSub = new EnsuresSubroutine<Label>(this, callee, builder, assume, null);
                        assumeSub.Initialize();
                        assumeSub.IsNecessaryAssumption = true;
                        // install assume
                        from.Subroutine.AddEdgeSubroutine(from, next, assumeSub, "afterCallAssume");
                        installed = true;
                    }
                }
            }
            return installed;
        }

        public bool AddInvariant<Label>(Type type, Label invariant, ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider)
        {
            var original = GetInvariant(type);

            if (original != null)
            {
                ITypeInfo<Type> ti = original as ITypeInfo<Type>;
                if (ti != null && !this.MetadataDecoder.Equal(ti.AssociatedType, type))
                {
                    // inherited, can't add
                    return false;
                }
            }

            var builder = new SimpleSubroutineBuilder<Label>(codeProvider, this, invariant);

            var newInvariant = new InvariantSubroutine<Label>(this, builder, invariant, original, type);
            newInvariant.Initialize();

            if (original == null)
            {
                invariantCache.Install(type, newInvariant);
                return true;
            }
            else
            {
                foreach (var pred in original.PredecessorBlocks(original.Exit))
                {
                    original.AddEdgeSubroutine(pred, original.Exit, newInvariant, "extra");
                }
                return true;
            }
        }


        internal Subroutine BuildSubroutine<Label>(
          int stackDelta,
          ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
          Label entryPoint)
        {
            var sb = new SimpleSubroutineBuilder<Label>(codeProvider, this, entryPoint);
            return new SimpleSubroutine<Label>(stackDelta, this, entryPoint, sb);
        }

        private static readonly IEnumerable<Field> emptyFields = new Field[0];
        private static readonly IEnumerable<Method> emptyMethods = new Method[0];

        /// <summary>
        /// We assume that we only call this on methods under analysis (never on methods from other assemblies)
        /// </summary>
        [ContractVerification(false)]
        internal IEnumerable<Field> GetModifies(Method method)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Field>>() != null);

            // make sure we computed the modifies info
            if (this.MetadataDecoder.HasBody(method))
            {
                this.GetCFG(method);

                Set<Field> result;
                if (methodModifies.TryGetValue(method, out result))
                {
                    Contract.Assume(result != null);
                    return result;
                }
            }
            return emptyFields;
        }

        /// <summary>
        /// We assume that we only call this on methods under analysis (never on methods from other assemblies)
        /// </summary>
        [ContractVerification(false)]
        internal IEnumerable<Field> GetReads(Method method)
        {
            // make sure we computed the modifies info
            if (this.MetadataDecoder.HasBody(method))
            {
                this.GetCFG(method);

                Set<Field> result;
                if (methodReads.TryGetValue(method, out result))
                {
                    return result;
                }
            }
            return emptyFields;
        }

        /// <summary>
        /// We assume that we only call this on fields of types under analysis (never on fields from other assemblies)
        /// </summary>
        [ContractVerification(false)]
        internal IEnumerable<Method> GetAffectedGetters(Field field)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

            // we can't really ensure that we computed this set for all methods in the type
            Set<Method> result;
            if (propertyReads.TryGetValue(field, out result))
            {
                Contract.Assume(result != null);
                return result;
            }

            return emptyMethods;
        }

        [ContractVerification(false)]
        private Set<Field> ModifiesSet(Method method)
        {
            Contract.Ensures(Contract.Result<Set<Field>>() != null);

            Set<Field> result;
            if (!methodModifies.TryGetValue(method, out result))
            {
                result = new Set<Field>();
                methodModifies.Add(method, result);
            }
            Contract.Assume(result != null);
            return result;
        }

        [ContractVerification(false)]
        private Set<Field> ReadSet(Method method)
        {
            Contract.Ensures(Contract.Result<Set<Field>>() != null);
            Set<Field> result;
            if (!methodReads.TryGetValue(method, out result))
            {
                result = new Set<Field>();
                methodReads.Add(method, result);
            }
            Contract.Assume(result != null);
            return result;
        }

        [ContractVerification(false)]
        private Set<Method> PropertySet(Field field)
        {
            Contract.Ensures(Contract.Result<Set<Method>>() != null);

            Set<Method> result;
            if (!propertyReads.TryGetValue(field, out result))
            {
                result = new Set<Method>();
                propertyReads.Add(field, result);
            }
            Contract.Assume(result != null);
            return result;
        }


        internal void AddModifies(Method method, Field field)
        {
            ModifiesSet(method).Add(field);
        }

        public bool IsMonitorWaitOrExit(Method method)
        {
            var t = this.MetadataDecoder.DeclaringType(method);
            if (this.MetadataDecoder.Name(t) != "Monitor") return false;
            var name = this.MetadataDecoder.Name(method);
            if (name == "Exit" || name == "Wait") return true;
            return false;
        }

        internal void AddReads(Method method, Field field)
        {
            this.ReadSet(method).Add(field);
            this.PropertySet(field).Add(method);
        }

        [ContractVerification(false)]
        public void RemoveContractsFor(Method method)
        {
            if (methodCache.ContainsKey(method))
            {
                methodCache.Remove(method);
            }
            if (methodModifies.ContainsKey(method))
            {
                methodModifies.Remove(method);
            }
            if (methodReads.ContainsKey(method))
            {
                methodReads.Remove(method);
            }
            requiresCache.Remove(method);
            ensuresCache.Remove(method);
            modelEnsuresCache.Remove(method);
        }
    }


    public class ControlFlow<Method, Type> : ICFG
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(contextCache != null);
            Contract.Invariant(consCache != null);
            Contract.Invariant(methodSubroutine != null); // F: because of many precondition suggestions, made it an object invariant
        }


        #region Private

        private readonly Subroutine methodSubroutine;
        private readonly object methodCache;

        private CFGBlock entry { get { return methodSubroutine.Entry; } }
        private CFGBlock normalExit { get { return methodSubroutine.Exit; } }
        private CFGBlock exceptionExit { get { return methodSubroutine.ExceptionExit; } }


        #endregion

        /// <summary>
        /// The control flow class provides an overlay of logical control flow over a syntactic
        /// code structure by making control flow explicit, including block entry/exit, and 
        /// finally block execution.
        /// </summary>
        /// <param name="start">Entry point for method</param>
        //^ [NotDelayed]
        internal ControlFlow(Subroutine subroutine, object methodCache)
        {
            Contract.Requires(subroutine != null); // F: As suggested by Clousot

            methodSubroutine = subroutine;
            consCache = contextCache.Cons;
            this.methodCache = methodCache;
        }


        #region ICFG<Code,Label,Handler,APC> Members

        public Method CFGMethod
        {
            get
            {
                IMethodInfo<Method> mi = methodSubroutine as IMethodInfo<Method>;
                if (mi != null) return mi.Method;
                throw new Exception("CFG has bad subroutine that is not a method");
            }
        }
        public APC Entry
        {
            get { return new APC(this.entry, 0, null); }
        }

        public APC EntryAfterRequires
        {
            get { return new APC(methodSubroutine.EntryAfterRequires, 0, null); }
        }

        public APC NormalExit
        {
            get { return new APC(this.normalExit, 0, null); }
        }

        public APC ExceptionExit
        {
            get { return new APC(methodSubroutine.ExceptionExit, 0, null); }
        }

        public APC Post(APC pc)
        {
            APC succ;
            if (this.HasSingleSuccessor(pc, out succ))
            {
                return succ;
            }
            return pc;
        }

        public bool HasSingleSuccessor(APC ppoint, out APC next)
        {
            return ppoint.Block.Subroutine.HasSingleSuccessor(ppoint, out next, consCache);
        }

        public bool HasSinglePredecessor(APC ppoint, out APC next, bool skipContracts)
        {
            return ppoint.Block.Subroutine.HasSinglePredecessor(ppoint, out next, consCache, skipContracts);
        }

        public APC PredecessorPCPriorToRequires(APC ppoint)
        {
            return ppoint.Block.Subroutine.PredecessorPCPriorToRequires(ppoint, consCache);
        }

        /// <summary>
        /// Returns all normal control flow successors
        /// </summary>
        public IEnumerable<APC>/*!*/ Successors(APC ppoint)
        {
            return ppoint.Block.Subroutine.Successors(ppoint, consCache);
        }


        public IEnumerable<APC> Predecessors(APC ppoint, bool skipContracts)
        {
            return ppoint.Block.Subroutine.Predecessors(ppoint, consCache, skipContracts);
        }

        public FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context)
        {
            return from.Subroutine.GetOrdinaryEdgeSubroutines(from, to, context);
        }


        /// <summary>
        /// Used to hash cons subroutine context edge lists
        /// </summary>
        private readonly SubroutineContextCache contextCache = new SubroutineContextCache();
        private readonly DConsCache consCache;

        private class SubroutineContextCache
        {
            private DoubleTable<SubroutineEdge, SubroutineContext, SubroutineContext> subroutineContextHashMap = new DoubleTable<STuple<CFGBlock, CFGBlock, string>, FList<STuple<CFGBlock, CFGBlock, string>>, FList<STuple<CFGBlock, CFGBlock, string>>>();
            private Dictionary<SubroutineEdge, SubroutineContext> subroutineContextHashMapSingleton = new Dictionary<STuple<CFGBlock, CFGBlock, string>, FList<STuple<CFGBlock, CFGBlock, string>>>();

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(subroutineContextHashMap != null);
                Contract.Invariant(subroutineContextHashMapSingleton != null);
            }

            public SubroutineContext Cons(SubroutineEdge edge, SubroutineContext tail)
            {
                SubroutineContext/*?*/ result;
                if (tail != null)
                {
                    Debug.Assert(tail.Head.One.Subroutine != edge.One.Subroutine);
                    if (!subroutineContextHashMap.TryGetValue(edge, tail, out result))
                    {
                        result = SubroutineContext.Cons(edge, tail);
                        subroutineContextHashMap.Add(edge, tail, result);
                    }
                }
                else
                {
                    if (!subroutineContextHashMapSingleton.TryGetValue(edge, out result))
                    {
                        result = SubroutineContext.Cons(edge, null);
                        subroutineContextHashMapSingleton[edge] = result;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Returns a set of program points that are possible execution continuations if 
        /// an exception is raised at the given ppoint.
        ///
        /// The supplied predicate allows the client to determine which handlers are possible
        /// candidates. The predicate is called from closest enclosing to outermost.
        /// </summary>
        /// <param name="data">Arbitrary client data passed to the handler predicate</param>
        /// <returns>A set of continuation program points that represent how execution continues to a chosen handler.
        /// The continuation includes Finally and Fault executions that intervene between the given ppoint and a
        /// chosen handler.
        /// </returns>
        public IEnumerable<APC> ExceptionHandlers<Type2, Data>(APC ppoint, Data data, IHandlerFilter<Type2, Data> handlerPredicate)
        {
            // This is a cross-subroutine operation. We need to ask for the handlers guarding the current block,
            // followed by the handlers guarding the source of each edge on the subroutine stack.

            // Ask the innermost subroutine for its handler continuations
            bool escapesFromSubroutine = false;
            foreach (CFGBlock handlerBlock in ppoint.Block.Subroutine.ExceptionHandlers(ppoint.Block, null, data, handlerPredicate))
            {
                if (handlerBlock != ppoint.Block.Subroutine.ExceptionExit)
                {
                    yield return ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint, handlerBlock, consCache);
                }
                else
                {
                    escapesFromSubroutine = true;
                }
            }

            if (!escapesFromSubroutine)
            {
                yield break; // no more outer handlers needed
            }


            // Now find handlers in surrounding subroutines, from inner to outer.
            //
            SubroutineContext outerContext = ppoint.SubroutineContext;
            FList<Pair<string, CFGBlock>> innerAbortingContext = null;
            APC innermostAbortingAPC = ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint.WithoutContext, ppoint.Block.Subroutine.ExceptionExit, consCache);
            SubroutineContext innerMostReversedContext = SubroutineContext.Reverse(innermostAbortingAPC.SubroutineContext);
            Subroutine innerSubroutine = ppoint.Block.Subroutine;
            while (outerContext != null)
            {
                escapesFromSubroutine = false;

                CFGBlock from = outerContext.Head.One;
                foreach (CFGBlock handlerBlock in from.Subroutine.ExceptionHandlers(from, innerSubroutine, data, handlerPredicate))
                {
                    if (handlerBlock == from.Subroutine.ExceptionExit)
                    {
                        escapesFromSubroutine = true;
                    }
                    else
                    {
                        yield return this.ComputeExceptionTarget(innermostAbortingAPC.Block, innermostAbortingAPC.Index, innerMostReversedContext,
                                                  innerAbortingContext,
                                                  consCache(new SubroutineEdge(from, handlerBlock, "exception"), outerContext.Tail),
                                                  consCache);
                    }
                }
                if (!escapesFromSubroutine)
                {
                    yield break; // no more outer handlers needed
                }
                innerAbortingContext = FList<Pair<string, CFGBlock>>.Cons(new Pair<string, CFGBlock>(outerContext.Head.Three, from), innerAbortingContext);
                outerContext = outerContext.Tail;
            }
            // if we get here, we are throwing out of the outermost context
            yield return this.ComputeExceptionTarget(innermostAbortingAPC.Block, innermostAbortingAPC.Index, innerMostReversedContext,
                                      innerAbortingContext,
                                      null,
                                      consCache);
        }

        /// <summary>
        /// Computes new APC corresponding to a concatenation of exectuion from the current abortPoint (and its subroutine execution, provided in reversed from as innerMostReversedContext)
        /// with the innerAbortingContext and the outerAbortingContext.
        /// The innerAbortingContext is a set of blocks from outer to inner surrounding the abort point.
        /// The resulting APC starts with the abort point and the innermost context (in proper order), followed by (x,exit_x) edges for all points in the innerAbortingContext (from inner to outer)
        /// This entire context is pre-pended to the outer subroutine context.
        /// </summary>
        private APC ComputeExceptionTarget(CFGBlock abortPoint, int abortIndex,
                                           SubroutineContext innerMostReversedContext,
                                           FList<Pair<string, CFGBlock>> innerAbortingContext,
                                           SubroutineContext outerSubroutineContext,
                                           DConsCache consCache)
        {
            while (innerAbortingContext != null)
            {
                outerSubroutineContext = this.consCache(new SubroutineEdge(innerAbortingContext.Head.Two, innerAbortingContext.Head.Two.Subroutine.ExceptionExit, innerAbortingContext.Head.One), outerSubroutineContext);
                innerAbortingContext = innerAbortingContext.Tail;
            }
            while (innerMostReversedContext != null)
            {
                outerSubroutineContext = this.consCache(innerMostReversedContext.Head, outerSubroutineContext);
                innerMostReversedContext = innerMostReversedContext.Tail;
            }
            return new APC(abortPoint, abortIndex, outerSubroutineContext);
        }



        public void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context)
        {
            var printed = new Set<Pair<Subroutine, SubroutineContext>>();
            methodSubroutine.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
        }

        private string FormatSourceContext(string doc, int line, int col)
        {
            return String.Format("{0}({1},{2})", doc, line, col);
        }


        public Subroutine Subroutine { get { return methodSubroutine; } }

        public bool IsJoinPoint(CFGBlock block)
        {
            Contract.Requires(block != null);// F: Added as of Clousot suggestion

            return block.Subroutine.IsJoinPoint(block);
        }

        public bool IsJoinPoint(APC ppoint)
        {
            if (ppoint.Index != 0) return false;
            return IsJoinPoint(ppoint.Block);
        }

        public bool IsSplitPoint(CFGBlock block)
        {
            Contract.Requires(block != null);// F: Added as of Clousot suggestion
            return block.Subroutine.IsSplitPoint(block);
        }

        public bool IsSplitPoint(APC ppoint)
        {
            if (ppoint.Index != ppoint.Block.Count) return false;

            return IsSplitPoint(ppoint.Block);
        }

        public bool IsForwardBackEdgeTarget(APC ppoint)
        {
            CFGBlock block = ppoint.Block;
            if (ppoint.Index != 0) return false; // not at beginning.
            return ppoint.Block.Subroutine.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge;
        }

        public bool IsForwardBackEdgeTarget(CFGBlock block)
        {
            Contract.Requires(block != null); // F: Added as of Clousot suggestion
            return block.Subroutine.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge;
        }

        public bool IsBackwardBackEdgeTarget(APC ppoint)
        {
            CFGBlock block = ppoint.Block;
            if (ppoint.Index != block.Count) return false; // not at end.
            return ppoint.Block.Subroutine.EdgeInfo.DepthFirstInfo(block).SourceOfBackEdge;
        }

        public bool IsForwardBackEdge(APC from, APC to)
        {
            if (to.Index != 0) return false;
            return IsForwardBackEdgeHelper(from, to);
        }

        public bool IsBackwardBackEdge(APC from, APC to)
        {
            if (from.Index != 0) return false;
            return IsBackwardBackEdgeHelper(from, to);
        }

        private bool IsForwardBackEdgeHelper(APC from, APC to)
        {
            Contract.Assume(to.Block != null, "assuming the object invariant");
            Contract.Assume(from.Block != null, "assuming the object invariant");
            if (to.Block.Subroutine.EdgeInfo.IsBackEdge(from.Block, Unit.Value, to.Block)) return true;

            // in case of an end finally we consult the original edge
            // If this is an endfinally, there must be at least one label
            if (from.SubroutineContext != null && from.SubroutineContext.Tail == to.SubroutineContext)
            {
                // it was a pop
                SubroutineEdge edge = from.SubroutineContext.Head;
                return edge.Two.Subroutine.EdgeInfo.IsBackEdge(edge.One, Unit.Value, edge.Two);
            }
            return false;
        }

        private bool IsBackwardBackEdgeHelper(APC from, APC to)
        {
            // use symmetry
            Contract.Assume(from.Block != null); // F: made an assumption

            if (from.Block.Subroutine.EdgeInfo.IsBackEdge(to.Block, Unit.Value, from.Block)) return true;

            // in case of an end finally we consult the original edge
            // If this is an endfinally, there must be at least one label
            if (to.SubroutineContext != null && to.SubroutineContext.Tail == from.SubroutineContext)
            {
                // it was a pop
                SubroutineEdge reversedEdge = to.SubroutineContext.Head;
                // ask reversed question
                return reversedEdge.One.Subroutine.EdgeInfo.IsBackEdge(reversedEdge.Two, Unit.Value, reversedEdge.One);
            }
            return false;
        }


        public bool IsBlockStart(APC ppoint)
        {
            return (ppoint.Index == 0);
        }

        public bool IsBlockEnd(APC ppoint)
        {
            return (ppoint.Index == ppoint.Block.Count);
        }

        #endregion


        #region IGraph of APCs (forward only)

        public IGraph<APC, Unit> AsForwardGraph(bool includeExceptionEdges)
        {
            var forwardGraphCache = new GraphWrapper<APC, Unit>(
                      new APC[0], // we don't provide the set of all apcs
                      (pc) => SuccessorEdges(pc, includeExceptionEdges));
            return forwardGraphCache;
        }

        private IEnumerable<Pair<Unit, APC>> SuccessorEdges(APC pc, bool includeExceptionEdges)
        {
            var normalized = pc.LastInBlock();
            foreach (APC succ in this.Successors(normalized))
            {
                yield return new Pair<Unit, APC>(Unit.Value, succ);
            }
            if (!includeExceptionEdges) yield break;

            foreach (APC succ in this.ExceptionHandlers<Type, int>(pc, 0, null))
            {
                yield return new Pair<Unit, APC>(Unit.Value, succ);
            }
        }


        public IGraph<APC, Unit> AsBackwardGraph(bool includeExceptionEdges, bool skipContracts)
        {
            if (includeExceptionEdges) throw new NotSupportedException();

            var backwardGraph = new GraphWrapper<APC, Unit>(
                new APC[0], // we don't provide the set of all apcs
                (pc) => PredecessorEdges(pc, includeExceptionEdges, skipContracts));

            return backwardGraph;
        }

        private IEnumerable<Pair<Unit, APC>> PredecessorEdges(APC pc, bool includeExceptionEdges, bool skipContracts)
        {
            var normalized = pc.FirstInBlock();
            foreach (APC pred in this.Predecessors(normalized, skipContracts))
            {
                yield return new Pair<Unit, APC>(Unit.Value, pred.FirstInBlock());
            }

            // TOOD: support backwards exception edges.
        }

        #endregion


        #region ICFG<Label,Handler,APC> Members

        public IDecodeMSIL<APC, Local, Parameter, Method2, Field, Type2, Unit, Unit, IMethodContext<Field, Method2>, Unit>
          GetDecoder<Local, Parameter, Method2, Field, Property, Event, Type2, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method2, Field, Property, Event, Type2, Attribute, Assembly> mdDecoder)
        {
            var mcache = methodCache as MethodCache<Local, Parameter, Type2, Method2, Field, Property, Event, Attribute, Assembly>;
            return new APCDecoder<Local, Parameter, Method2, Field, Property, Event, Type2, Attribute, Assembly>(
                       (ControlFlow<Method2, Type2>)(object)this,
                       mdDecoder,
                       mcache);
        }
        #endregion

        #region ICFG<Type,APC> Members


        public string ToString(APC pc)
        {
            return pc.ToString();
        }

        public IEnumerable<CFGBlock> LoopHeads
        {
            get
            {
                foreach (var sub in this.Subroutine.SubroutineTree())
                {
                    foreach (var block in sub.Blocks)
                    {
                        if (this.IsForwardBackEdgeTarget(block)) yield return block;
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// We know that Source and Dest are Unit
    /// </summary>
    internal class APCDecoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> :
      IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, UnitSource, UnitDest, IMethodContext<Field, Method>, Unit>,
      IMethodContext<Field, Method>, IMethodContextData<Field, Method>
    {
        private ControlFlow<Method, Type> parent;
        private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
        private MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache;

        public APCDecoder(
          ControlFlow<Method, Type> parent,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache
        )
        {
            this.parent = parent;
            this.mdDecoder = mdDecoder;
            this.methodCache = methodCache;
        }


        #region IMSILDecoder<APC,Local,Parameter,Method,Field,Type,Source,Dest> Members

        /// <summary>
        /// * Removes unconditional branches and branch true, branch false.
        /// * Turns binary conditional branches into their condition evaluation
        /// * Computes proper polarity of assume/assert based on tag and context
        /// * Turns Object.ReferenceEquals into binary Ceq
        /// </summary>
        private struct RemoveBranchDelegator<Data, Result, Visitor> :
          IVisitMSIL<APC, Local, Parameter, Method, Field, Type, UnitSource, UnitDest, Data, Result>
          where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, UnitSource, UnitDest, Data, Result>
        {
            private Visitor visitor;
            private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(mdDecoder != null);
            }

            public RemoveBranchDelegator(
              Visitor visitor,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
            )
            {
                Contract.Requires(mdDecoder != null);

                this.visitor = visitor;
                this.mdDecoder = mdDecoder;
            }

            /// <summary>
            /// If original block is a method contract block, then invert assume/assert
            /// </summary>
            public Result Assume(APC pc, string tag, UnitSource source, object provenance, Data data)
            {
                if ((tag == "requires" && pc.InsideRequiresAtCall) || (tag == "invariant" && pc.InsideInvariantOnExit))
                {
                    return visitor.Assert(pc, tag, source, provenance, data);
                }
                if (tag == "assume" && pc.InsideRequiresAtCall)
                {
                    // extra clousot extractor assume false in legacy requires. At call-site, need to null it out.
                    return visitor.Pop(pc, source, data);
                }
                return visitor.Assume(pc, tag, source, provenance, data);
            }


            /// <summary>
            /// If original block is a method contract block, then invert assume/assert
            /// </summary>
            public Result Assert(APC pc, string tag, UnitSource source, object provenance, Data data)
            {
                if (pc.InsideEnsuresAtCall)
                {
                    return visitor.Assume(pc, tag, source, provenance, data);
                }
                return visitor.Assert(pc, tag, source, provenance, data);
            }

            public Result Arglist(APC pc, UnitDest dest, Data data)
            {
                return visitor.Arglist(pc, dest, data);
            }

            public Result Binary(APC pc, BinaryOperator op, UnitDest dest, UnitSource s1, UnitSource s2, Data data)
            {
                return visitor.Binary(pc, op, dest, s1, s2, data);
            }

            public Result BranchCond(APC pc, APC target, BranchOperator bop, UnitSource value1, UnitSource value2, Data data)
            {
                UnitDest dest = UnitDest.Value;
                switch (bop)
                {
                    case BranchOperator.Beq:
                        // equal to Ceq followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Ceq, dest, value1, value2, data);

                    case BranchOperator.Bge:
                        // equal to Cge followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cge, dest, value1, value2, data);

                    case BranchOperator.Bge_un:
                        // equal to Cge_un followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cge_Un, dest, value1, value2, data);

                    case BranchOperator.Bgt:
                        // equal to Cgt followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cgt, dest, value1, value2, data);

                    case BranchOperator.Bgt_un:
                        // equal to Cgt_un followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cgt_Un, dest, value1, value2, data);

                    case BranchOperator.Ble:
                        // equal to Cle followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cle, dest, value1, value2, data);

                    case BranchOperator.Ble_un:
                        // equal to Cle_un followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cle_Un, dest, value1, value2, data);

                    case BranchOperator.Blt:
                        // equal to Clt followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Clt, dest, value1, value2, data);

                    case BranchOperator.Blt_un:
                        // equal to Clt_un followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Clt_Un, dest, value1, value2, data);

                    case BranchOperator.Bne_un:
                        // equal to Cne_un followed by br.true
                        return visitor.Binary(pc, BinaryOperator.Cne_Un, dest, value1, value2, data);
                }
                return visitor.Nop(pc, data);
            }

            public Result BranchTrue(APC pc, APC target, UnitSource cond, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public Result BranchFalse(APC pc, APC target, UnitSource cond, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public Result Branch(APC pc, APC target, bool leave, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public Result Break(APC pc, Data data)
            {
                return visitor.Break(pc, data);
            }

            public Result Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<UnitSource>
            {
                Type declaringType = mdDecoder.DeclaringType(method);
                if (
                    mdDecoder.IsStatic(method))
                {
                    if (args.Count == 2 && mdDecoder.Equal(declaringType, mdDecoder.System_Object) &&
                        mdDecoder.Name(method) == "ReferenceEquals"
                        )
                    {
                        // special case equal here
                        return visitor.Binary(pc, BinaryOperator.Ceq, dest, args[0], args[1], data);
                    }
                    // special case Decimal methods to operators
                    if (mdDecoder.Equal(declaringType, mdDecoder.System_Decimal))
                    {
                        switch (mdDecoder.Name(method))
                        {
                            case "op_Addition":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Add, dest, args[0], args[1], data);

                            case "op_Division":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Div, dest, args[0], args[1], data);

                            case "op_Equality":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Ceq, dest, args[0], args[1], data);

                            case "op_GreaterThan":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Cgt, dest, args[0], args[1], data);

                            case "op_GreaterThanOrEqual":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Cge, dest, args[0], args[1], data);

                            case "op_Inequality":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Cne_Un, dest, args[0], args[1], data);

                            case "op_LessThan":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Clt, dest, args[0], args[1], data);

                            case "op_LessThanOrEqual":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Cle, dest, args[0], args[1], data);

                            case "op_Modulus":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Rem, dest, args[0], args[1], data);

                            case "op_Multiply":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Mul, dest, args[0], args[1], data);

                            case "op_Subtraction":
                                Contract.Assume(args.Count >= 2);
                                return visitor.Binary(pc, BinaryOperator.Sub, dest, args[0], args[1], data);

                            case "op_UnaryNegation":
                                Contract.Assume(args.Count >= 1);
                                return visitor.Unary(pc, UnaryOperator.Neg, false, false, dest, args[0], data);

                            default:
                                break;
                        }
                    }
                }
                return visitor.Call(pc, method, tail, virt, extraVarargs, dest, args, data);
            }

            public Result Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, UnitDest dest, UnitSource fp, ArgList args, Data data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<UnitSource>
            {
                return visitor.Calli(pc, returnType, argTypes, tail, isInstance, dest, fp, args, data);
            }

            public Result Ckfinite(APC pc, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Ckfinite(pc, dest, source, data);
            }

            public Result Cpblk(APC pc, bool @volatile, UnitSource destaddr, UnitSource srcaddr, UnitSource len, Data data)
            {
                return visitor.Cpblk(pc, @volatile, destaddr, srcaddr, len, data);
            }

            public Result Endfilter(APC pc, UnitSource decision, Data data)
            {
                return visitor.Endfilter(pc, decision, data);
            }

            public Result Endfinally(APC pc, Data data)
            {
                return visitor.Endfinally(pc, data);
            }

            public Result Entry(APC pc, Method method, Data data)
            {
                return visitor.Entry(pc, method, data);
            }

            public Result Initblk(APC pc, bool @volatile, UnitSource destaddr, UnitSource value, UnitSource len, Data data)
            {
                return visitor.Initblk(pc, @volatile, destaddr, value, len, data);
            }

            public Result Jmp(APC pc, Method method, Data data)
            {
                return visitor.Jmp(pc, method, data);
            }

            public Result Ldarg(APC pc, Parameter argument, bool isOld, UnitDest dest, Data data)
            {
                return visitor.Ldarg(pc, argument, isOld, dest, data);
            }

            public Result Ldarga(APC pc, Parameter argument, bool isOld, UnitDest dest, Data data)
            {
                return visitor.Ldarga(pc, argument, isOld, dest, data);
            }

            public Result Ldconst(APC pc, object constant, Type type, UnitDest dest, Data data)
            {
                return visitor.Ldconst(pc, constant, type, dest, data);
            }

            public Result Ldnull(APC pc, UnitDest dest, Data data)
            {
                return visitor.Ldnull(pc, dest, data);
            }

            public Result Ldftn(APC pc, Method method, UnitDest dest, Data data)
            {
                return visitor.Ldftn(pc, method, dest, data);
            }

            public Result Ldind(APC pc, Type type, bool @volatile, UnitDest dest, UnitSource ptr, Data data)
            {
                return visitor.Ldind(pc, type, @volatile, dest, ptr, data);
            }

            public Result Ldloc(APC pc, Local local, UnitDest dest, Data data)
            {
                return visitor.Ldloc(pc, local, dest, data);
            }

            public Result Ldloca(APC pc, Local local, UnitDest dest, Data data)
            {
                return visitor.Ldloca(pc, local, dest, data);
            }

            public Result Ldstack(APC pc, int offset, UnitDest dest, UnitSource source, bool isOld, Data data)
            {
                return visitor.Ldstack(pc, offset, dest, source, isOld, data);
            }

            public Result Ldstacka(APC pc, int offset, UnitDest dest, UnitSource source, Type type, bool isOld, Data data)
            {
                return visitor.Ldstacka(pc, offset, dest, source, type, isOld, data);
            }

            public Result Localloc(APC pc, UnitDest dest, UnitSource size, Data data)
            {
                return visitor.Localloc(pc, dest, size, data);
            }

            public Result Nop(APC pc, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public Result Pop(APC pc, UnitSource source, Data data)
            {
                return visitor.Pop(pc, source, data);
            }

            public Result Return(APC pc, UnitSource source, Data data)
            {
                return visitor.Return(pc, source, data);
            }

            public Result Starg(APC pc, Parameter argument, UnitSource source, Data data)
            {
                return visitor.Starg(pc, argument, source, data);
            }

            public Result Stind(APC pc, Type type, bool @volatile, UnitSource ptr, UnitSource value, Data data)
            {
                return visitor.Stind(pc, type, @volatile, ptr, value, data);
            }

            public Result Stloc(APC pc, Local local, UnitSource source, Data data)
            {
                return visitor.Stloc(pc, local, source, data);
            }

            public Result Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, UnitSource value, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public Result Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Unary(pc, op, overflow, unsigned, dest, source, data);
            }

            public Result Box(APC pc, Type type, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Box(pc, type, dest, source, data);
            }

            public Result ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<UnitSource>
            {
                return visitor.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, data);
            }

            public Result Castclass(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Castclass(pc, type, dest, obj, data);
            }

            public Result Cpobj(APC pc, Type type, UnitSource destptr, UnitSource srcptr, Data data)
            {
                return visitor.Cpobj(pc, type, destptr, srcptr, data);
            }

            public Result Initobj(APC pc, Type type, UnitSource ptr, Data data)
            {
                return visitor.Initobj(pc, type, ptr, data);
            }

            public Result Isinst(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Isinst(pc, type, dest, obj, data);
            }

            public Result Ldelem(APC pc, Type type, UnitDest dest, UnitSource array, UnitSource index, Data data)
            {
                return visitor.Ldelem(pc, type, dest, array, index, data);
            }

            public Result Ldelema(APC pc, Type type, bool @readonly, UnitDest dest, UnitSource array, UnitSource index, Data data)
            {
                return visitor.Ldelema(pc, type, @readonly, dest, array, index, data);
            }

            public Result Ldfld(APC pc, Field field, bool @volatile, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Ldfld(pc, field, @volatile, dest, obj, data);
            }

            public Result Ldflda(APC pc, Field field, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Ldflda(pc, field, dest, obj, data);
            }

            public Result Ldlen(APC pc, UnitDest dest, UnitSource array, Data data)
            {
                return visitor.Ldlen(pc, dest, array, data);
            }

            public Result Ldsfld(APC pc, Field field, bool @volatile, UnitDest dest, Data data)
            {
                return visitor.Ldsfld(pc, field, @volatile, dest, data);
            }

            public Result Ldsflda(APC pc, Field field, UnitDest dest, Data data)
            {
                return visitor.Ldsflda(pc, field, dest, data);
            }

            public Result Ldtypetoken(APC pc, Type type, UnitDest dest, Data data)
            {
                return visitor.Ldtypetoken(pc, type, dest, data);
            }

            public Result Ldfieldtoken(APC pc, Field field, UnitDest dest, Data data)
            {
                return visitor.Ldfieldtoken(pc, field, dest, data);
            }

            public Result Ldmethodtoken(APC pc, Method method, UnitDest dest, Data data)
            {
                return visitor.Ldmethodtoken(pc, method, dest, data);
            }

            public Result Ldvirtftn(APC pc, Method method, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Ldvirtftn(pc, method, dest, obj, data);
            }

            public Result Mkrefany(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Mkrefany(pc, type, dest, obj, data);
            }

            public Result Newarray<ArgList>(APC pc, Type type, UnitDest dest, ArgList lengths, Data data)
              where ArgList : IIndexable<UnitSource>
            {
                return visitor.Newarray(pc, type, dest, lengths, data);
            }

            public Result Newobj<ArgList>(APC pc, Method ctor, UnitDest dest, ArgList args, Data data)
              where ArgList : IIndexable<UnitSource>
            {
                Type declaringType = mdDecoder.DeclaringType(ctor);
                if (args.Count == 2 && mdDecoder.Equal(declaringType, mdDecoder.System_Decimal))
                {
                    // note: ctor first arg is object, but not on stack during newobj.
                    // special case equal here
                    return visitor.Unary(pc, UnaryOperator.Conv_dec, false, false, dest, args[1], data);
                }

                return visitor.Newobj(pc, ctor, dest, args, data);
            }

            public Result Refanytype(APC pc, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Refanytype(pc, dest, source, data);
            }

            public Result Refanyval(APC pc, Type type, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Refanyval(pc, type, dest, source, data);
            }

            public Result Rethrow(APC pc, Data data)
            {
                return visitor.Rethrow(pc, data);
            }

            public Result Sizeof(APC pc, Type type, UnitDest dest, Data data)
            {
                return visitor.Sizeof(pc, type, dest, data);
            }

            public Result Stelem(APC pc, Type type, UnitSource array, UnitSource index, UnitSource value, Data data)
            {
                return visitor.Stelem(pc, type, array, index, value, data);
            }

            public Result Stfld(APC pc, Field field, bool @volatile, UnitSource obj, UnitSource value, Data data)
            {
                return visitor.Stfld(pc, field, @volatile, obj, value, data);
            }

            public Result Stsfld(APC pc, Field field, bool @volatile, UnitSource value, Data data)
            {
                return visitor.Stsfld(pc, field, @volatile, value, data);
            }

            public Result Throw(APC pc, UnitSource exn, Data data)
            {
                return visitor.Throw(pc, exn, data);
            }

            public Result Unbox(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Unbox(pc, type, dest, obj, data);
            }

            public Result Unboxany(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
            {
                return visitor.Unboxany(pc, type, dest, obj, data);
            }


            #region IVisitSynthIL<Label,Method2,Source,Dest,Data,Result> Members


            public Result BeginOld(APC pc, APC matchingEnd, Data data)
            {
                return visitor.BeginOld(pc, matchingEnd, data);
            }

            public Result EndOld(APC pc, APC matchingBegin, Type type, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.EndOld(pc, matchingBegin, type, dest, source, data);
            }

            public Result Ldresult(APC pc, Type type, UnitDest dest, UnitSource source, Data data)
            {
                return visitor.Ldresult(pc, type, dest, source, data);
            }

            #endregion
        }

        public Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
          where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, UnitSource, UnitDest, Data, Result>
        {
            return methodCache.ForwardDecode<Data, Result, RemoveBranchDelegator<Data, Result, Visitor>>(pc, new RemoveBranchDelegator<Data, Result, Visitor>(visitor, mdDecoder), data);
        }

        public IMethodContext<Field, Method> Context
        {
            get { return this; }
        }

        public bool IsUnreachable(APC pc) { return false; }

        #endregion

        #region IMethodContext<Method> Members
        public IMethodContextData<Field, Method> MethodContext { get { return this; } }
        #endregion

        #region IMethodContextData<Method> Members

        public Method CurrentMethod
        {
            get { return parent.CFGMethod; }
        }

        public ICFG CFG { get { return parent; } }

        public string SourceContext(APC pc)
        {
            return pc.PrimarySourceContext();
        }

        public string SourceDocument(APC pc)
        {
            return pc.Block.SourceDocument(pc);
        }

        public int SourceStartLine(APC pc)
        {
            return pc.Block.SourceStartLine(pc);
        }

        public int SourceEndLine(APC pc)
        {
            return pc.Block.SourceEndLine(pc);
        }

        public int SourceStartColumn(APC pc)
        {
            return pc.Block.SourceStartColumn(pc);
        }

        public int SourceEndColumn(APC pc)
        {
            return pc.Block.SourceEndColumn(pc);
        }

        public IEnumerable<Field> Modifies(Method method)
        {
            method = mdDecoder.Unspecialized(method);
            return methodCache.GetModifies(method);
        }

        public IEnumerable<Method> AffectedGetters(Field field)
        {
            field = mdDecoder.Unspecialized(field);
            return methodCache.GetAffectedGetters(field);
        }

        #endregion

        #region IDecodeMSIL<APC,Local,Parameter,Method,Field,Type,UnitDest,UnitDest,IMethodContext<Field,Method>,Unit> Members


        public UnitDest EdgeData(APC from, APC to)
        {
            return Unit.Value;
        }

        public void Display(TextWriter tw, string prefix, Unit data) { }

        #endregion
    }


    /// <summary>
    /// Represents edges as just an array ordered by the index of the cfg block where the edge starts.
    /// </summary>
    internal class EdgeMap<Tag> : IEnumerable<Pair<CFGBlock, Pair<Tag, CFGBlock>>>, IGraph<CFGBlock, Tag>
    {
        private List<Pair<CFGBlock, Pair<Tag, CFGBlock>>> edges;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(edges != null);
        }

        public EdgeMap(List<Pair<CFGBlock, Pair<Tag, CFGBlock>>> edges)
        {
            Contract.Requires(edges != null);

            this.edges = edges;
            edges.Sort(CompareFirstBlockIndex);
            CheckSort();
        }
        private void CheckSort()
        {
            int lastSeen = 0;
            for (int i = 0; i < edges.Count; i++)
            {
                Contract.Assume(edges[i].One != null);
                int currentIndex = edges[i].One.Index;
                Contract.Assume(currentIndex >= lastSeen);
                lastSeen = currentIndex;
            }
        }
        private static int CompareFirstBlockIndex(Pair<CFGBlock, Pair<Tag, CFGBlock>> e1, Pair<CFGBlock, Pair<Tag, CFGBlock>> e2)
        {
            if (e1.One.Index == e2.One.Index)
            {
                return e1.Two.Two.Index - e2.Two.Two.Index;
            }
            return e1.One.Index - e2.One.Index;
        }

        private struct Successors : ICollection<Pair<Tag, CFGBlock>>
        {
            private EdgeMap<Tag> underlying;
            private int startIndex;

            public Successors(EdgeMap<Tag> underlying, int startIndex)
            {
                Contract.Requires(startIndex >= 0);
                Contract.Requires(underlying != null);// F: As of Clousot suggestion
                Contract.Assume(underlying.edges != null, "Assuming the object invariant for underlying");

                this.underlying = underlying;
                this.startIndex = startIndex;
            }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(startIndex >= 0);
                Contract.Invariant(underlying.edges != null);
            }

            #region ICollection<Pair<Tag,CFGBlock>> Members

            public void Add(Pair<Tag, CFGBlock> item)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public void Clear()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            //^ [Confined]
            public bool Contains(Pair<Tag, CFGBlock> item)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public void CopyTo(Pair<Tag, CFGBlock>[] array, int arrayIndex)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int Count
            {
                [ContractVerification(false)]
                get
                {
                    int edgeIndex = startIndex;
                    if (edgeIndex < underlying.edges.Count)
                    {
                        // count equal first
                        int blockIndex = underlying.edges[edgeIndex].One.Index;
                        int count = 0;
                        do
                        {
                            count++;
                            edgeIndex++;
                        } while (edgeIndex < underlying.edges.Count && underlying.edges[edgeIndex].One.Index == blockIndex);
                        return count;
                    }
                    return 0;
                }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(Pair<Tag, CFGBlock> item)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion

            #region IEnumerable<Pair<Tag,CFGBlock>> Members

            //^ [Pure]
            public IEnumerator<Pair<Tag, CFGBlock>> GetEnumerator()
            {
                if (startIndex >= underlying.edges.Count) { yield break; }
                int edgeIndex = startIndex;
                int blockIndex = underlying.edges[edgeIndex].One.Index;
                do
                {
                    yield return underlying.edges[edgeIndex].Two;
                    edgeIndex++;
                }
                while (edgeIndex < underlying.edges.Count && underlying.edges[edgeIndex].One.Index == blockIndex);
            }

            #endregion

            #region IEnumerable Members

            //^ [Pure]
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion
        }

        public ICollection<Pair<Tag, CFGBlock>> this[CFGBlock from]
        {
            get
            {
                return new Successors(this, FindStartIndex(from));
            }
        }

        [ContractVerification(false)]
        private int FindStartIndex(CFGBlock from)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(Contract.Result<int>() <= edges.Count);

            // binary search
            int lo = 0;
            int hi = edges.Count;
            while (lo < hi)
            {
                Contract.Assert(lo >= 0);
                Contract.Assert(hi <= edges.Count);
                int mid = lo + (hi - lo) / 2;
                Contract.Assume(mid < edges.Count, "why can't we prove this");

                Contract.Assume(edges[mid].One != null);
                int indexAtMid = edges[mid].One.Index;
                if (indexAtMid == from.Index)
                {
                    // now find beginning of sequence
                    while (mid > 0 && edges[mid - 1].One.Index == indexAtMid)
                    {
                        mid--;
                    }
                    return mid;
                }
                if (indexAtMid < from.Index)
                {
                    lo = mid + 1;
                    continue;
                }
                Debug.Assert(indexAtMid > from.Index);
                hi = mid;
            }
            // if we fall out, we didn't find any. Return index past last
            return edges.Count;
        }


        public EdgeMap<Tag> ReversedEdges()
        {
            List<Pair<CFGBlock, Pair<Tag, CFGBlock>>> reversed = new List<Pair<CFGBlock, Pair<Tag, CFGBlock>>>(edges.Count);

            foreach (Pair<CFGBlock, Pair<Tag, CFGBlock>> edge in edges)
            {
                reversed.Add(new Pair<CFGBlock, Pair<Tag, CFGBlock>>(edge.Two.Two, new Pair<Tag, CFGBlock>(edge.Two.One, edge.One)));
            }
            return new EdgeMap<Tag>(reversed);
        }

        public void ReSort()
        {
            edges.Sort(CompareFirstBlockIndex);
        }

        /// <summary>
        /// Removes the unwanted edges
        /// </summary>
        public void Filter(Predicate<Pair<CFGBlock, Pair<Tag, CFGBlock>>> keep)
        {
            Contract.Requires(keep != null);

            // must separately build a list of indices to remove, as we can't change the list while traversing it or we'll skip
            // elements
            FList<int> toRemove = null;
            for (int i = 0; i < edges.Count; i++)
            {
                if (!keep(edges[i]))
                {
                    toRemove = FList<int>.Cons(i, toRemove);
                }
            }
            toRemove.Apply(delegate (int i) { edges.RemoveAt(i); });
        }

        #region IEnumerable<Pair<CFGBlock,Pair<Tag>,CFGBlock>> Members

        //^ [Pure]
        public IEnumerator<Pair<CFGBlock, Pair<Tag, CFGBlock>>> GetEnumerator()
        {
            return edges.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        //^ [Pure]
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IGraph<CFGBlock,Tag> Members

        IEnumerable<CFGBlock> IGraph<CFGBlock, Tag>.Nodes
        {
            get { throw new NotImplementedException(); }
        }

        bool IGraph<CFGBlock, Tag>.Contains(CFGBlock node)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pair<Tag, CFGBlock>> IGraph<CFGBlock, Tag>.Successors(CFGBlock node)
        {
            return this[node];
        }

        #endregion
    }

    public interface IEdgeSubroutineAdaptor
    {
        FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutinesInternal(CFGBlock from, CFGBlock to, SubroutineContext context);
    }

    /// <summary>
    /// special visitor to identify the return value of a function
    /// </summary>
    public class ReturnValueFinderVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly, ContextData, EdgeData>
    : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData>
    {
        private Local retvalLocal; // the local var holding the return value (if any) of the function in the preceding block
        private Parameter retvalParam; // the parameter holding the return value (if any) of the function in the preceding block
        private Field retvalField; // the field holding the return value (if any) of the function in the preceding block
        private bool foundLocalRetval = false;
        private bool foundParamRetval = false;
        private bool foundFieldRetval = false;

        public bool GetLocalReturnValue(out Local returnValue)
        {
            returnValue = retvalLocal;
            return foundLocalRetval;
        }

        public bool GetParameterReturnValue(out Parameter returnValue)
        {
            returnValue = retvalParam;
            return foundParamRetval;
        }

        public bool GetFieldReturnValue(out Field returnValue)
        {
            returnValue = retvalField;
            return foundFieldRetval;
        }

        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> Visitor() { return this; }

        protected override EdgeData Default(APC pc, ContextData data)
        {
            return default(EdgeData);
        }

        // the block after a function call stores the return value of the function in the appropriate local variable. capture the identity of this local variable
        public override EdgeData Stloc(APC pc, Local local, Expression source, ContextData data)
        {
            //Console.WriteLine("found stloc " + local);
            retvalLocal = local;
            foundLocalRetval = true;
            return default(EdgeData);
        }

        // the block after a function call stores the return value of the function in the appropriate local variable, which may be a parameter to the caller. captrue the identity of this local variable
        public override EdgeData Starg(APC pc, Parameter argument, Expression source, ContextData data)
        {
            retvalParam = argument;
            foundParamRetval = true;
            return default(EdgeData);
        }

        public override EdgeData Stfld(APC pc, Field field, bool @volatile, Expression obj, Expression value, ContextData data)
        {
            retvalField = field;
            foundFieldRetval = true;
            return default(EdgeData);
        }
    }
}
