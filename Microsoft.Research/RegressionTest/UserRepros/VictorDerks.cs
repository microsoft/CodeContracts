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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;
using System.Text;
using Microsoft.Research.ClousotRegression;

// This test has a small timeout because we want to test that we do not regress on performance

namespace VictorDefis
{

  public class LongRunRepro
  {
    private static readonly LongRunRepro defaultGroupLength = new LongRunRepro(0);
    private readonly uint value;
    private readonly string creatorId;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=7,MethodILOffset=0)]
    public LongRunRepro(ushort groupNumber, ushort elementNumber)
      : this(((uint)(groupNumber << 16)) | elementNumber)
    {
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=15,MethodILOffset=0)]
    public LongRunRepro(uint value)
    {
      this.value = value;
      creatorId = null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=29,MethodILOffset=0)]
    public LongRunRepro(uint value, string creatorId)
    {
      Contract.Requires(!string.IsNullOrEmpty(creatorId));

      this.value = value;
      this.creatorId = creatorId;
    }

    public ushort GroupNumber { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return (ushort)(value >> 16); } }

    public ushort ElementNumber 
    { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)] 
      get { return (ushort)(value & 0xFFFF); } 
    }
    
    public bool IsPrivate 
    { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return (value & 0x10000) != 0; } 
    }

    public bool IsGroupLength { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return ElementNumber == 0; } }

    public bool IsDataElement { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=10,MethodILOffset=0)]
      get { return GroupNumber > 7 && GroupNumber < 0xFFFF; } }

    public bool IsItemElement { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
get { return GroupNumber == 0xFFFE; } }

    public bool IsBlockId { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=11,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=19,MethodILOffset=0)]
      get { return IsPrivate && 0x0010 <= ElementNumber && ElementNumber <= 0x00FF; } }

    public bool HasCreatorId 
    { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return creatorId != null; } 
    }

    public string CreatorId{ 
      [ClousotRegressionTest]      
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return creatorId ?? string.Empty; } }


    internal bool IsFileMetaInfoElement{
      [ClousotRegressionTest] 
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      get { return GroupNumber == 0x0002; }}
    internal LongRunRepro BlockIdTag
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=12,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=18,MethodILOffset=0)]
      get
      {
        Contract.Requires(IsPrivate);
        return new LongRunRepro(GroupNumber, (ushort)(ElementNumber >> 8));
      }
    }
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'key'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    private static bool InRange(LongRunRepro key, ushort start)
    {
      return (key.GroupNumber >= start) && (key.GroupNumber <= start + 0xFF);
    }

    //This analysis took too much time because the heap analysis generates plenty of renamings, and Karr generates a huge environment (> 2000 variables)
    internal LongRunRepro NormalizedValue
    {    
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=15,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=43,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=73,MethodILOffset=0)]
      get
      {
        if (IsGroupLength)
          return defaultGroupLength;

        if (IsPrivate)
          return this;

        const ushort CurveGroupStart = 0x5000;
        if (InRange(this, CurveGroupStart))
          return new LongRunRepro(CurveGroupStart, ElementNumber);

        const ushort OverlayGroupStart = 0x6000;
        if (InRange(this, OverlayGroupStart))
          return new LongRunRepro(OverlayGroupStart, ElementNumber);

        return this;
      }
    }
  }
}
