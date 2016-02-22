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

using System.Data.Common;
using System.Diagnostics.Contracts;

namespace System.Data.SqlClient
{
	public sealed class SqlParameterCollection : DbParameterCollection
	{
		internal SqlParameterCollection()
		{
		}

		public SqlParameter Add(SqlParameter value)
		{
			Contract.Ensures(value == null || Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public override int Add(object value)
		{
			return default(int);
		}

		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			Contract.Ensures(Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public SqlParameter Add(string parameterName, object value)
		{
			Contract.Ensures(Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			Contract.Ensures(Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			Contract.Ensures(Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public void AddRange(SqlParameter[] values)
		{
			Contract.Requires(values != null);
		}

		public override void AddRange(Array values)
		{
		}

		public SqlParameter AddWithValue(string parameterName, object value)
		{
			Contract.Ensures(Contract.Result<SqlParameter>() != null);
			return default(SqlParameter);
		}

		public override int Count
		{
			get
			{
				return default(int);
			}
		}

		public new SqlParameter this[int index]
		{
			get
			{
				Contract.Ensures(Contract.Result<SqlParameter>() != null);
				return default(SqlParameter);
			}
			set
			{
				Contract.Requires(value != null);
				Contract.Requires(index >= 0);
				Contract.Requires(index < this.Count);
			}
		}

		public new SqlParameter this[string parameterName]
		{
			get
			{
				Contract.Ensures(Contract.Result<SqlParameter>() != null);
				return default(SqlParameter);
			}
			set
			{
				
			}
		}
	}
}