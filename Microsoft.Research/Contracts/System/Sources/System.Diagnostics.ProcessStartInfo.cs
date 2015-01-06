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

// File System.Diagnostics.ProcessStartInfo.cs
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


namespace System.Diagnostics
{
  sealed public partial class ProcessStartInfo
  {
    #region Methods and constructors
    public ProcessStartInfo(string fileName, string arguments)
    {
    }

    public ProcessStartInfo(string fileName)
    {
    }

    public ProcessStartInfo()
    {
    }
    #endregion

    #region Properties and indexers
    public string Arguments
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public bool CreateNoWindow
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Domain
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.StringDictionary EnvironmentVariables
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Specialized.StringDictionary>() != null);

        return default(System.Collections.Specialized.StringDictionary);
      }
    }

    public bool ErrorDialog
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IntPtr ErrorDialogParentHandle
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public string FileName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public bool LoadUserProfile
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Security.SecureString Password
    {
      get
      {
        return default(System.Security.SecureString);
      }
      set
      {
      }
    }

    public bool RedirectStandardError
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool RedirectStandardInput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool RedirectStandardOutput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Encoding StandardErrorEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public Encoding StandardOutputEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public string UserName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public bool UseShellExecute
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Verb
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public string[] Verbs
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);

        return default(string[]);
      }
    }

    public ProcessWindowStyle WindowStyle
    {
      get
      {
        return default(ProcessWindowStyle);
      }
      set
      {
      }
    }

    public string WorkingDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
