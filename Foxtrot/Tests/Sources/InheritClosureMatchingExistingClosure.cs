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
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Tests.Sources {

  [ContractClass(typeof(ICalculateContract))]
  public interface ICalculate
  {
    float Calculate(Dictionary<string, float> dict);
  }
  
  
  [ContractClassFor(typeof(ICalculate))]
  public abstract class ICalculateContract : ICalculate
  {
    public float Calculate(Dictionary<string, float> dict)
    {
      Contract.Requires<ArgumentNullException>(dict != null);
      Contract.Requires<ArgumentException>(Contract.ForAll(dict, item => item.Value > 0));
      throw new NotImplementedException();
    }
  }
  

  
  public class CalculateNode : ICalculate
  {
    private string _name;
    private string _ensures;
    
    public CalculateNode(string ensures, string name)
    {
      _name = name;
      _ensures = ensures;
    }
    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(!string.IsNullOrEmpty(_name));
      Contract.Invariant(!string.IsNullOrEmpty(_ensures));
    }
    
    public float Calculate(Dictionary<string, float> dict)
    {
      Contract.Ensures(Contract.Exists(dict, item => item.Key == _ensures));
      return 5f;
    }
  }


  partial class TestMain
  {


    partial void Run() {
      var x = new CalculateNode("ensures string", "name string");
      if (this.behave) {
        var dic = new Dictionary<string,float>();
        dic.Add("ensures string", 5.0f);
        x.Calculate(dic);
      }
      else {
        x.Calculate(null);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Precondition failed: dict != null";
  }
}
