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
using System.Diagnostics.Contracts;

namespace System.Diagnostics
{

  public class StackTrace
  {

    public virtual int FrameCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    public virtual StackFrame GetFrame(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < FrameCount);

      return default(StackFrame);
    }
    public StackTrace(System.Threading.Thread targetThread, bool needFileInfo)
    {

    }
    public StackTrace(StackFrame frame)
    {

    }
    public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
    {
      Contract.Requires(e != null);
      Contract.Requires(skipFrames >= 0);

    }
    public StackTrace(Exception e, int skipFrames)
    {
      Contract.Requires(e != null);
      Contract.Requires(skipFrames >= 0);

    }
    public StackTrace(Exception e, bool fNeedFileInfo)
    {
      Contract.Requires(e != null);

    }
    public StackTrace(Exception e)
    {
      Contract.Requires(e != null);

    }
    public StackTrace(int skipFrames, bool fNeedFileInfo)
    {
      Contract.Requires(skipFrames >= 0);

    }
    public StackTrace(int skipFrames)
    {
      Contract.Requires(skipFrames >= 0);

    }
    public StackTrace(bool fNeedFileInfo)
    {

    }
    public StackTrace()
    {
    }
  }
}