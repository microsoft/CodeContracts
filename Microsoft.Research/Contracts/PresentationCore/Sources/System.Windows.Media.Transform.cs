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

// File System.Windows.Media.Transform.cs
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


namespace System.Windows.Media
{
  abstract public partial class Transform : GeneralTransform, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    public Transform Clone()
    {
      return default(Transform);
    }

    public Transform CloneCurrentValue()
    {
      return default(Transform);
    }

    public static Transform Parse(string source)
    {
      return default(Transform);
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    internal Transform()
    {
    }

    public override System.Windows.Rect TransformBounds(System.Windows.Rect rect)
    {
      return default(System.Windows.Rect);
    }

    public override bool TryTransform(System.Windows.Point inPoint, out System.Windows.Point result)
    {
      result = default(System.Windows.Point);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.Media.Transform Identity
    {
      get
      {
        Contract.Ensures(Contract.Result<Transform>() != null);
        return default(System.Windows.Media.Transform);
      }
    }

    public override GeneralTransform Inverse
    {
      get
      {
        return default(GeneralTransform);
      }
    }

    public abstract Matrix Value
    {
      get;
    }
    #endregion
  }
}
