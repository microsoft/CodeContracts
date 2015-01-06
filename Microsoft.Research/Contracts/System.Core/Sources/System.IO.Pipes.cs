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

// File System.IO.Pipes.cs
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


namespace System.IO.Pipes
{
  public enum PipeAccessRights
  {
    ReadData = 1, 
    WriteData = 2, 
    ReadAttributes = 128, 
    WriteAttributes = 256, 
    ReadExtendedAttributes = 8, 
    WriteExtendedAttributes = 16, 
    CreateNewInstance = 4, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    Synchronize = 1048576, 
    FullControl = 2032031, 
    Read = 131209, 
    Write = 274, 
    ReadWrite = 131483, 
    AccessSystemSecurity = 16777216, 
  }

  public enum PipeDirection
  {
    In = 1, 
    Out = 2, 
    InOut = 3, 
  }

  public enum PipeOptions
  {
    None = 0, 
    WriteThrough = -2147483648, 
    Asynchronous = 1073741824, 
  }

  public delegate void PipeStreamImpersonationWorker();

  public enum PipeTransmissionMode
  {
    Byte = 0, 
    Message = 1, 
  }
}
