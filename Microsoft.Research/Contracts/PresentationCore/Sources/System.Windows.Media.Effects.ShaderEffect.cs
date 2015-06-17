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

// File System.Windows.Media.Effects.ShaderEffect.cs
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


namespace System.Windows.Media.Effects
{
  abstract public partial class ShaderEffect : Effect
  {
    #region Methods and constructors
    public ShaderEffect Clone()
    {
      return default(ShaderEffect);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public ShaderEffect CloneCurrentValue()
    {
      return default(ShaderEffect);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected static System.Windows.PropertyChangedCallback PixelShaderConstantCallback(int floatRegisterIndex)
    {
      return default(System.Windows.PropertyChangedCallback);
    }

    protected static System.Windows.PropertyChangedCallback PixelShaderSamplerCallback(int samplerRegisterIndex)
    {
      return default(System.Windows.PropertyChangedCallback);
    }

    protected static System.Windows.PropertyChangedCallback PixelShaderSamplerCallback(int samplerRegisterIndex, SamplingMode samplingMode)
    {
      return default(System.Windows.PropertyChangedCallback);
    }

    protected static System.Windows.DependencyProperty RegisterPixelShaderSamplerProperty(string dpName, Type ownerType, int samplerRegisterIndex)
    {
      return default(System.Windows.DependencyProperty);
    }

    protected static System.Windows.DependencyProperty RegisterPixelShaderSamplerProperty(string dpName, Type ownerType, int samplerRegisterIndex, SamplingMode samplingMode)
    {
      return default(System.Windows.DependencyProperty);
    }

    protected ShaderEffect()
    {
    }

    protected void UpdateShaderValue(System.Windows.DependencyProperty dp)
    {
    }
    #endregion

    #region Properties and indexers
    protected int DdxUvDdyUvRegisterIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    protected double PaddingBottom
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    protected double PaddingLeft
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    protected double PaddingRight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    protected double PaddingTop
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    protected PixelShader PixelShader
    {
      get
      {
        return default(PixelShader);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    protected readonly static System.Windows.DependencyProperty PixelShaderProperty;
    #endregion
  }
}
