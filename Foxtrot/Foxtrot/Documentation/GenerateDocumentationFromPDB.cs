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
using System.Compiler;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

[assembly: ContractVerification(false)]

namespace Microsoft.Contracts.Foxtrot
{
    public class GenerateDocumentationFromPDB : Inspector
    {
        readonly Func<string, string> sourceFileLocator;
        readonly int tabWidth;
        readonly bool writeOutput;
        readonly ContractNodes contracts;
        private int tabStops;

        public GenerateDocumentationFromPDB(ContractNodes contracts)
          : this(contracts, null)
        {
        }

        public GenerateDocumentationFromPDB(ContractNodes contracts, Func<string, string> sourceFileLocator)
          : this(contracts, sourceFileLocator, 2, false)
        {
        }

        public GenerateDocumentationFromPDB(ContractNodes contracts, Func<string, string> sourceFileLocator, int tabWidth, bool writeOutput)
        {
            this.contracts = contracts;
            this.sourceFileLocator = sourceFileLocator;
            this.tabWidth = tabWidth;
            this.tabStops = 0;
            this.writeOutput = writeOutput;
        }

        private void Indent()
        {
            string s = "";
            s = s.PadLeft(tabStops*tabWidth);
            Console.Write(s);
        }

        // Code from BrianGru to negate predicates coming from if-then-throw preconditions

        // Recognize some common predicate forms, and negate them.  Also, fall back to a correct default.
        private static string NegatePredicate(string predicate)
        {
            if (string.IsNullOrEmpty(predicate)) return "";

            // "(p)", but avoiding stuff like "(p && q) || (!p)"
            if (predicate[0] == '(' && predicate[predicate.Length - 1] == ')')
            {
                if (predicate.IndexOf('(', 1) == -1)
                    return '(' + NegatePredicate(predicate.Substring(1, predicate.Length - 2)) + ')';
            }

            // "!p"
            if (predicate[0] == '!' &&
                (ContainsNoOperators(predicate, 1, predicate.Length - 1) ||
                 IsSimpleFunctionCall(predicate, 1, predicate.Length - 1)))
                return predicate.Substring(1);

            // "a < b" or "a <= b"
            if (predicate.Contains("<"))
            {
                int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
                int ltIndex = predicate.IndexOf('<');
                bool ltOrEquals = predicate[ltIndex + 1] == '=';
                aEnd = ltIndex;
                bStart = ltOrEquals ? ltIndex + 2 : ltIndex + 1;

                string a = predicate.Substring(aStart, aEnd - aStart);
                string b = predicate.Substring(bStart, bEnd - bStart);
                if (ContainsNoOperators(a) && ContainsNoOperators(b))
                    return a + (ltOrEquals ? ">" : ">=") + b;
            }

            // "a > b" or "a >= b"
            if (predicate.Contains(">"))
            {
                int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
                int gtIndex = predicate.IndexOf('>');
                bool gtOrEquals = predicate[gtIndex + 1] == '=';
                aEnd = gtIndex;
                bStart = gtOrEquals ? gtIndex + 2 : gtIndex + 1;

                string a = predicate.Substring(aStart, aEnd - aStart);
                string b = predicate.Substring(bStart, bEnd - bStart);
                if (ContainsNoOperators(a) && ContainsNoOperators(b))
                    return a + (gtOrEquals ? "<" : "<=") + b;
            }

            // "a == b"  or  "a != b"
            if (predicate.Contains("="))
            {
                int aStart = 0, aEnd = -1, bStart = -1, bEnd = predicate.Length;
                int eqIndex = predicate.IndexOf('=');
                bool skip = false;
                bool equalsOperator = false;
                if (predicate[eqIndex - 1] == '!')
                {
                    aEnd = eqIndex - 1;
                    bStart = eqIndex + 1;
                    equalsOperator = false;
                }
                else if (predicate[eqIndex + 1] == '=')
                {
                    aEnd = eqIndex;
                    bStart = eqIndex + 2;
                    equalsOperator = true;
                }
                else
                {
                    skip = true;
                }

                if (!skip)
                {
                    string a = predicate.Substring(aStart, aEnd - aStart);
                    string b = predicate.Substring(bStart, bEnd - bStart);
                    if (ContainsNoOperators(a) && ContainsNoOperators(b))
                    {
                        return a + (equalsOperator ? "!=" : "==") + b;
                    }
                }
            }

            if (predicate.Contains("&&") || predicate.Contains("||"))
            {
                // Consider predicates like "(P) && (Q)", "P || Q", "(P || Q) && R", etc.
                // Apply DeMorgan's law, and recurse to negate both sides of the binary operator.
                int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
                int parenCount = 0;
                bool skip = false;
                bool foundAnd = false, foundOr = false;
                aEnd = 0;

                while (aEnd < predicate.Length && ((predicate[aEnd] != '&' && predicate[aEnd] != '|') || parenCount > 0))
                {
                    if (predicate[aEnd] == '(')
                        parenCount++;
                    else if (predicate[aEnd] == ')')
                        parenCount--;
                    aEnd++;
                }

                if (aEnd >= predicate.Length - 1)
                {
                    skip = true;
                }
                else
                {
                    if (aEnd + 1 < predicate.Length && predicate[aEnd] == '&' && predicate[aEnd + 1] == '&')
                        foundAnd = true;
                    else if (aEnd + 1 < predicate.Length && predicate[aEnd] == '|' && predicate[aEnd + 1] == '|')
                        foundOr = true;
                    if (!foundAnd && !foundOr)
                        skip = true;
                }

                if (!skip)
                {
                    bStart = aEnd + 2;
                    
                    while (Char.IsWhiteSpace(predicate[aEnd - 1]))
                        aEnd--;

                    while (Char.IsWhiteSpace(predicate[bStart]))
                        bStart++;

                    string a = predicate.Substring(aStart, aEnd - aStart);
                    string b = predicate.Substring(bStart, bEnd - bStart);
                    string op = foundAnd ? " || " : " && ";

                    return NegatePredicate(a) + op + NegatePredicate(b);
                }
            }

            return string.Format("!({0})", predicate);
        }

