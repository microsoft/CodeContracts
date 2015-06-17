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
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace CCDoc {
  /// <summary>
  /// Used for metric tracking and debug output
  /// </summary>
  [ContractVerification(true)]
  public class DocTracker {
    TextWriter writer;
    private List<MemberInfo> MembersInfo = new List<MemberInfo>();

    /// <summary>
    /// Creates a new DocTracker with the given TextWriter.
    /// </summary>
    /// <param name="writer">The TextWriter where debug output will be written to. If null, no output will be written.</param>
    public DocTracker(TextWriter writer) {
      this.writer = writer;
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(MembersInfo != null);
    }

    /// <summary>
    /// Writes a line.
    /// </summary>
    public void WriteLine(string text, params object[] args) {
      if (this.writer != null) //A null writer is allowed. This is not an error.
        this.writer.WriteLine(text, args);
    }
    /// <summary>
    /// Writes a line.
    /// </summary>
    public void WriteLine(string text) {
      if (this.writer != null) //A null writer is allowed. This is not an error.
        this.writer.WriteLine(text);
    }
    /// <summary>
    /// Add information about a member.
    /// </summary>
    public void AddMemberInfo(int contracts, MemberKind kind) {
      this.MembersInfo.Add(new MemberInfo(contracts, kind));
      if (contracts > 0)
        this.WriteLine("{0} contracts found for this {1}.", contracts, kind);
    }
    /// <summary>
    /// Writes the number of contracts for a given kind.
    /// </summary>
    /// <param name="kind"></param>
    public void WriteContractsPerKind(MemberKind kind) {
      int kindCount = 0;
      int kindWithContractsCount = 0;
      int contractCount = 0;
      foreach (var member in MembersInfo) {
        if (member.kind != kind) continue;
        kindCount++;
        if (member.contracts > 0) {
          kindWithContractsCount++;
          contractCount += member.contracts;
        }
      }
      this.WriteLine("-----");
      this.WriteLine("{0}s:", kind);
      this.WriteLine("{0} {1}s.", kindCount, kind);
      this.WriteLine("{0} have contracts.", kindWithContractsCount);
      this.WriteLine("{0} total contracts.", contractCount);
      this.WriteLine("{0} contracts on average per {1} with contracts.", ((float)contractCount / (kindWithContractsCount != 0 ? (float)kindWithContractsCount : -1)), kind);
    }
  }
  /// <summary>
  /// The kind of member.
  /// </summary>
  public enum MemberKind {
    /// <summary>
    /// A property.
    /// </summary>
    Property,
    /// <summary>
    /// A method.
    /// </summary>
    Method,
    /// <summary>
    /// A type.
    /// </summary>
    Type,
    /// <summary>
    /// A get accessor.
    /// </summary>
    Getter,
    /// <summary>
    /// A set accessor.
    /// </summary>
    Setter,
    /// <summary>
    /// An unidentified member kind.
    /// </summary>
    Undefined
  }
  /// <summary>
  /// Information about a member.
  /// </summary>
  public struct MemberInfo {
    /// <summary>
    /// Count of contracts.
    /// </summary>
    public int contracts;
    /// <summary>
    /// Kind of member.
    /// </summary>
    public MemberKind kind;
    /// <summary>
    /// Constructor for a MemberInfo.
    /// </summary>
    public MemberInfo(int contracts, MemberKind kind) {
      this.contracts = contracts;
      this.kind = kind;
    }
  }
}
