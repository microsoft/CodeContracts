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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CloudotControlCenter
{
  internal class CloudotLogFileView
  {
    public string Assembly { get; private set; }
    public DateTime When { get; private set; }
    public string CommandLine { get; set; }

    private CloudotLogFileView(string what, string when)
    {
      this.Assembly = what;
      this.When = DateTime.Parse(when);
    }

    public static List<CloudotLogFileView> GetFromXML(string fileName = @"E:\tmp\CloudotLog.xml")
    {
      var result = new List<CloudotLogFileView>();

      try
      {
        var data = XElement.Load(fileName);

        foreach (var e in data.Elements())
        {
          var cmdLine = new List<string>();
          foreach (var attr in e.Elements())
          {
            cmdLine.Add(attr.Value);
          }

          result.Add(new CloudotLogFileView(e.Attribute("What").Value, e.Attribute("When").Value)
          {
            CommandLine = CloudotLogFileView.CmdLine2String(cmdLine)
          });
        }
      }
      catch (Exception)
      {
        result = null;
      }
      return result;
    }

    public static string CmdLine2String(IEnumerable<string> cmd)
    {
      Contract.Requires(cmd != null);

      var result = new StringBuilder();
      foreach (var str in cmd)
      {
        result.AppendFormat("\"{0}\" ", str);
      }

      return result.ToString();
    }

    public static string[] String2CmdLine(string cmd)
    {
      Contract.Requires(cmd != null);

      var result = new List<string>();

      var inQuote = false;
      var currWord = new StringBuilder();
      for (var i = 0; i < cmd.Length; i++)
      {
        var currChar = cmd[i];
        if (currChar == '"')
        {
          // Do we start a new word?
          if (!inQuote)
          {
            inQuote = true;
            currWord = new StringBuilder();
          }
          // We finish a word
          else
          {
            inQuote = false;
            result.Add(currWord.ToString());
            currWord.Clear();
          }
        }
        // Eat spaces
        else if (Char.IsWhiteSpace(currChar))
        {
          // If in a quote just add it
          if (inQuote)
          {
            currWord.Append(currChar);
          }
          // otherwise we are done with the current word
          else
          {
            if (currWord.Length != 0)
            {
              result.Add(currWord.ToString());
              currWord.Clear();
            }
          }
        }
        else
        {
          currWord.Append(currChar);
        }
      }
      // Add everything that is left
      if (currWord.Length != 0)
      {
        result.Add(currWord.ToString());
        currWord.Clear();
      }

      return result.ToArray();
    }

  }
}
