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
using System.IO;
using System.Diagnostics.Contracts;
using System.Xml;
using Microsoft.Research.DataStructures;
using System.Diagnostics;

namespace CCDoc
{
  /// <summary>
  /// The custom options used by CCDoc.
  /// </summary>
  class Options : OptionParsing
  {
    [OptionDescription("The name of the reference assembly that will be used to pull contract information from. Ex. 'foo.contracts.dll'", ShortForm = "a")]
    public string assembly = null;

    [OptionDescription("The name of the XML reference file that this program will add contract information to. If none is provided or if it cannot be found, a new XML file will be generated using the given assembly file's name.", ShortForm = "xf")]
    public string xmlFile = null;

    [OptionDescription("Emit debug output.", ShortForm = "d")]
    public bool debug = false;

    [OptionDescription("The name of the textfile debug output should be written to. If none is provided, the console is used.", ShortForm = "of")]
    public string outFile = null;

    [OptionDescription("Search paths for assembly dependencies.", ShortForm = "lp")]
    public List<string> libpaths = new List<string>();

    [OptionDescription("Full paths to candidate dlls to load for resolution.")]
    public List<string> resolvedPaths = new List<string>();

    [OptionDescription("Causes the debugger to be invoked.", ShortForm = "break")]
    public bool breakIntoDebugger = false;

    [OptionDescription("If specified, then the input xmlFile is not overwritten, the output is instead written to this file.", ShortForm = "out")]
    public string outputFile = null;

    [OptionDescription("When true, create an exception element for each contract that specifies an exception type. (Default: true)", ShortForm = "exc")]
    public bool generateExceptionTable = true;
  }

}