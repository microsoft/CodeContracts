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
using System.Diagnostics.Contracts;

using Microsoft.Research.ClousotRegression;

using System.Collections.Generic;

internal class Test
{
    [ClousotRegressionTest]
    private static void MikesTest(string filename)
    {
        Contract.Requires(!string.IsNullOrEmpty(filename));
        var lines = System.IO.File.ReadAllLines(filename);
        var result = ParseLines(lines);
    }

    [ClousotRegressionTest]
    private static string ParseLines(string[] lines)
    {
        Contract.Requires(lines != null);
        Contract.Requires(Array.TrueForAll(lines, l => l != null));
        foreach (var line in lines)
        {
            int index = line.IndexOf('=');
            if (index >= 0)
            {
                string name = line.Substring(0, index);
                if (name == "Foo")
                {
                    string value = line.Substring(index + 1);
                    return value;
                }
            }
        }
        return null;
    }


    [ClousotRegressionTest]
    private static string CSharpColorizePre(string text)
    {
        Contract.Requires(text != null);
        var split = text.Split(new[] {"<pre>", "</pre>"}, StringSplitOptions.None);
        if (split.Length == 0)
            return text;
        Contract.Assume(Array.TrueForAll(split, s => s != null));
        var result = new StringBuilder();
        result.Append(split[0]);
        var index = 1;
        while (index < split.Length)
        {
            result.Append("<pre>");
            result.Append(CSharpColorize(split[index++]));
            result.Append("</pre>");
            if (index < split.Length)
            {
                result.Append(split[index++]);
            }
        }
        return result.ToString();
    }

    [ClousotRegressionTest]
    private static string CSharpColorize(string text)
    {
        Contract.Requires(text != null);
        Contract.Ensures(Contract.Result<string>() != null);

        var result = text;
        result = System.Text.RegularExpressions.Regex.Replace(result, "(bool|new|throw|public|interface|abstract|class|typeof|get|return|default|if|void|string|null)", "<span class=\"code\" style=\"color:Blue;\">$&</span>");
        result = System.Text.RegularExpressions.Regex.Replace(result, "Contract[a-zA-Z]*", "<span class=\"code\" style=\"color:#2B91AF;\">$&</span>");
        result = System.Text.RegularExpressions.Regex.Replace(result, "//.*", "<span class=\"code\" style=\"color:Green;\">$&</span>");
        return result;
    }
}

public static class FrancescoTest
{
    [Pure]
    [ClousotRegressionTest]
    public static T[] AssumeAllNonNull<T>(this T[] sequence) where T : class
    {
        Contract.Requires(sequence != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Array.TrueForAll(Contract.Result<T[]>(), e => e != null));
        Contract.Assume(Array.TrueForAll(sequence, e => e != null));
        return sequence;
    }

    [ClousotRegressionTest]
    public static void Test1(object[] x)
    {
        Contract.Requires(x != null);

        foreach (var e in x.AssumeAllNonNull())
        {
            Contract.Assert(e != null);
        }
    }

    [ClousotRegressionTest]
    public static void Test2(object[] x)
    {
        Contract.Requires(x != null);
        Contract.Requires(Array.TrueForAll(x, el => el != null));

        foreach (var e in x)
        {
            Contract.Assert(e != null);
        }
    }
}

public class MaFTests
{
    [Pure]
    public static void Check(string[] arg)
    {
        Contract.Requires(arg == null || Contract.ForAll(arg, p => p != null));
    }

    [ClousotRegressionTest]
    public static void Test(string[] args1, string[] args2)
    {
        Contract.Requires(args1 == null || Contract.ForAll(args1, p => p != null));
        Contract.Requires(args2 == null || Contract.ForAll(args2, p => p != null));

        Check(args1);
        Check(args2);
    }

    [ClousotRegressionTest]
    public void MafRepro(string text)
    {
        Contract.Requires(text != null);

        var lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 5)
            return;

        var firstLine = lines[0];

        Contract.Assert(firstLine != null);
    }
}