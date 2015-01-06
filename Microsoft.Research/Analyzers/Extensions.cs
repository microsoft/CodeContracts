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
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{

  public static class IFactQueryExtensions
  {
    public static bool TrySign<Expression, Variable>(this IFactQuery<Expression, Variable> query, APC pc, Expression exp, out int sign)
    {
      Contract.Requires(query != null);
      Contract.Requires(exp != null);

      var gez = query.IsGreaterEqualToZero(pc, exp);
      if (gez.IsNormal())
      {
        if (gez == ProofOutcome.False)
        {
          sign = -1;
          return true;
        }
        var intv = query.BoundsFor(pc, exp);
        if(intv.One == 0 && intv.Two == 0)
        {
          sign = 0;
          return true;
        }

        sign = +1;
        return true;
      }

      sign = default(int);
      return false;
    }
  }

  public static class ArrayExtensions
  {
    public static bool Contains<T>(this T[] arr, T el)
    {
      Contract.Requires(arr != null);

      foreach (var x in arr)
      {
        // Use equals as T may be float, and hence we should pay attention in compating NaN
        if (x.Equals(el))
          return true;
      }

      return false;
    }
  }

  public static class IFunctionalMapExtensions
  {
    public static bool IsIdentity<Variable>(this IFunctionalMap<Variable, FList<Variable>> map)
    {
      Contract.Requires(map != null);

      if (map.Count == 0)
      {
        return true;
      }

      foreach (var key in map.Keys)
      {
        var value = map[key];
        if (value.Length() != 1 || !value.Head.Equals(key))
          return false;
      }

      return true;
    }

    public static Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> RefineMapToBoxedVariables<Variable>(
      this IFunctionalMap<Variable, FList<Variable>> map, Renamings<Variable> renamings, int maxVarsInOneRenaming = Int32.MaxValue)
    {
      Contract.Requires(map != null);
      Contract.Ensures(Contract.Result<Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>>() != null);

      var result = new Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>(map.Count);

      var thresholds = 0;
      foreach (var v in map.Keys)
      {
        var bv = new BoxedVariable<Variable>(v);

        var originalMapEntry = map[v];
        var renamedMapEntry = originalMapEntry.Map(target => new BoxedVariable<Variable>(target), maxVarsInOneRenaming);

        if(renamedMapEntry.Length() != originalMapEntry.Length())
        {
          thresholds++;
        }
        result[bv] = renamedMapEntry;
      }

#if DEBUG && false

      if(thresholds > 0)
      {
        Console.WriteLine("[RENAMING] Cut {0} renaming(s) to length {1} ", thresholds, maxVarsInOneRenaming);
      }
#endif
      return result;
    }
  }

  public static class ICFGExtensions
  {
    [Pure]
    public static APC GetPCForMethodEntry(this ICFG CFG, int maxDepth = 10)
    {
      Contract.Requires(CFG != null);

      var entry = CFG.EntryAfterRequires;
      for (int count = 0; count < maxDepth; count++)
      {
        var candidate = entry.PrimaryMethodLocation();
        if (candidate.HasRealSourceContext)
        {
          return candidate;
        }
        entry = CFG.Post(entry);
      }
      return CFG.Entry;
    }
  }

  public static class IMethodDriverExtensions
  {
    public static string CurrentMethodName<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options>(
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options> mdriver)
      where Type : IEquatable<Type>
      where Options : IFrameworkLogOptions
    {
      Contract.Requires(mdriver != null);

      return mdriver.MetaDataDecoder.Name(mdriver.CurrentMethod);
    }
  }

  public static class IDecodeMetaDataExtensions
  {

    [Pure]
    static public Interval GetIntervalForType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      #region all the cases
      if (decoder.IsEnum(type) && !decoder.HasFlagsAttribute(type))
      {
        return Interval.Ranges.EnumRange(type, (Type t) =>
        {
          var result = new List<int>();
          if (decoder.TryGetEnumValues(t, out result))
          {
            return result;
          }
          else
          {
            return null;
          }
        }
        );
      }

      if (decoder.System_UInt8.Equals(type))
      {
        return Interval.Ranges.UInt8Range;
      }
      if (decoder.System_Char.Equals(type))
      {
        return Interval.Ranges.UInt16Range;
      }
      if (decoder.System_UInt16.Equals(type))
      {
        return Interval.Ranges.UInt16Range;
      }
      if (decoder.System_UInt32.Equals(type))
      {
        return Interval.Ranges.UInt32Range;
      }
      if (decoder.System_UInt64.Equals(type))
      {
        return Interval.Ranges.UInt64Range;
      }
      if (decoder.System_Int8.Equals(type))
      {
        return Interval.Ranges.Int8Range;
      }
      if (decoder.System_Int16.Equals(type))
      {
        return Interval.Ranges.Int16Range;
      }
      if (decoder.System_Int32.Equals(type))
      {
        return Interval.Ranges.Int32Range;
      }
      if (decoder.System_Int64.Equals(type))
      {
        return Interval.Ranges.Int64Range;
      }

      // Return unknown for float/double as they contain other values ad NaN, +oo, and -oo
      return Interval.UnknownInterval;
      #endregion
    }

    [Pure]
    static public DisInterval GetDisIntervalForType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      #region all the cases
      if (decoder.IsEnum(type) && !decoder.HasFlagsAttribute(type))
      {
        return DisInterval.Ranges.EnumValues(type, (Type t) =>
        {
          var result = new List<int>();
          if (decoder.TryGetEnumValues(t, out result))
          {
            return result;
          }
          else
          {
            return null;
          }
        }
        );
      }

      if (decoder.System_UInt8.Equals(type))
      {
        return DisInterval.Ranges.UInt8Range;
      }
      if (decoder.System_Char.Equals(type))
      {
        return DisInterval.Ranges.UInt16Range;
      }
      if (decoder.System_UInt16.Equals(type))
      {
        return DisInterval.Ranges.UInt16Range;
      }
      if (decoder.System_UInt32.Equals(type))
      {
        return DisInterval.Ranges.UInt32Range;
      }
      if (decoder.System_UInt64.Equals(type))
      {
        return DisInterval.Ranges.UInt64Range;
      }
      if (decoder.System_Int8.Equals(type))
      {
        return DisInterval.Ranges.Int8Range;
      }
      if (decoder.System_Int16.Equals(type))
      {
        return DisInterval.Ranges.Int16Range;
      }
      if (decoder.System_Int32.Equals(type))
      {
        return DisInterval.Ranges.Int32Range;
      }
      if (decoder.System_Int64.Equals(type))
      {
        return DisInterval.Ranges.Int64Range;
      }

      return DisInterval.UnknownInterval;
      #endregion
    }

    [Pure]
    static public bool TryGetEnumValues<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type, out List<int> values)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out values) != null);

      if (decoder.IsEnum(type))
      {
        values = new List<int>();
        foreach (var f in decoder.Fields(type))
        {
          object obj;

          if (decoder.TryInitialValue(f, out obj))
          {
            if (obj is Int32)
              values.Add((Int32)obj);
            else if (obj is Byte)
              values.Add((Int32)(Byte)obj);
            else if (obj is SByte)
              values.Add((Int32)(SByte)obj);
            else if (obj is Int16)
              values.Add((Int32)(Int16)obj);
            else if (obj is UInt16)
              values.Add((Int32)(UInt16)obj);
            // otherwise we give up
          }
        }

        return true;
      }

      values = default(List<int>);
      return false;
    }

    [Pure]
    static public bool TryGetEnumFieldNameFromValue<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
      (this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, object value, Type type, out Field enumField)
    {
      if (decoder == null || !decoder.IsEnum(type))
      {
        enumField = default(Field);
        return false;
      }

      foreach (var f in decoder.Fields(type))
      {
        object obj;

        if (decoder.TryInitialValue(f, out obj))
        {
          if (obj.Equals(value))
          {
            enumField = f;
            return true;
          }
        }
      }
      enumField = default(Field);
      return false;
    }


    [Pure]
    static public bool TryGetEnumFields<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type, out List<string> values)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out values) != null);

      if (decoder.IsEnum(type))
      {
        values = new List<string>();
        foreach (var f in decoder.Fields(type))
        {
          values.Add(decoder.Name(f));
        }

        return true;
      }

      values = default(List<string>);
      return false;
    }

    [Pure]
    static public bool TryGetMinValueForType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type, out object value)
    {
      #region all the cases
      if (decoder.System_UInt8.Equals(type))
      {
        value = Byte.MinValue;
        return true;
      }
      if (decoder.System_UInt16.Equals(type))
      {
        value = UInt16.MinValue;
        return true;
      }
      if (decoder.System_UInt32.Equals(type))
      {
        value = UInt32.MinValue;
        return true;
      }
      if (decoder.System_UInt64.Equals(type))
      {
        value = UInt64.MinValue;
        return true;
      }
      if (decoder.System_Int8.Equals(type))
      {
        value = SByte.MinValue;
        return true;
      }
      if (decoder.System_Int16.Equals(type))
      {
        value = Int16.MinValue;
        return true;
      }
      if (decoder.System_Int32.Equals(type))
      {
        value = Int32.MinValue;
        return true;
      }
      if (decoder.System_Int64.Equals(type))
      {
        value = Int64.MinValue;
        return true;
      }
      if (decoder.System_IntPtr.Equals(type))
      {
        value = Int32.MinValue;
        return true;
      }
      if (decoder.System_Single.Equals(type))
      {
        value = Single.MinValue;
        return true;
      }
      if (decoder.System_Double.Equals(type))
      {
        value = Double.MinValue;
        return true;
      }

      value = null;
      return false;
      #endregion
    }

    [Pure]
    static public bool TryGetMaxValueForType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type, out object value)
    {
      #region all the cases
      if (decoder.System_UInt8.Equals(type))
      {
        value = Byte.MaxValue;
        return true;
      }
      if (decoder.System_UInt16.Equals(type))
      {
        value = UInt16.MaxValue;
        return true;
      }
      if (decoder.System_UInt32.Equals(type))
      {
        value = UInt32.MaxValue;
        return true;
      }
      if (decoder.System_UInt64.Equals(type))
      {
        value = UInt64.MaxValue;
        return true;
      }
      if (decoder.System_Int8.Equals(type))
      {
        value = SByte.MaxValue;
        return true;
      }
      if (decoder.System_Int16.Equals(type))
      {
        value = Int16.MaxValue;
        return true;
      }
      if (decoder.System_Int32.Equals(type))
      {
        value = Int32.MaxValue;
        return true;
      }
      if (decoder.System_Int64.Equals(type))
      {
        value = Int64.MaxValue;
        return true;
      }
      if (decoder.System_IntPtr.Equals(type))
      {
        value = Int32.MaxValue;
        return true;
      }
      if (decoder.System_Single.Equals(type))
      {
        value = Single.MaxValue;
        return true;
      }
      if (decoder.System_Double.Equals(type))
      {
        value = Double.MaxValue;
        return true;
      }

      value = null;
      return false;
      #endregion
    }

    [Pure]
    static public bool TryInferTypeForOperator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, UnaryOperator op, out Type type)
    where Type : IEquatable<Type>
    {
      Contract.Requires(decoder != null);

      var typ = op.GetReturnType(decoder);

      if (typ.IsNormal)
      {
        type = typ.Value;
        return true;
      }

      type = default(Type);
      return false;
    }

    [Pure]
    static public bool IsPropertyGetterOrSetter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method m)
      where Type : IEquatable<Type>
    {
      Contract.Requires(metaDataDecoder != null);

      return metaDataDecoder.IsPropertyGetter(m) || metaDataDecoder.IsPropertySetter(m);
    }

    [Pure]
    static public bool IsPropertyGetterOrSetterAndGetProperty<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method m, out Property property)
      where Type : IEquatable<Type> {
      Contract.Requires(metaDataDecoder != null);

      if (metaDataDecoder.IsPropertyGetter(m)) { property = metaDataDecoder.GetPropertyFromAccessor(m); return true; }
      if (metaDataDecoder.IsPropertySetter(m)) { property = metaDataDecoder.GetPropertyFromAccessor(m); return true; }
      property = default(Property);
      return false;
    }

    /// <summary>
    /// Just using a simple name matching, this is brittle, we should fine a better way of doing it
    /// </summary>
    [Pure]
    static public bool IsNetFrameworkAssembly<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Assembly assembly)
    {
      Contract.Requires(decoder != null);

      var assemblyName = decoder.Name(assembly);

      switch (assemblyName)
      {
        case "mscorlib":
        case "Microsoft.VisualBasic":
        case "PresentationCore":
        case "PresentationFramework":
        case "System.ComponentModel.Composition":
        case "System.Configuration.Install":
        case "System.Configuration":
        case "System":
        case "System.Core":
        case "System.Data":
        case "System.DirectoryServices":
        case "System.Drawing":
        case "System.Numerics":
        case "System.Runtime.Caching":
        case "System.Security":
        case "System.ServiceModel":
        case "System.ServiceProcess":
        case "System.Web.ApplicationServices":
        case "System.Web":
        case "System.Windows.Browser":
        case "System.Windows.Forms":
        case "System.Windows":
        case "System.Xml.Linq":
        case "System.Xml":
        case "WindowsBase":
          return true;

        default:
          return false;
      }
    }



    [Pure]
    static public bool IsIntegerType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      Contract.Requires(decoder != null);

      return decoder.Equal(type, decoder.System_Int32) ||
                      decoder.Equal(type, decoder.System_Int64) ||
                      decoder.Equal(type, decoder.System_Int16) ||
                      decoder.Equal(type, decoder.System_Int8);               
    }

    [Pure]
    static public bool IsUnsignedIntegerType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      Contract.Requires(decoder != null);

      return decoder.Equal(type, decoder.System_UInt32) ||
                      decoder.Equal(type, decoder.System_UInt64) ||
                      decoder.Equal(type, decoder.System_UInt16) ||
                      decoder.Equal(type, decoder.System_UInt8);
    }

    [Pure]
    static public bool IsFloatType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      Contract.Requires(decoder != null);

      return decoder.Equal(type, decoder.System_Single) ||
                      decoder.Equal(type, decoder.System_Double);
    }

    [Pure]
    static public bool IsBoolean<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      Contract.Requires(decoder != null);

      if (decoder.System_Boolean.Equals(type))
      {
        return true;
      }

      return false;
    }

    [Pure]
    static public bool IsEnumWithoutFlagAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Type type)
    {
      Contract.Requires(decoder != null);

      return decoder.IsEnum(type) && !decoder.HasFlagsAttribute(type);
    }

    [Pure]
    static public bool IsInNamespace<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
     this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, string @namespace, Type type)
    {
      Contract.Requires(decoder != null);

      return decoder.FullName(type).StartsWith(@namespace);
    }

    [Pure]
    static public bool IsCallToIsNaN<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder,
      Method method)
    {
      return IsCallToMathFunction(decoder, method, "IsNaN");
    }

    [Pure]
    static public bool IsCallToIsPositiveInfinity<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder,
      Method method)
    {
      return IsCallToMathFunction(decoder, method, "IsPositiveInfinity");
    }

    [Pure]
    static public bool IsCallToIsNegativeInfinity<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder,
      Method method)
    {
      return IsCallToMathFunction(decoder, method, "IsNegativeInfinity");
    }

    [Pure]
    static private bool IsCallToMathFunction<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder,
      Method method, string functionName)
    {
      Contract.Requires(functionName != null);

      if (decoder.Name(method) == functionName)
      {
        var containingType = decoder.DeclaringType(method);
        var containingTypeName = decoder.Name(containingType);
        var nsName = decoder.Namespace(containingType);
        return nsName == "System" && (containingTypeName == "Double" || containingTypeName == "Single");
      }
      else
      {
        return false;
      }
    }

    [Pure]
    static public bool TryGetSetterFromGetter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder,
      Method method, out Method setter)
    {
      Contract.Requires(decoder != null);
      Contract.Requires(decoder.IsPropertyGetter(method));

      var name = decoder.Name(method).Replace("get_", "set_");
      foreach(var m in decoder.Methods(decoder.DeclaringType(method)).Where(p => decoder.IsPropertySetter(p)))
      {
        if(decoder.Name(m) == name)
        {
          setter = m;
          return true;
        }
      }

      setter = default(Method);
      return false;
    }


    [Pure]
    static public bool ContainsAPinnedLocal<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder, Method method)
    {
      Contract.Requires(decoder != null);

      return decoder.Locals(method).Enumerate().Any(loc => loc != null && decoder.IsPinned(loc));
    }

  }

  public static class UnaryOperatorExtensions
  {
    static public FlatDomain<Type> GetReturnType<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
       this UnaryOperator op, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder)
      where Type : IEquatable<Type>
    {
      Contract.Requires(decoder != null);

      switch (op)
      {
        case UnaryOperator.Conv_i:
          return new FlatDomain<Type>(decoder.System_IntPtr);

        case UnaryOperator.Conv_i1:
          return new FlatDomain<Type>(decoder.System_Int8);

        case UnaryOperator.Conv_i2:
          return new FlatDomain<Type>(decoder.System_Int16);

        case UnaryOperator.Conv_i4:
          return new FlatDomain<Type>(decoder.System_Int32);

        case UnaryOperator.Conv_i8:
          return new FlatDomain<Type>(decoder.System_Int64);

        case UnaryOperator.Conv_r_un:
          return new FlatDomain<Type>(decoder.System_Double);

        case UnaryOperator.Conv_r4:
          return new FlatDomain<Type>(decoder.System_Single);

        case UnaryOperator.Conv_r8:
          return new FlatDomain<Type>(decoder.System_Double);

        case UnaryOperator.Conv_u:
          return new FlatDomain<Type>(decoder.System_UIntPtr);

        case UnaryOperator.Conv_u1:
          return new FlatDomain<Type>(decoder.System_UInt8);

        case UnaryOperator.Conv_u2:
          return new FlatDomain<Type>(decoder.System_UInt16);

        case UnaryOperator.Conv_u4:
          return new FlatDomain<Type>(decoder.System_UInt32);

        case UnaryOperator.Conv_u8:
          return new FlatDomain<Type>(decoder.System_UInt64);

        default:
          return FlatDomain<Type>.TopValue;
      }
    }
  }

  public static class BinaryOperatorExtension
  {
    public static ExpressionOperator ToExpressionOperator(this BinaryOperator bop)
    {
      switch (bop)
      {
        case BinaryOperator.Add:
          return ExpressionOperator.Addition;

        case BinaryOperator.Add_Ovf:
          return ExpressionOperator.Addition_Overflow;

        case BinaryOperator.Add_Ovf_Un:
          return ExpressionOperator.Addition_Overflow;

        case BinaryOperator.And:
          return ExpressionOperator.And;

        case BinaryOperator.Ceq:
          return ExpressionOperator.Equal;

        case BinaryOperator.Cge:
          return ExpressionOperator.GreaterEqualThan;

        case BinaryOperator.Cge_Un:
          return ExpressionOperator.GreaterEqualThan_Un;

        case BinaryOperator.Cgt:
          return ExpressionOperator.GreaterThan;

        case BinaryOperator.Cgt_Un:
          return ExpressionOperator.GreaterThan_Un;

        case BinaryOperator.Cle:
          return ExpressionOperator.LessEqualThan;

        case BinaryOperator.Cle_Un:
          return ExpressionOperator.LessEqualThan_Un;

        case BinaryOperator.Clt:
          return ExpressionOperator.LessThan;

        case BinaryOperator.Clt_Un:
          return ExpressionOperator.LessThan_Un;

        case BinaryOperator.Cne_Un:
          return ExpressionOperator.NotEqual;

        case BinaryOperator.Cobjeq:
          return ExpressionOperator.Equal_Obj;

        case BinaryOperator.Div:
          return ExpressionOperator.Division;

        case BinaryOperator.Div_Un:
          return ExpressionOperator.Division;

        case BinaryOperator.LogicalAnd:
          return ExpressionOperator.LogicalAnd;

        case BinaryOperator.LogicalOr:
          return ExpressionOperator.LogicalOr;

        case BinaryOperator.Mul:
          return ExpressionOperator.Multiplication;

        case BinaryOperator.Mul_Ovf:
          return ExpressionOperator.Multiplication_Overflow;

        case BinaryOperator.Mul_Ovf_Un:
          return ExpressionOperator.Multiplication_Overflow;

        case BinaryOperator.Or:
          return ExpressionOperator.Or;

        case BinaryOperator.Rem:
          return ExpressionOperator.Modulus;

        case BinaryOperator.Rem_Un:
          return ExpressionOperator.Modulus;

        case BinaryOperator.Shl:
          return ExpressionOperator.ShiftLeft;

        case BinaryOperator.Shr:
          return ExpressionOperator.ShiftRight;

        case BinaryOperator.Shr_Un:
          return ExpressionOperator.Unknown;

        case BinaryOperator.Sub:
          return ExpressionOperator.Subtraction;

        case BinaryOperator.Sub_Ovf:
          return ExpressionOperator.Subtraction_Overflow;

        case BinaryOperator.Sub_Ovf_Un:
          return ExpressionOperator.Subtraction_Overflow;

        case BinaryOperator.Xor:
          return ExpressionOperator.Xor;

        default: return ExpressionOperator.Unknown;
      }
    }
  }

  public static class FlatAbstactDomainExtensions
  {
    public static ProofOutcome ToProofOutcome(this FlatAbstractDomain<bool> result)
    {
      if (result.IsBottom)
      {
        return ProofOutcome.Bottom;
      }
      else if (result.IsTop)
      {
        return ProofOutcome.Top;
      }
      else if (result.BoxedElement)
      {
        return ProofOutcome.True;
      }
      else if (!result.BoxedElement)
      {
        return ProofOutcome.False;
      }
      else
      {
        throw new AbstractInterpretationException("Impossible result?");
      }
    }
  }

  public static class BoxedExpressionsExtensions
  {

    public static BoxedExpression ReadAt<Local, Parameter, Method, Field, Type, Variable>(this BoxedExpression exp, APC pc, IValueContext<Local, Parameter, Method, Field, Type, Variable> context, bool failIfCannotReplaceVarsWithAccessPaths = true, Type allowReturnValue = default(Type))
      where Type : IEquatable<Type>
    {
      Contract.Requires(exp != null);

      return exp.Substitute<Variable>((var, original) =>
      {
        var accessPath = context.ValueContext.AccessPathList(pc, var, true, true, allowReturnValue);

        if (accessPath != null)
        {
          return BoxedExpression.Var(var, accessPath);
        }

        Contract.Assert(accessPath == null, "Just for readibility: we are here if we do not have an access path for the variable");

        return failIfCannotReplaceVarsWithAccessPaths ? null : original;

      });
    }

    [ContractVerification(true)]
    static public bool IsRelational(this BoxedExpression expression)
    {
      Contract.Requires(expression != null);

      BoxedExpression left, right;
      BinaryOperator op;
      if (expression.IsBinaryExpression(out op, out left, out right))
      {
        switch (op)
        {
          case BinaryOperator.Cne_Un:
          case BinaryOperator.Clt_Un:
          case BinaryOperator.Clt:
          case BinaryOperator.Cle_Un:
          case BinaryOperator.Cle:
          case BinaryOperator.Cgt_Un:
          case BinaryOperator.Cgt:
          case BinaryOperator.Cge_Un:
          case BinaryOperator.Cge:
          case BinaryOperator.Ceq:
          case BinaryOperator.Cobjeq:
            return true;
          default:
            break;
        }
      }
      return false;
    }

    /// <summary>
    /// May return null if it cannot convert the expression into a normalized expression or the input is null
    /// </summary>
    [ContractVerification(true)]
    static public NormalizedExpression<BoxedVariable<Variable>> ToNormalizedExpression<Variable>(this BoxedExpression exp, bool frameworkVarForComplexExpressionsAreOk = false)
    {
      if (exp == null)
      {
        return null;
      }

      int val;
      if (TryConstant(exp, out val))
      {
        return NormalizedExpression<BoxedVariable<Variable>>.For(val);
      }

      BoxedVariable<Variable> var;
      if (TryVariable(exp, out var, false))
      {
        return NormalizedExpression<BoxedVariable<Variable>>.For(var);
      }

      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right))
      {
        // F: TODO the other cases.
        if (bop == BinaryOperator.Add)
        {
          if ((TryVariable(left, out var, frameworkVarForComplexExpressionsAreOk) && TryConstant(right, out val)) 
            || (TryConstant(left, out val) && TryVariable(right, out var, frameworkVarForComplexExpressionsAreOk)))
          {
            return NormalizedExpression<BoxedVariable<Variable>>.For(var, val);
          }
        }

        if (bop == BinaryOperator.Sub)
        {
          if ((TryVariable(left, out var, frameworkVarForComplexExpressionsAreOk) && TryConstant(right, out val))
            || (TryConstant(left, out val) && TryVariable(right, out var, frameworkVarForComplexExpressionsAreOk)))
          {
            if (val == Int32.MinValue)
            {
              return null;
            }
            return NormalizedExpression<BoxedVariable<Variable>>.For(var, -val);
          }
        }
      }

      // in all the other cases, we just pack it
      if (exp.TryGetFrameworkVariable(out var))
      {
        return NormalizedExpression<BoxedVariable<Variable>>.For(var);
      }
      else
      {
        return null;
      }
    }

    static private bool TryConstant(BoxedExpression exp, out int val)
    {
      if (exp.IsConstantInt(out val))
      {
        return true;
      }

      return false;
    }

    static private bool TryVariable<Variable>(this BoxedExpression exp, out BoxedVariable<Variable> var, bool frameworkVarIsOk = false)
    {
      Contract.Requires(exp != null);

      Variable tmp;
      if (exp.IsVariable && exp.TryGetFrameworkVariable(out tmp))
      {
        var = new BoxedVariable<Variable>(tmp);
        return true;
      }
      if (exp.IsUnary && exp.UnaryOp == UnaryOperator.Conv_i4 && exp.UnaryArgument.TryGetFrameworkVariable(out tmp))
      {
        var = new BoxedVariable<Variable>(tmp);
        return true;
      }
      if (frameworkVarIsOk && exp.TryGetFrameworkVariable(out tmp))
      {
        var = new BoxedVariable<Variable>(tmp);
        return true;
      }

      var = default(BoxedVariable<Variable>);
      return false;
    }

    [Pure]
    [ContractVerification(true)]
    public static BoxedExpression SimplifyConstantsInDivision(this BoxedExpression exp, Func<long, BoxedExpression> MakeConstant)
    {
      Contract.Requires(exp != null);
      Contract.Requires(MakeConstant != null);
      Contract.Ensures(Contract.Result<BoxedExpression>() != null);

      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Div)
      {
        BinaryOperator bop2;
        BoxedExpression leftLeft, leftRight;
        int down;
        if (left.IsBinaryExpression(out bop2, out leftLeft, out leftRight) && bop2 == BinaryOperator.Mul && right.IsConstantInt(out down) && down != 0)
        {
          BoxedExpression newLeft = null;
          int k;
          if (leftLeft.IsConstantInt(out k))
          {
            newLeft = leftRight;
          }
          else if (leftRight.IsConstantInt(out k))
          {
            newLeft = leftLeft;
          }

          if (newLeft != null &&  k != Int32.MinValue && down != Int32.MinValue)
          {
            var gcd = Rational.GCD(Math.Abs(k), Math.Abs(down));
            if (gcd != 1)
            {
              var new_k = k / gcd;
              var new_down = down / gcd;
              var new_Upp = new_k == 1 ? newLeft : BoxedExpression.Binary(BinaryOperator.Mul, MakeConstant(new_k), newLeft);

              return new_down == 1 ? new_Upp : BoxedExpression.Binary(BinaryOperator.Div, new_Upp, MakeConstant(new_down).AssumeNotNull());

            }
          }
        }
      }

      return exp;
    }

    public static BoxedExpression Rename<Variable>(this BoxedExpression original, 
      Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> renaming)
    {
      Contract.Requires(renaming != null);

      if (original == null)
        return original;

      UnaryOperator uop;
      BinaryOperator bop;
      BoxedExpression left, right;

      if (original.IsBinaryExpression(out bop, out left, out right))
      {
        var exp1 = Rename(left, renaming);

        if (exp1 == null)
          return null;

        var exp2 = Rename(right, renaming);

        if (exp2 == null)
          return null;

        return BoxedExpression.Binary(bop, exp1, exp2);
      }

      if (original.IsUnaryExpression(out uop, out left))
      {
        var exp1 = Rename(left, renaming);

        if (exp1 == null)
          return null;

        return BoxedExpression.Unary(uop, exp1);
      }

      if (original.IsVariable)
      {
        Variable v;
        if (original.TryGetFrameworkVariable(out v))
        {
          FList<BoxedVariable<Variable>> newNames;
          if (renaming.TryGetValue(new BoxedVariable<Variable>(v), out newNames) && newNames.Length() == 1)
          {
            return BoxedExpression.Var(newNames.Head);
          }
          else
          {
            return null;
          }
        }
        else
        {
          return original;
        }
      }

      return original;
    }

    public static bool HasVariableRootedInThis(this BoxedExpression original)
    {
      if (original == null)
      {
        return false;
      }

      var visitor = new HasVariableRootedInThisVisitor();
      original.Dispatch(visitor);

      return visitor.result;
    }

    public static BoxedExpression ReplaceVariable(this BoxedExpression original, object prevVar, object newVar)
    {
      if (original == null || prevVar == null || newVar == null)
      {
        return null;
      }

      UnaryOperator uop;
      BinaryOperator bop;
      BoxedExpression left, right;

      if (original.IsBinaryExpression(out bop, out left, out right))
      {
        var exp1 = ReplaceVariable(left, prevVar, newVar);

        if (exp1 == null)
          return null;

        var exp2 = ReplaceVariable(right, prevVar, newVar);

        if (exp2 == null)
          return null;

        return BoxedExpression.Binary(bop, exp1, exp2);
      }

      if (original.IsUnaryExpression(out uop, out left))
      {
        var exp1 = ReplaceVariable(left, prevVar, newVar);

        if (exp1 == null)
          return null;

        return BoxedExpression.Unary(uop, exp1);
      }

      if (original.IsVariable && original.UnderlyingVariable.Equals(prevVar))
      {
        return BoxedExpression.Var(newVar);
      }

      return original;
    }

    public static BoxedExpression Concatenate(this IEnumerable<BoxedExpression> collection, BinaryOperator bop)
    {
      BoxedExpression result = null;
      foreach (var exp in collection)
      {
        if (result == null)
        {
          result = exp;
        }
        else
        {
          result = BoxedExpression.Binary(bop, result, exp);
        }
      }

      return result;
    }

    /// <summary>
    /// If <code>premise</code> is a variable, then it reintroduces the Boolean expression by turning  <code>premise </code> into 
    /// <code>condition != 0</code> (or <code>condition != null</code> if we have enough type information)
    /// </summary>
    public static BoxedExpression MakeNotEqualToZero<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this BoxedExpression premise, 
      FlatDomain<Type> type, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      where Type : System.IEquatable<Type>
    {
      Contract.Requires(mdDecoder != null);
      Contract.Requires(premise != null);

      Contract.Ensures(Contract.Result<BoxedExpression>() != null);

      if (premise.IsVariable)
      {
        object value;
        Type valueType;
        if (type.IsNormal)
        {
          if(mdDecoder.System_Boolean.Equals(type.Value))
          {
            return BoxedExpression.UnaryLogicalNot(premise);
          }
          // premise != null
          if (mdDecoder.IsReferenceType(type.Value))
          {
            value = null;
          }
          else // premise != 0
          {
            value = 0;
          }
          valueType = type.Value;
        }
        else
        {
          value = null;
          valueType = mdDecoder.System_Object;
        }

        return BoxedExpression.Binary(BinaryOperator.Cne_Un, premise, BoxedExpression.Const(value, valueType, mdDecoder));
      }

      return premise;
    }

    #region Private visitors
    
    class HasVariableRootedInThisVisitor 
      : IBoxedExpressionVisitor
    {
      public bool result;

      public HasVariableRootedInThisVisitor()
      {
        result = false;
      }

      #region IBoxedExpressionVisitor Members

      public void Variable(object var, PathElement[] path, BoxedExpression parent)
      {
        // filters this
        if (path.Length >= 3 && path[0].ToString() == "this")
          result = true;
      }

      public void Constant<Type>(Type type, object value, BoxedExpression parent)
      {
      }

      public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
      {
        left.Dispatch(this);
        right.Dispatch(this);
      }

      public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

      public void SizeOf<Type>(Type type, int sizeAsConstan, BoxedExpression parentt)
      {
      }

      public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

      public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
      {
        array.Dispatch(this);
        index.Dispatch(this);
      }

      public void Result<Type>(Type type, BoxedExpression parent)
      {
      }

      public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
      {
        expression.Dispatch(this);
      }

      public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
      {
        expression.Dispatch(this);
      }

      public void Assert(BoxedExpression condition, BoxedExpression parent)
      {
        condition.Dispatch(this);
      }

      public void Assume(BoxedExpression condition, BoxedExpression parent)
      {
        condition.Dispatch(this);
      }

      public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
      {
        foreach (var statement in statements.Enumerate())
        {
          statement.Dispatch(this);
        }
      }

      public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
      {
        boundVariable.Dispatch(this);
        lower.Dispatch(this);
        upper.Dispatch(this);
        body.Dispatch(this);
      }

      #endregion
    }

    #endregion
  }

  public static class SetOfConstraintsExtensions
  {
    public static SetOfConstraints<BoxedExpression> RenameVariable(
      this SetOfConstraints<BoxedExpression> soc, object oldVariable, object slack)
    {
      Contract.Requires(soc != null);
      Contract.Ensures(Contract.Result<SetOfConstraints<BoxedExpression>>() != null);

      if (soc.IsNormal())
      {
        var result = new Set<BoxedExpression>();
        foreach (var exp in soc.Values)
        {
          var renamedExp = exp.ReplaceVariable(oldVariable, slack);

          result.AddIfNotNull(renamedExp);
        }

        return result.Count > 0 ? new SetOfConstraints<BoxedExpression>(result, false) : SetOfConstraints<BoxedExpression>.Unknown;
      }

      return soc;
    }
  }

  public static class ListExtensions
  {
    static public List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>
      ToNormalizedExpressions<Variable>(this List<Pair<BoxedVariable<Variable>, Int32>> intConsts)
    {
      Contract.Requires(intConsts != null);
      Contract.Ensures(Contract.Result<List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>>() != null);

      var result = new List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>(intConsts.Count);

      foreach (var pair in intConsts)
      {
        var var = NormalizedExpression<BoxedVariable<Variable>>.For(pair.One);
        var val = NormalizedExpression<BoxedVariable<Variable>>.For(pair.Two);

        result.Add(var, val);
      }

      return result;
    }

    static public List<U> ConvertAllDroppingNulls<T, U>(this IList<T> input, Converter<T, U> convert)
      where T: class
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<List<U>>() != null);
      Contract.Ensures(Contract.Result<List<U>>().Count <= input.Count);
      Contract.Ensures(Contract.ForAll(Contract.Result<List<U>>(), el => el != null));

      var result = new List<U>(input.Count);

      foreach (var el in input)
      {
        var converted = convert(el);
        if (converted != null)
        {
          result.Add(converted);
        }
      }

      return result;
    }

    static public bool AddIfNotNull<T>(this List<T> list, T t)
     where T : class
    {
      Contract.Requires(list != null);

      if (t != null)
      {
        list.Add(t);
        return true;
      }
      else
      {
        return false;
      }
    }

    static public T ExtractFirst<T>(this List<T> list)
    {
      Contract.Requires(list != null);
      Contract.Requires(list.Count > 0);

      var first = list[0];
      list.RemoveAt(0);

      return first;
    }

    static public List<T> AddFunctional<T>(this List<T> list, T element)
    {
      if (list == null)
        return list;

      var copy = new List<T>(list);
      copy.Add(element);

      return copy;
    }
  }

  public static class DictionaryExtensions
  {
    public static void AddOrUpdate<K, V>(this IDictionary<K, List<V>> dict, K key, List<V> values)
    {
      Contract.Requires(dict != null);
      Contract.Requires(values != null);

      List<V> prevValues;
      if (dict.TryGetValue(key, out prevValues))
      {
        var newValues = new List<V>(prevValues);
        newValues.AddRange(values);

        dict[key] = newValues;
      }
      else
      {
        dict[key] = values;
      }
    }
  }

  public static class IEnumerableExtensions
  {
    public static bool Single<T>(this IEnumerable<T> enumerable, out T value)
    {
      Contract.Requires(enumerable != null);

      var count = 0;
      value = default(T);
      foreach (var el in enumerable)
      {
        count++;
 
        value = el;

        if (count != 1)
          break;
      }

      return count == 1;
    }

    public static IEnumerable<KeyValuePair<TKey, TValueResult>> Select<TKey, TValueSource, TValueResult>(this IEnumerable<KeyValuePair<TKey, TValueSource>> source, Func<TValueSource, TValueResult> selector)
    {
      return source.Select(p => new KeyValuePair<TKey, TValueResult>(p.Key, selector(p.Value)));
    }

    public static IEnumerable<KeyValuePair<TKey, TValueResult>> Select<TKey, TValueSource, TValueResult>(this IEnumerable<KeyValuePair<TKey, TValueSource>> source, Func<TKey, TValueSource, TValueResult> selector)
    {
      return source.Select(p => new KeyValuePair<TKey, TValueResult>(p.Key, selector(p.Key, p.Value)));
    }

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
      return source.ToDictionary(p => p.Key, p => p.Value);
    }

    public static string ToString<T>(this IList<T> sequence, string separator)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      if(sequence == null || sequence.Count == 0)
      {
        return "<empty sequence>";
      }

      var result = new StringBuilder();
      for(var i = 0; i < sequence.Count-1; i++)
      {
        result.AppendFormat("{0}{1}", sequence[i], separator);
      }
      result.Append(sequence[sequence.Count - 1]);

      return result.ToString();
    }
  }


  public static class FListExtensions
  {
    static public bool IsAnAccessPathFromOutParam(this FList<PathElement> ap)
    {
      if (ap == null || ap.Length() <= 2)
      {
        return false;
      }

      var head = ap.Head;

      return head.IsParameter && head.IsParameterRef && head.IsManagedPointer;
    }
  }
}