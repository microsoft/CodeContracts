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

#region Assembly System.Configuration.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Configuration.dll
#endregion

using System;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

namespace System.Configuration.Provider
{
    // Summary:
    //     Provides a base implementation for the extensible provider model.
    public abstract class ProviderBase
    {
        // Summary:
        //     Initializes a new instance of the System.Configuration.Provider.ProviderBase
        //     class.
        extern protected ProviderBase();

        // Summary:
        //     Gets a brief, friendly description suitable for display in administrative
        //     tools or other user interfaces (UIs).
        //
        // Returns:
        //     A brief, friendly description suitable for display in administrative tools
        //     or other UIs.
        extern public virtual string Description { get; }
        //
        // Summary:
        //     Gets the friendly name used to refer to the provider during configuration.
        //
        // Returns:
        //     The friendly name used to refer to the provider during configuration.
        extern public virtual string Name { get; }

        // Summary:
        //     Initializes the provider.
        //
        // Parameters:
        //   name:
        //     The friendly name of the provider.
        //
        //   config:
        //     A collection of the name/value pairs representing the provider-specific attributes
        //     specified in the configuration for this provider.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The name of the provider is null.
        //
        //   System.ArgumentException:
        //     The name of the provider has a length of zero.
        //
        //   System.InvalidOperationException:
        //     An attempt is made to call System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     on a provider after the provider has already been initialized.
        public virtual void Initialize(string name, NameValueCollection config)
        {
            Contract.Requires(!String.IsNullOrEmpty(name));
            Contract.Ensures(this.Name != null);
        }
    }
}
