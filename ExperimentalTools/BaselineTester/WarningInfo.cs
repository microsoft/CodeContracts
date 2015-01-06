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
using System.Linq;
using System.Text;

namespace Baseline
{
    /// <summary>
    /// provides logging of warning stats for a single repository version
    /// </summary>
    class WarningInfo
    {
        string RevisionId;
        int AssertionsChecked;
        int AssertionsCorrect;
        int AssertionsUnknown;
        int AssertionsFalse;
        int AssertionsMasked;
        int AssertionsUnreached;
        int MethodMatchFailures;
    
        // methods whose hash, but not name, have changed
        List<string> InterestingMethods;

        public WarningInfo(string revisionId) {
            this.RevisionId = revisionId;
            this.AssertionsChecked = 0;
            this.AssertionsCorrect = 0;
            this.AssertionsUnknown = 0;
            this.AssertionsFalse = 0;
            this.AssertionsMasked = 0;
            this.AssertionsUnreached = 0;
            this.InterestingMethods = new List<string>();
        }

        public void LogMethodMatchFailures(int methodMatchFailures)
        {
            this.MethodMatchFailures += methodMatchFailures;
        }

        public void LogWarningInfo(int assertionsChecked, int assertionsCorrect, int assertionsUnknown, int assertionsFalse, int assertionsMasked, int assertionsUnreached, List<string> interestingMethods) {
            this.AssertionsChecked += assertionsChecked;
            this.AssertionsCorrect += assertionsCorrect;
            this.AssertionsUnknown += assertionsUnknown;
            this.AssertionsFalse += assertionsFalse;
            this.AssertionsMasked += assertionsMasked;
            this.AssertionsUnreached += assertionsUnreached;
            this.InterestingMethods = interestingMethods;
            //Console.WriteLine(DumpToCSV());
        }

        public String DumpToCSV()
        {
            if (InterestingMethods.Count == 0) Console.WriteLine("No interesting methods found.");
            foreach (String s in InterestingMethods)
            {
                Console.WriteLine("INTERESTING METHOD: " + s);
            }
            return string.Join(",", new object[] { RevisionId, AssertionsChecked, AssertionsCorrect, AssertionsUnknown, AssertionsFalse, AssertionsUnreached, AssertionsMasked, MethodMatchFailures, InterestingMethods.Count }); 
            //return RevisionId + "," + AssertionsChecked + "," + AssertionsCorrect + "," + AssertionsUnknown + "," + AssertionsFalse + "," + AssertionsUnreached + "," + AssertionsMasked + "," + MethodMatchFailures;
        }
    }
}
