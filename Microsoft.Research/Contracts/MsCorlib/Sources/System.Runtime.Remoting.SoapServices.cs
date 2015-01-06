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

// File System.Runtime.Remoting.SoapServices.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Runtime.Remoting
{
  public partial class SoapServices
  {
    #region Methods and constructors
    public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
    {
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
    {
      Contract.Requires(inNamespace != null);

      typeNamespace = default(string);
      assemblyName = default(string);

      return default(bool);
    }

    public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
    {
      type = default(Type);
      name = default(string);
    }

    public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
    {
      type = default(Type);
      name = default(string);
    }

    public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
    {
      return default(Type);
    }

    public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
    {
      return default(Type);
    }

    public static string GetSoapActionFromMethodBase(System.Reflection.MethodBase mb)
    {
      return default(string);
    }

    public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
    {
      Contract.Requires(soapAction != null);

      typeName = default(string);
      methodName = default(string);

      return default(bool);
    }

    public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
    {
      xmlElement = default(string);
      xmlNamespace = default(string);

      return default(bool);
    }

    public static string GetXmlNamespaceForMethodCall(System.Reflection.MethodBase mb)
    {
      return default(string);
    }

    public static string GetXmlNamespaceForMethodResponse(System.Reflection.MethodBase mb)
    {
      return default(string);
    }

    public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
    {
      xmlType = default(string);
      xmlTypeNamespace = default(string);

      return default(bool);
    }

    public static bool IsClrTypeNamespace(string namespaceString)
    {
      Contract.Requires(namespaceString != null);

      return default(bool);
    }

    public static bool IsSoapActionValidForMethodBase(string soapAction, System.Reflection.MethodBase mb)
    {
      Contract.Requires(soapAction != null);

      return default(bool);
    }

    public static void PreLoad(Type type)
    {
    }

    public static void PreLoad(System.Reflection.Assembly assembly)
    {
    }

    public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
    {
    }

    public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
    {
    }

    public static void RegisterSoapActionForMethodBase(System.Reflection.MethodBase mb)
    {
    }

    public static void RegisterSoapActionForMethodBase(System.Reflection.MethodBase mb, string soapAction)
    {
    }

    internal SoapServices()
    {
    }
    #endregion

    #region Properties and indexers
    public static string XmlNsForClrType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string XmlNsForClrTypeWithAssembly
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string XmlNsForClrTypeWithNs
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string XmlNsForClrTypeWithNsAndAssembly
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    #endregion
  }
}
