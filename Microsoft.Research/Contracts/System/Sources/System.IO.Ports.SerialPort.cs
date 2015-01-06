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

// File System.IO.Ports.SerialPort.cs
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
  public partial class SerialPort : System.ComponentModel.Component
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public void DiscardInBuffer()
    {
    }

    public void DiscardOutBuffer()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public static string[] GetPortNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public void Open()
    {
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      return default(int);
    }

    public int Read(char[] buffer, int offset, int count)
    {
      return default(int);
    }

    public int ReadByte()
    {
      return default(int);
    }

    public int ReadChar()
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 65535);

      return default(int);
    }

    public string ReadExisting()
    {
      return default(string);
    }

    public string ReadLine()
    {
      return default(string);
    }

    public string ReadTo(string value)
    {
      return default(string);
    }

    public SerialPort()
    {
    }

    public SerialPort(string portName)
    {
      Contract.Ensures(0 <= portName.Length);
    }

    public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
    {
    }

    public SerialPort(string portName, int baudRate)
    {
      Contract.Ensures(0 <= portName.Length);
    }

    public SerialPort(string portName, int baudRate, Parity parity, int dataBits)
    {
      Contract.Ensures(0 <= portName.Length);
    }

    public SerialPort(System.ComponentModel.IContainer container)
    {
      Contract.Requires(container != null);
    }

    public SerialPort(string portName, int baudRate, Parity parity)
    {
      Contract.Ensures(0 <= portName.Length);
    }

    public void Write(byte[] buffer, int offset, int count)
    {
    }

    public void Write(string text)
    {
    }

    public void Write(char[] buffer, int offset, int count)
    {
    }

    public void WriteLine(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public Stream BaseStream
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(Stream);
      }
    }

    public int BaudRate
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool BreakState
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(bool);
      }
      set
      {
        Contract.Ensures(this.IsOpen == true);
      }
    }

    public int BytesToRead
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(int);
      }
    }

    public int BytesToWrite
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(this.IsOpen == true);

        return default(int);
      }
    }

    public bool CDHolding
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(bool);
      }
    }

    public bool CtsHolding
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(bool);
      }
    }

    public int DataBits
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool DiscardNull
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool DsrHolding
    {
      get
      {
        Contract.Ensures(this.IsOpen == true);

        return default(bool);
      }
    }

    public bool DtrEnable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Encoding Encoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public Handshake Handshake
    {
      get
      {
        return default(Handshake);
      }
      set
      {
      }
    }

    public bool IsOpen
    {
      get
      {
        return default(bool);
      }
    }

    public string NewLine
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Parity Parity
    {
      get
      {
        return default(Parity);
      }
      set
      {
      }
    }

    public byte ParityReplace
    {
      get
      {
        return default(byte);
      }
      set
      {
      }
    }

    public string PortName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int ReadBufferSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int ReadTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int ReceivedBytesThreshold
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool RtsEnable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public StopBits StopBits
    {
      get
      {
        return default(StopBits);
      }
      set
      {
      }
    }

    public int WriteBufferSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int WriteTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event SerialDataReceivedEventHandler DataReceived
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SerialErrorReceivedEventHandler ErrorReceived
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SerialPinChangedEventHandler PinChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public static int InfiniteTimeout;
    #endregion
  }
}
