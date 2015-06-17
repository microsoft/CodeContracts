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
using System.Xml.Schema;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Represents the document type declaration.
  public class XmlDocumentType //: XmlLinkedNode
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlDocumentType class.
    //
    // Parameters:
    //   name:
    //     The qualified name; see the System.Xml.XmlDocumentType.Name property.
    //
    //   publicId:
    //     The public identifier; see the System.Xml.XmlDocumentType.PublicId property.
    //
    //   systemId:
    //     The system identifier; see the System.Xml.XmlDocumentType.SystemId property.
    //
    //   internalSubset:
    //     The DTD internal subset; see the System.Xml.XmlDocumentType.InternalSubset
    //     property.
    //
    //   doc:
    //     The parent document.
    //protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc);

    // Summary:
    //     Gets the collection of System.Xml.XmlEntity nodes declared in the document
    //     type declaration.
    //
    // Returns:
    //     An System.Xml.XmlNamedNodeMap containing the XmlEntity nodes. The returned
    //     XmlNamedNodeMap is read-only.
    //public XmlNamedNodeMap Entities { get; }
    //
    // Summary:
    //     Gets the value of the document type definition (DTD) internal subset on the
    //     DOCTYPE declaration.
    //
    // Returns:
    //     The DTD internal subset on the DOCTYPE. If there is no DTD internal subset,
    //     String.Empty is returned.
    public string InternalSubset
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a value indicating whether the node is read-only.
    //
    // Returns:
    //     true if the node is read-only; otherwise false.  Because DocumentType nodes
    //     are read-only, this property always returns true.
    //public override bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets the local name of the node.
    //
    // Returns:
    //     For DocumentType nodes, this property returns the name of the document type.
    //public override string LocalName { get; }
    //
    // Summary:
    //     Gets the qualified name of the node.
    //
    // Returns:
    //     For DocumentType nodes, this property returns the name of the document type.
    //public override string Name { get; }
    //
    // Summary:
    //     Gets the type of the current node.
    //
    // Returns:
    //     For DocumentType nodes, this value is XmlNodeType.DocumentType.
    //public override XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the collection of System.Xml.XmlNotation nodes present in the document
    //     type declaration.
    //
    // Returns:
    //     An System.Xml.XmlNamedNodeMap containing the XmlNotation nodes. The returned
    //     XmlNamedNodeMap is read-only.
    public XmlNamedNodeMap Notations 
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlNamedNodeMap>() != null);

        return default(XmlNamedNodeMap);
      }
    }
    //
    // Summary:
    //     Gets the value of the public identifier on the DOCTYPE declaration.
    //
    // Returns:
    //     The public identifier on the DOCTYPE. If there is no public identifier, String.Empty
    //     is returned.
    public string PublicId 
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the value of the system identifier on the DOCTYPE declaration.
    //
    // Returns:
    //     The system identifier on the DOCTYPE. If there is no system identifier, String.Empty
    //     is returned.
    public string SystemId 
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    // Summary:
    //     Creates a duplicate of this node.
    //
    // Parameters:
    //   deep:
    //     true to recursively clone the subtree under the specified node; false to
    //     clone only the node itself. For document type nodes, the cloned node always
    //     includes the subtree, regardless of the parameter setting.
    //
    // Returns:
    //     The cloned node.
    //public override XmlNode CloneNode(bool deep);
    //
    // Summary:
    //     Saves all the children of the node to the specified System.Xml.XmlWriter.
    //     For XmlDocumentType nodes, this method has no effect.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //public override void WriteContentTo(XmlWriter w);
    //
    // Summary:
    //     Saves the node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //public override void WriteTo(XmlWriter w);
  }
}

#endif