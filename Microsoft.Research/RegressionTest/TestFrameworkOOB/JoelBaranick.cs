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

//--------------------------------------------------------------------------
// <copyright file="StoreBase.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>This file contains the code for the StoreBase class.</summary>
//--------------------------------------------------------------------------
namespace Microsoft.Advertising.Common.Caching
{
  #region References

  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.Globalization;
  using System.IO;
  using System.Xml.Serialization;
  using Microsoft.Research.ClousotRegression;

  #endregion

  /// <summary>
  /// The base class for all stores.
  /// </summary>
  /// <typeparam name="TKey">The type of the key.</typeparam>
  /// <typeparam name="TStatus">The type of the status.</typeparam>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  [ContractClass(typeof(StoreBaseContract<,,>))]
  public abstract class StoreBase<TKey, TStatus, TValue> where TStatus : IComparable<TStatus>
  {
    #region Protected Methods

    /// <summary>
    /// Writes the specified value to storage.
    /// </summary>
    /// <param name="value">The value.</param>
    protected abstract void WriteToStorageInternal(StoreValue<TKey, TStatus, TValue> value);

    #endregion
  }

  [ContractClassFor(typeof(StoreBase<,,>))]
  internal abstract class StoreBaseContract<TKey, TStatus, TValue> : StoreBase<TKey, TStatus, TValue> where TStatus : IComparable<TStatus>
  {
    /// <summary>
    /// Writes the specified value to storage.
    /// </summary>
    /// <param name="value">The value.</param>
    [ClousotRegressionTest]
    protected override void WriteToStorageInternal(StoreValue<TKey, TStatus, TValue> value)
    {
      Contract.Requires(value != null);
      Contract.Requires(!Equals(value.Key, null));
    }
  }

  /// <summary>
  /// A disk based implementation of <see cref="StoreBase{TKey,TStatus,TValue}"/>.
  /// </summary>
  /// <typeparam name="TKey">The type of the key.</typeparam>
  /// <typeparam name="TStatus">The type of the status.</typeparam>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  public class DiskStore<TKey, TStatus, TValue> : StoreBase<TKey, TStatus, TValue> where TStatus : IComparable<TStatus>
  {
    #region Private Fields

    private readonly object syncObj = new object();
    private readonly string storeDirectory;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DiskStore{TKey, TStatus, TValue}"/> class.
    /// </summary>
    /// <param name="storeDirectory">The store directory.</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 42)]
    public DiskStore(string storeDirectory)
    {
      Contract.Requires(!String.IsNullOrEmpty(storeDirectory));

      this.storeDirectory = storeDirectory;
    }

    #endregion

    #region Internal Methods

    /// <summary>
    /// Gets the store filename.
    /// </summary>
    /// <param name="key">The store key.</param>
    /// <returns>The path to the store file.</returns>
    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 90, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 99, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 104, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 119, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 145, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 76, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 90, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 90, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 92)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 92)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 112, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 125)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 125)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 138, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 153, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 184)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 35, MethodILOffset = 184)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 59, MethodILOffset = 184)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=76,MethodILOffset=0)]
    internal string GetStoreFilename(TKey key)
    {
      Contract.Requires(!Equals(null, key));
      Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
      Contract.Ensures(!String.IsNullOrEmpty(Path.GetDirectoryName(Contract.Result<string>())));

      string fileName = string.Format(CultureInfo.InvariantCulture, "{0}.xml", key);
      Contract.Assert(this.storeDirectory.Length > 0);
      var result = Path.Combine(this.storeDirectory, fileName);
      Contract.Assert(result != null);
      Contract.Assert(result.Length > 0);
      Contract.Assume(!String.IsNullOrEmpty(Path.GetDirectoryName(result)));
      return result;
    }

    /// <summary>
    /// Ensures the store directory exists.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 44)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=15,MethodILOffset=74)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=15,MethodILOffset=85)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=38,MethodILOffset=85)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=15,MethodILOffset=113)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 44)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 70)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 81)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 36, MethodILOffset = 81)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 101)]
