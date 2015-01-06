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
using System.Text;
using System.IO;

using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.Graphs;


namespace Microsoft.Research.CodeAnalysis
{
  using StackTemp = System.Int32;

  public class Tests<Label,Local,Parameter,Method,Field,Type,Handler,Attribute>
  {
    #region Privates

    bool doPrint = false;


    long analyzedCount = 1;


    private float Average(long tickCount)
    {
      float avg = (float)tickCount / (float)this.analyzedCount;
      return avg;
    }

    private void DebugWriteLine(string format, params object[] args)
    {
      if (doPrint) {
        Console.WriteLine(format, args);
      }
    }
    #endregion

  }


  class DummyTextWriter : System.IO.TextWriter {

    public override void WriteLine(string format, params object[] arg) {
      
    }
    public override Encoding Encoding {
      get { throw new Exception("The method or operation is not implemented."); }
    }
  }

}
