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
using System.Linq;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.Slicing
{
  public static partial class NameFor
  {
    public const string ContractClassAttribute = "System.Diagnostics.Contracts.ContractClassAttribute";
    public const string MethodHashAttribute = "MethodHashAttribute";
    public const string MethodHashAttributeConstructor = MethodHashAttribute + "..ctor(System.String, System.Int32)";
    public const string ObjectInvariantMethodName = "ObjectInvariant";
    public const string ObjectInvariantMethodAttribute = "ContractInvariantMethodAttribute";
  }


  static public class IDecodeMetadataExtensionsForContractAttributes
  {
    public static bool IsObjectInvariantMethod<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method)
    {
      Contract.Requires(metaDataDecoder != null);

      if (metaDataDecoder.Name(method) == NameFor.ObjectInvariantMethodName)
        return true;

      return metaDataDecoder.GetAttributes(method).Any(attr => metaDataDecoder.Name(metaDataDecoder.AttributeType(attr)) == NameFor.ObjectInvariantMethodAttribute);
    }

    [ThreadStatic]
    private static Pair<object, MethodHashAttribute> cache; 

    public static bool TryGetMethodHashAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method,
      out MethodHashAttribute mhAttr)
    {
      Contract.Requires(metaDataDecoder != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out mhAttr) != null);

      mhAttr = null;
      foreach (var attr in metaDataDecoder.GetAttributes(method))
      {
        var attrType = metaDataDecoder.AttributeType(attr);
        if (metaDataDecoder.FullName(attrType) == NameFor.MethodHashAttribute)
        {
          mhAttr = MethodHashAttribute.FromDecoder(metaDataDecoder.PositionalArguments(attr));
          if (mhAttr != null)
            break;
        }
      }

      cache = Pair.For<object, MethodHashAttribute>(method, mhAttr);

      return mhAttr != null;
    }

    public static MethodHashAttributeFlags GetMethodHashAttributeFlags<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method,
      MethodHashAttributeFlags def = MethodHashAttributeFlags.Default)
    {
      Contract.Requires(metaDataDecoder != null);

      MethodHashAttribute mhAttr;
      if (metaDataDecoder.TryGetMethodHashAttribute(method, out mhAttr))
        return mhAttr.Flags;
      return def;
    }

    public static bool MethodHashAttributeFirstInOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method)
    {
      Contract.Requires(metaDataDecoder != null);

      return metaDataDecoder.GetMethodHashAttributeFlags(method).HasFlag(MethodHashAttributeFlags.FirstInOrder);
    }

    public static ByteArray MethodHashAttributeHash<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method)
    {
      Contract.Requires(metaDataDecoder != null);

      MethodHashAttribute mhAttr;
      if (metaDataDecoder.TryGetMethodHashAttribute(method, out mhAttr))
        return mhAttr.MethodId;
      return null;
    }

    public static bool IsMethodHashAttributeConstructor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method)
    {
      Contract.Requires(metaDataDecoder != null);

      return metaDataDecoder.FullName(method) == NameFor.MethodHashAttributeConstructor;
    }
  }
}
