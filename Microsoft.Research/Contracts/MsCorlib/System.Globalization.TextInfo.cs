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

namespace System.Globalization
{

  public class TextInfo
  {
#if false
        public int EBCDICCodePage
        {
          get;
        }

        public int MacCodePage
        {
          get;
        }

        public int ANSICodePage
        {
          get;
        }
#endif

    public virtual string ListSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#if false
        public int OEMCodePage
        {
          get;
        }
#endif

#if !SILVERLIGHT
    public string ToTitleCase(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#endif

    public virtual string ToUpper(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#if false
        public Char ToUpper (Char c) {

          return default(Char);
        }
#endif
    public virtual string ToLower(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#if false
        public Char ToLower (Char c) {
          return default(Char);
        }
#endif
  }
}
