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
using System.Text;
using System.Diagnostics.Contracts;
#if !SILVERLIGHT
using System.Xml.XPath;
#endif

namespace System.Xml
{
  [ContractClass(typeof(XmlWriterContracts))]
  public abstract class XmlWriter //: IDisposable
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlWriter class.
    //protected XmlWriter();

    // Summary:
    //     Gets the System.Xml.XmlWriterSettings object used to create this System.Xml.XmlWriter
    //     instance.
    //
    // Returns:
    //     The System.Xml.XmlWriterSettings object used to create this writer instance.
    //     If this writer was not created using the Overload:System.Xml.XmlWriter.Create
    //     method, this property returns null.
    //public virtual XmlWriterSettings Settings { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the state of the writer.
    //
    // Returns:
    //     One of the System.Xml.WriteState values.
    //public abstract WriteState WriteState { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the current xml:lang scope.
    //
    // Returns:
    //     The current xml:lang scope.
    //public virtual string XmlLang { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets an System.Xml.XmlSpace representing
    //     the current xml:space scope.
    //
    // Returns:
    //     An XmlSpace representing the current xml:space scope.  Value Meaning None
    //     This is the default if no xml:space scope exists. Default The current scope
    //     is xml:space="default". Preserve The current scope is xml:space="preserve".
    //public virtual XmlSpace XmlSpace { get; }

