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

#if !SILVERLIGHT
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics.Contracts;

namespace System.IO.IsolatedStorage
{
  public abstract class IsolatedStorage //: MarshalByRefObject
  {
    //protected IsolatedStorage() { }

    extern public object ApplicationIdentity { get; }
    extern public object AssemblyIdentity { get; }
    extern public virtual ulong CurrentSize { get; }
    extern public object DomainIdentity { get; }
    extern public virtual ulong MaximumSize { get; }
    // extern public virtual long Quota { get; internal set; }
    extern public IsolatedStorageScope Scope { get; }
    extern protected virtual char SeparatorExternal { get; }
    extern protected virtual char SeparatorInternal { get; }
    //extern public virtual long UsedSize { get; }
    //extern protected abstract IsolatedStoragePermission GetPermission(PermissionSet ps);

#if NETFRAMEWORK_4_0
    public virtual bool IncreaseQuotaTo(long newQuotaSize)
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      return false;
    }
#endif

    extern protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType);
    extern protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType);
  }
}
#endif