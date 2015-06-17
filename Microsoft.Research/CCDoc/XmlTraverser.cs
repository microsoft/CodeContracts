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
  /// A custom XmlTraverser that writes contracts to an XML file.
  /// </summary>
  [ContractVerification(true)]
  sealed class XmlTraverser : XmlTraverserBase {
    IDictionary<string, XContract[]> contracts;
    DocTracker docTracker;
    bool inMember = false;
    string currentMemberName;
    int memberDepth = 0;
    private Options options;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(contracts != null);
      Contract.Invariant(docTracker != null);
    }

    public XmlTraverser(IDictionary<string, XContract[]> contracts, DocTracker docTracker, Options options) {
      Contract.Requires(contracts != null);
      Contract.Requires(docTracker != null);
      this.contracts = contracts;
      this.docTracker = docTracker;
      this.options = options;
    }

    protected override void WriteElement(XmlReader reader, XmlWriter writer) {
      if (!inMember && String.Equals(reader.LocalName, "member", StringComparison.Ordinal)) {
        writer.WriteStartElement(reader.LocalName);
        currentMemberName = reader.GetAttribute("name");
        this.WriteAttributes(reader, writer);
        inMember = true;
        memberDepth = 0;
      } else {
        if (inMember) memberDepth++;
        base.WriteElement(reader, writer);
      }
    }
    protected override void WriteEndElement(XmlReader reader, XmlWriter writer) {
      if (inMember && memberDepth == 0) {
        XContract[] contractArray = null;
        if (contracts.TryGetValue(currentMemberName, out contractArray)) {
          Contract.Assume(contractArray != null);
          foreach (var contractElement in contractArray) {
            Contract.Assume(contractElement != null);
            //Write contracts to the member
            contractElement.WriteTo(writer);
            //Add exception information to the member if relevent
            if (this.options.generateExceptionTable) {
              var contractWithException = contractElement as IContractWithException;
              if (contractWithException != null)
                contractWithException.WriteExceptionTo(writer);
            }
          }
        }
        inMember = false;
      } else if (inMember) {
        memberDepth--;
      } 
      #region Write out all the contracts that haven't been written yet.
      else if (String.Equals(reader.LocalName, "members", StringComparison.Ordinal)) {
        foreach (var kv in contracts) {
          Contract.Assume(kv.Value != null);
          Contract.Assume(kv.Value.Length > 0);
          Contract.Assume(kv.Value[0] != null);
          if (kv.Value[0].wasWritten == false) {
            docTracker.WriteLine("Member does not exist. Adding...");
            docTracker.WriteLine("{0}", kv.Key);
            writer.WriteStartElement("member");
            writer.WriteAttributeString("name", kv.Key);
            foreach (var contract in kv.Value) {
              Contract.Assume(contract != null);
              contract.WriteTo(writer);
              if (this.options.generateExceptionTable) {
                var contractWithException = contract as IContractWithException;
                if (contractWithException != null)
                  contractWithException.WriteExceptionTo(writer);
              }
            }
            writer.WriteEndElement();
          }
        }
      }
      #endregion
      base.WriteEndElement(reader, writer);
    }
    protected override void WriteWhitespace(XmlReader reader, XmlWriter writer) {
      //Whitespace from the reader is skipped because it corrupts the indendation of the writer.
    }
  }
}
