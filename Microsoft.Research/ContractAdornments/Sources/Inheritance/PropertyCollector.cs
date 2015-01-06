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
  public class PropertyCollector : ParseTreeVisitor {
    public const string PropertyTagSuffix = "KIND:PROPERTY!";

    readonly IDictionary<Tuple<object, object>, PropertyDeclarationNode> _properties;
    readonly ITextSnapshot _snapshot;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_properties != null);
    }

    public PropertyCollector(ITextSnapshot snapshot)
      : base() {
      _snapshot = snapshot;
      _properties = new Dictionary<Tuple<object, object>, PropertyDeclarationNode>();
    }

    public override void VisitPropertyDeclarationNode(PropertyDeclarationNode node) {
      base.VisitPropertyDeclarationNode(node);
      var keys = GenerateTag(node, _snapshot);
      if (keys == null) {
        //We can't get a proper tag from this (likely ill-formed) property, so we'll skip it for now.
        var name = node.GetName(_snapshot);
        VSServiceProvider.Current.Logger.WriteToLog("Can't form a proper tag (likely ill-formed), ignoring member '" + name == null ? "" : name + "' for now...");
        return;
      }
      if (_properties.ContainsKey(keys)) {
        //For some reason, we have two properties with the same signature. There is 
        //nothing we can do in this case so we just throw out the second property.
        VSServiceProvider.Current.Logger.WriteToLog("Two properties where found to have the exact same signature, ignoring second property for now...");
        return;
      }
      _properties.Add(keys, node);
    }

    public IDictionary<Tuple<object, object>, PropertyDeclarationNode> GetProperties() {
      Contract.Ensures(Contract.Result<IDictionary<Tuple<object, object>, PropertyDeclarationNode>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IDictionary<Tuple<object, object>, PropertyDeclarationNode>>(), (kvp) =>
                          (kvp.Key.Item1 == null) == (kvp.Value.GetAccessorDeclaration == null)
                       && (kvp.Key.Item2 == null) == (kvp.Value.SetAccessorDeclaration == null)
                      ));

      return new Dictionary<Tuple<object, object>, PropertyDeclarationNode>(_properties);
    }

    public static Tuple<object, object> GenerateTag(PropertyDeclarationNode property, ITextSnapshot snapshot) {
      Contract.Requires(property != null);
      Contract.Requires(snapshot != null);
      Contract.Ensures((property.GetAccessorDeclaration == null) || (Contract.Result<Tuple<object, object>>().Item1 != null));
      Contract.Ensures((property.SetAccessorDeclaration == null) || (Contract.Result<Tuple<object, object>>().Item2 != null));

      var sb = new StringBuilder();

      //Append our name
      var name = property.GetName(snapshot);
      if (name != null) {
        sb.Append(name);
        sb.Append('!');
      }

      //Append our return type
      if (property.Type != null) {
        var tn = property.Type.GetName(snapshot);
        if (tn != null) {
          sb.Append(tn);
          sb.Append('!');
        }
      }

      //Append parameters
      if (property.FormalParameterList != null) {
        foreach (var param in property.FormalParameterList) {

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

          //Append flags?
          if (param.Flags != default(NodeFlags)) {
            sb.Append(param.Flags);
            sb.Append('!');
          }
        }
      }

      //Append parent information
      var containingType = property.Parent;
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

      //Append our flags
      if (property.Flags != default(NodeFlags)) {
        sb.Append(property.Flags);
        sb.Append('!');
      }

      //Append what kind of node we are
      sb.Append(PropertyTagSuffix);

      object getterTag = null;
      object setterTag = null;

      if (property.GetAccessorDeclaration != null)
        getterTag = "GETTER!" + sb.ToString();
      if (property.SetAccessorDeclaration != null)
        setterTag = "SETTER!" + sb.ToString();

      //return
      return new Tuple<object, object>(getterTag, setterTag);
    }
  }
}
