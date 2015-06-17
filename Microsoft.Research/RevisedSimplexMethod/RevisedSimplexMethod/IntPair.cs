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

namespace Microsoft.Glee {
    /// <summary>
    /// Represents a couple of integers.
    /// </summary>
    [Serializable]
    public class IntPair {

        internal int x;
        internal int y;

        public int X {
            get { return x; }
            set { x = value; }
        }
        
        public int Y {
            get { return y; }
            set { y = value; }
        }
        
        public static bool operator <(IntPair a, IntPair b) {
            if (a != null && b != null)
                return a.x < b.x || a.x == b.x && a.y < b.y;

            throw new InvalidOperationException();

        }

        public static bool operator >(IntPair a, IntPair b) {
            return b < a;
        }

        public static int Compare(IntPair a, IntPair b) {
            if (a < b)
                return 1;
            if (a == b)
                return 0;

            return -1;
        }

        public override bool Equals(object obj) {
            IntPair other = obj as IntPair;
            if (other == null)
                return false;

            return x == other.x && y == other.y;
        }

        public override int GetHashCode() {
            uint hc = (uint)x.GetHashCode();
            return (int)((hc << 5 | hc >> 27) + (uint)y);
        }

        public IntPair(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override string ToString() {
            return "(" + x + "," + y + ")";
        }

    }
}
