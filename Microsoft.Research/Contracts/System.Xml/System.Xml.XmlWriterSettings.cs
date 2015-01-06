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
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Specifies a set of features to support on the System.Xml.XmlWriter object
  //     created by the Overload:System.Xml.XmlWriter.Create method.
  public sealed class XmlWriterSettings
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlWriterSettings class.
    //public XmlWriterSettings();

    // Summary:
    //     Gets or sets a value indicating whether to do character checking.
    //
    // Returns:
    //     true to do character checking; otherwise false. The default is true.
    //public bool CheckCharacters { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Xml.XmlWriter should also
    //     close the underlying stream or System.IO.TextWriter when the System.Xml.XmlWriter.Close()
    //     method is called.
    //
    // Returns:
    //     true to also close the underlying stream or System.IO.TextWriter; otherwise
    //     false. The default is false.
    //public bool CloseOutput { get; set; }
    //
    // Summary:
    //     Gets or sets the level of conformance which the System.Xml.XmlWriter complies
    //     with.
    //
    // Returns:
    //     One of the System.Xml.ConformanceLevel values. The default is ConformanceLevel.Document.
    //public ConformanceLevel ConformanceLevel { get; set; }
    //
    // Summary:
    //     Gets or sets the text encoding to use.
    //
    // Returns:
    //     The text encoding to use. The default is Encoding.UTF8.
    //public Encoding Encoding { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to indent elements.
    //
    // Returns:
    //     true to write individual elements on new lines and indent; otherwise false.
    //     The default is false.
    //public bool Indent { get; set; }
    //
    // Summary:
    //     Gets or sets the character string to use when indenting. This setting is
    //     used when the System.Xml.XmlWriterSettings.Indent property is set to true.
    //
    // Returns:
    //     The character string to use when indenting. This can be set to any string
    //     value. However, to ensure valid XML, you should specify only valid white
    //     space characters, such as space characters, tabs, carriage returns, or line
    //     feeds. The default is two spaces.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value assigned to the System.Xml.XmlWriterSettings.IndentChars is null.
    public string IndentChars 
    { 
      get 
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets or sets the character string to use for line breaks.
    //
    // Returns:
    //     The character string to use for line breaks. This can be set to any string
    //     value. However, to ensure valid XML, you should specify only valid white
    //     space characters, such as space characters, tabs, carriage returns, or line
    //     feeds. The default is \r\n (carriage return, new line).
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value assigned to the System.Xml.XmlWriterSettings.NewLineChars is null.
    public string NewLineChars
        { 
      get 
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    
    //
    // Summary:
    //     Gets or sets a value indicating whether to normalize line breaks in the output.
    //
    // Returns:
    //     One of the System.Xml.NewLineHandling values. The default is System.Xml.NewLineHandling.Replace.
    //public NewLineHandling NewLineHandling { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to write attributes on a new line.
    //
    // Returns:
    //     true to write attributes on individual lines; otherwise false. The default
    //     is false.  Note: This setting has no effect when the System.Xml.XmlWriterSettings.Indent
    //     property value is false.  When System.Xml.XmlWriterSettings.NewLineOnAttributes
    //     is set to true, each attribute is pre-pended with a new line and one extra
    //     level of indentation.
    //public bool NewLineOnAttributes { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to write an XML declaration.
    //
    // Returns:
    //     true to omit the XML declaration; otherwise false. The default is false,
    //     an XML declaration is written.
    //public bool OmitXmlDeclaration { get; set; }
    //
    // Summary:
    //     Gets the method used to serialize the System.Xml.XmlWriter output.
    //
    // Returns:
    //     One of the System.Xml.XmlOutputMethod values. The default is System.Xml.XmlOutputMethod.Xml.
    //public XmlOutputMethod OutputMethod { get; internal set; }

    // Summary:
    //     Creates a copy of the System.Xml.XmlWriterSettings instance.
    //
    // Returns:
    //     The cloned System.Xml.XmlWriterSettings object.
    public XmlWriterSettings Clone()
    {
      Contract.Ensures(Contract.Result<XmlWriterSettings>() != null);

      return default(XmlWriterSettings);
    }

    //
    // Summary:
    //     Resets the members of the settings class to their default values.
    //public void Reset();
  }
}
