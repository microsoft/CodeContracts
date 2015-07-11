// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.Slicing
{
    using Dest = Int32;
    using Source = Int32;

    [ContractVerification(false)]
    public abstract class Chain<M, F, T> : IChain<M, F, T>
    {
        public virtual ChainTag Tag { get { throw new InvalidOperationException(); } }
        public virtual F Field { get { throw new InvalidOperationException(); } }
        public virtual M Method { get { throw new InvalidOperationException(); } }
        public virtual T Type { get { throw new InvalidOperationException(); } }
        public virtual MethodHashAttribute MethodHashAttribute { get { throw new InvalidOperationException(); } internal set { throw new InvalidOperationException(); } }
        public virtual IEnumerable<IChain<M, F, T>> Children { get { throw new InvalidOperationException(); } }


        public abstract class MemberBindingChain<Member> : Chain<M, F, T>
        {
            protected readonly Member member;
            protected MemberBindingChain(Member member) { this.member = member; }
            public override int GetHashCode() { return this.member.GetHashCode(); }
            public override string ToString() { return String.Format("{0}:{1}", this.Tag, this.member); }
        }

        private class FieldChain : MemberBindingChain<F>
        {
            public FieldChain(F f) : base(f) { }
            public override ChainTag Tag { get { return ChainTag.Field; } }
            public override F Field { get { return this.member; } }
        }

        internal class MethodChain : MemberBindingChain<M>
        {
            public MethodChain(M m) : base(m) { }
            public override ChainTag Tag { get { return ChainTag.Method; } }
            public override M Method { get { return this.member; } }
            public override MethodHashAttribute MethodHashAttribute { get; internal set; } // cannot do only override get;
        }

        public class TypeRootChain : MemberBindingChain<T>
        {
            public TypeRootChain(T t) : base(t) { }
            public override ChainTag Tag { get { return ChainTag.Type; } }
            public override T Type { get { return this.member; } }

            private readonly Dictionary<T, Chain<M, F, T>> _childrenT = new Dictionary<T, Chain<M, F, T>>();

            internal void Add(T t, Chain<M, F, T> chain)
            {
                Contract.Assume(!_childrenT.ContainsKey(t));
                _childrenT.Add(t, chain);
            }

            public override IEnumerable<IChain<M, F, T>> Children { get { return _childrenT.Values; } }
        }

        internal class TypeChain : TypeRootChain
        {
            public TypeChain(T t) : base(t) { }

            // We use three different dictionaries because equality is lost if a M, a F, or a T is boxed into an Object
            private readonly Dictionary<M, MethodChain> _childrenM = new Dictionary<M, MethodChain>();
            private readonly Dictionary<F, FieldChain> _childrenF = new Dictionary<F, FieldChain>();

            internal TypeChain ContractClass { get; set; }

            internal void Add(F f)
            {
                if (!_childrenF.ContainsKey(f))
                    _childrenF.Add(f, new FieldChain(f));
            }

            internal MethodChain Add(M m)
            {
                Contract.Assume(!_childrenM.ContainsKey(m));
                var chain = new MethodChain(m);
                _childrenM.Add(m, chain);
                return chain;
            }

            public override IEnumerable<IChain<M, F, T>> Children { get { return base.Children.Concat(_childrenM.Values).Concat(_childrenF.Values); } }
        }

        public class RootChain : TypeRootChain
        {
            public RootChain() : base(default(T)) { }
            public override int GetHashCode() { return 0; }
            public override string ToString() { return "RootChain"; }
        }
    }

    public class Slice<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : Chain<Method, Field, Type>.RootChain, ISlice<Method, Field, Type, Assembly>
      where Type : IEquatable<Type>
    {
        #region Object invariants

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_containingAssembly != null);
            Contract.Invariant(_containingAssemblyName != null);
            Contract.Invariant(this.mdDecoder != null);
            Contract.Invariant(this.methods != null);
            Contract.Invariant(this.dependencies != null);
            Contract.Invariant(this.contractDecoder != null);
        }

        #endregion

        // the state should not contain method-specific objects

        private readonly string _name;
        private readonly Assembly _containingAssembly;
        private readonly string _containingAssemblyName;
        protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
        protected readonly IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder;
        protected readonly HashSet<Method> methods = new HashSet<Method>();
        protected readonly HashSet<Method> dependencies = new HashSet<Method>();

        public string Name { get { return _name; } }
        public Assembly ContainingAssembly { get { return _containingAssembly; } }
        public IEnumerable<Method> Methods { get { return this.methods; } }
        public IEnumerable<IChain<Method, Field, Type>> Chains { get { return this.Children; } }
        public IEnumerable<Method> Dependencies { get { return this.dependencies; } }

        private IEnumerable<Method> _cachedMethodsInTheSameType = null;
        public IEnumerable<Method> OtherMethodsInTheSameType
        {
            get
            {
                if (_cachedMethodsInTheSameType == null)
                {
                    var tmp = new Set<Method>();

                    foreach (var method in this.methods)
                    {
                        foreach (var m in this.mdDecoder.Methods(this.mdDecoder.DeclaringType(method)))
                        {
                            if (!method.Equals(m))
                            {
                                tmp.Add(m);
                            }
                        }
                    }

                    _cachedMethodsInTheSameType = tmp;
                }
                Contract.Assert(_cachedMethodsInTheSameType != null);

                return _cachedMethodsInTheSameType;
            }
        }


        protected Slice(
          string name,
          Assembly containingAssembly,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder)
        {
            Contract.Requires(containingAssembly != null);
            Contract.Requires(contractDecoder != null);
            Contract.Requires(mdDecoder != null);

            _name = name;
            _containingAssembly = containingAssembly;
            _containingAssemblyName = mdDecoder.Name(containingAssembly);
            this.mdDecoder = mdDecoder;
            this.contractDecoder = contractDecoder;
        }

        protected bool IsAssemblySelected(string moduleName)
        {
            return moduleName == _containingAssemblyName;
        }
    }

    public class SliceBuilder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Context, Expression, SymbolicValue>
      : Slice<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
      where Type : IEquatable<Type>
      where Method : IEquatable<Method>
    {
        #region Object invariants

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_cachedTouchedTypes != null);
            Contract.Invariant(_cachedTouchedMethods != null);
            Contract.Invariant(_cachedTouchedFields != null);
            Contract.Invariant(_methodsToTouchLater != null);
            Contract.Invariant(_typesToTouchLater != null);
            Contract.Invariant(_getMethodDriver != null);
        }

        #endregion

        private readonly Dictionary<Type, TypeChain> _cachedTouchedTypes = new Dictionary<Type, TypeChain>();
        private readonly Dictionary<Method, MethodChain> _cachedTouchedMethods = new Dictionary<Method, MethodChain>();
        private readonly Set<Field> _cachedTouchedFields = new Set<Field>();
        private readonly Set<Type> _typesToTouchLater = new Set<Type>();
        private readonly Set<Method> _methodsToTouchLater = new Set<Method>();
        private readonly Func<Method, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions>> _getMethodDriver;

        public SliceBuilder(
          string name,
          IEnumerable<Method> methods,
          Assembly containingAssembly,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
          Func<Method, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions>> getMethodDriver,
          bool includeOtherMethodsInTheType)
          : base(name, containingAssembly, mdDecoder, contractDecoder)
        {
            Contract.Requires(methods != null);
            Contract.Requires(containingAssembly != null);
            Contract.Requires(contractDecoder != null);
            Contract.Requires(mdDecoder != null);
            Contract.Requires(getMethodDriver != null);

            _getMethodDriver = getMethodDriver;

            foreach (var m in methods)
            {
                this.AddMethod(m);
            }

            if (includeOtherMethodsInTheType)
            {
                foreach (var m in this.OtherMethodsInTheSameType)
                {
                    Touch(m);
                }
            }
        }

        private void Touch(Type type)
        {
            // lazyly touch types to avoid cyclic recursivity

            if (!_cachedTouchedTypes.ContainsKey(type))
                _typesToTouchLater.Add(type);
        }
        private void Touch(Method method)
        {
            if (!_cachedTouchedMethods.ContainsKey(method))
                _methodsToTouchLater.Add(method);
        }

        private TypeChain GetTypeChain(Type type)
        {
            TypeChain chain;
            if (_cachedTouchedTypes.TryGetValue(type, out chain))
                return chain;

            if (this.mdDecoder.IsVoid(type) || this.mdDecoder.IsPrimitive(type) || this.mdDecoder.Equal(type, this.mdDecoder.System_Object) // primitive types
              || this.mdDecoder.IsFormalTypeParameter(type) || this.mdDecoder.IsMethodFormalTypeParameter(type)) // abstract types
            {
                _cachedTouchedTypes.Add(type, null);
                return null;
            }

            Type modified;
            IIndexable<Pair<bool, Type>> modifiers;
            if (this.mdDecoder.IsModified(type, out modified, out modifiers)) // modified types
            {
                foreach (var p in modifiers.Enumerate())
                    this.Touch(p.Two);
                chain = this.GetTypeChain(modified);
                _cachedTouchedTypes.Add(type, chain);
            }
            else if (this.mdDecoder.IsArray(type) || this.mdDecoder.IsManagedPointer(type) || this.mdDecoder.IsUnmanagedPointer(type)) // boxing types
            {
                chain = this.GetTypeChain(this.mdDecoder.ElementType(type));
                _cachedTouchedTypes.Add(type, chain);
            }
            else
            {
                IIndexable<Type> typeArguments;
                if (this.mdDecoder.NormalizedIsSpecialized(type, out typeArguments)) // specialized types
                {
                    foreach (var t in typeArguments.Enumerate())
                        this.Touch(t);
                    chain = this.GetTypeChain(this.mdDecoder.Unspecialized(type));
                    _cachedTouchedTypes.Add(type, chain);
                }
                else if (this.mdDecoder.Namespace(type) == null && this.mdDecoder.Name(type) == this.mdDecoder.FullName(type)) // weird types like function pointers (TODO)
                {
                    _cachedTouchedTypes.Add(type, null);
                    return null;
                }
                else // named types
                {
                    // here type should be a INamedTypeDefinition
                    if (!this.IsAssemblySelected(this.mdDecoder.DeclaringModuleName(type)))
                    {
                        // do not keep types from other assemblies
                        _cachedTouchedTypes.Add(type, null);
                        return null;
                    }

                    chain = new TypeChain(type);
                    _cachedTouchedTypes.Add(type, chain); // put this here because of cyclic dependences

                    Type parentType;
                    if (this.mdDecoder.IsNested(type, out parentType)) // nested types
                    {
                        var parentChain = this.GetTypeChain(parentType);
                        if (parentChain == null)
                        {
                            _cachedTouchedTypes[type] = null;
                            return null;
                        }
                        parentChain.Add(type, chain);
                    }
                    else
                        this.Add(type, chain);
                }
            }
            // No need to keep dependences of types that we won't keep anyway
            if (chain == null)
                return null;

            // Put this here to avoid infinite recursion

            if (this.mdDecoder.HasBaseClass(type)) // add the base class
                this.Touch(this.mdDecoder.BaseClass(type));

            foreach (var t in this.mdDecoder.Interfaces(type)) // add the implemented interfaces
                this.Touch(t);

            this.Touch(this.mdDecoder.GetAttributes(type), chain);

            foreach (var f in this.mdDecoder.Fields(type))
                this.Touch(f);
            foreach (var p in this.mdDecoder.Properties(type))
            {
                Method getter;
                if (this.mdDecoder.HasGetter(p, out getter))
                    this.Touch(getter);
            }
            foreach (var m in this.mdDecoder.Methods(type))
            {
                if (this.contractDecoder.IsPure(m))
                    this.Touch(m);
            }

            return chain;
        }
        private void Touch(IEnumerable<Attribute> attrs, TypeChain typeChain = null)
        {
            Contract.Requires(attrs != null);

            foreach (var attr in attrs)
                this.Touch(attr, typeChain);
        }
        private void Touch(Attribute attr, TypeChain typeChain = null)
        {
            var type = this.mdDecoder.AttributeType(attr);
            this.Touch(type);

            this.Touch(this.mdDecoder.AttributeConstructor(attr));

            foreach (var arg in this.mdDecoder.PositionalArguments(attr).Enumerate())
            {
                if (arg == null || arg is string || arg.GetType().IsPrimitive)
                    continue;
                if (arg is Type)
                    this.Touch((Type)arg);
                else
                    throw new NotImplementedException("Attribute argument of type " + arg.GetType().Name);
            }

            if (typeChain != null && this.mdDecoder.FullName(type) == NameFor.ContractClassAttribute)
            {
                var arg = this.mdDecoder.PositionalArguments(attr)[0];
                if (arg is Type)
                    typeChain.ContractClass = this.GetTypeChain((Type)arg);
            }
        }
        private void Touch(Parameter param)
        {
            this.Touch(this.mdDecoder.ParameterType(param));
            this.Touch(this.mdDecoder.GetAttributes(param));
        }
        private void Touch(Local local)
        {
            this.Touch(this.mdDecoder.LocalType(local));
        }
        private void TouchAutoProperty(Property property)
        {
            this.Touch(this.mdDecoder.DeclaringType(property));
            this.Touch(this.mdDecoder.PropertyType(property));
            Method getter, setter;
            if (this.mdDecoder.HasGetter(property, out getter))
                this.Touch(getter); // the contract decoder assumes an auto property has a non-null getter
            if (this.mdDecoder.HasSetter(property, out setter))
            {
                this.AddMethod(setter); // we will need it to retrieve the backing field again
                Field backingField;
                if (this.mdDecoder.IsAutoPropertySetter(setter, out backingField))
                    this.Touch(backingField);
            }
        }
        private void Touch(Field field)
        {
            if (_cachedTouchedFields.Add(field))
            {
                if (this.mdDecoder.IsSpecialized(field))
                    this.Touch(this.mdDecoder.Unspecialized(field));
                else
                {
                    var parentChain = this.GetTypeChain(this.mdDecoder.DeclaringType(field));
                    if (parentChain == null)
                        return;
                    parentChain.Add(field);
                    this.Touch(this.mdDecoder.FieldType(field));
                    this.Touch(this.mdDecoder.GetAttributes(field));
                }
            }
        }
        private MethodChain GetMethodChain(Method method)
        {
            MethodChain chain;
            if (_cachedTouchedMethods.TryGetValue(method, out chain))
                return chain;
            IIndexable<Type> methodTypeArguments;
            Method genericMethod;
            if (this.mdDecoder.IsSpecialized(method, out genericMethod, out methodTypeArguments))
            {
                foreach (var t in methodTypeArguments.Enumerate())
                    this.Touch(t);
                var unspecialized = genericMethod;
                if (!unspecialized.Equals(method)) // avoids infinite recursion for anonymous delegates
                {
                    chain = this.GetMethodChain(unspecialized);
                    _cachedTouchedMethods.Add(method, chain);
                    return chain;
                }
            }

            var declaringType = this.mdDecoder.DeclaringType(method);
            var declaringTypeChain = this.GetTypeChain(declaringType);

            if (declaringTypeChain == null || this.mdDecoder.IsDelegate(declaringType))
                chain = null;
            else
                chain = declaringTypeChain.Add(method);

            _cachedTouchedMethods.Add(method, chain);

            if (chain == null)
                return null;

            this.dependencies.Add(method);

            if (declaringTypeChain.ContractClass != null)
            {
                Method implementingMethod;
                if (this.mdDecoder.TryGetImplementingMethod(declaringTypeChain.ContractClass.Type, method, out implementingMethod))
                    this.Touch(implementingMethod);
            }

            if (this.mdDecoder.IsObjectInvariantMethod(method))
                this.AddMethod(method);

            this.Touch(this.mdDecoder.ReturnType(method));

            if (!this.mdDecoder.IsStatic(method))
                this.Touch(this.mdDecoder.This(method));

            foreach (var arg in this.mdDecoder.Parameters(method).Enumerate())
                this.Touch(arg);

            foreach (var local in this.mdDecoder.Locals(method).Enumerate())
                this.Touch(local);

            foreach (var m in this.mdDecoder.OverriddenAndImplementedMethods(method))
                this.Touch(m);

            this.Touch(this.mdDecoder.GetAttributes(method));

            if (this.mdDecoder.IsAutoPropertyMember(method))
            {
                var property = this.mdDecoder.GetPropertyFromAccessor(method);
                if (property != null)
                    this.TouchAutoProperty(property);
            }

            return chain;
        }

        private void TouchMethodBody(Method method)
        {
            var mDriver = _getMethodDriver(method);
            if (mDriver != null)
                new SlicerMethodVisitor(this, mDriver).TouchMe();
        }

        // Entry method
        // Add the method and its body
        public void AddMethod(Method method)
        {
            this.Touch(method);

            if (!this.methods.Add(method))
                return;

            this.TouchMethodBody(method);
        }

        public Slice<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> ComputeSlice(Func<Method, ByteArray> getMethodHash)
        {
            Contract.Requires(getMethodHash != null);

            // We were lazy, now it's time to work
            while (_typesToTouchLater.Any() || _methodsToTouchLater.Any())
            {
                while (_typesToTouchLater.Any())
                {
                    var t = _typesToTouchLater.First();
                    this.GetTypeChain(t);
                    _typesToTouchLater.Remove(t);
                }
                while (_methodsToTouchLater.Any())
                {
                    var m = _methodsToTouchLater.First();
                    this.GetMethodChain(m);
                    _methodsToTouchLater.Remove(m);
                }
            }

            this.dependencies.ExceptWith(this.methods);

            foreach (var m in this.dependencies)
            {
                if (m == null) continue;
                Contract.Assume(_cachedTouchedMethods[m] != null);
                _cachedTouchedMethods[m].MethodHashAttribute = new MethodHashAttribute(getMethodHash(m), MethodHashAttributeFlags.ForDependenceMethod);
            }
            foreach (var m in this.methods)
            {
                var methodChain = _cachedTouchedMethods[m];
                if (methodChain != null) // this should not happen, but it currently happens because of lambda/anonymous delegates
                    methodChain.MethodHashAttribute = new MethodHashAttribute(getMethodHash(m), MethodHashAttributeFlags.Default);
            }

            return this;
        }


        #region IL Visitor
        private class SlicerMethodVisitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Dest, Unit, Unit>
        {
            #region Object Invariant

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(_visitedSubroutines != null);
                Contract.Invariant(_mDriver != null);
                Contract.Invariant(_parent != null);
                Contract.Invariant(this.mdDecoder != null);
            }

            #endregion

            private readonly SliceBuilder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Context, Expression, SymbolicValue> _parent;
            private readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> _mDriver;

            private readonly Set<Subroutine> _visitedSubroutines = new Set<Subroutine>();

            private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder { get { return _parent.mdDecoder; } }
            private ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Source, Dest, IStackContext<Field, Method>, Unit> codeLayer { get { return _mDriver.StackLayer; } }

            public SlicerMethodVisitor(
              SliceBuilder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Context, Expression, SymbolicValue> parent,
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver)
            {
                Contract.Requires(parent != null);
                Contract.Requires(mDriver != null);

                _parent = parent;
                _mDriver = mDriver;
            }

            #region Slicing members

            public void TouchMe()
            {
                //this.Touch(mDriver.CFG.Subroutine);
                this.Touch(_mDriver.StackLayer.Decoder.Context.MethodContext.CFG.Subroutine);
            }

            private void Touch(Subroutine subroutine)
            {
                Contract.Requires(subroutine != null);

                if (!_visitedSubroutines.Add(subroutine))
                    return;

                var usedSubroutines = new Set<Subroutine>(subroutine.UsedSubroutines());
                var stackCFG = _mDriver.StackLayer.Decoder.Context.MethodContext.CFG;

                foreach (var block in subroutine.Blocks)
                {
                    Contract.Assume(block != null);
                    foreach (var apc in block.APCs())
                    {
                        _mDriver.StackLayer.Decoder.ForwardDecode<Unit, Unit, SlicerMethodVisitor>(apc, this, Unit.Value);
                    }

                    foreach (var taggedSucc in subroutine.SuccessorEdgesFor(block))
                    {
                        foreach (var edgeSub in stackCFG.GetOrdinaryEdgeSubroutines(block, taggedSucc.Two, null).GetEnumerable())
                        {
                            var sub = edgeSub.Two;
                            usedSubroutines.Add(sub);

                            var methodInfo = sub as IMethodInfo<Method>;
                            if (methodInfo != null && !_mDriver.CurrentMethod.Equals(methodInfo.Method))
                            {
                                _parent.Touch(methodInfo.Method);
                            }
                        }
                    }
                }
                foreach (var usedSubroutine in usedSubroutines)
                {
                    this.Touch(usedSubroutine);
                }
            }

            #endregion

            private void InlineMethod(Method method)
            {
                method = this.mdDecoder.Unspecialized(method);
                if (this.mdDecoder.HasBody(method))
                    this.Touch(_mDriver.AnalysisDriver.MethodCache.GetCFG(method).Subroutine);
            }

            #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Source,Dest,Unit,Unit> Members

            #region Should not be used in the stack based CFG -> NotImplementedException

            public Unit BranchCond(APC pc, APC target, BranchOperator bop, Source value1, Source value2, Unit data)
            {
                throw new NotImplementedException();
            }
            public Unit BranchTrue(APC pc, APC target, Source cond, Unit data)
            {
                throw new NotImplementedException();
            }
            public Unit BranchFalse(APC pc, APC target, Source cond, Unit data)
            {
                throw new NotImplementedException();
            }
            public Unit Branch(APC pc, APC target, bool leave, Unit data)
            {
                throw new NotImplementedException();
            }
            public Unit Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Source value, Unit data)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region No interesting argument -> do nothing

            public Unit Break(APC pc, Unit data)
            {
                return Unit.Value;
            }

            public Unit Arglist(APC pc, Dest dest, Unit data)
            {
                return Unit.Value;
            }

            public Unit Ckfinite(APC pc, Dest dest, Source source, Unit data)
            {
                return Unit.Value;
            }

            public Unit Cpblk(APC pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Unit data)
            {
                return Unit.Value;
            }

            public Unit Endfilter(APC pc, Source decision, Unit data)
            {
                return Unit.Value;
            }

            public Unit Endfinally(APC pc, Unit data)
            {
                return Unit.Value;
            }

            public Unit Initblk(APC pc, bool @volatile, Source destaddr, Source value, Source len, Unit data)
            {
                return Unit.Value;
            }

            public Unit Localloc(APC pc, Dest dest, Source size, Unit data)
            {
                return Unit.Value;
            }

            public Unit Nop(APC pc, Unit data)
            {
                return Unit.Value;
            }

            public Unit Pop(APC pc, Source source, Unit data)
            {
                return Unit.Value;
            }

            public Unit Return(APC pc, Source source, Unit data)
            {
                return Unit.Value;
            }

            public Unit Ldlen(APC pc, Dest dest, Source array, Unit data)
            {
                return Unit.Value;
            }

            public Unit Rethrow(APC pc, Unit data)
            {
                return Unit.Value;
            }

            public Unit Refanytype(APC pc, Dest dest, Source source, Unit data)
            {
                return Unit.Value;
            }

            public Unit Throw(APC pc, Source exn, Unit data)
            {
                return Unit.Value;
            }

            #endregion

            #region Calls

            public Unit Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Unit data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                _parent.Touch(method);

                if (args != null && args.Count > 0 && this.codeLayer.ContractDecoder.IsPure(method))
                {
                    // if we call Contract.{ForAll, Exists} with a delegate that we decompile, we need to hash the delegate's il too
                    var mName = this.mdDecoder.Name(method);
                    if ((mName == "ForAll") || (mName == "Exists"))
                    {
                        // last argument is closure
                        Method quantifierClosure;
                        if (this.codeLayer.Decoder.Context.StackContext.TryGetCallArgumentDelegateTarget(pc, args[args.Count - 1], out quantifierClosure))
                            this.InlineMethod(quantifierClosure);
                    }
                }

                if (extraVarargs != null)
                    foreach (var argType in extraVarargs.Enumerate())
                        _parent.Touch(argType);

                return Unit.Value;
            }

            public Unit ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, Unit data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                if (extraVarargs != null)
                    foreach (var argType in extraVarargs.Enumerate())
                        _parent.Touch(argType);

                return Unit.Value;
            }

            public Unit Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, Unit data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                _parent.Touch(returnType);

                if (argTypes != null)
                    foreach (var argType in argTypes.Enumerate())
                        _parent.Touch(argType);

                return Unit.Value;
            }

            #endregion

            #region Method -> Visit(method)

            public Unit Jmp(APC pc, Method method, Unit data)
            {
                _parent.Touch(method);
                return Unit.Value;
            }

            public Unit Ldftn(APC pc, Method method, Dest dest, Unit data)
            {
                _parent.Touch(method);
                return Unit.Value;
            }

            public Unit Ldmethodtoken(APC pc, Method method, Dest dest, Unit data)
            {
                _parent.Touch(method);
                return Unit.Value;
            }

            public Unit Ldvirtftn(APC pc, Method method, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(method);
                return Unit.Value;
            }

            public Unit Newobj<ArgList>(APC pc, Method ctor, Dest dest, ArgList args, Unit data) where ArgList : IIndexable<Source>
            {
                _parent.Touch(ctor);
                return Unit.Value;
            }

            #endregion

            #region Parameter -> Visit(argument)

            public Unit Ldarg(APC pc, Parameter argument, bool isOld, Dest dest, Unit data)
            {
                _parent.Touch(argument);
                return Unit.Value;
            }

            public Unit Ldarga(APC pc, Parameter argument, bool isOld, Dest dest, Unit data)
            {
                _parent.Touch(argument);
                return Unit.Value;
            }

            public Unit Starg(APC pc, Parameter argument, Source source, Unit data)
            {
                _parent.Touch(argument);
                return Unit.Value;
            }

            #endregion

            #region Type -> Visit(type)

            public Unit Castclass(APC pc, Type type, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Cpobj(APC pc, Type type, Source destptr, Source srcptr, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Initobj(APC pc, Type type, Source ptr, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Ldelem(APC pc, Type type, Dest dest, Source array, Source index, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Ldelema(APC pc, Type type, bool @readonly, Dest dest, Source array, Source index, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Ldind(APC pc, Type type, bool @volatile, Dest dest, Source ptr, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Stind(APC pc, Type type, bool @volatile, Source ptr, Source value, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Box(APC pc, Type type, Dest dest, Source source, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Ldtypetoken(APC pc, Type type, Dest dest, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Mkrefany(APC pc, Type type, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Newarray<ArgList>(APC pc, Type type, Dest dest, ArgList len, Unit data)
              where ArgList : IIndexable<Source>
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Refanyval(APC pc, Type type, Dest dest, Source source, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Stelem(APC pc, Type type, Source array, Source index, Source value, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Unbox(APC pc, Type type, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Unboxany(APC pc, Type type, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            #endregion

            #region Local -> Visit(local)

            public Unit Ldloc(APC pc, Local local, Dest dest, Unit data)
            {
                _parent.Touch(local);
                return Unit.Value;
            }

            public Unit Ldloca(APC pc, Local local, Dest dest, Unit data)
            {
                _parent.Touch(local);
                return Unit.Value;
            }

            public Unit Stloc(APC pc, Local local, Source source, Unit data)
            {
                _parent.Touch(local);
                return Unit.Value;
            }

            #endregion

            #region Field -> Visit(field)

            public Unit Ldfld(APC pc, Field field, bool @volatile, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Ldflda(APC pc, Field field, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Ldsfld(APC pc, Field field, bool @volatile, Dest dest, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Ldfieldtoken(APC pc, Field field, Dest dest, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Ldsflda(APC pc, Field field, Dest dest, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Stfld(APC pc, Field field, bool @volatile, Source obj, Source value, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            public Unit Stsfld(APC pc, Field field, bool @volatile, Source value, Unit data)
            {
                _parent.Touch(field);
                return Unit.Value;
            }

            #endregion

            #endregion

            #region IVisitSynthIL<Label,Method,Type,Source,Dest,Unit,Unit> Members

            #region no nothing

            public Unit Ldstack(APC pc, int offset, Dest dest, Source source, bool isOld, Unit data)
            {
                return Unit.Value;
            }

            public Unit BeginOld(APC pc, APC matchingEnd, Unit data)
            {
                return Unit.Value;
            }

            #endregion

            #region assume, assert -> do nothing

            public Unit Assume(APC pc, string tag, Source condition, object provenance, Unit data)
            {
                // TODO: do we need to visit the provenance ?
                return Unit.Value;
            }

            public Unit Assert(APC pc, string tag, Source condition, object provenance, Unit data)
            {
                // TODO: do we need to visit the provenance ?
                return Unit.Value;
            }

            #endregion

            #region Type

            public Unit Ldstacka(APC pc, int offset, Dest dest, Source source, Type origParamType, bool isOld, Unit data)
            {
                _parent.Touch(origParamType);
                return Unit.Value;
            }

            public Unit Ldresult(APC pc, Type type, Dest dest, Source source, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit EndOld(APC pc, APC matchingBegin, Type type, Dest dest, Source source, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            #endregion

            #region Method

            public Unit Entry(APC pc, Method method, Unit data)
            {
                _parent.Touch(method);
                return Unit.Value;
            }

            #endregion

            #endregion

            #region IVisitExprIL<Label,Type,Source,Dest,Unit,Unit> Members

            #region operators, null -> do nothing

            public Unit Binary(APC pc, BinaryOperator op, Dest dest, Source s1, Source s2, Unit data)
            {
                return Unit.Value;
            }

            public Unit Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Source source, Unit data)
            {
                return Unit.Value;
            }

            public Unit Ldnull(APC pc, Dest dest, Unit data)
            {
                return Unit.Value;
            }

            #endregion

            #region Type

            public Unit Isinst(APC pc, Type type, Dest dest, Source obj, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Ldconst(APC pc, object constant, Type type, Dest dest, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            public Unit Sizeof(APC pc, Type type, Dest dest, Unit data)
            {
                _parent.Touch(type);
                return Unit.Value;
            }

            #endregion

            #endregion
        }
        #endregion
    }
}
