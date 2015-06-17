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

// File System.Windows.Style.cs
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


namespace System.Windows
{
  public partial class Style : System.Windows.Threading.DispatcherObject, System.Windows.Markup.INameScope, System.Windows.Markup.IAddChild, System.Windows.Markup.IHaveResources, System.Windows.Markup.IQueryAmbient
  {
    #region Methods and constructors
    public override int GetHashCode()
    {
      return default(int);
    }

    public void RegisterName(string name, Object scopedElement)
    {
    }

    public void Seal()
    {
    }

    public Style()
    {
    }

    public Style(Type targetType)
    {
    }

    public Style(Type targetType, System.Windows.Style basedOn)
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    Object System.Windows.Markup.INameScope.FindName(string name)
    {
      return default(Object);
    }

    bool System.Windows.Markup.IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
    {
      return default(bool);
    }

    public void UnregisterName(string name)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Style BasedOn
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public bool IsSealed
    {
      get
      {
        return default(bool);
      }
    }

    public ResourceDictionary Resources
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceDictionary>() != null);

        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public SetterBaseCollection Setters
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.SetterBaseCollection>() != null);

        return default(SetterBaseCollection);
      }
    }

    ResourceDictionary System.Windows.Markup.IHaveResources.Resources
    {
      get
      {
        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public Type TargetType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public TriggerCollection Triggers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TriggerCollection>() != null);

        return default(TriggerCollection);
      }
    }
    #endregion
  }
}
