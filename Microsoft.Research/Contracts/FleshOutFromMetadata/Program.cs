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
using System.Text;
using System.Collections.Generic;
using System.IO;

interface PartialParser
{
}

class CodeFile
{
  List<Using> usings;
  Namespace namesp;
}

class Using
{
string value;
}

class Namespace
{
  string name;
  TypeElement type;
}

abstract class TypeElement
{
  string name;
  List<TypeMember> members;
}

class Class : TypeElement
{
}

class Struct : TypeElement
{
}

abstract class TypeMember
{
}

class Method : TypeMember
{
}

class Property : TypeMember
{
  Getter getter;
  Setter setter;
}

class Getter
{
}
class Setter
{
}




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

public class MetadataToFox {

  public static void Main(string[] args) {

    string file = args[0];

    //System.Diagnostics.Debugger.Break();

    if (!File.Exists(file)) {
      Console.WriteLine("File {0} does not exist", file);
      Environment.Exit(-1);
    }

    var transformer = new MetadataToFox(file);

    transformer.Transform();
  }

  string fileName;

  public MetadataToFox(string file) {
    this.fileName = file;
  }

  public void Transform()
  {
    string[] readLines = File.ReadAllLines(fileName);
    for (int i = 0; i < readLines.Length; i++) 
    {
      RewriteLine(ref readLines[i]);
    }

    List<string> lines = new List<string>(readLines);
    PostProcess(lines);

    File.WriteAllLines(fileName, lines.ToArray());
  }

  enum State { Start, InType, Method, Property };

  public static void PostProcess(List<string> lines) {
    State state = State.Start;

    List<EndMethodInfo> linesWhereToInsert = new List<EndMethodInfo>();
    int lastStartIndex = 0;
    string lastTypeName = null;
    bool isStruct = false;

    for (int i = 0; i<lines.Count; i++) {
      string line = lines[i];

      switch (state)
      {
        case State.Start:
          if (line.Contains("struct")) isStruct = true;
          state = State.InType;
          break;

        case State.InType:
          if (line.Contains("public"))
          {
            if (line.Contains("("))
            {
              state = State.Method;
            }
            else
            {
              state = State.Property;
            }
          }
          break;

        case State.Method:
          break;
        case State.Property:
          break;

        default:
          throw new NotImplementedException();
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

  /// <summary>
  /// public property or method to be made extern, unless it already is abstract/extern
  /// </summary>
  /// <param name="line"></param>
  public static void RewriteLine(ref string line) {
    line = line.Replace("[Serializable]", "");
    line = line.Replace("[ComVisible(true)]", "");
    line = line.Replace("[ComVisible(false)]", "");
    if (!line.Contains("public")) return;
    if (line.Contains("abstract")) return;
    if (line.Contains("extern")) return;
    if (line.Contains(")") && line.Contains(";"))
    {
      line = line.Replace("public", "extern public");
    }
    if (line.Contains("get;") && (line.Contains("set;") || !line.Contains("set")))
    {
      line = line.Replace("public", "extern public");
    }
  }
}
