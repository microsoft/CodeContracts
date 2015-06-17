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

// File System.Windows.Data.MultiBinding.cs
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


namespace System.Windows.Data
{
  public partial class MultiBinding : BindingBase, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    public MultiBinding()
    {
    }

    public bool ShouldSerializeBindings()
    {
      return default(bool);
    }

    public bool ShouldSerializeValidationRules()
    {
      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.Collection<BindingBase> Bindings
    {
      get
      {
        return default(System.Collections.ObjectModel.Collection<BindingBase>);
      }
    }

    public IMultiValueConverter Converter
    {
      get
      {
        return default(IMultiValueConverter);
      }
      set
      {
      }
    }

    public System.Globalization.CultureInfo ConverterCulture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public Object ConverterParameter
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public BindingMode Mode
    {
      get
      {
        Contract.Ensures(((System.Windows.Data.BindingMode)(0)) <= Contract.Result<System.Windows.Data.BindingMode>());
        Contract.Ensures(Contract.Result<System.Windows.Data.BindingMode>() <= ((System.Windows.Data.BindingMode)(4)));

        return default(BindingMode);
      }
      set
      {
      }
    }

    public bool NotifyOnSourceUpdated
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool NotifyOnTargetUpdated
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool NotifyOnValidationError
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter
    {
      get
      {
        return default(UpdateSourceExceptionFilterCallback);
      }
      set
      {
      }
    }

    public UpdateSourceTrigger UpdateSourceTrigger
    {
      get
      {
        Contract.Ensures(((System.Windows.Data.UpdateSourceTrigger)(0)) <= Contract.Result<System.Windows.Data.UpdateSourceTrigger>());
        Contract.Ensures(Contract.Result<System.Windows.Data.UpdateSourceTrigger>() <= ((System.Windows.Data.UpdateSourceTrigger)(3)));

        return default(UpdateSourceTrigger);
      }
      set
      {
      }
    }

    public bool ValidatesOnDataErrors
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ValidatesOnExceptions
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule> ValidationRules
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule>>() != null);

        return default(System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule>);
      }
    }
    #endregion
  }
}
