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
using Microsoft.Research.DataStructures;

using StackTemp = System.Int32;

namespace Microsoft.Research.CodeAnalysis
{
  using System.Diagnostics.Contracts;

  #region Delegate types
  /// <summary>
  /// Prints data on given output where each line is prefixed by prefix
  /// 
  /// </summary>
  /// <param name="tw">Output stream</param>
  /// <param name="prefix">Prefix to be printed on each new line</param>
  public delegate void Printer<Data>(TextWriter writer, string prefix, Data data);

  /// <summary>
  /// Generates a textual representation of code at label (none if label is a skip instruction)
  /// </summary>
  /// <param name="tw">Output stream</param>
  /// <param name="prefix">Prefix to be printed on each new line</param>
  /// <param name="lab">Label of code to be printed</param>
  public delegate void ILPrinter<Label>(Label label, string prefix, TextWriter writer);

  /// <summary>
  /// Used to print extra info associated with the end of control flow blocks.
  /// </summary>
  public delegate void BlockInfoPrinter<Label>(Label at, string prefix, TextWriter writer);

  #endregion

  #region IL visitor data types

  /// <summary>
  /// The binary branch operators
  /// </summary>
  public enum BranchOperator
  {
    Beq,
    Bge,
    Bge_un,
    Bgt,
    Bgt_un,
    Ble,
    Ble_un,
    Blt,
    Blt_un,
    Bne_un,
  }

  /// <summary>
  /// Slightly uniformized version of unary operations in IL
  /// </summary>
  [Serializable]
  public enum UnaryOperator
  {
    Conv_i,
    Conv_i1,
    Conv_i2,
    Conv_i4,
    Conv_i8,
    Conv_r4,
    Conv_r8,
    Conv_u,
    Conv_u1,
    Conv_u2,
    Conv_u4,
    Conv_u8,
    Conv_r_un,
    Neg,
    Not,
    /// <summary>
    /// A unary operator on pointers to express contracts about the writeable region extending forward from the pointer.
    /// The argument is of pointer type, the result type ulong.
    /// </summary>
    WritableBytes,
    /// <summary>
    /// A synthetic conversion to Decimal. We insert this instead of Decimal constructors.
    /// </summary>
    Conv_dec,
  }

  /// <summary>
  /// Slightly uniformized version of binary operations in IL
  /// </summary>
  [Serializable]
  public enum BinaryOperator
  {
    Add,
    Add_Ovf,
    Add_Ovf_Un,
    And,
    Ceq,
    Cobjeq,  // corresponds to Object.Equals, which behaves differently for some values from Ceq (NaN)
    Cne_Un,  // for semantics, consult Ceq in part III ECMA and negate condition
    Cge,     // for semantics, consult Bge in part III ECMA
    Cge_Un,  // for semantics, consult Bge_un in part III ECMA
    Cgt,
    Cgt_Un,
    Cle,     // for semantics, consult Ble in part III ECMA 
    Cle_Un,  // for semantics, consult Ble_un in part III ECMA
    Clt,
    Clt_Un,
    Div,
    Div_Un,
    LogicalAnd, // F: I've added those two for use in the refinement of expression involving boolean connectors
    LogicalOr,
    Mul,
    Mul_Ovf,
    Mul_Ovf_Un,
    Or,
    Rem,
    Rem_Un,
    Shl,
    Shr,
    Shr_Un,
    Sub,
    Sub_Ovf,
    Sub_Ovf_Un,
    Xor,
  }

  public static class OperatorExtensions
  {
    public static string ToCodeString(this BinaryOperator bop)
    {
      switch (bop)
      {
        case BinaryOperator.Add:
        case BinaryOperator.Add_Ovf:
        case BinaryOperator.Add_Ovf_Un:
          return "+";
        case BinaryOperator.And:
          return "&";
        case BinaryOperator.Cobjeq:
          return "=="; // TODO: eventually need something else here.
        case BinaryOperator.Ceq:
          return "==";
        case BinaryOperator.Cge:
        case BinaryOperator.Cge_Un:
          return ">=";
        case BinaryOperator.Cgt:
        case BinaryOperator.Cgt_Un:
          return ">";
        case BinaryOperator.Cle:
        case BinaryOperator.Cle_Un:
          return "<=";
        case BinaryOperator.Clt:
        case BinaryOperator.Clt_Un:
          return "<";
        case BinaryOperator.Cne_Un:
          return "!=";
        case BinaryOperator.Div:
        case BinaryOperator.Div_Un:
          return "/";
        case BinaryOperator.LogicalAnd:
          return "&&";
        case BinaryOperator.LogicalOr:
          return "||";
        case BinaryOperator.Mul:
        case BinaryOperator.Mul_Ovf:
        case BinaryOperator.Mul_Ovf_Un:
          return "*";
        case BinaryOperator.Or:
          return "|";
        case BinaryOperator.Rem:
        case BinaryOperator.Rem_Un:
          return "%";
        case BinaryOperator.Shl:
          return "<<";
        case BinaryOperator.Shr:
        case BinaryOperator.Shr_Un:
          return ">>";
        case BinaryOperator.Sub:
        case BinaryOperator.Sub_Ovf:
        case BinaryOperator.Sub_Ovf_Un:
          return "-";
        case BinaryOperator.Xor:
          return "^";

        default:
          throw new InvalidOperationException("Unknown binary operator");
      }
    }

    public static string ToCodeString(this UnaryOperator op)
    {
      switch (op)
      {
        case UnaryOperator.Conv_i:
          return "(UIntPtr)";
        case UnaryOperator.Conv_i1:
          return "(sbyte)";
        case UnaryOperator.Conv_i2:
          return "(short)";
        case UnaryOperator.Conv_i4:
          return "(int)";
        case UnaryOperator.Conv_i8:
          return "(long)";
        case UnaryOperator.Conv_r_un:
          return "(double)";
        case UnaryOperator.Conv_r4:
          return "(float)";
        case UnaryOperator.Conv_r8:
          return "(double)";
        case UnaryOperator.Conv_u:
          return "(UIntPtr)";
        case UnaryOperator.Conv_u1:
          return "(byte)";
        case UnaryOperator.Conv_u2:
          return "(ushort)";
        case UnaryOperator.Conv_u4:
          return "(uint)";
        case UnaryOperator.Conv_u8:
          return "(ulong)";
        case UnaryOperator.Neg:
          return "-";
        case UnaryOperator.Not:
          return "!";
        case UnaryOperator.WritableBytes:
          return "Contracts.WritableBytes";
        case UnaryOperator.Conv_dec:
          return "new Decimal";
        default:
          throw new InvalidOperationException("Unknown unary op");
      }
    }

    [Pure]
    public static bool IsComparisonBinaryOperator(this BinaryOperator op)
    {
      switch (op)
      {
        case BinaryOperator.Ceq:
        case BinaryOperator.Cge:
        case BinaryOperator.Cge_Un:
        case BinaryOperator.Cgt:
        case BinaryOperator.Cgt_Un:
        case BinaryOperator.Cle:
        case BinaryOperator.Cle_Un:
        case BinaryOperator.Clt:
        case BinaryOperator.Clt_Un:
        case BinaryOperator.Cne_Un:
        case BinaryOperator.Cobjeq:
          return true;
        default:
          return false;
      }
    }

    [Pure]
    public static bool IsEqualityOrDisequality(this BinaryOperator bop)
    {
      return bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un;
    }

    public static bool IsBooleanBinaryOperator(this BinaryOperator op)
    {
      switch (op)
      {
        case BinaryOperator.LogicalAnd:
        case BinaryOperator.LogicalOr:
          return true;
        default:
          return false;
      }
    }

    public static bool IsBitwiseBinaryOperator(this BinaryOperator op)
    {
      switch (op)
      {
        case BinaryOperator.And:
        case BinaryOperator.Or:
        case BinaryOperator.Xor:
          return true;
        default:
          return false;
      }
    }

