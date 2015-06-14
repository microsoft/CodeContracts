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
using System.Diagnostics.Contracts;

namespace System.Text.RegularExpressions
{

  public delegate string MatchEvaluator(Match match);

  public enum RegexOptions
  {
#if !SILVERLIGHT
    Compiled = 8,
#endif
    CultureInvariant = 0x200,
    ECMAScript = 0x100,
    ExplicitCapture = 4,
    IgnoreCase = 1,
    IgnorePatternWhitespace = 0x20,
    Multiline = 2,
    None = 0,
    RightToLeft = 0x40,
    Singleline = 0x10
  }

 

 

  public class Regex
  {

    extern public RegexOptions Options
    {
      get;
    }

    extern public bool RightToLeft
    {
      get;
    }

#if false
    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname, System.Reflection.Emit.CustomAttributeBuilder[] attributes, string resourceFile)
    {

    }
    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname, System.Reflection.Emit.CustomAttributeBuilder[] attributes)
    {

    }
    public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, System.Reflection.AssemblyName assemblyname)
    {

    }
#endif

    public String[] Split(string input, int count, int startat)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<String[]>() != null);

      return default(String[]);
    }
    public String[] Split(string input, int count)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<String[]>() != null);

      return default(String[]);
    }
    public String[] Split(string input)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<String[]>() != null);

      return default(String[]);
    }
    public static String[] Split(string input, string pattern, RegexOptions options)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Ensures(Contract.Result<String[]>() != null);

      return default(String[]);
    }
    public static String[] Split(string input, string pattern)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Ensures(Contract.Result<String[]>() != null);

      return default(String[]);
    }
    public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
    {
      Contract.Requires(input != null);
      Contract.Requires(evaluator != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public string Replace(string input, MatchEvaluator evaluator, int count)
    {
      Contract.Requires(input != null);
      Contract.Requires(evaluator != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public string Replace(string input, MatchEvaluator evaluator)
    {
      Contract.Requires(input != null);
      Contract.Requires(evaluator != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Requires(evaluator != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public static string Replace(string input, string pattern, MatchEvaluator evaluator)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Requires(evaluator != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public string Replace(string input, string replacement, int count, int startat)
    {
      Contract.Requires(input != null);
      Contract.Requires(replacement != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public string Replace(string input, string replacement, int count)
    {
      Contract.Requires(input != null);
      Contract.Requires(replacement != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public string Replace(string input, string replacement)
    {
      Contract.Requires(input != null);
      Contract.Requires(replacement != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public static string Replace(string input, string pattern, string replacement, RegexOptions options)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Requires(replacement != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public static string Replace(string input, string pattern, string replacement)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Requires(replacement != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public MatchCollection Matches(string input, int startat)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<MatchCollection>() != null);

      return default(MatchCollection);
    }
    public MatchCollection Matches(string input)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<MatchCollection>() != null);

      return default(MatchCollection);
    }
    public static MatchCollection Matches(string input, string pattern, RegexOptions options)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Ensures(Contract.Result<MatchCollection>() != null);

      return default(MatchCollection);
    }
    public static MatchCollection Matches(string input, string pattern)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Ensures(Contract.Result<MatchCollection>() != null);

      return default(MatchCollection);
    }
    public Match Match(string input, int beginning, int length)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<Match>() != null);
      return default(Match);
    }
    public Match Match(string input, int startat)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<Match>() != null);

      return default(Match);
    }
    [Pure]
    public Match Match(string input)
    {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.Result<Match>() != null);

      return default(Match);
    }
    [Pure]
    public static Match Match(string input, string pattern, RegexOptions options)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);
      Contract.Ensures(Contract.Result<Match>() != null);
      return default(Match);
    }
    [Pure]
    public static Match Match(string input, string pattern)
    {
      Contract.Requires(input != null);
      Contract.Requires(pattern != null);

      Contract.Ensures(Contract.Result<Match>() != null);
      return default(Match);
    }
    [Pure]
    public bool IsMatch(string input, int startat)
    {
      Contract.Requires(input != null);
      Contract.Requires(startat >= 0);
      
      return default(bool);
    }
    [Pure]
    public bool IsMatch(string input)
    {
      Contract.Requires(input != null);

      return default(bool);
    }
    [Pure]
    public static bool IsMatch(string input, string pattern, RegexOptions options)
    {
      return default(bool);
    }
    [Pure]
    public static bool IsMatch(string input, string pattern)
    {
      return default(bool);
    }
    [Pure]
    public int GroupNumberFromName(string name)
    {
      Contract.Requires(name != null);

      return default(int);
    }
    [Pure]
    public string GroupNameFromNumber(int i)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    [Pure]
    public Int32[] GetGroupNumbers()
    {
      Contract.Ensures(Contract.Result<int[]>() != null);

      return default(Int32[]);
    }

    [Pure]
    public String[] GetGroupNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(String[]);
    }

    [Pure]
    public static string Unescape(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    [Pure]
    public static string Escape(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    public Regex(string pattern, RegexOptions options)
    {
      Contract.Requires(pattern != null);
    }
    public Regex(string pattern)
    {
      Contract.Requires(pattern != null);
    }
  }
}
