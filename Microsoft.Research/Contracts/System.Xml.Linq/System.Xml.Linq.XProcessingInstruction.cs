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

namespace System.Xml.Linq
{
    using System.Diagnostics.Contracts;

    public class XProcessingInstruction : XNode
    {
        public string Data
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return null;
            }
            set
            {
                Contract.Requires(value != null);
            }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                return XmlNodeType.ProcessingInstruction;
            }
        }

        public string Target
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return null;
            }
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        public XProcessingInstruction(string target, string data)
        {
            Contract.Requires(!string.IsNullOrEmpty(target));
            Contract.Requires(data != null);
        }

        public XProcessingInstruction(XProcessingInstruction other)
        {
            Contract.Requires(other != null);
        }

        public override void WriteTo(XmlWriter writer)
        {
        }
    }
}