    public static bool IsArithmeticBinaryOperator(this BinaryOperator op)
    {
      switch (op)
      {
        case BinaryOperator.Add:
        case BinaryOperator.Add_Ovf:
        case BinaryOperator.Add_Ovf_Un:
        case BinaryOperator.Div:
        case BinaryOperator.Div_Un:
        case BinaryOperator.Mul:
        case BinaryOperator.Mul_Ovf:
        case BinaryOperator.Mul_Ovf_Un:
        case BinaryOperator.Rem:
        case BinaryOperator.Rem_Un:
        case BinaryOperator.Sub:
        case BinaryOperator.Sub_Ovf:
        case BinaryOperator.Sub_Ovf_Un:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    public static bool IsOverflowChecked(this BinaryOperator op)
    {
      return op == BinaryOperator.Add_Ovf || op == BinaryOperator.Add_Ovf_Un ||
            op == BinaryOperator.Sub_Ovf || op == BinaryOperator.Sub_Ovf_Un ||
            op == BinaryOperator.Mul_Ovf || op == BinaryOperator.Mul_Ovf_Un;
    }

    public static bool TryNegate(this BinaryOperator op, out BinaryOperator negated)
    {
      switch (op)
      {
        case BinaryOperator.Ceq:
          {
            negated = BinaryOperator.Cne_Un;
            return true;
          }

        case BinaryOperator.Cge:
          {
            negated = BinaryOperator.Clt;
            return true;
          }

        case BinaryOperator.Cge_Un:
          {
            negated = BinaryOperator.Clt_Un;
            return true;
          }
        case BinaryOperator.Cgt:
          {
            negated = BinaryOperator.Cle;
            return true;
          }
        case BinaryOperator.Cgt_Un:
          {
            negated = BinaryOperator.Cle_Un;
            return true;
          }
        case BinaryOperator.Cle:
          {
            negated = BinaryOperator.Cgt;
            return true;
          }
        case BinaryOperator.Cle_Un:
          {
            negated = BinaryOperator.Cgt_Un;
            return true;
          }
        case BinaryOperator.Clt:
          {
            negated = BinaryOperator.Cge;
            return true;
          }
        case BinaryOperator.Clt_Un:
          {
            negated = BinaryOperator.Cge_Un;
            return true;
          }
        case BinaryOperator.Cne_Un:
          {
            negated = BinaryOperator.Ceq;
            return true;
          }
        default:
          {
            negated = BinaryOperator.Add;
            return false;
          }
      }
    }

    public static bool TryInvert(this BinaryOperator op, out BinaryOperator inverted)
    {
      switch (op)
      {
        case BinaryOperator.Ceq:
          {
            inverted = BinaryOperator.Ceq;
            return true;
          }

        case BinaryOperator.Cge:
          {
            inverted = BinaryOperator.Cle;
            return true;
          }

        case BinaryOperator.Cge_Un:
          {
            inverted = BinaryOperator.Cle_Un;
            return true;
          }
        case BinaryOperator.Cgt:
          {
            inverted = BinaryOperator.Clt;
            return true;
          }
        case BinaryOperator.Cgt_Un:
          {
            inverted = BinaryOperator.Clt_Un;
            return true;
          }
        case BinaryOperator.Cle:
          {
            inverted = BinaryOperator.Cge;
            return true;
          }
        case BinaryOperator.Cle_Un:
          {
            inverted = BinaryOperator.Cge_Un;
            return true;
          }
        case BinaryOperator.Clt:
          {
            inverted = BinaryOperator.Cgt;
            return true;
          }
        case BinaryOperator.Clt_Un:
          {
            inverted = BinaryOperator.Cgt_Un;
            return true;
          }
        case BinaryOperator.Cne_Un:
          {
            inverted = BinaryOperator.Cne_Un;
            return true;
          }
        default:
          {
            inverted = default(BinaryOperator);
            return false;
          }
      }
    }


    public static bool IsConversionOperator(this UnaryOperator op)
    {
      switch (op)
      {
        case UnaryOperator.Conv_i:
        case UnaryOperator.Conv_i1:
        case UnaryOperator.Conv_i2:
        case UnaryOperator.Conv_i4:
        case UnaryOperator.Conv_i8:
        case UnaryOperator.Conv_r4:
        case UnaryOperator.Conv_r8:
        case UnaryOperator.Conv_u:
        case UnaryOperator.Conv_u1:
        case UnaryOperator.Conv_u2:
        case UnaryOperator.Conv_u4:
        case UnaryOperator.Conv_u8:
        case UnaryOperator.Conv_r_un:
          return true;

        default:
          return false;
      }
    }
  }
  #endregion

  #region IDecodeMethods contract binding
  [ContractClass(typeof(IDecodeMethodsContract<>))]
  public partial interface IDecodeMethods<Method>
  {
    /// <summary>
    /// Short name of method without namespace and outer types
    /// </summary>
    [Pure]
    string Name(Method method);

    /// <summary>
    /// Full name of method including namespace and outer types
    /// </summary>
    [Pure]
    string FullName(Method method);

    /// <summary>
    /// Obtain the Documentation Id for the method
    /// </summary>
    [Pure]
    string DocumentationId(Method method);

    /// <summary>
    /// Get the canonical name of the member associated with this method. 
    /// A member can be a method, an event or a property. 
    /// This name is aimed at beeing created by the visual studio package
    /// </summary>
    [Pure]
    string DeclaringMemberCanonicalName(Method method);

    /// <summary>
    /// True iff method is the entry point of an executable
    /// </summary>
    [Pure]
    bool IsMain(Method method);

    /// <summary>
    /// True if method is static.
    /// </summary>
    [Pure]
    bool IsStatic(Method method);

    /// <summary>
    /// True if method is private
    /// </summary>
    [Pure]
    bool IsPrivate(Method method);

    /// <summary>
    /// True if method is protected
    /// </summary>
    [Pure]
    bool IsProtected(Method method);

    /// <summary>
    /// True if method is public
    /// </summary>
    [Pure]
    bool IsPublic(Method method);

    /// <summary>
    /// True if method is virtual
    /// </summary>
    [Pure]
    bool IsVirtual(Method method);

    /// <summary>
    /// True if method is declared as "new"
    /// </summary>
    [Pure]
    bool IsNewSlot(Method method);

    /// <summary>
    /// True if method is "override"
    /// </summary>
    [Pure]
    bool IsOverride(Method method);

    /// <summary>
    /// True if method is sealed
    /// </summary>
    [Pure]
    bool IsSealed(Method method);

    /// <summary>
    /// True if the return type of the method is void
    /// </summary>
    [Pure]
    bool IsVoidMethod(Method method);

    /// <summary>
    /// Returns true if the method is an instance constructor
    /// </summary>
    [Pure]
    bool IsConstructor(Method method);

    /// <summary>
    /// Returns true if the method is a finalizer
    /// </summary>
    [Pure]
    bool IsFinalizer(Method method);

    /// <summary>
    /// Returns true if the method is a Dispose implementation
    /// </summary>
    [Pure]
    bool IsDispose(Method method);

    /// <summary>
    /// Returns true if the method is internal
    /// </summary>
    [Pure]
    bool IsInternal(Method method);

    /// <summary>
    /// True if method is visible outside assembly within which it is declared
    /// E.g., public methods, or protected methods of publicly visible types, etc.
    /// </summary>
    [Pure]
    bool IsVisibleOutsideAssembly(Method method);

    /// <summary>
    /// True if method is abstract and has no body
    /// </summary>
    [Pure]
    bool IsAbstract(Method method);

    /// <summary>
    /// True if method is declared extern and has no body
    /// </summary>
    [Pure]
    bool IsExtern(Method method);

    /// <summary>
    /// Returns true if the method is a property setter
    /// </summary>
    [Pure]
    bool IsPropertySetter(Method method);

    /// <summary>
    /// Returns true if the method is a property getter
    /// </summary>
    [Pure]
    bool IsPropertyGetter(Method method);

    /// <summary>
    /// For an async generated MoveNext methods, returns true
    /// </summary>
    [Pure]
    bool IsAsyncMoveNext(Method method);

    /// <summary>
    /// For a MoveNext method from async or iterators, returns the start state (-1 or 0). Otherwise returns null.
    /// </summary>
    [Pure]
    int? MoveNextStartState(Method method);

  }

  [ContractClassFor(typeof(IDecodeMethods<>))]
  abstract class IDecodeMethodsContract<Method> : IDecodeMethods<Method>
  {
    public string Name(Method method)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return null;
    }

    public string FullName(Method method)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return null;
    }

    public string DocumentationId(Method method)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return null;
    }

