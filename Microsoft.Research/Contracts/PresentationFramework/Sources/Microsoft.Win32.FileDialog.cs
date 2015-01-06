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

// File Microsoft.Win32.FileDialog.cs
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


namespace Microsoft.Win32
{
  abstract public partial class FileDialog : CommonDialog
  {
    #region Methods and constructors
    protected FileDialog()
    {
      Contract.Ensures(this.CustomPlaces.Count == 0);
    }

    protected override IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
    {
      return default(IntPtr);
    }

    protected void OnFileOk(System.ComponentModel.CancelEventArgs e)
    {
    }

    public override void Reset()
    {
    }

    protected override bool RunDialog(IntPtr hwndOwner)
    {
      return default(bool);
    }

    #endregion

    #region Properties and indexers
    public bool AddExtension
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool CheckFileExists
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CheckPathExists
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IList<FileDialogCustomPlace> CustomPlaces
    {
      get
      {
        return default(IList<FileDialogCustomPlace>);
      }
      set
      {
      }
    }

    public string DefaultExt
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

    public bool DereferenceLinks
    {
      get
      {
        return default(bool);
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

    public string[] FileNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);

        return default(string[]);
      }
    }

    public string Filter
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

    public int FilterIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string InitialDirectory
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

    protected int Options
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());

        return default(int);
      }
    }

    public bool RestoreDirectory
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string SafeFileName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public string[] SafeFileNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);

        return default(string[]);
      }
    }

    public string Title
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

    public bool ValidateNames
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

    #region Events
    public event System.ComponentModel.CancelEventHandler FileOk
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