        private static bool ContainsNoOperators(string s)
        {
            return ContainsNoOperators(s, 0, s.Length);
        }

        // These aren't operators like + per se, but ones that will cause evaluation order to possibly change,
        // or alter the semantics of what might be in a predicate.
        // @TODO: Consider adding '~'
        private static readonly string[] Operators = new string[]
        {"==", "!=", "=", "<", ">", "(", ")", "//", "/*", "*/"};

        private static bool ContainsNoOperators(string s, int start, int end)
        {
            foreach (string op in Operators)
            {
                if (s.IndexOf(op) >= 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ArrayContains<T>(T[] array, T item)
        {
            foreach (T x in array)
            {
                if (item.Equals(x))
                {
                    return true;
                }
            }

            return false;
        }

        // Recognize only SIMPLE method calls, like "System.string.Equals("", "")".
        private static bool IsSimpleFunctionCall(string s, int start, int end)
        {
            char[] badChars = {'+', '-', '*', '/', '~', '<', '=', '>', ';', '?', ':'};

            int parenCount = 0;
            int index = start;
            bool foundMethod = false;

            for (; index < end; index++)
            {
                if (s[index] == '(')
                {
                    parenCount++;
                    if (parenCount > 1)
                        return false;

                    if (foundMethod == true)
                        return false;

                    foundMethod = true;
                }
                else if (s[index] == ')')
                {
                    parenCount--;
                    if (index != end - 1)
                        return false;
                }
                else if (ArrayContains(badChars, s[index]))
                {
                    return false;
                }
            }

            return foundMethod;
        }

        /// <summary>
        /// Stupid parser that returns every character in between the first opening
        /// parenthesis and either the matching closing parenthesis or else (the first
        /// comma it finds *if* it is at the top level of parentheses).
        /// NOTE: it does not take into account embedded character strings in the text,
        /// so if those are not matched, then it will not return the correct result.
        /// </summary>
        [ContractVerification(true)]
        private static string /*?*/ Parse(string prefix, DocSlice /*?*/ text, out bool isVB)
        {
            int i;
            if (prefix == "if")
            {
                // legacy. Watch for VB If ... Then, in which case we are not seeing any parentheses
                i = SkipToPrefixWithWhiteSpace(ref text, "If", 0);
                if (i >= 0)
                {
                    isVB = true;
                    return ParseVBIfThenText(text, i + 3);
                }
            }

            isVB = false;

            // skip to first open paren
            i = SkipToOpenParen(ref text, 0);
            if (i < 0) return null;

            i++; // skip open paren
            // handle VB generics (Of
            if (i < text.Length && i + 1 < text.Length && text[i] == 'O' && text[i + 1] == 'f')
            {
                i = SkipToMatchingCloseParen(ref text, i + 2, false);
                if (i < 0) return null;

                i = SkipToOpenParen(ref text, i + 1);
                if (i < 0) return null;

                i++; // skip open paren
            }

            int indexOfFirstCharOfCondition = i;

            int closingParen = SkipToMatchingCloseParen(ref text, i, true);
            if (closingParen < indexOfFirstCharOfCondition) return null;

            return text.Substring(indexOfFirstCharOfCondition, closingParen - indexOfFirstCharOfCondition);
        }

        [ContractVerification(true)]
        private static string ParseVBIfThenText(DocSlice text, int start)
        {
            Contract.Requires(start >= 0);

            int i = start;
            while (i < text.Length)
            {
                // find space before Then
                while (i < text.Length && !Char.IsWhiteSpace(text[i])) i++;

                // skip white space (or past text)
                i++;

                // check Then
                if (HasChar('T', text, i) && HasChar('h', text, i + 1) && HasChar('e', text, i + 2) &&
                    HasChar('n', text, i + 3))
                {
                    return text.Substring(start, i - start);
                }
            }

            return null;
        }

        [ContractVerification(true)]
        private static bool HasChar(char c, DocSlice text, int i)
        {
            Contract.Requires(i >= 0);
            Contract.Ensures(i < text.Length || !Contract.Result<bool>());

            if (i >= text.Length) return false;

            return text[i] == c;
        }

        [ContractVerification(true)]
        private static int SkipToPrefixWithWhiteSpace(ref DocSlice text, string prefix, int i)
        {
            Contract.Requires(i >= 0);
            Contract.Requires(!string.IsNullOrEmpty(prefix));
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < text.Length);

            Contract.Assert(prefix != null);

            while (i < text.Length && Char.IsWhiteSpace(text[i])) i++;

            if (i >= text.Length) return -1;
            int start = i;
            int j = 0;

            while (i < text.Length && j < prefix.Length && text[i] == prefix[j])
            {
                i++;
                j++;
            }

            if (j == prefix.Length && i < text.Length && Char.IsWhiteSpace(text[i])) return start;

            return -1;
        }

        [ContractVerification(true)]
        [Pure]
        private static int SkipToOpenParen(ref DocSlice text, int i)
        {
            Contract.Requires(i >= 0);
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < text.Length);

            while (i < text.Length && text[i] != '(') i++;

            if (i < text.Length) return i;

            return -1;
        }

        /// <summary>
        /// Returns matching close parenthesis or comma if paren count == 1. -1 if no match before end of text
        /// 
        /// Problems: given generic method instantiations, there can be commas that are not the end of the
        ///           condition, but parsing expressions and determining if a open angle bracket is a generic
        ///           argument list or a greater than is non-trivial. It seems to require parsing types.
        ///           
        ///           Instead, we identify the closing paren and then work backwards from there. We assume
        ///           that the extra expression argument to Contract calls cannot contain greater and less than
        ///           comparisons or shifts so all angle brackets are part of generic instantiations.
        /// </summary>
        [ContractVerification(true)]
        [Pure]
        private static int SkipToMatchingCloseParen(ref DocSlice text, int i, bool allowCommaAsEndingParen)
        {
            Contract.Requires(i >= 0);
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < text.Length);

            bool containsComma;
            var closingParen = FindClosingParenthesis(ref text, i, out containsComma);

            if (closingParen < 0 || !allowCommaAsEndingParen || !containsComma) return closingParen;

            var commaPos = FindPreceedingComma(ref text, closingParen);
            if (commaPos >= 0) return commaPos;

            return closingParen;
        }

