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

// File System.Web.Configuration.AuthorizationRuleCollection.cs
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


namespace System.Web.Configuration
{
  sealed public partial class AuthorizationRuleCollection : System.Configuration.ConfigurationElementCollection
  {
    #region Methods and constructors
    public void Add(AuthorizationRule rule)
    {
    }

    public AuthorizationRuleCollection()
    {
    }

    public void Clear()
    {
    }

    protected override System.Configuration.ConfigurationElement CreateNewElement()
    {
      return default(System.Configuration.ConfigurationElement);
    }

    protected override System.Configuration.ConfigurationElement CreateNewElement(string elementName)
    {
      return default(System.Configuration.ConfigurationElement);
    }

    public AuthorizationRule Get(int index)
    {
      return default(AuthorizationRule);
    }

    protected override Object GetElementKey(System.Configuration.ConfigurationElement element)
    {
      return default(Object);
    }

    public int IndexOf(AuthorizationRule rule)
    {
      return default(int);
    }

    protected override bool IsElementName(string elementname)
    {
      return default(bool);
    }

    public void Remove(AuthorizationRule rule)
    {
    }

    public void RemoveAt(int index)
    {
    }

    public void Set(int index, AuthorizationRule rule)
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Configuration.ConfigurationElementCollectionType CollectionType
    {
      get
      {
        return default(System.Configuration.ConfigurationElementCollectionType);
      }
    }

    protected override string ElementName
    {
      get
      {
        return default(string);
      }
    }

    public AuthorizationRule this [int index]
    {
      get
      {
        return default(AuthorizationRule);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }
    #endregion
  }
}
