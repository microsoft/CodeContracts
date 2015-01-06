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

using System.Diagnostics.Contracts;

namespace MyDataStructures
{
  public class NonNullStack<T>
  where T : class
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(arr != null);

      Contract.Invariant(nextFree >= 0);
      Contract.Invariant(nextFree <= arr.Length);

      Contract.Invariant(Contract.ForAll(0, nextFree, i => arr[i] != null));
    }

    protected T[] arr;
    private int nextFree;

    public NonNullStack(int len)
    {
      Contract.Requires(len >= 0);

      this.arr = new T[len];
      this.nextFree = 0;
    }

    public void Push(T x)
    {
      Contract.Requires(x != null);

      if (nextFree == arr.Length)
      {
        var newArr = new T[arr.Length * 2 + 1];
        for (int i = 0; i < nextFree; i++)
        {
          newArr[i] = arr[i];
        }

        arr = newArr;
      }

      this.arr[nextFree++] = x;
    }

    public T Pop()
    {
      Contract.Requires(!this.IsEmpty);
      Contract.Ensures(Contract.Result<T>() != null);

      return this.arr[--nextFree];
    }

    public bool IsEmpty
    {
      get { return this.nextFree == 0; }
    }

    public int Count
    {
      get
      {
        return this.nextFree;
      }
    }
  }
}