        [ContractVerification(true)]
        [Pure]
        private static bool IsEscape(ref DocSlice text, int i)
        {
            Contract.Requires(i < text.Length);

            if (i < 0) return false;

            if (text[i] != '\\') return false;

            // avoid escaped backslash
            if (!IsEscape(ref text, i - 1)) return true;

            return false;
        }

        /// <summary>
        /// Assume that code between i and the comma does not contain comparison operators, shift, etc.
        /// so all angle brackets are part of generic argument lists and properly matched.
        /// </summary>
        [ContractVerification(true)]
        [Pure]
        private static int FindPreceedingComma(ref DocSlice text, int i)
        {
            Contract.Requires(i >= 0);
            Contract.Requires(i < text.Length);
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() <= i);

            bool insideQuotes = false;
            bool insideDoubleQuotes = false;
            int parenCount = 0;
            int angleCount = 0;
            i--; // skip closing paren

            while (i >= 0)
            {
                Contract.Assert(i < text.Length);

                if (!insideDoubleQuotes && text[i] == '\'' && !IsEscape(ref text, i - 1))
                {
                    insideQuotes = !insideQuotes;
                }

                if (!insideQuotes && text[i] == '"' && !IsEscape(ref text, i - 1))
                {
                    insideDoubleQuotes = !insideDoubleQuotes;
                }

                if (insideDoubleQuotes || insideQuotes)
                {
                    // skip
                }
                else
                {
                    if (text[i] == ',' && angleCount == 0 && parenCount == 0) return i;
                    
                    if (text[i] == ')') parenCount++;
                    else if (text[i] == '(') parenCount--;
                    else if (text[i] == '>') angleCount++;
                    else if (text[i] == '<') angleCount--;
                }

                i--;
            }

