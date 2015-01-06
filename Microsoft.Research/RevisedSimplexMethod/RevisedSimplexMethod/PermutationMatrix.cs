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
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
  public class PermutationMatrix : Matrix
  {

    int[] permutation;
    int[] reverse;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal int[] Reverse
    {
      get { return reverse; }
      set { reverse = value; }
    }
    List<int> cycleRoots;
    internal int[] Permutation
    {
      get { return permutation; }
      set { permutation = value; }
    }

    public PermutationMatrix(int[] perm)
    {
      Contract.Requires(perm != null);

      Contract.Assume(Contract.ForAll(perm, index => index >= 0));
      Contract.Assume(Contract.ForAll(perm, index => index < perm.Length));

      this.Permutation = perm;
      reverse = new int[perm.Length];
      for (int i = 0; i < perm.Length; i++)
      {
        var index = perm[i];
        reverse[index] = i;
      }
      CreateCycleRoots();
    }



    private void CreateCycleRoots()
    {
      int n = this.permutation.Length;
      bool[] visited = new bool[n];
      cycleRoots = new List<int>();

      for (int i = 0; i < n; i++)
        if (visited[i] == false)
        {
          cycleRoots.Add(i);
          FindTheCycle(i, visited);
        }

    }

    private void FindTheCycle(int i, bool[] visited)
    {
      visited[i] = true;

      for (int j = permutation[i]; j != i; j = permutation[j])
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
      foreach (int root in p.cycleRoots)
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
