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

//
// We test here that ?? operators which are compiled to Dup if then pop else .. by C# do not cause
// unwarranted warnings in the extractor.
//

using System.Diagnostics.Contracts;
public class Foo
{
  private string _bar;

  public string Bar
  {
    get
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return _bar;
    }
  }

  public void Bad() {
    this._bar = null;
  }

#if NETFRAMEWORK_3_5
  public Foo() : this(null) {}

  public Foo(string bar)
  {
    Contract.Ensures(Bar == (bar ?? string.Empty));
    _bar = bar ?? string.Empty;
  }
#else
  public Foo(string bar = null)
  {
    Contract.Ensures(Bar == (bar ?? string.Empty));
    _bar = bar ?? string.Empty;
  }
#endif

  [ContractInvariantMethod]
  private void ObjectInvariant()
  {
    Contract.Invariant(_bar != null);
  }
}

namespace Tests.Sources {


  partial class TestMain
  {
    partial void Run()
    {

      var x = new Foo();

      var y = new Foo("test");

      if (!this.behave)
      {
        y.Bad();
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Invariant;
    public string NegativeExpectedCondition = "_bar != null";
  }

}