    public string DeclaringMemberCanonicalName(Method method)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      throw new NotImplementedException();
    }

    public bool IsMain(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsStatic(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsPrivate(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsProtected(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsPublic(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsVirtual(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsNewSlot(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsOverride(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsSealed(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsVoidMethod(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsConstructor(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsFinalizer(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsDispose(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsInternal(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleOutsideAssembly(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsAbstract(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsExtern(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsPropertySetter(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsPropertyGetter(Method method)
    {
      throw new NotImplementedException();
    }

    #region IDecodeMethods<Method> Members


    public bool IsAsyncMoveNext(Method method)
    {
      throw new NotImplementedException();
    }

    public int? MoveNextStartState(Method method)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  [ContractClass(typeof(IDecodeMetaDataContracts<,,,,,,,,>))]
  public partial interface IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IDecodeMethods<Method>
  {
    #region Methods

    /// <summary>
    /// Returns the return type of a method
    /// </summary>
    [Pure]
    Type ReturnType(Method method);

    /// <summary>
    /// Returns the parameters of a method, NOT including "this" (even if the method is an instance method)
    /// </summary>
    [Pure]
    IIndexable<Parameter> Parameters(Method method);

    /// <summary>
    /// For non-static methods, returns a parameter corresponding to "this or arg0".
    /// Can throw if the method is static.
    /// </summary>
    [Pure]
    Parameter This(Method method);

    /// <summary>
    /// True if property is visible outside assembly within which it is declared
    /// </summary>
    [Pure]
    bool IsVisibleOutsideAssembly(Property property);

    /// <summary>
    /// True if event is visible outside assembly within which it is declared
    /// </summary>
    [Pure]
    bool IsVisibleOutsideAssembly(Event @event);

    /// <summary>
    /// True if field is visible outside assembly within which it is declared
    /// </summary>
    [Pure]
    bool IsVisibleOutsideAssembly(Field field);

    /// <summary>
    /// Returns the property containing the getter/setter if the method is actually a getter/setter.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    [Pure]
    Property GetPropertyFromAccessor(Method method);

    /// <summary>
    /// Returns true if the method is compiler generated
    /// </summary>
    [Pure]
    bool IsCompilerGenerated(Method method);

    /// <summary>
    /// Returns true if the method is marked with the DebuggerNonUserCode attribute
    /// </summary>
    [Pure]
    bool IsDebuggerNonUserCode(Method method);

    /// <summary>
    /// Returns true if the type is marked with the DebuggerNonUserCode attribute
    /// </summary>
    [Pure]
    bool IsDebuggerNonUserCode(Type t);

    /// <summary>
    /// Returns true if the method is an auto property getter or setter
    /// </summary>
    [Pure]
    bool IsAutoPropertyMember(Method method);

    /// <summary>
    /// Returns true if the method is an auto property setter
    /// </summary>
    [Pure]
    bool IsAutoPropertySetter(Method method, out Field backingField);

    /// <summary>
    /// Returns true if the type is compiler generated
    /// </summary>
    [Pure]
    bool IsCompilerGenerated(Type type);

    /// <summary>
    /// Returns true if the type was generated from C++
    /// </summary>
    [Pure]
    bool IsNativeCpp(Type type);

    /// <summary>
    /// Returns the declaring type containing this method
    /// </summary>
    [Pure]
    Type DeclaringType(Method method);

    /// <summary>
    /// Returns the declaring assembly containing this method
    /// </summary>
    [Pure]
    Assembly DeclaringAssembly(Method method);

    /// <summary>
    /// Returns the method def/ref token of the method if possible.
    /// </summary>
    [Pure]
    int MethodToken(Method method);

    /// <summary>
    /// Returns true if this method reference is specialized w.r.t. type parameters, either
    /// because the method is generic and instantiated, or because a declaring/parent type is
    /// instantiated.
    /// </summary>
    [Pure]
    bool IsSpecialized(Method method);

    /// <summary>
    /// Returns true if this method reference is specialized w.r.t. type parameters, either
    /// because the method is generic and instantiated, or because a declaring/parent type is
    /// instantiated.
    /// </summary>
    /// <param name="specialization">the map is updated with all the specializations
    /// (mapping formal type arguments to actuals)</param>
    [Pure]
    bool IsSpecialized(Method method, ref IFunctionalMap<Type, Type> specialization);

    /// <summary>
    /// Returns true if this method reference is specialized w.r.t. type parameters, either
    /// because the method is generic and instantiated, or because a declaring/parent type is
    /// instantiated.
    /// </summary>
    [Pure]
    bool IsSpecialized(Method method, out Method genericMethod, out IIndexable<Type> methodTypeArguments);

    /// <summary>
    /// Returns true if method is a template method with formal type arguments. 
    /// NOTE: only the method type parameter of the given method are reported, no type parameters of
    /// enclosing types.
    /// </summary>
    [Pure]
    bool IsGeneric(Method method, out IIndexable<Type> formals);

    /// <summary>
    /// For specialized methods, this returns the original template method, otherwise identity
    /// </summary>
    [Pure]
    Method Unspecialized(Method method);

    /// <summary>
    /// Specializes a generic method with the given type arguments
    /// </summary>
    [Pure]
    Method Specialize(Method method, Type[] methodTypeArguments);

    /// <summary>
    /// Returns the set of methods that are immediately overridden from a base class (no intermediate override)
    /// </summary>
    [Pure]
    IEnumerable<Method> OverriddenMethods(Method method);

    /// <summary>
    /// Returns the set of methods that are interface methods implemented explicitly or implicitly by this method
    /// </summary>
    [Pure]
    IEnumerable<Method> ImplementedMethods(Method method);

    /// <summary>
    /// Returns the set of methods that are either
    ///  - immediately overridden from a base class (no intermediate override)
    ///  - interface methods implemented explicitly or implicitly by this method 
    /// </summary>
    [Pure]
    IEnumerable<Method> OverriddenAndImplementedMethods(Method method);

    /// <summary>
    /// Returns true if method implements a method from an interface implicitly (as opposed to explict implementations)
    /// </summary>
    [Pure]
    bool IsImplicitImplementation(Method method);

    /// <summary>
    /// Returns true if the method has a body and Entry(method) can be called.
    /// </summary>
    [Pure]
    bool HasBody(Method method);

    /// <summary>
    /// Provides access to a method body (if HasBody returned true), existentially abstracting the
    /// internal code representation of Labels and Handlers.
    /// </summary>
    [Pure]
    Result AccessMethodBody<Data, Result>(Method method, IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data);

    /// <summary>
    /// Equality between methods
    /// </summary>
    [Pure]
    bool Equal(Method m1, Method m2);

    /// <summary>
    /// Returns true if two types, type1 and type2, are equivalent types, as interpreted by the underlying 
    /// object model. 
    /// </summary>
    /// <param name="type1"></param>
    /// <param name="type2"></param>
    /// <returns></returns>
    [Pure]
    bool Equal(Type type1, Type type2);

    /// <summary>
    /// Returns true if any context that can see m2 can also see m1
    /// </summary>
    [Pure]
    bool IsAsVisibleAs(Method m1, Method m2);

    /// <summary>
    /// Returns true if any context that can see m can also see t
    /// </summary>
    [Pure]
    bool IsAsVisibleAs(Type t, Method m);

    /// <summary>
    /// Returns true if m can be seen from the type t
    /// </summary>
    [Pure]
    bool IsVisibleFrom(Method m, Type t);

    /// <summary>
    /// Returns true if f can be seen from the type t
    /// </summary>
    [Pure]
    bool IsVisibleFrom(Field f, Type t);

    /// <summary>
    /// Returns true if t can be seen from the body of tfrom
    /// </summary>
    [Pure]
    bool IsVisibleFrom(Type t, Type tfrom);


    /// <summary>
    /// Tries to find the method that implements baseMethod for type. This is either an
    /// explicit implementation in type, an implicit method in type, or an implicit or explicit
    /// implementation in the base-type of type.
    /// </summary>
    [Pure]
    bool TryGetImplementingMethod(Type type, Method baseMethod, out Method implementingMethod);

    /// <summary>
    /// Returns the locals of a given method
    /// </summary>
    [Pure]
    IIndexable<Local> Locals(Method method);

    /// <summary>
    /// If method is overriding another base method, then this returns the root method of the inheritance 
    /// hierarchy.
    /// </summary>
    bool TryGetRootMethod(Method method, out Method rootMethod);


    #endregion

    #region Fields

    /// <summary>
    /// Returns the type of the field
    /// </summary>
    [Pure]
    Type FieldType(Field field);

    /// <summary>
    /// Returns the full name of the field including surrounding types and namespaces
    /// </summary>
    [Pure]
    string FullName(Field field);

    /// <summary>
    /// Returns the short name of a field
    /// </summary>
    [Pure]
    string Name(Field field);

    /// <summary>
    /// Obtain the Documentation Id for the method
    /// </summary>
    [Pure]
    string DocumentationId(Field field);

    /// <summary>
    /// True if the field is static
    /// </summary>
    [Pure]
    bool IsStatic(Field field);

    /// <summary>
    /// Returns true if field is private
    /// </summary>
    [Pure]
    bool IsPrivate(Field field);

    /// <summary>
    /// Returns true if field is protected
    /// </summary>
    [Pure]
    bool IsProtected(Field field);

    /// <summary>
    /// Returns true if field is public
    /// </summary>
    [Pure]
    bool IsPublic(Field field);

    /// <summary>
    /// true if field is internal
    /// </summary>
    [Pure]
    bool IsInternal(Field field);

    /// <summary>
    /// true if field is volatile
    /// </summary>
    [Pure]
    bool IsVolatile(Field field);

    /// <summary>
    /// true if field is declared as "new"
    /// </summary>
    [Pure]
    bool IsNewSlot(Field field);

    /// <summary>
    /// Returns the declaring type containing this field
    /// </summary>
    [Pure]
    Type DeclaringType(Field field);

    /// <summary>
    /// Returns true if the field is declared readonly
    /// </summary>
    [Pure]
    bool IsReadonly(Field field);

    /// <summary>
    /// Returns true if any context that can see method can also see field.
    /// </summary>
    [Pure]
    bool IsAsVisibleAs(Field field, Method method);

    /// <summary>
    /// Returns true if this field reference is specialized w.r.t. type parameters, 
    /// because the a declaring/parent type is instantiated.
    /// </summary>
    [Pure]
    bool IsSpecialized(Field field);

    /// <summary>
    /// Returns the unspecialized field corresponding to this field, if the field
    /// is specialized due to parent types being instantiated. Acts as an identity
    /// for non-specialized fields.
    /// </summary>
    [Pure]
    Field Unspecialized(Field field);

    /// <summary>
    /// Returns the initial value of the field. Note: if field is an enum field, returns its value.
    /// </summary>
    [Pure]
    bool TryInitialValue(Field field, out object value);

    /// <summary>
    /// Returns if the two fields are equal
    /// </summary>
    [Pure]
    bool Equal(Field f1, Field f2);

    /// <summary>
    /// Return true if the field is compiler generated
    /// </summary>
    [Pure]
    bool IsCompilerGenerated(Field f);

    #endregion

    #region Properties
    [Pure]
    string Name(Property property);

    [Pure]
    bool HasGetter(Property property, out Method getter);
    [Pure]
    bool HasSetter(Property property, out Method setter);

    [Pure]
    IEnumerable<Property> Properties(Type type);

    [Pure]
    bool IsStatic(Property p);
    [Pure]
    bool IsOverride(Property p);
    [Pure]
    bool IsNewSlot(Property p);
    [Pure]
    bool IsSealed(Property p);

    [Pure]
    Type DeclaringType(Property p);
    [Pure]
    Type PropertyType(Property property);

    /// <summary>
    /// Returns if the two properties are the same
    /// </summary>
    [Pure]
    bool Equal(Property p1, Property p2);

    #endregion

    #region Events
    [Pure]
    string Name(Event @event);

    [Pure]
    bool HasAdder(Event @event, out Method adder);
    [Pure]
    bool HasRemover(Event @event, out Method remover);

    [Pure]
    IEnumerable<Event> Events(Type type);

    [Pure]
    bool IsStatic(Event e);
    [Pure]
    bool IsOverride(Event e);
    [Pure]
    bool IsNewSlot(Event e);
    [Pure]
    bool IsSealed(Event e);

    [Pure]
    Type DeclaringType(Event e);
    [Pure]
    Type HandlerType(Event e);

    /// <summary>
    /// Returns if the two events are the same
    /// </summary>
    [Pure]
    bool Equal(Event e1, Event e2);

    bool IsEventAdder(Method method, out Event @event);

    bool IsEventRemover(Method method, out Event @event);

    #endregion

    #region Types

    /// <summary>
    /// True if this is the framework System.Void type, ignoring surrounding optional/required modifiers
    /// </summary>
    [Pure]
    bool IsVoid(Type/*!*/ type);

    /// <summary>
    /// True if this is an unmanaged pointer, ignoring surrounding optional/required modifiers
    /// </summary>
    [Pure]
    bool IsUnmanagedPointer(Type/*!*/ type);   // * type

    /// <summary>
    /// True if this is a managed pointer, ignoring surrounding optional/required modifiers
    /// </summary>
    [Pure]
    bool IsManagedPointer(Type/*!*/ type);     // & type

    /// <summary>
    /// True if this is a primitive type, ignoring surrounding optional/required modifiers
    /// </summary>
    [Pure]
    bool IsPrimitive(Type/*!*/ type);

    /// <summary>
    /// True if this is a struct/value type type, ignoring surrounding optional/required modifiers
    /// </summary>
    //^ [Confined]
    [Pure]
    bool IsStruct(Type/*!*/ type);

    /// <summary>
    /// True if this is an array type, ignoring surrounding optional/required modifiers
    /// </summary>
    //^ [Confined]
    [Pure]
    bool IsArray(Type/*!*/ type);

    /// <summary>
    /// Returns the number of dimensions if the type is an array.
    /// Requires IsArray
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Pure]
    int Rank(Type type);

    /// <summary>
    /// Returns true if type is an interface
    /// </summary>
    [Pure]
    bool IsInterface(Type/*!*/ type);

    /// <summary>
    /// Returns true if type is static
    /// </summary>
    [Pure]
    bool IsStatic(Type type);

    /// <summary>
    /// Returns true if type is a class
    /// </summary>
    [Pure]
    bool IsClass(Type/*!*/ type);

    /// <summary>
    /// Returns true if type is declared abstract
    /// </summary>
    [Pure]
    bool IsAbstract(Type type);

    /// <summary>
    /// Returns true if type is declared public
    /// </summary>
    [Pure]
    bool IsPublic(Type type);

    /// <summary>
    /// Returns true if type is declared internal
    /// </summary>
    [Pure]
    bool IsInternal(Type type);

    /// <summary>
    /// Returns true if type is declared protected
    /// </summary>
    [Pure]
    bool IsProtected(Type type);

    /// <summary>
    /// Returns true if type is private
    /// </summary>
    [Pure]
    bool IsPrivate(Type type);

    /// <summary>
    /// Returns true if type is sealed
    /// </summary>
    [Pure]
    bool IsSealed(Type type);

    /// <summary>
    /// Returns the parent type and true if the given type is nested.
    /// </summary>
    [Pure]
    bool IsNested(Type/*!*/ type, out Type/*?*/ parentType);

    /// <summary>
    /// Returns true if the formal generic argument is reference constrained. Either class or method formal argument.
    /// </summary>
    [Pure]
    bool IsReferenceConstrained(Type type);

    /// <summary>
    /// Returns true if the formal generic argument is value constrained. Either class or method formal argument.
    /// </summary>
    [Pure]
    bool IsValueConstrained(Type type);

    /// <summary>
    /// Returns true if the formal generic argument is constructor constrained. Either class or method formal argument.
    /// </summary>
    [Pure]
    bool IsConstructorConstrained(Type type);


    /// <summary>
    /// Returns true if the given type is a type bound formal type parameter
    /// </summary>
    [Pure]
    bool IsFormalTypeParameter(Type type);

    /// <summary>
    /// Returns true if the given type is a method bound formal type parameter
    /// </summary>
    [Pure]
    bool IsMethodFormalTypeParameter(Type type);

    /// <summary>
    /// Returns true if type represents a modified type with the given set of 
    /// optional and required modifiers. The pairings of the modifiers with a boolean indicates 
    /// if the modifier is optional (bool is true), or required (bool is false).
    /// </summary>
    [Pure]
    bool IsModified(Type type, out Type modified, out IIndexable<Pair<bool, Type>> modifiers);

    /// <summary>
    /// Returns true if type is a template type with formal type arguments. NOTE: if normalized is true,
    /// this provides the IL/Metadata view where nested types inherit the type formals of their enclosing types.
    /// Otherwise, only the type parameter of the given type are reported.
    /// </summary>
    [Pure]
    bool IsGeneric(Type type, out IIndexable<Type> formals, bool normalized);

    /// <summary>
    /// Returns true if the type is a delegate
    /// </summary>
    [Pure]
    bool IsDelegate(Type type);

    /// <summary>
    /// For type bound type parameters, provides the normalized index of the type parameter.
    /// For nested types, they are normalized to include all parent type parameters.
    /// </summary>
    [Pure]
    int NormalizedFormalTypeParameterIndex(Type type);

    /// <summary>
    /// For type bound type parameters, provides the defined type of the type parameter
    /// </summary>
    [Pure]
    Type FormalTypeParameterDefiningType(Type type);

    /// <summary>
    /// Returns the actual type arguments of type (including parent type arguments).
    /// If type isn't an instance, result is of length 0.
    /// </summary>
    [Pure]
    IIndexable<Type> NormalizedActualTypeArguments(Type type);

    /// <summary>
    /// Returns the actual type arguments of the method (NOT including declaring type or parent types).
    /// If method isn't a generic method instance, result is of length 0.
    /// </summary>
    [Pure]
    IIndexable<Type> ActualTypeArguments(Method method);

    /// <summary>
    /// For method bound formal type parameters, this method returns the index of the type parameter on the method.
    /// </summary>
    [Pure]
    int MethodFormalTypeParameterIndex(Type type);

    /// <summary>
    /// For method bound formal type parameters, this method returns the method defining the type parameter
    /// </summary>
    [Pure]
    Method MethodFormalTypeDefiningMethod(Type type);

    /// <summary>
    /// Returns true if type represents an enum
    /// </summary>
    [Pure]
    bool IsEnum(Type type);

    /// <summary>
    /// Returns true if the Flags attribute is specified for that type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Pure]
    bool HasFlagsAttribute(Type type);

    /// <summary>
    /// Returns the underlying type if type is an enum
    /// </summary>
    [Pure]
    Type TypeEnum(Type type);

    /// <summary>
    /// Returns the MVID of the declaring module (if nested type, of the declaring module of the container)
    /// </summary>
    [Pure]
    System.Guid DeclaringModule(Type/*!*/ type);


    /// <summary>
    /// Returns the module name defining this type (if nested, the declaring module of the container)
    /// </summary>
    [Pure]
    string DeclaringModuleName(Type type);

    /// <summary>
    /// Returns the fields of a type
    /// </summary>
    [Pure]
    IEnumerable<Field/*!*/> Fields(Type/*!*/ type);

    /// <summary>
    /// Returns the methods of a type
    /// </summary>
    [Pure]
    IEnumerable<Method> Methods(Type type);

    /// <summary>
    /// Returns the nested types of a type
    /// </summary>
    [Pure]
    IEnumerable<Type> NestedTypes(Type type);

    /// <summary>
    /// Returns the short name of the type
    /// </summary>
    [Pure]
    string Name(Type type);

    /// <summary>
    /// Returns the full name including surrounding types and namespaces
    /// </summary>
    [Pure]
    string FullName(Type/*!*/ type);


    /// <summary>
    /// Obtain the Documentation Id for the method
    /// </summary>
    [Pure]
    string DocumentationId(Type type);
    

    /// <summary>
    /// Returns the namespace of the type (if not a nested type)
    /// </summary>
    [Pure]
    string Namespace(Type type);

    /// <summary>
    /// Constructs an array type with the given element type and rank
    /// </summary>
    [Pure]
    Type/*!*/ ArrayType(Type/*!*/ type, int rank);  // type[]...

    /// <summary>
    /// Constructs a managed pointer type with the given element type
    /// </summary>
    [Pure]
    Type/*!*/ ManagedPointer(Type/*!*/ type);       // type&

    /// <summary>
    /// Constructs an unmanaged pointer type with the given element type
    /// </summary>
    [Pure]
    Type/*!*/ UnmanagedPointer(Type/*!*/ type);       // type&

    /// <summary>
    /// For managed and unmanaged pointers and arrays, returns the type pointed to (in the array)
    /// </summary>
    [Pure]
    Type/*!*/ ElementType(Type/*!*/ type);

    /// <summary>
    /// If the type has explicit layout information, it returns the size of the type. Otherwise -1.
    /// </summary>
    [Pure]
    int TypeSize(Type/*!*/ type);

    /// <summary>
    /// Returns true if the type is a class and has a base class (e.g., not Object)
    /// </summary>
    [Pure]
    bool HasBaseClass(Type type);

    /// <summary>
    /// Returns the base class of the given type, or fails if the type has no base class
    /// </summary>
    [Pure]
    Type BaseClass(Type type);

    /// <summary>
    /// If type is a class or struct, returns the implemented interfaces
    /// </summary>
    [Pure]
    IEnumerable<Type> Interfaces(Type type);

    /// <summary>
    /// If type is a type or method parameter, return the list of type constraints
    /// </summary>
    [Pure]
    IEnumerable<Type> TypeParameterConstraints(Type type);

    /// <summary>
    /// Returns true if the type reference is a specialized type with type arguments.
    /// NOTE: if this is a nested type, it will return true even if only parent types are specialized.
    ///       In that case, typeArguments returned might be of lenth 0.
    /// </summary>
    [Pure]
    bool IsSpecialized(Type type, out IIndexable<Type> typeArguments);

    /// <summary>
    /// Returns true if the type reference is a specialized type with type arguments.
    /// NOTE: if this is a nested type, it will return true even if only parent types are specialized.
    ///       In that case, typeArguments will inherit the type arguments of the parent types
    /// </summary>
    [Pure]
    bool NormalizedIsSpecialized(Type type, out IIndexable<Type> typeArguments);

    /// <summary>
    /// For instantiated types, this returns the original template type
    /// </summary>
    [Pure]
    Type Unspecialized(Type type);

    /// <summary>
    /// Returns true if type is definetely represented as a reference (pointer),
    /// not a value type.
    /// </summary>
    [Pure]
    bool IsReferenceType(Type type);

    /// <summary>
    /// Returns true if type is uintptr, intptr, or X*
    /// </summary>
    [Pure]
    bool IsNativePointerType(Type declaringType);

    /// <summary>
    /// Returns true if sub derives from super directly or indirectly
    /// </summary>
    [Pure]
    bool DerivesFrom(Type sub, Type super);

    /// <summary>
    /// For non-generic types this is exactly the same as DerivesFrom.
    /// For generic-types, it checks whether the template of sub derives
    /// from the template of super, without considering their type
    /// arguments.
    /// This method is needed only because inherited contracts are not specialized
    /// into their inheriting context. If they were, then DerivesFrom would
    /// be exactly the right test and this method should be removed.
    /// </summary>
    [Pure]
    bool DerivesFromIgnoringTypeArguments(Type sub, Type super);

    /// <summary>
    /// Returns the number of (non-static) constructors in this specific type
    /// </summary>
    [Pure]
    int ConstructorsCount(Type type);

    /// <summary>
    /// Returns true if type is visible outside assembly
    /// </summary>
    [Pure]
    bool IsVisibleOutsideAssembly(Type type);

    /// <summary>
    /// Specializes a generic type with the given type arguments. This should work for normalized specializations too.
    /// </summary>
    [Pure]
    Type Specialize(Type type, Type[] typeArguments);

    #endregion

    #region Modules

    /// <summary>
    /// Loads the given assembly loaded from file.
    /// The assemblyCache is a hack to give the code provider a chance to reuse the same assembly if referenced
    /// multiple times and it also contains contract assemblies for out-of-band contracts.
    /// </summary>
    /// <returns>false if assembly cannot be loaded.</returns>
    [Pure]
    bool TryLoadAssembly(string fileName, System.Collections.IDictionary assemblyCache, Action<System.CodeDom.Compiler.CompilerError> errorHandler, out Assembly assembly, bool legacyContractMode, List<string> referencedAssemblies, bool extractSourceText);

    /// <summary>
    /// Returns all (and only) the top-level types in the given assembly.
    /// </summary>
    [Pure]
    IEnumerable<Type> GetTypes(Assembly assembly);

    /// <summary>
    /// Returns the set of assembly references of an assembly
    /// </summary>
    [Pure]
    IEnumerable<Assembly> AssemblyReferences(Assembly assembly);

    /// <summary>
    /// Returns the string representing the short name of the assembly
    /// </summary>
    [Pure]
    string Name(Assembly assembly);

    /// <summary>
    /// Returns the version of the assembly
    /// </summary>
    [Pure]
    Version Version(Assembly assembly);

    [Pure]
    Guid AssemblyGuid(Assembly assembly);
    #endregion

    #region Well-known types (alphabetical)

    Type/*!*/ System_Array { get; }
    Type/*!*/ System_Boolean { get; }
    Type/*!*/ System_Char { get; }
    Type/*!*/ System_DynamicallyTypedReference { get; }
    Type/*!*/ System_Double { get; }
    Type/*!*/ System_Decimal { get; }
    Type/*!*/ System_Int8 { get; }
    Type/*!*/ System_Int16 { get; }
    Type/*!*/ System_Int32 { get; }
    Type/*!*/ System_Int64 { get; }
    Type/*!*/ System_IntPtr { get; }
    Type/*!*/ System_Object { get; }
    Type/*!*/ System_RuntimeArgumentHandle { get; }
    Type/*!*/ System_RuntimeFieldHandle { get; }
    Type/*!*/ System_RuntimeMethodHandle { get; }
    Type/*!*/ System_RuntimeTypeHandle { get; }
    Type/*!*/ System_Single { get; }
    Type/*!*/ System_String { get; }
    Type/*!*/ System_Type { get; }
    Type/*!*/ System_UInt8 { get; }
    Type/*!*/ System_UInt16 { get; }
    Type/*!*/ System_UInt32 { get; }
    Type/*!*/ System_UInt64 { get; }
    Type/*!*/ System_UIntPtr { get; }
    Type/*!*/ System_Void { get; }

    [Pure]
    bool TryGetSystemType(string fullName, out Type type);

    #endregion

    #region Parameter

    /// <summary>
    /// Returns the name of the given parameter
    /// </summary>
    [Pure]
    string Name(Parameter param);

    /// <summary>
    /// Returns the type of the parameter
    /// </summary>
    [Pure]
    Type ParameterType(Parameter p);

    /// <summary>
    /// True if the parameter was declared as an "out" parameter
    /// </summary>
    [Pure]
    bool IsOut(Parameter p);

    /// <summary>
    /// Return the argument index as used in the IL.
    /// </summary>
    [Pure]
    int ArgumentIndex(Parameter p);

    /// <summary>
    /// Return the offset on the stack of this parameter at a call site.
    /// The offset is essentially #method-args + (IsStatic?0:1) - argumentIndex
    /// For example: a method with a "this" parameter and another parameter x would
    /// have offset 0 for x and offset 1 for this.
    /// </summary>
    [Pure]
    int ArgumentStackIndex(Parameter p);

    /// <summary>
    /// Returns the method declaring this parameter.
    /// </summary>
    [Pure]
    Method DeclaringMethod(Parameter p);

    #endregion

    #region Local

    /// <summary>
    /// Returns the name of the local
    /// </summary>
    [Pure]
    string Name(Local local);

    /// <summary>
    /// Returns the declared type of the local
    /// </summary>
    [Pure]
    Type LocalType(Local local);

    /// <summary>
    /// Is the local a pinned variable?
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    [Pure]
    bool IsPinned(Local local);

    #endregion

    #region Attributes
    [Pure]
    IEnumerable<Attribute> GetAttributes(Type type);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Method method);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Field field);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Property field);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Event @event);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Assembly assembly);

    [Pure]
    IEnumerable<Attribute> GetAttributes(Parameter parameter);

    [Pure]
    Type AttributeType(Attribute attribute);

    [Pure]
    Method AttributeConstructor(Attribute attribute);

    [Pure]
    IIndexable<object> PositionalArguments(Attribute attribute);

    [Pure]
    object NamedArgument(string name, Attribute attribute);

    #endregion

    #region Platform

    bool IsPlatformInitialized { get; }
    /// <summary>
    /// Set the platform and search paths for assembly resolution.
    /// </summary>
    void SetTargetPlatform(string framework, System.Collections.IDictionary assemblyCache, string platform, IEnumerable<string> resolved, IEnumerable<string> libPaths, Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool trace);

    /// <summary>
    /// To override default Microsoft.Contracts.dll or mscorlib or current. Needs to be done prior to SetTargetPlatform
    /// </summary>
    string SharedContractClassAssembly
    {
      get;
      set;
    }
    #endregion


  }

  #region Contracts for IDecodeMetaData
  [ContractClassFor(typeof(IDecodeMetaData<,,,,,,,,>))]
  abstract class IDecodeMetaDataContracts<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    : IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    #region IDecodeMetaData<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly> Members

    public Type ReturnType(Method method)
    {
      throw new NotImplementedException();
    }

    public IIndexable<Parameter> Parameters(Method method)
    {
      Contract.Ensures(Contract.Result<IIndexable<Parameter>>() != null);
      throw new NotImplementedException();
    }

    public Parameter This(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleOutsideAssembly(Property property)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleOutsideAssembly(Event @event)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleOutsideAssembly(Field field)
    {
      throw new NotImplementedException();
    }


    [Pure]
    public Property GetPropertyFromAccessor(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsCompilerGenerated(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsDebuggerNonUserCode(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsDebuggerNonUserCode(Type t)
    {
      throw new NotImplementedException();
    }

    public bool IsAutoPropertyMember(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsAutoPropertySetter(Method method, out Field backingField)
    {
      throw new NotImplementedException();
    }

    public bool IsCompilerGenerated(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsNativeCpp(Type type)
    {
      throw new NotImplementedException();
    }

    public Type DeclaringType(Method method)
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      throw new NotImplementedException();
    }

    public Assembly DeclaringAssembly(Method method)
    {
      Contract.Ensures(Contract.Result<Assembly>() != null);

      throw new NotImplementedException();
    }

    public StackTemp MethodToken(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsSpecialized(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsSpecialized(Method method, ref IFunctionalMap<Type, Type> specialization)
    {
      Contract.Requires(specialization != null);

      throw new NotImplementedException();
    }

    public bool IsSpecialized(Method method, out Method genericMethod, out IIndexable<Type> methodTypeArguments)
    {
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out methodTypeArguments) != null));
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out genericMethod) != null));

      throw new NotImplementedException();
    }

    public bool IsGeneric(Method method, out IIndexable<Type> formals)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out formals) != null);
      throw new NotImplementedException();
    }

    public Method Unspecialized(Method method)
    {
      throw new NotImplementedException();
    }

    public Method Specialize(Method method, Type[] methodTypeArguments)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Method> OverriddenMethods(Method method)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

      return null;
    }

    public IEnumerable<Method> ImplementedMethods(Method method)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

      return null;
    }

    public IEnumerable<Method> OverriddenAndImplementedMethods(Method method)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

      return null;
    }

    public bool IsImplicitImplementation(Method method)
    {
      throw new NotImplementedException();
    }

    public bool HasBody(Method method)
    {
      throw new NotImplementedException();
    }

    public Result AccessMethodBody<Data, Result>(Method method, IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    public bool Equal(Method m1, Method m2)
    {
      throw new NotImplementedException();
    }

    public bool Equal(Type type1, Type type2)
    {
      throw new NotImplementedException();
    }

    public bool IsAsVisibleAs(Method m1, Method m2)
    {
      throw new NotImplementedException();
    }

    public bool IsAsVisibleAs(Type t, Method m)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleFrom(Method m, Type t)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleFrom(Field f, Type t)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleFrom(Type t, Type tfrom)
    {
      throw new NotImplementedException();
    }

    public bool TryGetImplementingMethod(Type type, Method baseMethod, out Method implementingMethod)
    {
      throw new NotImplementedException();
    }

    public IIndexable<Local> Locals(Method method)
    {
      Contract.Ensures(Contract.Result<IIndexable<Local>>() != null);
      throw new NotImplementedException();
    }

    public bool TryGetRootMethod(Method method, out Method rootMethod)
    {
      throw new NotImplementedException();
    }

    public Type FieldType(Field field)
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      throw new NotImplementedException();
    }

    public string FullName(Field field)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public string Name(Field field)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public bool IsStatic(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsPrivate(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsProtected(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsPublic(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsInternal(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsVolatile(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsNewSlot(Field field)
    {
      throw new NotImplementedException();
    }

    public Type DeclaringType(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsReadonly(Field field)
    {
      throw new NotImplementedException();
    }

    public bool IsAsVisibleAs(Field field, Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsSpecialized(Field field)
    {
      throw new NotImplementedException();
    }

    public Field Unspecialized(Field field)
    {
      throw new NotImplementedException();
    }


    public bool Equal(Field f1, Field f2)
    {
      throw new NotImplementedException();
    }

    public bool IsCompilerGenerated(Field f)
    {
      throw new NotImplementedException();
    }

    public string Name(Property property)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public bool HasGetter(Property property, out Method getter)
    {
      throw new NotImplementedException();
    }

    public bool HasSetter(Property property, out Method setter)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Property> Properties(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsStatic(Property p)
    {
      throw new NotImplementedException();
    }

    public bool IsOverride(Property p)
    {
      throw new NotImplementedException();
    }

    public bool IsNewSlot(Property p)
    {
      throw new NotImplementedException();
    }

    public bool IsSealed(Property p)
    {
      throw new NotImplementedException();
    }

    public Type DeclaringType(Property p)
    {
      throw new NotImplementedException();
    }

    public Type PropertyType(Property property)
    {
      throw new NotImplementedException();
    }

    public bool Equal(Property p1, Property p2)
    {
      throw new NotImplementedException();
    }

    public string Name(Event @event)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public bool HasAdder(Event @event, out Method adder)
    {
      throw new NotImplementedException();
    }

    public bool HasRemover(Event @event, out Method remover)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Event> Events(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsStatic(Event e)
    {
      throw new NotImplementedException();
    }

    public bool IsOverride(Event e)
    {
      throw new NotImplementedException();
    }

    public bool IsNewSlot(Event e)
    {
      throw new NotImplementedException();
    }

    public bool IsSealed(Event e)
    {
      throw new NotImplementedException();
    }

    public Type DeclaringType(Event e)
    {
      throw new NotImplementedException();
    }

    public Type HandlerType(Event e)
    {
      throw new NotImplementedException();
    }

    public bool Equal(Event e1, Event e2)
    {
      throw new NotImplementedException();
    }

    public bool IsEventAdder(Method method, out Event @event)
    {
      throw new NotImplementedException();
    }

    public bool IsEventRemover(Method method, out Event @event)
    {
      throw new NotImplementedException();
    }

    public bool IsVoid(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsUnmanagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsManagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsPrimitive(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsStruct(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsArray(Type type)
    {
      throw new NotImplementedException();
    }

    public StackTemp Rank(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsInterface(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsStatic(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsClass(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsAbstract(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsPublic(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsInternal(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsProtected(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsPrivate(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsSealed(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsNested(Type type, out Type parentType)
    {
      throw new NotImplementedException();
    }

    public bool IsReferenceConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsValueConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsConstructorConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsFormalTypeParameter(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsMethodFormalTypeParameter(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsModified(Type type, out Type modified, out IIndexable<Pair<bool, Type>> modifiers)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out modifiers) != null);
      throw new NotImplementedException();
    }

    public bool IsGeneric(Type type, out IIndexable<Type> formals, bool normalized)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out formals) != null);
      throw new NotImplementedException();
    }

    public bool IsDelegate(Type type)
    {
      throw new NotImplementedException();
    }

    public int NormalizedFormalTypeParameterIndex(Type type)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      throw new NotImplementedException();
    }

    public Type FormalTypeParameterDefiningType(Type type)
    {
      Contract.Ensures(Contract.Result<Type>() != null);
      throw new NotImplementedException();
    }

    public IIndexable<Type> NormalizedActualTypeArguments(Type type)
    {
      Contract.Ensures(Contract.Result<IIndexable<Type>>() != null);
      throw new NotImplementedException();
    }

    public IIndexable<Type> ActualTypeArguments(Method method)
    {
      Contract.Ensures(Contract.Result<IIndexable<Type>>() != null);
      throw new NotImplementedException();
    }

    public int MethodFormalTypeParameterIndex(Type type)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      throw new NotImplementedException();
    }

    public Method MethodFormalTypeDefiningMethod(Type type)
    {
      Contract.Ensures(Contract.Result<Method>() != null);
      throw new NotImplementedException();
    }

    public bool IsEnum(Type type)
    {
      throw new NotImplementedException();
    }

    public bool HasFlagsAttribute(Type type)
    {
      throw new NotImplementedException();
    }

    public Type TypeEnum(Type type)
    {
      throw new NotImplementedException();
    }

    public Guid DeclaringModule(Type type)
    {
      throw new NotImplementedException();
    }

    public string DeclaringModuleName(Type type)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Field> Fields(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Field>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Method> Methods(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Type> NestedTypes(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);
      throw new NotImplementedException();
    }

    public string Name(Type type)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public string FullName(Type type)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public string Namespace(Type type)
    {
      throw new NotImplementedException();
    }

    public Type ArrayType(Type type, StackTemp rank)
    {
      throw new NotImplementedException();
    }

    public Type ManagedPointer(Type type)
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      throw new NotImplementedException();
    }

    public Type UnmanagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    public Type ElementType(Type type)
    {
      throw new NotImplementedException();
    }

    public StackTemp TypeSize(Type type)
    {
      throw new NotImplementedException();
    }

    public bool HasBaseClass(Type type)
    {
      throw new NotImplementedException();
    }

    public Type BaseClass(Type type)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Type> Interfaces(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Type> TypeParameterConstraints(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);
      throw new NotImplementedException();
    }

    public bool IsSpecialized(Type type, out IIndexable<Type> typeArguments)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out typeArguments) != null);
      throw new NotImplementedException();
    }

    public bool NormalizedIsSpecialized(Type type, out IIndexable<Type> typeArguments)
    {
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out typeArguments) != null && Contract.ValueAtReturn(out typeArguments).Count > 0));
      throw new NotImplementedException();
    }

    public Type Unspecialized(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsReferenceType(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsNativePointerType(Type declaringType)
    {
      throw new NotImplementedException();
    }

    public bool DerivesFrom(Type sub, Type super)
    {
      throw new NotImplementedException();
    }

    public bool DerivesFromIgnoringTypeArguments(Type sub, Type super)
    {
      throw new NotImplementedException();
    }

    public StackTemp ConstructorsCount(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsVisibleOutsideAssembly(Type type)
    {
      throw new NotImplementedException();
    }

    public Type Specialize(Type type, Type[] typeArguments)
    {
      throw new NotImplementedException();
    }

    public bool TryLoadAssembly(string fileName, System.Collections.IDictionary assemblyCache, Action<System.CodeDom.Compiler.CompilerError> errorHandler, out Assembly assembly, bool legacyContractMode, List<string> referencedAssemblies, bool extractContractText)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Type> GetTypes(Assembly assembly)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Assembly> AssemblyReferences(Assembly assembly)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Assembly>>() != null);
      throw new NotImplementedException();
    }

    public string Name(Assembly assembly)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public Version Version(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    public Guid AssemblyGuid(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    public Type System_Array
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Boolean
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Char
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_DynamicallyTypedReference
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Double
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Decimal
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Int8
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Int16
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Int32
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Int64
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_IntPtr
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Object
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_RuntimeArgumentHandle
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_RuntimeFieldHandle
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_RuntimeMethodHandle
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_RuntimeTypeHandle
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Single
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_String
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Type
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_UInt8
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_UInt16
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_UInt32
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_UInt64
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_UIntPtr
    {
      get { throw new NotImplementedException(); }
    }

    public Type System_Void
    {
      get { throw new NotImplementedException(); }
    }

    public bool TryGetSystemType(string fullName, out Type type)
    {
      Contract.Ensures(Contract.ValueAtReturn(out type) != null || !Contract.Result<bool>());
      throw new NotImplementedException();
    }

    public string Name(Parameter param)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public Type ParameterType(Parameter p)
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      throw new NotImplementedException();
    }

    public bool IsOut(Parameter p)
    {
      throw new NotImplementedException();
    }

    public int ArgumentIndex(Parameter p)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      throw new NotImplementedException();
    }

    public StackTemp ArgumentStackIndex(Parameter p)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      throw new NotImplementedException();
    }

    public Method DeclaringMethod(Parameter p)
    {
      throw new NotImplementedException();
    }

    public string Name(Local local)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public Type LocalType(Local local)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Method method)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Field field)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Property field)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Event @event)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Assembly assembly)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public IEnumerable<Attribute> GetAttributes(Parameter parameter)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);

      throw new NotImplementedException();
    }

    public Type AttributeType(Attribute attribute)
    {
      throw new NotImplementedException();
    }

    public Method AttributeConstructor(Attribute attribute)
    {
      throw new NotImplementedException();
    }

    public IIndexable<object> PositionalArguments(Attribute attribute)
    {
      Contract.Ensures(Contract.Result<IIndexable<object>>() != null);
      throw new NotImplementedException();
    }

    public object NamedArgument(string name, Attribute attribute)
    {
      throw new NotImplementedException();
    }

    public bool IsPlatformInitialized { get { return false; } }

    public void SetTargetPlatform(string framework, System.Collections.IDictionary assemblyCache, string platform, IEnumerable<string> resolved, IEnumerable<string> libPaths, Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool trace)
    {
      Contract.Requires(assemblyCache != null);
      Contract.Requires(resolved != null);
      Contract.Requires(libPaths != null);
    }

    public string SharedContractClassAssembly
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public bool TryInitialValue(Field field, out object value)
    {
      //Contract.Requires(field != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);

      throw new NotImplementedException();
    }

    #endregion


    string IDecodeMethods<Method>.DocumentationId(Method method)
    {
      return null;
    }

    string IDecodeMethods<Method>.Name(Method method)
    {
      throw new NotImplementedException();
    }

    string IDecodeMethods<Method>.FullName(Method method)
    {
      throw new NotImplementedException();
    }

    string IDecodeMethods<Method>.DeclaringMemberCanonicalName(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsMain(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsStatic(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsPrivate(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsProtected(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsPublic(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsVirtual(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsNewSlot(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsOverride(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsSealed(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsVoidMethod(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsConstructor(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsFinalizer(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsDispose(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsInternal(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsVisibleOutsideAssembly(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsAbstract(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsExtern(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsPropertySetter(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMethods<Method>.IsPropertyGetter(Method method)
    {
      throw new NotImplementedException();
    }

    #region IDecodeMethods<Method> Members


    public bool IsAsyncMoveNext(Method method)
    {
      throw new NotImplementedException();
    }

    public StackTemp? MoveNextStartState(Method method)
    {
      throw new NotImplementedException();
    }

    #endregion

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ReturnType(Method method)
    {
      throw new NotImplementedException();
    }

    IIndexable<Parameter> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Parameters(Method method)
    {
      throw new NotImplementedException();
    }

    Parameter IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.This(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleOutsideAssembly(Property property)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleOutsideAssembly(Event @event)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleOutsideAssembly(Field field)
    {
      throw new NotImplementedException();
    }

    Property IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetPropertyFromAccessor(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsCompilerGenerated(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsDebuggerNonUserCode(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsDebuggerNonUserCode(Type t)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAutoPropertyMember(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAutoPropertySetter(Method method, out Field backingField)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsCompilerGenerated(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNativeCpp(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringType(Method method)
    {
      throw new NotImplementedException();
    }

    Assembly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringAssembly(Method method)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.MethodToken(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSpecialized(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSpecialized(Method method, ref IFunctionalMap<Type, Type> specialization)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSpecialized(Method method, out Method genericMethod, out IIndexable<Type> methodTypeArguments)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsGeneric(Method method, out IIndexable<Type> formals)
    {
      throw new NotImplementedException();
    }

    Method IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Unspecialized(Method method)
    {
      throw new NotImplementedException();
    }

    Method IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Specialize(Method method, Type[] methodTypeArguments)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Method> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.OverriddenMethods(Method method)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Method> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ImplementedMethods(Method method)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Method> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.OverriddenAndImplementedMethods(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsImplicitImplementation(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasBody(Method method)
    {
      throw new NotImplementedException();
    }

    Result IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.AccessMethodBody<Data, Result>(Method method, IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Equal(Method m1, Method m2)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Equal(Type type1, Type type2)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAsVisibleAs(Method m1, Method m2)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAsVisibleAs(Type t, Method m)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleFrom(Method m, Type t)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleFrom(Field f, Type t)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleFrom(Type t, Type tfrom)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TryGetImplementingMethod(Type type, Method baseMethod, out Method implementingMethod)
    {
      throw new NotImplementedException();
    }

    IIndexable<Local> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Locals(Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TryGetRootMethod(Method method, out Method rootMethod)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.FieldType(Field field)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.FullName(Field field)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsStatic(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPrivate(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsProtected(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPublic(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsInternal(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVolatile(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNewSlot(Field field)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringType(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsReadonly(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAsVisibleAs(Field field, Method method)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSpecialized(Field field)
    {
      throw new NotImplementedException();
    }

    Field IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Unspecialized(Field field)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TryInitialValue(Field field, out object value)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Equal(Field f1, Field f2)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsCompilerGenerated(Field f)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Property property)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasGetter(Property property, out Method getter)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasSetter(Property property, out Method setter)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Property> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Properties(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsStatic(Property p)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsOverride(Property p)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNewSlot(Property p)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSealed(Property p)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringType(Property p)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.PropertyType(Property property)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Equal(Property p1, Property p2)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Event @event)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasAdder(Event @event, out Method adder)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasRemover(Event @event, out Method remover)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Event> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Events(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsStatic(Event e)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsOverride(Event e)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNewSlot(Event e)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSealed(Event e)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringType(Event e)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HandlerType(Event e)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Equal(Event e1, Event e2)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsEventAdder(Method method, out Event @event)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsEventRemover(Method method, out Event @event)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVoid(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsUnmanagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsManagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPrimitive(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsStruct(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsArray(Type type)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Rank(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsInterface(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsStatic(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsClass(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsAbstract(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPublic(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsInternal(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsProtected(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPrivate(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSealed(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNested(Type type, out Type parentType)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsReferenceConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsValueConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsConstructorConstrained(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsFormalTypeParameter(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsMethodFormalTypeParameter(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsModified(Type type, out Type modified, out IIndexable<Pair<bool, Type>> modifiers)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsGeneric(Type type, out IIndexable<Type> formals, bool normalized)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsDelegate(Type type)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.NormalizedFormalTypeParameterIndex(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.FormalTypeParameterDefiningType(Type type)
    {
      throw new NotImplementedException();
    }

    IIndexable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.NormalizedActualTypeArguments(Type type)
    {
      throw new NotImplementedException();
    }

    IIndexable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ActualTypeArguments(Method method)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.MethodFormalTypeParameterIndex(Type type)
    {
      throw new NotImplementedException();
    }

    Method IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.MethodFormalTypeDefiningMethod(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsEnum(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasFlagsAttribute(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TypeEnum(Type type)
    {
      throw new NotImplementedException();
    }

    Guid IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringModule(Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringModuleName(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Field> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Fields(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Method> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Methods(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.NestedTypes(Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.FullName(Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DocumentationId(Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Namespace(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ArrayType(Type type, StackTemp rank)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ManagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.UnmanagedPointer(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ElementType(Type type)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TypeSize(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.HasBaseClass(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.BaseClass(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Interfaces(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TypeParameterConstraints(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsSpecialized(Type type, out IIndexable<Type> typeArguments)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.NormalizedIsSpecialized(Type type, out IIndexable<Type> typeArguments)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Unspecialized(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsReferenceType(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsNativePointerType(Type declaringType)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DerivesFrom(Type sub, Type super)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DerivesFromIgnoringTypeArguments(Type sub, Type super)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ConstructorsCount(Type type)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsVisibleOutsideAssembly(Type type)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Specialize(Type type, Type[] typeArguments)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TryLoadAssembly(string fileName, System.Collections.IDictionary assemblyCache, Action<System.CodeDom.Compiler.CompilerError> errorHandler, out Assembly assembly, bool legacyContractMode, List<string> referencedAssemblies, bool extractContractText)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Type> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetTypes(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Assembly> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.AssemblyReferences(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    Version IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Version(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    Guid IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.AssemblyGuid(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Array
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Boolean
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Char
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_DynamicallyTypedReference
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Double
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Decimal
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Int8
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Int16
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Int32
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Int64
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_IntPtr
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Object
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_RuntimeArgumentHandle
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_RuntimeFieldHandle
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_RuntimeMethodHandle
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_RuntimeTypeHandle
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Single
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_String
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Type
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_UInt8
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_UInt16
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_UInt32
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_UInt64
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_UIntPtr
    {
      get { throw new NotImplementedException(); }
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.System_Void
    {
      get { throw new NotImplementedException(); }
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.TryGetSystemType(string fullName, out Type type)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Parameter param)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ParameterType(Parameter p)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsOut(Parameter p)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ArgumentIndex(Parameter p)
    {
      throw new NotImplementedException();
    }

    StackTemp IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.ArgumentStackIndex(Parameter p)
    {
      throw new NotImplementedException();
    }

    Method IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DeclaringMethod(Parameter p)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Name(Local local)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.LocalType(Local local)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Type type)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Method method)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Field field)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Property field)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Event @event)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Attribute> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.GetAttributes(Parameter parameter)
    {
      throw new NotImplementedException();
    }

    Type IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.AttributeType(Attribute attribute)
    {
      throw new NotImplementedException();
    }

    Method IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.AttributeConstructor(Attribute attribute)
    {
      throw new NotImplementedException();
    }

    IIndexable<object> IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.PositionalArguments(Attribute attribute)
    {
      throw new NotImplementedException();
    }

    object IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.NamedArgument(string name, Attribute attribute)
    {
      throw new NotImplementedException();
    }

    bool IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.IsPlatformInitialized
    {
      get { throw new NotImplementedException(); }
    }

    void IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.SetTargetPlatform(string framework, System.Collections.IDictionary assemblyCache, string platform, IEnumerable<string> resolved, IEnumerable<string> libPaths, Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool trace)
    {
      throw new NotImplementedException();
    }

    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.SharedContractClassAssembly
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }


    bool IDecodeMethods<Method>.IsAsyncMoveNext(Method method)
    {
      throw new NotImplementedException();
    }

    StackTemp? IDecodeMethods<Method>.MoveNextStartState(Method method)
    {
      throw new NotImplementedException();
    }


    string IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.DocumentationId(Field field)
    {
      throw new NotImplementedException();
    }


    public bool IsPinned(Local local)
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  /// <summary>
  /// Access to contracts, encoded in whatever form
  /// </summary>
  [ContractClass(typeof(IDecodeContractsContracts<,,,,>))]
  public partial interface IDecodeContracts<Local, Parameter, Method, Field, Type>
  {

    #region Methods

    /// <summary>
    /// Should be informed by ContractVerification attribute and defaults
    /// </summary>
    bool VerifyMethod(Method method, bool analyzeNonUserCode, bool namespaceSelected, bool typeSelected, bool memberSelected);

    /// <summary>
    /// Do we have a source option specific for Clousot?
    /// </summary>
    bool HasOptionForClousot(Method method, string optionName, out string optionValue);

    /// <summary>
    /// Returns true if this method has explictit pre-conditions
    /// </summary>
    bool HasRequires(Method method);

    /// <summary>
    /// Provides access to the pre-conditions of a method, provided HasRequires returns true.
    /// </summary>
    Result AccessRequires<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data);

    /// <summary>
    /// Returns true if this method has explicit post-conditions
    /// </summary>
    bool HasEnsures(Method method);

    /// <summary>
    /// Provides access to the post-conditions of a method, provided HasEnsures returns true.
    /// </summary>
    Result AccessEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data);

    /// <summary>
    /// Returns true if this method has explicit model post-conditions
    /// </summary>
    bool HasModelEnsures(Method method);

    /// <summary>
    /// Provides access to the model post-conditions of a method, provided HasModelEnsures returns true.
    /// </summary>
    Result AccessModelEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data);

    /// <summary>
    /// Returns true if the declaring type of this method has an invariant observed by this method
    /// </summary>
    bool HasInvariant(Method method);


    /// <summary>
    /// Returns true, if method is pure (no visible side effects), and the result of the method
    /// depends only on data that does not change, even if the heap changes.
    /// E.g., Object.GetType, Array.get_Rank, etc.
    /// TODO TODO TODO: Implementations should probably cache this.
    /// </summary>
    bool IsMutableHeapIndependent(Method method);

    /// <summary>
    /// Returns true, if method is pure (no visible side effects).
    /// </summary>
    bool IsPure(Method method);

    /// <summary>
    /// True if method may inherit contracts from base methods or interface contract declarations.
    /// Can be controled with ContractOption("Contract", "Inheritance", true/false) attributes.
    /// </summary>
    bool CanInheritContracts(Method method);

    /// <summary>
    /// True if method is marked with ContractModel
    /// </summary>
    bool IsModel(Method method);

    /// <summary>
    /// Retrieves any model fields that might exist for this type.
    /// </summary>
    [Pure]
    IEnumerable<Field> ModelFields(Type type);

    /// <summary>
    /// Retrieves any model methods that might exist for this type.
    /// </summary>
    [Pure]
    IEnumerable<Method> ModelMethods(Type type);

    #endregion

    #region Types
    /// <summary>
    /// Returns true if the type has an invariant 
    /// </summary>
    bool HasInvariant(Type type);

    /// <summary>
    /// Provides access to the object invariant of a type, provided HasInvariant returns true.
    /// </summary>
    Result AccessInvariant<Data, Result>(Type type, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data);

    /// <summary>
    /// True if type may inherit contracts from base types or interfaces.
    /// Can be controled with ContractOption("Contract", "Inheritance", true/false) attributes.
    /// </summary>
    bool CanInheritContracts(Type type);

    /// <summary>
    /// True if the method returns a fresh object. Used in conjunction with Pure to tell
    /// static checker that the return value should not be cached.
    /// </summary>
    bool IsFreshResult(Method method);

    #endregion
  }

  #region Contracts for IDecodeContracts
  [ContractClassFor(typeof(IDecodeContracts<,,,,>))]
  abstract class IDecodeContractsContracts<Local, Parameter, Method, Field, Type>
    : IDecodeContracts<Local, Parameter, Method, Field, Type>
  {
    public bool VerifyMethod(Method method, bool analyzeNonUserCode, bool namespaceSelected, bool typeSelected, bool memberSelected)
    {
      throw new NotImplementedException();
    }

    public bool HasOptionForClousot(Method method, string optionName, out string optionValue)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out optionValue) != null);

      throw new NotImplementedException();
    }


    public bool HasRequires(Method method)
    {
      throw new NotImplementedException();
    }

    public Result AccessRequires<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    public bool HasEnsures(Method method)
    {
      throw new NotImplementedException();
    }

    public Result AccessEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    public bool HasModelEnsures(Method method)
    {
      throw new NotImplementedException();
    }

    public Result AccessModelEnsures<Data, Result>(Method method, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    public bool HasInvariant(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsPure(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsMutableHeapIndependent(Method method)
    {
      throw new NotImplementedException();
    }

    public bool CanInheritContracts(Method method)
    {
      throw new NotImplementedException();
    }

    public bool IsModel(Method method)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Field> ModelFields(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Field>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Method> ModelMethods(Type type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);
      throw new NotImplementedException();
    }

    public bool HasInvariant(Type type)
    {
      throw new NotImplementedException();
    }

    public Result AccessInvariant<Data, Result>(Type type, ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> consumer, Data data)
    {
      throw new NotImplementedException();
    }

    public bool CanInheritContracts(Type type)
    {
      throw new NotImplementedException();
    }

    public bool IsFreshResult(Method method)
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  /// <summary>
  /// General decoder interface for MSIL
  /// </summary>
  /// <typeparam name="Source">Type of IL source operands</typeparam>
  /// <typeparam name="Dest">Type of IL destinations</typeparam>
  /// <typeparam name="Context">Context provided by the particular decoder</typeparam>
  /// <typeparam name="EdgeConversionData">Extra info for control flow edges, like renamings etc.</typeparam>
  public interface IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, ContextData, EdgeConversionData>
  {
    /// <summary>
    /// Decode the instruction immediately following the label on a forward path.
    /// </summary>
    Result ForwardDecode<Data, Result, Visitor>(Label lab, Visitor visitor, Data data)
     where Visitor : IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Data, Result>;

    /// <summary>
    /// Extra information about Source, Dest, or the controlflow provided by this decoder
    /// </summary>
    ContextData Context { get; }

    /// <summary>
    /// Lookup EdgeData
    /// </summary>
    EdgeConversionData EdgeData(Label from, Label to);

    /// <summary>
    /// Show the edge conversion data
    /// </summary>
    void Display(TextWriter tw, string prefix, EdgeConversionData data);

    /// <summary>
    /// Allows to stop decoding/traversing if a given pc is unreachable.
    /// Some abstraction layer have non-trivial reachability info, whereas base layers can always return false here.
    /// </summary>
    bool IsUnreachable(Label pc);
  }


  public interface IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Data, Result>
    : IVisitSynthIL<Label, Method, Type, Source, Dest, Data, Result>, IVisitExprIL<Label, Type, Source, Dest, Data, Result>
  {
    #region Basic IL
    Result Arglist(Label pc, Dest dest, Data data);
    Result BranchCond(Label pc, Label target, BranchOperator bop, Source value1, Source value2, Data data);
    Result BranchTrue(Label pc, Label target, Source cond, Data data);
    Result BranchFalse(Label pc, Label target, Source cond, Data data);
    Result Branch(Label pc, Label target, bool leave, Data data);
    Result Break(Label pc, Data data);
    /// <summary>
    /// A Call or Callvirt instruction
    /// </summary>
    /// <typeparam name="TypeList">IIndexable of Type"/></typeparam>
    /// <typeparam name="ArgList">IIndexable of Source</typeparam>
    Result Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>; // could be dummy dest
    /// <summary>
    /// A Calli instruction.
    /// 
    /// Note: the argument type list does not contain the type of the first instance parameter if the
    /// method calling convention is Instance.
    /// </summary>
    /// <typeparam name="TypeList">IIndexable of Type"/></typeparam>
    /// <typeparam name="ArgList">IIndexable of Source</typeparam>
    Result Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, bool instance, Dest dest, Source fp, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>; // could be dummy dest
    Result Ckfinite(Label pc, Dest dest, Source source, Data data);
    Result Cpblk(Label pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Data data);
    // For Dup instruction, see Ldstack in IVisitSynthIL
    Result Endfilter(Label pc, Source decision, Data data);
    Result Endfinally(Label pc, Data data);
    Result Initblk(Label pc, bool @volatile, Source destaddr, Source value, Source len, Data data);
    Result Jmp(Label pc, Method method, Data data);
    /// <summary>
    /// Load argument onto stack.
    /// </summary>
    /// <param name="argument">parameter being loaded</param>
    /// <param name="isOld">true if this load refers to loading the original value of the parameter on entry
    /// to the method.</param>
    Result Ldarg(Label pc, Parameter argument, bool isOld, Dest dest, Data data);
    Result Ldarga(Label pc, Parameter argument, bool isOld, Dest dest, Data data);
    Result Ldftn(Label pc, Method method, Dest dest, Data data);
    Result Ldind(Label pc, Type type, bool @volatile, Dest dest, Source ptr, Data data);
    Result Ldloc(Label pc, Local local, Dest dest, Data data);
    Result Ldloca(Label pc, Local local, Dest dest, Data data);
    Result Localloc(Label pc, Dest dest, Source size, Data data);
    Result Nop(Label pc, Data data);
    Result Pop(Label pc, Source source, Data data);
    Result Return(Label pc, Source source, Data data); // could be dummy source
    Result Starg(Label pc, Parameter argument, Source source, Data data);
    Result Stind(Label pc, Type type, bool @volatile, Source ptr, Source value, Data data);
    Result Stloc(Label pc, Local local, Source source, Data data);
    /// <summary>
    /// Generalization of switch on ints, can switch on other data constants as well.
    /// </summary>
    /// <param name="type">Type of data being switched on, typically int32</param>
    Result Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, Source value, Data data);
    #endregion

    #region Object level instructions
    /// <summary>
    /// A Constrained.Callvirt instruction
    /// </summary>
    /// <typeparam name="TypeList">IIndexable of Type"/></typeparam>
    /// <typeparam name="ArgList">IIndexable of Source</typeparam>
    Result ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, Data data)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Source>; // could be dummy dest
    Result Castclass(Label pc, Type type, Dest dest, Source obj, Data data);
    Result Cpobj(Label pc, Type type, Source destptr, Source srcptr, Data data);
    Result Initobj(Label pc, Type type, Source ptr, Data data);
    Result Ldelem(Label pc, Type type, Dest dest, Source array, Source index, Data data);
    Result Ldelema(Label pc, Type type, bool @readonly, Dest dest, Source array, Source index, Data data);
    Result Ldfld(Label pc, Field field, bool @volatile, Dest dest, Source obj, Data data);
    Result Ldflda(Label pc, Field field, Dest dest, Source obj, Data data);
    Result Ldlen(Label pc, Dest dest, Source array, Data data);

    // Handled by Ldind
    // Result Ldobj(Label pc, Type type, bool @volatile, Dest dest, Source ptr, Data data);
    Result Ldsfld(Label pc, Field field, bool @volatile, Dest dest, Data data);
    Result Ldsflda(Label pc, Field field, Dest dest, Data data);
    Result Ldtypetoken(Label pc, Type type, Dest dest, Data data);
    Result Ldfieldtoken(Label pc, Field field, Dest dest, Data data);
    Result Ldmethodtoken(Label pc, Method method, Dest dest, Data data);
    Result Ldvirtftn(Label pc, Method method, Dest dest, Source obj, Data data);
    Result Mkrefany(Label pc, Type type, Dest dest, Source obj, Data data);
    Result Newarray<ArgList>(Label pc, Type type, Dest dest, ArgList len, Data data)
      where ArgList : IIndexable<Source>;
    /// <summary>
    /// A newobj instruction
    /// </summary>
    /// <typeparam name="ArgList">IIndexable of Source</typeparam>
    Result Newobj<ArgList>(Label pc, Method ctor, Dest dest, ArgList args, Data data)
      where ArgList : IIndexable<Source>;
    Result Refanytype(Label pc, Dest dest, Source source, Data data);
    Result Refanyval(Label pc, Type type, Dest dest, Source source, Data data);
    Result Rethrow(Label pc, Data data);
    Result Stelem(Label pc, Type type, Source array, Source index, Source value, Data data);
    Result Stfld(Label pc, Field field, bool @volatile, Source obj, Source value, Data data);

    // Handled by stind
    // Result Stobj(Label pc, Type type, bool @volatile, Source ptr, Source value, Data data);
    Result Stsfld(Label pc, Field field, bool @volatile, Source value, Data data);
    Result Throw(Label pc, Source exn, Data data);
    Result Unbox(Label pc, Type type, Dest dest, Source obj, Data data);
    Result Unboxany(Label pc, Type type, Dest dest, Source obj, Data data);
    #endregion
  }


  /// <summary>
  /// Some synthetic IL instructions
  /// </summary>
  public interface IVisitSynthIL<Label, Method, Type, Source, Dest, Data, Result>
  {
    /// <summary>
    /// Instruction is visited on entry to a method as the first instruction.
    /// </summary>
    Result Entry(Label pc, Method method, Data data);

    /// <summary>
    /// Appears at targets of conditional control transfers (branches and switch)
    /// to express the branch condition in a uniform accessible way.
    ///
    /// Can also appear due to preconditions in a method and due to post conditions after a method call
    /// or invariants on field reads.
    /// </summary>
    /// <param name="tag">Is the tag selected by the code provider for a control transfer, or "false"
    /// for the fallthrough of a conditional branch or the default target of a switch.</param>
    /// <param name="source">The conditional value deciding the branch</param>
    /// <param name="provenance">An opaque object (possibly null) explaining where this assert comes from.</param>
    Result Assume(Label pc, string tag, Source condition, object provenance, Data data);

    /// <summary>
    /// Can appear due to explicit asserts in the code or due to post conditions in a method, or due
    /// to preconditions at method calls, or due to invariants at field updates.
    /// </summary>
    /// <param name="tag">Is a tag indicating what kind of assert it is, e.g., requires, ensures, inline, etc.</param>
    /// <param name="provenance">An opaque object (possibly null) explaining where this assert comes from.</param>
    Result Assert(Label pc, string tag, Source condition, object provenance, Data data);

    /// <summary>
    /// This is a generalized Dup instruction, where dup is equal to ldstack 0. ldstack i duplicates the i-th element on
    /// the evaluation stack and pushes it onto the evaluation stack.
    /// </summary>
    /// <param name="offset">0 based offset from the top of the evaluation stack.</param>
    /// <param name="isOld">if true, the stack access should happen in the pre-state of the method (or surrounding call)</param>
    Result Ldstack(Label pc, int offset, Dest dest, Source source, bool isOld, Data data);

    /// <summary>
    /// Similar to ldstack.i but pretends that stack locations also have an address and gets that address.
    /// This is mainly used to deal with access to struct parameters in pre/post conditions and for uni-
    /// formity.
    /// </summary>
    Result Ldstacka(Label pc, int offset, Dest dest, Source source, Type origParamType, bool isOld, Data data);

    /// <summary>
    /// This instruction provides access to the result of a method in the context of a post condition.
    /// Typically, this accesses the top of the evaluation stack, however, there may be other temporaries
    /// above the result. CodeProviders generate this instruction for accessing the result and the
    /// analysis infrastructure can compute the proper offsets, turning it essentially into a ldstack/copy.
    /// </summary>
    Result Ldresult(Label pc, Type type, Dest dest, Source source, Data data);

    /// <summary>
    /// Marks the beginning of an old expression, ie., a code sequence evaluating in the state of the machine
    /// as it was at the beginning of the current method call
    /// This operation should push the current machine state onto a temporary machine stack (to be popped by EndOld),
    /// and switch state to the state at the beginning of the current method call.
    /// </summary>
    Result BeginOld(Label pc, Label matchingEnd, Data data);

    /// <summary>
    /// Marks the end of an old expression evaluation, where the result of the expression is read in the current state
    /// from Source. The instruction should then switch to the machine state that is on top of the machine state stack
    /// (pushed by BeginOld), and store the result read from the old store into this store at the destination.
    /// </summary>
    /// <param name="matchingBegin">Label of the matching BeginOld</param>
    /// <param name="dest">Destination to store result of old expression in the restored current state</param>
    /// <param name="source">Result of old expression as read from the old state</param>
    Result EndOld(Label pc, Label matchingBegin, Type type, Dest dest, Source source, Data data);
  }

  /// <summary>
  /// Factored these operations out as they are the ones used by an expression visitor.
  /// If need be, we can move more ops from IVisitMSIL to here without breaking code depending on IVisitMSIL.
  /// </summary>
  public interface IVisitExprIL<Label, Type, Source, Dest, Data, Result>
  {
    Result Binary(Label pc, BinaryOperator op, Dest dest, Source s1, Source s2, Data data);
    Result Box(Label pc, Type type, Dest dest, Source source, Data data);
    Result Isinst(Label pc, Type type, Dest dest, Source obj, Data data);
    Result Ldconst(Label pc, Object constant, Type type, Dest dest, Data data);
    Result Ldnull(Label pc, Dest dest, Data data);
    Result Sizeof(Label pc, Type type, Dest dest, Data data);
    Result Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Source source, Data data);
  }

  [ContractClass(typeof(IMethodContextContracts<,>))]
  public interface IMethodContext<Field, Method>
  {
    IMethodContextData<Field, Method> MethodContext { get; }
  }

  #region Contracts for IMethodContext
  [ContractClassFor(typeof(IMethodContext<,>))]
  abstract class IMethodContextContracts<Field, Method> : IMethodContext<Field, Method>
  {
    public IMethodContextData<Field, Method> MethodContext
    {
      get
      {
        Contract.Ensures(Contract.Result<IMethodContextData<Field, Method>>() != null);

        return null;
      }
    }
  }
  #endregion

  public partial interface IMethodContextData<Field, Method>
  {
    Method CurrentMethod { get; }

    ICFG CFG { get; }

    /// <summary>
    /// Returns closest source context for this label
    /// </summary>
    string SourceContext(APC label);

    /// <summary>
    /// Returns the name of the associated document of the source context for this pc
    /// </summary>
    string SourceDocument(APC pc);

    /// <summary>
    /// Returns the first line number in the source context of this pc.
    /// </summary>
    int SourceStartLine(APC pc);

    /// <summary>
    /// Returns the last line number in the source context of this pc.
    /// </summary>
    int SourceEndLine(APC pc);

    /// <summary>
    /// Returns the first column number in the source context of this pc.
    /// </summary>
    int SourceStartColumn(APC pc);

    /// <summary>
    /// Returns the first column number in the source context of this pc.
    /// </summary>
    int SourceEndColumn(APC pc);
    /// <summary>
    /// Returns the IL offset within the method of the given instruction or 0
    /// </summary>

    IEnumerable<Field> Modifies(Method method);

    IEnumerable<Method> AffectedGetters(Field field);
  }

  #region IMethodContextData<Field, Method> contract binding
  [ContractClass(typeof(IMethodContextDataContract<,>))]
  public partial interface IMethodContextData<Field, Method>
  {

  }

  [ContractClassFor(typeof(IMethodContextData<,>))]
  abstract class IMethodContextDataContract<Field, Method> : IMethodContextData<Field, Method>
  {
    #region IMethodContextData<Field,Method> Members

    public Method CurrentMethod
    {
      get { throw new NotImplementedException(); }
    }

    public ICFG CFG
    {
      get
      {
        Contract.Ensures(Contract.Result<ICFG>() != null);
        throw new NotImplementedException();
      }
    }

    public string SourceContext(APC label)
    {
      throw new NotImplementedException();
    }

    public string SourceDocument(APC pc)
    {
      throw new NotImplementedException();
    }

    public StackTemp SourceStartLine(APC pc)
    {
      throw new NotImplementedException();
    }

    public StackTemp SourceEndLine(APC pc)
    {
      throw new NotImplementedException();
    }

    public StackTemp SourceStartColumn(APC pc)
    {
      throw new NotImplementedException();
    }

    public StackTemp SourceEndColumn(APC pc)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Field> Modifies(Method method)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Field>>() != null);
      throw new NotImplementedException();
    }

    public IEnumerable<Method> AffectedGetters(Field field)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  static public class IDecodeMetadataExtensionsForPurityInfo
  {
    [ThreadStatic]
    private static Dictionary<string, long> additionalPurityInfo;

    // Need lazy-initialization because of ThreadStatic
    private static Dictionary<string, long> AdditionalPurityInfo
    {
      get
      {
        if (additionalPurityInfo == null)
          additionalPurityInfo = new Dictionary<string, long>();
        return additionalPurityInfo;
      }
    }

    [Pure]
    static public bool TryInferredPureParametersMask(string methodName, out long mask)
    {
      return AdditionalPurityInfo.TryGetValue(methodName, out mask);
    }

    [Pure]
    static public void SetPureParametersMask(string methodName, long mask)
    {
      AdditionalPurityInfo[methodName] = mask;
    }

    [Pure]
    static public void AddPure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method m, int parameterPosition)
    {
      Contract.Requires(parameterPosition <= 64);

      if (metaDataDecoder == null)
      {
        return;
      }

      var name = metaDataDecoder.FullName(m);

      /*
      if (name == null)
      {
        return;
      }
      */

      long prevMask, mask = 1L << parameterPosition;
      if (AdditionalPurityInfo.TryGetValue(name, out prevMask))
      {
        mask |= prevMask;
      }

      AdditionalPurityInfo[name] = mask;
    }

    /// <param name="parameterPosition">The position of the parameter counting this</param>
    /// <returns>
    /// true if the parameter in that position is marked with Pure
    /// </returns>
    [Pure]
    static public bool IsPure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method, int parameterPosition)
      where Type : IEquatable<Type>
    {
      Contract.Requires(metaDataDecoder != null);
      Contract.Requires(parameterPosition >= 0);

      long purityMask;
      if (AdditionalPurityInfo.TryGetValue(metaDataDecoder.FullName(method), out purityMask) && (purityMask & (1L << parameterPosition)) != 0)
      {
        return true;
      }

      Parameter p;
      if (metaDataDecoder.IsStatic(method))
      {
        var args = metaDataDecoder.Parameters(method);
        Contract.Assume(args != null);
        // F: I've added this check because in some code from Midori the invariant was violated
        if (parameterPosition < args.Count)
        {
          p = args[parameterPosition];
        }
        else
        {
          return false;
        }
      }
      else
      {
        if (parameterPosition == 0)
        {
          return false;
        }
        p = metaDataDecoder.Parameters(method).AssumeNotNull()[parameterPosition - 1];
      }

      foreach (var attrib in metaDataDecoder.GetAttributes(p).AssumeNotNull())
      {
        var attibType = metaDataDecoder.AttributeType(attrib);
        if (metaDataDecoder.Name(attibType) == "PureAttribute")
        {
          return true;
        }
      }

      return false;
    }

    [Pure]
    static public bool IsPure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method, Parameter p)
    where Type : IEquatable<Type>
    {
      Contract.Requires(metaDataDecoder != null);

      foreach (var attrib in metaDataDecoder.GetAttributes(p).AssumeNotNull())
      {
        var attibType = metaDataDecoder.AttributeType(attrib);
        if (metaDataDecoder.Name(attibType) == "PureAttribute")
        {
          return true;
        }
      }

      return false;
    }
  }

  /// <summary>
  /// A slice is an analyzable subset of an assembly. The subset is a set
  /// of methods whose bodies are to be analyzed.
  /// </summary>

  [ContractClass(typeof(ISliceContracts<,,,>))]
  public interface ISlice<Method, Field, Type, Assembly>
  {
    /// <summary>
    /// The name of the slice. Used, e.g., as the module name
    /// and file name for a slice persisted as an assembly on disk.
    /// </summary>
    string Name { get; }
    /// <summary>
    /// The assembly that the methods and chains are defined in.
    /// </summary>
    Assembly ContainingAssembly { get; }
    /// <summary>
    /// A set of method definitions, all of which are defined in the containing
    /// assembly. All of the methods must also appear within one of the Chains.
    /// </summary>
    IEnumerable<Method> Methods { get; }
    /// <summary>
    /// A set of specifications of members whose metadata is needed in order to
    /// have the slice be a valid .NET assembly. Chains are structured so that
    /// all members belonging to the same type are contained within the chain
    /// of that type.
    /// </summary>
    IEnumerable<IChain<Method, Field, Type>> Chains { get; }
  }

  #region Contracts

  [ContractClassFor(typeof(ISlice<,,,>))]
  abstract class ISliceContracts<Method, Field, Type, Assembly> : ISlice<Method, Field, Type, Assembly>
  {

    public string Name
    {
      get { throw new NotImplementedException(); }
    }

    public Assembly ContainingAssembly
    {
      get {
        Contract.Ensures(Contract.Result<Assembly>() != null);
        throw new NotImplementedException(); }
    }

    public IEnumerable<Method> Methods
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

        throw new NotImplementedException();
      }
    }

    public IEnumerable<IChain<Method, Field, Type>> Chains
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<IChain<Method, Field, Type>>>() != null);

        throw new NotImplementedException();
      }
    }
  }
  #endregion

  #region MB: suggested change for IChain
#if false
  public interface IType<M, F, T>
  {
    T Type { get; }
    IEnumerable<F> Fields { get; }
    IEnumerable<M> Methods { get; }
    IEnumerable<IType<M, F, T>> NestedTypes { get; }
  }
#endif
  #endregion

  public enum ChainTag
  {
    Field,
    Method,
    Type,
  }

  /// <summary>
  /// A chain is a tree specifying a set of members whose metadata is needed
  /// in a slice. It is a tree because each member (type, method, field) must
  /// be contained within the chain of its declaring type.
  /// For instance, a method T.Nested.M, must be represented as
  /// Chain("type", T, [Chain("type", Nested, [Chain("method", M)])])
  /// where the quoted strings are values of type <see cref="ChainTag"/>.
  /// The sequences are so that multiple subtrees can be specified.
  /// </summary>
  /// <typeparam name="M">The type representing a method.</typeparam>
  /// <typeparam name="F">The type representing a field.</typeparam>
  /// <typeparam name="T">The type representing a type.</typeparam>
  [ContractClass(typeof(IChainContract<,,>))]
  public interface IChain<M, F, T>
  {
    ChainTag Tag { get; }
    F Field { get; }
    M Method { get; }
    MethodHashAttribute MethodHashAttribute { get; }
    T Type { get; }
    IEnumerable<IChain<M, F, T>>/*?*/ Children { get; }
  }
  #region IChain contract binding
  [ContractClassFor(typeof(IChain<,,>))]
  abstract class IChainContract<M2, F2, T2> : IChain<M2, F2, T2>
  {
    public ChainTag Tag
    {
      get { throw new NotImplementedException(); }
    }

    public F2 Field
    {
      get
      {
        Contract.Requires(Tag == ChainTag.Field);
        throw new NotImplementedException();
      }
    }

    public M2 Method
    {
      get
      {
        Contract.Requires(Tag == ChainTag.Method);
        throw new NotImplementedException();
      }
    }

    public MethodHashAttribute MethodHashAttribute
    {
      get
      {
        Contract.Requires(Tag == ChainTag.Method);
        throw new NotImplementedException();
      }
    }

    public T2 Type
    {
      get
      {
        Contract.Requires(Tag == ChainTag.Type);
        throw new NotImplementedException();
      }
    }

    public IEnumerable<IChain<M2, F2, T2>> Children
    {
      get
      {
        Contract.Requires(Tag == ChainTag.Type);
        throw new NotImplementedException();
      }
    }
  }
  #endregion

  public interface ICodeWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    bool WriteSliceToFile(ISlice<Method, Field, Type, Assembly> slice, string directory, out string dll);
  }

}