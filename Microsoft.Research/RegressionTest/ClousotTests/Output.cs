// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit.Abstractions;

namespace Tests
{
    public class Output : Microsoft.Research.DataStructures.IVerySimpleLineWriterWithEncoding, Microsoft.Research.DataStructures.ISimpleLineWriterWithEncoding
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly string name;
        private readonly TextWriter textWriter;

        // Do not use a static ConsoleOutput because the Visual Studio test environment
        // uses a different Console for each test case

        private static Output ignore;

        public static Output Ignore(ITestOutputHelper testOutputHelper)
        {
            if (ignore == null)
                ignore = new Output(testOutputHelper, "Ignore");

            return ignore;
        }

        public static Tuple<Output, StringWriter> ConsoleOutputFor(ITestOutputHelper testOutputHelper, string name)
        {
            StringWriter writer = new StringWriter();
            Output output = new Output(testOutputHelper, String.Format("Console::{0}", name), writer);
            return Tuple.Create(output, writer);
        }

        private Output(ITestOutputHelper testOutputHelper, string name)
        {
            this.testOutputHelper = testOutputHelper;
            this.name = name;
        }

        public Output(ITestOutputHelper testOutputHelper, string name, TextWriter textWriter)
          : this(testOutputHelper, name)
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
                //testOutputHelper.WriteLine(value);
                testOutputHelper.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, value);
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
                //testOutputHelper.WriteLine(value ?? "", arg);
                testOutputHelper.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, String.Format(value ?? "", arg));
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
