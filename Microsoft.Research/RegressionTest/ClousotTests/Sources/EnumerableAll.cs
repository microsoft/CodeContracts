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
using System.Linq;
using System.Diagnostics.Contracts;

using Microsoft.Research.ClousotRegression;

using System.Collections.Generic;

internal class Test
{
    [ClousotRegressionTest]
    private static string CSharpColorizePre(string text)
    {
        Contract.Requires(text != null);
        var split = text.Split(new[] {"<pre>", "</pre>"}, StringSplitOptions.None);
        if (split.Length == 0)
            return text;
        Contract.Assume(split.All(s => s != null));
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
    public static IEnumerable<T> AssumeAllNonNull<T>(this IEnumerable<T> sequence) where T : class
    {
        Contract.Requires(sequence != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>().All(e => e != null));
        Contract.Assume(sequence.All(e => e != null));
        return sequence;
    }

    [ClousotRegressionTest]
    public static void Test1(IEnumerable<object> x)
    {
        Contract.Requires(x != null);

        foreach (var e in x.AssumeAllNonNull())
        {
            Contract.Assert(e != null);
        }
    }

    [ClousotRegressionTest]
    public static void Test2(IEnumerable<object> x)
    {
        Contract.Requires(x != null);
        Contract.Requires(x.All(el => el != null));

        foreach (var e in x)
        {
            Contract.Assert(e != null);
        }
    }
}