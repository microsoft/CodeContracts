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
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Net.Mail
{
  // Summary:
  //     Represents the address of an electronic mail sender or recipient.
  public class MailAddress
  {
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailAddress class using
    //     the specified address.
    //
    // Parameters:
    //   address:
    //     A System.String that contains an e-mail address.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     address is null.
    //
    //   System.ArgumentException:
    //     address is System.String.Empty ("").
    //
    //   System.FormatException:
    //     address is not in a recognized format.
    public MailAddress(string address)
    {
      Contract.Requires(!String.IsNullOrEmpty(address));
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailAddress class using
    //     the specified address and display name.
    //
    // Parameters:
    //   address:
    //     A System.String that contains an e-mail address.
    //
    //   displayName:
    //     A System.String that contains the display name associated with address. This
    //     parameter can be null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     address is null.
    //
    //   System.ArgumentException:
    //     address is System.String.Empty ("").
    //
    //   System.FormatException:
    //     address is not in a recognized format.  -or- address contains non-ASCII characters.
    public MailAddress(string address, string displayName)
    {
      Contract.Requires(!String.IsNullOrEmpty(address));
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailAddress class using
    //     the specified address, display name, and encoding.
    //
    // Parameters:
    //   address:
    //     A System.String that contains an e-mail address.
    //
    //   displayName:
    //     A System.String that contains the display name associated with address.
    //
    //   displayNameEncoding:
    //     The System.Text.Encoding that defines the character set used for displayName.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     address is null.  -or- displayName is null.
    //
    //   System.ArgumentException:
    //     address is System.String.Empty ("").  -or- displayName is System.String.Empty
    //     ("").
    //
    //   System.FormatException:
    //     address is not in a recognized format.  -or- address contains non-ASCII characters.
    public MailAddress(string address, string displayName, Encoding displayNameEncoding)
    {
      Contract.Requires(!String.IsNullOrEmpty(address));
    }

    // Summary:
    //     Gets the e-mail address specified when this instance was created.
    //
    // Returns:
    //     A System.String that contains the e-mail address.
    public string Address
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the display name composed from the display name and address information
    //     specified when this instance was created.
    //
    // Returns:
    //     A System.String that contains the display name; otherwise, System.String.Empty
    //     ("") if no display name information was specified when this instance was
    //     created.
    extern public string DisplayName { get; }
    //
    // Summary:
    //     Gets the host portion of the address specified when this instance was created.
    //
    // Returns:
    //     A System.String that contains the name of the host computer that accepts
    //     e-mail for the System.Net.Mail.MailAddress.User property.
    public string Host
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the user information from the address specified when this instance was
    //     created.
    //
    // Returns:
    //     A System.String that contains the user name portion of the System.Net.Mail.MailAddress.Address.
    public string User
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

  }
}

#endif
