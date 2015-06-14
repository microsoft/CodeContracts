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

// File System.ComponentModel.EventDescriptor.cs
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


namespace System.ComponentModel
{
  abstract public partial class EventDescriptor : MemberDescriptor
  {
    #region Methods and constructors
    public abstract void AddEventHandler(Object component, Delegate value);

    protected EventDescriptor(string name, Attribute[] attrs) : base (default(string))
    {
    }

    protected EventDescriptor(MemberDescriptor descr) : base (default(string))
    {
      Contract.Requires(0 <= descr.Attributes.Count);
      Contract.Requires(descr.Name != null);
    }

    protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs) : base (default(string))
    {
      Contract.Requires(descr != null);
      Contract.Requires(descr.Attributes != null);
      Contract.Requires(descr.Name != null);
    }

    public abstract void RemoveEventHandler(Object component, Delegate value);
    #endregion

    #region Properties and indexers
    public abstract Type ComponentType
    {
      get;
    }

    public abstract Type EventType
    {
      get;
    }

    public abstract bool IsMulticast
    {
      get;
    }
    #endregion
  }
}
