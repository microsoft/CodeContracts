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

// File System.Text.RegularExpressions.RegexRunner.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Text.RegularExpressions
{
  abstract public partial class RegexRunner
  {
    #region Methods and constructors
    protected void Capture(int capnum, int start, int end)
    {
      Contract.Ensures((this.runcrawlpos - this.runcrawl.Length) <= 0);
      Contract.Ensures(0 <= this.runcrawlpos);
      Contract.Ensures(1 <= this.runcrawl.Length);
      Contract.Ensures(this.runcrawl != null);
      Contract.Ensures(this.runcrawlpos <= 2147483646);
      Contract.Ensures(this.runmatch != null);
    }

    protected static bool CharInClass(char ch, string charClass)
    {
      Contract.Requires(charClass != null);

      return default(bool);
    }

    protected static bool CharInSet(char ch, string set, string category)
    {
      Contract.Requires(category != null);
      Contract.Requires(set != null);
      Contract.Ensures(0 <= category.Length);
      Contract.Ensures(0 <= set.Length);
      Contract.Ensures(category.Length <= 65535);
      Contract.Ensures(set.Length <= 65537);

      return default(bool);
    }

    protected void Crawl(int i)
    {
      Contract.Ensures((this.runcrawlpos - this.runcrawl.Length) <= 0);
      Contract.Ensures(0 <= this.runcrawlpos);
      Contract.Ensures(1 <= this.runcrawl.Length);
      Contract.Ensures(this.runcrawl != null);
      Contract.Ensures(this.runcrawlpos <= 2147483646);
    }

    protected int Crawlpos()
    {
      Contract.Requires(this.runcrawl != null);
      Contract.Ensures((this.runcrawl.Length - Contract.Result<int>() - this.runcrawlpos) == 0);
      Contract.Ensures(-2147483647 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() == ((int)(this.runcrawl.Length) - this.runcrawlpos));
      Contract.Ensures(this.runcrawl != null);

      return default(int);
    }

    protected void DoubleCrawl()
    {
      Contract.Requires(this.runcrawl != null);
      Contract.Ensures((Contract.OldValue(this.runcrawl.Length) - this.runcrawl.Length) <= 0);
      Contract.Ensures((Contract.OldValue(this.runcrawl.Length) + Contract.OldValue(this.runcrawlpos) - this.runcrawlpos) == 0);
      Contract.Ensures((Contract.OldValue(this.runcrawlpos) - this.runcrawlpos) <= 0);
      Contract.Ensures((this.runcrawl.Length + 2 * Contract.OldValue(this.runcrawlpos) + -2 * this.runcrawlpos) == 0);
      Contract.Ensures(this.runcrawl != null);
    }

    protected void DoubleStack()
    {
      Contract.Requires(this.runstack != null);
      Contract.Ensures((Contract.OldValue(this.runstack.Length) - this.runstack.Length) <= 0);
      Contract.Ensures((Contract.OldValue(this.runstack.Length) + Contract.OldValue(this.runstackpos) - this.runstackpos) == 0);
      Contract.Ensures((Contract.OldValue(this.runstackpos) - this.runstackpos) <= 0);
      Contract.Ensures((this.runstack.Length + 2 * Contract.OldValue(this.runstackpos) + -2 * this.runstackpos) == 0);
      Contract.Ensures(this.runstack != null);
    }

    protected void DoubleTrack()
    {
      Contract.Requires(this.runtrack != null);
      Contract.Ensures((Contract.OldValue(this.runtrack.Length) - this.runtrack.Length) <= 0);
      Contract.Ensures((Contract.OldValue(this.runtrack.Length) + Contract.OldValue(this.runtrackpos) - this.runtrackpos) == 0);
      Contract.Ensures((Contract.OldValue(this.runtrackpos) - this.runtrackpos) <= 0);
      Contract.Ensures((this.runtrack.Length + 2 * Contract.OldValue(this.runtrackpos) + -2 * this.runtrackpos) == 0);
      Contract.Ensures(this.runtrack != null);
    }

    protected void EnsureStorage()
    {
    }

    protected abstract bool FindFirstChar();

    protected abstract void Go();

    protected abstract void InitTrackCount();

    protected bool IsBoundary(int index, int startpos, int endpos)
    {
      return default(bool);
    }

    protected bool IsECMABoundary(int index, int startpos, int endpos)
    {
      return default(bool);
    }

    protected bool IsMatched(int cap)
    {
      Contract.Requires(this.runmatch != null);
      Contract.Ensures(this.runmatch != null);

      return default(bool);
    }

    protected int MatchIndex(int cap)
    {
      Contract.Requires(this.runmatch != null);
      Contract.Ensures(this.runmatch != null);

      return default(int);
    }

    protected int MatchLength(int cap)
    {
      Contract.Requires(this.runmatch != null);
      Contract.Ensures(this.runmatch != null);

      return default(int);
    }

    protected int Popcrawl()
    {
      Contract.Requires(this.runcrawl != null);
      Contract.Ensures((Contract.OldValue(this.runcrawlpos) - this.runcrawl.Length) < 0);
      Contract.Ensures((this.runcrawlpos - Contract.OldValue(this.runcrawlpos)) == 1);
      Contract.Ensures(-2147483647 <= this.runcrawlpos);
      Contract.Ensures(this.runcrawl != null);

      return default(int);
    }

    protected internal RegexRunner()
    {
    }

    protected internal Match Scan(Regex regex, string text, int textbeg, int textend, int textstart, int prevlen, bool quick)
    {
      Contract.Requires(regex != null);

      return default(Match);
    }

    protected void TransferCapture(int capnum, int uncapnum, int start, int end)
    {
      Contract.Requires(this.runmatch != null);
      Contract.Ensures((this.runcrawlpos - this.runcrawl.Length) <= 0);
      Contract.Ensures(0 <= this.runcrawlpos);
      Contract.Ensures(1 <= this.runcrawl.Length);
      Contract.Ensures(this.runcrawl != null);
      Contract.Ensures(this.runcrawlpos <= 2147483646);
      Contract.Ensures(this.runmatch != null);
    }

    protected void Uncapture()
    {
      Contract.Requires(this.runcrawl != null);
      Contract.Ensures((Contract.OldValue(this.runcrawlpos) - this.runcrawl.Length) < 0);
      Contract.Ensures((Contract.OldValue(this.runcrawlpos) - this.runcrawlpos) == -(1));
      Contract.Ensures(-2147483647 <= this.runcrawlpos);
      Contract.Ensures(this.runcrawl != null);
      Contract.Ensures(this.runmatch != null);
    }
    #endregion

    #region Fields
    internal protected int[] runcrawl;
    internal protected int runcrawlpos;
    internal protected Match runmatch;
    internal protected Regex runregex;
    internal protected int[] runstack;
    internal protected int runstackpos;
    internal protected string runtext;
    internal protected int runtextbeg;
    internal protected int runtextend;
    internal protected int runtextpos;
    internal protected int runtextstart;
    internal protected int[] runtrack;
    internal protected int runtrackcount;
    internal protected int runtrackpos;
    #endregion
  }
}
