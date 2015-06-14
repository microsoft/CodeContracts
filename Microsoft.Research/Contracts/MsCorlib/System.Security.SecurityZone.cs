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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Security {
  // Summary:
  //     Defines the integer values corresponding to security zones used by security
  //     policy.
  [Serializable]
  [ComVisible(true)]
  public enum SecurityZone {
    // Summary:
    //     No zone is specified.
    NoZone = -1,
    //
    // Summary:
    //     The local computer zone is an implicit zone used for content that exists
    //     on the user's computer.
    MyComputer = 0,
    //
    // Summary:
    //     The local intranet zone is used for content located on a company's intranet.
    //     Because the servers and information would be within a company's firewall,
    //     a user or company could assign a higher trust level to the content on the
    //     intranet.
    Intranet = 1,
    //
    // Summary:
    //     The trusted sites zone is used for content located on Web sites considered
    //     more reputable or trustworthy than other sites on the Internet. Users can
    //     use this zone to assign a higher trust level to these sites to minimize the
    //     number of authentication requests. The URLs of these trusted Web sites need
    //     to be mapped into this zone by the user.
    Trusted = 2,
    //
    // Summary:
    //     The Internet zone is used for the Web sites on the Internet that do not belong
    //     to another zone.
    Internet = 3,
    //
    // Summary:
    //     The restricted sites zone is used for Web sites with content that could cause,
    //     or could have caused, problems when downloaded. The URLs of these untrusted
    //     Web sites need to be mapped into this zone by the user.
    Untrusted = 4,
  }
}
