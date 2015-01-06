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

#region Assembly System.Drawing.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll
#endregion

using System;
using System.Diagnostics.Contracts;

namespace System.Drawing {
  // Summary:
  //     Each property of the System.Drawing.SystemIcons class is an System.Drawing.Icon
  //     object for Windows system-wide icons. This class cannot be inherited.
  public sealed class SystemIcons {

    private SystemIcons() { }

    // Summary:
    //     Gets an System.Drawing.Icon object that contains the default application
    //     icon (WIN32: IDI_APPLICATION).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the default application icon.
    public static Icon Application {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system asterisk icon
    //     (WIN32: IDI_ASTERISK).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system asterisk icon.
    public static Icon Asterisk {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system error icon (WIN32:
    //     IDI_ERROR).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system error icon.
    public static Icon Error {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system exclamation icon
    //     (WIN32: IDI_EXCLAMATION).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system exclamation icon.
    public static Icon Exclamation {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system hand icon (WIN32:
    //     IDI_HAND).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system hand icon.
    public static Icon Hand {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system information icon
    //     (WIN32: IDI_INFORMATION).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system information icon.
    public static Icon Information {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system question icon
    //     (WIN32: IDI_QUESTION).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system question icon.
    public static Icon Question {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the shield icon.
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the shield icon.
    public static Icon Shield {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the system warning icon
    //     (WIN32: IDI_WARNING).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the system warning icon.
    //public static Icon Warning { get; }
    //
    // Summary:
    //     Gets an System.Drawing.Icon object that contains the Windows logo icon (WIN32:
    //     IDI_WINLOGO).
    //
    // Returns:
    //     An System.Drawing.Icon object that contains the Windows logo icon.
    public static Icon WinLogo {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);
        return default(Icon);
      }
    }
  }
}
