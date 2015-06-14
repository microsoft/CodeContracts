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
using System.IO;
using System.Diagnostics.Contracts;


namespace System.Web
{
  // Summary:
  //     Provides access to individual files that have been uploaded by a client.
  public sealed class HttpPostedFile
  {
    // Summary:
    //     Gets the size of an uploaded file, in bytes.
    //
    // Returns:
    //     The file length, in bytes.
    public int ContentLength
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    //
    // Summary:
    //     Gets the MIME content type of a file sent by a client.
    //
    // Returns:
    //     The MIME content type of the uploaded file.
    public string ContentType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the fully qualified name of the file on the client.
    //
    // Returns:
    //     The name of the client's file, including the directory path.
    public string FileName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a System.IO.Stream object that points to an uploaded file to prepare
    //     for reading the contents of the file.
    //
    // Returns:
    //     A System.IO.Stream pointing to a file.
    public Stream InputStream
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
    }

    // Summary:
    //     Saves the contents of an uploaded file.
    //
    // Parameters:
    //   filename:
    //     The name of the saved file.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     The System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath property
    //     of the System.Web.Configuration.HttpRuntimeSection object is set to true,
    //     but filename is not an absolute path.
    public void SaveAs(string filename)
    {
      Contract.Requires(filename != null);

    }
  }
}
