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

// File System.IO.Ports.cs
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


namespace System.IO.Ports
{
  public enum Handshake
  {
    None = 0, 
    XOnXOff = 1, 
    RequestToSend = 2, 
    RequestToSendXOnXOff = 3, 
  }

  public enum Parity
  {
    None = 0, 
    Odd = 1, 
    Even = 2, 
    Mark = 3, 
    Space = 4, 
  }

  public enum SerialData
  {
    Chars = 1, 
    Eof = 2, 
  }

  public delegate void SerialDataReceivedEventHandler(Object sender, SerialDataReceivedEventArgs e);

  public enum SerialError
  {
    TXFull = 256, 
    RXOver = 1, 
    Overrun = 2, 
    RXParity = 4, 
    Frame = 8, 
  }

  public delegate void SerialErrorReceivedEventHandler(Object sender, SerialErrorReceivedEventArgs e);

  public enum SerialPinChange
  {
    CtsChanged = 8, 
    DsrChanged = 16, 
    CDChanged = 32, 
    Ring = 256, 
    Break = 64, 
  }

  public delegate void SerialPinChangedEventHandler(Object sender, SerialPinChangedEventArgs e);

  public enum StopBits
  {
    None = 0, 
    One = 1, 
    Two = 2, 
    OnePointFive = 3, 
  }
}
