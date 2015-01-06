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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Tests.Sources
{

  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        var users = new List<User>()
          {
            new User() { Name = "One"},
            new User() { Name = "Two" }
          };
        UpdateUserList(users);

      }
      else
      {
        UpdateUserList(null);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Precondition failed: list != null";


    static void UpdateUserList(ICollection<User> list)
    {
      Contract.Requires<ArgumentNullException>(list != null);

      using (var novy = new Seznam())
        {
          var smazani = (from u in list
                         where novy.All(n => n.Name != u.Name)
                         select u).ToArray();
          foreach (User u in smazani)
            list.Remove(u);

          Contract.Assert(novy.Count == list.Count);
        }
    }
  }

  class User
  {
    public string Name { get; set; }
  }

  class Seznam : IEnumerable<User>, IDisposable
  {
    public int Count { get { return 0; } }

    public IEnumerator<User> GetEnumerator()
    {
      yield break;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Dispose()
    {
    }
  }
}