    // Summary:
    //     When overridden in a derived class, closes this stream and the underlying
    //     stream.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     A call is made to write more output after Close has been called or the result
    //     of this call is an invalid XML document.
    //public abstract void Close();
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified stream.
    //
    // Parameters:
    //   output:
    //     The stream to which you want to write. The System.Xml.XmlWriter writes XML
    //     1.0 text syntax and appends it to the specified stream.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The stream value is null.
    public static XmlWriter Create(Stream output)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified filename.
    //
    // Parameters:
    //   outputFileName:
    //     The file to which you want to write. The System.Xml.XmlWriter creates a file
    //     at the specified path and writes to it in XML 1.0 text syntax. The outputFileName
    //     must be a file system path.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The url value is null.
#if !SILVERLIGHT
    public static XmlWriter Create(string outputFileName)
    {
      Contract.Requires(outputFileName != null);
      Contract.Requires(!String.IsNullOrEmpty(outputFileName));
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
#endif
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified System.Text.StringBuilder.
    //
    // Parameters:
    //   output:
    //     The System.Text.StringBuilder to which to write to. Content written by the
    //     System.Xml.XmlWriter is appended to the System.Text.StringBuilder.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The builder value is null.
    public static XmlWriter Create(StringBuilder output)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified System.IO.TextWriter.
    //
    // Parameters:
    //   output:
    //     The System.IO.TextWriter to which you want to write. The System.Xml.XmlWriter
    //     writes XML 1.0 text syntax and appends it to the specified System.IO.TextWriter.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The text value is null.
    public static XmlWriter Create(TextWriter output)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified System.Xml.XmlWriter
    //     object.
    //
    // Parameters:
    //   output:
    //     The System.Xml.XmlWriter object that you want to use as the underlying writer.
    //
    // Returns:
    //     An System.Xml.XmlWriter object that is wrapped around the specified System.Xml.XmlWriter
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The writer value is null.
    public static XmlWriter Create(XmlWriter output)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the stream and System.Xml.XmlWriterSettings
    //     object.
    //
    // Parameters:
    //   output:
    //     The stream to which you want to write. The System.Xml.XmlWriter writes XML
    //     1.0 text syntax and appends it to the specified stream
    //
    //   settings:
    //     The System.Xml.XmlWriterSettings object used to configure the new System.Xml.XmlWriter
    //     instance. If this is null, a System.Xml.XmlWriterSettings with default settings
    //     is used.  If the System.Xml.XmlWriter is being used with the System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)
    //     method, you should use the System.Xml.Xsl.XslCompiledTransform.OutputSettings
    //     property to obtain an System.Xml.XmlWriterSettings object with the correct
    //     settings. This ensures that the created System.Xml.XmlWriter object has the
    //     correct output settings.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The stream value is null.
    public static XmlWriter Create(Stream output, XmlWriterSettings settings)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the filename and System.Xml.XmlWriterSettings
    //     object.
    //
    // Parameters:
    //   outputFileName:
    //     The file to which you want to write. The System.Xml.XmlWriter creates a file
    //     at the specified path and writes to it in XML 1.0 text syntax. The outputFileName
    //     must be a file system path.
    //
    //   settings:
    //     The System.Xml.XmlWriterSettings object used to configure the new System.Xml.XmlWriter
    //     instance. If this is null, a System.Xml.XmlWriterSettings with default settings
    //     is used.  If the System.Xml.XmlWriter is being used with the System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)
    //     method, you should use the System.Xml.Xsl.XslCompiledTransform.OutputSettings
    //     property to obtain an System.Xml.XmlWriterSettings object with the correct
    //     settings. This ensures that the created System.Xml.XmlWriter object has the
    //     correct output settings.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The url value is null.
#if !SILVERLIGHT
    public static XmlWriter Create(string outputFileName, XmlWriterSettings settings)
    {
      Contract.Requires(outputFileName != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
#endif
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the System.Text.StringBuilder
    //     and System.Xml.XmlWriterSettings objects.
    //
    // Parameters:
    //   output:
    //     The System.Text.StringBuilder to which to write to. Content written by the
    //     System.Xml.XmlWriter is appended to the System.Text.StringBuilder.
    //
    //   settings:
    //     The System.Xml.XmlWriterSettings object used to configure the new System.Xml.XmlWriter
    //     instance. If this is null, a System.Xml.XmlWriterSettings with default settings
    //     is used.  If the System.Xml.XmlWriter is being used with the System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)
    //     method, you should use the System.Xml.Xsl.XslCompiledTransform.OutputSettings
    //     property to obtain an System.Xml.XmlWriterSettings object with the correct
    //     settings. This ensures that the created System.Xml.XmlWriter object has the
    //     correct output settings.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The builder value is null.
    public static XmlWriter Create(StringBuilder output, XmlWriterSettings settings)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the System.IO.TextWriter
    //     and System.Xml.XmlWriterSettings objects.
    //
    // Parameters:
    //   output:
    //     The System.IO.TextWriter to which you want to write. The System.Xml.XmlWriter
    //     writes XML 1.0 text syntax and appends it to the specified System.IO.TextWriter.
    //
    //   settings:
    //     The System.Xml.XmlWriterSettings object used to configure the new System.Xml.XmlWriter
    //     instance. If this is null, a System.Xml.XmlWriterSettings with default settings
    //     is used.  If the System.Xml.XmlWriter is being used with the System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)
    //     method, you should use the System.Xml.Xsl.XslCompiledTransform.OutputSettings
    //     property to obtain an System.Xml.XmlWriterSettings object with the correct
    //     settings. This ensures that the created System.Xml.XmlWriter object has the
    //     correct output settings.
    //
    // Returns:
    //     An System.Xml.XmlWriter object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The text value is null.
    public static XmlWriter Create(TextWriter output, XmlWriterSettings settings)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Creates a new System.Xml.XmlWriter instance using the specified System.Xml.XmlWriter
    //     and System.Xml.XmlWriterSettings objects.
    //
    // Parameters:
    //   output:
    //     The System.Xml.XmlWriter object that you want to use as the underlying writer.
    //
    //   settings:
    //     The System.Xml.XmlWriterSettings object used to configure the new System.Xml.XmlWriter
    //     instance. If this is null, a System.Xml.XmlWriterSettings with default settings
    //     is used.  If the System.Xml.XmlWriter is being used with the System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)
    //     method, you should use the System.Xml.Xsl.XslCompiledTransform.OutputSettings
    //     property to obtain an System.Xml.XmlWriterSettings object with the correct
    //     settings. This ensures that the created System.Xml.XmlWriter object has the
    //     correct output settings.
    //
    // Returns:
    //     An System.Xml.XmlWriter object that is wrapped around the specified System.Xml.XmlWriter
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The writer value is null.
    public static XmlWriter Create(XmlWriter output, XmlWriterSettings settings)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<XmlWriter>() != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Xml.XmlWriter and optionally
    //     releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     When overridden in a derived class, flushes whatever is in the buffer to
    //     the underlying streams and also flushes the underlying stream.
    //public abstract void Flush();
    //
    // Summary:
    //     When overridden in a derived class, returns the closest prefix defined in
    //     the current namespace scope for the namespace URI.
    //
    // Parameters:
    //   ns:
    //     The namespace URI whose prefix you want to find.
    //
    // Returns:
    //     The matching prefix or null if no matching namespace URI is found in the
    //     current scope.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     ns is either null or String.Empty.
    public virtual string LookupPrefix(string ns)
    {
      Contract.Requires(!String.IsNullOrEmpty(ns));

      return default(string);
    }
    //
    // Summary:
    //     When overridden in a derived class, writes out all the attributes found at
    //     the current position in the System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The XmlReader from which to copy the attributes.
    //
    //   defattr:
    //     true to copy the default attributes from the XmlReader; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     reader is null.
    //
    //   System.Xml.XmlException:
    //     The reader is not positioned on an element, attribute or XmlDeclaration node.
    public virtual void WriteAttributes(XmlReader reader, bool defattr)
    {
      Contract.Requires(reader != null);
    }

    public void WriteAttributeString(string localName, string value)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }
    public void WriteAttributeString(string prefix, string localName, string ns, string value)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }
    //
    // Summary:
    //     When overridden in a derived class, encodes the specified binary bytes as
    //     Base64 and writes out the resulting text.
    //
    // Parameters:
    //   buffer:
    //     Byte array to encode.
    //
    //   index:
    //     The position in the buffer indicating the start of the bytes to write.
    //
    //   count:
    //     The number of bytes to write.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentException:
    //     The buffer length minus index is less than count.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.
    public virtual void WriteBase64(byte[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);
    }
    //
    // Summary:
    //     When overridden in a derived class, encodes the specified binary bytes as
    //     BinHex and writes out the resulting text.
    //
    // Parameters:
    //   buffer:
    //     Byte array to encode.
    //
    //   index:
    //     The position in the buffer indicating the start of the bytes to write.
    //
    //   count:
    //     The number of bytes to write.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentException:
    //     The buffer length minus index is less than count.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.
    //
    //   System.InvalidOperationException:
    //     The writer is closed or in error state.
    public virtual void WriteBinHex(byte[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);
    }
    //
    // Summary:
    //     When overridden in a derived class, writes out a <![CDATA[...]]> block containing
    //     the specified text.
    //
    // Parameters:
    //   text:
    //     The text to place inside the CDATA block.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The text would result in a non-well formed XML document.
    //public abstract void WriteCData(string text);
    //
    // Summary:
    //     When overridden in a derived class, forces the generation of a character
    //     entity for the specified Unicode character value.
    //
    // Parameters:
    //   ch:
    //     The Unicode character for which to generate a character entity.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The character is in the surrogate pair character range, 0xd800 - 0xdfff.
    //public abstract void WriteCharEntity(char ch);
    //
    // Summary:
    //     When overridden in a derived class, writes text one buffer at a time.
    //
    // Parameters:
    //   buffer:
    //     Character array containing the text to write.
    //
    //   index:
    //     The position in the buffer indicating the start of the text to write.
    //
    //   count:
    //     The number of characters to write.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero. -or- The buffer length minus index is less
    //     than count; the call results in surrogate pair characters being split or
    //     an invalid surrogate pair being written.
    //
    //   System.ArgumentException:
    //     The buffer parameter value is not valid.
    public virtual void WriteChars(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);

    }
    //
    // Summary:
    //     When overridden in a derived class, writes out a comment <!--...--> containing
    //     the specified text.
    //
    // Parameters:
    //   text:
    //     Text to place inside the comment.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The text would result in a non-well formed XML document.
    //public abstract void WriteComment(string text);
    //
    // Summary:
    //     When overridden in a derived class, writes the DOCTYPE declaration with the
    //     specified name and optional attributes.
    //
    // Parameters:
    //   name:
    //     The name of the DOCTYPE. This must be non-empty.
    //
    //   pubid:
    //     If non-null it also writes PUBLIC "pubid" "sysid" where pubid and sysid are
    //     replaced with the value of the given arguments.
    //
    //   sysid:
    //     If pubid is null and sysid is non-null it writes SYSTEM "sysid" where sysid
    //     is replaced with the value of this argument.
    //
    //   subset:
    //     If non-null it writes [subset] where subset is replaced with the value of
    //     this argument.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This method was called outside the prolog (after the root element).
    //
    //   System.ArgumentException:
    //     The value for name would result in invalid XML.
    //public abstract void WriteDocType(string name, string pubid, string sysid, string subset);
    //
    // Summary:
    //     When overridden in a derived class, writes an element with the specified
    //     local name and value.
    //
    // Parameters:
    //   localName:
    //     The local name of the element.
    //
    //   value:
    //     The value of the element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The localName value is null or an empty string.  -or- The parameter values
    //     are not valid.
    public void WriteElementString(string localName, string value)
    {
      Contract.Requires(!String.IsNullOrEmpty(localName));

    }
    //
    // Summary:
    //     When overridden in a derived class, writes an element with the specified
    //     local name, namespace URI, and value.
    //
    // Parameters:
    //   localName:
    //     The local name of the element.
    //
    //   ns:
    //     The namespace URI to associate with the element.
    //
    //   value:
    //     The value of the element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The localName value is null or an empty string.  -or- The parameter values
    //     are not valid.
    public void WriteElementString(string localName, string ns, string value)
    {
      Contract.Requires(!String.IsNullOrEmpty(localName));
    }
    //
    // Summary:
    //     Writes an element with the specified local name, namespace URI, and value.
    //
    // Parameters:
    //   prefix:
    //     The prefix of the element.
    //
    //   localName:
    //     The local name of the element.
    //
    //   ns:
    //     The namespace URI of the element.
    //
    //   value:
    //     The value of the element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The localName value is null or an empty string.  -or- The parameter values
    //     are not valid.
    public void WriteElementString(string prefix, string localName, string ns, string value)
    {
      Contract.Requires(!String.IsNullOrEmpty(prefix));

    }

