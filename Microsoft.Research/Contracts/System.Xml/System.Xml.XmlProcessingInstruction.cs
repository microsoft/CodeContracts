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

#if !SILVERLIGHT

using System;


namespace System.Xml
{
  // Summary:
  //     Represents a processing instruction, which XML defines to keep processor-specific
  //     information in the text of the document.
  public class XmlProcessingInstruction //: XmlLinkedNode
  {
    // Summary:
    //     Gets or sets the content of the processing instruction, excluding the target.
    //
    // Returns:
    //     The content of the processing instruction, excluding the target.
    //public string Data { get; set; }
    
    // .. Overridden methods here ..
    //
    // Summary:
    //     Gets the target of the processing instruction.
    //
    // Returns:
    //     The target of the processing instruction.
    // f: can be null
    // public string Target { get; }
  }
}

#endif