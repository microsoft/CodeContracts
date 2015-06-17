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

// File System.Windows.Data.MultiBindingExpression.cs
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
  sealed public partial class MultiBindingExpression : BindingExpressionBase, MS.Internal.Data.IDataBindEngineClient
  {
    #region Methods and constructors
    bool MS.Internal.Data.IDataBindEngineClient.AttachToContext(bool lastChance)
    {
      return default(bool);
    }

    void MS.Internal.Data.IDataBindEngineClient.OnTargetUpdated()
    {
    }

    void MS.Internal.Data.IDataBindEngineClient.TransferValue()
    {
    }

    void MS.Internal.Data.IDataBindEngineClient.UpdateValue()
    {
    }

    void MS.Internal.Data.IDataBindEngineClient.VerifySourceReference(bool lastChance)
    {
    }

    extern MultiBindingExpression() ;

    public override void UpdateSource()
    {
    }

    public override void UpdateTarget()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.ReadOnlyCollection<BindingExpressionBase> BindingExpressions
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Data.BindingExpressionBase>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<BindingExpressionBase>);
      }
    }

    public override bool HasError
    {
      get
      {
        return default(bool);
      }
    }

    System.Windows.DependencyObject MS.Internal.Data.IDataBindEngineClient.TargetElement
    {
      get
      {
        return default(System.Windows.DependencyObject);
      }
    }

    public MultiBinding ParentMultiBinding
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Data.MultiBinding>() == ((System.Windows.Data.MultiBinding)(this.ParentBindingBase)));

        return default(MultiBinding);
      }
    }

    public override System.Windows.Controls.ValidationError ValidationError
    {
      get
      {
        return default(System.Windows.Controls.ValidationError);
      }
    }
    #endregion
  }
}