            return -1;
        }

        [Pure]
        [ContractVerification(true)]
        private static int FindClosingParenthesis(ref DocSlice text, int i, out bool containsComma)
        {
            Contract.Requires(i >= 0);
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < text.Length);

            containsComma = false;

            if (i >= text.Length)
            {
                return -1;
            } // no closing paren

            bool wasEscape = false;
            bool insideQuotes = false;
            bool insideDoubleQuotes = false;
            int parenCount = 1;

            while (i < text.Length && 0 < parenCount)
            {
                if (!insideDoubleQuotes && !wasEscape && text[i] == '\'')
                {
                    insideQuotes = !insideQuotes;
                }

                if (!insideQuotes && !wasEscape && text[i] == '"')
                {
                    insideDoubleQuotes = !insideDoubleQuotes;
                }

                if (insideDoubleQuotes || insideQuotes)
                {
                    if (!wasEscape && text[i] == '\\')
                    {
                        wasEscape = true;
                    }
                    else
                    {
                        wasEscape = false;
                    }
                    // skip
                }
                else
                {
                    if (text[i] == '(') parenCount++;
                    else if (text[i] == ')') parenCount--;
                    else if (text[i] == ',') containsComma = true;
                }

                i++;
            }

            if (parenCount > 0) return -1;

