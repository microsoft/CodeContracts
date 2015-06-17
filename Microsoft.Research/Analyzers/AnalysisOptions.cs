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

// The class that is used to keep the analysis options

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// The abstraction layer for the analysis options
  /// </summary>
  public class AnalysisOptions
  {
    #region Private state
    private bool checkLowerBounds;
    private bool checkUpperBounds;
    private bool doDebug;
    #endregion

    // default: no checks
    public AnalysisOptions()
    {
      this.checkUpperBounds = false;
      this.checkLowerBounds = false;
      this.doDebug = false;
    }

    public bool CheckLowerBounds
    {
      get
      {
        return this.checkLowerBounds;
      }
      set
      {
        this.checkLowerBounds = value;
      }
    }

    public bool CheckUpperBounds
    {
      get
      {
        return this.checkUpperBounds;
      }
      set
      {
        this.checkUpperBounds = value;
      }
    }

    public bool Debug
    {
      get
      {
        return this.doDebug;
      }
      set
      {
        this.doDebug = value;
      }
    }
  }

}
