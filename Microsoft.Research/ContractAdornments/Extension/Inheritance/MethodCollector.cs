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

using System.Collections.Generic;
using Microsoft.RestrictedUsage.CSharp.Syntax;
using Microsoft.RestrictedUsage.CSharp.Extensions;
using System;
using Microsoft.VisualStudio.Text;
using System.Linq;
using ContractAdornments;
using Microsoft.RestrictedUsage.CSharp.Core;
using System.Text;
using System.Diagnostics.Contracts;

namespace ContractAdornments {
  public class MethodCollector : ParseTreeVisitor {
    public const string MethodTagSuffix = "KIND:METHOD!";

    readonly IDictionary<object, MethodDeclarationNode> _methods;
    ITextSnapshot _snapshot;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_methods != null);
    }

    public MethodCollector(ITextSnapshot snapshot)
      : base() {
      _snapshot = snapshot;
      _methods = new Dictionary<object, MethodDeclarationNode>();
    }

    public override void VisitMethodDeclarationNode(MethodDeclarationNode node)
    {
        if (_snapshot == null) return;
        base.VisitMethodDeclarationNode(node);
        var key = GenerateTag(node, _snapshot);
        if (key == null)
        {
            //We can't get a proper tag from this (likely ill-formed) method, so we'll skip it for now.
            var name = node.GetName(_snapshot);
            VSServiceProvider.Current.Logger.WriteToLog("Can't form a proper tag (likely ill-formed), ignoring member '" + name == null ? "" : name + "' for now...");
            return;
        }
        if (_methods.ContainsKey(key))
        {
            //For some reason, we have two methods with the same signature. There is 
            //nothing we can do in this case so we just throw out the second method.
            VSServiceProvider.Current.Logger.WriteToLog("Two methods where found to have the exact same signature, ignoring second method for now...");
            return;
        }
        _methods.Add(key, node);
    }

    public IDictionary<object, MethodDeclarationNode> GetMethods() {
      Contract.Ensures(Contract.Result<IDictionary<object, MethodDeclarationNode>>() != null);

      return new Dictionary<object, MethodDeclarationNode>(_methods);
    }

    public void Clear() {
      _methods.Clear();
      _snapshot = null;
    }

    public static object GenerateTag(MethodDeclarationNode method, ITextSnapshot snapshot) {
      Contract.Requires(method != null);
      Contract.Requires(snapshot != null);

      var sb = new StringBuilder();

      //Append our name
      var name = method.GetName(snapshot);
      if (name == null)
        return null;
      sb.Append(name);
      sb.Append('!');

      //Append our return type
      if (method.ReturnType != null) {
        var rn = method.ReturnType.GetName(snapshot);
        if (rn == null) 
          rn = "NONAME";
        sb.Append(rn);
        sb.Append('!');
      }

      //Append parameters
      if (method.FormalParameterList != null) {
        foreach (var param in method.FormalParameterList) {

          //AppendParamter nameS
          if (param.Identifier != null && param.Identifier.Name != null) {
            sb.Append(param.Identifier.Name.Text);
            sb.Append('!');
          }

          //Append parameter type
          if (param.Type != null) {
            var tn = param.Type.GetName(snapshot);
            if (tn != null) {
              sb.Append(tn);
              sb.Append('!');
            }
          }

          //Append attributes?
          if (param.Attributes != null && param.Attributes.Count > 0) {
          }

          //Append flags
          if (param.Flags != default(NodeFlags)) {
            sb.Append(param.Flags);
            sb.Append('!');
          }
        }
      }

      //var snapshotSpan = method.GetSpan().Convert(snapshot);
      //var methodText = snapshotSpan.GetText();
      //string methodHeader;
      //if (methodText.Contains('{')) {
      //  methodHeader = methodText.Substring(0, methodText.IndexOf('{')).Trim();
      //} else if (methodText.Contains(';')) {
      //  methodHeader = methodText.Substring(0, methodText.IndexOf(';')).Trim();
      //} else
      //  return null;
      //sb.Append(methodHeader);
      //sb.Append('!');

      //Apend parent information
      var containingType = method.Parent;
      while (containingType != null) {
        var asClass = containingType.AsClass();
        if (asClass != null) {
          sb.Append(asClass.Identifier.Name.Text);
          sb.Append('!');
          goto EndOfLoop;
        }
        var asInterface = containingType.AsInterface();
        if (asInterface != null) {
          sb.Append(asInterface.Identifier.Name.Text);
          sb.Append('!');
          goto EndOfLoop;
        }
      /*Note: there is nothing we can do about the namespace, we can't seem to get namespace name info from the syntactic model. 
       */
      EndOfLoop:
        containingType = containingType.Parent;
      }

      //Append flags
      if (method.Flags != default(NodeFlags)) {
        sb.Append(method.Flags);
        sb.Append('!');
      }

      //Append what kind of node we are
      sb.Append(MethodTagSuffix);

      return sb.ToString();
    }
  }
}
