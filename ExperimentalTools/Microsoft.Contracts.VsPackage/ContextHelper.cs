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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace Microsoft.Contracts {

  static class ContextHelper
  {

    /// <summary>
    /// Code taken from Pex
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool TryGetSelectionPoint(IServiceProvider serviceProvider, out VirtualPoint point)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Ensures(Contract.ValueAtReturn(out point) != null || !Contract.Result<bool>());

      point = null;

      var dte = VsServiceProviderHelper.GetService<DTE>(serviceProvider);

      if (dte == null)
      {
        return false;
      }

      try
      {
        // code window supported only
        Document document = dte.ActiveDocument;
        if (document != null)
        {
          TextDocument tdoc = document.Object("TextDocument") as TextDocument;
          if (tdoc != null)
          {
            try
            {
              Contract.Assume(tdoc.Selection != null);
              point = tdoc.Selection.TopPoint;
            }
            catch (COMException)
            { }
          }
        }
      }
      catch (ArgumentException) { } // reported by user
      catch (COMException) { }

      return point != null;
    }

    /// <summary>
    /// Code stolen from Pex.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool TryGetSelectedElement<T>(
        IServiceProvider serviceProvider,
        vsCMElement elementType, out T element)
        where T : class
    {
      Contract.Requires(serviceProvider != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out element) != default(T));

      element = default(T);

      try
      {
        VirtualPoint point;
        if (ContextHelper.TryGetSelectionPoint(serviceProvider, out point))
          element = point.get_CodeElement(elementType) as T;
      }
      catch (COMException)
      { }

      return element != null;
    }

    private static bool TryMangleNamespaceName(CodeNamespace codeNamespace, out string mangled)
    {
      Contract.Requires(codeNamespace != null);

      mangled = null;
      string res = codeNamespace.FullName;
      Contract.Assume(res != null);

      switch(codeNamespace.Language) {
        case CodeModelLanguageConstants.vsCMLanguageVB:
          res = res.Replace("[", "").Replace("]", "");
          break;
        case CodeModelLanguageConstants.vsCMLanguageCSharp:
          res = res.Replace("@", "");
          break;
        default :
          return false;
      }

      mangled = res;
      return true;
    }

    public static bool TryGetSelectedNamespaceFullNameMangled(IServiceProvider serviceProvider, out string fullName)
    {
      Contract.Requires(serviceProvider != null);

      fullName = null;
      CodeNamespace codeNamespace;
      if(!TryGetSelectedElement<CodeNamespace>(serviceProvider, vsCMElement.vsCMElementNamespace, out codeNamespace))
        return false;
      
      return TryMangleNamespaceName(codeNamespace, out fullName);
    }

    /// <summary>
    /// Do not handle generic types nor array types
    /// </summary>
    /// <param name="codeType"></param>
    /// <param name="mangled"></param>
    /// <returns></returns>
    private static bool TryMangleTypeName(CodeType codeType, out string mangled)
    {
      Contract.Requires(codeType != null);

      mangled = null;

      string namespaceFullName = codeType.Namespace == null ? "" : codeType.Namespace.FullName;

      if (namespaceFullName == null)
      {
        return false;
      }

      string name = codeType.FullName;
      Contract.Assume(name != null);
      if(namespaceFullName != "" && !name.StartsWith(namespaceFullName + "."))
        return false;
      int startIndex = namespaceFullName == "" ? 0 : namespaceFullName.Length + 1;
      Contract.Assert(startIndex <= name.Length);
      name = name.Substring(startIndex);

      string genericsMarker;
      switch(codeType.Language) {
        case CodeModelLanguageConstants.vsCMLanguageVB:
          name = name.Replace("[", "").Replace("]", "");
          genericsMarker = "(Of";
          break;
        case CodeModelLanguageConstants.vsCMLanguageCSharp:
          name = name.Replace("@", "");
          genericsMarker = "<";
          break;
        default :
          return false;
      }

      string namespaceMangled = null;
      if(codeType.Namespace != null && !TryMangleNamespaceName(codeType.Namespace, out namespaceMangled))
        return false;

      namespaceMangled = namespaceMangled == null ? "" : namespaceMangled + ".";

      mangled = String.Format("{0}{1}", namespaceMangled, String.Join("+", name
        .Split('.')
        .Select(className =>
          {
            int startGenerics = className.LastIndexOf(genericsMarker);
            if (startGenerics >= 0)
              return className.Substring(0, startGenerics) + '`' + (className.Count(c => c == ',')+1).ToString();
            return className;
          })));

      return true;
    }

    public static bool TryGetSelectedTypeFullNameMangled(IServiceProvider serviceProvider, out string fullName)
    {
      Contract.Requires(serviceProvider != null);

      fullName = null;

      CodeType codeType;
      if (!TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementClass, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementStruct, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementInterface, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementDelegate, out codeType))
      {
        return false;
      }

      return TryMangleTypeName(codeType, out fullName);
    }

    /// <summary>
    /// Works for methods, properties, events, constructors, destructors.
    /// Do NOT work for fields.
    /// Do NOT work for operators (TODO).
    /// Does not contain the function parameters types (TODO).
    /// </summary>
    public static bool TryGetSelectedMemberFullNameMangled(IServiceProvider serviceProvider, out string fullName)
    {
      Contract.Requires(serviceProvider != null);

      fullName = null;
      
      CodeElement2 codeElmt;
      if (!TryGetSelectedElement<CodeElement2>(serviceProvider, vsCMElement.vsCMElementFunction, out codeElmt) &&
          !TryGetSelectedElement<CodeElement2>(serviceProvider, vsCMElement.vsCMElementProperty, out codeElmt) &&
          !TryGetSelectedElement<CodeElement2>(serviceProvider, vsCMElement.vsCMElementEvent, out codeElmt))
        return false;

      string name = codeElmt.Name;
      Contract.Assume(name != null);

      var codeFunc = codeElmt as CodeFunction2;
      if (codeFunc != null && (codeFunc.FunctionKind & vsCMFunction.vsCMFunctionConstructor) != 0)
        name = codeFunc.IsShared ? ".cctor" : ".ctor";
      else if (codeFunc != null && (codeFunc.FunctionKind & vsCMFunction.vsCMFunctionDestructor) != 0)
        name = "Finalize";
      else if (codeFunc != null && (codeFunc.FunctionKind & vsCMFunction.vsCMFunctionOperator) != 0)
        return false; // TODO
      else switch (codeElmt.Language)
      {
        case CodeModelLanguageConstants.vsCMLanguageVB:
          name = name.Replace("[", "").Replace("]", "");
          break;
        case CodeModelLanguageConstants.vsCMLanguageCSharp:
          if (name == "this") // hack for indexer
            name = "Item";
          name = name.Replace("@", "");
          break;
        default:
          return false;
      }

      CodeType codeType;
      if (!TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementClass, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementStruct, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementInterface, out codeType) &&
          !TryGetSelectedElement<CodeType>(serviceProvider, vsCMElement.vsCMElementDelegate, out codeType))
      {
        return false;
      }
      string typeFullName;
      if (!TryMangleTypeName(codeType, out typeFullName))
        return false;

      fullName = typeFullName + "." + name;

      return true;
    }
  }
}
