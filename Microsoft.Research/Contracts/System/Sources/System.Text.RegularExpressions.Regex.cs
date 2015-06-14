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

// File System.Text.RegularExpressions.Regex.cs
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
  public partial class Regex : System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname)
    {
    }

    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname, System.Reflection.Emit.CustomAttributeBuilder[] attributes)
    {
    }

    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname, System.Reflection.Emit.CustomAttributeBuilder[] attributes, string resourceFile)
    {
    }

    public static string Escape(string str)
    {
      return default(string);
    }

    public string[] GetGroupNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public int[] GetGroupNumbers()
    {
      Contract.Ensures(Contract.Result<int[]>() != null);

      return default(int[]);
    }

    public string GroupNameFromNumber(int i)
    {
      return default(string);
    }

    public int GroupNumberFromName(string name)
    {
      return default(int);
    }

    protected void InitializeReferences()
    {
    }

    public bool IsMatch(string input)
    {
      Contract.Ensures(0 <= input.Length);

      return default(bool);
    }

    public bool IsMatch(string input, int startat)
    {
      Contract.Ensures((startat - input.Length) <= 0);
      Contract.Ensures(0 <= input.Length);

      return default(bool);
    }

    public static bool IsMatch(string input, string pattern, RegexOptions options)
    {
      Contract.Ensures(0 <= input.Length);

      return default(bool);
    }

    public static bool IsMatch(string input, string pattern)
    {
      Contract.Ensures(0 <= input.Length);

      return default(bool);
    }

    public System.Text.RegularExpressions.Match Match(string input)
    {
      Contract.Ensures(0 <= input.Length);

      return default(System.Text.RegularExpressions.Match);
    }

    public System.Text.RegularExpressions.Match Match(string input, int beginning, int length)
    {
      Contract.Ensures((length - input.Length) <= 0);
      Contract.Ensures(0 <= input.Length);

      return default(System.Text.RegularExpressions.Match);
    }

    public static System.Text.RegularExpressions.Match Match(string input, string pattern, RegexOptions options)
    {
      Contract.Ensures(0 <= input.Length);

      return default(System.Text.RegularExpressions.Match);
    }

    public System.Text.RegularExpressions.Match Match(string input, int startat)
    {
      Contract.Ensures((startat - input.Length) <= 0);
      Contract.Ensures(0 <= input.Length);

      return default(System.Text.RegularExpressions.Match);
    }

    public static Match Match(string input, string pattern)
    {
      Contract.Ensures(0 <= input.Length);

      return default(Match);
    }

    public static MatchCollection Matches(string input, string pattern, RegexOptions options)
    {
      Contract.Ensures(Contract.Result<System.Text.RegularExpressions.MatchCollection>() != null);

      return default(MatchCollection);
    }

    public MatchCollection Matches(string input)
    {
      Contract.Ensures(Contract.Result<System.Text.RegularExpressions.MatchCollection>() != null);

      return default(MatchCollection);
    }

    public static MatchCollection Matches(string input, string pattern)
    {
      Contract.Ensures(Contract.Result<System.Text.RegularExpressions.MatchCollection>() != null);

      return default(MatchCollection);
    }

    public MatchCollection Matches(string input, int startat)
    {
      Contract.Ensures(Contract.Result<System.Text.RegularExpressions.MatchCollection>() != null);

      return default(MatchCollection);
    }

    public Regex(string pattern, RegexOptions options)
    {
    }

    protected Regex(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);
    }

    public Regex(string pattern)
    {
    }

    protected Regex()
    {
    }

    public static string Replace(string input, string pattern, MatchEvaluator evaluator)
    {
      return default(string);
    }

    public string Replace(string input, string replacement, int count)
    {
      return default(string);
    }

    public string Replace(string input, string replacement, int count, int startat)
    {
      return default(string);
    }

    public string Replace(string input, string replacement)
    {
      return default(string);
    }

    public static string Replace(string input, string pattern, string replacement)
    {
      return default(string);
    }

    public static string Replace(string input, string pattern, string replacement, RegexOptions options)
    {
      return default(string);
    }

    public string Replace(string input, MatchEvaluator evaluator, int count)
    {
      return default(string);
    }

    public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
    {
      return default(string);
    }

    public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
    {
      return default(string);
    }

    public string Replace(string input, MatchEvaluator evaluator)
    {
      return default(string);
    }

    public string[] Split(string input, int count, int startat)
    {
      return default(string[]);
    }

    public string[] Split(string input)
    {
      return default(string[]);
    }

    public static string[] Split(string input, string pattern, RegexOptions options)
    {
      return default(string[]);
    }

    public static string[] Split(string input, string pattern)
    {
      return default(string[]);
    }

    public string[] Split(string input, int count)
    {
      return default(string[]);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public static string Unescape(string str)
    {
      return default(string);
    }

    protected bool UseOptionC()
    {
      Contract.Ensures(Contract.Result<bool>() == ((((this.roptions & ((System.Text.RegularExpressions.RegexOptions)(8)))) == ((System.Text.RegularExpressions.RegexOptions)(0))) == false));

      return default(bool);
    }

    protected bool UseOptionR()
    {
      Contract.Ensures(Contract.Result<bool>() == ((((this.roptions & ((System.Text.RegularExpressions.RegexOptions)(64)))) == ((System.Text.RegularExpressions.RegexOptions)(0))) == false));

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public static int CacheSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public RegexOptions Options
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.RegularExpressions.RegexOptions>() == this.roptions);

        return default(RegexOptions);
      }
    }

    public bool RightToLeft
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.roptions & ((System.Text.RegularExpressions.RegexOptions)(64)))) == ((System.Text.RegularExpressions.RegexOptions)(0))) == false));

        return default(bool);
      }
    }
    #endregion

    #region Fields
    internal protected System.Collections.Hashtable capnames;
    internal protected System.Collections.Hashtable caps;
    internal protected int capsize;
    internal protected string[] capslist;
    internal protected RegexRunnerFactory factory;
    internal protected string pattern;
    internal protected RegexOptions roptions;
    #endregion
  }
}
