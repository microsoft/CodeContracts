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
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Provides text manipulation methods that are used by several classes.
  public /*abstract*/ class XmlCharacterData // : XmlLinkedNode
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlCharacterData class.
    //
    // Parameters:
    //   data:
    //
    //   doc:
    //protected internal XmlCharacterData(string data, XmlDocument doc);

    // Summary:
    //     Contains the data of the node.
    //
    // Returns:
    //     The data of the node.
    //public virtual string Data { get; set; }
    //
    // Summary:
    //     Gets or sets the concatenated values of the node and all the children of
    //     the node.
    //
    // Returns:
    //     The concatenated values of the node and all the children of the node.
    //public override string InnerText { get; set; }
    //
    // Summary:
    //     Gets the length of the data, in characters.
    //
    // Returns:
    //     The length, in characters, of the string in the System.Xml.XmlCharacterData.Data
    //     property. The length may be zero; that is, CharacterData nodes can be empty.
    public virtual int Length 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    
    
    //
    // Summary:
    //     Gets or sets the value of the node.
    //
    // Returns:
    //     The value of the node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     Node is read-only.
    //public override string Value { get; set; }

    // Summary:
    //     Appends the specified string to the end of the character data of the node.
    //
    // Parameters:
    //   strData:
    //     The string to insert into the existing string.
    // F: strData can be null
    //public virtual void AppendData(string strData);
    //
    // Summary:
    //     Removes a range of characters from the node.
    //
    // Parameters:
    //   offset:
    //     The position within the string to start deleting.
    //
    //   count:
    //     The number of characters to delete.
    public virtual void DeleteData(int offset, int count)
    {
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);


    }
    
    // Summary:
    //     Inserts the specified string at the specified character offset.
    //
    // Parameters:
    //   offset:
    //     The position within the string to insert the supplied string data.
    //
    //   strData:
    //     The string data that is to be inserted into the existing string.
    public virtual void InsertData(int offset, string strData)
    {
      Contract.Requires(offset >= 0);

    }
    

    // Summary:
    //     Replaces the specified number of characters starting at the specified offset
    //     with the specified string.
    //
    // Parameters:
    //   offset:
    //     The position within the string to start replacing.
    //
    //   count:
    //     The number of characters to replace.
    //
    //   strData:
    //     The new data that replaces the old string data.
    public virtual void ReplaceData(int offset, int count, string strData)
    {
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
    }
    
    
    //
    // Summary:
    //     Retrieves a substring of the full string from the specified range.
    //
    // Parameters:
    //   offset:
    //     The position within the string to start retrieving. An offset of zero indicates
    //     the starting point is at the start of the data.
    //
    //   count:
    //     The number of characters to retrieve.
    //
    // Returns:
    //     The substring corresponding to the specified range.
    public virtual string Substring(int offset, int count)
    {
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(string);
    }
  }
}

#endif