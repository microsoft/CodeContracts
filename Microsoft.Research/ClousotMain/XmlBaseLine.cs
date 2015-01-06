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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// Filters output based on an existing baseline. If an outcome is in the baseline, it is not emitted.
  /// </summary>
  class XmlBaseLine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> 
  {
    protected readonly IOutputFullResults<Method, Assembly> output;
    protected readonly GeneralOptions options;
    protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

    public XmlBaseLine(IOutputFullResults<Method, Assembly> output, GeneralOptions options, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      this.output = output;
      this.options = options;
      this.mdDecoder = mdDecoder;
    }

    public static string CanonicalMethodName(Method method, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder)
    {
      StringBuilder sb = new StringBuilder();

      CanonicalTypeDefName(decoder.DeclaringType(method), sb, decoder);

      sb.AppendFormat(".{0}(", decoder.Name(method));
      var parameters = decoder.Parameters(method);
      for (int i = 0; i < parameters.Count; i++)
      {
        Type parameterType = decoder.ParameterType(parameters[i]);
        CanonicalTypeRefName(parameterType, sb, decoder);
        if (i < parameters.Count - 1) { sb.Append(','); }
      }
      sb.Append(')');
      return sb.ToString();
    }

    protected static void CanonicalTypeDefName(Type type, StringBuilder sb, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder)
    {
      Type parentType;
      if (decoder.IsNested(type, out parentType))
      {
        CanonicalTypeDefName(parentType, sb, decoder);
        sb.Append('/');
      }
      else
      {
        string ns = decoder.Namespace(type);
        if (ns != null && ns != "")
        {
          sb.AppendFormat("{0}.", ns);
        }
      }
      sb.AppendFormat("{0}", decoder.Name(type));
    }

    private static void CanonicalTypeRefName(Type type, StringBuilder sb, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder)
    {
      if (decoder.IsArray(type))
      {
        CanonicalTypeRefName(decoder.ElementType(type), sb, decoder);
        sb.Append("[]");
        return;
      }
      if (decoder.IsManagedPointer(type))
      {
        sb.Append("ref ");
        CanonicalTypeRefName(decoder.ElementType(type), sb, decoder);
        return;
      }
      if (decoder.IsFormalTypeParameter(type))
      {
        sb.Append(decoder.Name(type));
        return;
      }
      if (decoder.IsMethodFormalTypeParameter(type))
      {
        sb.Append(decoder.Name(type));
        return;
      }
      IIndexable<Type> typeArgs;
      if (decoder.IsSpecialized(type, out typeArgs))
      {
        CanonicalTypeRefName(decoder.Unspecialized(type), sb, decoder);
        sb.Append('<');
        for (int i = 0; i < typeArgs.Count; i++)
        {
          CanonicalTypeRefName(typeArgs[i], sb, decoder);
          if (i < typeArgs.Count - 1)
          {
            sb.Append(',');
          }
        }
        return;
      }
      if (decoder.IsUnmanagedPointer(type))
      {
        CanonicalTypeRefName(decoder.ElementType(type), sb, decoder);
        sb.Append("*");
        return;
      }
      Type modified;
      IIndexable<Pair<bool, Type>> modifiers;
      if (decoder.IsModified(type, out modified, out modifiers))
      {
        CanonicalTypeRefName(modified, sb, decoder);
        for (int i = 0; i < modifiers.Count; i++)
        {
          if (modifiers[i].One)
          {
            sb.Append(" opt(");
          }
          else
          {
            sb.Append(" req(");
          }
          CanonicalTypeRefName(modifiers[i].Two, sb,decoder);
          sb.Append(')');
        }
        return;
      }
      CanonicalTypeDefName(type, sb, decoder);
    }



  }


}
