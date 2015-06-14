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
using System.Diagnostics.Contracts;

namespace System.Runtime.Serialization
{

    public class SerializationInfo
    {

        public string! AssemblyName
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public int MemberCount
        {
          get;
        }

        public string! FullTypeName
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string GetString (string name) {

          return default(string);
        }
        public DateTime GetDateTime (string name) {

          return default(DateTime);
        }
        public Decimal GetDecimal (string name) {

          return default(Decimal);
        }
        public double GetDouble (string name) {

          return default(double);
        }
        public Single GetSingle (string name) {

          return default(Single);
        }
        public UInt64 GetUInt64 (string name) {

          return default(UInt64);
        }
        public Int64 GetInt64 (string name) {

          return default(Int64);
        }
        public UInt32 GetUInt32 (string name) {

          return default(UInt32);
        }
        public int GetInt32 (string name) {

          return default(int);
        }
        public UInt16 GetUInt16 (string name) {

          return default(UInt16);
        }
        public Int16 GetInt16 (string name) {

          return default(Int16);
        }
        public byte GetByte (string name) {

          return default(byte);
        }
        public SByte GetSByte (string name) {

          return default(SByte);
        }
        public Char GetChar (string name) {

          return default(Char);
        }
        public bool GetBoolean (string name) {

          return default(bool);
        }
        public object GetValue (string name, Type! type) {
            CodeContract.Requires(type != null);

          return default(object);
        }
        public void AddValue (string name, DateTime value) {

        }
        public void AddValue (string name, Decimal value) {

        }
        public void AddValue (string name, double value) {

        }
        public void AddValue (string name, Single value) {

        }
        public void AddValue (string name, UInt64 value) {

        }
        public void AddValue (string name, Int64 value) {

        }
        public void AddValue (string name, UInt32 value) {

        }
        public void AddValue (string name, int value) {

        }
        public void AddValue (string name, UInt16 value) {

        }
        public void AddValue (string name, Int16 value) {

        }
        public void AddValue (string name, byte value) {

        }
        public void AddValue (string name, SByte value) {

        }
        public void AddValue (string name, Char value) {

        }
        public void AddValue (string name, bool value) {

        }
        public void AddValue (string name, object value) {

        }
        public void AddValue (string! name, object value, Type! type) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(type != null);

        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public SerializationInfoEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<SerializationInfoEnumerator>() != null);
          return default(SerializationInfoEnumerator);
        }
        public void SetType (Type! type) {
            CodeContract.Requires(type != null);

        }
        public SerializationInfo (Type! type, IFormatterConverter! converter) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(converter != null);
          return default(SerializationInfo);
        }
    }
}
