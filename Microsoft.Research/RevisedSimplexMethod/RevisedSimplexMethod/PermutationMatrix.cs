// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    public class PermutationMatrix : Matrix
    {
        private int[] _permutation;
        private int[] _reverse;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal int[] Reverse
        {
            get { return _reverse; }
            set { _reverse = value; }
        }
        private List<int> _cycleRoots;
        internal int[] Permutation
        {
            get { return _permutation; }
            set { _permutation = value; }
        }

        public PermutationMatrix(int[] perm)
        {
            Contract.Requires(perm != null);

            Contract.Assume(Contract.ForAll(perm, index => index >= 0));
            Contract.Assume(Contract.ForAll(perm, index => index < perm.Length));

            this.Permutation = perm;
            _reverse = new int[perm.Length];
            for (int i = 0; i < perm.Length; i++)
            {
                var index = perm[i];
                _reverse[index] = i;
            }
            CreateCycleRoots();
        }



        private void CreateCycleRoots()
        {
            int n = _permutation.Length;
            bool[] visited = new bool[n];
            _cycleRoots = new List<int>();

            for (int i = 0; i < n; i++)
                if (visited[i] == false)
                {
                    _cycleRoots.Add(i);
                    FindTheCycle(i, visited);
                }
        }

        private void FindTheCycle(int i, bool[] visited)
        {
            visited[i] = true;

            for (int j = _permutation[i]; j != i; j = _permutation[j])
            {
                Contract.Assume(j >= 0);
                Contract.Assume(j < visited.Length);
                visited[j] = true;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfRows
        {
            get { return Permutation.Length; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
        public override int NumberOfColumns
        {
            get { return Permutation.Length; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }


        static public double[] operator /(double[] a, PermutationMatrix p)
        {
            Contract.Requires(a != null);
            Contract.Requires(p != null);
            foreach (int root in p._cycleRoots)
                PermuteCycle(a, root, p.Reverse);
            return a;
        }

        private static void PermuteCycle(double[] a, int root, int[] perm)
        {
            double t = a[root];
            int i = root;
            for (int j = perm[root]; j != root; j = perm[j])
            {
                a[i] = a[j];
                i = j;
            }
            a[i] = t;
        }


        public override double this[int i, int j]
        {
            get
            {
                int k = Permutation[i];
                if (i == k)
                    return 1;
                return 0;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
