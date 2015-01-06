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
    
    public interface ISurrogateSelector
    {
        void ChainSelector(ISurrogateSelector selector);
        /*
            Contract.Requires(selector != null);
            Contract.Requires(selector != this);
            */

        ISurrogateSelector GetNextSelector();

        ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);
        // FIXME - why aren't these allowed?
           // Contract.Requires(type != null);
            //Contract.Requires(surrogate != null);
    }
    

    public class SurrogateSelector : ISurrogateSelector
    {

        public void RemoveSurrogate (Type type, StreamingContext context) {
            Contract.Requires(type != null);

        }
        public ISerializationSurrogate GetSurrogate (Type type, StreamingContext context, out ISurrogateSelector selector) {
            //Contract.Requires(type != null);

          return default(ISerializationSurrogate);
        }
        public ISurrogateSelector GetNextSelector () {

          return default(ISurrogateSelector);
        }
        public void ChainSelector (ISurrogateSelector selector) {
        /*
            Contract.Requires(selector != null);
            Contract.Requires(selector != this);
            */

        }
        public void AddSurrogate (Type type, StreamingContext context, out ISerializationSurrogate surrogate) {
            Contract.Requires(type != null);
            Contract.Requires(surrogate != null);
        }
        public SurrogateSelector () {
          return default(SurrogateSelector);
        }
    }
}