    //
    // Summary:
    //     When overridden in a derived class, closes the previous System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)
    //     call.
    //public abstract void WriteEndAttribute();
    //
    // Summary:
    //     When overridden in a derived class, closes any open elements or attributes
    //     and puts the writer back in the Start state.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XML document is invalid.
    //public abstract void WriteEndDocument();
    //
    // Summary:
    //     When overridden in a derived class, closes one element and pops the corresponding
    //     namespace scope.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This results in an invalid XML document.
    //public abstract void WriteEndElement();
    //
    // Summary:
    //     When overridden in a derived class, writes out an entity reference as &name;.
    //
    // Parameters:
    //   name:
    //     The name of the entity reference.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is either null or String.Empty.
    public virtual void WriteEntityRef(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
    }
    //
    // Summary:
    //     When overridden in a derived class, closes one element and pops the corresponding
    //     namespace scope.
    //public abstract void WriteFullEndElement();
    //
    // Summary:
    //     When overridden in a derived class, writes out the specified name, ensuring
    //     it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
    //
    // Parameters:
    //   name:
    //     The name to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is not a valid XML name; or name is either null or String.Empty.
    public virtual void WriteName(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
    }
    //
    // Summary:
    //     When overridden in a derived class, writes out the specified name, ensuring
    //     it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
    //
    // Parameters:
    //   name:
    //     The name to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is not a valid NmToken; or name is either null or String.Empty.
    public virtual void WriteNmToken(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
    }
    //
    // Summary:
    //     When overridden in a derived class, copies everything from the reader to
    //     the writer and moves the reader to the start of the next sibling.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader to read from.
    //
    //   defattr:
    //     true to copy the default attributes from the XmlReader; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     reader is null.
    //
    //   System.ArgumentException:
    //     reader contains invalid characters.
    public virtual void WriteNode(XmlReader reader, bool defattr)
    {
      Contract.Requires(reader != null);
    }
    //
    // Summary:
    //     Copies everything from the System.Xml.XPath.XPathNavigator object to the
    //     writer. The position of the System.Xml.XPath.XPathNavigator remains unchanged.
    //
    // Parameters:
    //   navigator:
    //     The System.Xml.XPath.XPathNavigator to copy from.
    //
    //   defattr:
    //     true to copy the default attributes; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     navigator is null.
#if !SILVERLIGHT
    public virtual void WriteNode(XPathNavigator navigator, bool defattr)
    {
      Contract.Requires(navigator != null);
    }
#endif
    //
    // Summary:
    //     When overridden in a derived class, writes out a processing instruction with
    //     a space between the name and text as follows: <?name text?>.
    //
    // Parameters:
    //   name:
    //     The name of the processing instruction.
    //
    //   text:
    //     The text to include in the processing instruction.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The text would result in a non-well formed XML document.  name is either
    //     null or String.Empty.  This method is being used to create an XML declaration
    //     after System.Xml.XmlWriter.WriteStartDocument() has already been called.
    public virtual void WriteProcessingInstruction(string name, string text)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));

    }
    //
    // Summary:
    //     When overridden in a derived class, writes out the namespace-qualified name.
    //     This method looks up the prefix that is in scope for the given namespace.
    //
    // Parameters:
    //   localName:
    //     The local name to write.
    //
    //   ns:
    //     The namespace URI for the name.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     localName is either null or String.Empty.  localName is not a valid name.
    public virtual void WriteQualifiedName(string localName, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }

    //
    // Summary:
    //     When overridden in a derived class, writes raw markup manually from a string.
    //
    // Parameters:
    //   data:
    //     String containing the text to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     data is either null or String.Empty.
    public virtual void WriteRaw(string data)
    {
      Contract.Requires(!string.IsNullOrEmpty(data));
    }
    //
    // Summary:
    //     When overridden in a derived class, writes raw markup manually from a character
    //     buffer.
    //
    // Parameters:
    //   buffer:
    //     Character array containing the text to write.
    //
    //   index:
    //     The position within the buffer indicating the start of the text to write.
    //
    //   count:
    //     The number of characters to write.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero. -or- The buffer length minus index is less
    //     than count.
    public virtual void WriteRaw(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);

      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);
    }


    public void WriteStartAttribute(string localName)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }

    public void WriteStartAttribute(string localName, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));

    }

    public abstract void WriteStartAttribute(string prefix, string localName, string ns);

    // Summary:
    //     When overridden in a derived class, writes the XML declaration with the version
    //     "1.0".
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This is not the first write method called after the constructor.
    //public abstract void WriteStartDocument();

    public void WriteStartElement(string localName)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }

    public void WriteStartElement(string localName, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }

    public abstract void WriteStartElement(string prefix, string localName, string ns);

    //
    // Summary:
    //     When overridden in a derived class, writes the given text content.
    //
    // Parameters:
    //   text:
    //     The text to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The text string contains an invalid surrogate pair.
    //public abstract void WriteString(string text);
    //
    // Summary:
    //     When overridden in a derived class, generates and writes the surrogate character
    //     entity for the surrogate character pair.
    //
    // Parameters:
    //   lowChar:
    //     The low surrogate. This must be a value between 0xDC00 and 0xDFFF.
    //
    //   highChar:
    //     The high surrogate. This must be a value between 0xD800 and 0xDBFF.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid surrogate character pair was passed.
    //public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);
    //
    // Summary:
    //     Writes a System.Boolean value.
    //
    // Parameters:
    //   value:
    //     The System.Boolean value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(bool value);
    //
    // Summary:
    //     Writes a System.DateTime value.
    //
    // Parameters:
    //   value:
    //     The System.DateTime value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(DateTime value);
    //
    // Summary:
    //     Writes a System.Decimal value.
    //
    // Parameters:
    //   value:
    //     The System.Decimal value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(decimal value);
    //
    // Summary:
    //     Writes a System.Double value.
    //
    // Parameters:
    //   value:
    //     The System.Double value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(double value);
    //
    // Summary:
    //     Writes a single-precision floating-point number.
    //
    // Parameters:
    //   value:
    //     The single-precision floating-point number to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(float value);
    //
    // Summary:
    //     Writes a System.Int32 value.
    //
    // Parameters:
    //   value:
    //     The System.Int32 value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(int value);
    //
    // Summary:
    //     Writes a System.Int64 value.
    //
    // Parameters:
    //   value:
    //     The System.Int64 value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(long value);
    //
    // Summary:
    //     Writes the object value.
    //
    // Parameters:
    //   value:
    //     The object value to write. Note: With the release of the .NET Framework 3.5,
    //     this method accepts System.DateTimeOffset as a parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //
    //   System.ArgumentNullException:
    //     The value is null.
    //
    //   System.InvalidOperationException:
    //     The writer is closed or in error state.
    public virtual void WriteValue(object value)
    {
      Contract.Requires(value != null);
    }
    //
    // Summary:
    //     Writes a System.String value.
    //
    // Parameters:
    //   value:
    //     The System.String value to write.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An invalid value was specified.
    //public virtual void WriteValue(string value);
    //
    // Summary:
    //     When overridden in a derived class, writes out the given white space.
    //
    // Parameters:
    //   ws:
    //     The string of white space characters.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The string contains non-white space characters.
    //public abstract void WriteWhitespace(string ws);
  }

  [ContractClassFor(typeof(XmlWriter))]
  abstract class XmlWriterContracts : XmlWriter
  {
    public override void WriteStartAttribute(string prefix, string localName, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName) || prefix == "xmlns");
    }

    public override void WriteStartElement(string prefix, string localName, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(localName));
    }
  }
}
