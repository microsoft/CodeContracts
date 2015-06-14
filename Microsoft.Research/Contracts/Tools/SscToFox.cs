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

//
//  Very hacky code to convert Spec# interface files to Foxtrot out-of-band contracts.
//
//  Here's what the code does and doesn't do.
//
//  What works (more or less):
//   * Converts or add proper using statement
//   * Convert requires and ensures (removes otherwise clauses)
//   * Add curly begin/end
//   * Add return statement
//   * Add ensures for ! (non-null) return type
//
//  What does not work and needs to be manually done (or extended below)			
//	 * Multi line contracts (closing parenthesis is missing)
//   * Filtering non-null types (!'s) in argument types 
//       - we must be careful to add a Requires(x != null) for such parameters unless it is already present
//	 * Getters and setters don't get proper curlies and return statement for getter.
//	 * Removing Spec# specific attributes
//
//
//  Cyril: don't take this code as a good example of coding. I just hacked it up in an hour. [maf]
//
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

struct EndMethodInfo {
  public readonly int Line;
  public readonly int Offset;
  public readonly string TypeName;

  public EndMethodInfo(int line, int offset, string typeName) {
    this.Line = line;
    this.Offset = offset;
    this.TypeName = typeName;
  }
}

public static class SscToFox {

  public static void Main(string[] args) {
    string file = args[0];

    //System.Diagnostics.Debugger.Break();

    if (!File.Exists(file)) {
      Console.WriteLine("File {0} does not exist", file);
      Environment.Exit(-1);
    }

    string[] read = File.ReadAllLines(file);
    List<string> lines = new List<string>(read);

    for(int i=0; i<lines.Count; i++) 
    {
      string line = lines[i];
      RewriteLine(ref line);
      lines[i] = line;
    }

    AddUsing(lines);
    AddClosingCurlies(lines);

    File.WriteAllLines(file, lines.ToArray());
  }

  public static void AddUsing(List<string> lines) {
    // first try to find if there is Microsoft.Contracts ...
    
    // only look for usings
    for (int i = 0; i<lines.Count; i++) {
      int indexOfUsing = lines[i].IndexOf("using");
      if (indexOfUsing < 0) break;
      if (lines[i].Contains("Microsoft.Contracts;")) {
        lines[i] = lines[i].Replace("Microsoft.Contracts", "System.Diagnostics.Contracts");
        return;
      }
      if (lines[i].Contains("System.Diagnostics.Contracts")) {
        return;
      }
    }
    // insert a using
    lines.Insert(0, "using System.Diagnostics.Contracts;");
  }

  public static void AddClosingCurlies(List<string> lines) {
    // Keep track of whether we are in a method or not (having a curly opening at end)
    bool inMethod = false;
    List<EndMethodInfo> linesWhereToInsert = new List<EndMethodInfo>();
    int lastStartIndex = 0;
    string lastTypeName = null;

    for (int i = 0; i<lines.Count; i++) {
      string line = lines[i];

      int indexOfPublic = line.IndexOf("public");
      if (inMethod && (indexOfPublic >= 0 || line.Trim() == "}")) {
        linesWhereToInsert.Add(new EndMethodInfo(FindMethodHeaderPrefix(lines,i),lastStartIndex, lastTypeName));
        inMethod = false;
      }

      if (indexOfPublic >= 0) {
        // find type
        string typeName = GetTypeName(line, indexOfPublic+7);
        if (typeName == null) {
          // not a method
          continue;
        }

        // if it starts with public and ends with ) or );, remove semi at end (if present).
        int index = line.LastIndexOf(");");
        if (index > 0) {
            if (line.Contains("extern"))
                continue;
            if (line.Contains("abstract"))
              continue;
            lines[i] = line.Replace(");", ") {");
          inMethod = true;
          lastStartIndex = indexOfPublic;
          lastTypeName = typeName;
          CleanReturnType(lines, i, typeName);
        }
        else {
          index = line.LastIndexOf(")");
          if (index > 0 && line.Substring(index + 1).Trim() == "") {
            lines[i] = line.Replace(")", ") {");
            inMethod = true;
            lastStartIndex = indexOfPublic;
            lastTypeName = typeName;
            CleanReturnType(lines, i, typeName);
          }
        }
      }
    }

    // now insert the lines
    int inserted = 0;
    for (int i = 0; i<linesWhereToInsert.Count; i++) {
      int line = linesWhereToInsert[i].Line + inserted++;
      int padding = linesWhereToInsert[i].Offset;
      string paddingString = "".PadLeft(padding);

      string typeName = linesWhereToInsert[i].TypeName;
      bool addNonNullReturnEnsures = false;
      if (typeName.EndsWith("!"))
      {
        addNonNullReturnEnsures = true;
        typeName = typeName.Substring(0, typeName.Length - 1);
      }
      string toInsertLine0 = (!addNonNullReturnEnsures) ? "" : String.Format("{0}  Contract.Ensures(Contract.Result<{1}>() != null);\n", paddingString, typeName);
      string toInsertLine1 = (typeName == "void")?"" : String.Format("{0}  return default({1});\n", paddingString, typeName);
      string toInsertLine2 = paddingString + "}";
      lines.Insert(line, toInsertLine0 + toInsertLine1 + toInsertLine2);
    }
  }

  static void CleanReturnType(List<string> lines, int index, string typeName)
  {
    if (typeName.EndsWith("!"))
    {
      lines[index] = lines[index].Replace(typeName, typeName.Substring(0, typeName.Length - 1));
    }
  }
  /// <summary>
  /// line is pointing to a method header containing an access qualifier (public)
  /// return line if the method has no attributes, otherwise return first line of attributes.
  /// </summary>
  static int FindMethodHeaderPrefix(List<string> lines, int line)
  {
    int newLine = line - 1;
    while (newLine >= 0)
    {
      if (lines[newLine].Contains("["))
      {
        line = newLine;
        newLine--;
      }
      break;
    }
    return line;
  }

  public static string GetTypeName(string line, int startIndex) {
    if (startIndex >= line.Length) return null;

    int endOfTypeEstimate = line.IndexOf(" ", startIndex);
    if (endOfTypeEstimate > startIndex) {
      string typeName = line.Substring(startIndex, endOfTypeEstimate - startIndex);
      if (typeName == "static") {
        return GetTypeName(line, endOfTypeEstimate + 1);
      }
      if (typeName == "override")
      {
        return GetTypeName(line, endOfTypeEstimate + 1);
      }
      switch (typeName)
      {
      case "class":
      case "struct":
      case "interface":
        return null;
      }
      return typeName;
    }
    return null;
  }

  public static void RewriteLine(ref string line) {
    bool hasContract = false;
    int indexOfRequires = line.IndexOf("requires");
    if (indexOfRequires > 0)
    {
      hasContract = true;
      line = line.Replace("requires ", "Contract.Requires(");
    }
    else
    {
      int indexOfEnsures = line.IndexOf("ensures");
      if (indexOfEnsures > 0)
      {
        hasContract = true;
        line = line.Replace("ensures ", "Contract.Ensures(");
      }
    }
    if (hasContract)
    {
      int indexOfOtherwise = line.IndexOf(" otherwise ");
      if (indexOfOtherwise > 0)
      {
        line = line.Substring(0, indexOfOtherwise) + ");";
      }
      else
      {
        // must have a semi
        line = line.Replace(";", ");");
      }
    }

    System.Reflection.MemberFilter memberfilter;
  }
}
