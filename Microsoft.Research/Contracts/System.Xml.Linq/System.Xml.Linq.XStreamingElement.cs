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
using System.IO;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Specifies serialization options.
  [Flags]
  public enum SaveOptions
  {
    // Summary:
    //     Format (indent) the XML while serializing.
    None = 0,
    //
    // Summary:
    //     Preserve all insignificant white space while serializing.
    DisableFormatting = 1,
  }

  // Summary:
  //     Represents elements in an XML tree that supports deferred streaming output.
  public class XStreamingElement
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class from the
    //     specified System.Xml.Linq.XName.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the name of the element.
    public XStreamingElement(XName name)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XStreamingElement class
    //     with the specified name and content.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the element name.
    //
    //   content:
    //     The contents of the element.
    public XStreamingElement(XName name, object content)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XStreamingElement class
    //     with the specified name and content.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the element name.
    //
    //   content:
    //     The contents of the element.
    public XStreamingElement(XName name, params object[] content)
    {
      Contract.Requires(name != null);
    }

    // Summary:
    //     Gets or sets the name of this streaming element.
    //
    // Returns:
    //     An System.Xml.Linq.XName that contains the name of this streaming element.
    public XName Name
    {
      get
      {
        Contract.Ensures(Contract.Result<XName>() != null);
        return default(XName);
      }
      set
      {
        Contract.Requires(value != null);
      } 
    }

    // Summary:
    //     Adds the specified content as children to this System.Xml.Linq.XStreamingElement.
    //
    // Parameters:
    //   content:
    //     Content to be added to the streaming element.
    extern public void Add(object content);
    //
    // Summary:
    //     Adds the specified content as children to this System.Xml.Linq.XStreamingElement.
    //
    // Parameters:
    //   content:
    //     Content to be added to the streaming element.
    extern public void Add(params object[] content);
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this streaming element to a file.
    //
    // Parameters:
    //   fileName:
    //     A System.String that contains the name of the file.
    public void Save(string fileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
    }
#endif
    //
    // Summary:
    //     Serialize this streaming element to a System.IO.TextWriter.
    //
    // Parameters:
    //   textWriter:
    //     A System.IO.TextWriter that the System.Xml.Linq.XStreamingElement will be
    //     written to.
    public void Save(TextWriter textWriter)
    {
      Contract.Requires(textWriter != null);
    }
    //
    // Summary:
    //     Serialize this streaming element to an System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     A System.Xml.XmlWriter that the System.Xml.Linq.XElement will be written
    //     to.
    public void Save(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this streaming element to a file, optionally disabling formatting.
    //
    // Parameters:
    //   fileName:
    //     A System.String that contains the name of the file.
    //
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    public void Save(string fileName, SaveOptions options)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
    }
#endif
    //
    // Summary:
    //     Serialize this streaming element to a System.IO.TextWriter, optionally disabling
    //     formatting.
    //
    // Parameters:
    //   textWriter:
    //     The System.IO.TextWriter to output the XML to.
    //
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    public void Save(TextWriter textWriter, SaveOptions options)
    {
      Contract.Requires(textWriter != null);
    }
    //
    // Summary:
    //     Returns the XML for this streaming element, optionally disabling formatting.
    //
    // Parameters:
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    //
    // Returns:
    //     A System.String containing the XML.
    public string ToString(SaveOptions options)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Writes this streaming element to an System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     An System.Xml.XmlWriter into which this method will write.
    public void WriteTo(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }
  }
}
