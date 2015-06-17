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

// File System.Windows.FrameworkTemplate.cs
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
  abstract public partial class FrameworkTemplate : System.Windows.Threading.DispatcherObject, System.Windows.Markup.INameScope, System.Windows.Markup.IHaveResources, System.Windows.Markup.IQueryAmbient
  {
    #region Methods and constructors
    public Object FindName(string name, FrameworkElement templatedParent)
    {
      return default(Object);
    }

    protected FrameworkTemplate()
    {
    }

    public DependencyObject LoadContent()
    {
      return default(DependencyObject);
    }

    public void RegisterName(string name, Object scopedElement)
    {
    }

    public void Seal()
    {
    }

    public bool ShouldSerializeResources(System.Windows.Markup.XamlDesignerSerializationManager manager)
    {
      return default(bool);
    }

    public bool ShouldSerializeVisualTree()
    {
      return default(bool);
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

    protected virtual new void ValidateTemplatedParent(FrameworkElement templatedParent)
    {
    }
    #endregion

    #region Properties and indexers
    public bool HasContent
    {
      get
      {
        return default(bool);
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

    public TemplateContent Template
    {
      get
      {
        return default(TemplateContent);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public FrameworkElementFactory VisualTree
    {
      get
      {
        return default(FrameworkElementFactory);
      }
      set
      {
      }
    }
    #endregion
  }
}
