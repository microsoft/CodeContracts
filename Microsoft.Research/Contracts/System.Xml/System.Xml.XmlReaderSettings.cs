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
using System.Xml.Schema;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Specifies a set of features to support on the System.Xml.XmlReader object
  //     created by the Overload:System.Xml.XmlReader.Create method.
  public sealed class XmlReaderSettings
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlReaderSettings class.
    //public XmlReaderSettings();

    // Summary:
    //     Gets or sets a value indicating whether to do character checking.
    //
    // Returns:
    //     true to do character checking; otherwise false. The default is true.  Note:
    //     If the System.Xml.XmlReader is processing text data, it always checks that
    //     the XML names and text content are valid, regardless of the property setting.
    //     Setting System.Xml.XmlReaderSettings.CheckCharacters to false turns off character
    //     checking for character entity references.
    //public bool CheckCharacters { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the underlying stream or System.IO.TextReader
    //     should be closed when the reader is closed.
    //
    // Returns:
    //     true to close the underlying stream or System.IO.TextReader when the reader
    //     is closed; otherwise false. The default is false.
    //public bool CloseInput { get; set; }
    //
    // Summary:
    //     Gets or sets the level of conformance which the System.Xml.XmlReader will
    //     comply.
    //
    // Returns:
    //     One of the System.Xml.ConformanceLevel values. The default is ConformanceLevel.Document.
    //public ConformanceLevel ConformanceLevel { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to ignore comments.
    //
    // Returns:
    //     true to ignore comments; otherwise false. The default is false.
    //public bool IgnoreComments { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to ignore processing instructions.
    //
    // Returns:
    //     true to ignore processing instructions; otherwise false. The default is false.
    //public bool IgnoreProcessingInstructions { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to ignore insignificant white space.
    //
    // Returns:
    //     true to ignore white space; otherwise false. The default is false.
    //public bool IgnoreWhitespace { get; set; }
    //
    // Summary:
    //     Gets or sets line number offset of the System.Xml.XmlReader object.
    //
    // Returns:
    //     The line number offset. The default is 0.
    public int LineNumberOffset
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }
    
    //
    // Summary:
    //     Gets or sets line position offset of the System.Xml.XmlReader object.
    //
    // Returns:
    //     The line number offset. The default is 0.
    public int LinePositionOffset
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }
    
    //
    // Summary:
    //     Gets or sets a value indicating the maximum allowable number of characters
    //     in a document that result from expanding entities.
    //
    // Returns:
    //     The maximum allowable number of characters from expanded entities. The default
    //     is 0.
    public long MaxCharactersFromEntities
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0L);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0L);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating the maximum allowable number of characters
    //     XML document. A zero (0) value means no limits on the size of the XML document.
    //     A non-zero value specifies the maximum size, in characters.
    //
    // Returns:
    //     The maximum allowable number of characters in an XML document. The default
    //     is 0.
    public long MaxCharactersInDocument
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0L);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0L);
      }
    }
    //
    // Summary:
    //     Gets or sets the System.Xml.XmlNameTable used for atomized string comparisons.
    //
    // Returns:
    //     The System.Xml.XmlNameTable that stores all the atomized strings used by
    //     all System.Xml.XmlReader instances created using this System.Xml.XmlReaderSettings
    //     object.  The default is null. The created System.Xml.XmlReader instance will
    //     use a new empty System.Xml.NameTable if this value is null.
    //public XmlNameTable NameTable { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to prohibit document type definition
    //     (DTD) processing.
    //
    // Returns:
    //     true to prohibit DTD processing; otherwise false. The default is true.
    //public bool ProhibitDtd { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Xml.Schema.XmlSchemaSet to use when performing schema
    //     validation.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaSet to use. The default is an empty System.Xml.Schema.XmlSchemaSet
    //     object.
#if !SILVERLIGHT
    public XmlSchemaSet Schemas 
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaSet>() != null);

        return default(XmlSchemaSet);
      }
      //set; 
    }
#endif
    //
    // Summary:
    //     Gets or sets a value indicating the schema validation settings. This setting
    //     applies to schema validating System.Xml.XmlReader objects (System.Xml.XmlReaderSettings.ValidationType
    //     property set to ValidationType.Schema).
    //
    // Returns:
    //     A set of System.Xml.Schema.XmlSchemaValidationFlags values. System.Xml.Schema.XmlSchemaValidationFlags.ProcessIdentityConstraints
    //     and System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes are enabled
    //     by default. System.Xml.Schema.XmlSchemaValidationFlags.ProcessInlineSchema,
    //     System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation, and System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings
    //     are disabled by default.
    //public XmlSchemaValidationFlags ValidationFlags { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Xml.XmlReader will perform
    //     validation or type assignment when reading.
    //
    // Returns:
    //     One of the System.Xml.ValidationType values. The default is ValidationType.None.
    //public ValidationType ValidationType { get; set; }
    //
    // Summary:
    //     Sets the System.Xml.XmlResolver used to access external documents.
    //
    // Returns:
    //     An System.Xml.XmlResolver used to access external documents. If set to null,
    //     an System.Xml.XmlException is thrown when the System.Xml.XmlReader tries
    //     to access an external resource. The default is a new System.Xml.XmlUrlResolver
    //     with no credentials.
    //public XmlResolver XmlResolver { set; }

    // Summary:
    //     Occurs when the reader encounters validation errors.
    //public event ValidationEventHandler ValidationEventHandler;

    // Summary:
    //     Creates a copy of the System.Xml.XmlReaderSettings instance.
    //
    // Returns:
    //     The cloned System.Xml.XmlReaderSettings object.
    public XmlReaderSettings Clone()
    {
      Contract.Ensures(Contract.Result<XmlReaderSettings>()!= null);

      return default(XmlReaderSettings);
    }
    //
    // Summary:
    //     Resets the members of the settings class to their default values.
    //public void Reset();
  }
}
