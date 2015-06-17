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

    public class ObjectManager
    {

        public void RaiseDeserializationEvent () {

        }
        public void RecordArrayElementFixup (Int64 arrayToBeFixed, Int32[] indices, Int64 objectRequired) {
            Contract.Requires(arrayToBeFixed > 0);
            Contract.Requires(objectRequired > 0);
            Contract.Requires(indices != null);

        }
        public void RecordArrayElementFixup (Int64 arrayToBeFixed, int index, Int64 objectRequired) {

        }
        public void RecordDelayedFixup (Int64 objectToBeFixed, string memberName, Int64 objectRequired) {
            Contract.Requires(objectToBeFixed > 0);
            Contract.Requires(objectRequired > 0);
            Contract.Requires(memberName != null);

        }
        public void RecordFixup (Int64 objectToBeFixed, System.Reflection.MemberInfo member, Int64 objectRequired) {
            Contract.Requires(objectToBeFixed > 0);
            Contract.Requires(objectRequired > 0);
            Contract.Requires(member != null);

        }
        public void DoFixups () {

        }
        public void RegisterObject (object obj, Int64 objectID, SerializationInfo info, Int64 idOfContainingObj, System.Reflection.MemberInfo member, Int32[] arrayIndex) {
            Contract.Requires(obj != null);
            Contract.Requires(objectID > 0);

        }
        public void RegisterObject (object obj, Int64 objectID, SerializationInfo info, Int64 idOfContainingObj, System.Reflection.MemberInfo member) {

        }
        public void RegisterObject (object obj, Int64 objectID, SerializationInfo info) {

        }
        public void RegisterObject (object obj, Int64 objectID) {

        }
        public object GetObject (Int64 objectID) {
            Contract.Requires(objectID > 0);

          return default(object);
        }
        public ObjectManager (ISurrogateSelector selector, StreamingContext context) {
          return default(ObjectManager);
        }
    }
}