#endif
    internal void EnsureStoreDirectoryExists(string fileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
      Contract.Requires(!String.IsNullOrEmpty(Path.GetDirectoryName(fileName)));

      var directoryName = Path.GetDirectoryName(fileName);
      if (!Directory.Exists(directoryName))
      {
        lock (this.syncObj)
        {
          if (!Directory.Exists(directoryName))
          {
            Directory.CreateDirectory(directoryName);
          }
        }
      }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Writes the specified value to storage.
    /// </summary>
    /// <param name="value">The value.</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 16, MethodILOffset = 8)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 14)]
    protected override void WriteToStorageInternal(StoreValue<TKey, TStatus, TValue> value)
    {
      var fileName = this.GetFileName(value.Key);
    }

    #endregion

    #region Private Methods

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 16, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 30, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 43)]
    private string GetFileName(TKey key)
    {
      Contract.Requires(!Equals(key, null));

      var fileName = this.GetStoreFilename(key);
      this.EnsureStoreDirectoryExists(fileName);

      return fileName;
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(!String.IsNullOrEmpty(this.storeDirectory));
    }

    #endregion
  }

  /// <summary>
  /// The container for store status.
  /// </summary>
  /// <typeparam name="TStatus">The type of the status.</typeparam>
  [Serializable]
  public class StoreStatus<TStatus> : IComparable<StoreStatus<TStatus>>, IEquatable<StoreStatus<TStatus>> where TStatus : IComparable<TStatus>
  {
    #region Private Fields

    private bool isEmpty;
    private TStatus status;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreStatus{TStatus}"/> class.
    /// </summary>
    public StoreStatus()
    {
      this.isEmpty = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreStatus{TStatus}"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    public StoreStatus(TStatus status)
    {
      this.Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreStatus{TStatus}"/> class.
    /// </summary>
    /// <param name="storeStatus">The store status.</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
    public StoreStatus(StoreStatus<TStatus> storeStatus)
    {
      Contract.Requires(storeStatus != null);

      this.Status = storeStatus.Status;
      this.PreventRefresh = false;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>The status.</value>
    public TStatus Status
    {
      get
      {
        return this.status;
      }

      set
      {
        this.isEmpty = ReferenceEquals(value, null);
        this.status = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether refresh should be prevented.
    /// </summary>
    /// <value><c>True</c> if refresh should be prevented; otherwise, <c>false</c>.</value>
    [XmlAttribute("PreventRefresh")]
    public bool PreventRefresh { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left status.</param>
    /// <param name="right">The right status.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(StoreStatus<TStatus> left, StoreStatus<TStatus> right)
    {
      return object.Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left status.</param>
    /// <param name="right">The right status.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(StoreStatus<TStatus> left, StoreStatus<TStatus> right)
    {
      return !object.Equals(left, right);
    }

    /// <summary>
    /// Implements the operator &gt;.
    /// </summary>
    /// <param name="left">The left status.</param>
    /// <param name="right">The right status.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator >(StoreStatus<TStatus> left, StoreStatus<TStatus> right)
    {
      if (left == null)
      {
        return false;
      }

      if (right == null)
      {
        return true;
      }

      return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Implements the operator &lt;.
    /// </summary>
    /// <param name="left">The left status.</param>
    /// <param name="right">The right status.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator <(StoreStatus<TStatus> left, StoreStatus<TStatus> right)
    {
      if (left == null)
      {
        return true;
      }

      if (right == null)
      {
        return false;
      }

      return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Compares the current object with another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.</returns>
    public int CompareTo(StoreStatus<TStatus> other)
    {
      if (other == null)
      {
        return 1;
      }

      if (this.isEmpty)
      {
        if (other.isEmpty)
        {
          return 0;
        }

        return -1;
      }

      if (other.isEmpty)
      {
        return 1;
      }

      return this.Status.CompareTo(other.Status);
    }

    /// <summary>
    /// Determines whether the specified <see cref="StoreStatus{TStatus}"/> is equal to the current <see cref="StoreStatus{TStatus}"/>.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// True if the specified <see cref="StoreStatus{TStatus}"/> is equal to the current <see cref="StoreStatus{TStatus}"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
    public bool Equals(StoreStatus<TStatus> other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return object.Equals(other.isEmpty, this.isEmpty) && object.Equals(other.Status, this.Status);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param>
    /// <filterpriority>2</filterpriority>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != typeof(StoreStatus<TStatus>))
      {
        return false;
      }

      return this.Equals((StoreStatus<TStatus>)obj);
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      int result = this.isEmpty.GetHashCode();
      result = (result * 397) ^ (!ReferenceEquals(this.Status, null) ? this.Status.GetHashCode() : 0);
      return result;
    }

    #endregion
  }

  /// <summary>
  /// The store value.
  /// </summary>
  /// <typeparam name="TKey">The type of the key.</typeparam>
  /// <typeparam name="TStatus">The type of the status.</typeparam>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  [Serializable]
  [XmlRoot("Store")]
  public class StoreValue<TKey, TStatus, TValue> : IEquatable<StoreValue<TKey, TStatus, TValue>> where TStatus : IComparable<TStatus>
  {
    #region Private Fields

    private TKey key;
    private StoreStatus<TStatus> status;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreValue{TKey,TStatus,TValue}"/> class.
    /// </summary>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 32)]
    public StoreValue()
      : this(default(TKey), new StoreStatus<TStatus>(), default(TValue))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreValue{TKey,TStatus,TValue}"/> class.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="status">The status.</param>
    /// <param name="value">The value.</param>
    [ClousotRegressionTest]
    //[ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 39, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 46)]
    public StoreValue(TKey key, StoreStatus<TStatus> status, TValue value)
    {
      Contract.Requires(status != null);

      this.Key = key;
      this.Status = status;
      this.Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreValue{TKey,TStatus,TValue}"/> class.
    /// </summary>
    /// <param name="storeValue">The store value.</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 41)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 46)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 66)]
    public StoreValue(StoreValue<TKey, TStatus, TValue> storeValue)
    {
      Contract.Requires(storeValue != null);

      this.Key = storeValue.Key;
      this.Status = new StoreStatus<TStatus>(storeValue.Status);
      this.Value = storeValue.Value;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>The store key.</value>
    [XmlElement("StoreKey")]
    public TKey Key
    {
      get
      {
        return this.key;
      }

      set
      {
        this.key = value;
      }
    }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>The status.</value>
    [XmlElement("StoreStatus")]
    public StoreStatus<TStatus> Status
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 19, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 28)]
      get
      {
        Contract.Ensures(Contract.Result<StoreStatus<TStatus>>() != null);

        return this.status;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 26)]
      set
      {
        Contract.Requires(value != null, "Store status cannot be set to null.");

        this.status = value;
      }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [XmlElement("StoreValue")]
    public TValue Value { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(StoreValue<TKey, TStatus, TValue> left, StoreValue<TKey, TStatus, TValue> right)
    {
      return object.Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(StoreValue<TKey, TStatus, TValue> left, StoreValue<TKey, TStatus, TValue> right)
    {
      return !object.Equals(left, right);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>True</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.</returns>
    public bool Equals(StoreValue<TKey, TStatus, TValue> other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return object.Equals(other.Key, this.Key) && object.Equals(other.Status, this.Status) && object.Equals(other.Value, this.Value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
    /// <returns><c>True</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
    /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != typeof(StoreValue<TKey, TStatus, TValue>))
      {
        return false;
      }

      return this.Equals((StoreValue<TKey, TStatus, TValue>)obj);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
      unchecked
      {
        int result = this.Key.GetHashCode();
        result = (result * 397) ^ (this.Status != null ? this.Status.GetHashCode() : 0);
        result = (result * 397) ^ this.Value.GetHashCode();
        return result;
      }
    }

    #endregion

    #region Private Methods

    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The 'this' parameter is used and so this cannot be static.")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.status != null);
    }

    #endregion
  }
}
