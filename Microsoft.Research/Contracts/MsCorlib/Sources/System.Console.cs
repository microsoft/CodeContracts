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

// File System.Console.cs
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


namespace System
{
  static public partial class Console
  {
    #region Methods and constructors
    public static void Beep(int frequency, int duration)
    {
    }

    public static void Beep()
    {
    }

    public static void Clear()
    {
    }

    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
    {
    }

    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
    {
    }

    public static Stream OpenStandardError()
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static Stream OpenStandardError(int bufferSize)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static Stream OpenStandardInput()
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static Stream OpenStandardInput(int bufferSize)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static Stream OpenStandardOutput(int bufferSize)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static Stream OpenStandardOutput()
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public static int Read()
    {
      return default(int);
    }

    public static ConsoleKeyInfo ReadKey(bool intercept)
    {
      return default(ConsoleKeyInfo);
    }

    public static ConsoleKeyInfo ReadKey()
    {
      return default(ConsoleKeyInfo);
    }

    public static string ReadLine()
    {
      return default(string);
    }

    public static void ResetColor()
    {
    }

    public static void SetBufferSize(int width, int height)
    {
    }

    public static void SetCursorPosition(int left, int top)
    {
    }

    public static void SetError(TextWriter newError)
    {
    }

    public static void SetIn(TextReader newIn)
    {
    }

    public static void SetOut(TextWriter newOut)
    {
    }

    public static void SetWindowPosition(int left, int top)
    {
    }

    public static void SetWindowSize(int width, int height)
    {
    }

    public static void Write(ulong value)
    {
    }

    public static void Write(long value)
    {
    }

    public static void Write(uint value)
    {
    }

    public static void Write(string format, Object arg0)
    {
    }

    public static void Write(string value)
    {
    }

    public static void Write(Object value)
    {
    }

    public static void Write(int value)
    {
    }

    public static void Write(string format, Object[] arg)
    {
    }

    public static void Write(bool value)
    {
    }

    public static void Write(string format, Object arg0, Object arg1, Object arg2, Object arg3)
    {
    }

    public static void Write(string format, Object arg0, Object arg1)
    {
    }

    public static void Write(string format, Object arg0, Object arg1, Object arg2)
    {
    }

    public static void Write(char value)
    {
    }

    public static void Write(Decimal value)
    {
    }

    public static void Write(float value)
    {
    }

    public static void Write(double value)
    {
    }

    public static void Write(char[] buffer)
    {
    }

    public static void Write(char[] buffer, int index, int count)
    {
    }

    public static void WriteLine(double value)
    {
    }

    public static void WriteLine(Decimal value)
    {
    }

    public static void WriteLine(int value)
    {
    }

    public static void WriteLine(float value)
    {
    }

    public static void WriteLine(char[] buffer, int index, int count)
    {
    }

    public static void WriteLine(bool value)
    {
    }

    public static void WriteLine()
    {
    }

    public static void WriteLine(char[] buffer)
    {
    }

    public static void WriteLine(char value)
    {
    }

    public static void WriteLine(uint value)
    {
    }

    public static void WriteLine(string format, Object arg0, Object arg1, Object arg2)
    {
    }

    public static void WriteLine(string format, Object arg0, Object arg1)
    {
    }

    public static void WriteLine(string format, Object[] arg)
    {
    }

    public static void WriteLine(string format, Object arg0, Object arg1, Object arg2, Object arg3)
    {
    }

    public static void WriteLine(string format, Object arg0)
    {
    }

    public static void WriteLine(ulong value)
    {
    }

    public static void WriteLine(long value)
    {
    }

    public static void WriteLine(string value)
    {
    }

    public static void WriteLine(Object value)
    {
    }
    #endregion

    #region Properties and indexers
    public static ConsoleColor BackgroundColor
    {
      get
      {
        return default(ConsoleColor);
      }
      set
      {
      }
    }

    public static int BufferHeight
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.BufferWidth);
        Contract.Ensures(System.Console.BufferWidth <= 32767);
      }
    }

    public static int BufferWidth
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.BufferHeight);
        Contract.Ensures(System.Console.BufferHeight <= 32767);
      }
    }

    public static bool CapsLock
    {
      get
      {
        return default(bool);
      }
    }

    public static int CursorLeft
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.CursorTop);
        Contract.Ensures(System.Console.CursorTop <= 32767);
      }
    }

    public static int CursorSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static int CursorTop
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.CursorLeft);
        Contract.Ensures(System.Console.CursorLeft <= 32767);
      }
    }

    public static bool CursorVisible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static TextWriter Error
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.TextWriter>() != null);

        return default(TextWriter);
      }
    }

    public static ConsoleColor ForegroundColor
    {
      get
      {
        Contract.Ensures(((System.ConsoleColor)(0)) <= Contract.Result<System.ConsoleColor>());
        Contract.Ensures(Contract.Result<System.ConsoleColor>() <= ((System.ConsoleColor)(15)));

        return default(ConsoleColor);
      }
      set
      {
      }
    }

    public static TextReader In
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.TextReader>() != null);

        return default(TextReader);
      }
    }

    public static Encoding InputEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(Encoding);
      }
      set
      {
      }
    }

    public static bool KeyAvailable
    {
      get
      {
        return default(bool);
      }
    }

    public static int LargestWindowHeight
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
    }

    public static int LargestWindowWidth
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
    }

    public static bool NumberLock
    {
      get
      {
        return default(bool);
      }
    }

    public static TextWriter Out
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.TextWriter>() != null);

        return default(TextWriter);
      }
    }

    public static Encoding OutputEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(Encoding);
      }
      set
      {
      }
    }

    public static string Title
    {
      get
      {
        Contract.Ensures(0 <= string.Empty.Length);

        return default(string);
      }
      set
      {
        Contract.Ensures(value.Length <= 24500);
      }
    }

    public static bool TreatControlCAsInput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static int WindowHeight
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static int WindowLeft
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.WindowTop);
        Contract.Ensures(System.Console.WindowTop <= 32767);
      }
    }

    public static int WindowTop
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
      set
      {
        Contract.Ensures(-32768 <= System.Console.WindowLeft);
        Contract.Ensures(System.Console.WindowLeft <= 32767);
      }
    }

    public static int WindowWidth
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
    public static event ConsoleCancelEventHandler CancelKeyPress
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
