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
using Microsoft.Research.DataStructures;
using Microsoft.Research.Cloudot.Common;

namespace Microsoft.Research.CodeAnalysis
{
  // Additional options to Clousot for Clousot as Service client
  class ClousotServiceOptions : OptionParsing
  {
  
    /// <param name="forceServiceAddress">if not null, overrides the service address in the options</param>
    public ClousotServiceOptions(string[] args, string forceServiceAddress)
    {
      this.Parse(args, false);

      if(!string.IsNullOrEmpty(forceServiceAddress))
      {
        this.serviceAddress = forceServiceAddress;
      }
    }

    protected override bool TreatGeneralArgumentsAsUnknown { get { return true; } }
    protected override bool TreatHelpArgumentAsUnknown { get { return true; } }

    public readonly List<string> remainingArgs = new List<string>();

    protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
    {
      this.remainingArgs.Add(args[index]);
      
      return true;
    }

    [OptionDescription("Run the analysis on the service")]
    public bool cloudot = false;

    [OptionDescription("Number of service connection retries")]
    public uint serviceRetries = 2; // This is mostly needed for regression tests, where all the world calls the service at the same time

    [OptionDescription("Service uri")]
    public string serviceAddress = ClousotWCFServiceCommon.BaseAddress;

    [OptionDescription("Try to start the CCCheck Windows Service")]
    public bool windowsService = false; // essentially needed to start the windows service, if it is in a stop state. If it is not, then WCF will find the windows service automatically

    [OptionDescription("Skip creating analysis package, and use shared directories instead -- mainly for debugging")]
    // Using shared dirs seems to be a lot slower (from mini benchmarks)
    public bool useSharedDirs = false;

    private void HACK_FOR_Clousot()
    {
      this.cloudot = this.windowsService = this.useSharedDirs = false;
      this.serviceRetries = 3;
      this.serviceAddress = null;
    }
  }
}
