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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Data.Common
{
  public class DbConnectionStringBuilder // : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
  {
    public DbConnectionStringBuilder() { }
    public DbConnectionStringBuilder(bool useOdbcRules) { }

    extern public bool BrowsableConnectionString { get; set; }
    public string ConnectionString { 
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return null;
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public virtual int Count { get { Contract.Ensures(Contract.Result<int>() >= 0); return 0;} }

    extern public virtual bool IsFixedSize { get; }
    extern public virtual bool IsReadOnly { get; }

     // Exceptions:
    //   System.ArgumentNullException:
    //     keyword is a null reference (Nothing in Visual Basic).
    //
    //   System.NotSupportedException:
    //     The property is set, and the System.Data.Common.DbConnectionStringBuilder
    //     is read-only. -or-The property is set, keyword does not exist in the collection,
    //     and the System.Data.Common.DbConnectionStringBuilder has a fixed size.
    public virtual object this[string keyword]
    {
      get
      {
        Contract.Ensures(Contract.Result<object>() != null);
        return null;
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    // Summary:
    //     Adds an entry with the specified key and value into the System.Data.Common.DbConnectionStringBuilder.
    //
    // Parameters:
    //   keyword:
    //     The key to add to the System.Data.Common.DbConnectionStringBuilder.
    //
    //   value:
    //     The value for the specified key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     keyword is a null reference (Nothing in Visual Basic).
    //
    //   System.NotSupportedException:
    //     The System.Data.Common.DbConnectionStringBuilder is read-only. -or-The System.Data.Common.DbConnectionStringBuilder
    //     has a fixed size.
    public void Add(string keyword, object value)
    {
      Contract.Requires(keyword!= null);
      Contract.Requires(!this.IsReadOnly);
      Contract.Requires(!this.IsFixedSize);
    }
    
    public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value) { }
    
    public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules) { }
 
    public virtual void Clear() { }
    //
    // Summary:
    //     Clears the collection of System.ComponentModel.PropertyDescriptor objects
    //     on the associated System.Data.Common.DbConnectionStringBuilder.
    protected internal void ClearPropertyDescriptors() { }
    
    // public virtual bool ContainsKey(string keyword);

    [Pure]
    public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder) { return false; }

    [Pure]
    protected virtual void GetProperties(Hashtable propertyDescriptors) { }

    [Pure]
    public virtual bool Remove(string keyword) { return false; }

    [Pure]
    public virtual bool ShouldSerialize(string keyword) { return false; }

    [Pure]
    public virtual bool TryGetValue(string keyword, out object value)
    {
      Contract.Requires(keyword != null);

      value = null;
      return false;
    }
  }
}
