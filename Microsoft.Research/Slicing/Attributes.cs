// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private static Pair<object, MethodHashAttribute> t_cache;

        public static bool TryGetMethodHashAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
          Method method,
          out MethodHashAttribute mhAttr)
        {
            Contract.Requires(metaDataDecoder != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out mhAttr) != null);

#if false // dead code???
      if (false && method.Equals(cache.One)) // problem: successive analyses are run in the same worker
      {
        mhAttr = cache.Two;
        return mhAttr != null;
      }
#endif
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

            t_cache = Pair.For<object, MethodHashAttribute>(method, mhAttr);

            return mhAttr != null;
        }

#if false
    public static ThreeValued MethodHashAttributeDoAnalyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Method method)
    {
      MethodHashAttribute mhAttr;
      if (metaDataDecoder.TryGetMethodHashAttribute(method, out mhAttr))
        return new ThreeValued(mhAttr.DoAnalyze);
      return new ThreeValued();
    }
#endif

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
