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

using System.Collections;
using System.Diagnostics.Contracts;

namespace System.Collections
{
    // Summary:
    //     Supports a simple iteration over a nongeneric collection.
    public partial interface IEnumerator
    {
        // Summary:
        //     Gets the current element in the collection.
        //
        // Returns:
        //     The current element in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The enumerator is positioned before the first element of the collection or
        //     after the last element.
        
        object Current { get; }

        // Summary:
        //     Advances the enumerator to the next element of the collection.
        //
        // Returns:
        //     true if the enumerator was successfully advanced to the next element; false
        //     if the enumerator has passed the end of the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        bool MoveNext();
        //
        // Summary:
        //     Sets the enumerator to its initial position, which is before the first element
        //     in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        void Reset();

        /// <summary>
        /// Used for static analysis to model the enumeration as a sequence
        /// </summary>
          [ContractModel]
        object[] Model {
          [ContractRuntimeIgnored]
          get;
        }
       
        /// <summary>
        /// Used for static analysis to model current position in sequence
        /// </summary>
        [ContractModel]
        int CurrentIndex { 
          [ContractRuntimeIgnored]
          get;
        }

    }

    #region IEnumerator contract binding
    [ContractClass(typeof(IEnumeratorContract))]
    public partial interface IEnumerator {

    }

    [ContractClassFor(typeof(IEnumerator))]
    abstract class IEnumeratorContract : IEnumerator {
      #region IEnumerator Members

      public object Current {
        get {
          Contract.Ensures(Contract.Result<object>() == this.Model[this.CurrentIndex]);

          throw new NotImplementedException(); }
      }

      public bool MoveNext() {
        Contract.Ensures(this.Model == Contract.OldValue(this.Model));
        Contract.Ensures(this.CurrentIndex < this.Model.Length);
        Contract.Ensures(this.CurrentIndex >= 0);
        Contract.Ensures(this.CurrentIndex == Contract.OldValue(this.CurrentIndex) + 1);

        throw new NotImplementedException();
      }

      public void Reset() {
        throw new NotImplementedException();
      }

      [ContractModel]
      public object[] Model {
        get { throw new NotImplementedException(); }
      }

      #endregion

      #region IEnumerator Members


      [ContractModel]
      public int CurrentIndex {
        get { throw new NotImplementedException(); }
      }

      #endregion
    }
    #endregion

}
