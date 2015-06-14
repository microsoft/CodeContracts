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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace System.Net.Mail
{
  // Summary:
  //     Store e-mail addresses that are associated with an e-mail message.
  public class MailAddressCollection : Collection<MailAddress>
  {
    // Summary:
    //     Initializes an empty instance of the System.Net.Mail.MailAddressCollection
    //     class.
    extern public MailAddressCollection();

    // Summary:
    //     Add a list of e-mail addresses to the collection.
    //
    // Parameters:
    //   addresses:
    //     The e-mail addresses to add to the System.Net.Mail.MailAddressCollection.
    //     Multiple e-mail addresses must be separated with a comma character (",").
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The addresses parameter is null.
    //
    //   System.ArgumentException:
    //     The addresses parameter is an empty string.
    //
    //   System.FormatException:
    //     The addresses parameter contains an e-mail address that is invalid or not
    //     supported.
    public void Add(string addresses)
    {
      Contract.Requires(!String.IsNullOrEmpty(addresses));
      Contract.Ensures(Count > Contract.OldValue(Count));
    }

  }
}

#endif
