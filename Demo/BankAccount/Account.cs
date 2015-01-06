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

namespace Bank
{
  public class Account
  {
    public bool SupportsOverdraft { get { return _supportsOverdraft; } }
    public float OverdraftLimit
    {
      get
      {
        Contract.Ensures(Contract.Result<float>() >= 0);

        return _overdraftLimit;
      }
    }
    public float Amount
    {
      get
      {
        return _amount;
      }
    }

    private float _amount;
    private readonly bool _supportsOverdraft;
    private readonly float _overdraftLimit;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this._supportsOverdraft ? this._overdraftLimit > 0 : this._overdraftLimit == 0);
      Contract.Invariant(this._overdraftLimit <= 1000);
      Contract.Invariant(this._amount > -this._overdraftLimit);
    }

    public Account(float openingAmount, bool supportsOverdraft = false, float overdraftLimit = 0)
    {
      Contract.Requires(openingAmount > -overdraftLimit);
      Contract.Requires(supportsOverdraft ? overdraftLimit > 0 : overdraftLimit == 0);
      Contract.Requires(overdraftLimit <= 1000);

      Contract.Ensures(this.Amount == openingAmount);
      Contract.Ensures(this.OverdraftLimit == overdraftLimit);

      this._amount = openingAmount;
      this._supportsOverdraft = supportsOverdraft;
      this._overdraftLimit = overdraftLimit;
    }

    public void Deposit(float deposit)
    {
      Contract.Requires(deposit > 0.0);
      this._amount = this._amount + deposit;
    }

    public void Withdraw(float withdrawAmount)
    {
      Contract.Requires(withdrawAmount <= this.Amount + this.OverdraftLimit);
      Contract.Ensures(this.Amount == Contract.OldValue(this.Amount) - withdrawAmount);

      this._amount = this._amount - withdrawAmount;
    }


  }

}
