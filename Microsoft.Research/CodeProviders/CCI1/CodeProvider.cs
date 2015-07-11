// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if CCINamespace
using Cci = Microsoft.Cci;
#else
using Cci = System.Compiler;
#endif

namespace System.Compiler.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.IO;

    using Microsoft.Research.DataStructures;
    using Microsoft.Research.CodeAnalysis;

    using UnaryExpression = Cci.UnaryExpression;
    using BinaryExpression = Cci.BinaryExpression;
    using System.Diagnostics.Contracts;
    using Microsoft.Contracts.Foxtrot;

    public abstract class CCIBaseProvider<PC>
    {
        public bool HasBody(Method method)
        {
            Contract.Requires(method != null);

            return method.Body != null && method.Body.Statements != null && !method.IsAbstract && !method.IsExtern;
        }

        public TypeNode DeclaringType(Method method)
        {
            Contract.Requires(method != null);

            return method.DeclaringType;
        }

        public bool IsInterface(TypeNode type)
        {
            return type is Interface;
        }

        public bool IsVirtual(Method method)
        {
            Contract.Requires(method != null);

            return method.IsVirtual;
        }

        public bool IsConstructor(Method method)
        {
            return method is InstanceInitializer;
        }

        public bool IsFaultHandler(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return info.HandlerType == NodeType.FaultHandler;
        }

        public bool IsFinallyHandler(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return info.HandlerType == NodeType.Finally;
        }

        public bool IsCatchHandler(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return info.HandlerType == NodeType.Catch;
        }

        public bool IsCatchAllHandler(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return IsCatchHandler(info) && (info.FilterType == null || SystemTypes.Exception.IsAssignableTo(info.FilterType));
        }

        public TypeNode CatchType(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return info.FilterType;
        }

        public bool IsFilterHandler(ExceptionHandler info)
        {
            Contract.Requires(info != null);

            return info.HandlerType == NodeType.Filter;
        }

        public bool IsVoidMethod(Method method)
        {
            Contract.Requires(method != null);

            return CCIMDDecoder.Value.IsVoid(method.ReturnType);
        }

        public string MethodName(Method method)
        {
            Contract.Requires(method != null);

            return method.FullName;
        }

        [Pure]
        protected abstract Node SourceNode(PC pc);

        public bool HasSourceContext(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.IsValid);
        }


        public string SourceDocument(PC pc)
        {
            var node = SourceNode(pc);
            Contract.Assume(node != null);
            if (node.SourceContext.IsValid)
            {
                return node.SourceContext.Document.Name;
            }
            return null;
        }

        public string SourceAssertionCondition(PC pc)
        {
            var sourceNode = SourceNode(pc);
            string sourceCondition = null;
            string userText = null;
            var mce = sourceNode as MethodContractElement;
            if (mce != null)
            {
                sourceCondition = (mce.SourceConditionText != null) ? mce.SourceConditionText.Value as string : null;
                Literal userLit = mce.UserMessage as Literal;
                userText = (userLit != null) ? userLit.Value as string : null;
            }
            else
            {
                var inv = sourceNode as Invariant;
                if (inv != null)
                {
                    sourceCondition = (inv.SourceConditionText != null) ? inv.SourceConditionText.Value as string : null;
                    userText = (inv.UserMessage != null) ? inv.UserMessage.Value as string : null;
                }
            }
            if (sourceCondition != null)
            {
                if (userText != null)
                {
                    return String.Format("{0} ({1})", sourceCondition, userText);
                }
                return sourceCondition;
            }
            return userText;
        }

        public int SourceStartLine(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.StartLine);
        }

        public int SourceStartColumn(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.StartColumn);
        }

        public int SourceEndLine(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.EndLine);
        }

        public int SourceEndColumn(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.EndColumn);
        }

        public int SourceStartIndex(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.StartPos);
        }

        public int SourceLength(PC pc)
        {
            return (SourceNode(pc).AssumeNotNull().SourceContext.EndPos - SourceNode(pc).AssumeNotNull().SourceContext.StartPos + 1);
        }

        public int ILOffset(PC pc)
        {
            Node node = SourceNode(pc);
            Statement stmt = node as Statement;
            if (stmt != null) return stmt.ILOffset;
            Expression expr = node as Expression;
            if (expr != null) return expr.ILOffset;
            Requires requires = node as Requires;
            if (requires != null) return requires.ILOffset;
            Ensures ensures = node as Ensures;
            if (ensures != null) return ensures.ILOffset;
            Invariant invariant = node as Invariant;
            if (invariant != null) return invariant.ILOffset;
            return 0;
        }

        private int FindILOffset(Expression node)
        {
            if (node.ILOffset != 0) return node.ILOffset;
            BlockExpression be = node as BlockExpression;
            if (be != null && be.Block != null)
            {
                return FindILOffset(be.Block);
            }
            return 0;
        }

        private int FindILOffset(Statement node)
        {
            if (node.ILOffset != 0) return node.ILOffset;
            ExpressionStatement es = node as ExpressionStatement;
            if (es != null && es.Expression != null)
            {
                FindILOffset(es.Expression);
            }
            Block b = node as Block;
            if (b != null)
            {
                return FindILOffset(b);
            }
            return 0;
        }

        private int FindILOffset(Block block)
        {
            if (block.ILOffset != 0) return block.ILOffset;
            if (block.Statements == null) return 0;
            for (int i = 0; i < block.Statements.Count; i++)
            {
                int result = FindILOffset(block.Statements[i]);
                if (result != 0) return result;
            }
            return 0;
        }
    }


    public class CCIMDDecoder : IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, TypeNode, AttributeNode, AssemblyNode>
    {
        public static readonly CCIMDDecoder Value = new CCIMDDecoder(); // Not thread-safe because of libPaths, resolvedPaths, and sharedContractClassLibrary

        #region Methods

#if false
    public bool TryGetImplementingMethod(TypeNode type, Method baseMethod, out Method implementing)
    {
      return TryGetImplementingMethod(type, baseMethod, out implementing);
    }
#endif

        public bool TryGetImplementingMethod(TypeNode type, Method baseMethod, out Method implementing)
        {
            if (type == null) { implementing = null; return false; }
            if (type is Class || type is Struct || type is ArrayType)
            {
                Method candidate = type.ExplicitImplementation(baseMethod);
                if (candidate != null)
                {
                    implementing = candidate;
                    return true;
                }
                // we search shallow only
                candidate = type.GetImplementingMethod(baseMethod, false);
                if (candidate != null)
                {
                    implementing = candidate;
                    return true;
                }
            }
            var reference = type as Reference;
            if (reference != null && reference.ElementType is Struct)
            {
                Method candidate = reference.ElementType.ExplicitImplementation(baseMethod);
                if (candidate != null)
                {
                    implementing = candidate;
                    return true;
                }
                // we search shallow only
                candidate = reference.ElementType.GetImplementingMethod(baseMethod, false);
                if (candidate != null)
                {
                    implementing = candidate;
                    return true;
                }
            }
            return TryGetImplementingMethod(type.BaseType, baseMethod, out implementing);
        }

        public TypeNode ReturnType(Method method)
        {
            return method.ReturnType;
        }

        public IIndexable<Parameter> Parameters(Method method)
        {
            return new ParameterListIndexer(method.Parameters);
        }

        public bool IsCompilerGenerated(Method method)
        {
            method = Unspecialized(method);
            return HasAttributeWithName(method.Attributes, "CompilerGeneratedAttribute") || HasAttributeWithName(method.Attributes, "GeneratedCodeAttribute");
        }

        /// <summary>
        /// In C# this means we have an CompilerGenerated attribute, in VB a DebuggerNonUserCode attribute
        /// </summary>
        public bool IsAutoPropertyMember(Method method)
        {
            if (IsCompilerGenerated(method)) return true;
            if (IsDebuggerNonUserCode(method)) return true;
            return false;
        }

        /// <summary>
        /// In C# this means we have an CompilerGenerated attribute, in VB a DebuggerNonUserCode attribute
        /// </summary>
        public bool IsAutoPropertySetter(Method setter, out Field backingfield)
        {
            if (!IsAutoPropertyMember(setter)) { backingfield = null; return false; }
            backingfield = Microsoft.Contracts.Foxtrot.HelperMethods.TryGetBackingField(setter);
            return backingfield != null;
        }

        public bool IsDebuggerNonUserCode(Method method)
        {
            method = Unspecialized(method);
            return HasAttributeWithName(method.Attributes, "DebuggerNonUserCodeAttribute");
        }

        public bool IsDebuggerNonUserCode(TypeNode type)
        {
            type = Unspecialized(type);
            return HasAttributeWithName(type.Attributes, "DebuggerNonUserCodeAttribute");
        }

        public bool IsCompilerGenerated(TypeNode type)
        {
            type = Unspecialized(type);
            return HasAttributeWithName(type.Attributes, "CompilerGeneratedAttribute") || HasAttributeWithName(type.Attributes, "GeneratedCodeAttribute");
        }

        public bool IsNativeCpp(TypeNode type)
        {
            type = Unspecialized(type);
            return HasAttributeWithName(type.Attributes, "NativeCppClassAttribute");
        }

        public static bool HasAttributeWithName(AttributeList attributes, string expected)
        {
            if (attributes == null) return false;
            foreach (var attr in attributes) // Just use name matching
            {
                if (attr == null) continue;
                if (attr.Type == null) continue;
                if (attr.Type.Name == null) continue;
                if (attr.Type.Name.Name == expected)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStatic(Method method) { return method.IsStatic; }

        public bool IsVirtual(Method method)
        {
            return method.IsVirtual;
        }

        public bool IsNewSlot(Method method)
        {
            return (method.Flags & MethodFlags.NewSlot) != 0;
        }

        public bool IsVoidMethod(Method method)
        {
            return TypeNode.StripModifiers(method.ReturnType) == SystemTypes.Void;
        }

        public bool IsOverride(Method method)
        {
            if (IsNewSlot(method))
                return false;
            return method.OverriddenMethod != null;
        }

        public bool IsSealed(Method method)
        {
            return method.IsFinal;
        }

        public IEnumerable<Method> OverriddenMethods(Method method)
        {
            #region Search the class hierarchy for first overriden method
            if (method.OverriddenMethod != null)
            {
                yield return method.OverriddenMethod;
            }
#if false
      if (method.OverridesBaseClassMember && method.DeclaringType != null && method.OverriddenMethod != null)
      {
        for (TypeNode super = method.DeclaringType.BaseType; super != null; super = super.BaseType)
        {
          Method baseMethod = super.GetImplementingMethod(method.OverriddenMethod, false);
          if (baseMethod != null)
          {
            yield return baseMethod;
            yield break;
          }
        }
      }
#endif
            #endregion
        }

        public IEnumerable<Method> ImplementedMethods(Method method)
        {
            #region Find explicitly implemented interface methods.
            if (method.ImplementedInterfaceMethods != null)
            {
                foreach (Method interfaceMethod in method.ImplementedInterfaceMethods)
                {
                    if (interfaceMethod != null)
                    {
                        yield return interfaceMethod;
                    }
                }
            }
            #endregion

            #region Find implicitly implemented interface methods.
            if (method.ShallowImplicitlyImplementedInterfaceMethods != null)
            {
                foreach (Method interfaceMethod in method.ShallowImplicitlyImplementedInterfaceMethods)
                {
                    yield return interfaceMethod;
                }
            }
            #endregion
        }

        public bool TryGetRootMethod(Method method, out Method rootMethod)
        {
            Method current = method;

            while (current.OverriddenMethod != null && Microsoft.Contracts.Foxtrot.HelperMethods.DoesInheritContracts(current))
            {
                current = current.OverriddenMethod;
            }
            if (current == method) { rootMethod = null; return false; } // no root base method
            rootMethod = current;
            return true;
        }

        public IEnumerable<Method> OverriddenAndImplementedMethods(Method method)
        {
            #region Merge
            // Simply merge the two IEnumerable
            var enum1 = OverriddenMethods(method);
            var enum2 = ImplementedMethods(method);
            foreach (var e1 in enum1)
                yield return e1;
            foreach (var e2 in enum2)
                yield return e2;
            #endregion
        }

        public bool IsImplicitImplementation(Method method)
        {
            return method.ImplicitlyImplementedInterfaceMethods != null && method.ImplicitlyImplementedInterfaceMethods.Count > 0;
        }

        public bool IsConstructor(Method method)
        {
            return method is InstanceInitializer;
        }

        public bool IsFinalizer(Method method)
        {
            return method.Name.Name == "Finalize";
        }

        /// <summary>
        /// Dispose methods have 0 or 1 bool args.
        /// </summary>
        public bool IsDispose(Method method)
        {
            return (method.Name.Name == "Dispose" || method.Name.Name == "System.IDisposable.Dispose") &&
              (method.Parameters == null ||
               method.Parameters.Count == 0 ||
               (method.Parameters.Count == 1 &&
                method.Parameters[0].Type == SystemTypes.Boolean));
        }

        public bool IsMain(Method method)
        {
            var entryPoint = Microsoft.Contracts.Foxtrot.HelperMethods.AssemblyOf(method).EntryPoint;

            if (entryPoint != null)
                return entryPoint.Equals(method);

            return false;
        }

        public string Name(Method m)
        {
            return m.Name.Name;
        }

        public string FullName(Method method)
        {
            var result = method.FullName;
            if (method.DeclaringMember == null && !result.EndsWith(")"))
            {
                result = result + "()"; // nullary methods
            }
            return result;
        }

        public string DocumentationId(Method method)
        {
            return method.DocumentationId.Name;
        }

        public string DocumentationId(TypeNode type)
        {
            return type.DocumentationId.Name;
        }

        public string DocumentationId(Field field)
        {
            return field.DocumentationId.Name;
        }

        public string DeclaringMemberCanonicalName(Method method)
        {
            string res;
            var prop = method.DeclaringMember as Property;
            var @event = method.DeclaringMember as Event;
            if (prop != null)
                res = prop.FullName;
            else if (@event != null)
                res = @event.FullName;
            else
                res = method.FullName;
            res = res.Replace("#ctor", ".ctor");
            int pPos = res.LastIndexOf('(');
            if (pPos >= 0)
                res = res.Substring(0, pPos);
            return res;
        }

        public int MethodToken(Method method)
        {
            return method.MethodToken;
        }

        public TypeNode DeclaringType(Method method)
        {
            return method.DeclaringType;
        }

        public AssemblyNode DeclaringAssembly(Method method)
        {
            return method.DeclaringType.DeclaringModule.ContainingAssembly;
        }

        public Parameter This(Method method)
        {
            return method.ThisParameter;
        }

        public bool IsPrivate(Method method)
        {
            return method.IsPrivate;
        }

        public bool IsVisibleOutsideAssembly(Method method)
        {
            return method.IsVisibleOutsideAssembly;
        }

        public bool IsVisibleOutsideAssembly(Property property)
        {
            return property.IsVisibleOutsideAssembly;
        }

        public bool IsVisibleOutsideAssembly(Event @event)
        {
            return @event.IsVisibleOutsideAssembly;
        }

        public bool IsVisibleOutsideAssembly(Field field)
        {
            return field.IsVisibleOutsideAssembly;
        }

        public bool IsVisibleOutsideAssembly(TypeNode type)
        {
            return type.IsVisibleOutsideAssembly;
        }

        public bool TryInitialValue(Field field, out object value)
        {
            if ((field.Flags & FieldFlags.HasDefault) != 0)
            {
                value = field.DefaultValue.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public bool IsAbstract(Method method)
        {
            return method.IsAbstract;
        }

        public bool IsExtern(Method method)
        {
            return method.IsExtern;
        }

        public bool IsInternal(Method method)
        {
            return method.IsAssembly || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
        }

        public bool HasBody(Method method)
        {
            return method.Body != null && method.Body.Statements != null && !method.IsAbstract && !method.IsExtern;
        }

        public Result AccessMethodBody<Data, Result>(Method method, IMethodCodeConsumer<Local, Parameter, Method, Field, TypeNode, Data, Result> consumer, Data data)
        {
            return consumer.Accept(CCIILProvider.Value, CCIILProvider.Value.Entry(method), method, data);
        }

        public bool IsProtected(Method method)
        {
            return method.IsFamily || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
        }

        public bool IsPublic(Method method)
        {
            return method.IsPublic;
        }


        public bool IsSpecialized(Method method)
        {
            return method.Template != null || method.DeclaringType.Template != null;
        }

        public bool IsSpecialized(Method method, ref IFunctionalMap<TypeNode, TypeNode> specialization)
        {
            bool result = false;
            if (method.Template != null && method.Template.TemplateParameters != null && method.TemplateArguments != null)
            {
                var formals = method.Template.TemplateParameters;
                var actuals = method.TemplateArguments;

                for (int i = 0; i < formals.Count; i++)
                {
                    result = true;
                    var formal = formals[i];
                    var actual = actuals[i];
                    if (formal != null && actual != null)
                    {
                        specialization = specialization.Add(formal, actual);
                    }
                }
            }
            result |= IsSpecialized(method.DeclaringType, ref specialization);
            return result;
        }

        public bool IsSpecialized(Method method, out Method genericMethod, out IIndexable<TypeNode> methodTypeArguments)
        {
            if (method.Template != null && method.TemplateArguments != null && method.TemplateArguments.Count > 0)
            {
                genericMethod = method.Template;
                methodTypeArguments = new TypeNodeListIndexer(method.TemplateArguments);
                return true;
            }
            genericMethod = null;
            methodTypeArguments = null;
            return false;
        }

        public bool IsGeneric(Method method, out IIndexable<TypeNode> typeParameters)
        {
            if (method.TemplateParameters != null && method.TemplateParameters.Count > 0)
            {
                typeParameters = new TypeNodeListIndexer(method.TemplateParameters);
                return true;
            }
            typeParameters = null;
            return false;
        }

        public bool IsSpecialized(TypeNode type, ref IFunctionalMap<TypeNode, TypeNode> specialization)
        {
            Contract.Requires(specialization != null);

            bool result = false;
            if (type == null) return result;

            if (type.Template != null && type.Template.TemplateParameters != null && type.TemplateArguments != null)
            {
                var formals = type.Template.TemplateParameters;
                var actuals = type.TemplateArguments;

                for (int i = 0; i < formals.Count; i++)
                {
                    result = true;
                    var formal = formals[i];
                    var actual = actuals[i];
                    if (formal != actual && formal != null && actual != null)
                    {
                        specialization = specialization.Add(formal, actual);
                    }
                }
            }
            result |= IsSpecialized(type.DeclaringType, ref specialization);
            return result;
        }

        public Method Unspecialized(Method method)
        {
            return InternalUnspecialized(method);
        }

        internal static Method InternalUnspecialized(Method method)
        {
            if (method == null) return null;
            if (method.Template != null) return InternalUnspecialized(method.Template);
            if (method.DeclaringType.Template != null)
            {
                var declType = method.DeclaringType.Template;
                return declType.GetExactMatchingMethod(method);
            }
            return method;
        }

        public Method Specialize(Method method, TypeNode[] methodTypeArguments)
        {
            return method.GetTemplateInstance(method.DeclaringType, methodTypeArguments);
        }

        public IEnumerable<AttributeNode> GetAttributes(Method method)
        {
            for (int i = 0; i < method.Attributes.Count; i++)
            {
                AttributeNode attr = method.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        public bool Equal(Method m1, Method m2)
        {
            return m1 == m2;
        }

        public bool IsAsVisibleAs(Method m1, Method m2)
        {
            return Microsoft.Contracts.Foxtrot.HelperMethods.IsReferenceAsVisibleAs(m1, m2);
        }


        public bool IsVisibleFrom(TypeNode t, TypeNode from)
        {
            return HelperMethods.IsVisibleFrom(t, from);
        }

        public bool IsVisibleFrom(Method m, TypeNode from)
        {
            return HelperMethods.IsVisibleFrom(m, from);
        }

        public bool IsVisibleFrom(Field f, TypeNode from)
        {
            return HelperMethods.IsVisibleFrom(f, from);
        }


        public IIndexable<Local> Locals(Method method)
        {
            return new LocalListIndexer(method.LocalList);
        }

        public bool IsAsyncMoveNext(Method method) { return method.IsAsync; }

        public int? MoveNextStartState(Method method) { return method.MoveNextStartState; }

        #endregion

        #region System Types (alphabetically)

        public TypeNode System_Array
        {
            get { return SystemTypes.Array; }
        }

        public TypeNode System_Boolean
        {
            get { return SystemTypes.Boolean; }
        }

        public TypeNode System_Char
        {
            get { return SystemTypes.Char; }
        }

        public TypeNode System_Double
        {
            get { return SystemTypes.Double; }
        }

        public TypeNode System_Decimal
        {
            get { return SystemTypes.Decimal; }
        }

        public TypeNode System_DynamicallyTypedReference
        {
            get { return SystemTypes.DynamicallyTypedReference; }
        }

        public bool TryGetSystemType(string fullname, out TypeNode type)
        {
            int lastDot = fullname.LastIndexOf('.');
            string @namespace = "";
            string name = fullname;
            if (lastDot > 0)
            {
                @namespace = fullname.Substring(0, lastDot);
                name = fullname.Substring(lastDot + 1);
            }
            TypeNode candidate = SystemTypes.SystemAssembly.GetType(Identifier.For(@namespace), Identifier.For(name));
            if (candidate != null)
            {
                type = candidate;
                return true;
            }
            type = default(TypeNode);
            return false;
        }

        public TypeNode System_Int8
        {
            get { return SystemTypes.Int8; }
        }

        public TypeNode System_Int16
        {
            get { return SystemTypes.Int16; }
        }

        public TypeNode System_Int32
        {
            get { return SystemTypes.Int32; }
        }

        public TypeNode System_Int64
        {
            get { return SystemTypes.Int64; }
        }

        public TypeNode System_IntPtr
        {
            get { return SystemTypes.IntPtr; }
        }

        public TypeNode System_Object
        {
            get { return SystemTypes.Object; }
        }

        public TypeNode System_RuntimeArgumentHandle
        {
            get { return SystemTypes.RuntimeArgumentHandle; }
        }

        public TypeNode System_RuntimeFieldHandle
        {
            get { return SystemTypes.RuntimeFieldHandle; }
        }

        public TypeNode System_RuntimeMethodHandle
        {
            get { return SystemTypes.RuntimeMethodHandle; }
        }

        public TypeNode System_RuntimeTypeHandle
        {
            get { return SystemTypes.RuntimeTypeHandle; }
        }

        public TypeNode System_Single
        {
            get { return SystemTypes.Single; }
        }

        public TypeNode System_String
        {
            get { return SystemTypes.String; }
        }

        public TypeNode System_Type
        {
            get { return SystemTypes.Type; }
        }

        public TypeNode System_UInt8
        {
            get { return SystemTypes.UInt8; }
        }

        public TypeNode System_UInt16
        {
            get { return SystemTypes.UInt16; }
        }

        public TypeNode System_UInt32
        {
            get { return SystemTypes.UInt32; }
        }

        public TypeNode System_UInt64
        {
            get { return SystemTypes.UInt64; }
        }

        public TypeNode System_UIntPtr
        {
            get { return SystemTypes.UIntPtr; }
        }

        public TypeNode System_Void
        {
            get { return SystemTypes.Void; }
        }

        #endregion System Types

        #region Types

        public bool IsInterface(TypeNode type)
        {
            return type is Interface;
        }

        public bool IsAbstract(TypeNode type)
        {
            return type.IsAbstract;
        }

        public bool IsPublic(TypeNode type)
        {
            return type.IsPublic || type.IsNestedPublic;
        }

        public bool IsProtected(TypeNode type)
        {
            return type.IsNestedFamily || type.IsNestedFamilyAndAssembly || type.IsFamily || type.IsFamilyAndAssembly || type.IsFamilyOrAssembly;
        }

        public bool IsPrivate(TypeNode type)
        {
            return type.IsPrivate/* || type.IsAssembly*/;
        }

        public bool IsInternal(TypeNode type)
        {
            return type.IsNestedAssembly || type.IsNestedFamilyAndAssembly || type.IsNestedInternal || type.IsFamilyAndAssembly || type.IsAssembly || type.IsFamilyOrAssembly;
        }

        public bool IsSealed(TypeNode type)
        {
            return type.IsSealed;
        }

        public bool IsDelegate(TypeNode type)
        {
            return type.IsDelegateType();
        }

        public bool IsStatic(TypeNode type)
        {
            return type.IsStatic;
        }

        public bool IsVoid(TypeNode type)
        {
            return TypeNode.StripModifiers(type) == SystemTypes.Void;
        }

        public string FullName(TypeNode type)
        {
            return type.FullName;
        }

        public bool IsUnmanagedPointer(TypeNode type)
        {
            return type.IsPointerType;
        }

        public bool IsManagedPointer(TypeNode type)
        {
            return type is Reference;
        }

        public TypeNode ElementType(TypeNode type)
        {
            TypeNode node = TypeNode.StripModifiers(type);
            Reference r = node as Reference;
            if (r != null) return r.ElementType;
            ArrayType a = node as ArrayType;
            if (a != null) return a.ElementType;
            Pointer p = node as Pointer;
            if (p != null) return p.ElementType;
            throw new NotImplementedException();
        }

        public bool IsPrimitive(TypeNode type)
        {
            return TypeNode.StripModifiers(type).IsPrimitive || type == SystemTypes.Decimal;
        }

        public bool IsStruct(TypeNode type)
        {
            bool result = TypeNode.StripModifiers(type).IsValueType;
            return result;
        }

        public bool IsArray(TypeNode type)
        {
            return TypeNode.StripModifiers(type) is ArrayType;
        }

        public int Rank(TypeNode type)
        {
            ArrayType at = (ArrayType)TypeNode.StripModifiers(type);
            return at.Rank;
        }

        public IEnumerable<Field> Fields(TypeNode type)
        {
            TypeNode node = TypeNode.StripModifiers(type);
#if false
      if (type.Template == null && type.TemplateParameters != null)
      {
        // Turn C.f into C<T>.f where T is C's own type argument list
        node = type.GetGenericTemplateInstance(type.DeclaringModule, type.ConsolidatedTemplateParameters);
      }
#endif
            for (int i = 0; i < node.Members.Count; i++)
            {
                Field f = node.Members[i] as Field;
                if (f == null) continue;
                yield return f;
            }
        }

        public IEnumerable<Property> Properties(TypeNode type)
        {
            TypeNode node = TypeNode.StripModifiers(type);
#if false
      if (type.Template == null && type.TemplateParameters != null)
      {
        // Turn C.f into C<T>.f where T is C's own type argument list
        node = type.GetGenericTemplateInstance(type.DeclaringModule, type.ConsolidatedTemplateParameters);
      }
#endif
            for (int i = 0; i < node.Members.Count; i++)
            {
                Property p = node.Members[i] as Property;
                if (p == null) continue;
                // don't return any model properties: if a client wants those, then they get them from
                // ModelMethods in the contract decoder.
                if (p.Getter != null && CCIContractDecoder.IsModel(p.Getter)) continue;
                yield return p;
            }
        }


        public IEnumerable<Event> Events(TypeNode type)
        {
            TypeNode node = TypeNode.StripModifiers(type);
#if false
      if (type.Template == null && type.TemplateParameters != null)
      {
        // Turn C.f into C<T>.f where T is C's own type argument list
        node = type.GetGenericTemplateInstance(type.DeclaringModule, type.ConsolidatedTemplateParameters);
      }
#endif
            for (int i = 0; i < node.Members.Count; i++)
            {
                Event e = node.Members[i] as Event;
                if (e == null) continue;
                yield return e;
            }
        }

        public bool IsNested(TypeNode type, out TypeNode parentType)
        {
            parentType = type.DeclaringType;
            return parentType != null;
        }

        public bool IsModified(TypeNode type, out TypeNode modified, out IIndexable<Pair<bool, TypeNode>> modifiers)
        {
            if (type is TypeModifier)
            {
                List<Pair<bool, TypeNode>> mods = new List<Pair<bool, TypeNode>>();
                TypeNode t = type;
                TypeModifier mod;
                while ((mod = t as OptionalModifier) != null)
                {
                    bool isOpt = (mod is OptionalModifier);
                    mods.Add(new Pair<bool, TypeNode>(isOpt, mod.Modifier));
                    t = mod.ModifiedType;
                }
                modified = t;
                modifiers = mods.AsIndexable();
                return true;
            }
            modified = null;
            modifiers = null;
            return false;
        }

        public bool IsSpecialized(TypeNode type, out IIndexable<TypeNode> typeArguments)
        {
            if (type.Template != null)
            {
                typeArguments = new TypeNodeListIndexer(type.TemplateArguments);
                return true;
            }
            typeArguments = null;
            return false;
        }

        public bool NormalizedIsSpecialized(TypeNode type, out IIndexable<TypeNode> typeArguments)
        {
            if (type.Template != null && type.ConsolidatedTemplateArguments != null && type.ConsolidatedTemplateArguments.Count > 0)
            {
                typeArguments = new TypeNodeListIndexer(type.ConsolidatedTemplateArguments);
                return true;
            }
            typeArguments = null;
            return false;
        }

        public bool IsGeneric(TypeNode type, out IIndexable<TypeNode> typeFormals, bool normalized)
        {
            if (normalized)
            {
                if (type.ConsolidatedTemplateParameters != null && type.ConsolidatedTemplateParameters.Count > 0)
                {
                    typeFormals = new TypeNodeListIndexer(type.ConsolidatedTemplateParameters);
                    return true;
                }
                typeFormals = null;
                return false;
            }
            // non-normalized
            if (type.TemplateParameters != null && type.TemplateParameters.Count > 0)
            {
                typeFormals = new TypeNodeListIndexer(type.TemplateParameters);
                return true;
            }
            typeFormals = null;
            return false;
        }

        public TypeNode Specialize(TypeNode type, TypeNode[] typeArguments)
        {
            return type.GetTemplateInstance(type, typeArguments);
        }

        public TypeNode Unspecialized(TypeNode type)
        {
            Contract.Ensures(Contract.Result<TypeNode>() != null);

            TypeNode template = type.Template;
            if (template == null) return type;
            return (template);
        }

        public Guid DeclaringModule(TypeNode type)
        {
            if (type.DeclaringModule != null) { return type.DeclaringModule.Mvid; }
            return DeclaringModule((type.DeclaringType));
        }

        public int TypeSize(TypeNode type)
        {
            int size = type.ClassSize;
            if (size > 0) return size;
            size = TypeSizeHelper(type);
            type.ClassSize = size;
            return size;
        }

        private int TypeSizeHelper(TypeNode type)
        {
            if (this.IsUnmanagedPointer(type)) return 4;
            if (this.IsManagedPointer(type)) return 4;
            TypeNode reference = type;
            if (reference == System_Boolean) return 1;
            if (reference == System_Char) return 2;
            if (reference == System_DynamicallyTypedReference) return 4;
            if (reference == System_Int8) return 1;
            if (reference == System_Int16) return 2;
            if (reference == System_Int32) return 4;
            if (reference == System_Int64) return 8;
            if (reference == System_IntPtr) return 4;
            if (reference == System_Object) return 4;
            if (reference == System_RuntimeArgumentHandle) return 4;
            if (reference == System_RuntimeFieldHandle) return 4;
            if (reference == System_RuntimeMethodHandle) return 4;
            if (reference == System_RuntimeTypeHandle) return 4;
            if (reference == System_String) return 4;
            if (reference == System_Type) return 4;
            if (reference == System_UInt8) return 1;
            if (reference == System_UInt16) return 2;
            if (reference == System_UInt32) return 4;
            if (reference == System_UInt64) return 8;
            if (reference == System_UIntPtr) return 4;
            if (reference == System_Single) return 4;
            if (reference == System_Double) return 8;

            if (reference.IsUnmanaged)
            {
                int total = ComputeSizeOfFields(reference);
                return total;
            }
            //Debug.Assert(false, "type unknown");
            return -1;
        }

        private int ComputeSizeOfFields(TypeNode type)
        {
            int total = 0;
            for (int i = 0; i < type.Members.Count; i++)
            {
                Field f = type.Members[i] as Field;
                if (f == null) continue;
                if (f.IsStatic) continue;
                if (f.IsLiteral) continue;
                int size = TypeSize((f.Type));
                if (size == 0) return -1; // no size
                total = AdjustForAlignment(total, size);
                total = total + size;
            }
            return total;
        }

        private int AdjustForAlignment(int total, int size)
        {
            if (size > 4)
            {
                // align to 8 byte
                int lowerbits = total & 0x7;
                if (lowerbits != 0)
                {
                    total = total + 0x8 - lowerbits;
                }
                return total;
            }
            if (size > 2)
            {
                // align to 4 byte
                int lowerbits = total & 0x3;
                if (lowerbits != 0)
                {
                    total = total + 0x4 - lowerbits;
                }
                return total;
            }
            if (size > 1)
            {
                // align to 2 byte
                int lowerbits = total & 0x1;
                if (lowerbits != 0)
                {
                    total = total + 0x2 - lowerbits;
                }
                return total;
            }
            return total;
        }

        public TypeNode ArrayType(TypeNode type, int rank)
        {
            return (type.GetArrayType(rank));
        }

        public TypeNode ManagedPointer(TypeNode type)
        {
            return (type.GetReferenceType());
        }

        public TypeNode UnmanagedPointer(TypeNode type)
        {
            return (type.GetPointerType());
        }

        public IEnumerable<Method> Methods(TypeNode type)
        {
            MemberList ml = type.Members;
            int len = ml != null ? ml.Count : 0;
            for (int i = 0; i < len; i++)
            {
                Method method = ml[i] as Method;
                if (method != null && !Microsoft.Contracts.Foxtrot.ContractNodes.IsAbbreviatorMethod(method))
                {
                    yield return method;
                }
            }
        }

        public IEnumerable<TypeNode> NestedTypes(TypeNode type)
        {
            MemberList ml = type.Members;
            int len = ml != null ? ml.Count : 0;
            for (int i = 0; i < len; i++)
            {
                TypeNode nested = ml[i] as TypeNode;
                if (nested != null) yield return (nested);
            }
        }

        public bool Equal(TypeNode type1, TypeNode type2)
        {
            return type1 == type2;
        }

        public IEnumerable<AttributeNode> GetAttributes(TypeNode type)
        {
            int count = (type.Attributes == null) ? 0 : type.Attributes.Count;

            for (int i = 0; i < count; i++)
            {
                yield return type.Attributes[i];
            }
        }

        public string DeclaringModuleName(TypeNode type)
        {
            if (type.DeclaringModule != null) { return type.DeclaringModule.Name; }
            return DeclaringModuleName((type.DeclaringType));
        }

        public string Name(TypeNode type)
        {
            if (type.Name != null)
            {
                var result = type.Name.Name;
                if (result != null && type.TemplateParameters != null && type.TemplateParameters.Count > 0)
                {
                    // strip `x from end
                    var lastquote = result.LastIndexOf('`');
                    if (lastquote >= 0)
                    {
                        result = result.Substring(0, lastquote);
                    }
                }
                return result;
            }
            return type.ToString();
        }

        public string Namespace(TypeNode type)
        {
            if (type.Namespace != null) { return type.Namespace.Name; }
            return null;
        }

        public bool IsClass(TypeNode type)
        {
            return type is Class;
        }

        public bool HasBaseClass(TypeNode type)
        {
            Class c = type as Class;
            if (c != null && c.BaseClass != null) return true;
            return false;
        }

        public TypeNode BaseClass(TypeNode type)
        {
            Class c = (Class)type;
            return (c.BaseClass);
        }

        public IEnumerable<TypeNode> Interfaces(TypeNode type)
        {
            if (type.Interfaces != null)
            {
                for (int i = 0; i < type.Interfaces.Count; i++)
                {
                    yield return type.Interfaces[i];
                }
            }
        }

        public IEnumerable<TypeNode> TypeParameterConstraints(TypeNode type)
        {
            ITypeParameter tp = type as ITypeParameter;
            if (tp != null)
            {
                Class cp = tp as Class;
                if (cp != null) { yield return cp.BaseClass; }
                if (type.Interfaces != null)
                {
                    for (int i = 0; i < type.Interfaces.Count; i++)
                    {
                        yield return type.Interfaces[i];
                    }
                }
            }
        }

        // Helper method for type constraints flags
        private bool CheckTypeFlag(TypeNode type, TypeParameterFlags fl)
        {
            if (IsFormalTypeParameter(type))
            {
                ITypeParameter tp = type as ITypeParameter;
                return (tp.TypeParameterFlags & fl) != 0;
            }
            else if (IsMethodFormalTypeParameter(type))
            {
                ITypeParameter tp = type as ITypeParameter;
                return (tp.TypeParameterFlags & fl) != 0;

                //MethodTypeParameter tp = type as MethodTypeParameter;
                //return (tp.TypeParameterFlags & fl) != 0;
            }
            else
                throw new ArgumentException("Type is not a formal type parameter");
        }

        public bool IsReferenceConstrained(TypeNode type)
        {
            if (IsReferenceType(type)) return true;
            if (!(type is ITypeParameter)) return false;
            return CheckTypeFlag(type, TypeParameterFlags.ReferenceTypeConstraint);
        }

        public bool IsValueConstrained(TypeNode type)
        {
            if (IsStruct(type)) return true;
            if (!(type is ITypeParameter)) return false;
            return CheckTypeFlag(type, TypeParameterFlags.ValueTypeConstraint);
        }

        public bool IsConstructorConstrained(TypeNode type)
        {
            return CheckTypeFlag(type, TypeParameterFlags.DefaultConstructorConstraint);
        }

        public bool IsFormalTypeParameter(TypeNode type)
        {
            return type is ITypeParameter && !(type is MethodTypeParameter || type is MethodClassParameter);
        }

        public bool IsMethodFormalTypeParameter(TypeNode type)
        {
            return type is MethodTypeParameter || type is MethodClassParameter;
        }

        public int NormalizedFormalTypeParameterIndex(TypeNode type)
        {
            ITypeParameter tp = type as ITypeParameter;
            if (tp == null) throw new ArgumentException("Type is not a type-bound type variable");
            int result = tp.ParameterListIndex;
            // CCI1 seems to return the normalized index already
            return result;
#if false
      TypeNode declaringType = tp.DeclaringType;
      if (declaringType == null)
      {
        declaringType = (TypeNode)tp.DeclaringMember;
      }
      Debug.Assert(declaringType != null);
      TypeNode parent = declaringType.DeclaringType;
      while (parent != null)
      {
        if (parent.TemplateParameters != null)
        {
          result += parent.TemplateParameters.Count;
        }
        parent = parent.DeclaringType;
      }
      return result;
#endif
        }

        public TypeNode FormalTypeParameterDefiningType(TypeNode type)
        {
            ITypeParameter tp = type as ITypeParameter;
            TypeNode dm = tp == null ? null : tp.DeclaringMember as TypeNode;
            if (dm == null) throw new ArgumentException("Type is not a type-bound type variable");
            return dm;
        }

        public IIndexable<TypeNode> NormalizedActualTypeArguments(TypeNode type)
        {
            var targs = type.ConsolidatedTemplateArguments;
            return new TypeNodeListIndexer(targs);
        }

        public IIndexable<TypeNode> ActualTypeArguments(Method method)
        {
            var targs = method.TemplateArguments;
            return new TypeNodeListIndexer(targs);
        }

        public int MethodFormalTypeParameterIndex(TypeNode type)
        {
            var mtp = type as ITypeParameter;
            if (mtp == null) { throw new ArgumentException("Type is not a method-bound formal type parameter"); }
            return mtp.ParameterListIndex;
        }

        public Method MethodFormalTypeDefiningMethod(TypeNode type)
        {
            var mtp = type as ITypeParameter;
            var dm = mtp == null ? null : mtp.DeclaringMember as Method;
            if (dm == null) { throw new ArgumentException("Type is not a method-bound formal type parameter"); }
            return dm;
        }

        public bool IsEnum(TypeNode type)
        {
            return type is EnumNode;
        }

        public bool HasFlagsAttribute(TypeNode type)
        {
            foreach (var attr in type.Attributes)
            {
                if (attr.Type == System.Compiler.SystemTypes.FlagsAttribute)
                    return true;
            }

            return false;
        }

        public TypeNode TypeEnum(TypeNode type)
        {
            if (IsEnum(type))
            {
                foreach (var f in Fields(type))
                {
                    if (Name(f) == "value__")
                        return FieldType(f);
                }
                throw new ArgumentException("Type is not a proper enum (no value__ field)");
            }
            else
                throw new ArgumentException("Type is not an enum");
        }

        public bool IsReferenceType(TypeNode adaptor)
        {
            TypeNode t = adaptor;
            if (t.IsValueType) return false;
            if (t.IsReferenceType) return true;
            if (t is ITypeParameter) return false;
            return true;
        }

        public bool IsNativePointerType(TypeNode adaptor)
        {
            return adaptor == SystemTypes.IntPtr || adaptor == SystemTypes.UIntPtr || (adaptor is Pointer);
        }

        public bool DerivesFrom(TypeNode sub, TypeNode super)
        {
            return sub.IsAssignableTo(super);
        }
        /// <summary>
        /// CCI1's IsAssignableTo is broken: it doesn't consider the type
        /// arguments anyway. E.g., if C&lt;W&gr; implements the interface
        /// J&lt;W&gr;, it still considers it to derive from J&lt;T&gr; for
        /// any T.
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="super"></param>
        /// <returns></returns>
        public bool DerivesFromIgnoringTypeArguments(TypeNode sub, TypeNode super)
        {
            while (sub.Template != null) { sub = sub.Template; }
            while (super.Template != null) { super = super.Template; }
            return sub.IsAssignableTo(super);
        }

        public int ConstructorsCount(TypeNode type)
        {
            if (!IsClass(type) && !IsEnum(type))
                return 0;

            int count = 0;
            foreach (var m in Methods(type))
            {
                if (IsConstructor(m) && !IsStatic(m))
                    ++count;
            }
            return count;
        }
        #endregion

        #region Fields

        public TypeNode FieldType(Field field)
        {
            return (field.Type);
        }

        public string FullName(Field field)
        {
            return field.FullName;
        }

        public bool IsStatic(Field field) { return field.IsStatic; }

        public bool IsReadonly(Field field) { return field.IsInitOnly; }

        public TypeNode DeclaringType(Field field)
        {
            return (field.DeclaringType);
        }

        public bool IsPrivate(Field field)
        {
            return field.IsPrivate;
        }

        public bool IsProtected(Field field)
        {
            return field.IsFamily || field.IsFamilyAndAssembly || field.IsFamilyOrAssembly;
        }

        public bool IsPublic(Field field)
        {
            return field.IsPublic;
        }

        public bool IsInternal(Field field)
        {
            return field.IsFamilyAndAssembly || field.IsAssembly || field.IsFamilyOrAssembly;
        }

        public bool IsVolatile(Field field)
        {
            return field.IsVolatile;
        }

        public bool IsNewSlot(Field field)
        {
            // TODO: IsNewSlot
            return false;
            //return field.IsNewSlot;
        }

        public string Name(Field field)
        {
            return EscapeKeywords(field.Name.Name);
        }

        public bool IsCompilerGenerated(Field field)
        {
            field = Unspecialized(field);
            Contract.Assume(field != null);
            return HasAttributeWithName(field.Attributes, "CompilerGeneratedAttribute");
        }

        public IEnumerable<AttributeNode> GetAttributes(Field field)
        {
            for (int i = 0; i < field.Attributes.Count; i++)
            {
                AttributeNode attr = field.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        /// <summary>
        /// Returns true if any context that can see method can also see field.
        /// </summary>
        public bool IsAsVisibleAs(Field field, Method method)
        {
            return Microsoft.Contracts.Foxtrot.HelperMethods.IsReferenceAsVisibleAs(field, method);
        }

        /// <summary>
        /// Returns true if any context that can see method can also see type.
        /// </summary>
        public bool IsAsVisibleAs(TypeNode type, Method method)
        {
            return Microsoft.Contracts.Foxtrot.HelperMethods.IsTypeAsVisibleAs(type, method);
        }

        public bool IsSpecialized(Field field)
        {
            if (field.DeclaringType.Template == null) return false;
            return true;
        }

        public Field Unspecialized(Field field)
        {
            var template = field.DeclaringType.Template;
            if (template == null) return field;
            while (template.Template != null) template = template.Template;
            MemberList specializedMembers = field.DeclaringType.Members;
            MemberList unspecializedMembers = template.Members;
            for (int i = 0, n = specializedMembers.Count; i < n; i++)
            {
                if (specializedMembers[i] != field) continue;
                var unspecializedField = unspecializedMembers[i] as Field;
                if (unspecializedField == null) { Contract.Assume(false, "Should find unspeced field here"); unspecializedField = field; }
                return unspecializedField;
            }
            Contract.Assume(false, "Couldn't find unspecialized field");
            return field;
        }

        public bool Equal(Field f1, Field f2)
        {
            return f1 == f2;
        }
        #endregion

        #region Properties
        public string Name(Property property)
        {
            return property.Name.Name;
        }

        public TypeNode PropertyType(Property property)
        {
            return property.Type;
        }

        public IEnumerable<AttributeNode> GetAttributes(Property property)
        {
            for (int i = 0; i < property.Attributes.Count; i++)
            {
                AttributeNode attr = property.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        public IEnumerable<AttributeNode> GetAttributes(Parameter parameter)
        {
            // F: for some reason it seems that CCI1 admits a null list of attributes for parameters, so we do the check here
            if (parameter.Attributes == null)
            {
                yield break;
            }

            for (int i = 0; i < parameter.Attributes.Count; i++)
            {
                var attr = parameter.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        public bool IsPropertyGetter(Method method)
        {
            var property = method.DeclaringMember as Property;
            return property != null && property.Getter == method;
        }

        public bool IsPropertySetter(Method method)
        {
            var property = method.DeclaringMember as Property;
            return property != null && property.Setter == method;
        }

        public Property GetPropertyFromAccessor(Method method)
        {
            var property = method.DeclaringMember as Property;
            if (property != null && (property.Getter == method || property.Setter == method)) return property;
            return null;
        }

        public bool HasGetter(Property property, out Method getter)
        {
            getter = property.Getter;
            return getter != null;
        }

        public bool HasSetter(Property property, out Method setter)
        {
            setter = property.Setter;
            return setter != null;
        }

        public bool IsStatic(Property prop) { return prop.IsStatic; }

        public TypeNode DeclaringType(Property prop)
        {
            Method get_or_set = prop.Getter != null ? prop.Getter : prop.Setter;
            if (get_or_set == null)
                throw new ArgumentException("Prop is not a valid property"); ;
            return DeclaringType(get_or_set);
        }

        public bool IsOverride(Property prop)
        {
            if (prop.Getter != null)
                return IsOverride(prop.Getter);
            else if (prop.Setter != null)
                return IsOverride(prop.Setter);
            throw new ArgumentException("prop is not a valid property");
        }

        public bool IsNewSlot(Property prop)
        {
            if (prop.Getter != null)
                return IsNewSlot(prop.Getter);
            else if (prop.Setter != null)
                return IsNewSlot(prop.Setter);
            throw new ArgumentException("prop is not a valid property");
        }

        public bool IsSealed(Property prop)
        {
            if (prop.Getter != null)
                return IsSealed(prop.Getter);
            else if (prop.Setter != null)
                return IsSealed(prop.Setter);
            throw new ArgumentException("prop is not a valid property");
        }

        public bool Equal(Property p1, Property p2)
        {
            return p1 == p2;
        }
        #endregion

        #region Parameters

        private bool IsKeyword(string name)
        {
            switch (name)
            {
                case "this":
                case "namespace":
                case "event":
                case "class":
                case "struct":
                case "delegate":
                case "enum":
                case "new":
                    return true;
                default:
                    return false;
            }
        }

        private string EscapeKeywords(string name)
        {
            if (IsKeyword(name)) { return "@" + name; }
            else return name;
        }

        public string Name(Parameter p)
        {
            if (p.Name == null || string.IsNullOrEmpty(p.Name.Name))
            {
                return "A_" + p.ParameterListIndex.ToString();
            }
            if (p is This) { return p.Name.Name; }

            return EscapeKeywords(p.Name.Name);
        }

        public TypeNode ParameterType(Parameter p)
        {
            return (p.Type);
        }

        public bool IsOut(Parameter p)
        {
            return p.IsOut;
        }

        public int ArgumentIndex(Parameter p)
        {
            return p.ArgumentListIndex;
        }

        public int ArgumentStackIndex(Parameter p)
        {
            int totalArgs = p.DeclaringMethod.Parameters.Count + (p.DeclaringMethod.IsStatic ? 0 : 1);
            return totalArgs - p.ArgumentListIndex - 1;
        }

        public Method DeclaringMethod(Parameter p)
        {
            return p.DeclaringMethod;
        }

        #endregion

        #region Locals

        public string Name(Local l)
        {
            return l.Name != null // F: Added this check as we had a case in Q where a local in a precondition is null. I think the problem is because the contract was malformed
              ? l.Name.Name
              : "<local_noname>" + System.Environment.TickCount;
        }

        public TypeNode LocalType(Local l)
        {
            return (l.Type);
        }
        public bool IsPinned(Local local)
        {
            return local.Pinned;
        }
        public int LocalIndex(Local l)
        {
            Contract.Requires(l != null);

            return l.Index;
        }
        #endregion

        #region Contracts and assembly resolution

        private class ContractAttacher : AssemblyResolver
        {
            #region Fields

            private readonly System.Collections.IDictionary _assemblyCache;
            private Microsoft.Contracts.Foxtrot.ContractNodes _contractNodes;
            private Microsoft.Contracts.Foxtrot.ContractNodes _lastUsedContractNodes;
            private readonly Action<System.CodeDom.Compiler.CompilerError> _errorHandler;
            private readonly string _contractClassAssembly;
            private bool _verbose = false; // no way to set it from the command line yet.
            #endregion

            public ContractAttacher(System.Collections.IDictionary assemblyCache,
                                    Action<System.CodeDom.Compiler.CompilerError> errorHandler,
                                    IEnumerable<string> libPaths,
                                    IEnumerable<string> resolvedPaths,
                                    string contractClassAssembly,
                                    bool trace
                                   )
              : base(resolvedPaths, libPaths, true, true, trace, (resolver, assembly) => ((ContractAttacher)resolver).AttachContracts(assembly))
            {
                _assemblyCache = assemblyCache;
                _errorHandler = errorHandler;
                _contractClassAssembly = contractClassAssembly;
            }

            internal Microsoft.Contracts.Foxtrot.ContractNodes ContractNodes { get { return _contractNodes; } }
            internal Microsoft.Contracts.Foxtrot.ContractNodes LastUsedContractNodes { get { return _lastUsedContractNodes; } }

            private void ExtractFoxtrotContracts(AssemblyNode assembly, AssemblyNode/*?*/ outOfBandAssembly)
            {
                Contract.Requires(assembly != null);

                if (_contractNodes == null)
                {
                    AssemblyNode contractAssembly = this.ProbeForAssembly(_contractClassAssembly, null, this.AllExt);
                    Microsoft.Contracts.Foxtrot.ContractNodes contractNodes = null;
                    if (contractAssembly != null)
                    {
                        _assemblyCache[contractAssembly.StrongName] = contractAssembly;
                        contractNodes = Microsoft.Contracts.Foxtrot.ContractNodes.GetContractNodes(contractAssembly, _errorHandler);
                    }
                    // if contractNodes is null then let the extractor worry about where to find the contracts
                    _contractNodes = contractNodes;
                }
                var wasNull = _contractNodes == null;

                Microsoft.Contracts.Foxtrot.ContractNodes lastUsedContractNodes;
                Microsoft.Contracts.Foxtrot.Extractor.ExtractContracts(assembly, outOfBandAssembly, _contractNodes, _contractNodes, null, out lastUsedContractNodes, _errorHandler, true);
                if (wasNull) // then Extractor found a different set of nodes, use that since we didn't have a better idea
                    _contractNodes = lastUsedContractNodes;

                // save last used nodes in case we need to run post extraction checker
                _lastUsedContractNodes = lastUsedContractNodes;
            }

            /// <summary>
            /// Attach contract assemblies for this assembly and it's immediate referenced assemblies
            /// </summary>
            public void AttachContracts(AssemblyNode assem)
            {
                string codebase = typeof(System.Compiler.Statement).Assembly.CodeBase;
                codebase = codebase.Replace("#", "%23");
                Uri codebaseUri = new Uri(codebase);

                // try to attach contracts to given assembly
                AttachContractAssembly(codebaseUri, assem);
            }

            /// <summary>
            /// Attach contract assembly to this assembly unless the assembly is a declarative assembly
            /// </summary>
            private void AttachContractAssembly(Uri codebaseUri, AssemblyNode assembly)
            {
                if (_verbose)
                {
                    Console.WriteLine("Loaded assembly '{0}' from '{1}'", assembly.Name, assembly.Location);
                }

                // attach assembly resolver
                // assembly.AssemblyReferenceResolution += this.ResolveAssemblyReference;

                // if post load is not set, add it for any references we may chase
                if (assembly.GetAfterAssemblyLoad() == null)
                {
                    assembly.AfterAssemblyLoad += (a) => { this.PostLoadHook(a); };
                }

                // skip contract reference assemblies, but not Microsoft.Contracts.dll (for purity annotations)
                AssemblyNode contracts = null;
                if (assembly.Name != "Microsoft.Contracts")
                {
                    if (assembly.Name.EndsWith(".Contracts")) return;

                    if (!IsContractDeclarativeAssembly(assembly))
                    {
                        string contractsName = assembly.Name + ".Contracts";
                        contracts = (AssemblyNode)_assemblyCache[contractsName];
                        if (contracts == null)
                        {
                            contracts = this.ProbeForAssembly(contractsName, assembly.Directory, this.AllExt);
                        }
                    }
                }
                // extract foxtrot contracts
                ExtractFoxtrotContracts(assembly, contracts);

                return;
            }

            private bool IsContractDeclarativeAssembly(AssemblyNode assembly)
            {
                return CCIMDDecoder.HasAttributeWithName(assembly.Attributes, "ContractDeclarativeAssemblyAttribute");
            }

#if false
      private Cci.AssemblyNode ResolveAssemblyReference(Cci.AssemblyReference aref, Cci.Module referencingModule)
      {
        return FindAssemblyWithLibPaths(aref.Name);
      }

      private Cci.AssemblyNode FindAssemblyWithLibPaths(string assemblyName)
      {
        Cci.AssemblyNode assembly = FindFrameworkAssembly(assemblyName);
        if (assembly != null) return assembly;
        if (resolvedPaths != null)
        {
          foreach (string candidate in resolvedPaths)
          {
            var candidateAssemblyName = Path.GetFileNameWithoutExtension(candidate);
            if (String.Compare(candidateAssemblyName, assemblyName, true) != 0) continue;

            if (File.Exists(candidate))
            {
              assembly = Cci.AssemblyNode.GetAssembly(candidate, this.assemblyCache, true, true, true, this.AttachContracts);
              if (assembly != null) return assembly;
            }
          }
        }
        // use directories in libPaths
        foreach (string path in libPaths)
        {
          string file = Path.Combine(path, assemblyName + ".dll");
          if (File.Exists(file))
          {
            assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
            if (assembly != null) return assembly;
          }
          file = Path.Combine(path, assemblyName + ".winmd");
          if (File.Exists(file))
          {
            assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
            if (assembly != null) return assembly;
          }
          file = Path.Combine(path, assemblyName + ".exe");
          if (File.Exists(file))
          {
            assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
            if (assembly != null) return assembly;
          }
          // try without extension
          file = Path.Combine(path, assemblyName);
          if (File.Exists(file))
          {
            assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
            if (assembly != null) return assembly;
          }
        }
        return null;
      }

      private AssemblyNode FindFrameworkAssembly(string assemblyName)
      {
        Cci.AssemblyNode assembly;
        string path = System.Compiler.TargetPlatform.PlatformAssembliesLocation;
        if (path == null) { path = Path.GetDirectoryName(typeof(object).Assembly.Location); }
        string file = Path.Combine(path, assemblyName + ".dll");
        if (File.Exists(file))
        {
          assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
          if (assembly != null) return assembly;
        }
        file = Path.Combine(path, assemblyName + ".winmd");
        if (File.Exists(file))
        {
          assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
          if (assembly != null) return assembly;
        }
        file = Path.Combine(path, assemblyName + ".exe");
        if (File.Exists(file))
        {
          assembly = Cci.AssemblyNode.GetAssembly(file, this.assemblyCache, true, true, true, this.AttachContracts);
          if (assembly != null) return assembly;
        }
        return null;
      }
#endif
        }

        private ContractAttacher _contractAttacher;
        #endregion

        #region Modules

        public bool NestedTypesProvided { get { return false; } }

        public string Name(AssemblyNode assem)
        {
            return assem.Name;
        }

        public Version Version(AssemblyNode assem)
        {
            return assem.Version;
        }

        public Guid AssemblyGuid(AssemblyNode assem)
        {
            return assem.Mvid;
        }
        public bool TryLoadAssembly(string fileName, System.Collections.IDictionary assemblyCache, Action<System.CodeDom.Compiler.CompilerError> errorHandler, out AssemblyNode assem, bool legacyContractMode, List<string> referencedAssemblies, bool extractContractText)
        {
            var attacher = _contractAttacher;
            if (attacher == null)
            {
                Console.WriteLine("SetTargetPlatform not called prior to TryLoadAssembly");
                throw new ExitRequestedException(-55555);
            }
            assem = AssemblyNode.GetAssembly(fileName, assemblyCache, true, true, true, attacher.PostLoadHook);

            if (assem == null) return false;
            if (assem.MetadataImportErrors != null && assem.MetadataImportErrors.Count != 0)
            {
                if (errorHandler != null)
                {
                    Exception e = (Exception)assem.MetadataImportErrors[0];
                    errorHandler(new System.CodeDom.Compiler.CompilerError(fileName, 0, 0, "", String.Format("Assembly load resulted in metadata import error '{0}'", e.Message)));
                }
                return false;
            }
            #region Check contract validity on this assembly
            if (!fileName.ToLower().EndsWith(".contracts.dll") && errorHandler != null && attacher.LastUsedContractNodes != null)
            {
                // saved last used contracts as touching other things may cause further extraction
                var usedContracts = attacher.LastUsedContractNodes;

                if (extractContractText)
                {
                    // extract doc first, then run checker
                    var gd = new Microsoft.Contracts.Foxtrot.GenerateDocumentationFromPDB(usedContracts);
                    gd.VisitForDoc(assem);
                }

                var checker = new Microsoft.Contracts.Foxtrot.PostExtractorChecker(usedContracts, errorHandler, false, false, legacyContractMode, false, 4);
                checker.Visit(assem);
            }
            #endregion
            return true;
        }

        public IEnumerable<TypeNode> GetTypes(AssemblyNode assem)
        {
            return EnumerateTypesFromAssembly(assem);
        }

        private IEnumerable<TypeNode> EnumerateTypesFromAssembly(AssemblyNode assem)
        {
            TypeNodeList tl = assem.Types;

            for (int i = 0; i < tl.Count; i++)
            {
                yield return (tl[i]);
            }
        }

        public IEnumerable<AssemblyNode> AssemblyReferences(AssemblyNode assembly)
        {
            if (assembly.AssemblyReferences != null)
            {
                for (int i = 0; i < assembly.AssemblyReferences.Count; i++)
                {
                    yield return assembly.AssemblyReferences[i].Assembly;
                }
            }
        }

        #endregion

        #region Attributes

        public TypeNode AttributeType(AttributeNode attribute)
        {
            return (attribute.Type);
        }

        public Method AttributeConstructor(AttributeNode attribute)
        {
            MemberBinding mb = (MemberBinding)attribute.Constructor;
            return (Method)mb.BoundMember;
        }

        public IIndexable<object> PositionalArguments(AttributeNode attribute)
        {
            Contract.Assume(attribute != null);

            Method ctor = AttributeConstructor(attribute);
            int positionalCount = ctor.Parameters.Count;
            object[] args = new object[positionalCount];
            for (int i = 0; i < positionalCount; i++)
            {
                Literal l = (Literal)attribute.GetPositionalArgument(i);
                args[i] = l.Value;
            }
            return new ArrayIndexable<object>(args);
        }

        public object NamedArgument(string name, AttributeNode attribute)
        {
            Literal l = (Literal)attribute.GetNamedArgument(Identifier.For(name));
            if (l == null) return null;
            return l.Value;
        }

        public IEnumerable<AttributeNode> GetAttributes(AssemblyNode assembly)
        {
            for (int i = 0; i < assembly.Attributes.Count; i++)
            {
                AttributeNode attr = assembly.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        #endregion

        #region Platform

        public bool IsPlatformInitialized { get; private set; }

        public void SetTargetPlatform(string framework, System.Collections.IDictionary assemblyCache, string stdlib, IEnumerable<string> resolved, IEnumerable<string> libPaths, Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool trace)
        {
            this.IsPlatformInitialized = true;

            _contractAttacher = new ContractAttacher(assemblyCache, errorHandler, resolvedPaths: resolved, libPaths: libPaths, contractClassAssembly: _sharedContractClassLibrary, trace: trace);

            stdlib = AssemblyResolver.GetTargetPlatform(stdlib, resolved, libPaths);

            // Trigger static initializer of SystemTypes
            System.Compiler.TypeNode dummy = System.Compiler.SystemTypes.Object;
            System.Compiler.TargetPlatform.Clear();
            System.Compiler.TargetPlatform.AssemblyReferenceFor = null;

            System.Compiler.SystemAssemblyLocation.SystemAssemblyCache = assemblyCache;

            if (stdlib != null)
            {
                string platformDir = Path.GetDirectoryName(stdlib);
                if (platformDir == "") platformDir = Environment.CurrentDirectory;
                if (!Directory.Exists(platformDir))
                {
                    Console.WriteLine("Directory '" + platformDir + "' doesn't exist.");
                    throw new ExitRequestedException(-1);
                }
                System.Compiler.SystemAssemblyLocation.Location = stdlib;

                System.Compiler.TargetPlatform.GetDebugInfo = true;
                switch (framework)
                {
                    case "v1.0":
                        System.Compiler.TargetPlatform.SetToV1(platformDir);
                        break;
                    case "v1.1":
                        System.Compiler.TargetPlatform.SetToV1_1(platformDir);
                        break;
                    case "v2.0":
                    case "v3.0":
                    case "v3.5":
                        System.Compiler.TargetPlatform.SetToV2(platformDir);
                        break;
                    case "v4.0":
                        System.Compiler.TargetPlatform.SetToV4(platformDir);
                        break;
                    case "v4.5":
                    case "v4.5.1":
                    case "v4.5.2":
                        System.Compiler.TargetPlatform.SetToV4_5(platformDir);
                        break;
                    default:
                        // Use our modified platform handling
                        System.Compiler.TargetPlatform.SetToPostV2(platformDir);
                        break;
                }
                // reset system to be mscorlib, not Runtime.dll
                System.Compiler.SystemAssemblyLocation.Location = stdlib;

                // also set the SystemDll path
                //System.Compiler.SystemDllAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(stdlib), "System.dll");
            }
            else
            {
                // no mscorlib found, just hope for the best
                switch (framework)
                {
                    case "v1.0":
                        System.Compiler.TargetPlatform.SetToV1();
                        break;
                    case "v1.1":
                        System.Compiler.TargetPlatform.SetToV1_1();
                        break;
                    case "v2.0":
                    case "v3.0":
                    case "v3.5":
                        System.Compiler.TargetPlatform.SetToV2();
                        break;
                    case "v4.0":
                        System.Compiler.TargetPlatform.SetToV4();
                        break;
                    case "v4.5":
                    case "v4.5.1":
                        System.Compiler.TargetPlatform.SetToV4_5();
                        break;
                    default:
                        // Use whatever CCI thinks is best
                        break;
                }
            }


            System.Compiler.SystemTypes.Initialize(false, true, _contractAttacher.PostLoadHook);

            // make sure we attach contracts to the system assembly and the system.dll assembly.
            // as they are already loaded, they will never trigger the post assembly load event.
            _contractAttacher.PostLoadHook(SystemTypes.SystemAssembly);
            _contractAttacher.PostLoadHook(SystemTypes.SystemDllAssembly);
        }

        private string _sharedContractClassLibrary = "Microsoft.Contracts";

        public string SharedContractClassAssembly
        {
            get { return _sharedContractClassLibrary; }
            set { _sharedContractClassLibrary = value; }
        }

        #endregion

        #region Events

        public string Name(Event @event)
        {
            return @event.Name.Name;
        }

        public IEnumerable<AttributeNode> GetAttributes(Event @event)
        {
            for (int i = 0; i < @event.Attributes.Count; i++)
            {
                AttributeNode attr = @event.Attributes[i];
                if (attr == null) continue;
                yield return attr;
            }
        }

        public bool HasAdder(Event @event, out Method adder)
        {
            adder = @event.HandlerAdder;
            return adder != null;
        }

        public bool HasRemover(Event @event, out Method remover)
        {
            remover = @event.HandlerRemover;
            return remover != null;
        }

        public bool IsStatic(Event e)
        {
            return e.IsStatic;
        }

        public bool IsOverride(Event e)
        {
            if (e.HandlerAdder != null)
                return IsOverride(e.HandlerAdder);
            else if (e.HandlerRemover != null)
                return IsOverride(e.HandlerRemover);
            throw new ArgumentException("event is not a valid event");
        }

        public bool IsNewSlot(Event e)
        {
            if (e.HandlerAdder != null)
                return IsNewSlot(e.HandlerAdder);
            else if (e.HandlerRemover != null)
                return IsNewSlot(e.HandlerRemover);
            throw new ArgumentException("event is not a valid event");
        }

        public bool IsSealed(Event e)
        {
            if (e.HandlerAdder != null)
                return IsSealed(e.HandlerAdder);
            else if (e.HandlerRemover != null)
                return IsSealed(e.HandlerRemover);
            throw new ArgumentException("event is not a valid event");
        }

        public TypeNode DeclaringType(Event e)
        {
            return e.DeclaringType;
        }

        public TypeNode HandlerType(Event e)
        {
            return e.HandlerType;
        }

        public bool Equal(Event e1, Event e2)
        {
            return e1 == e2;
        }

        public bool IsEventAdder(Method method, out Event @event)
        {
            @event = method.DeclaringMember as Event;
            if (@event != null && @event.HandlerAdder == method) return true;
            return false;
        }

        public bool IsEventRemover(Method method, out Event @event)
        {
            @event = method.DeclaringMember as Event;
            if (@event != null && @event.HandlerRemover == method) return true;
            return false;
        }

        #endregion
    }

    public class CCIContractDecoder : IDecodeContracts<Local, Parameter, Method, Field, TypeNode>
    {
        public static readonly CCIContractDecoder Value = new CCIContractDecoder(); // Thread-safe

        #region IDecodeContracts<Method,Field,TypeNode> Members

        public bool VerifyMethod(Method method, bool analyzeNonUserCode, bool namespaceSelected, bool typeSelected, bool memberSelected)
        {
            if (memberSelected) return true;
            ThreeValued tv = CheckIfVerify(method.Attributes);
            if (tv.IsDetermined) return tv.Truth;

            if (!analyzeNonUserCode && NonUserCode(method.Attributes)) return false;

            var declaringMember = method.DeclaringMember;
            if (declaringMember != null)
            {
                tv = CheckIfVerify(declaringMember.Attributes);
                if (tv.IsDetermined) return tv.Truth;

                if (!analyzeNonUserCode && NonUserCode(declaringMember.Attributes)) return false;
            }
            if (typeSelected) return true;
            var declaringType = method.DeclaringType;
            do
            {
                tv = CheckIfVerify(declaringType.Attributes);
                if (tv.IsDetermined) return tv.Truth;

                if (!analyzeNonUserCode && NonUserCode(declaringType.Attributes)) return false;
                if (declaringType.DeclaringType == null) break;
                declaringType = declaringType.DeclaringType;
            }
            while (true);

            if (namespaceSelected) return true;

            tv = CheckIfVerify(declaringType.DeclaringModule.Attributes);
            if (tv.IsFalse) return false;

            return true;
        }

        private ThreeValued CheckIfVerify(AttributeList attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];

                if (attr != null && attr.Type.Name.Name == "ContractVerificationAttribute")
                {
                    for (int j = 0; j < attr.Expressions.Count; j++)
                    {
                        Literal l = attr.Expressions[j] as Literal;
                        if (l == null) continue;
                        if (l.Value is bool)
                        {
                            bool value = (bool)l.Value;
                            return new ThreeValued(value);
                        }
                    }
                }
            }
            return new ThreeValued();
        }

        private bool NonUserCode(AttributeList attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];

                if (attr != null)
                {
                    switch (attr.Type.Name.Name)
                    {
                        case "DebuggerNonUserCodeAttribute":
                        case "CompilerGeneratedAttribute":
                        case "GeneratedCodeAttribute":
                            return true;
                    }
                }
            }
            return false;
        }

        public bool HasOptionForClousot(Method method, string optionName, out string optionValue)
        {
            optionValue = null;

            if (CheckIfClousotOption(method.Attributes, optionName, out optionValue))
            {
                return true;
            }

            var declaringMember = method.DeclaringMember;
            if (declaringMember != null)
            {
                if (CheckIfClousotOption(declaringMember.Attributes, optionName, out optionValue))
                {
                    return true;
                }
            }
            var declaringType = method.DeclaringType;
            do
            {
                if (CheckIfClousotOption(declaringType.Attributes, optionName, out optionValue))
                {
                    return true;
                }

                if (declaringType.DeclaringType == null)
                {
                    break;
                }
                declaringType = declaringType.DeclaringType;
            }
            while (true);

            if (CheckIfClousotOption(declaringType.DeclaringModule.Attributes, optionName, out optionValue))
            {
                return true;
            }

            // If no explicit attibute was found, then we search for an inherited attribute

            var inheritedOptionName = "inherit " + optionName;

            if (method.ImplementedInterfaceMethods != null)
            {
                foreach (var interfaceMemberImplementation in method.ImplementedInterfaceMethods)
                {
                    if (CheckIfClousotOption(interfaceMemberImplementation.Attributes, inheritedOptionName, out optionValue))
                    {
                        return true;
                    }
                }
            }

            if (method.DeclaringType.Interfaces != null)
            {
                foreach (var interfaceImplementation in method.DeclaringType.Interfaces)
                {
                    if (CheckIfClousotOption(interfaceImplementation.Attributes, inheritedOptionName, out optionValue))
                    {
                        return true;
                    }
                }
            }

            for (var baseType = method.DeclaringType.BaseType; baseType != null; baseType = baseType.BaseType)
            {
                if (CheckIfClousotOption(baseType.Attributes, inheritedOptionName, out optionValue))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfClousotOption(AttributeList attributes, string what, out string value)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);

            if (attributes != null)
            {
                for (int i = 0; i < attributes.Count; i++)
                {
                    var attr = attributes[i];

                    if (attr != null && attr.Type.Name.Name == "ContractOptionAttribute")
                    {
                        if (attr.Expressions.Count >= 3)
                        {
                            var p1 = attr.Expressions[0] as Literal;
                            var p2 = attr.Expressions[1] as Literal;
                            var p3 = attr.Expressions[2] as Literal;

                            if (p1 != null && p2 != null && p3 != null)
                            {
                                var toolName = p1.Value as string;
                                var optionName = p2.Value as string;
                                var optionValue = p3.Value as string;

                                if (toolName != null && optionName != null && optionValue != null)
                                {
                                    toolName = toolName.ToLower();
                                    if (toolName.Contains("cccheck") || toolName.Contains("clousot"))
                                    {
                                        if (optionName.ToLower() == what.ToLower())
                                        {
                                            value = optionValue;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            value = null;
            return false;
        }

        private Method GetMethodWithContractFor(Method method)
        {
            Contract.Ensures(Contract.Result<Method>() != null);

            var declType = method.DeclaringType;
            if (!declType.IsAbstract) return method; // only abstract types and interfaces can have OOB

            var al = declType.Attributes;
            //      if (al != null)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    var attr = al[i];
                    if (attr == null) continue;
                    if (attr.Type.Name.Name != "ContractClassAttribute") continue;

                    Literal lit = attr.Expressions[0] as Literal;
                    if (lit == null) return method; // not found
                    TypeNode t = lit.Value as TypeNode;
                    if (t == null) return method; // not found
                    if (t.Template != null) { t = t.Template; }
                    return FindMatchingMethod(t, method);
                }
            }
            return method; // not found
        }

        /// <summary>
        /// Find the method in t that matches the signature of m
        /// </summary>
        /// <returns>returns m if we can't find m' in t matching m</returns>
        private Method FindMatchingMethod(TypeNode t, Method m)
        {
            Contract.Requires(m != null);
            Contract.Ensures(Contract.Result<Method>() != null);

            var members = t.Members;
            for (int i = 0; i < members.Count; i++)
            {
                var method = members[i] as Method;
                if (method == null) continue;

                if (m.DeclaringType is Interface)
                {
                    var explicitInterfaces = method.ImplementedInterfaceMethods;
                    if (explicitInterfaces != null)
                    {
                        for (int j = 0; j < explicitInterfaces.Count; j++)
                        {
                            Method ifm = CCIMDDecoder.InternalUnspecialized(explicitInterfaces[j]);
                            if (ifm == m) return method;
                        }
                    }
                    var implicitInterfaces = method.ImplicitlyImplementedInterfaceMethods;
                    if (implicitInterfaces != null)
                    {
                        for (int j = 0; j < implicitInterfaces.Count; j++)
                        {
                            Method ifm = CCIMDDecoder.InternalUnspecialized(implicitInterfaces[j]);
                            if (ifm == m) return method;
                        }
                    }
                }
                else
                {
                    var unspecced = CCIMDDecoder.InternalUnspecialized(method.OverriddenMethod);
                    if (unspecced != null && unspecced == m) return method;
                }
            }
            return m;
        }

        public bool HasRequires(Method method)
        {
            method = GetMethodWithContractFor(method);
            if (method.Contract == null) return false;
            // WARNING:
            // because we perform Old manifestation in pre-state and Old can refer to contract locals,
            // we have to insert any local initializations from the contract local block
            return HasContractInitializer(method.Contract) || method.Contract.RequiresCount > 0;
        }

        private bool HasContractInitializer(MethodContract methodContract)
        {
            if (methodContract.ContractInitializer == null) return false;
            if (methodContract.ContractInitializer.Statements == null) return false;
            if (methodContract.ContractInitializer.Statements.Count == 0) return false;
            return true;
        }

        public bool HasEnsures(Method method)
        {
            method = GetMethodWithContractFor(method);
            if (method.Contract == null) return false;
            return method.Contract.EnsuresCount > 0;
        }

        public bool HasModelEnsures(Method method)
        {
            method = GetMethodWithContractFor(method);
            if (method.Contract == null) return false;
            return method.Contract.ModelEnsuresCount > 0;
        }

        public bool HasInvariant(Method method)
        {
            method = GetMethodWithContractFor(method);
            if (!method.IsStatic || method is InstanceInitializer)
            {
                return HasInvariant((method.DeclaringType));
            }
            return false;
        }

        public bool HasInvariant(TypeNode type)
        {
            return type.Contract != null && type.Contract.Invariants != null && type.Contract.Invariants.Count > 0;
        }

        private static bool IsImmutable(TypeNode type)
        {
            if (type.IsPrimitive) return true;
            if (type == SystemTypes.String) return true;
            return false;
        }

        public bool IsPure(Method method)
        {
            method = CCIMDDecoder.InternalUnspecialized(method);
            if (method == null)
            {
                return false;
            }
            var possiblyProxyMethod = GetMethodWithContractFor(method);
            if (possiblyProxyMethod != method && possiblyProxyMethod.Contract != null && possiblyProxyMethod.Contract.IsPure)
            {
                return true;
            }

            if (method.Contract != null && method.Contract.IsPure) return true;
            Property prop = method.DeclaringMember as Property;
            // getters are assumed to be heap independent
            if (prop != null)
            {
                if (prop.Getter == method) return true;
            }
            // types can be pure, meaning all methods are pure (except constructors)
            if (!(method is InstanceInitializer))
            {
                var declaringType = method.DeclaringType;
                if (declaringType != null)
                {
                    if (IsPure(declaringType)) return true;
                }
            }
            // all operators are considered pure
            if (method.IsStatic)
            {
                if (method.Name.Name.StartsWith("op_"))
                    return true;
            }

            foreach (var baseMethod in CCIMDDecoder.Value.OverriddenAndImplementedMethods(method))
            {
                if (IsPure(baseMethod)) return true;
            }
            return false;
        }

        public bool IsPure(Property prop)
        {
            Contract.Requires(prop != null);

            return IsPure((Member)prop);
        }

        public bool IsPure(Member member)
        {
            Contract.Requires(member != null);

            foreach (var attr in member.Attributes) // Just use name matching
                if (attr.Type.Name.Name == "PureAttribute")
                    return true;
            return false;
        }

        public bool IsMutableHeapIndependent(Method method)
        {
            return IsMutableHeapIndependentInternal(method) == HelperMethods.ThreeValued.True;
        }

        private HelperMethods.ThreeValued IsMutableHeapIndependentInternal(Method method)
        {
            method = CCIMDDecoder.InternalUnspecialized(method);
            Contract.Assume(method != null, "Why is this the case?");
            foreach (var attr in method.Attributes) // Just use name matching
            {
                var v = HelperMethods.IsMutableHeapIndependent(attr);
                switch (v)
                {
                    case HelperMethods.ThreeValued.False:
                    case HelperMethods.ThreeValued.True:
                        return v;
                    default:
                        break;
                }
            }
            return IsMutableHeapIndependent(method.DeclaringType);
        }

        private HelperMethods.ThreeValued IsMutableHeapIndependent(TypeNode type)
        {
            Contract.Requires(type != null);

            type = (TypeNode)HelperMethods.Unspecialize(type);
            foreach (var attr in type.Attributes) // Just use name matching
            {
                var v = HelperMethods.IsMutableHeapIndependent(attr);
                switch (v)
                {
                    case HelperMethods.ThreeValued.False:
                    case HelperMethods.ThreeValued.True:
                        return v;
                    default:
                        break;
                }
            }
            var parent = type.DeclaringType;
            if (parent != null) return IsMutableHeapIndependent(parent);
            return IsMutableHeapIndependent(type.DeclaringModule);
        }

        private HelperMethods.ThreeValued IsMutableHeapIndependent(Module module)
        {
            foreach (var attr in module.Attributes) // Just use name matching
            {
                var v = HelperMethods.IsMutableHeapIndependent(attr);
                switch (v)
                {
                    case HelperMethods.ThreeValued.False:
                    case HelperMethods.ThreeValued.True:
                        return v;
                    default:
                        break;
                }
            }
            var assembly = module.ContainingAssembly;
            if (assembly != null && assembly != module)
            {
                IsMutableHeapIndependent(assembly);
            }
            return HelperMethods.ThreeValued.Maybe;
        }

        private HelperMethods.ThreeValued IsMutableHeapIndependent(AssemblyNode assembly)
        {
            foreach (var attr in assembly.ModuleAttributes) // Just use name matching
            {
                var v = HelperMethods.IsMutableHeapIndependent(attr);
                switch (v)
                {
                    case HelperMethods.ThreeValued.False:
                    case HelperMethods.ThreeValued.True:
                        return v;
                    default:
                        break;
                }
            }
            return HelperMethods.ThreeValued.Maybe;
        }

        public bool IsFreshResult(Method method)
        {
            method = CCIMDDecoder.InternalUnspecialized(method);
            if (CCIMDDecoder.Value.IsPropertyGetter(method)) return false;
            var possiblyProxyMethod = GetMethodWithContractFor(method);
            if (possiblyProxyMethod != method && CCIMDDecoder.HasAttributeWithName(possiblyProxyMethod.ReturnAttributes, "FreshAttribute"))
            {
                return true;
            }

            if (CCIMDDecoder.HasAttributeWithName(method.ReturnAttributes, "FreshAttribute")) return true;
            foreach (var baseMethod in CCIMDDecoder.Value.OverriddenAndImplementedMethods(method))
            {
                if (IsFreshResult(baseMethod)) return true;
            }
            return false;
        }

        bool IDecodeContracts<Local, Parameter, Method, Field, TypeNode>.IsModel(Method method)
        {
            return CCIContractDecoder.IsModel(method);
        }
        public static bool IsModel(Method method)
        {
            method = CCIMDDecoder.InternalUnspecialized(method);
            if (method == null)
            {
                return false;
            }
            var result = CCIMDDecoder.HasAttributeWithName(method.Attributes, "ContractModelAttribute");
            if (result) return true;
            var prop = method.DeclaringMember as Property;
            if (prop != null && CCIMDDecoder.HasAttributeWithName(prop.Attributes, "ContractModelAttribute")) return true;
            return false;
        }

        public static bool IsModel(Field field)
        {
            Contract.Requires(field != null);

            return CCIMDDecoder.HasAttributeWithName(field.Attributes, "ContractModelAttribute");
        }

        public IEnumerable<Field> ModelFields(TypeNode type)
        {
            var fs = new List<Field>();
            if (type == null) return fs; ;
            TypeNode node = TypeNode.StripModifiers(type);
            for (int i = 0; i < node.Members.Count; i++)
            {
                var f = node.Members[i] as Field;
                if (f == null || !CCIContractDecoder.IsModel(f)) continue;
                fs.Add(f);
            }
            return fs;
        }

        public IEnumerable<Method> ModelMethods(TypeNode type)
        {
            if (type == null) yield break;
            TypeNode node = TypeNode.StripModifiers(type);
            for (int i = 0; i < node.Members.Count; i++)
            {
                Property p = node.Members[i] as Property;
                if (p == null || p.Getter == null || !CCIContractDecoder.IsModel(p.Getter)) continue;
                yield return p.Getter;
            }
            yield break;
        }


        #endregion

        #region IDecodeContracts<Local,Parameter,Method,Field,TypeNode> Members


        public Result AccessRequires<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, TypeNode, Data, Result> consumer, Data data)
        {
            method = GetMethodWithContractFor(method);
            return consumer.Accept(CCIILProvider.Value, new CCIILProvider.PC(method.Contract, 0), data);
        }

        public Result AccessEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, TypeNode, Data, Result> consumer, Data data)
        {
            method = GetMethodWithContractFor(method);
            return consumer.Accept(CCIILProvider.Value, new CCIILProvider.PC(method.Contract, method.Contract.RequiresCount + 2), data);
        }

        public Result AccessModelEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, TypeNode, Data, Result> consumer, Data data)
        {
            method = GetMethodWithContractFor(method);
            return consumer.Accept(CCIILProvider.Value, new CCIILProvider.PC(method.Contract, method.Contract.RequiresCount + method.Contract.EnsuresCount + 5), data);
        }

        public Result AccessInvariant<Data, Result>(TypeNode type, ICodeConsumer<Local, Parameter, Method, Field, TypeNode, Data, Result> consumer, Data data)
        {
            return consumer.Accept(CCIILProvider.Value, new CCIILProvider.PC(type.Contract, 0), data);
        }

        public bool CanInheritContracts(Method method)
        {
            return Microsoft.Contracts.Foxtrot.HelperMethods.DoesInheritContracts(method);
        }

        public bool CanInheritContracts(TypeNode type)
        {
            return Microsoft.Contracts.Foxtrot.HelperMethods.DoesInheritContracts(type);
        }

        #endregion
    }

#if false
  public struct TypeNode : IEquatable<TypeNode> {
    public readonly TypeNode Reference;
    private TypeNode(TypeNode node) {
      this = node;
    }
    public static TypeNode AdaptorOf(TypeNode node) {
      return new TypeNode(node);
    }
    public bool Equals(TypeNode other) {
      return (this == other);
    }
    public override string ToString()
    {
      return Reference.ToString();
    }
  }
#endif
    internal struct ParameterListIndexer : IIndexable<Parameter>
    {
        private readonly ParameterList/*?*/ _pl;

        public ParameterListIndexer(ParameterList/*?*/ pl)
        {
            _pl = pl;
        }

        int IIndexable<Parameter>.Count { get { return (_pl != null) ? _pl.Count : 0; } }

        Parameter IIndexable<Parameter>.this[int index] { get { return _pl[index]; } }
    }
    internal struct LocalListIndexer : IIndexable<Local>
    {
        private readonly LocalList/*?*/ _ll;

        public LocalListIndexer(LocalList/*?*/ ll)
        {
            _ll = ll;
        }

        int IIndexable<Local>.Count { get { return (_ll != null) ? _ll.Count : 0; } }

        Local IIndexable<Local>.this[int index] { get { return _ll[index]; } }
    }

    internal struct TypeNodeListIndexer : IIndexable<TypeNode>
    {
        private readonly TypeNodeList/*?*/ _tl;

        public TypeNodeListIndexer(TypeNodeList/*?*/ tl)
        {
            _tl = tl;
        }

        int IIndexable<TypeNode>.Count { get { return (_tl != null) ? _tl.Count : 0; } }

        TypeNode IIndexable<TypeNode>.this[int index] { get { return _tl[index]; } }
    }


    /// <summary>
    /// Provides a stack machine level view of CCI statements, splitting up each push into a
    /// separate code point.
    /// </summary>
    public class CCIILProvider : CCIBaseProvider<CCIILProvider.PC>,
                                 IMethodCodeProvider<CCIILProvider.PC, Local, Parameter, Method, Field, TypeNode, ExceptionHandler>
    //                               IDecodeMSIL<CCIILProvider.PC, Local, Parameter, Method, Field, TypeNode, Unit, Unit, Unit>
    {
        public static readonly CCIILProvider Value = new CCIILProvider(); // Not thread-safe because mdDecoder is not


        #region Private details
        private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, TypeNode, AttributeNode, AssemblyNode> _mdDecoder;
        private readonly IDecodeContracts<Local, Parameter, Method, Field, TypeNode> _contractDecoder;

        private CCIILProvider(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, TypeNode, AttributeNode, AssemblyNode> mdDecoder,
                              IDecodeContracts<Local, Parameter, Method, Field, TypeNode> contractDecoder)
        {
            _mdDecoder = mdDecoder;
            _contractDecoder = contractDecoder;
            //      this.printer = PrinterFactory.Create/*<PC, Local, Parameter, Method, Field, TypeNode, Unit, Unit>*/(this, mdDecoder, null, null);
        }

        public struct PC : IEquatable<PC>
        {
            public readonly Node Node;
            public readonly int Index;

            public PC(Node node)
              : this(node, 0)
            {
            }

            public PC(Node node, int index)
            {
                this.Node = node;
                this.Index = index;
            }

            public bool Equals(PC pc)
            {
                return this.Node == pc.Node && this.Index == pc.Index;
            }

            public override int GetHashCode()
            {
                if (Node != null) return Node.UniqueKey;
                return Index;
            }
        }

        private Result DecodeLdtoken<Data, Result>(PC pc, IVisitMSIL<PC, Local, Parameter, Method, Field, TypeNode, Unit, Unit, Data, Result> visit, Expression expression, Data data)
        {
            Literal lit = expression as Literal;
            Member member = null;
            if (lit != null) member = (Member)lit.Value;
            else
            {
                member = (Member)((MemberBinding)expression).BoundMember;
            }
            TypeNode type = member as TypeNode;
            if (type != null)
            {
                return visit.Ldtypetoken(pc, (type), Unit.Value, data);
            }
            Field field = member as Field;
            if (field != null)
            {
                return visit.Ldfieldtoken(pc, field, Unit.Value, data);
            }
            return visit.Ldmethodtoken(pc, (Method)member, Unit.Value, data);
        }

        private void GetCalliFunctionSignature(MethodCall calli, out TypeNode returnType, out IIndexable<TypeNode> argTypes, out bool isInstance)
        {
            MemberBinding mb = calli.Callee as MemberBinding;
            Contract.Assume(mb != null);
            FunctionPointer fp = mb.BoundMember as FunctionPointer;
            Contract.Assume(fp != null);
            returnType = (fp.ReturnType);
            argTypes = new TypeNodeListIndexer(fp.ParameterTypes);
            isInstance = !fp.IsStatic;
        }

        private static IEnumerable<PC> SwitchTargets(BlockList targets)
        {
            if (targets != null)
            {
                foreach (Block block in targets)
                {
                    yield return new PC(block);
                }
            }
        }
        private static Method GetMethodFrom(Expression mbexp)
        {
            Contract.Ensures(Contract.Result<Method>() != null);

            MemberBinding mb = (MemberBinding)mbexp;
            return (Method)mb.BoundMember;
        }
        private static Field GetFieldFrom(Expression mbexp)
        {
            MemberBinding mb = (MemberBinding)mbexp;
            return (Field)mb.BoundMember;
        }
        private static TypeNode GetTypeFrom(Expression expr)
        {
            Literal lit = expr as Literal;
            if (lit != null) return ((TypeNode)lit.Value);
            return ((TypeNode)((MemberBinding)expr).BoundMember);
        }
        private static ArrayIndexable<TypeNode> GetVarargs(MethodCall call, Method method)
        {
            Contract.Assume(call.Operands != null); // It should be a property of MethodCall - For the moment just making the assumption explicit

            int pCount = method.Parameters.Count;
            int aCount = call.Operands.Count;
            if (aCount <= pCount) return new ArrayIndexable<TypeNode>(null);
            // extra args are var args
            TypeNode[] varargs = new TypeNode[aCount - pCount];
            for (int i = pCount; i < aCount; i++)
            {
                varargs[i - pCount] = (call.Operands[i - pCount].Type);
            }
            return new ArrayIndexable<TypeNode>(varargs);
        }

        #endregion

        #region ICodeProvider<Statement,PC,ExceptionHandler> Members

        private PC SingletonPC(Block target)
        {
            return new PC(target);
        }

        private IEnumerable<Pair<object, PC>> SwitchPatternsTargets(BlockList targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                Block b = targets[i];
                yield return new Pair<object, PC>(i, new PC(b));
            }
        }

        private static bool IsAtomicNested(Node nestedStart)
        {
            if (nestedStart == null) return false;
            switch (nestedStart.NodeType)
            {
                case NodeType.This:
                case NodeType.Parameter:
                case NodeType.Local:
                case NodeType.Literal:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// To avoid redundantly encoding the logic for decoding the next and nested ops,
        /// we return the next flat aggregate label as well as the possible nested aggregate op at the
        /// current point.
        ///
        /// There are a number of possible outcomes
        /// 1) the current program point refers to a sub-expression
        ///   1a) and there is a syntactic next program point
        ///   1b) and there is no syntactic next program point
        /// 2) the current program point refers to a final operation (e.g. add elements on stack)
        ///    and there is no syntactic next program point
        /// 3) the current program point is a no-op and there is no syntactic next program point
        ///
        /// We encode these outcomes as follows:
        /// If the result is null, it means there is a next program point, otherwise, it is the final operation
        /// of the current PC (possibly no-ops, e.g. Block, or ExpressionStatement).
        /// The out parameter nestedStart is non-null if the current PC labels that particular sub-expression
        /// and it is null, if the current program point does not have a sub-expression to be evaluated.
        /// If both the result and the nexted start are null, then this is a special encoding, e.g., for Begin_Old
        /// </summary>
        /// <param name="nestedStart">Returns the sub-tree at this PC or null.</param>
        /// <returns>The final operation referred to by this PC or null if there is a syntactic next pc.
        /// </returns>
        public static Node Decode(PC pc, out Node nestedStart)
        {
            Node result = DecodeInternal(pc, out nestedStart);

            // shortcut certain nested nodes:
            if (nestedStart != null)
            {
                switch (nestedStart.NodeType)
                {
                    case NodeType.BlockExpression:
                        BlockExpression blockExpression = (BlockExpression)nestedStart;
                        nestedStart = blockExpression.Block;
                        break;
                }
            }
            if (nestedStart != null && !(nestedStart is Variable) && !nestedStart.SourceContext.IsValid)
            {
                if (!(nestedStart is Literal) && pc.Node.SourceContext.IsValid)
                {
                    nestedStart.SourceContext = pc.Node.SourceContext;
                }
                else
                {
                    Block block = pc.Node as Block;
                    if (block != null)
                    {
                        // prefer source context of next statement.
                        if (pc.Index + 1 < block.Statements.Count && block.Statements[pc.Index + 1] != null && block.Statements[pc.Index + 1].SourceContext.IsValid)
                        {
                            nestedStart.SourceContext = block.Statements[pc.Index + 1].SourceContext;
                        }
                        else if (pc.Index > 0 && block.Statements[pc.Index - 1] != null && block.Statements[pc.Index - 1].SourceContext.IsValid)
                        {
                            nestedStart.SourceContext = block.Statements[pc.Index - 1].SourceContext;
                        }
                    }
                }
            }
            return result;
        }
        private static Node DecodeInternal(PC pc, out Node nestedStart)
        {
            Node node = pc.Node;
            if (node == null) { nestedStart = null; return node; }
            int index = pc.Index;
            switch (node.NodeType)
            {
                case NodeType.Block:
                    Block block = (Block)node;
                    if (block.Statements == null)
                    {
                        nestedStart = null;
                        return block;
                    }
                    // determine the sub-expression
                    if (index < block.Statements.Count)
                    {
                        nestedStart = block.Statements[index];
                    }
                    else
                    {
                        nestedStart = null;
                    }
                    // determine if there is a syntactic next point in this block
                    if (index + 1 < block.Statements.Count)
                    {
                        return null;
                    }
                    else
                    {
                        return block; // final point in this block
                    }

                case NodeType.ExpressionStatement:
                    // has a single PC, prior to evaluating the expression
                    ExpressionStatement estmt = (ExpressionStatement)node;
                    nestedStart = estmt.Expression;
                    // final program point
                    return estmt;

                case NodeType.AssignmentStatement:
                    AssignmentStatement aStmt = (AssignmentStatement)node;
                    // There are 3 cases
                    // a) the left-hand-side top-level is an Indexer e0[e1] := e2
                    //    Then we have 4 program points.
                    //    0 - e0
                    //    1 - e1
                    //    2 - e2
                    //    3 - stelem
                    //
                    // b) the left hand side is an instance member binding e0.f := e1 or an indirect store *e0 := e1
                    //    Then we have 3 program points
                    //    0 - e0
                    //    1 - e1
                    //    2 - ldfld or stind
                    //
                    // c) the left hand side is a variable x := e0 or a static field sf := e0
                    //    Then we have 2 program points
                    //    0 - e0
                    //    1 - st or stsfld
                    int adjustedIndex = index;
                    Indexer asgIndex = aStmt.Target as Indexer;
                    if (asgIndex != null)
                    {
                        switch (index)
                        {
                            case 0:
                                nestedStart = asgIndex.Object;
                                return null;
                            case 1:
                                nestedStart = asgIndex.Operands[0];
                                return null;
                        }
                        goto assignmentDefault;
                    }
                    AddressDereference asgAD = aStmt.Target as AddressDereference;
                    if (asgAD != null)
                    {
                        adjustedIndex++;
                        if (adjustedIndex == 1)
                        {
                            nestedStart = asgAD.Address;
                            return null;
                        }
                        // check for hacky CCI1 encoding of initobj
                        var litnull = aStmt.Source as Literal;
                        if (litnull != null && litnull.Value == null && IsPotentialValueType(asgAD))
                        {
                            // end of initobj statement, don't visit null literal.
                            nestedStart = null;
                            return aStmt; // now effect assignment
                        }

                        goto assignmentDefault;
                    }
                    MemberBinding asgMB = aStmt.Target as MemberBinding;
                    if (asgMB != null)
                    {
                        adjustedIndex++;
                        if (asgMB.BoundMember.IsStatic)
                        {
                            adjustedIndex++;
                        }
                        if (adjustedIndex == 1)
                        {
                            nestedStart = asgMB.TargetObject;
                            return null;
                        }
                        goto assignmentDefault;
                    }
                    Variable asgVar = aStmt.Target as Variable;
                    if (asgVar != null)
                    {
                        adjustedIndex += 2;
                    }
                    else
                    {
                        nestedStart = null;
                        return aStmt;
                    }
                assignmentDefault:
                    // all different targets have now adjusted so that 2 means source, 3 means execute
                    switch (adjustedIndex)
                    {
                        case 2:
                            nestedStart = aStmt.Source;
                            return null;

                        default:
                            nestedStart = null;
                            return aStmt; // now effect assignment
                    }


                case NodeType.Branch:
                    // we have 2 points if the branch is conditional, otherwise a single point
                    Branch branch = (Branch)node;
                    if (branch.Condition != null && index == 0)
                    {
                        nestedStart = branch.Condition;
                        return null;
                    }
                    // effect branch return
                    nestedStart = null;
                    return branch;

                case NodeType.SwitchInstruction:
                    SwitchInstruction swInst = (SwitchInstruction)node;
                    // there are 2 points, the switch expression, and the actual switch decision
                    if (index == 0)
                    {
                        nestedStart = swInst.Expression;
                        return null;
                    }
                    // effect switch
                    nestedStart = null;
                    return swInst;

                case NodeType.Return:
                    // we have 2 points if the return takes an argument, othwerise just 1.
                    Return retStmt = (Return)node;
                    if (retStmt.Expression != null && index == 0)
                    {
                        nestedStart = retStmt.Expression;
                        return null;
                    }
                    // effect return
                    nestedStart = null;
                    return retStmt;

                case NodeType.Throw:
                    // there are 2 points, the thrown expression, and the actual throw
                    Throw throwStmt = (Throw)node;
                    if (index == 0)
                    {
                        nestedStart = throwStmt.Expression;
                        return null;
                    }
                    // effect throw
                    nestedStart = null;
                    return throwStmt;

                //
                // Expression
                //
                case NodeType.Indexer:
                    // there are three program points:
                    // 0 - indexed object
                    // 1 - index
                    // 2 - actual ldelem
                    Indexer indexer = (Indexer)node;
                    if (index == 0)
                    {
                        nestedStart = indexer.Object;
                        return null;
                    }
                    if (index == 1)
                    {
                        nestedStart = indexer.Operands[0];
                        return null;
                    }
                    // effect indexing
                    nestedStart = null;
                    return indexer;

                case NodeType.MemberBinding:
                    // there are 1 or 2 program points, depending on whether the field is static
                    // 0 - object whose field is read
                    // 1 - instance field read
                    //
                    // 0 - static field read
                    MemberBinding mb = (MemberBinding)node;
                    if (index == 0 && !mb.BoundMember.IsStatic)
                    {
                        nestedStart = mb.TargetObject;
                        return null;
                    }
                    // effect ldfld or ldsfld
                    nestedStart = null;
                    return mb;

                case NodeType.AddressDereference:
                    // there are 2 program points
                    // 0 - evaluating the address
                    // 1 - doing the dereference
                    AddressDereference adref = (AddressDereference)node;
                    if (index == 0)
                    {
                        nestedStart = adref.Address;
                        return null;
                    }
                    nestedStart = null;
                    return adref;

                case NodeType.ConstructArray:
                    // there are 2 points
                    // 0 - size expression
                    // 1 - actual construction
                    ConstructArray carray = (ConstructArray)node;
                    if (index == 0)
                    {
                        nestedStart = carray.Operands[0];
                        return null;
                    }
                    // effect allocation
                    nestedStart = null;
                    return carray;

                case NodeType.Construct:
                    // there are n+1 program points if there are n arguments, one for each arg + the actual op
                    Construct constr = (Construct)node;
                    if (index < constr.Operands.Count)
                    {
                        nestedStart = constr.Operands[index];
                        return null;
                    }
                    // effect the construction
                    nestedStart = null;
                    return constr;

                case NodeType.MethodCall:
                case NodeType.Callvirt:
                case NodeType.Call:
                case NodeType.Calli:
                    // there are 2 cases: 1) static method call 2) instance method call
                    // In case 1) there are n+1 program points if there are n arguments, one for each arg + the actual op
                    // in case 2) there are n+2 program points, the target object, the arguments, and the actual call
                    MethodCall call = (MethodCall)node;
                    MemberBinding callTarget = (MemberBinding)call.Callee;
                    if (callTarget.BoundMember.IsStatic)
                    {
                        if (index < call.Operands.Count)
                        {
                            nestedStart = call.Operands[index];
                            return null;
                        }
                        nestedStart = null;
                        return call;
                    }
                    if (index == 0)
                    {
                        nestedStart = callTarget.TargetObject;
                        return null;
                    }
                    if (index < call.Operands.Count + 1)
                    {
                        nestedStart = call.Operands[index - 1];
                        return null;
                    }
                    // effect the call
                    nestedStart = null;
                    return call;

                case NodeType.OldExpression:
                    // has 3 points:
                    //   0 is begin_old
                    //   1 is nested aggregate expression
                    //   2 is end_old
                    if (index == 0)
                    {
                        nestedStart = null;
                        return null; // special encoding
                    }
                    if (index == 1)
                    {
                        OldExpression oldExp = (OldExpression)node;
                        nestedStart = oldExp.expression;
                        return null;
                    }
                    // no more next
                    nestedStart = null;
                    return node;

                case NodeType.ReadOnlyAddressOf:
                case NodeType.AddressOf:
                case NodeType.RefAddress:
                case NodeType.OutAddress:
                    // This depends on the element whose address is taken
                    // a) static field or a local variable (single program point)
                    // b) instance field (two points, object, and ldfldaddr)
                    // c) indexer (three points, array, index, and ldelema)
                    UnaryExpression addrOf = (UnaryExpression)node;
                    MemberBinding addrMB = addrOf.Operand as MemberBinding;
                    if (addrMB != null)
                    {
                        if (index == 0 && !addrMB.BoundMember.IsStatic)
                        {
                            nestedStart = addrMB.TargetObject;
                            return null;
                        }
                        // effect ldflda ldsflda
                        nestedStart = null;
                        return addrOf;
                    }
                    Indexer addrIndexer = addrOf.Operand as Indexer;
                    if (addrIndexer != null)
                    {
                        if (index == 0)
                        {
                            nestedStart = addrIndexer.Object;
                            return null;
                        }
                        if (index == 1)
                        {
                            nestedStart = addrIndexer.Operands[0];
                            return null;
                        }
                        // effect ldelema
                        nestedStart = null;
                        return addrOf;
                    }
                    Contract.Assume(addrOf.Operand is Variable);
                    // effect lda
                    nestedStart = null;
                    return addrOf;

                case NodeType.Ldftn:
                    UnaryExpression unexpForLdftn = (UnaryExpression)node;
                    MemberBinding mbForLdftn = (MemberBinding)unexpForLdftn.Operand;
                    if (mbForLdftn.TargetObject == null || index > 0)
                    {
                        nestedStart = null;
                        return node;
                    }
                    nestedStart = mbForLdftn.TargetObject;
                    return null;

                case NodeType.Ldvirtftn:
                    BinaryExpression binexpForLdVirtftn = (BinaryExpression)node;
                    if (index == 0)
                    {
                        nestedStart = binexpForLdVirtftn.Operand1;
                        return null;
                    }
                    MemberBinding mbForLdVirtftn = (MemberBinding)binexpForLdVirtftn.Operand2;
                    if (mbForLdVirtftn.TargetObject == null || index > 1)
                    {
                        nestedStart = null;
                        return node;
                    }
                    nestedStart = mbForLdVirtftn.TargetObject;
                    return null;

                case NodeType.Ldtoken:
                case NodeType.Sizeof:
                    // single program point
                    nestedStart = null;
                    return node;

                case NodeType.As:
                case NodeType.Mkrefany:
                case NodeType.Is:
                case NodeType.Isinst:
                case NodeType.Box:
                case NodeType.ExplicitCoercion:
                case NodeType.Unbox:
                case NodeType.UnboxAny:
                case NodeType.Castclass:
                case NodeType.Refanyval:
                    BinaryExpression binexpWithSingleEval = (BinaryExpression)node;
                    // there are 2 program points
                    // 0 - single evaluation argument
                    // 1 - actual operation
                    if (index == 0)
                    {
                        nestedStart = binexpWithSingleEval.Operand1;
                        return null;
                    }
                    // effect operation
                    nestedStart = null;
                    return binexpWithSingleEval;

                case NodeType.MethodContract:
                    MethodContract methodContract = (MethodContract)node;
                    // there are many sub-expressions.
                    //   0                                                            : contractInitializer block for requires
                    //                                                                  and old
                    //   1 - Requires.Length                                          : individual requires sub expressions. 
                    //   Requires.Length + 1                                          : is end of requires
                    //   -------------------- note: after requires, we don't fall into ensures evaluation
                    //   Requires.Length + 2                                          : contractInitializer block for ensures
                    //   Requires.Length + 3                                          : post preamble block (possibly null)
                    //   Requires.Length + 4 - (Requires.Length + Ensures.Length + 3) : individual ensures expressions
                    //   Requires.Length + Ensures.Length + 4                         : is end of ensures
                    //   -------------------- note: after ensures, we don't fall into model ensures evaluation
                    //   Requires.Length + Ensures.Length + 5                         : is start of model ensures
                    //
                    if (index == 0)
                    {
                        nestedStart = methodContract.ContractInitializer;
                        return null;
                    }
                    if (index <= methodContract.RequiresCount)
                    {
                        nestedStart = methodContract.Requires[index - 1]; // note shift due to locals initializer index
                        return null;
                    }
                    if (index == methodContract.RequiresCount + 1)
                    {
                        // end of requires
                        nestedStart = null;
                        return methodContract;
                    }
                    int ensuresIndex = index - 4 - methodContract.RequiresCount;
                    if (ensuresIndex == -2)
                    {
                        nestedStart = methodContract.ContractInitializer;
                        return null;
                    }
                    if (ensuresIndex == -1)
                    {
                        nestedStart = methodContract.PostPreamble;
                        return null;
                    }
                    if (ensuresIndex < methodContract.EnsuresCount)
                    {
                        nestedStart = methodContract.Ensures[ensuresIndex];
                        return null;
                    }
                    if (ensuresIndex == methodContract.EnsuresCount)
                    {
                        // end of ensures
                        nestedStart = null;
                        return methodContract;
                    }
                    var modelEnsuresIndex = ensuresIndex - methodContract.EnsuresCount - 1;
                    if (modelEnsuresIndex < methodContract.ModelEnsuresCount)
                    {
                        nestedStart = methodContract.ModelEnsures[modelEnsuresIndex];
                        return null;
                    }
                    // end of model ensures
                    nestedStart = null;
                    return methodContract;

                case NodeType.TypeContract:
                    TypeContract typeContract = (TypeContract)node;
                    // there are sub-expressions for each invariant
                    //   0 - Invariants.Length - 1                              : individual invariant sub expressions. 
                    if (index < typeContract.InvariantCount)
                    {
                        nestedStart = typeContract.Invariants[index];
                        return null;
                    }
                    // end of invariants
                    nestedStart = null;
                    return typeContract;

                case NodeType.Invariant:
                    // 0 is nested condition
                    // 1 is actual invariant
                    Invariant invariant = (Invariant)node;
                    if (index == 0)
                    {
                        nestedStart = invariant.Condition;
                        return null;
                    }
                    nestedStart = null;
                    return node;

                case NodeType.RequiresPlain:
                case NodeType.RequiresOtherwise:
                case NodeType.Requires:
                    // 0 is nested condition
                    // 1 is actual requires
                    Requires requires = (Requires)node;
                    if (index == 0)
                    {
                        nestedStart = requires.Condition;
                        return null;
                    }
                    nestedStart = null;
                    return node;

                case NodeType.Ensures:
                case NodeType.EnsuresNormal:
                case NodeType.EnsuresExceptional:
                    // 0 is nested condition
                    // 1 is actual ensures
                    Ensures ensures = (Ensures)node;
                    if (index == 0)
                    {
                        nestedStart = ensures.PostCondition;
                        return null;
                    }
                    nestedStart = null;
                    return node;

                /*
                 * The following node types have no sub-expressions to evaluate
                case NodeType.Arglist:

                */
                default:
                    // Handle remaining ternary, unary and binary expressions here
                    BinaryExpression binexp = node as BinaryExpression;
                    if (binexp != null)
                    {
                        if (index == 0)
                        {
                            nestedStart = binexp.Operand1;
                            return null;
                        }
                        if (index == 1)
                        {
                            nestedStart = binexp.Operand2;
                            return null;
                        }
                        // effect binary operation
                        nestedStart = null;
                        return binexp;
                    }
                    UnaryExpression unexp = node as UnaryExpression;
                    if (unexp != null)
                    {
                        if (index == 0)
                        {
                            nestedStart = unexp.Operand;
                            return null;
                        }
                        nestedStart = null;
                        return unexp;
                    }
                    TernaryExpression ternexp = node as TernaryExpression;
                    if (ternexp != null)
                    {
                        if (index == 0)
                        {
                            nestedStart = ternexp.Operand1;
                            return null;
                        }
                        if (index == 1)
                        {
                            nestedStart = ternexp.Operand2;
                            return null;
                        }
                        if (index == 2)
                        {
                            nestedStart = ternexp.Operand3;
                            return null;
                        }
                        // effect ternary operation
                        nestedStart = null;
                        return ternexp;
                    }
                    // all other cases points that have no successor (atomic) and no nested sub-expressions
                    nestedStart = null;
                    return node;
            }

            // dead code.
        }

        private static bool IsPotentialValueType(AddressDereference asgAD)
        {
            return asgAD.Type.IsValueType || asgAD.Type is ITypeParameter;
        }

#if false
    public R Decode<T, R>(T data, CCIILProvider.PC label, ICodeQuery<CCIILProvider.PC, Local, Parameter, Method, Field, TypeNode, T, R> query) {

      Node nestedAggregate;
      Node finalOperation = Decode(label, out nestedAggregate);

      if (IsAtomicNested(nestedAggregate)) {
          return query.Atomic(data, label);
      }
      if (nestedAggregate != null) {
        return query.Aggregate(data, label, new PC(nestedAggregate), nestedAggregate is Block);
      }
      if (finalOperation == null)
      {
        // happens when a nested aggregate is null (e.g. a block statement element)
        // or special encoding for say begin_old
        if (label.Node != null && label.Node.NodeType == NodeType.OldExpression && label.Index == 0)
        {
          return query.BeginOld(data, label);
        }
        // treat it as NOP
        return query.Nop(data, label);
      }
      switch (finalOperation.NodeType) {
        case NodeType.Return:
          return query.Return(data, label);

        case NodeType.Throw:
        case NodeType.Rethrow:
          return query.Throw(data, label);

        case NodeType.Branch:
          Branch branch = (Branch)finalOperation;
          if (branch.Condition == null) {
            return query.Branch(data, label, SingletonPC(branch.Target));
          }
          else {
            return query.BranchCond(data, label, SingletonPC(branch.Target), true, true, new PC());
          }

        case NodeType.SwitchInstruction:
          SwitchInstruction switchInst = (SwitchInstruction)finalOperation;
          return query.BranchSwitch(data, label, Adapt(SystemTypes.Int32), ListOfPCs(switchInst.Targets), true, true, new PC());

        case NodeType.EndFinally:
          return query.EndFinally(data, label);

        case NodeType.Callvirt:
        case NodeType.Call:
          MethodCall call = (MethodCall)finalOperation;
          MemberBinding callee = (MemberBinding)call.Callee;
          Method method = (Method)callee.BoundMember;
          return query.Call(data, label, method, finalOperation.NodeType == NodeType.Callvirt);

        case NodeType.Construct:
          Construct construct = (Construct)finalOperation;
          MemberBinding calledConstructor = (MemberBinding)construct.Constructor;
          Method constructorMethod = (Method)calledConstructor.BoundMember;
          return query.NewObj(data, label, constructorMethod);

        case NodeType.OldExpression:
          return query.EndOld(data, label);

        case NodeType.Nop:
          return query.Nop(data, label);

        default:
          return query.Atomic(data, label);
      }

    }
#endif
        public Node Lookup(CCIILProvider.PC label)
        {
            return null;
        }

        public bool Next(CCIILProvider.PC current, out CCIILProvider.PC nextLabel)
        {
            Node subEval;
            Node finalOp = Decode(current, out subEval);
            if (finalOp == null && current.Node != null)
            {
                // has next
                nextLabel = new PC(current.Node, current.Index + 1);
                return true;
            }
            nextLabel = new PC();
            return false;
        }

        public PC Entry(Method method)
        {
            Contract.Requires(method != null);

            return new PC(method.Body);
        }

        public PC Requires(Method method)
        {
            Contract.Requires(method != null);

            return new PC(method.Contract, 0);
        }

        public PC Ensures(Method method)
        {
            Contract.Requires(method != null);
            Contract.Assume(method.Contract != null);
            return new PC(method.Contract, method.Contract.RequiresCount + 1);
        }

        public PC Invariant(TypeNode type)
        {
            Contract.Requires(type != null);

            return new PC(type.Contract, 0);
        }

        public IEnumerable<ExceptionHandler> TryBlocks(Method method)
        {
            foreach (ExceptionHandler eh in method.ExceptionHandlers)
            {
                yield return eh;
            }
        }

        public CCIILProvider.PC TryStart(ExceptionHandler info)
        {
            return new PC(info.TryStartBlock);
        }

        public CCIILProvider.PC TryEnd(ExceptionHandler info)
        {
            return new PC(info.BlockAfterTryEnd);
        }

        public CCIILProvider.PC FilterDecisionStart(ExceptionHandler info)
        {
            return new PC(info.FilterExpression);
        }

        public CCIILProvider.PC HandlerStart(ExceptionHandler info)
        {
            return new PC(info.HandlerStartBlock);
        }

        public CCIILProvider.PC HandlerEnd(ExceptionHandler info)
        {
            return new PC(info.BlockAfterHandlerEnd);
        }

#if false
    public void PrintCodeAt(PC label, string indent, TextWriter tw) {
      Node subEval;
      Node current = Decode(label, out subEval);
      if (IsAtomicNested(subEval)) {
        this.printer(label, indent, tw);
        return;
      }
      if (subEval != null) {
        return; // this is a skip step.
      }
      if (current == null)
      {
        this.printer(label, indent, tw);
      }
      if (current.NodeType == NodeType.Block) return;
      this.printer(label, indent, tw);
    }
#endif

        #endregion

        #region ICodeProvider<TypeNode,Method,PC,ExceptionHandler> Members

        protected override Node SourceNode(PC pc)
        {
            Node nestedStart;
            Node current = Decode(pc, out nestedStart);

            if (nestedStart != null)
            {
                if (nestedStart is Variable)
                {
                    // locals and parameters have bad source contexts
                    nestedStart.SourceContext = new SourceContext();
                    return nestedStart;
                }
                if (nestedStart is Literal)
                {
                    // literals have bad source contexts
                    nestedStart.SourceContext = new SourceContext();
                    return nestedStart;
                }
                if (nestedStart.SourceContext.IsValid) return nestedStart;
            }
            if (current != null && current.SourceContext.IsValid) return current;

            return pc.Node;
        }

        #endregion

        public CCIILProvider()
          : this(CCIMDDecoder.Value, CCIContractDecoder.Value)
        {
        }

        public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, TypeNode, AttributeNode, AssemblyNode> MetaData { get { return _mdDecoder; } }
        public IDecodeContracts<Local, Parameter, Method, Field, TypeNode> ContractDecoder { get { return _contractDecoder; } }

        public Result Decode<Visitor, Data, Result>(PC pc, Visitor visit, Data data) where Visitor : ICodeQuery<PC, Local, Parameter, Method, Field, TypeNode, Data, Result>
        {
            Contract.Assume(visit != null);

            Node nestedStart;
            Node currentOp = Decode(pc, out nestedStart);
            if (IsAtomicNested(nestedStart))
            {
                currentOp = nestedStart;
            }
            else if (nestedStart != null)
            {
                return visit.Aggregate(pc, new PC(nestedStart), nestedStart is Block, data);
            }
            if (currentOp == null)
            {
                // special encoding e.g. Begin_Old
                if (pc.Node.NodeType == NodeType.OldExpression && pc.Index == 0)
                {
                    // begin_old
                    return visit.BeginOld(pc, new PC(pc.Node, 2), data);
                }
                return visit.Nop(pc, data);
            }
            // decode current op
            switch (currentOp.NodeType)
            {
                case NodeType.Add:
                    return visit.Binary(pc, BinaryOperator.Add, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Add_Ovf:
                    return visit.Binary(pc, BinaryOperator.Add_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Add_Ovf_Un:
                    return visit.Binary(pc, BinaryOperator.Add_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.AddressDereference:
                    AddressDereference aderef = (AddressDereference)currentOp;
                    return visit.Ldind(pc, (aderef.Type), aderef.Volatile, Unit.Value, Unit.Value, data);

                case NodeType.AddressOf:
                case NodeType.OutAddress:
                case NodeType.RefAddress:
                case NodeType.ReadOnlyAddressOf:
                    UnaryExpression addressOf = (UnaryExpression)currentOp;
                    MemberBinding adofMb = addressOf.Operand as MemberBinding;
                    if (adofMb != null)
                    {
                        if (adofMb.BoundMember.IsStatic)
                        {
                            return visit.Ldsflda(pc, (Field)adofMb.BoundMember, Unit.Value, data);
                        }
                        return visit.Ldflda(pc, (Field)adofMb.BoundMember, Unit.Value, Unit.Value, data);
                    }
                    Indexer adofInd = addressOf.Operand as Indexer;
                    if (adofInd != null)
                    {
                        return visit.Ldelema(pc, (adofInd.Type), (currentOp.NodeType == NodeType.ReadOnlyAddressOf), Unit.Value, Unit.Value, Unit.Value, data);
                    }
                    Parameter adofParam = addressOf.Operand as Parameter;
                    if (adofParam != null)
                    {
                        return visit.Ldarga(pc, adofParam, false, Unit.Value, data);
                    }
                    Local adofLoc = addressOf.Operand as Local;
                    Contract.Assume(adofLoc != null);
                    return visit.Ldloca(pc, adofLoc, Unit.Value, data);

                case NodeType.And:
                    return visit.Binary(pc, BinaryOperator.And, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Arglist:
                    return visit.Arglist(pc, Unit.Value, data);

                case NodeType.AssignmentStatement:
                    AssignmentStatement astmt = (AssignmentStatement)currentOp;
                    Local astmtLoc = astmt.Target as Local;
                    if (astmtLoc != null)
                    {
                        return visit.Stloc(pc, astmtLoc, Unit.Value, data);
                    }
                    Parameter astmtParam = astmt.Target as Parameter;
                    if (astmtParam != null)
                    {
                        return visit.Starg(pc, astmtParam, Unit.Value, data);
                    }
                    MemberBinding astmtMb = astmt.Target as MemberBinding;
                    if (astmtMb != null)
                    {
                        if (astmtMb.BoundMember.IsStatic)
                        {
                            return visit.Stsfld(pc, (Field)astmtMb.BoundMember, astmtMb.Volatile, Unit.Value, data);
                        }
                        return visit.Stfld(pc, (Field)astmtMb.BoundMember, astmtMb.Volatile, Unit.Value, Unit.Value, data);
                    }
                    Indexer astmtInd = astmt.Target as Indexer;
                    if (astmtInd != null)
                    {
                        return visit.Stelem(pc, (astmtInd.Type), Unit.Value, Unit.Value, Unit.Value, data);
                    }
                    AddressDereference astmtad = astmt.Target as AddressDereference;
                    Contract.Assume(astmtad != null);

                    var litnull = astmt.Source as Literal;
                    if (litnull != null && litnull.Value == null && IsPotentialValueType(astmtad))
                    {
                        // cci1 hacky encoding of initobj
                        return visit.Initobj(pc, (astmtad.Type), Unit.Value, data);
                    }
                    // because stind does not contain unsigned/signed info, we have to grab the type from the pointer
                    var eltType = (astmtad.Address.Type is Reference) ? ((Reference)astmtad.Address.Type).ElementType :
                                  (astmtad.Address.Type is Pointer) ? ((Pointer)astmtad.Address.Type).ElementType :
                                  astmtad.Type;
                    return visit.Stind(pc, eltType, astmtad.Volatile, Unit.Value, Unit.Value, data);

                case NodeType.Box:
                    BinaryExpression boxexp = (BinaryExpression)currentOp;
                    return visit.Box(pc, GetTypeFrom(boxexp.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Branch:
                    Branch branch = (Branch)currentOp;
                    if (branch.Condition != null)
                    {
                        return visit.BranchTrue(pc, new PC(branch.Target), Unit.Value, data);
                    }
                    return visit.Branch(pc, new PC(branch.Target), branch.LeavesExceptionBlock, data);

                case NodeType.Call:
                    {
                        MethodCall call = (MethodCall)currentOp;
                        Method method = GetMethodFrom(call.Callee);
                        // foxtrot decoding
                        Method foxmethod = method;
                        if (method.Template != null) { foxmethod = method.Template; }
                        if (foxmethod.Name != null && foxmethod.DeclaringType.Name != null && foxmethod.DeclaringType.Name.Name.EndsWith("Contract"))
                        {
                            switch (foxmethod.Name.Name)
                            {
                                case "Assume":
                                    if (foxmethod.Parameters.Count == 1)
                                    {
                                        return visit.Assume(pc, "assume", Unit.Value, null, data);
                                    }
                                    break;

                                case "Assert":
                                    if (foxmethod.Parameters.Count == 1)
                                    {
                                        return visit.Assert(pc, "assert", Unit.Value, null, data);
                                    }
                                    break;

                                case "WritableBytes":
                                    if (foxmethod.Parameters.Count == 1)
                                    {
                                        return visit.Unary(pc, UnaryOperator.WritableBytes, false, true, Unit.Value, Unit.Value, data);
                                    }
                                    break;
                            }
                        }
                        ArrayIndexable<TypeNode> varargs = GetVarargs(call, method);
                        return visit.Call(pc, method, call.IsTailCall, false, varargs, Unit.Value, new UnitIndexable(method.Parameters.Count), data);
                    }

                case NodeType.Calli:
                    MethodCall calli = (MethodCall)currentOp;
                    TypeNode returnType;
                    IIndexable<TypeNode> argTypes;
                    bool isInstance;
                    GetCalliFunctionSignature(calli, out returnType, out argTypes, out isInstance);
                    return visit.Calli(pc, returnType, argTypes, calli.IsTailCall, isInstance, Unit.Value, Unit.Value, new ArrayIndexable<Unit>(null), data);

                case NodeType.Callvirt:
                    {
                        MethodCall callvirt = (MethodCall)currentOp;
                        Method method = GetMethodFrom(callvirt.Callee);
                        ArrayIndexable<TypeNode> varargs = GetVarargs(callvirt, method);
                        if (callvirt.Constraint != null)
                        {
                            return visit.ConstrainedCallvirt(pc, method, callvirt.IsTailCall, (callvirt.Constraint), varargs, Unit.Value, new ArrayIndexable<Unit>(null), data);
                        }
                        return visit.Call(pc, method, callvirt.IsTailCall, true, varargs, Unit.Value, new ArrayIndexable<Unit>(null), data);
                    }

                case NodeType.Castclass:
                    BinaryExpression castclass = (BinaryExpression)currentOp;
                    return visit.Castclass(pc, GetTypeFrom(castclass.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Ceq:
                    return visit.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Cgt:
                    return visit.Binary(pc, BinaryOperator.Cgt, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Cgt_Un:
                    return visit.Binary(pc, BinaryOperator.Cgt_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Ckfinite:
                    return visit.Ckfinite(pc, Unit.Value, Unit.Value, data);

                case NodeType.Clt:
                    return visit.Binary(pc, BinaryOperator.Clt, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Clt_Un:
                    return visit.Binary(pc, BinaryOperator.Clt_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Construct:
                    Construct construct = (Construct)currentOp;
                    Method ctor = GetMethodFrom(construct.Constructor);
                    if (ctor.DeclaringType is ArrayType)
                    {
                        ArrayType arrayType = (ArrayType)ctor.DeclaringType;
                        return visit.Newarray(pc, (arrayType.ElementType), Unit.Value, new UnitIndexable(ctor.Parameters.Count), data);
                    }
                    else
                    {
                        return visit.Newobj(pc, ctor, Unit.Value, new UnitIndexable(ActualParameters(ctor)), data);
                    }

                case NodeType.ConstructArray:
                    ConstructArray constructarray = (ConstructArray)currentOp;
                    return visit.Newarray(pc, (constructarray.ElementType), Unit.Value, new UnitIndexable(1), data);

                case NodeType.Conv_I:
                    return visit.Unary(pc, UnaryOperator.Conv_i, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_I1:
                    return visit.Unary(pc, UnaryOperator.Conv_i1, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_I2:
                    return visit.Unary(pc, UnaryOperator.Conv_i2, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_I4:
                    return visit.Unary(pc, UnaryOperator.Conv_i4, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_I8:
                    return visit.Unary(pc, UnaryOperator.Conv_i8, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I:
                    return visit.Unary(pc, UnaryOperator.Conv_i, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I1:
                    return visit.Unary(pc, UnaryOperator.Conv_i1, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I2:
                    return visit.Unary(pc, UnaryOperator.Conv_i2, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I4:
                    return visit.Unary(pc, UnaryOperator.Conv_i4, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I8:
                    return visit.Unary(pc, UnaryOperator.Conv_i8, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_i, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I1_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_i1, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I2_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_i2, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I4_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_i4, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_I8_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_i8, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_U:
                    return visit.Unary(pc, UnaryOperator.Conv_u, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_U1:
                    return visit.Unary(pc, UnaryOperator.Conv_u1, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_U2:
                    return visit.Unary(pc, UnaryOperator.Conv_u2, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_U4:
                    return visit.Unary(pc, UnaryOperator.Conv_u4, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_U8:
                    return visit.Unary(pc, UnaryOperator.Conv_u8, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U:
                    return visit.Unary(pc, UnaryOperator.Conv_u, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U1:
                    return visit.Unary(pc, UnaryOperator.Conv_u1, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U2:
                    return visit.Unary(pc, UnaryOperator.Conv_u2, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U4:
                    return visit.Unary(pc, UnaryOperator.Conv_u4, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U8:
                    return visit.Unary(pc, UnaryOperator.Conv_u8, true, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_u, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U1_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_u1, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U2_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_u2, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U4_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_u4, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_Ovf_U8_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_u8, true, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_R_Un:
                    return visit.Unary(pc, UnaryOperator.Conv_r_un, false, true, Unit.Value, Unit.Value, data);

                case NodeType.Conv_R4:
                    return visit.Unary(pc, UnaryOperator.Conv_r4, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Conv_R8:
                    return visit.Unary(pc, UnaryOperator.Conv_r8, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Cpblk:
                    return visit.Cpblk(pc, false, Unit.Value, Unit.Value, Unit.Value, data); // where is the volatile flag?

                case NodeType.DebugBreak:
                    return visit.Break(pc, data);

                case NodeType.Div:
                    return visit.Binary(pc, BinaryOperator.Div, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Div_Un:
                    return visit.Binary(pc, BinaryOperator.Div_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Dup:
                    return visit.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);

                case NodeType.EndFilter:
                    EndFilter ef = (EndFilter)currentOp;
                    return visit.Endfilter(pc, Unit.Value, data);

                case NodeType.EndFinally:
                    return visit.Endfinally(pc, data);

                case NodeType.Ensures:
                case NodeType.EnsuresNormal:
                    {
                        // update source context of ensures
                        Ensures ensures = (Ensures)currentOp;
                        if (!currentOp.SourceContext.IsValid)
                        {
                            UpdateSourceContext(ensures, ensures.PostCondition);
                        }
                        if (ensures.UsesModels)
                        {
                            return visit.Assume(pc, "assume", Unit.Value, null, data);
                        }
                        else
                        {
                            return visit.Assert(pc, "ensures", Unit.Value, null, data);
                        }
                    }
                case NodeType.EnsuresExceptional:
                    {
                        // update source context of ensures
                        EnsuresExceptional ensures = (EnsuresExceptional)currentOp;
                        if (!currentOp.SourceContext.IsValid)
                        {
                            UpdateSourceContext(ensures, ensures.PostCondition);
                        }
                        if (ensures.UsesModels)
                        {
                            return visit.Assume(pc, "assume", Unit.Value, null, data);
                        }
                        else
                        {
                            return visit.Assert(pc, "ensures_exceptional", Unit.Value, null, data);
                        }
                    }
                case NodeType.Eq:
                    return visit.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);

                // this case is used because deserialization produces pre-normalized il
                case NodeType.ExplicitCoercion:
                    BinaryExpression excor = (BinaryExpression)currentOp;
                    TypeNode cortype = (TypeNode)((Literal)excor.Operand2).Value;
                    if (cortype.IsPointerType)
                    {
                        // do nothing
                        return visit.Nop(pc, data);
                    }
                    else if (cortype is ITypeParameter)
                    {
                        return visit.Unboxany(pc, (cortype), Unit.Value, Unit.Value, data);
                    }
                    else if (cortype.IsValueType)
                    {
                        // either unbox or primitive
                        if (excor.Operand1.Type == null || excor.Operand1.Type.IsValueType)
                        {
                            // for now, ignore primitive coercions
                            return visit.Nop(pc, data);
                        }
                        else
                        {
                            return visit.Unbox(pc, (cortype), Unit.Value, Unit.Value, data);
                        }
                    }
                    else
                    {
                        return visit.Castclass(pc, (cortype), Unit.Value, Unit.Value, data);
                    }

                case NodeType.Ge:
                    return visit.Binary(pc, BinaryOperator.Cge, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Gt:
                    return visit.Binary(pc, BinaryOperator.Cgt, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Indexer:
                    Indexer indexer = (Indexer)currentOp;
                    return visit.Ldelem(pc, (indexer.ElementType), Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Initblk:
                    return visit.Initblk(pc, false, Unit.Value, Unit.Value, Unit.Value, data); // where is the volatile flag?

                case NodeType.Invariant:
                    {
                        // update source context of invariant
                        Invariant invariant = (Invariant)currentOp;
                        if (!currentOp.SourceContext.IsValid)
                        {
                            UpdateSourceContext(invariant, invariant.Condition);
                        }
                        if (invariant.UsesModels)
                        {
                            return visit.Assume(pc, "assume", Unit.Value, null, data);
                        }
                        else
                        {
                            return visit.Assume(pc, "invariant", Unit.Value, null, data);
                        }
                    }
                case NodeType.Isinst:
                    BinaryExpression isinst = (BinaryExpression)currentOp;
                    return visit.Isinst(pc, GetTypeFrom(isinst.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Jmp:
                    MethodCall jmp = (MethodCall)currentOp;
                    return visit.Jmp(pc, GetMethodFrom(jmp.Callee), data);

                case NodeType.Ldftn:
                    UnaryExpression ldftn = (UnaryExpression)currentOp;
                    return visit.Ldftn(pc, GetMethodFrom(ldftn.Operand), Unit.Value, data);

                case NodeType.Ldlen:
                    return visit.Ldlen(pc, Unit.Value, Unit.Value, data);

                case NodeType.Ldtoken:
                    UnaryExpression ldtok = (UnaryExpression)currentOp;
                    Contract.Assert(visit != null);
                    return DecodeLdtoken(pc, visit, ldtok.Operand, data);

                case NodeType.Ldvirtftn:
                    BinaryExpression ldvirtftn = (BinaryExpression)currentOp;
                    return visit.Ldvirtftn(pc, GetMethodFrom(ldvirtftn.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Le:
                    return visit.Binary(pc, BinaryOperator.Cle, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Literal:
                    Literal lit = (Literal)currentOp;
                    if (lit.Value == null) { return visit.Ldnull(pc, Unit.Value, data); }
                    if (lit.Type == SystemTypes.Boolean)
                    {
                        // the extractor introduces Boolean true and false values into the code
                        bool value = (bool)lit.Value;
                        if (value)
                        {
                            return visit.Ldconst(pc, (Int32)1, (SystemTypes.Int32), Unit.Value, data);
                        }
                        else
                        {
                            return visit.Ldconst(pc, (Int32)0, (SystemTypes.Int32), Unit.Value, data);
                        }
                    }
                    Contract.Assume(lit.Type != SystemTypes.Int32 || lit.Value is Int32);
                    return visit.Ldconst(pc, lit.Value, (lit.Type), Unit.Value, data);

                case NodeType.Local:
                    return visit.Ldloc(pc, (Local)currentOp, Unit.Value, data);

                case NodeType.Localloc:
                    return visit.Localloc(pc, Unit.Value, Unit.Value, data);

                case NodeType.Lt:
                    return visit.Binary(pc, BinaryOperator.Clt, Unit.Value, Unit.Value, Unit.Value, data); // where is the unsigned info?

                case NodeType.MemberBinding:
                    MemberBinding mb = (MemberBinding)currentOp;
                    if (mb.BoundMember.IsStatic)
                    {
                        return visit.Ldsfld(pc, (Field)mb.BoundMember, mb.Volatile, Unit.Value, data);
                    }
                    return visit.Ldfld(pc, (Field)mb.BoundMember, mb.Volatile, Unit.Value, Unit.Value, data);

                case NodeType.MethodContract:
                    return visit.Nop(pc, data);

                case NodeType.Mkrefany:
                    BinaryExpression mkrefany = (BinaryExpression)currentOp;
                    return visit.Mkrefany(pc, GetTypeFrom(mkrefany.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Mul:
                    return visit.Binary(pc, BinaryOperator.Mul, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Mul_Ovf:
                    return visit.Binary(pc, BinaryOperator.Mul_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Mul_Ovf_Un:
                    return visit.Binary(pc, BinaryOperator.Mul_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Ne:
                    return visit.Binary(pc, BinaryOperator.Cne_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Neg:
                    return visit.Unary(pc, UnaryOperator.Neg, false, false, Unit.Value, Unit.Value, data);

                case NodeType.Nop:
                case NodeType.Block: // happens due to our block decoding
                    return visit.Nop(pc, data);

                case NodeType.Not:
                case NodeType.LogicalNot:
                    return visit.Unary(pc, UnaryOperator.Not, false, false, Unit.Value, Unit.Value, data);

                case NodeType.OldExpression:
                    TypeNode oldExpType = ((OldExpression)pc.Node).expression.Type;
                    return visit.EndOld(pc, new PC(pc.Node, 0), (oldExpType), Unit.Value, Unit.Value, data);

                case NodeType.Or:
                    return visit.Binary(pc, BinaryOperator.Or, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Parameter:
                case NodeType.This:
                    return visit.Ldarg(pc, (Parameter)currentOp, false, Unit.Value, data);

                case NodeType.Pop:
                    if (currentOp is Statement || currentOp is UnaryExpression)
                    {
                        return visit.Pop(pc, Unit.Value, data);
                    }
                    return visit.Nop(pc, data); // this is an EPOP, CCI's representation for picking the value from the stack.

                case NodeType.Refanytype:
                    return visit.Refanytype(pc, Unit.Value, Unit.Value, data);

                case NodeType.Refanyval:
                    BinaryExpression refanyval = (BinaryExpression)currentOp;
                    return visit.Refanyval(pc, GetTypeFrom(refanyval.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Rem:
                    return visit.Binary(pc, BinaryOperator.Rem, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Rem_Un:
                    return visit.Binary(pc, BinaryOperator.Rem_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Rethrow:
                    return visit.Rethrow(pc, data);

                case NodeType.Return:
                    return visit.Return(pc, Unit.Value, data);

                case NodeType.ReturnValue:
                    TypeNode retvalType = ((ReturnValue)pc.Node).Type;
                    return visit.Ldresult(pc, retvalType, Unit.Value, Unit.Value, data);

                case NodeType.Requires:
                case NodeType.RequiresOtherwise:
                case NodeType.RequiresPlain:
                    {
                        Requires requires = (Requires)currentOp;
                        // update source context of requires
                        if (!currentOp.SourceContext.IsValid)
                        {
                            UpdateSourceContext(requires, requires.Condition);
                        }
                        if (requires.UsesModels)
                        {
                            return visit.Assume(pc, "assume", Unit.Value, null, data);
                        }
                        else
                        {
                            return visit.Assume(pc, "requires", Unit.Value, null, data);
                        }
                    }
                case NodeType.Shl:
                    return visit.Binary(pc, BinaryOperator.Shl, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Shr:
                    return visit.Binary(pc, BinaryOperator.Shr, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Shr_Un:
                    return visit.Binary(pc, BinaryOperator.Shr_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Sizeof:
                    UnaryExpression sizeofexp = (UnaryExpression)currentOp;
                    return visit.Sizeof(pc, GetTypeFrom(sizeofexp.Operand), Unit.Value, data);

                case NodeType.Sub:
                    return visit.Binary(pc, BinaryOperator.Sub, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Sub_Ovf:
                    return visit.Binary(pc, BinaryOperator.Sub_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.Sub_Ovf_Un:
                    return visit.Binary(pc, BinaryOperator.Sub_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

                case NodeType.SwitchInstruction:
                    SwitchInstruction switchInst = (SwitchInstruction)currentOp;
                    return visit.Switch(pc, (SystemTypes.Int32), SwitchPatternsTargets(switchInst.Targets), Unit.Value, data);

                case NodeType.Throw:
                    return visit.Throw(pc, Unit.Value, data);

                case NodeType.TypeContract:
                    return visit.Nop(pc, data);

                case NodeType.Unbox:
                    BinaryExpression unbox = (BinaryExpression)currentOp;
                    return visit.Unbox(pc, GetTypeFrom(unbox.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.UnboxAny:
                    BinaryExpression unboxany = (BinaryExpression)currentOp;
                    return visit.Unboxany(pc, GetTypeFrom(unboxany.Operand2), Unit.Value, Unit.Value, data);

                case NodeType.Xor:
                    return visit.Binary(pc, BinaryOperator.Xor, Unit.Value, Unit.Value, Unit.Value, data);
            }
            Contract.Assume(false);
            return visit.Nop(pc, data);
        }

        private int ActualParameters(Method m)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return m.Parameters.Count + (((m.CallingConvention & CallingConventionFlags.HasThis) != 0) ? 1 : 0);
        }

        private void UpdateSourceContext(Node node, Expression expression)
        {
            if (node.SourceContext.IsValid) return;
            BlockExpression bexp = expression as BlockExpression;
            Statement stmt = null;
            if (bexp != null)
            {
                Block b = bexp.Block;
                while (b != null && b.Statements.Count > 0)
                {
                    stmt = b.Statements[b.Statements.Count - 1];
                    b = stmt as Block;
                }
            }
            if (stmt != null)
            {
                node.SourceContext = stmt.SourceContext;
            }
            else
            {
                node.SourceContext = expression.SourceContext;
            }
        }

#if false
    public Transformer<PC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<PC, Local, Parameter, Method, Field, TypeNode, Unit, Unit, Data, Result> visitor)
    {
      // can't further cache
      return delegate(PC label, Data data) { return this.Decode<Data,Result,IVisitMSIL<PC,Local,Parameter,Method,Field,TypeNode,Unit,Unit,Data,Result>>(label, visitor, data); };
    }
#endif
        public Unit GetContext { get { return Unit.Value; } }
    }
}
