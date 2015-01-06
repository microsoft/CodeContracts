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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class TextWriterOutput : SimpleLineWriter.FromTextWriter, IOutput
  {
    private readonly IFrameworkLogOptions options;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.options != null);
    }

    public TextWriterOutput(TextWriter tw, IFrameworkLogOptions options)
      : base(tw)
    {
      Contract.Requires(tw != null);
      Contract.Requires(options != null);

      this.options = options;
    }

    #region IOutput Members

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      this.WriteLine("{0}: message : Suggested {1}: {2}", pc.PrimarySourceContext(), kind, suggestion);
    }

    public void Statistic(string format, params object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      this.WriteLine(format, args);
    }

    public void FinalStatistic(string assembly, string msg)
    {
      this.WriteLine("{0} : {1}", assembly, msg);
    }

    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      this.WriteLine("{0}({1},{2}): {3}", error.FileName, error.Line, error.Column, error.ErrorText);
    }

    public IFrameworkLogOptions LogOptions
    {
      get { return options; }
    }

    public void Close()
    {
      if (this.textWriter != Console.Out)
      {
        this.textWriter.Flush();
        this.textWriter.Close();
      }
    }

    #endregion

  }

  public class TextWriterOutputWithPrefix : TextWriterOutput
  {
    private readonly string prefix;

    public TextWriterOutputWithPrefix(TextWriter tw, IFrameworkLogOptions options, string prefix)
      : base(tw, options)
    {
      Contract.Requires(tw != null);
      Contract.Requires(options != null);
      Contract.Requires(prefix == null || !prefix.Contains("{"));

      this.prefix = prefix ?? "";
    }

    public override void WriteLine(string value, params object[] arg)
    {
      base.WriteLine(prefix + value, arg);
    }
  }
}