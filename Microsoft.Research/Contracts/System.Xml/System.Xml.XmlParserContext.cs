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
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Provides all the context information required by the System.Xml.XmlReader
  //     to parse an XML fragment.
  public class XmlParserContext
  {
    // Summary:
    //     Initializes a new instance of the XmlParserContext class with the specified
    //     System.Xml.XmlNameTable, System.Xml.XmlNamespaceManager, xml:lang, and xml:space
    //     values.
    //
    // Parameters:
    //   nt:
    //     The System.Xml.XmlNameTable to use to atomize strings. If this is null, the
    //     name table used to construct the nsMgr is used instead. For more information
    //     about atomized strings, see System.Xml.XmlNameTable.
    //
    //   nsMgr:
    //     The System.Xml.XmlNamespaceManager to use for looking up namespace information,
    //     or null.
    //
    //   xmlLang:
    //     The xml:lang scope.
    //
    //   xmlSpace:
    //     An System.Xml.XmlSpace value indicating the xml:space scope.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     nt is not the same XmlNameTable used to construct nsMgr.
    //public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace);
    ////
    // Summary:
    //     Initializes a new instance of the XmlParserContext class with the specified
    //     System.Xml.XmlNameTable, System.Xml.XmlNamespaceManager, xml:lang, xml:space,
    //     and encoding.
    //
    // Parameters:
    //   nt:
    //     The System.Xml.XmlNameTable to use to atomize strings. If this is null, the
    //     name table used to construct the nsMgr is used instead. For more information
    //     on atomized strings, see System.Xml.XmlNameTable.
    //
    //   nsMgr:
    //     The System.Xml.XmlNamespaceManager to use for looking up namespace information,
    //     or null.
    //
    //   xmlLang:
    //     The xml:lang scope.
    //
    //   xmlSpace:
    //     An System.Xml.XmlSpace value indicating the xml:space scope.
    //
    //   enc:
    //     An System.Text.Encoding object indicating the encoding setting.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     nt is not the same XmlNameTable used to construct nsMgr.
    //public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace, Encoding enc);
    //
    // Summary:
    //     Initializes a new instance of the XmlParserContext class with the specified
    //     System.Xml.XmlNameTable, System.Xml.XmlNamespaceManager, base URI, xml:lang,
    //     xml:space, and document type values.
    //
    // Parameters:
    //   nt:
    //     The System.Xml.XmlNameTable to use to atomize strings. If this is null, the
    //     name table used to construct the nsMgr is used instead. For more information
    //     about atomized strings, see System.Xml.XmlNameTable.
    //
    //   nsMgr:
    //     The System.Xml.XmlNamespaceManager to use for looking up namespace information,
    //     or null.
    //
    //   docTypeName:
    //     The name of the document type declaration.
    //
    //   pubId:
    //     The public identifier.
    //
    //   sysId:
    //     The system identifier.
    //
    //   internalSubset:
    //     The internal DTD subset.
    //
    //   baseURI:
    //     The base URI for the XML fragment (the location from which the fragment was
    //     loaded).
    //
    //   xmlLang:
    //     The xml:lang scope.
    //
    //   xmlSpace:
    //     An System.Xml.XmlSpace value indicating the xml:space scope.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     nt is not the same XmlNameTable used to construct nsMgr.
    //public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace);
    //
    // Summary:
    //     Initializes a new instance of the XmlParserContext class with the specified
    //     System.Xml.XmlNameTable, System.Xml.XmlNamespaceManager, base URI, xml:lang,
    //     xml:space, encoding, and document type values.
    //
    // Parameters:
    //   nt:
    //     The System.Xml.XmlNameTable to use to atomize strings. If this is null, the
    //     name table used to construct the nsMgr is used instead. For more information
    //     about atomized strings, see System.Xml.XmlNameTable.
    //
    //   nsMgr:
    //     The System.Xml.XmlNamespaceManager to use for looking up namespace information,
    //     or null.
    //
    //   docTypeName:
    //     The name of the document type declaration.
    //
    //   pubId:
    //     The public identifier.
    //
    //   sysId:
    //     The system identifier.
    //
    //   internalSubset:
    //     The internal DTD subset.
    //
    //   baseURI:
    //     The base URI for the XML fragment (the location from which the fragment was
    //     loaded).
    //
    //   xmlLang:
    //     The xml:lang scope.
    //
    //   xmlSpace:
    //     An System.Xml.XmlSpace value indicating the xml:space scope.
    //
    //   enc:
    //     An System.Text.Encoding object indicating the encoding setting.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     nt is not the same XmlNameTable used to construct nsMgr.
    //public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc);

    // Summary:
    //     Gets or sets the base URI.
    //
    // Returns:
    //     The base URI to use to resolve the DTD file.
    public string BaseURI 
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    
    //
    // Summary:
    //     Gets or sets the name of the document type declaration.
    //
    // Returns:
    //     The name of the document type declaration.
    public string DocTypeName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    //
    // Summary:
    //     Gets or sets the encoding type.
    //
    // Returns:
    //     An System.Text.Encoding object indicating the encoding type.
    //public Encoding Encoding { get; set; }
    //
    // Summary:
    //     Gets or sets the internal DTD subset.
    //
    // Returns:
    //     The internal DTD subset. For example, this property returns everything between
    //     the square brackets <!DOCTYPE doc [...]>.
    public string InternalSubset
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    //
    // Summary:
    //     Gets or sets the System.Xml.XmlNamespaceManager.
    //
    // Returns:
    //     The XmlNamespaceManager.
    //public XmlNamespaceManager NamespaceManager { get; set; }
    //
    // Summary:
    //     Gets the System.Xml.XmlNameTable used to atomize strings. For more information
    //     on atomized strings, see System.Xml.XmlNameTable.
    //
    // Returns:
    //     The XmlNameTable.
    //public XmlNameTable NameTable { get; set; }
    //
    // Summary:
    //     Gets or sets the public identifier.
    //
    // Returns:
    //     The public identifier.
    public string PublicId
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    //
    // Summary:
    //     Gets or sets the system identifier.
    //
    // Returns:
    //     The system identifier.
    public string SystemId
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    //
    // Summary:
    //     Gets or sets the current xml:lang scope.
    //
    // Returns:
    //     The current xml:lang scope. If there is no xml:lang in scope, String.Empty
    //     is returned.
    public string XmlLang
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //; set; 
    }
    
    //
    // Summary:
    //     Gets or sets the current xml:space scope.
    //
    // Returns:
    //     An System.Xml.XmlSpace value indicating the xml:space scope.
    //public XmlSpace XmlSpace { get; set; }
  }
}
