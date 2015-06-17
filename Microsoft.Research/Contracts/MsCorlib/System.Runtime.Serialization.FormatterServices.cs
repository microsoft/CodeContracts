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

using System.Diagnostics.Contracts;
using System;

namespace System.Runtime.Serialization
{

    public class FormatterServices
    {

        public static Type GetTypeFromAssembly(System.Reflection.Assembly assem, string name)
        {
            Contract.Requires(assem != null);

            return default(Type);
        }
        public static Object[] GetObjectData(object obj, System.Reflection.MemberInfo[] members)
        {
            Contract.Requires(obj != null);
            Contract.Requires(members != null);

            return default(Object[]);
        }
        public static object PopulateObjectMembers(object obj, System.Reflection.MemberInfo[] members, Object[] data)
        {
            Contract.Requires(obj != null);
            Contract.Requires(members != null);
            Contract.Requires(data != null);
            Contract.Requires(members.Length == data.Length);

            return default(object);
        }
        public static object GetSafeUninitializedObject(Type type)
        {
            Contract.Requires(type != null);

            return default(object);
        }
        public static object GetUninitializedObject(Type type)
        {
            Contract.Requires(type != null);

            return default(object);
        }
#if false
        public static void CheckTypeSecurity(Type t, System.Runtime.Serialization.Formatters.TypeFilterLevel securityLevel)
        {

        }
#endif
        public static System.Reflection.MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
        {
            Contract.Requires(type != null);

            return default(System.Reflection.MemberInfo[]);
        }

        public static System.Reflection.MemberInfo[] GetSerializableMembers(Type type)
        {
        }
    }
}
