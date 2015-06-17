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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ClousotExtension
{
  public static class Utils
  {
    //[Conditional("FULLDEBUG")]
    [Conditional("DEBUG")]
    public static void DumpCSFile(string filename, string text, string comment = null, string path = @"c:\\tmp\\Roslyn\\")
    {
      try
      {
        System.IO.Directory.CreateDirectory(path);
        if (comment != null)
        {
          comment = string.Format("/* {0} */{1}", comment, Environment.NewLine);
        }
        System.IO.File.WriteAllText(path + filename, comment + text);
      }
      catch(Exception)
      {
        System.Diagnostics.Trace.WriteLine("Cannot open the .cs dump file " + filename);
      }
    }

    [Conditional("DEBUG")]
    public static void Trace(string s)
    {
      System.Diagnostics.Trace.WriteLine("--- Clousot Extension : " + s);
    }

    [Conditional("DEBUG")]
    public static void TraceTime(string operation, TimeSpan time)
    {
      System.Diagnostics.Trace.WriteLine("--- Clousot Extension : The operation " + operation + " took " + time.ToString());
    }

  }
}
