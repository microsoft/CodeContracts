// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    using SubroutineContext = FList<STuple<CFGBlock, CFGBlock, string>>;
    using SubroutineEdge = STuple<CFGBlock, CFGBlock, string>;

    public static class Quantifiers
    {
        internal enum Quantifier { ForAll = 0, Exists = 1 }

        public static ForAllIndexedExpression AsForAllIndexed<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions>(
          this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver,
          APC pc,
          SymbolicValue value)
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
          where Expression : IEquatable<Expression>
        {
            var qd = new QuantifierDecompiler<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions>(driver);
            return qd.AsQuantifiedIndexed(Quantifier.ForAll, pc, value) as ForAllIndexedExpression;
        }

        public static ExistsIndexedExpression AsExistsIndexed<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions>(
          this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver,
          APC pc,
          SymbolicValue value)
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
          where Expression : IEquatable<Expression>
        {
            var qd = new QuantifierDecompiler<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions>(driver);
            return qd.AsQuantifiedIndexed(Quantifier.Exists, pc, value) as ExistsIndexedExpression;
        }

        internal class QuantifierDecompiler<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions>
          : IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, BoxedExpression>
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
          where Expression : IEquatable<Expression>
        {
            private enum TypeName { Contract = 0, Enumerable = 1, Array = 2 }

            // T
            private static readonly string[,] NameTable = { 
                                     // Contract  Enumerable     Array
                     /* ForAll */      {"ForAll",   "All",     "TrueForAll"},
                     /* Exist */       {"Exists",   "Any",     "Exists"}
                                    };

            private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver;
            private SymbolicValue targetObject;
            private APC contractPC;
            private BoxedExpression boundVariable;

            public QuantifierDecompiler(
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver
              )
            {
                this.driver = driver;
            }

            [ContractVerification(true)]
            internal QuantifiedIndexedExpression AsQuantifiedIndexed(Quantifier quantifier, APC pc, SymbolicValue value)
            {
                var MetaDataDecoder = driver.RawLayer.MetaDataDecoder;
                Method method;
                SymbolicValue[] args;
                contractPC = pc;
                if (!driver.Context.ValueContext.IsPureFunctionCall(pc, value, out method, out args)) return null;
                var methodName = MetaDataDecoder.Name(method);
                var typeName = MetaDataDecoder.Name(MetaDataDecoder.DeclaringType(method));
                if (args.Length == 2)
                {
                    if (methodName == NameTable[(int)quantifier, 0] && typeName == "Contract"
                      || methodName == NameTable[(int)quantifier, 1] && typeName == "Enumerable")
                    {
                        Method forallBody;
                        if (!driver.Context.ValueContext.IsDelegateValue(pc, args[1], out targetObject, out forallBody)) return null;
                        var boundParameters = MetaDataDecoder.Parameters(forallBody);
                        Contract.Assume(boundParameters.Count == 1, "should be ensured by ForAll/Exists type");
                        var boundParameter = boundParameters[0];
                        var boundParameterType = MetaDataDecoder.ParameterType(boundParameter);
                        forallBody = MetaDataDecoder.Unspecialized(forallBody);
                        if (!MetaDataDecoder.HasBody(forallBody)) return null;
                        // find enumerable (arg to forall is enumerable object version)
                        SymbolicValue enumerable;
                        if (!driver.Context.ValueContext.TryGetObjectFromObjectVersion(pc, args[0], out enumerable)) return null;
                        // find model array in enumerable
                        var enumerableType = driver.Context.ValueContext.GetType(pc, enumerable);
                        SymbolicValue modelArray;
                        if (enumerableType.IsNormal && MetaDataDecoder.IsArray(enumerableType.Value))
                        {
                            modelArray = enumerable; // itself
                        }
                        else
                        {
                            if (!driver.Context.ValueContext.TryGetModelArray(pc, enumerable, out modelArray)) return null;
                        }
                        SymbolicValue arrayLen;
                        if (!driver.Context.ValueContext.TryGetArrayLength(pc, modelArray, out arrayLen)) return null;
                        // bound variable is array index
                        var index = BoxedExpression.Var("i", null, MetaDataDecoder.System_Int32);
                        var arrayBox = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, modelArray), driver.ExpressionDecoder);
                        if (arrayBox != null)
                        {
                            boundVariable = BoxedExpression.ArrayIndex<Type>(arrayBox, index, boundParameterType);
                            var bodyDecoded = MetaDataDecoder.AccessMethodBody(forallBody, this, Unit.Value);
                            if (bodyDecoded == null) return null;
                            var lowerBound = BoxedExpression.Const(0, MetaDataDecoder.System_Int32, MetaDataDecoder);
                            var upperBound = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, arrayLen), driver.ExpressionDecoder);

                            if (/*lowerBound != null && */ upperBound != null)
                            {
                                return MakeQuantifierExpression(value, quantifier, index, lowerBound, upperBound, bodyDecoded);
                            }
                        }
                    }
                    else if (methodName == NameTable[(int)quantifier, 2] && typeName == "Array")
                    {
                        Method forallBody;
                        if (!driver.Context.ValueContext.IsDelegateValue(pc, args[1], out targetObject, out forallBody)) return null;
                        var boundParameters = MetaDataDecoder.Parameters(forallBody);
                        Contract.Assume(boundParameters != null && boundParameters.Count == 1, "should be ensured by TrueForAll/Exists type");
                        var boundParameter = boundParameters[0];
                        var boundParameterType = MetaDataDecoder.ParameterType(boundParameter);
                        forallBody = MetaDataDecoder.Unspecialized(forallBody);
                        if (!MetaDataDecoder.HasBody(forallBody)) return null;
                        // find enumerable (arg to forall is enumerable object version)
                        SymbolicValue modelArray;
                        if (!driver.Context.ValueContext.TryGetObjectFromObjectVersion(pc, args[0], out modelArray)) return null;
                        // find array length
                        SymbolicValue arrayLen;
                        if (!driver.Context.ValueContext.TryGetArrayLength(pc, modelArray, out arrayLen)) return null;
                        // bound variable is array index
                        var index = BoxedExpression.Var("i", null, MetaDataDecoder.System_Int32);
                        var arrayBox = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, modelArray), driver.ExpressionDecoder);
                        if (arrayBox != null)
                        {
                            boundVariable = BoxedExpression.ArrayIndex<Type>(arrayBox, index, boundParameterType);
                            var bodyDecoded = MetaDataDecoder.AccessMethodBody(forallBody, this, Unit.Value);
                            if (bodyDecoded == null) return null;
                            var lowerBound = BoxedExpression.Const(0, MetaDataDecoder.System_Int32, MetaDataDecoder);
                            var upperBound = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, arrayLen), driver.ExpressionDecoder);

                            if (/*lowerBound != null && */ upperBound != null)
                            {
                                return MakeQuantifierExpression(value, quantifier, index, lowerBound, upperBound, bodyDecoded);
                            }
                        }
                    }
                    return null;
                }
                if (args.Length == 3)
                {
                    if (driver.MetaDataDecoder.Name(method) != NameTable[(int)quantifier, 0]) return null;
                    if (driver.MetaDataDecoder.Name(driver.MetaDataDecoder.DeclaringType(method)) != "Contract") return null;
                    Method forallBody;
                    if (!driver.Context.ValueContext.IsDelegateValue(pc, args[2], out targetObject, out forallBody)) return null;
                    forallBody = driver.MetaDataDecoder.Unspecialized(forallBody);
                    if (!driver.MetaDataDecoder.HasBody(forallBody)) return null;
                    // bound variable is index
                    boundVariable = BoxedExpression.Var("i", null, driver.MetaDataDecoder.System_Int32);

                    var bodyDecoded = driver.MetaDataDecoder.AccessMethodBody(forallBody, this, Unit.Value);
                    if (bodyDecoded == null) return null;
                    var lowerBound = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, args[0]), driver.ExpressionDecoder);
                    var upperBound = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(pc, args[1]), driver.ExpressionDecoder);

                    if (lowerBound != null && upperBound != null)
                    {
                        return MakeQuantifierExpression(value, quantifier, boundVariable, lowerBound, upperBound, bodyDecoded);
                    }
                }
                return null;
            }

            [ContractVerification(true)]
            private static QuantifiedIndexedExpression MakeQuantifierExpression(
              object variable,
              Quantifier quantifier,
              BoxedExpression index, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression body)
            {
                Contract.Requires(index != null);
                Contract.Requires(lowerBound != null);
                Contract.Requires(upperBound != null);
                Contract.Requires(body != null);

                Contract.Ensures(Contract.Result<QuantifiedIndexedExpression>() != null);

                switch (quantifier)
                {
                    case Quantifier.Exists:
                        return new ExistsIndexedExpression(variable, index, lowerBound, upperBound, body);

                    case Quantifier.ForAll:
                        return new ForAllIndexedExpression(variable, index, lowerBound, upperBound, body);

                    default:
                        Contract.Assert(false);
                        return null;
                }
            }

            #region IMethodCodeConsumer<Local,Parameter,Method,Field,Type,Env,BoxedExpression> Members

            private class Env
            {
                public struct Both
                {
                    public BoxedExpression Boxed;
                    public SymbolicValue Sym;

                    public Both(BoxedExpression boxedExpression)
                    {
                        this.Boxed = boxedExpression;
                        this.Sym = default(SymbolicValue);
                    }

                    public Both(SymbolicValue sym)
                    {
                        this.Boxed = null;
                        this.Sym = sym;
                    }
                }

                private readonly Stack<Both> stack = new Stack<Both>();
                readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver;
                readonly private APC contractPC;

                public Env(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> iMethodDriver,
                           APC contractPC)
                {
                    Contract.Requires(iMethodDriver != null);

                    this.contractPC = contractPC;
                    driver = iMethodDriver;
                }

                internal bool PopBE(out BoxedExpression boxed)
                {
                    Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out boxed) != null);

                    var both = Pop();
                    if (both.Boxed != null) { boxed = both.Boxed; return true; }
                    if (!driver.Context.ValueContext.IsValid(both.Sym)) { boxed = null; return false; }
                    boxed = BoxedExpression.Convert(driver.Context.ExpressionContext.Refine(contractPC, both.Sym), driver.ExpressionDecoder);

                    // conversion may fail, so we check it

                    return boxed != null ? true : false;
                }

                internal void Push(BoxedExpression boxedExpression)
                {
                    stack.Push(new Both(boxedExpression));
                }

                internal void Push(SymbolicValue sym)
                {
                    stack.Push(new Both(sym));
                }

                internal bool PopSV(out SymbolicValue sym)
                {
                    var both = Pop();
                    if (!driver.Context.ValueContext.IsValid(both.Sym)) { sym = default(SymbolicValue); return false; }
                    sym = both.Sym;
                    return true;
                }

                internal Both Pop()
                {
                    if (stack.Count > 0) { return stack.Pop(); }
                    return default(Both);
                }

                internal bool Dup(int offset)
                {
                    if (stack.Count > offset)
                    {
                        var array = stack.ToArray();
                        var toCopy = array[stack.Count - 1 - offset];
                        stack.Push(toCopy);
                        return true;
                    }
                    return false;
                }

                public int StackDepth { get { return stack.Count; } }
            }

            private class Decoder<Label2, Handler> : ICodeQuery<Label2, Local, Parameter, Method, Field, Type, Env, bool>
            {
                internal BoxedExpression resultExpression;
                private BoxedExpression boundVar;
                private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> driver;
                private Method body;
                private bool isStatic;
                private SymbolicValue closure;
                private APC contractPC;
                private IMethodCodeProvider<Label2, Local, Parameter, Method, Field, Type, Handler> codeProvider;

                public Decoder(
                  APC contractPC,
                  IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, LogOptions> iMethodDriver,
                  SymbolicValue closure,
                  Method body,
                  BoxedExpression boundVar,
                  IMethodCodeProvider<Label2, Local, Parameter, Method, Field, Type, Handler> codeProvider)
                {
                    this.closure = closure;
                    this.contractPC = contractPC;
                    driver = iMethodDriver;
                    this.body = body;
                    this.codeProvider = codeProvider;
                    this.boundVar = boundVar;
                    isStatic = driver.MetaDataDecoder.IsStatic(body);
                }


                #region ICodeQuery<Label2,Local,Parameter,Method,Field,Type,Env,BoxedExpression> Members

                public bool Aggregate(Label2 current, Label2 aggregateStart, bool canBeTargetOfBranch, Env data)
                {
                    current = aggregateStart;
                    do
                    {
                        var success = codeProvider.Decode<Decoder<Label2, Handler>, Env, bool>(current, this, data);
                        if (!success) return false;
                    }
                    while (codeProvider.Next(current, out current));
                    return true;
                }

                #endregion

                #region IVisitMSIL<Label2,Local,Parameter,Method,Field,Type,Unit,Unit,Env,BoxedExpression> Members

                public bool Arglist(Label2 pc, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool BranchCond(Label2 pc, Label2 target, BranchOperator bop, DataStructures.Unit value1, DataStructures.Unit value2, Env data)
                {
                    return false;
                }

                public bool BranchTrue(Label2 pc, Label2 target, DataStructures.Unit cond, Env data)
                {
                    return false;
                }

                public bool BranchFalse(Label2 pc, Label2 target, DataStructures.Unit cond, Env data)
                {
                    return false;
                }

                public bool Branch(Label2 pc, Label2 target, bool leave, Env data)
                {
                    return false;
                }

                public bool Break(Label2 pc, Env data)
                {
                    return true;
                }

                public bool Call<TypeList, ArgList>(Label2 pc, Method method, bool tail, bool virt, TypeList extraVarargs, DataStructures.Unit dest, ArgList args, Env data)
                  where TypeList : DataStructures.IIndexable<Type>
                  where ArgList : DataStructures.IIndexable<DataStructures.Unit>
                {
                    var mdDecoder = driver.MetaDataDecoder;
                    var declaringType = mdDecoder.DeclaringType(method);
                    var methodName = mdDecoder.Name(method);

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
                    return false;
                }

                public bool Calli<TypeList, ArgList>(Label2 pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, DataStructures.Unit dest, DataStructures.Unit fp, ArgList args, Env data)
                  where TypeList : DataStructures.IIndexable<Type>
                  where ArgList : DataStructures.IIndexable<DataStructures.Unit>
                {
                    return false;
                }

                public bool Ckfinite(Label2 pc, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                public bool Cpblk(Label2 pc, bool @volatile, DataStructures.Unit destaddr, DataStructures.Unit srcaddr, DataStructures.Unit len, Env data)
                {
                    return false;
                }

                public bool Endfilter(Label2 pc, DataStructures.Unit decision, Env data)
                {
                    return false;
                }

                public bool Endfinally(Label2 pc, Env data)
                {
                    return false;
                }

                public bool Initblk(Label2 pc, bool @volatile, DataStructures.Unit destaddr, DataStructures.Unit value, DataStructures.Unit len, Env data)
                {
                    return false;
                }

                public bool Jmp(Label2 pc, Method method, Env data)
                {
                    return false;
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
                private Parameter RemapParameter(Parameter p, CFGBlock parentMethodBlock, CFGBlock subroutineBlock)
                {
                    Contract.Requires(subroutineBlock != null);// F: added because of Clousot suggestion
                    Contract.Requires(parentMethodBlock != null);// F: added because of Clousot suggestion

                    var mdDecoder = driver.MetaDataDecoder;

                    IMethodInfo<Method> parentMethodInfo = (IMethodInfo<Method>)parentMethodBlock.Subroutine;
                    IMethodInfo<Method> subroutineMethodInfo = (IMethodInfo<Method>)subroutineBlock.Subroutine;

                    if (mdDecoder.Equal(parentMethodInfo.Method, subroutineMethodInfo.Method))
                    {
                        return p;
                    }

                    var argIndex = mdDecoder.ArgumentIndex(p);
                    Contract.Assume(argIndex >= 0);

                    if (mdDecoder.IsStatic(parentMethodInfo.Method))
                    {
                        return mdDecoder.Parameters(parentMethodInfo.Method)[argIndex];
                    }
                    else
                    {
                        if (argIndex == 0)
                        {
                            // refers to "this"
                            return mdDecoder.This(parentMethodInfo.Method);
                        }
                        var parameters = mdDecoder.Parameters(parentMethodInfo.Method).AssumeNotNull();
                        Contract.Assume(argIndex < parameters.Count);
                        return parameters[argIndex - 1];
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
                private bool RemapParameterToLdStack(APC pc, ref Parameter p, out bool isLdResult, out int ldStackOffset, out bool isOld, out APC lookupPC, Env data)
                {
                    Contract.Requires(data != null);

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

                            for (var scontext = pc.SubroutineContext; scontext != null; scontext = scontext.Tail)
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
                                    int localStackDepth = driver.Context.StackContext.LocalStackDepth(contractPC) + data.StackDepth;
                                    ldStackOffset = driver.MetaDataDecoder.ArgumentStackIndex(p) + localStackDepth;
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
                                    ldStackOffset = driver.MetaDataDecoder.ArgumentStackIndex(p);

                                    // no need to correct for local stack depth, as we lookup the stack in the old-state
                                    isLdResult = false;
                                    lookupPC = new APC(scontext.Head.One, 0, scontext.Tail);
                                    return true;
                                }
                                if (callTag == "afterNewObj")
                                {
                                    // here we have to deal with the special case of referencing argument 0, which is the result
                                    // of the construction. In that case we have to return "ldresult"
                                    int parameterIndex = driver.MetaDataDecoder.ArgumentIndex(p);
                                    if (parameterIndex == 0)
                                    {
                                        ldStackOffset = driver.Context.StackContext.LocalStackDepth(pc) + data.StackDepth;
                                        isLdResult = true;
                                        lookupPC = pc; // irrelevant
                                        isOld = false;
                                        return false;
                                    }

                                    // we need to compute the offset to the "parameter" on the stack. For this we must
                                    // know what method is being called.
                                    ldStackOffset = driver.MetaDataDecoder.ArgumentStackIndex(p);

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
                                // must be "this"
                                Contract.Assume(driver.MetaDataDecoder.ArgumentIndex(p) == 0);

                                if (callTag == "entry" || callTag == "exit")
                                {
                                    // keep as ldarg, but remap to this of containing method
                                    Method containingMethod;
                                    if (pc.TryGetContainingMethod(out containingMethod))
                                    {
                                        p = driver.MetaDataDecoder.This(containingMethod);
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
                                    var success = scontext.Head.One.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall);
                                    Contract.Assume(success); // must be a method call blcok
                                    int parameterCount = driver.MetaDataDecoder.Parameters(calledMethod).Count;

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
                                    ldStackOffset = driver.Context.StackContext.LocalStackDepth(pc) + data.StackDepth;
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
                /// We have to distinguish 3 cases:
                /// 
                /// 1. Access to an argument of the closure method (the index)
                /// 2. Access to the "this" argument of the closure method (to get to the closure values under CCI1)
                /// 3. Access to the enclosing method parameters (under CCI2)
                ///    3a. This can be the method under analysis, if the forall is a pre/post condition
                ///    3b. This can be a called method from the method under analysis (at new or call)
                ///    3c. This can be called methods from within contracts of called methods
                /// </summary>
                public bool Ldarg(Label2 pc, Parameter argument, bool isOldIgnored, DataStructures.Unit dest, Env data)
                {
                    Contract.Assume(data != null);

                    // determine if argument belongs to method on stack at contarctPC
                    if (!OnStack(contractPC, driver.MetaDataDecoder.DeclaringMethod(argument)))
                    {
                        // could be closure this
                        if (!isStatic && driver.MetaDataDecoder.ArgumentIndex(argument) == 0)
                        {
                            data.Push(closure);
                            return true;
                        }
                        // can only be index
                        data.Push(boundVar);
                        return true;
                    }

                    // First check to see if the parameter is a parameter of the method enclosing the
                    // quantifier.
                    SymbolicValue sv;
                    if (driver.Context.ValueContext.TryParameterValue(contractPC, argument, out sv))
                    {
                        data.Push(sv);
                        return true;
                    }

                    // Now try to figure out where on the evaluation stack the parameters of this inner call would be
                    bool isLdResult;
                    int ldStackOffset;
                    bool isOld;
                    APC lookupPC;
                    var mdDecoder = driver.MetaDataDecoder;

                    Parameter oldArgument = argument;

                    if (RemapParameterToLdStack(contractPC, ref argument, out isLdResult, out ldStackOffset, out isOld, out lookupPC, data))
                    {
                        // ldstack
                        return this.LdstackSpecial(lookupPC, ldStackOffset, dest, Unit.Value, isOld, data);
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
                        if (mdDecoder.IsStruct(mdDecoder.DeclaringType(mdDecoder.DeclaringMethod(argument))))
                        {
                            return this.Ldstacka(pc, ldStackOffset, dest, Unit.Value, mdDecoder.ParameterType(argument), isOld, data);
                        }
                        return this.Ldresult(pc, mdDecoder.ParameterType(argument), dest, Unit.Value, data);
                    }

                    // if we can't find anything, it might be the index
                    data.Push(boundVar);
                    return true;
                }

                private bool OnStack(APC aPC, Method method)
                {
                    var pcMethod = aPC.Block.Subroutine as IMethodInfo<Method>;
                    if (pcMethod != null && driver.MetaDataDecoder.Equal(pcMethod.Method, method)) return true;
                    for (var context = aPC.SubroutineContext; context != null; context = context.Tail)
                    {
                        pcMethod = context.Head.One.AssumeNotNull().Subroutine as IMethodInfo<Method>;
                        if (pcMethod != null && driver.MetaDataDecoder.Equal(pcMethod.Method, method)) return true;
                    }
                    return false;
                }

                public bool Ldarga(Label2 pc, Parameter argument, bool isOld, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldftn(Label2 pc, Method method, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldind(Label2 pc, Type type, bool @volatile, DataStructures.Unit dest, DataStructures.Unit ptr, Env data)
                {
                    SymbolicValue ptrValue;
                    if (!data.PopSV(out ptrValue)) return false;
                    SymbolicValue result;
                    if (!driver.Context.ValueContext.TryLoadIndirect(contractPC, ptrValue, out result)) return false;
                    data.Push(result);
                    return true;
                }

                public bool Ldloc(Label2 pc, Local local, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldloca(Label2 pc, Local local, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Localloc(Label2 pc, DataStructures.Unit dest, DataStructures.Unit size, Env data)
                {
                    return false;
                }

                public bool Nop(Label2 pc, Env data)
                {
                    return true;
                }

                public bool Pop(Label2 pc, DataStructures.Unit source, Env data)
                {
                    data.Pop();
                    return true;
                }

                public bool Return(Label2 pc, DataStructures.Unit source, Env data)
                {
                    return data.PopBE(out this.resultExpression);
                }

                public bool Starg(Label2 pc, Parameter argument, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                public bool Stind(Label2 pc, Type type, bool @volatile, DataStructures.Unit ptr, DataStructures.Unit value, Env data)
                {
                    return false;
                }

                public bool Stloc(Label2 pc, Local local, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                public bool Switch(Label2 pc, Type type, System.Collections.Generic.IEnumerable<DataStructures.Pair<object, Label2>> cases, DataStructures.Unit value, Env data)
                {
                    return false;
                }

                public bool Box(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    // HACK: just leave the same value on the stack
                    return true;
                }

                public bool ConstrainedCallvirt<TypeList, ArgList>(Label2 pc, Method method, bool tail, Type constraint, TypeList extraVarargs, DataStructures.Unit dest, ArgList args, Env data)
                  where TypeList : DataStructures.IIndexable<Type>
                  where ArgList : DataStructures.IIndexable<DataStructures.Unit>
                {
                    return false;
                }

                public bool Castclass(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return true; // just leave the same value on the stack
                }

                public bool Cpobj(Label2 pc, Type type, DataStructures.Unit destptr, DataStructures.Unit srcptr, Env data)
                {
                    return false;
                }

                public bool Initobj(Label2 pc, Type type, DataStructures.Unit ptr, Env data)
                {
                    return false;
                }

                public bool Ldelem(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit array, DataStructures.Unit index, Env data)
                {
                    BoxedExpression indexExpr;
                    if (!data.PopBE(out indexExpr)) return false;
                    BoxedExpression arrayExpr;
                    if (!data.PopBE(out arrayExpr)) return false;
                    data.Push(BoxedExpression.ArrayIndex(arrayExpr, indexExpr, type));
                    return true;
                }

                public bool Ldelema(Label2 pc, Type type, bool @readonly, DataStructures.Unit dest, DataStructures.Unit array, DataStructures.Unit index, Env data)
                {
                    return false;
                }

                public bool Ldfld(Label2 pc, Field field, bool @volatile, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    SymbolicValue objectValue;
                    if (!data.PopSV(out objectValue)) return false;
                    SymbolicValue fieldAddress;
                    if (!driver.Context.ValueContext.TryFieldAddress(contractPC, objectValue, field, out fieldAddress)) return false;
                    SymbolicValue result;
                    if (!driver.Context.ValueContext.TryLoadIndirect(contractPC, fieldAddress, out result)) return false;
                    data.Push(result);
                    return true;
                }

                public bool Ldflda(Label2 pc, Field field, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                public bool Ldlen(Label2 pc, DataStructures.Unit dest, DataStructures.Unit array, Env data)
                {
                    SymbolicValue objectValue;
                    if (!data.PopSV(out objectValue)) return false;
                    SymbolicValue length;
                    if (!driver.Context.ValueContext.TryGetArrayLength(contractPC, objectValue, out length)) return false;
                    data.Push(length);
                    return true;
                }

                public bool Ldsfld(Label2 pc, Field field, bool @volatile, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldsflda(Label2 pc, Field field, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldtypetoken(Label2 pc, Type type, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldfieldtoken(Label2 pc, Field field, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldmethodtoken(Label2 pc, Method method, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Ldvirtftn(Label2 pc, Method method, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                public bool Mkrefany(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                public bool Newarray<ArgList>(Label2 pc, Type type, DataStructures.Unit dest, ArgList len, Env data) where ArgList : DataStructures.IIndexable<DataStructures.Unit>
                {
                    return false;
                }

                public bool Newobj<ArgList>(Label2 pc, Method ctor, DataStructures.Unit dest, ArgList args, Env data) where ArgList : DataStructures.IIndexable<DataStructures.Unit>
                {
                    return false;
                }

                public bool Refanytype(Label2 pc, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                public bool Refanyval(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                public bool Rethrow(Label2 pc, Env data)
                {
                    return false;
                }

                public bool Stelem(Label2 pc, Type type, DataStructures.Unit array, DataStructures.Unit index, DataStructures.Unit value, Env data)
                {
                    return false;
                }

                public bool Stfld(Label2 pc, Field field, bool @volatile, DataStructures.Unit obj, DataStructures.Unit value, Env data)
                {
                    return false;
                }

                public bool Stsfld(Label2 pc, Field field, bool @volatile, DataStructures.Unit value, Env data)
                {
                    return false;
                }

                public bool Throw(Label2 pc, DataStructures.Unit exn, Env data)
                {
                    return false;
                }

                public bool Unbox(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                public bool Unboxany(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                #endregion

                #region IVisitSynthIL<Label2,Method,Type,Unit,Unit,Env,BoxedExpression> Members

                public bool Entry(Label2 pc, Method method, Env data)
                {
                    return true;
                }

                public bool Assume(Label2 pc, string tag, DataStructures.Unit condition, object provenance, Env data)
                {
                    return false;
                }

                public bool Assert(Label2 pc, string tag, DataStructures.Unit condition, object provenance, Env data)
                {
                    return false;
                }

                private bool LdstackSpecial(APC apc, int offset, DataStructures.Unit dest, DataStructures.Unit source, bool isOld, Env data)
                {
                    if (offset >= data.StackDepth)
                    {
                        var normalizedOffset = offset - data.StackDepth;
                        var top = driver.Context.StackContext.StackDepth(apc) - 1;
                        var index = top - normalizedOffset;
                        SymbolicValue result;
                        if (driver.Context.ValueContext.TryStackValue(apc, index, out result))
                        {
                            data.Push(result);
                            return true;
                        }
                    }
                    return data.Dup(offset);
                }

                public bool Ldstack(Label2 pc, int offset, DataStructures.Unit dest, DataStructures.Unit source, bool isOld, Env data)
                {
                    return data.Dup(offset);
                }

                public bool Ldstacka(Label2 pc, int offset, DataStructures.Unit dest, DataStructures.Unit source, Type origParamType, bool isOld, Env data)
                {
                    return false;
                }

                public bool Ldresult(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    SymbolicValue result;

                    if (!driver.Context.ValueContext.TryResultValue(contractPC, out result)) return false;

                    data.Push(result);

                    return true;
                }

                public bool BeginOld(Label2 pc, Label2 matchingEnd, Env data)
                {
                    return false;
                }

                public bool EndOld(Label2 pc, Label2 matchingBegin, Type type, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    return false;
                }

                #endregion

                #region IVisitExprIL<Label2,Type,Unit,Unit,Env,BoxedExpression> Members

                public bool Binary(Label2 pc, BinaryOperator op, DataStructures.Unit dest, DataStructures.Unit s1, DataStructures.Unit s2, Env data)
                {
                    BoxedExpression right;
                    if (!data.PopBE(out right)) return false;
                    BoxedExpression left;
                    if (!data.PopBE(out left)) return false;
                    data.Push(BoxedExpression.Binary(op, left, right));
                    return true;
                }

                public bool Isinst(Label2 pc, Type type, DataStructures.Unit dest, DataStructures.Unit obj, Env data)
                {
                    return false;
                }

                public bool Ldconst(Label2 pc, object constant, Type type, DataStructures.Unit dest, Env data)
                {
                    data.Push(BoxedExpression.Const(constant, type, driver.MetaDataDecoder));
                    return true;
                }

                public bool Ldnull(Label2 pc, DataStructures.Unit dest, Env data)
                {
                    data.Push(BoxedExpression.Const(null, default(Type), driver.MetaDataDecoder));
                    return true;
                }

                public bool Sizeof(Label2 pc, Type type, DataStructures.Unit dest, Env data)
                {
                    return false;
                }

                public bool Unary(Label2 pc, UnaryOperator op, bool overflow, bool unsigned, DataStructures.Unit dest, DataStructures.Unit source, Env data)
                {
                    BoxedExpression arg;
                    if (!data.PopBE(out arg)) return false;
                    data.Push(BoxedExpression.Unary(op, arg));
                    return true;
                }

                #endregion
            }

            BoxedExpression IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, BoxedExpression>.Accept<Label2, Handler>(
              IMethodCodeProvider<Label2, Local, Parameter, Method, Field, Type, Handler> codeProvider,
              Label2 entryPoint, Method method, Unit unit)
            {
                var current = entryPoint;
                Env env = new Env(driver, contractPC);
                var decoder = new Decoder<Label2, Handler>(contractPC, driver, targetObject, method, boundVariable, codeProvider);
                if (!decoder.Aggregate(entryPoint, entryPoint, false, env)) return null;
                return decoder.resultExpression;
            }

            #endregion
        }
    }
}