            return i - 1;
        }

        private struct DocSlice
        {
            private SourceDocument doc;
            private int startLine;
            private int startColumn;
            private int endLine;
            private int count;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.doc != null);
                Contract.Invariant(this.startLine > 0);
                Contract.Invariant(this.startColumn >= 0);
                Contract.Invariant(this.count >= 0);
            }

            public DocSlice(SourceDocument doc, int startLine, int startColumn, int endLine)
            {
                Contract.Requires(doc != null);
                Contract.Requires(startLine > 0);

                this.doc = doc;
                this.startColumn = startColumn;
                this.startLine = startLine;
                this.endLine = endLine;
                this.count = 0;

                // compute count
                int i = startLine;

                while (i <= endLine)
                {
                    string s = doc[i];
                    if (s == null) return;

                    count += s.Length;
                    i++;
                }

                count -= startColumn;
                if (count < 0)
                {
                    // problem. indicates source/pdb are out of date w.r.t each other
                    count = 0;
                }
            }

            public int Length
            {
                get
                {
                    Contract.Ensures(Contract.Result<int>() >= 0);
                    return this.count;
                }
            }

            public char this[int column]
            {
                get
                {
                    Contract.Requires(column >= 0);
                    Contract.Requires(column < Length);

                    int skipped = 0;
                    int i = startLine;
                    column += this.startColumn; // adjust

                    while (i <= endLine)
                    {
                        string s = doc[i];

                        if (s == null)
                        {
                            throw new InvalidOperationException();
                        } // cannot happen if count was properly computed in constructor

                        if (column < skipped + s.Length) return s[column - skipped];

                        skipped += s.Length;
                        i++;
                    }

                    throw new InvalidOperationException();
                }
            }

            [ContractVerification(true)]
            internal string Substring(int startIndex, int length)
            {
                Contract.Requires(startIndex >= 0);
                Contract.Requires(length >= 0);
                Contract.Requires(startIndex + length <= Length);

                int skipped = 0;
                int i = startLine;
                StringBuilder sb = null; // lazily create
                startIndex += this.startColumn; // skip to column

                while (i <= endLine)
                {
                    string s = doc[i];
                    if (s == null)
                    {
                        throw new InvalidOperationException();
                    } // cannot happen if count was properly computed in constructor

                    if (startIndex < skipped + s.Length)
                    {
                        int lengthInS = s.Length - (startIndex - skipped);
                        if (length <= lengthInS)
                        {
                            if (sb == null)
                            {
                                return TrimmedSubstring(s, startIndex - skipped, length);
                            }

                            sb.Append(' ');
                            sb.Append(TrimmedSubstring(s, startIndex - skipped, length));
                            return sb.ToString();
                        }

                        // more than 1 string long
                        if (sb == null)
                        {
                            sb = new StringBuilder(length);
                        }
                        else
                        {
                            sb.Append(' ');
                        }

                        sb.Append(TrimmedSubstring(s, startIndex - skipped, s.Length - (startIndex - skipped)));
                        length -= lengthInS;
                        startIndex += lengthInS;
                    }

                    skipped += s.Length;
                    i++;
                }

                throw new InvalidOperationException();
            }

            [ContractVerification(true)]
            private static string TrimmedSubstring(string s, int startIndex, int length)
            {
                Contract.Requires(s != null);
                Contract.Requires(startIndex >= 0);
                Contract.Requires(length >= 0);
                Contract.Requires(startIndex + length <= s.Length);

                var index = startIndex;
                var remaining = length;

                while (remaining > 0 && char.IsWhiteSpace(s[index]))
                {
                    Contract.Assert(index + remaining <= s.Length);
                    index++;
                    remaining--;
                }

                while (remaining > 0 && char.IsWhiteSpace(s[index + remaining - 1]))
                {
                    remaining--;
                }

                return s.Substring(index, remaining);
            }
        }

        private class SourceDocument
        {
            private TextReader tr;
            private List<string> lines;

            public SourceDocument(string fileName)
            {
                if (!File.Exists(fileName))
                {
                    return; // dummy
                }
                try
                {
                    tr = File.OpenText(fileName);
                }
                catch
                {
                }
            }

            public string this[int line]
            {
                get
                {
                    // lines are indexed starting by 1, so dec
                    if (line < 1) throw new IndexOutOfRangeException();
                    Contract.EndContractBlock();

                    line--;

                    EnsureRead(line);

                    if (lines != null && lines.Count > line)
                    {
                        return lines[line];
                    }

                    return null;
                }
            }

            private void EnsureRead(int line)
            {
                if (tr == null) return;

                if (lines == null) lines = new List<string>();
                try
                {
                    while (lines.Count <= line)
                    {
                        var linestring = tr.ReadLine();
                        if (linestring == null)
                        {
                            tr = null;
                            break;
                        }

                        lines.Add(linestring);
                    }
                }
                catch
                {
                    tr = null;
                }
            }
        }

        [Pure]
        private SourceDocument GetDocument(Document doc)
        {
            if (doc == null) return null;

            string sourceName = doc.Name;
            if (sourceName == null) return null;

            SourceDocument result;
            if (!sourceTexts.TryGetValue(sourceName, out result))
            {
                var sourceFilePath = sourceName;
                if (sourceFileLocator != null)
                {
                    sourceFilePath = sourceFileLocator(sourceFilePath);
                }

                result = new SourceDocument(sourceFilePath);
                sourceTexts.Add(sourceName, result);
            }

            return result;
        }

        private Dictionary<string, SourceDocument> sourceTexts = new Dictionary<string, SourceDocument>();

        private string GetSource(string prefix, SourceContext sctx)
        {
            bool dummy;
            return GetSource(prefix, sctx, out dummy);
        }

        /// <summary>
        /// Opens the source file specified in the provided source context and retrieves
        /// the source text corresponding to the argument to the method call.
        /// </summary>
        /// <param name="prefix">Either "Contract.Requires", "Contract.Ensures", etc.</param>
        /// <param name="sctx">A valid source context, i.e., one with a document and start
        /// line and end line fields.</param>
        /// <returns></returns>
        [ContractVerification(true)]
        private string /*?*/ GetSource(string prefix, SourceContext sctx, out bool isVB, bool islegacy = false)
        {
            isVB = false;
            if (!sctx.IsValid) return null;

            if (sctx.StartLine <= 0) return null;

            SourceDocument doc = GetDocument(sctx.Document);
            if (doc == null) return null;

            DocSlice slice = new DocSlice(doc, sctx.StartLine, (islegacy) ? 0 : sctx.StartColumn, sctx.EndLine);
            var parsedText = Parse(prefix, slice, out isVB);

            return parsedText;
        }

        public override void VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return;

            if (writeOutput)
            {
                Indent();
                Console.WriteLine(typeNode.FullName);
                this.tabStops++;
            }

            base.VisitTypeNode(typeNode);
            if (writeOutput)
            {
                this.tabStops--;
            }
        }

        public override void VisitInvariant(Invariant invariant)
        {
            SourceContext sctx = invariant.SourceContext;

            string source = GetSource("Contract.Invariant", sctx);
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                invariant.SourceConditionText = new Literal(source, SystemTypes.String);
                if (writeOutput)
                {
                    Indent();
                    Console.WriteLine("{0}", source);
                }
            }
        }

        [ContractVerification(true)]
        public override void VisitMethod(Method method)
        {
            if (method == null) return;

            if (this.contracts != null && contracts.IsObjectInvariantMethod(method)) return;

            if (writeOutput)
            {
                Indent();
                Console.WriteLine(method.FullName);
                this.tabStops++;
            }

            base.VisitMethod(method);

            // inspector is not visiting validation list, so visit it here
            if (method.Contract != null)
            {
                this.VisitRequiresList(method.Contract.Validations);
            }

            if (writeOutput)
            {
                this.tabStops--;
            }
        }

        private void ExtractSourceTextFromLegacyRequires(Requires otherwise)
        {
            bool isVB;
            string source = GetSource("if", ContextForTextExtraction(otherwise), out isVB, true);
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                if (isVB)
                {
                    // negation not implemented, just return this with Not
                    otherwise.SourceConditionText = new Literal(string.Format("Not({0})", source), SystemTypes.String);
                }
                else
                {
                    string negatedCondition = NegatePredicate(source);
                    if (negatedCondition != null)
                    {
                        otherwise.SourceConditionText = new Literal(negatedCondition, SystemTypes.String);
                    }
                    else
                    {
                        otherwise.SourceConditionText = new Literal(source, SystemTypes.String);
                    }

                    if (writeOutput)
                    {
                        Indent();
                        if (negatedCondition != null)
                        {
                            Console.WriteLine("{0}", negatedCondition);
                        }
                        else
                        {
                            Console.WriteLine("<error generating documentation>");
                        }
                    }
                }
            }
        }

        public override void VisitRequiresPlain(RequiresPlain plain)
        {
            // Since we can copy contracts around now, we may already have the source text if it comes from a contract reference assembly
            if (plain.SourceConditionText != null) return;

            if (plain.IsFromValidation)
            {
                ExtractSourceTextFromLegacyRequires(plain);
            }
            else
            {
                ExtractSourceTextFromRegularRequires(plain);
            }
        }

        public override void VisitRequiresOtherwise(RequiresOtherwise otherwise)
        {
            if (otherwise.SourceConditionText != null) return;

            ExtractSourceTextFromLegacyRequires(otherwise);
        }

        private static SourceContext ContextForTextExtraction(MethodContractElement mce)
        {
            if (mce.DefSite.IsValid) return mce.DefSite;

            return mce.SourceContext;
        }

        private void ExtractSourceTextFromRegularRequires(RequiresPlain plain)
        {
            string source = GetSource("Contract.Requires", ContextForTextExtraction(plain));
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                plain.SourceConditionText = new Literal(source, SystemTypes.String);
                if (writeOutput)
                {
                    Indent();
                    Console.WriteLine("{0}", source);
                }
            }
        }

        public override void VisitEnsuresNormal(EnsuresNormal normal)
        {
            // Since we can copy contracts around now, we may already have the source text if it comes from a contract reference assembly
            if (normal.SourceConditionText != null) return;

            string source = GetSource("Contract.Ensures", ContextForTextExtraction(normal));
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                normal.SourceConditionText = new Literal(source, SystemTypes.String);
                if (writeOutput)
                {
                    Indent();
                    Console.WriteLine("{0}", source);
                }
            }
        }

        public override void VisitEnsuresExceptional(EnsuresExceptional exceptional)
        {
            // Since we can copy contracts around now, we may already have the source text if it comes from a contract reference assembly
            if (exceptional.SourceConditionText != null) return;
            string source = GetSource("Contract.Ensures", ContextForTextExtraction(exceptional));
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                exceptional.SourceConditionText = new Literal(source, SystemTypes.String);
                if (writeOutput)
                {
                    Indent();
                    Console.WriteLine("{0}", source);
                }
            }
        }

        public override void VisitStatementList(StatementList statements)
        {
            if (statements == null) return;

            for (int i = 0; i < statements.Count; i++)
            {
                var st = statements[i];
                ExpressionStatement est = st as ExpressionStatement;
                if (est != null)
                {
                    statements[i] = TransformExpressionStatement(est);
                }

                this.Visit(st);
            }
        }

        private Statement TransformExpressionStatement(ExpressionStatement statement)
        {
            Method m = HelperMethods.IsMethodCall(statement);

            if (m == null) goto done;

            if (m != contracts.AssertMethod && m != contracts.AssumeMethod
                && m != contracts.AssertWithMsgMethod && m != contracts.AssumeWithMsgMethod)
            {
                goto done;
            }

            SourceContext sctx = statement.SourceContext;
            string source = GetSource("Contract.Assert", sctx);
            if (source == null)
            {
                if (writeOutput) Console.WriteLine("<error generating documentation>");
            }
            else
            {
                return new ContractAssumeAssertStatement(statement.Expression, sctx, source);
            }

        done:
            
            this.VisitExpressionStatement(statement);
            return statement;
        }

        public void VisitForDoc(AssemblyNode assemblyNode)
        {
            this.VisitAssembly(assemblyNode);
        }
    }
}