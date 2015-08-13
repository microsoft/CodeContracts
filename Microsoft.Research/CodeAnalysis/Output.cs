// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private void ObjectInvariant()
        {
            Contract.Invariant(options != null);
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