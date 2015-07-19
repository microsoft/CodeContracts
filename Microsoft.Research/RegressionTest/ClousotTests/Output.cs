// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tests
{
    public class Output : Microsoft.Research.DataStructures.IVerySimpleLineWriterWithEncoding, Microsoft.Research.DataStructures.ISimpleLineWriterWithEncoding
    {
        private readonly string name;
        private readonly TextWriter textWriter;

        // Do not use a static ConsoleOutput because the Visual Studio test environment
        // uses a different Console for each test case

        public static readonly Output Ignore = new Output("Ignore");

        public static Output ConsoleOutputFor(string name)
        {
            return new Output(String.Format("Console::{0}", name), Console.Out);
        }

        private Output(string name)
        {
            this.name = name;
        }

        public Output(string name, TextWriter textWriter)
          : this(name)
        {
            this.textWriter = textWriter;
        }

        public void WriteLine(string value)
        {
            if (textWriter == null)
                return;
            try
            {
                textWriter.WriteLine(value);
            }
            catch (Exception e)
            {
                //Console.WriteLine(value);
                Console.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, value);
            }
        }

        public void WriteLine(string value, params object[] arg)
        {
            if (textWriter == null)
                return;
            try
            {
                textWriter.WriteLine(value ?? "", arg);
            }
            catch (Exception e)
            {
                //Console.WriteLine(value ?? "", arg);
                Console.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, String.Format(value ?? "", arg));
            }
        }

        public Encoding Encoding
        {
            get
            {
                return textWriter == null ? Encoding.Default : textWriter.Encoding;
            }
        }

        public void OutputDataReceivedEventHandler(Object sender, DataReceivedEventArgs e)
        {
            this.WriteLine(e.Data);
        }
        public void ErrDataReceivedEventHandler(Object sender, DataReceivedEventArgs e)
        {
            this.WriteLine(e.Data);
        }

        public void Dispose()
        {
            // does nothing
        }
    }
}
