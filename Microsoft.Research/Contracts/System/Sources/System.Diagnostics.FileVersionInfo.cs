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

// File System.Diagnostics.FileVersionInfo.cs
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
  sealed public partial class FileVersionInfo
  {
    #region Methods and constructors
    internal FileVersionInfo()
    {
    }

    public static FileVersionInfo GetVersionInfo(string fileName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.FileVersionInfo>() != null);

      return default(FileVersionInfo);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public string Comments
    {
      get
      {
        return default(string);
      }
    }

    public string CompanyName
    {
      get
      {
        return default(string);
      }
    }

    public int FileBuildPart
    {
      get
      {
        return default(int);
      }
    }

    public string FileDescription
    {
      get
      {
        return default(string);
      }
    }

    public int FileMajorPart
    {
      get
      {
        return default(int);
      }
    }

    public int FileMinorPart
    {
      get
      {
        return default(int);
      }
    }

    public string FileName
    {
      get
      {
        return default(string);
      }
    }

    public int FilePrivatePart
    {
      get
      {
        return default(int);
      }
    }

    public string FileVersion
    {
      get
      {
        return default(string);
      }
    }

    public string InternalName
    {
      get
      {
        return default(string);
      }
    }

    public bool IsDebug
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPatched
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPreRelease
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPrivateBuild
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSpecialBuild
    {
      get
      {
        return default(bool);
      }
    }

    public string Language
    {
      get
      {
        return default(string);
      }
    }

    public string LegalCopyright
    {
      get
      {
        return default(string);
      }
    }

    public string LegalTrademarks
    {
      get
      {
        return default(string);
      }
    }

    public string OriginalFilename
    {
      get
      {
        return default(string);
      }
    }

    public string PrivateBuild
    {
      get
      {
        return default(string);
      }
    }

    public int ProductBuildPart
    {
      get
      {
        return default(int);
      }
    }

    public int ProductMajorPart
    {
      get
      {
        return default(int);
      }
    }

    public int ProductMinorPart
    {
      get
      {
        return default(int);
      }
    }

    public string ProductName
    {
      get
      {
        return default(string);
      }
    }

    public int ProductPrivatePart
    {
      get
      {
        return default(int);
      }
    }

    public string ProductVersion
    {
      get
      {
        return default(string);
      }
    }

    public string SpecialBuild
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
