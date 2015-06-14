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

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Runtime.InteropServices.ComTypes {
  // Summary:
  //     Provides the managed definition of the ITypeComp interface.
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020403-0000-0000-C000-000000000046")]
  public interface ITypeComp {
    // Summary:
    //     Maps a name to a member of a type, or binds global variables and functions
    //     contained in a type library.
    //
    // Parameters:
    //   szName:
    //     The name to bind.
    //
    //   lHashVal:
    //     A hash value for szName computed by LHashValOfNameSys.
    //
    //   wFlags:
    //     A flags word containing one or more of the invoke flags defined in the INVOKEKIND
    //     enumeration.
    //
    //   ppTInfo:
    //     When this method returns, contains a reference to the type description that
    //     contains the item to which it is bound, if a FUNCDESC or VARDESC was returned.
    //     This parameter is passed uninitialized.
    //
    //   pDescKind:
    //     When this method returns, contains a reference to a DESCKIND enumerator that
    //     indicates whether the name bound-to is a VARDESC, FUNCDESC, or TYPECOMP.
    //     This parameter is passed uninitialized.
    //
    //   pBindPtr:
    //     When this method returns, contains a reference to the bound-to VARDESC, FUNCDESC,
    //     or ITypeComp interface. This parameter is passed uninitialized.
    void Bind(string szName, int lHashVal, short wFlags, out ITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);
    //
    // Summary:
    //     Binds to the type descriptions contained within a type library.
    //
    // Parameters:
    //   szName:
    //     The name to bind.
    //
    //   lHashVal:
    //     A hash value for szName determined by LHashValOfNameSys.
    //
    //   ppTInfo:
    //     When this method returns, contains a reference to an ITypeInfo of the type
    //     to which szName was bound. This parameter is passed uninitialized.
    //
    //   ppTComp:
    //     When this method returns, contains a reference to an ITypeComp variable.
    //     This parameter is passed uninitialized.
    void BindType(string szName, int lHashVal, out ITypeInfo ppTInfo, out ITypeComp ppTComp);
  }
}

