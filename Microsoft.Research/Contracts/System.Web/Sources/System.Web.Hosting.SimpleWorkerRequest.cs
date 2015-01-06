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

// File System.Web.Hosting.SimpleWorkerRequest.cs
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


namespace System.Web.Hosting
{
  public partial class SimpleWorkerRequest : System.Web.HttpWorkerRequest
  {
    #region Methods and constructors
    public override void EndOfRequest()
    {
    }

    public override void FlushResponse(bool finalFlush)
    {
    }

    public override string GetAppPath()
    {
      return default(string);
    }

    public override string GetAppPathTranslated()
    {
      return default(string);
    }

    public override string GetFilePath()
    {
      return default(string);
    }

    public override string GetFilePathTranslated()
    {
      return default(string);
    }

    public override string GetHttpVerbName()
    {
      return default(string);
    }

    public override string GetHttpVersion()
    {
      return default(string);
    }

    public override string GetLocalAddress()
    {
      return default(string);
    }

    public override int GetLocalPort()
    {
      return default(int);
    }

    public override string GetPathInfo()
    {
      return default(string);
    }

    public override string GetQueryString()
    {
      return default(string);
    }

    public override string GetRawUrl()
    {
      return default(string);
    }

    public override string GetRemoteAddress()
    {
      return default(string);
    }

    public override int GetRemotePort()
    {
      return default(int);
    }

    public override string GetServerVariable(string name)
    {
      return default(string);
    }

    public override string GetUriPath()
    {
      return default(string);
    }

    public override IntPtr GetUserToken()
    {
      return default(IntPtr);
    }

    public override string MapPath(string path)
    {
      return default(string);
    }

    public override void SendKnownResponseHeader(int index, string value)
    {
    }

    public override void SendResponseFromFile(IntPtr handle, long offset, long length)
    {
    }

    public override void SendResponseFromFile(string filename, long offset, long length)
    {
    }

    public override void SendResponseFromMemory(byte[] data, int length)
    {
    }

    public override void SendStatus(int statusCode, string statusDescription)
    {
    }

    public override void SendUnknownResponseHeader(string name, string value)
    {
    }

    public SimpleWorkerRequest(string page, string query, TextWriter output)
    {
    }

    public SimpleWorkerRequest(string appVirtualDir, string appPhysicalDir, string page, string query, TextWriter output)
    {
    }
    #endregion

    #region Properties and indexers
    public override string MachineConfigPath
    {
      get
      {
        return default(string);
      }
    }

    public override string MachineInstallDirectory
    {
      get
      {
        return default(string);
      }
    }

    public override string RootWebConfigPath
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
