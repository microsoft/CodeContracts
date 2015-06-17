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
using System.IO;
using System.Diagnostics;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis {


  /// <summary>
  /// Put any option as a public field. The name is what gets parsed on the command line.
  /// It can be bool, int, string, or List&lt;string&gt;, or List&lt;int&gt; or enum type
  ///
  /// Derived options are public const strings
  /// </summary>
  public class GeneralOptions : OptionParsing {

    #region Parsing
    public static GeneralOptions ParseCommandLineArguments(string[] args)
    {
      GeneralOptions options = new GeneralOptions();
      options.Parse(args);
      return options;
    }

    protected override bool ParseUnknown(string arg, string[] args, ref int index, string equalArgument)
    {
      // don't care about case
      arg = arg.ToLower();

      // see if it is a break
      if (arg == "break") {
        System.Diagnostics.Debugger.Break();
        return true;
      }

      if (arg == "echo")
      {
        int j = 0;
        foreach (string s in args)
        {
          Console.WriteLine("arg{0} = '{1}'", j, s);
          j++;
        }
        Console.WriteLine("curent dir = '{0}'", Environment.CurrentDirectory);
        return true;
      }

      return false;
    }


    private GeneralOptions()
    {
    }

    public static void PrintUsageAndExit()
    {
      GeneralOptions defaultOptions = new GeneralOptions();

      Console.WriteLine("usage: <general-option>* <assembly>+");
      Console.WriteLine("\nwhere <general-option> is one of");
      defaultOptions.PrintOptions("");
      Console.WriteLine("\nwhere derived options are of");
      defaultOptions.PrintDerivedOptions("");
      Console.WriteLine("\nTo clear a list, use -<option>=");
      Environment.Exit(-1);
    }

    #endregion 


      
    #region Atomic Options, i.e. actual state of options

    [OptionDescription("Output file")]
    public string o = "callgraph.dot";
    #endregion

    #region Derived options, define as const strings


    #endregion


    #region Public accessors
    public List<string> Assemblies { get { return this.GeneralArguments; } }
    public string OutputFile { get { return this.o; } }
    #endregion
  }

}
