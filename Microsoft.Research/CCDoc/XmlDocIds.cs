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

// Copyright (c) Microsoft Corporation.  All rights reserved.
//

using System;
// using System.Collections.Generic;
using System.IO;

using Microsoft.Cci;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CCDoc
{
  [ContractVerification(true)]
  internal class XmlDocIds
  {
    public static string GetMemberId(ITypeDefinitionMember member)
    {
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<string>() != null);

      using (TextWriter writer = new StringWriter())
      {
        WriteMember(member, writer);
        return (writer.ToString());
      }
    }

    private static void WriteMember(ITypeDefinitionMember member, TextWriter writer)
    {
      Contract.Requires(member != null);
      Contract.Requires(writer != null);

      IMethodDefinition method = member as IMethodDefinition;
      if (method != null)
      {
        writer.Write("M:");
        WriteMethod(method, writer);
        return;
      }

      IFieldDefinition field = member as IFieldDefinition;
      if (field != null)
      {
        writer.Write("F:");
        WriteField(field, writer);
        return;
      }

      IPropertyDefinition property = member as IPropertyDefinition;
      if (property != null)
      {
        writer.Write("P:");
        WriteProperty(property, writer);
        return;
      }

      IEventDefinition eventdef = member as IEventDefinition;
      if (eventdef != null)
      {
        writer.Write("E:");
        WriteEvent(eventdef, writer);
        return;
      }
      throw new NotImplementedException("missing case");
    }

    public static string GetTypeId(ITypeReference type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<string>() != null);

      using (TextWriter writer = new StringWriter())
      {
        writer.Write("T:");
        WriteType(type, writer);
        return (writer.ToString());
      }
    }



    private static void WriteEvent(IEventDefinition trigger, TextWriter writer)
    {
      Contract.Requires(trigger != null);
      Contract.Requires(writer != null);

      Contract.Assume(trigger.ContainingType != null, "lack of CCI2 contracts");
      Contract.Assume(trigger.Name != null, "lack of CCI2 contracts");
      Contract.Assume(trigger.Name.Value != null, "lack of CCI2 contracts");

      WriteType(trigger.ContainingType, writer);

      var name = SanitizeMemberName(trigger.Name.Value);

      writer.Write(".{0}", name);

    }

    private static void WriteField(IFieldDefinition field, TextWriter writer)
    {
      Contract.Requires(field != null);
      Contract.Requires(writer != null);

      Contract.Assume(field.ContainingType != null, "lack of CCI2 contracts");

      WriteType(field.ContainingType, writer);
      writer.Write(".{0}", field.Name.Value);
    }

    private static void WriteMethod(IMethodDefinition method, TextWriter writer)
    {
      Contract.Requires(method != null);
      Contract.Requires(writer != null);

      Contract.Assume(method.ContainingType != null, "lack of CCI2 contracts");
      Contract.Assume(method.Parameters != null, "lack of CCI2 contracts");
      Contract.Assume(method.Name != null, "lack of CCI2 contracts");
      Contract.Assume(method.Name.Value != null, "lack of CCI2 contracts");
      Contract.Assume(method.Type != null, "lack of CCI2 contracts");

      WriteType(method.ContainingType, writer);

      if (method.IsConstructor)
      {
        if (method.IsStatic)
        {
          writer.Write(".#cctor");
        }
        else
        {
          writer.Write(".#ctor");
        }
        WriteParameters(method.Parameters, writer);
      }
      else
      {
        string name = method.Name.Value;
        name = SanitizeMemberName(name);

        writer.Write(".{0}", name);

        if (method.IsGeneric)
        {
          writer.Write("``{0}", method.GenericParameterCount);
        }
        WriteParameters(method.Parameters, writer);
        // add ~ for conversion operators
        
        // Doublecheck that CCI gives us these names.
        if ((name == "op_Implicit") || (name == "op_Explicit"))
        {
          writer.Write("~");
          WriteType(method.Type, writer);
        }
      }
    }

    private static string SanitizeMemberName(string name)
    {
      Contract.Requires(name != null);

      name = name.Replace(".", "#");
      name = name.Replace("<", "{");
      name = name.Replace(">", "}");
      name = name.Replace(',', '@'); //change the seperator between params

      return name;
    }


    private static void WriteParameters(IEnumerable<IParameterDefinition> parameters, TextWriter writer)
    {
      Contract.Requires(parameters != null);
      Contract.Requires(writer != null);

      bool first = true;
      foreach (var par in parameters)
      {
        Contract.Assume(par != null, "lack of collection and CCI2 contracts");
        Contract.Assume(par.Type != null, "lack of CCI2 contracts");

        if (first)
        {
          writer.Write("(");
          first = false;
        }
        else
        {
          writer.Write(",");
        }
        WriteType(par.Type, writer);
      }
      if (!first)
      {
        writer.Write(")");
      }
    }

    private static void WriteProperty(IPropertyDefinition property, TextWriter writer)
    {
      Contract.Requires(property != null);
      Contract.Requires(writer != null);

      Contract.Assume(property.ContainingType != null, "lack of CCI2 contracts");
      Contract.Assume(property.Name != null, "lack of CCI2 contracts");
      Contract.Assume(property.Name.Value != null, "lack of CCI2 contracts");
      Contract.Assume(property.Parameters != null, "lack of CCI2 contracts");

      WriteType(property.ContainingType, writer);

      string name = property.Name.Value;

      name = SanitizeMemberName(name);

      writer.Write(".{0}", name);

      WriteParameters(property.Parameters, writer);
    }



    private static void WriteNamespaceAndSeparator(IUnitNamespaceReference ns, TextWriter writer)
    {
      Contract.Requires(ns != null);
      Contract.Requires(writer != null);

      INestedUnitNamespaceReference nested = ns as INestedUnitNamespaceReference;
      if (nested == null) return; // top-level

      Contract.Assume(nested.ContainingUnitNamespace != null, "lack of CCI2 contracts");

      WriteNamespaceAndSeparator(nested.ContainingUnitNamespace, writer);
      writer.Write(nested.Name.Value);
      writer.Write(".");
    }

    private static void WriteType(ITypeReference type, TextWriter writer)
    {
      Contract.Requires(type != null);
      Contract.Requires(writer != null);

      WriteType(type, writer, false);
    }

    private static void WriteType(ITypeReference type, TextWriter writer, bool omitOutermostTypeFormals)
    {
      Contract.Requires(type != null);
      Contract.Requires(writer != null);

      IArrayType array = type as IArrayType;
      if (array != null)
      {
        Contract.Assume(array.ElementType != null, "lack of CCI2 contracts");

        WriteType(array.ElementType, writer);
        writer.Write("[");
        if (array.Rank > 1)
        {
          for (int i = 0; i < array.Rank; i++)
          {
            if (i > 0) writer.Write(",");
            writer.Write("0:");
          }
        }
        writer.Write("]");
        return;
      }

      IManagedPointerTypeReference reference = type as IManagedPointerTypeReference; ;
      if (reference != null)
      {
        Contract.Assume(reference.TargetType != null, "lack of CCI2 contracts");

        var referencedType = reference.TargetType;
        WriteType(referencedType, writer);
        writer.Write("@");
        return;
      }

      IPointerTypeReference pointer = type as IPointerTypeReference;
      if (pointer != null)
      {
        Contract.Assume(pointer.TargetType != null, "lack of CCI2 contracts");

        WriteType(pointer.TargetType, writer);
        writer.Write("*");
        return;
      }

      IModifiedTypeReference modref = type as IModifiedTypeReference;
      if (modref != null)
      {
        Contract.Assume(modref.UnmodifiedType != null, "lack of CCI2 contracts");
        Contract.Assume(modref.CustomModifiers != null, "lack of CCI2 contracts");

        WriteType(modref.UnmodifiedType, writer);
        foreach (var modifier in modref.CustomModifiers)
        {
          Contract.Assume(modifier != null, "lack of collection contracts and CCI2 contracts");
          Contract.Assume(modifier.Modifier != null, "lack of CCI2 contracts");

          if (modifier.IsOptional)
          {
            writer.Write("!");
          }
          else
          {
            writer.Write("|");
          }
          WriteType(modifier.Modifier, writer);
        }
        return;
      }

      IGenericTypeParameterReference gtp = type as IGenericTypeParameterReference;
      if (gtp != null)
      {
        writer.Write("`");
        writer.Write(gtp.Index);
        return;
      }
      IGenericMethodParameterReference gmp = type as IGenericMethodParameterReference;
      if (gmp != null)
      {
        writer.Write("``");
        writer.Write(gmp.Index);
        return;
      }

      IGenericTypeInstanceReference instance = type as IGenericTypeInstanceReference;
      if (instance != null)
      {
        Contract.Assume(instance.GenericType != null, "lack of CCI2 contracts");
        Contract.Assume(instance.GenericArguments != null, "lack of CCI2 contracts");

        WriteType(instance.GenericType, writer, true);
        writer.Write("{");
        var first = true;
        foreach (var arg in instance.GenericArguments)
        {
          Contract.Assume(arg != null, "lack of collection and CCI2 contracts");

          if (first)
          {
            first = false;
          }
          else { writer.Write(","); }
          WriteType(arg, writer);
        }
        writer.Write("}");
        return;
      }

      // namespace or nested
      INamedTypeReference named = (INamedTypeReference)type;
      INestedTypeReference nested = type as INestedTypeReference;
      if (nested != null)
      {
        Contract.Assume(nested.ContainingType != null, "lack of CCI2 contracts");

        // names of nested types begin with outer type name
        WriteType(nested.ContainingType, writer);
        writer.Write(".");
        // continue to write type sig
      }

      INamespaceTypeReference nt = type as INamespaceTypeReference;
      if (nt != null)
      {
        Contract.Assume(nt.ContainingUnitNamespace != null, "lack of CCI2 contracts");

        WriteNamespaceAndSeparator(nt.ContainingUnitNamespace, writer);
        // continue to write type sig
      }
      // name
      writer.Write(named.Name.Value);
      // generic parameters
      if (omitOutermostTypeFormals) return;

      if (named.GenericParameterCount > 0)
      {
        writer.Write("`{0}", named.GenericParameterCount);
      }
    }
  }

}

