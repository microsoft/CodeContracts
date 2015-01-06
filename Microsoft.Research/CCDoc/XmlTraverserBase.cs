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
using System.Xml;
using System.Diagnostics.Contracts;

namespace CCDoc {
  /// <summary>
  /// A base class for building efficient xml transformations
  /// </summary>
  [ContractVerification(true)]
  public abstract class XmlTraverserBase {
    /// <summary>
    /// Initializes a new instance of the visitor
    /// </summary>
    protected XmlTraverserBase() { }

    /// <summary>
    /// Applies the transformation from the reader to the writer
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="writer"></param>
    public void Transform(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      while (reader.Read())
        this.WriteNodeSingle(reader, writer);
    }

    private void WriteNodeSingle(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      switch (reader.NodeType) {
        case XmlNodeType.Element:
          this.WriteElement(reader, writer);
          break;
        case XmlNodeType.Text:
          this.WriteText(reader, writer);
          break;
        case XmlNodeType.Whitespace:
        case XmlNodeType.SignificantWhitespace:
          this.WriteWhitespace(reader, writer);
          break;
        case XmlNodeType.CDATA:
          this.WriteCDATA(reader, writer);
          break;
        case XmlNodeType.EntityReference:
          Contract.Assume(false, "entityref not supported");
          writer.WriteString(reader.Name);
          break;
        case XmlNodeType.XmlDeclaration:
        case XmlNodeType.ProcessingInstruction:
          var pinstr = reader.Name;
          Contract.Assume(!String.IsNullOrEmpty(pinstr));
          writer.WriteProcessingInstruction(pinstr, reader.Value);
          break;
        case XmlNodeType.DocumentType:
          writer.WriteDocType(
              reader.Name,
              reader.GetAttribute("PUBLIC"),
              reader.GetAttribute("SYSTEM"),
              reader.Value);
          break;
        case XmlNodeType.Comment:
          WriteComment(reader, writer);
          break;
        case XmlNodeType.EndElement:
          this.WriteEndElement(reader, writer);
          break;
      }
    }

    /// <summary>
    /// Writes the end of an element and closes it
    /// </summary>
    protected virtual void WriteEndElement(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteEndElement();
    }

    /// <summary>
    /// Writes a whitespace from the reader to the writer
    /// </summary>
    protected virtual void WriteWhitespace(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteWhitespace(reader.Value);
    }

    /// <summary>
    /// Writes a comment from the reader to the writer
    /// </summary>
    protected virtual void WriteComment(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteComment(reader.Value);
    }

    /// <summary>
    /// Writes a CDATA from the reader to the writer
    /// </summary>
    protected virtual void WriteCDATA(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteCData(reader.Value);
    }

    /// <summary>
    /// Writes text from the reader to the writer
    /// </summary>
    protected virtual void WriteText(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteString(reader.Value);
    }

    /// <summary>
    /// Write element
    /// </summary>
    protected virtual void WriteElement(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteStartElement(reader.LocalName);
      var emptyElement = reader.IsEmptyElement;
      this.WriteAttributes(reader, writer);
      if (emptyElement)
        this.WriteEndElement(reader, writer);
    }

    /// <summary>
    /// Writes the element attributes from the reader to the writer
    /// </summary>
    protected virtual void WriteAttributes(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      if (reader.MoveToFirstAttribute()) {
        do { writer.WriteAttributeString(reader.Name, reader.Value); }
        while (reader.MoveToNextAttribute());
      }
    }

    /// <summary>
    /// Advances the read pointer to the end of the element
    /// </summary>
    protected void SkipElement(XmlReader reader) {
      Contract.Requires(reader != null);

      using (var elementReader = reader.ReadSubtree())
        while (elementReader.Read()) { }
    }

    /// <summary>
    /// Advances the read pointer to the end of the element
    /// </summary>
    protected void WriteElementNoDescendant(XmlReader reader, XmlWriter writer) {
      Contract.Requires(reader != null);
      Contract.Requires(writer != null);

      writer.WriteStartElement(reader.LocalName);
      using (var elementReader = reader.ReadSubtree()) {
        this.WriteAttributes(reader, writer);
        while (elementReader.Read()) { }
      }
      this.WriteEndElement(reader, writer);
    }
  }
}
