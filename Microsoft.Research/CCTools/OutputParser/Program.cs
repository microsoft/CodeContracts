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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace OutputParser
{
  class Program
  {
    enum Action { ExtractStats, Help }

    struct Parse
    {
      public Action action;
      public string input;
    }

    static void Main(string[] args)
    {
      Parse result;
      if (!TryParse(args, out result))
      {
        Console.WriteLine("Usage: OutputParser -<action> filename");
        Console.WriteLine("             action in {stats}");
        Environment.Exit(-1) ;
      }

      var filtered = Exec(result);
      if (filtered != null)
      {
        Array.Sort(filtered);
        Console.WriteLine("name, asserts, validated, top, bottom, masked");
        foreach (var line in filtered)
        {
          Console.WriteLine(line);
        }
      }
    }

    private static string[] Exec(Parse result)
    {
      switch (result.action)
      {
        case Action.ExtractStats:
          {
            return ParseResults(ReadAndKeepLine(result.input, line => line.Contains("CodeContracts: Checked")));
          }

        case Action.Help:
          {
            Console.WriteLine("-stats ");
          }
          break;
      }
      return null;
    }

    private static string[] ReadAndKeepLine(string filename, Predicate<string> lineCondition)
    {
      Contract.Requires(lineCondition != null);

      if (!File.Exists(filename))
      {
        Console.Error.WriteLine("File {0} does not exists", filename);
        Environment.Exit(-1);
      }

      var result = new List<string>();
      foreach (var line in File.ReadAllLines(filename))
      {
        if (lineCondition(line))
        {
          result.Add(line);
        }
      }

      return result.ToArray();
    }

    private static string[] ParseResults(string[] lines)
    {
      var result = new List<string>();

      if (lines == null || lines.Length == 0)
      {
        return result.ToArray();
      }

      for (var i = 0; i < lines.Length; i++)
      {
        Contract.Assume(lines[i] != null);
        var line = lines[i].Split(' ');

        string name = null;
        int asserts = 0, validated = 0, top = 0, bottom = 0, masked = 0;
        var pos = 0;
        for (; pos < line.Length; pos++)
        {
          if (line[pos].Contains(".dll") || line[pos].Contains(".exe"))
          {
            name = RemovePath(line[pos]);
            pos++;
            break;
          }
        }

        for (; pos < line.Length; pos++)
        {
          Contract.Assume(line[pos] != null);
          int v;
          if (Int32.TryParse(RemoveBracket(line[pos]), out v))
          {
            pos++;
            Contract.Assume(pos < line.Length);
            switch (line[pos])
            {
              case "assertions:":
                asserts = v;
                break;
              case "correct":
                validated = v;
                break;
              case "unknown":
                top = v;
                break;
              case "unreached":
                bottom = v;
                break;

              case "masked)":
                masked = v;
                break;

              default:
                break;
            }
          }
        }

        Contract.Assume(name != null);
        var normalizedName = Normalize(name);
        var str = string.Format("{0}, {1}, {2}, {3}, {4}, {5}", normalizedName, asserts, validated, top, bottom, masked);
        result.Add(str);

      }


      return result.ToArray();
    }

    /// <summary>
    /// Remove dots, keep only uppercase letters, except change Tf to TF
    /// </summary>
    private static string Normalize(string name)
    {
      var ext = Path.GetExtension(name);
      name = name.Replace(".", "");
      name = name.Replace("Tf", "TF");
      var chars = name.Where(Char.IsUpper);
      return String.Concat(chars) + ext;
    }

    static private string RemovePath(string s)
    {
      if (string.IsNullOrEmpty(s))
        return s;

      var start = s.LastIndexOf('\\')+1;
      var end = s.IndexOf('(');
      Contract.Assume(start < end);
      return s.Substring(start, end - start);
    }

    static private string RemoveBracket(string s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<string>() != null);


      if (s.Length > 0 && s[0] == '(')
        return s.Substring(1);

      return s;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    private static bool TryParse(string[] args, out Parse result)
    {
      result = new Parse();


      if (args.Length == 0 || args.Length > 2 || args[0] == null || args[0].Length <= 1)
      {
        return false;
      }

      var action = args[0].Substring(1).ToLower();

      if (action == "help")
      {
        result.action = Action.Help;
        return true;
        
      }
      else if (args.Length != 2)
      {
        return false;
      }

      result.input = args[1];

      switch (action)
      {
        case "validations":
        case "stats":
          {
            result.action = Action.ExtractStats;
            return true;
          }
      }

      return false;
    }
  }
}
