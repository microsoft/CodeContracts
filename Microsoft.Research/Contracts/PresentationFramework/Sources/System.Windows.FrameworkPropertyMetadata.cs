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

// File System.Windows.FrameworkPropertyMetadata.cs
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
  public partial class FrameworkPropertyMetadata : UIPropertyMetadata
  {
    #region Methods and constructors
    public FrameworkPropertyMetadata(Object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback, bool isAnimationProhibited)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback, bool isAnimationProhibited, System.Windows.Data.UpdateSourceTrigger defaultUpdateSourceTrigger)
    {
    }

    public FrameworkPropertyMetadata(PropertyChangedCallback propertyChangedCallback)
    {
    }

    public FrameworkPropertyMetadata(PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
    {
    }

    public FrameworkPropertyMetadata()
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, PropertyChangedCallback propertyChangedCallback)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
    {
    }

    public FrameworkPropertyMetadata(Object defaultValue, FrameworkPropertyMetadataOptions flags)
    {
    }

    protected override void Merge(PropertyMetadata baseMetadata, DependencyProperty dp)
    {
    }

    protected override void OnApply(DependencyProperty dp, Type targetType)
    {
    }
    #endregion

    #region Properties and indexers
    public bool AffectsArrange
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AffectsMeasure
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AffectsParentArrange
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AffectsParentMeasure
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AffectsRender
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool BindsTwoWayByDefault
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Data.UpdateSourceTrigger DefaultUpdateSourceTrigger
    {
      get
      {
        Contract.Ensures(((System.Windows.Data.UpdateSourceTrigger)(0)) <= Contract.Result<System.Windows.Data.UpdateSourceTrigger>());
        Contract.Ensures(Contract.Result<System.Windows.Data.UpdateSourceTrigger>() <= ((System.Windows.Data.UpdateSourceTrigger)(3)));

        return default(System.Windows.Data.UpdateSourceTrigger);
      }
      set
      {
      }
    }

    public bool Inherits
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsDataBindingAllowed
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNotDataBindable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool Journal
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool OverridesInheritanceBehavior
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool SubPropertiesDoNotAffectRender
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
