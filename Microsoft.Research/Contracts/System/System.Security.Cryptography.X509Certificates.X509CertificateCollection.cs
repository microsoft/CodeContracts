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


#if !SILVERLIGHT

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Security.Cryptography.X509Certificates
{
  // Summary:
  //     Defines a collection that stores System.Security.Cryptography.X509Certificates.X509Certificate
  //     objects.
  public class X509CertificateCollection : CollectionBase
  {
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     class.
    extern public X509CertificateCollection();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     class from an array of System.Security.Cryptography.X509Certificates.X509Certificate
    //     objects.
    //
    // Parameters:
    //   value:
    //     The array of System.Security.Cryptography.X509Certificates.X509Certificate
    //     objects with which to initialize the new object.
    public X509CertificateCollection(X509Certificate[] value)
    {
      Contract.Requires(value != null);

    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     class from another System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     with which to initialize the new object.
    public X509CertificateCollection(X509CertificateCollection value)
    {
      Contract.Requires(value != null);
    }

    // Summary:
    //     Gets or sets the entry at the specified index of the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry to locate in the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Returns:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate at the
    //     specified index of the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index parameter is outside the valid range of indexes for the collection.
    public X509Certificate this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(X509Certificate);
      }
      set
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
      }
    }

    // Summary:
    //     Adds an System.Security.Cryptography.X509Certificates.X509Certificate with
    //     the specified value to the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate to add
    //     to the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Returns:
    //     The index into the current System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     at which the new System.Security.Cryptography.X509Certificates.X509Certificate
    //     was inserted.
    public int Add(X509Certificate value)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < this.Count);
      return default(int);
    }
    //
    // Summary:
    //     Copies the elements of an array of type System.Security.Cryptography.X509Certificates.X509Certificate
    //     to the end of the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The array of type System.Security.Cryptography.X509Certificates.X509Certificate
    //     containing the objects to add to the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value parameter is null.
    public void AddRange(X509Certificate[] value)
    {
      Contract.Requires(value != null);
    }
    //
    // Summary:
    //     Copies the elements of the specified System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     to the end of the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     containing the objects to add to the collection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value parameter is null.
    public void AddRange(X509CertificateCollection value)
    {
      Contract.Requires(value != null);
    }
    //
    // Summary:
    //     Gets a value indicating whether the current System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     contains the specified System.Security.Cryptography.X509Certificates.X509Certificate.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate to locate.
    //
    // Returns:
    //     true if the System.Security.Cryptography.X509Certificates.X509Certificate
    //     is contained in this collection; otherwise, false.
    [Pure]
    public bool Contains(X509Certificate value)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Copies the System.Security.Cryptography.X509Certificates.X509Certificate
    //     values in the current System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     to a one-dimensional System.Array instance at the specified index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the values copied
    //     from System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    //   index:
    //     The index into array to begin copying.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The array parameter is multidimensional.  -or- The number of elements in
    //     the System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     is greater than the available space between arrayIndex and the end of array.
    //
    //   System.ArgumentNullException:
    //     The array parameter is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The arrayIndex parameter is less than the array parameter's lower bound.
    public void CopyTo(X509Certificate[] array, int index)
    {
      Contract.Requires(array != null);
      //Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= 0);
      Contract.Requires(index < array.Length);
    }
    //
    // Summary:
    //     Returns an enumerator that can iterate through the System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Returns:
    //     An enumerator of the subelements of System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     you can use to iterate through the collection.
    public new X509CertificateEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<X509CertificateEnumerator>() != null);
      return default(X509CertificateEnumerator);
    }
    //
    // Summary:
    //     Returns the index of the specified System.Security.Cryptography.X509Certificates.X509Certificate
    //     in the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate to locate.
    //
    // Returns:
    //     The index of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     specified by the value parameter in the System.Security.Cryptography.X509Certificates.X509CertificateCollection,
    //     if found; otherwise, -1.
    [Pure]
    public int IndexOf(X509Certificate value)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < this.Count);
      return default(int);
    }
    //
    // Summary:
    //     Inserts a System.Security.Cryptography.X509Certificates.X509Certificate into
    //     the current System.Security.Cryptography.X509Certificates.X509CertificateCollection
    //     at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index where value should be inserted.
    //
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate to insert.
    public void Insert(int index, X509Certificate value)
    {
      Contract.Requires(index >= 0);
    }
    //
    // Summary:
    //     Removes a specific System.Security.Cryptography.X509Certificates.X509Certificate
    //     from the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Parameters:
    //   value:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate to remove
    //     from the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Security.Cryptography.X509Certificates.X509Certificate specified
    //     by the value parameter is not found in the current System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    extern public void Remove(X509Certificate value);

    // Summary:
    //     Enumerates the System.Security.Cryptography.X509Certificates.X509Certificate
    //     objects in an System.Security.Cryptography.X509Certificates.X509CertificateCollection.
    public class X509CertificateEnumerator // : IEnumerator
    {
    }
  }
}

#endif
